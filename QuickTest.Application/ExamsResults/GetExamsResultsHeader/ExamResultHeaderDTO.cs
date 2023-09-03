namespace QuickTest.Application.ExamsResults.GetExamsResultsHeader
{
    public sealed record ExamResultHeaderDTO
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public DateTime AvailableFrom { get; init; }

        public DateTime AvailableTo { get; init; }
    }
}
