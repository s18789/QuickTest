using MediatR;
using QuickTest.Application.Common.Enums;
using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;
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

        var result = new ExamDto
        {
            Id = exam.Id,
            Title = exam.Title,
            Status = DateTime.Compare(exam.AvailableTo, DateTime.Now) > 0 ? ExamStatus.Active : ExamStatus.Inactive,
            QuestionsCount = exam.Questions.Count(),
            AvailableFrom = exam.AvailableFrom,
            AvailableTo = exam.AvailableTo,
            Average = exam.ExamResults.Where(er => er.Score != null).Average(er => er.Score) / exam.MaxPoints * 100,
            ExamResults = exam.ExamResults.Select(er => new ExamResultDto
            {
                Id = er.FinishExamTime is null ? null : er.Id,
                FullName = $"{er.Student.FirstName} {er.Student.LastName}",
                Email = er.Student.Email,
                Status = this.GetExamResultStatus(er, exam.Questions),
                FinishTime = er.FinishExamTime,
                Score = er.Score / exam.MaxPoints * 100,
            }).OrderBy(er => er.Status).ThenBy(er => er.Score)
        };

        if (exam.ExamResults.Any())
        {
            var hardestQuestion = await examRepository.FindTheHardestQuestion(request.Id);
            result.HardQuestion = new QuestionDto
            {
                Index = hardestQuestion.Item1,
                Average = hardestQuestion.Item2,
            };
        }

        return result;
    }

    private ExamResultStatus GetExamResultStatus(ExamResult examResult, IEnumerable<Question> questions)
    {
        if (examResult.FinishExamTime is null)
        {
            return ExamResultStatus.NotResolved;
        }

        return questions.Any(x => x.Type == QuestionType.Open) && examResult.Score is null 
            ? ExamResultStatus.ToCheck 
            : ExamResultStatus.Completed;
    }
}
