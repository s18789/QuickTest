using MediatR;
using QuickTest.Application.Services;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetExamsResultsToResolve
{
    public sealed class GetExamsResultsToResolveHandler : IRequestHandler<GetExamsResultsToResolveRequest, IEnumerable<ExamResultToResolveDTO>>
    {
        private readonly IExamResultRepository examResultRepository;
        private readonly IUserContextService userContextService;

        public GetExamsResultsToResolveHandler(IExamResultRepository examResultRepository, IUserContextService userContextService)
        {
            this.examResultRepository = examResultRepository;
            this.userContextService = userContextService;
        }

        public async Task<IEnumerable<ExamResultToResolveDTO>> Handle(GetExamsResultsToResolveRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userContextService.GetUserContextAsync();
            var examsResult = await this.examResultRepository.GetExamsResultsToResolve(user.Id);

            return examsResult.Select(er => new ExamResultToResolveDTO
            {
                Id = er.Id,
                Title = er.Exam.Title,
                Deadline = er.Exam.AvailableTo,
                Teacher = new TeacherDTO
                {
                    FirstName = er.Exam.Teacher.FirstName,
                    LastName = er.Exam.Teacher.LastName,
                }
            });
        }
    }
}
