using MediatR;

namespace QuickTest.Application.ExamsResults.GetExamsResultsHeader
{
    public sealed class GetExamsResultsHeaderRequest : IRequest<IEnumerable<ExamResultHeaderDTO>>
    {
        public int Month { get; init; }

        public int Year { get; init; }
    }
}
