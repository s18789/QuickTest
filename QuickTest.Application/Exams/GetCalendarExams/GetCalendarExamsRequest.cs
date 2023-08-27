using MediatR;

namespace QuickTest.Application.Exams.GetCalendarExams
{
    public sealed class GetCalendarExamsRequest : IRequest<IEnumerable<CalendarExamDTO>>
    {
        public int Month { get; init; }

        public int Year { get; init; }
    }
}
