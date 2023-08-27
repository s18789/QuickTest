using MediatR;
using QuickTest.Application.Services;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetExamsResultsToCheck
{
    public sealed class GetExamsResultsToCheckHandler : IRequestHandler<GetExamsResultsToCheckRequest, IEnumerable<ExamResultToCheckDTO>>
    {
        private readonly IExamResultRepository examResultRepository;
        private readonly IUserContextService userContextService;

        public GetExamsResultsToCheckHandler(IExamResultRepository examResultRepository, IUserContextService userContextService)
        {
            this.examResultRepository = examResultRepository;
            this.userContextService = userContextService;
        }

        public async Task<IEnumerable<ExamResultToCheckDTO>> Handle(GetExamsResultsToCheckRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userContextService.GetUserContextAsync();
            var examsResults = await this.examResultRepository.GetExamsResultsToCheck(user.Id);

            return examsResults.Select(er => new ExamResultToCheckDTO
            {
                Id = er.Id,
                Title = er.Exam.Title,
                CompletionDate = er.FinishExamTime.Value,
                Student = new Student
                {
                    FirstName = er.Student.FirstName,
                    LastName = er.Student.LastName,
                }
            });
        }
    }
}
