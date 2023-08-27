using MediatR;
using QuickTest.Application.Services;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetCompletedExamsResults
{
    public sealed class GetCompletedExamsResultsHandler : IRequestHandler<GetCompletedExamsResultsRequest, IEnumerable<CompletedExamResultDTO>>
    {
        private readonly IExamResultRepository examResultRepository;
        private readonly IUserContextService userContextService;

        public GetCompletedExamsResultsHandler(IExamResultRepository examResultRepository, IUserContextService userContextService)
        {
            this.examResultRepository = examResultRepository;
            this.userContextService = userContextService;
        }

        public async Task<IEnumerable<CompletedExamResultDTO>> Handle(GetCompletedExamsResultsRequest request, CancellationToken cancellationToken)
        {
            var completedExamsResults = new List<CompletedExamResultDTO>();

            var user = await this.userContextService.GetUserContextAsync();
            var examsResult = await this.examResultRepository.GetCompletedExamsResults(user.Id);

            foreach (var examResult in examsResult)
            {
                var score = examResult.Score.Value / examResult.Exam.MaxPoints * 100;
                var avg = await this.examResultRepository.GetAvgScoreForExam(examResult.ExamId);
                var comparisonToOthers = score - (avg / examResult.Exam.MaxPoints * 100);

                completedExamsResults.Add(new CompletedExamResultDTO
                {
                    Id = examResult.Id,
                    Title = examResult.Exam.Title,
                    CompletionDate = examResult.FinishExamTime.Value,
                    Score = score,
                    ComparisonToOthers = comparisonToOthers,
                });
            }

            return completedExamsResults;
        }
    }
}
