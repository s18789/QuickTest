using MediatR;

namespace QuickTest.Application.Students.GetStudentsForExam
{
    public sealed class GetStudentsForExamRequest : IRequest<IEnumerable<StudentDto>>
    {
        public int Id { get; set; }
    }
}
