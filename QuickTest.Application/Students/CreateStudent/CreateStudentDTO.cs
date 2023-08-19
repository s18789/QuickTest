namespace QuickTest.Application.Students.CreateStudent
{
    public sealed record CreateStudentDTO
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }

        public int GroupId { get; init; }
    }
}
