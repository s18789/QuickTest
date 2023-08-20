using MediatR;
using QuickTest.Application.Common.Enums;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Exams.GetExams;
public class GetExamsHandler : IRequestHandler<GetExamsRequest, IEnumerable<ExamDTO>>
{
    private readonly IExamRepository examRepository;

    public GetExamsHandler(IExamRepository examRepository)
    {
        this.examRepository = examRepository;
    }

    public async Task<IEnumerable<ExamDTO>> Handle(GetExamsRequest request, CancellationToken cancellationToken)
    {
        var exams = await this.examRepository.GetAllExams();

        return exams.Select(x => new ExamDTO
        {
            Id = x.Id,
            Title = x.Title,
            Status = DateTime.Compare(x.AvailableTo, DateTime.Now) > 0 ? ExamStatus.Active : ExamStatus.Inactive,
            Class = "1 Biol-Chem",
            CompletedExams = x.ExamResults.Where(x => x.FinishExamTime != null).Count(),
            AllExams = x.ExamResults.Count(),
            AvailableTo = x.AvailableTo
        });
    }
}
