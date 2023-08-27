namespace QuickTest.Application.Exams.GetCalendarExams
{
    public sealed record CalendarExamDTO
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public int DayOfMonth { get; init; }
    }
}
