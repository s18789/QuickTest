namespace QuickTest.Application.Exams.GetExamsHeader
{
    public sealed record ExamHeaderDTO
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public DateTime AvailableFrom { get; init; }

        public DateTime AvailableTo { get; init; }
    }
}
