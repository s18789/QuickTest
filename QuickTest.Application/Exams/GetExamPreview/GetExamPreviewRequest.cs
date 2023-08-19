using MediatR;
using QuickTest.Application.Common;

namespace QuickTest.Application.Exams.GetExamPreview
{
    public sealed class GetExamPreviewRequest : IRequest<ExamPreviewDTO>
    {
        public int ExamId { get; init; }
    }
}
