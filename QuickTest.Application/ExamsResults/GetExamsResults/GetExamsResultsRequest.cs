using MediatR;

namespace QuickTest.Application.ExamsResults.GetExamsResults;

public class GetExamsResultsRequest : IRequest<IEnumerable<GetExamResultDto>>
{
    public int StudentId { get; set; }
}
