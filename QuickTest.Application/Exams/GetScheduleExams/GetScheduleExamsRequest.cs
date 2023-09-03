using MediatR;

namespace QuickTest.Application.Exams.GetScheduleExams
{
    public sealed class GetScheduleExamsRequest : IRequest<IEnumerable<ScheduleExamDTO>>
    {
    }
}
