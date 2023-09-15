using QuickTest.Core.Entities;

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
