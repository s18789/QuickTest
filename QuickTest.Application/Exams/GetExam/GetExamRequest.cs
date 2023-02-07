using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Exams.GetExam;

public class GetExamRequest : IRequest<ExamDto>
{
    public int Id { get; set; }
}
