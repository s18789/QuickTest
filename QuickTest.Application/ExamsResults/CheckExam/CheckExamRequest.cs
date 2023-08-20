using MediatR;

namespace QuickTest.Application.ExamsResults.CheckExam
{
    public sealed class CheckExamRequest : IRequest<int>
    {
        public CheckedExamDTO CheckedExam { get; init; }
    }
}
