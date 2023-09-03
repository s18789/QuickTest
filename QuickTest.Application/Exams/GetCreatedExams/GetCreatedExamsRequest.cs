using MediatR;

namespace QuickTest.Application.Exams.GetCreatedExams
{
    public sealed class GetCreatedExamsRequest : IRequest<IEnumerable<CreatedExamDTO>>
    {
    }
}
