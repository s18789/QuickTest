using MediatR;
using QuickTest.Application.Services;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Exams.GetScheduleExams
{
    public sealed class GetScheduleExamsHandler : IRequestHandler<GetScheduleExamsRequest, IEnumerable<ScheduleExamDTO>>
    {
        private readonly IUserContextService userContextService;
        private readonly IExamRepository examRepository;

        public GetScheduleExamsHandler(IUserContextService userContextService, IExamRepository examRepository)
        {
            this.userContextService = userContextService;
            this.examRepository = examRepository;
        }

        public async Task<IEnumerable<ScheduleExamDTO>> Handle(GetScheduleExamsRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userContextService.GetUserContextAsync();

            var scheduleExams = user.UserRoleId == (int)RoleType.TEACHER
                ? await this.examRepository.GetScheduleExams(user)
                : await this.examRepository.GetScheduleExams();

            return scheduleExams.Select(e => new ScheduleExamDTO
            {
                Id = e.Id,
                Title = e.Title,
                Date = e.AvailableTo,
                Subject = "some title",
            });
        }
    }
}
