using MediatR;

namespace QuickTest.Application.ExamsResults.GetExamsResults;

public class GetExamsResultsRequest : IRequest<IEnumerable<ExamResultDto>>
{
    public int StudentId { get; set; }
}
