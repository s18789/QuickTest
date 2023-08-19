using QuickTest.Core.Entities.Enums;

namespace QuickTest.Application.ExamsResults.FinishExam;

public sealed record class FinishExamDto
{
    public int ExamId { get; init; }

    public int ExamResultId { get; init; }

    public IEnumerable<FinishExamQuestionDto> Questions { get; init; }
}

public sealed record FinishExamQuestionDto
{
    public int QuestionId { get; init; }

    public string Content { get; init; }

    public QuestionType Type { get; init; }

    public string? AnswerContent { get; init; }

    public IEnumerable<FinishExamAnswerDto>? Answers { get; init; }
}

public sealed record FinishExamAnswerDto
{
    public int AnswerId { get; init; }

    public string Content { get; init; }

    public bool IsSelected { get; init; }
}