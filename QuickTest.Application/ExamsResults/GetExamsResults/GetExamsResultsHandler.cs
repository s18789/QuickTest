using MediatR;
using QuickTest.Application.Common.Enums;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetExamsResults;

public class GetExamsResultsHandler : IRequestHandler<GetExamsResultsRequest, ExamResultDto>
{
    private readonly IExamResultRepository examResultRepository;

    public GetExamsResultsHandler(IExamResultRepository examResultRepository)
    {
        this.examResultRepository = examResultRepository;
    }

    public async Task<ExamResultDto> Handle(GetExamsResultsRequest request, CancellationToken cancellationToken)
    {
        var examsResults = await this.examResultRepository.GetStudentExamsResults(request.StudentId);
        var lastCompleted = this.GetLastCompletedExam(examsResults);

        var groupStudentsAverages = await this.examResultRepository.GetGroupStudentsAverageScore(request.StudentId);

        return new ExamResultDto
        {
            LastCompleted = lastCompleted is null 
                ? null 
                : new LastCompletedDto
                    {
                        Title = lastCompleted.Exam.Title,
                        CompletionDate = lastCompleted.FinishExamTime.Value,
                        Score = lastCompleted.Score / lastCompleted.Exam.MaxPoints * 100 ?? null,
                        ComparisonToOthers = lastCompleted?.Score is not null
                            ? lastCompleted.Score / lastCompleted.Exam.MaxPoints * 100 - ((await this.examResultRepository.GetExamAverageScore(lastCompleted.ExamId) ?? 0) / lastCompleted.Exam.MaxPoints * 100)
                            : null,
                    },
            StudentAverage = await this.examResultRepository.GetStudentAverageScore(request.StudentId),
            Best = groupStudentsAverages?.Max(x => x.Item2),
            Average = groupStudentsAverages?.Average(x => x.Item2),
            Worst = groupStudentsAverages?.Min(x => x.Item2),
            ExamsResultsGridItems = examsResults.Select(x => new ExamResultGridItemDto
            {
                Id = x.Id,
                ExamName = x.Exam.Title,
                Status = this.GetExamResultExam(x),
                Score = x.Score / x.Exam.MaxPoints * 100,
                EndingDate = x.Exam.AvailableTo,
            })
        };
    }

    private ExamResultStatus GetExamResultExam(ExamResult examResult)
    {
        if (examResult.FinishExamTime is null)
        {
            return  ExamResultStatus.NotResolved;
        }

        if (examResult.Score is null)
        {
            return ExamResultStatus.ToCheck;
        }

        return ExamResultStatus.Completed;
    }

    private ExamResult? GetLastCompletedExam(IEnumerable<ExamResult?> examsResults)
    {
        return examsResults
            .Where(x => x.FinishExamTime != null)
            .OrderByDescending(x => x.FinishExamTime)
            .FirstOrDefault(); ;
    }
}
