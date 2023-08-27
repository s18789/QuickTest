using MediatR;
using QuickTest.Application.Services;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Exams.GetExamsHeader
{
    public sealed class GetExamsHeaderHandler : IRequestHandler<GetExamsHeaderRequest, IEnumerable<ExamHeaderDTO>>
    {
        private readonly IUserContextService userContextService;
        private readonly IExamRepository examRepository;

        public GetExamsHeaderHandler(IUserContextService userContextService, IExamRepository examRepository)
        {
            this.userContextService = userContextService;
            this.examRepository = examRepository;
        }

        public async Task<IEnumerable<ExamHeaderDTO>> Handle(GetExamsHeaderRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userContextService.GetUserContextAsync();
            var exams = await this.examRepository.GetExamsForMonth(user.Id, request.Month, request.Year);

            return exams.Select(e => new ExamHeaderDTO
            {
                Id = e.Id,
                Title = e.Title,
                AvailableFrom = e.AvailableFrom,
                AvailableTo = e.AvailableTo,
            });
        }
    }
}
