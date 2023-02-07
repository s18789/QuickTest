using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetExamResultStatus;

public class GetExamResultStatusHandler : IRequestHandler<GetExamResultStatusRequest, GetExamResultStatusDto>
{
    private readonly IExamResultRepository examResultRepository;

    public GetExamResultStatusHandler(IExamResultRepository examResultRepository)
    {
        this.examResultRepository = examResultRepository;
    }

    public async Task<GetExamResultStatusDto> Handle(GetExamResultStatusRequest request, CancellationToken cancellationToken)
    {
        var examResult = await this.examResultRepository.GetByIdAsync(request.ExamResultId);

        return new GetExamResultStatusDto
        {
            Status = examResult.FinishExamTime is null
                ? "Active"
                : "Inactive"
        };
    }
}
