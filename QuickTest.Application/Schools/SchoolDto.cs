using QuickTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Schools
{
    public class SchoolDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Address? Address { get; set; }
        public ICollection<Group>? Groups { get; set; }
    }
}
