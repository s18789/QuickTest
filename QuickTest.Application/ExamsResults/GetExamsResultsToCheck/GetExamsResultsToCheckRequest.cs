using MediatR;

namespace QuickTest.Application.ExamsResults.GetExamsResultsToCheck
{
    public sealed class GetExamsResultsToCheckRequest : IRequest<IEnumerable<ExamResultToCheckDTO>>
    {
    }
}
