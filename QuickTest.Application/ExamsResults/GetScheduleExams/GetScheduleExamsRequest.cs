using MediatR;

namespace QuickTest.Application.ExamsResults.GetScheduleExams
{
    public sealed class GetScheduleExamsRequest : IRequest<IEnumerable<ScheduleExamDTO>>
    {
    }
}
