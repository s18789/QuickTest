namespace QuickTest.Application.ExamsResults.GetExamsResults;

public class GetExamResultDto
{
    public int Id { get; set; }

    public string ExamName { get; set; }

    public string Status { get; set; }

    public double? Score { get; set; }

    public DateTime EndingDate { get; set; }
}
