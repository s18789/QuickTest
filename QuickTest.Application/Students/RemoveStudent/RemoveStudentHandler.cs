using MediatR;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Students.RemoveStudent
{
    public class RemoveStudentHandler : IRequestHandler<RemoveStudentRequest, bool>
    {
        private readonly IStudentRepository studentRepository;

        public RemoveStudentHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<bool> Handle(RemoveStudentRequest request, CancellationToken cancellationToken)
        {
            var student = await this.studentRepository.GetByIdAsync(request.StudentId);

            if (student == null)
            {
                return false;
            }

            await this.studentRepository.DeleteAsync(student);

            return true;
        }
    }
}
