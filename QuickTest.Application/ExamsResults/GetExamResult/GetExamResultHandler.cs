using MediatR;
using QuickTest.Application.Common.Enums;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetExamResult;

public class GetExamResultHandler : IRequestHandler<GetExamResultRequest, GetExamResultDto>
{
    private readonly IExamResultRepository examResultRepository;

    public GetExamResultHandler(IExamResultRepository examResultRepository)
    {
        this.examResultRepository = examResultRepository;
    }

    public async Task<GetExamResultDto> Handle(GetExamResultRequest request, CancellationToken cancellationToken)
    {
        var examResult = await this.examResultRepository.GetExamResultById(request.ExamResultId);

        if (examResult.FinishExamTime is null)
        {
            return new GetExamResultDto { Status = ExamResultStatus.NotResolved };
        }

        if (examResult.Score is null)
        {
            return new GetExamResultDto { Status = ExamResultStatus.ToCheck };
        }

        return  new GetExamResultDto
        {
            Status = ExamResultStatus.Completed,
            Score = examResult.Score,
            MaxPoints = examResult.Exam.MaxPoints,
            ClosedQuestionMaxPoints = examResult.Exam.Questions.Where(x => x.Type != QuestionType.Open).Sum(q => q.Points),
            CorrectOpenQuestions = examResult.StudentAnswers.Where(x => x.QuestionId != null).Sum(x => x.Points),
            CorrectClosedQuestions = examResult.StudentAnswers.Where(x => x.QuestionId == null).Sum(x => x.Points),
            StartTime = examResult.StartExamTime,
            EndTime = examResult.FinishExamTime,
        };
    }
}
