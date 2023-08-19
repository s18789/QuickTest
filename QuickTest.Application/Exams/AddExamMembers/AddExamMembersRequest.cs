using MediatR;

namespace QuickTest.Application.Exams.AddExamMembers
{
    public sealed class AddExamMembersRequest : IRequest<int>
    {
        public ExamMembersDTO ExamMembers { get; init; }
    }
}
