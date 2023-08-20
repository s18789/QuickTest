using MediatR;
using QuickTest.Application.Groups;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Students.GetStudentsForExam
{
    public sealed class GetStudentsForExamHandler : IRequestHandler<GetStudentsForExamRequest, IEnumerable<StudentDto>>
    {
        private readonly IStudentRepository studentRepository;

        public GetStudentsForExamHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentDto>> Handle(GetStudentsForExamRequest request, CancellationToken cancellationToken)
        {
            var allStudents = await this.studentRepository.GetStudentsWithGroup();
            var examStudents = await this.studentRepository.GetStudentsWithGroupForExam(request.Id);

            return allStudents.Except(examStudents).Select(x => new StudentDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                GroupDto = new GroupDto
                {
                    Id = x.Group.Id,
                    Name = x.Group.Name
                }
            });
        }
    }
}
