namespace QuickTest.Application.ExamsResults.GetCalendarExamsResults
{
    public sealed record CalendarExamResultDTO
    {
        public int? Id { get; init; }

        public string Title { get; init; }

        public int DayOfMonth { get; init; }
    }
}
