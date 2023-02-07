namespace QuickTest.Application.Exams.GetExam;

public class ExamDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Status { get; set; }

    public string Category { get; set; }

    public int QuestionNumber { get; set; }

    public DateTime AvailableFrom { get; set; }

    public DateTime AvailableTo { get; set; }

    public int Time { get; set; }

    public IEnumerable<ExamResultDto> ExamResults { get; set; }
}

public class ExamResultDto
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Status { get; set; }

    public DateTime? FinishTime { get; set; }

    public double? Score { get; set; }
}
