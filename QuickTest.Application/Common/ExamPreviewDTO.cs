using QuickTest.Core.Entities.Enums;

namespace QuickTest.Application.Common
{
    public sealed record ExamPreviewDTO
    {
        public string title { get; init; }

        public IEnumerable<QuestionPreviewDTO> Questions { get; init; }
    }

    public sealed record QuestionPreviewDTO
    {
        public int QuestionId { get; init; }

        public QuestionType Type { get; init; }

        public string Content { get; init; }

        public double Points { get; init; }

        public double? Score { get; init; }

        public string? AnswerContent { get; init; }

        public IEnumerable<AnswerPreviewDTO> Answers { get; init; }
    }

    public sealed record AnswerPreviewDTO
    {
        public int AnswerId { get; init; }

        public string Content { get; init; }

        public bool IsCorrect { get; init; }

        public bool? IsSelected { get; init; }
    }
}
