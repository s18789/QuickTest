using MediatR;
using QuickTest.Application.Common.Enums;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetExamsResults;

public class GetExamsResultsHandler : IRequestHandler<GetExamsResultsRequest, IEnumerable<GetExamResultDto>>
{
    private readonly IExamResultRepository examResultRepository;

    public GetExamsResultsHandler(IExamResultRepository examResultRepository)
    {
        this.examResultRepository = examResultRepository;
    }

    public async Task<IEnumerable<GetExamResultDto>> Handle(GetExamsResultsRequest request, CancellationToken cancellationToken)
    {
        var examsResults = await this.examResultRepository.GetStudentExamsResults(request.StudentId);

        return examsResults.Select(x => new GetExamResultDto
        {
            Id = x.Id,
            ExamName = x.Exam.Title,
            Status = GetExamResultExam(x),
            Score = x.Score,
            EndingDate = x.Exam.AvailableTo,
        });
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
}
