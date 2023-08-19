using QuickTest.Application.Answers;
using QuickTest.Core.Entities.Enums;

namespace QuickTest.Application.Questions;
public sealed record QuestionDTO
{
    public int Points { get; init; }

    public string QuestionContent { get; init; }

    public QuestionType Type { get; set; }

    public IEnumerable<AnswerDTO> Answers { get; init; }
}
