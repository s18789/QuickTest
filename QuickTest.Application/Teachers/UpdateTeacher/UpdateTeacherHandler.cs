using AutoMapper;
using MediatR;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.UpdateTeacher
{
    public class UpdateTeacherHandler : IRequestHandler<UpdateTeacherRequest, TeacherDto>
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly IMapper mapper;

        public UpdateTeacherHandler(ITeacherRepository teacherRepository, IMapper mapper)
        {
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
        }

        public async Task<TeacherDto> Handle(UpdateTeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = await this.teacherRepository.GetByIdAsync(request.Teacher.Id.Value);

            if (teacher == null)
            {
                return null;
            }

            mapper.Map(request.Teacher, teacher);

            await this.teacherRepository.UpdateAsync(teacher);

            return mapper.Map<TeacherDto>(teacher);
        }
    }
}
