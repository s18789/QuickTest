using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.StartExam;

public class StartExamHandler : IRequestHandler<StartExamRequest, StartExamDto>
{
    private readonly IExamResultRepository examResultRepository;

    public StartExamHandler(IExamResultRepository examResultRepository)
    {
        this.examResultRepository = examResultRepository;
    }

    public async Task<StartExamDto> Handle(StartExamRequest request, CancellationToken cancellationToken)
    {
        var examResult = await this.examResultRepository.GetExamResultToSolve(request.ExamResultId);

        if (examResult.StartExamTime == null && examResult.FinishExamTime == null)
        {
            examResult = await this.examResultRepository.StartExam(examResult);
        }

        return new StartExamDto
        {
            ExamResultId = examResult.Id,
            ExamId = examResult.ExamId,
            StartDate = examResult.StartExamTime,
            FinishDate = examResult.FinishExamTime,
            Questions = examResult.Exam.Questions.Select(q => new StartExamQuestionDto
            {
                QuestionId = q.Id,
                Content = q.QuestionContent,
                Type = q.Type,
                Answers = q.PredefinedAnswers.Select(a => new StartExamAnswerDto
                {
                    AnswerId = a.Id,
                    Content = a.Content,
                    IsSelected = false
                })
            })
        };
    }
}
