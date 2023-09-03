namespace QuickTest.Application.ExamsResults.GetCompletedExamsResults
{
    public sealed record CompletedExamResultDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CompletionDate { get; set; }

        public double Score { get; set; }

        public double ComparisonToOthers { get; set; }
    }
}
