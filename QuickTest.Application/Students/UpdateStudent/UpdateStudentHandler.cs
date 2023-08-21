using AutoMapper;
using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Students.UpdateStudent;

public class UpdateStudentHandler : IRequestHandler<UpdateStudentRequest, StudentDto>
{
    private readonly IStudentRepository studentRepository;
    private readonly IGroupRepository groupRepository;
    private readonly IMapper mapper;

    public UpdateStudentHandler(IStudentRepository studentRepository, IGroupRepository groupRepository, IMapper mapper)
    {
        this.studentRepository = studentRepository;
        this.groupRepository = groupRepository;
        this.mapper = mapper;
    }

    public async Task<StudentDto> Handle(UpdateStudentRequest request, CancellationToken cancellationToken)
    {
        var existingStudent = await this.studentRepository.GetByIdAsync(request.Student.Id.Value);


        if (existingStudent == null)
        {
            return null;
        }


        mapper.Map(request.Student, existingStudent);

        if (request.Student.Group != null)
        {
            var group = await this.groupRepository.GetByIdAsync(request.Student.Group.Id);
            if (group != null)
            {
                existingStudent.Group = group;
            }
        }

        await this.studentRepository.UpdateAsync(existingStudent);

        var updatedStudentDto = mapper.Map<StudentDto>(existingStudent);

        return updatedStudentDto;
    }
}
