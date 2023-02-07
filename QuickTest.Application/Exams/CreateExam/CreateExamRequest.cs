using MediatR;

namespace QuickTest.Application.Exams.CreateExam;

public class CreateExamRequest : IRequest<CreateExamDto>
{
    public CreateExamDto Exam { get; set; }
}
