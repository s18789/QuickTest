namespace QuickTest.Application.ExamsResults.GetExamsResultsToCheck
{
    public sealed record ExamResultToCheckDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CompletionDate { get; set; }

        public Student Student { get; set; }
    }

    public sealed record Student
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
