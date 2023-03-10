using MediatR;
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
            return new GetExamResultDto { Status = "Active" };
        }

        return  new GetExamResultDto
        {
            Status = "Inactive",
            MaxPoints = examResult.Exam.MaxPoints,
            Score = examResult.Score,
            QuestionCount = examResult.Exam.Questions.Count(),
            CorrectAnswers = examResult.StudentAnswers.Where(x => x.Points != 0).Count(),
            WrongAnswers = examResult.StudentAnswers.Where(x => x.Points == 0).Count(),
            StartTime = examResult.StartExamTime,
            EndTime = examResult.FinishExamTime,
        };
    }
}
