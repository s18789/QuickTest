using MediatR;

namespace QuickTest.Application.ExamsResults.FinishExam;

public class FinishExamRequest : IRequest
{
    public FinishExamDto Exam { get; set; }
}
