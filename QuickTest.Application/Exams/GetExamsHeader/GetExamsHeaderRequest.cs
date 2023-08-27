using MediatR;

namespace QuickTest.Application.Exams.GetExamsHeader
{
    public sealed class GetExamsHeaderRequest : IRequest<IEnumerable<ExamHeaderDTO>>
    {
        public int Month { get; init; }

        public int Year { get; init; }
    }
}
