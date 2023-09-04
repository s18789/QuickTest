using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
using QuickTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Users.CreateUser
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsEmailSent { get; set; }
        public TeacherDto AddedTeacher { get; set; }
        public StudentDto AddedStudent { get; set; }
    }
}
