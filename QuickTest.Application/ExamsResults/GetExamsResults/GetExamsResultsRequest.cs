using MediatR;

namespace QuickTest.Application.ExamsResults.GetExamsResults;

public class GetExamsResultsRequest : IRequest<ExamResultDto>
{
    public int StudentId { get; set; }
}
