using MediatR;

namespace QuickTest.Application.ExamsResults.GetCalendarExamsResults
{
    public sealed class GetCalendarExamsResultsRequest : IRequest<IEnumerable<CalendarExamResultDTO>>
    {
        public int Month { get; init; }

        public int Year { get; init; }
    }
}
