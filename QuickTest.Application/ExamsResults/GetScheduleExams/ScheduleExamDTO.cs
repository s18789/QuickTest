namespace QuickTest.Application.ExamsResults.GetScheduleExams
{
    public sealed record ScheduleExamDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }
    }
}
