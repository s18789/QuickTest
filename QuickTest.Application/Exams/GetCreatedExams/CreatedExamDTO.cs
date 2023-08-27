namespace QuickTest.Application.Exams.GetCreatedExams
{
    public sealed record CreatedExamDTO
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public DateTime ValidTo { get; init; }

        public int CompletedExams { get; init; }

        public int AllExams { get; init; }

        public double? Average { get; init; }
    }
}
