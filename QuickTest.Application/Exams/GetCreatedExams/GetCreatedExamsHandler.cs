using MediatR;
using QuickTest.Application.Services;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Exams.GetCreatedExams
{
    public sealed class GetCreatedExamsHandler : IRequestHandler<GetCreatedExamsRequest, IEnumerable<CreatedExamDTO>>
    {
        private readonly IUserContextService userContextService;
        private readonly IExamRepository examRepository;

        public GetCreatedExamsHandler(IUserContextService userContextService, IExamRepository examRepository)
        {
            this.userContextService = userContextService;
            this.examRepository = examRepository;
        }
        public async Task<IEnumerable<CreatedExamDTO>> Handle(GetCreatedExamsRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userContextService.GetUserContextAsync();
            var exams = await this.examRepository.GetCreatedExam(user.Id);

            return exams.Select(x => new CreatedExamDTO
            {
                Id = x.Id,
                Title = x.Title,
                ValidTo = x.AvailableTo,
                Average = x.ExamResults.Average(er => er.Score) / x.MaxPoints * 100,
                CompletedExams = x.ExamResults.Where(er => er.Score != null).Count(),
                AllExams = x.ExamResults.Count()
            });
        }
    }
}
