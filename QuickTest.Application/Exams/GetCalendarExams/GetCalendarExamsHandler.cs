using MediatR;
using QuickTest.Application.Services;
using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Exams.GetCalendarExams
{
    public sealed class GetCalendarExamsHandler : IRequestHandler<GetCalendarExamsRequest, IEnumerable<CalendarExamDTO>>
    {
        private readonly IUserContextService userContextService;
        private readonly IExamRepository examRepository;

        public GetCalendarExamsHandler(IUserContextService userContextService, IExamRepository examRepository)
        {
            this.userContextService = userContextService;
            this.examRepository = examRepository;
        }

        public async Task<IEnumerable<CalendarExamDTO>> Handle(GetCalendarExamsRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userContextService.GetUserContextAsync();
            var exams = await this.examRepository.GetExamsForMonth(user.Id, request.Month, request.Year);

            if (user.UserRoleId == (int)RoleType.TEACHER)
            {
                exams = await this.examRepository.GetExamsForMonth(user.Id, request.Month, request.Year);
            }
            else
            {
                exams = await this.examRepository.GetExamsForMonth(request.Month, request.Year);
            }


            return exams.Select(e => new CalendarExamDTO
            {
                Id = e.Id,
                Title = e.Title,
                DayOfMonth = e.AvailableTo.Day,
            });
        }
    }
}
