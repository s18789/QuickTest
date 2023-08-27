using MediatR;

namespace QuickTest.Application.ExamsResults.GetExamsResultsToResolve
{
    public sealed class GetExamsResultsToResolveRequest : IRequest<IEnumerable<ExamResultToResolveDTO>>
    {
    }
}
