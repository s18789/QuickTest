namespace QuickTest.Application.Answers;
public sealed record class AnswerDTO
{
    public string AnswerContent { get; init; }

    public bool? Correct { get; init; }
}
