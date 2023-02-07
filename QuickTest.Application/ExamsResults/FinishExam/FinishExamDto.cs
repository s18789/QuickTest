namespace QuickTest.Application.ExamsResults.FinishExam;

public class FinishExamDto
{
    public int ExamId { get; set; }

    public int ExamResultId { get; set; }

    public IEnumerable<FinishExamQuestionDto> Questions { get; set; }
}

public class FinishExamQuestionDto
{
    public int QuestionId { get; set; }

    public string Content { get; set; }

    public IEnumerable<FinishExamAnswerDto> Answers { get; set; }
}

public class FinishExamAnswerDto
{
    public int AnswerId { get; set; }

    public string Content { get; set; }

    public bool IsSelected { get; set; }
}