using MediatR;
using QuickTest.Application.Common;

namespace QuickTest.Application.ExamsResults.GetExamResultPreview
{
    public sealed class GetExamResultPreviewRequest : IRequest<ExamPreviewDTO>
    {
        public int ExamResultId { get; init; }
    }
}
