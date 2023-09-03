using MediatR;
using QuickTest.Application.Services;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetExamsResultsHeader
{
    public sealed class GetExamsResultsHeaderHandler : IRequestHandler<GetExamsResultsHeaderRequest, IEnumerable<ExamResultHeaderDTO>>
    {
        private readonly IUserContextService userContextService;
        private readonly IExamResultRepository examResultRepository;

        public GetExamsResultsHeaderHandler(IUserContextService userContextService, IExamResultRepository examResultRepository)
        {
            this.userContextService = userContextService;
            this.examResultRepository = examResultRepository;
        }

        public async Task<IEnumerable<ExamResultHeaderDTO>> Handle(GetExamsResultsHeaderRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userContextService.GetUserContextAsync();
            var examsResults = await this.examResultRepository.GetExamsResultsForMonth(user.Id, request.Month, request.Year);

            return examsResults.Select(er => new ExamResultHeaderDTO
            {
                Id = er.Id,
                Title = er.Exam.Title,
                AvailableFrom = er.Exam.AvailableFrom,
                AvailableTo = er.Exam.AvailableTo
            });
        }
    }
}
