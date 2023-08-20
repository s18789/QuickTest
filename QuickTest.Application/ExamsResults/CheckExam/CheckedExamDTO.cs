using QuickTest.Core.Entities.Enums;

namespace QuickTest.Application.ExamsResults.CheckExam
{
    public record CheckedExamDTO
    {
        public int ExamResultId { get; init; }

        public IEnumerable<CheckedQuestionDTO> Questions { get; init; }
    }

    public record CheckedQuestionDTO
    {
        public int QuestionId { get; init; }

        public QuestionType Type { get; init; }

        public double Score { get; init; }
    }
}
