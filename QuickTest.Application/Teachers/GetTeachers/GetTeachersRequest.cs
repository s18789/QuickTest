using MediatR;


namespace QuickTest.Application.Teachers.GetTeachers
{
    public class GetTeachersRequest : IRequest<IEnumerable<TeacherDto>>
    {
    }
}
