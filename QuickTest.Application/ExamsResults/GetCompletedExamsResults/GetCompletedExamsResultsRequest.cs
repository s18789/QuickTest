using MediatR;

namespace QuickTest.Application.ExamsResults.GetCompletedExamsResults
{
    public sealed class GetCompletedExamsResultsRequest : IRequest<IEnumerable<CompletedExamResultDTO>>
    {
    }
}
