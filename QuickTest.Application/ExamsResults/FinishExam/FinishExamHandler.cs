using MediatR;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.FinishExam;

public class FinishExamHandler : IRequestHandler<FinishExamRequest>
{
    private readonly IQuestionRepository questionRepository;
    private readonly IExamResultRepository examResultRepository;
    private readonly IStudentAnswerRepository studentAnswerRepository;
    private readonly IPredefinedAnswerRepository predefinedAnswerRepository;

    public FinishExamHandler(
        IQuestionRepository questionRepository,
        IExamResultRepository examResultRepository, 
        IStudentAnswerRepository studentAnswerRepository,
        IPredefinedAnswerRepository predefinedAnswerRepository)
    {
        this.questionRepository = questionRepository;
        this.examResultRepository = examResultRepository;
        this.studentAnswerRepository = studentAnswerRepository;
        this.predefinedAnswerRepository = predefinedAnswerRepository;
    }

    public async Task<Unit> Handle(FinishExamRequest request, CancellationToken cancellationToken)
    {
        var score = 0.0;

        foreach (var question in request.Exam.Questions)
        {
            var qu = await this.questionRepository.GetByIdAsync(question.QuestionId);
            int correctAnswers = 0;

            var studentAnswer = new StudentAnswer()
            {
                ExamResultId = request.Exam.ExamResultId,
                SelectedStudentAnswers = question.Answers.Select(x => new SelectedStudentAnswer()
                {
                    PredefinedAnswerId = x.AnswerId,
                    IsSelected = x.IsSelected,
                }).ToList()
            };


            foreach (var answer in question.Answers)
            {
                var predefinedAnswer = await this.predefinedAnswerRepository.GetByIdAsync(answer.AnswerId);

                if (predefinedAnswer.IsCorrect == answer.IsSelected)
                {
                    correctAnswers++;
                }
            }

            var questionPoints = correctAnswers == question.Answers.Count()
                ? qu.Points
                : 0;

            studentAnswer.Points = questionPoints;
            score += questionPoints;

            await this.studentAnswerRepository.AddAsync(studentAnswer);
        }

        var examResult = await this.examResultRepository.GetByIdAsync(request.Exam.ExamResultId);

        examResult.Score = score;
        examResult.FinishExamTime = DateTime.Now;

        await this.examResultRepository.UpdateAsync(examResult);

        return Unit.Value;
    }
}
