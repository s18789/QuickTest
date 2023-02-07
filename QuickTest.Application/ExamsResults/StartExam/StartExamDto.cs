namespace QuickTest.Application.ExamsResults.StartExam;

public class StartExamDto
{
    public int ExamId { get; set; }

    public int ExamResultId { get; set; }

    public IEnumerable<StartExamQuestionDto> Questions { get; set; }
}

public class StartExamQuestionDto
{
    public int QuestionId { get; set; }

    public string Content { get; set; }

    public IEnumerable<StartExamAnswerDto> Answers { get; set; }
}

public class StartExamAnswerDto
{
    public int AnswerId { get; set; }

    public string Content { get; set; }

    public bool IsSelected { get; set; }
}
