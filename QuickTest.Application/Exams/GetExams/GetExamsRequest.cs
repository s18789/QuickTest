using MediatR;

namespace QuickTest.Application.Exams.GetExams;
public class GetExamsRequest : IRequest<IEnumerable<ExamDTO>>
{
}
