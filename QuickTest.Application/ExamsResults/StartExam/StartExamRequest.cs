using MediatR;

namespace QuickTest.Application.ExamsResults.StartExam;

public class StartExamRequest : IRequest<StartExamDto>
{
    public int ExamResultId { get; set; }
}
