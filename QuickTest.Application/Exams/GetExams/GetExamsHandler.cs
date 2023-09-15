using MediatR;
using QuickTest.Application.Common.Enums;
using QuickTest.Application.Services;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Exams.GetExams;
public class GetExamsHandler : IRequestHandler<GetExamsRequest, IEnumerable<ExamDTO>>
{
    private readonly IUserContextService userContextService;
    private readonly IExamRepository examRepository;

    public GetExamsHandler(IExamRepository examRepository, IUserContextService userContextService)
    {
        this.examRepository = examRepository;
        this.userContextService = userContextService;
    }

    public async Task<IEnumerable<ExamDTO>> Handle(GetExamsRequest request, CancellationToken cancellationToken)
    {
        var user = await this.userContextService.GetUserContextAsync();
        var exams = await this.examRepository.GetExams(user);

        return exams.Select(x => new ExamDTO
        {
            Id = x.Id,
            Title = x.Title,
            Status = DateTime.Compare(x.AvailableTo, DateTime.Now) > 0 ? ExamStatus.Active : ExamStatus.Inactive,
            CompletedExams = x.ExamResults.Where(x => x.FinishExamTime != null).Count(),
            AllExams = x.ExamResults.Count(),
            AvailableTo = x.AvailableTo,
            AvailableFrom = x.AvailableFrom,
        });
    }
}
