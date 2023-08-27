using MediatR;
using QuickTest.Application.Services;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetCalendarExamsResults
{
    public sealed class GetCalendarExamsResultsHandler : IRequestHandler<GetCalendarExamsResultsRequest, IEnumerable<CalendarExamResultDTO>>
    {
        private readonly IUserContextService userContextService;
        private readonly IExamResultRepository examResultRepository;

        public GetCalendarExamsResultsHandler(IUserContextService userContextService, IExamResultRepository examResultRepository)
        {
            this.userContextService = userContextService;
            this.examResultRepository = examResultRepository;
        }

        public async Task<IEnumerable<CalendarExamResultDTO>> Handle(GetCalendarExamsResultsRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userContextService.GetUserContextAsync();
            var exams = await this.examResultRepository.GetExamsResultsForMonth(user.Id, request.Month, request.Year);

            return exams.Select(x => new CalendarExamResultDTO
            {
                Id = x.FinishExamTime != null && x.Score == null 
                    ? null
                    : x.Id,
                Title = x.Exam.Title,
                DayOfMonth = x.Exam.AvailableTo.Day,
            });
        }
    }
}
