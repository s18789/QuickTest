namespace QuickTest.Application.Exams.AddExamMembers
{
    public sealed record ExamMembersDTO
    {
        public int ExamId { get; init; }

        public IEnumerable<int> MembersIds { get; init; }
    }
}
