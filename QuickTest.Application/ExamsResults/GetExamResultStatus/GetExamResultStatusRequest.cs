using MediatR;

namespace QuickTest.Application.ExamsResults.GetExamResultStatus;
public class GetExamResultStatusRequest : IRequest<GetExamResultStatusDto>
{
    public int ExamResultId { get; set; }
}
