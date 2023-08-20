using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Core.Entities
{
    [Table("Admins")]
    public class Admin : User
    {
        public int? SchoolId { get; set; }
        public School? AdministeredSchool { get; set; }
    }
}
