using MediatR;
using QuickTest.Application.Services;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetScheduleExams
{
    public sealed class GetScheduleExamsHandler : IRequestHandler<GetScheduleExamsRequest, IEnumerable<ScheduleExamDTO>>
    {
        private readonly IUserContextService userContextService;
        private readonly IExamResultRepository examResultRepository;

        public GetScheduleExamsHandler(IUserContextService userContextService, IExamResultRepository examResultRepository)
        {
            this.userContextService = userContextService;
            this.examResultRepository = examResultRepository;
        }

        public async Task<IEnumerable<ScheduleExamDTO>> Handle(GetScheduleExamsRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userContextService.GetUserContextAsync();
            var examsResults = await this.examResultRepository.GetScheduleExams(user);

            return examsResults.Select(er => new ScheduleExamDTO
            {
                Id = er.Id,
                Title = er.Exam.Title,
                Date = er.Exam.AvailableTo,
                Subject = "some title",
            });
        }
    }
}
