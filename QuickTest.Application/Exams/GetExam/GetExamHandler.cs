using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Exams.GetExam;

public class GetExamHandler : IRequestHandler<GetExamRequest, ExamDto>
{
    private readonly IExamRepository examRepository;

    public GetExamHandler(IExamRepository examRepository)
    {
        this.examRepository = examRepository;
    }

    public async Task<ExamDto> Handle(GetExamRequest request, CancellationToken cancellationToken)
    {
        var exam = await examRepository.GetExamIncludeExamResultsAndQuestions(request.Id);

        return new ExamDto
        {
            Id = exam.Id,
            Name = exam.Title,
            Status = DateTime.Compare(exam.AvailableTo, DateTime.Now) > 0 ? "Active" : "Inactive",
            Category = "1 Biol-Chem",
            QuestionNumber = exam.Questions.Count(),
            AvailableFrom = exam.AvailableFrom,
            AvailableTo = exam.AvailableTo,
            Time = exam.Time,
            ExamResults = exam.ExamResults.Select(er => new ExamResultDto
            {
                Id = er.FinishExamTime is null ? null : er.Id,
                FullName = $"{er.Student.FirstName} {er.Student.LastName}",
                Email = er.Student.Email,
                Status = er.FinishExamTime is null
                    ? "Not resolved"
                    : "Completed",
                FinishTime = er.FinishExamTime,
                Score = er.Score
            }).OrderBy(er => er.Status).ThenBy(er => er.Score)
        };
    }
}
