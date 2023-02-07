using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetExamsResults;

public class GetExamsResultsHandler : IRequestHandler<GetExamsResultsRequest, IEnumerable<GetExamResultDto>>
{
    private readonly IExamResultRepository examResultRepository;

    public GetExamsResultsHandler(IExamResultRepository examResultRepository)
    {
        this.examResultRepository = examResultRepository;
    }

    public async Task<IEnumerable<GetExamResultDto>> Handle(GetExamsResultsRequest request, CancellationToken cancellationToken)
    {
        var examsResults = await this.examResultRepository.GetStudentExamsResults(request.StudentId);

        return examsResults.Select(x => new GetExamResultDto
        {
            Id = x.Id,
            ExamName = x.Exam.Title,
            Status = x.FinishExamTime is null ? "Active" : "Completed",
            Score = x.Score,
            EndingDate = x.Exam.AvailableTo,
        });
    }
}
