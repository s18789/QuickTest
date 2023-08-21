using MediatR;
using QuickTest.Application.Services;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Exams.CreateExam;
public class CreateExamHandler : IRequestHandler<CreateExamRequest, CreateExamDto>
{
    private readonly IExamRepository examRepository;
    private readonly IUserContextService userContextService;

    public CreateExamHandler(IExamRepository examRepository, IUserContextService userContextService)
    {
        this.examRepository = examRepository;
        this.userContextService = userContextService;
    }

    public async Task<CreateExamDto> Handle(CreateExamRequest request, CancellationToken cancellationToken)
    {
        var user = await this.userContextService.GetUserContextAsync();

        await this.examRepository
            .AddAsync(new Exam()
            {
                Title = request.Exam.Title,
                Time = request.Exam.Time,
                CreationDate = DateTime.Now,
                AvailableFrom = request.Exam.AvailableFrom,
                AvailableTo = request.Exam.AvailableTo,
                MaxPoints = request.Exam.Questions.Sum(x => x.Points),
                Description = "", // do usuniecia z bazy
                Questions = request.Exam.Questions
                    .Select(x => new Question()
                    {
                        QuestionContent = x.QuestionContent,
                        Type = x.Type,
                        Points = x.Points,
                        PredefinedAnswers = x.Answers.Select(a => new PredefinedAnswer()
                        {
                            Content = a.AnswerContent,
                            IsCorrect = a.Correct == true
                        }).ToList()
                    }).ToList(),
                ExamResults = request.Exam.Students.Select(x => new ExamResult()
                {
                    StudentId = x.Id
                }).ToList(),
                TeacherId = user.Id
            });

        return request.Exam;
    }
}