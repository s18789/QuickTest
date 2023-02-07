using MediatR;

namespace QuickTest.Application.ExamsResults.GetExamResult;

public class GetExamResultRequest : IRequest<GetExamResultDto>
{
    public int ExamResultId { get; set; }
}
