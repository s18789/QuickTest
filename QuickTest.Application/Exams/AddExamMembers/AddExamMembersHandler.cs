using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Exams.AddExamMembers
{
    public sealed class AddExamMembersHandler : IRequestHandler<AddExamMembersRequest, int>
    {

        IExamResultRepository examResultRepository;

        public AddExamMembersHandler(IExamResultRepository examResultRepository)
        {
            this.examResultRepository = examResultRepository;
        }

        public async Task<int> Handle(AddExamMembersRequest request, CancellationToken cancellationToken)
        {
            await this.examResultRepository.AddMembersToExam(request.ExamMembers.ExamId, request.ExamMembers.MembersIds);

            return request.ExamMembers.ExamId;
        }
    }
}
