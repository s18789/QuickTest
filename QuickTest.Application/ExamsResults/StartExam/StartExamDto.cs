using QuickTest.Core.Entities.Enums;

namespace QuickTest.Application.ExamsResults.StartExam;

public sealed record StartExamDto
{
    public int ExamId { get; init; }

    public int ExamResultId { get; init; }

    public IEnumerable<StartExamQuestionDto> Questions { get; init; }
}

public sealed record StartExamQuestionDto
{
    public int QuestionId { get; init; }

    public string Content { get; init; }

    public QuestionType Type { get; init; }

    public IEnumerable<StartExamAnswerDto> Answers { get; init; }
}

public sealed record StartExamAnswerDto
{
    public int AnswerId { get; init; }

    public string Content { get; init; }

    public bool IsSelected { get; init; }
}
