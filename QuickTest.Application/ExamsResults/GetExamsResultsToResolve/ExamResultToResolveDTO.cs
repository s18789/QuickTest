namespace QuickTest.Application.ExamsResults.GetExamsResultsToResolve
{
    public sealed record ExamResultToResolveDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Deadline { get; set; }

        public TeacherDTO Teacher { get; set; }
    }

    public sealed record TeacherDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
