using QuickTest.Application.Answers;

namespace QuickTest.Application.Questions;
public class QuestionDTO
{
    public int Points { get; set; }

    public string QuestionContent { get; set; }

    public IEnumerable<AnswerDTO> Answers { get; set; }
}
