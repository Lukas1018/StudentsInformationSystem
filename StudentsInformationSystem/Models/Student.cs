using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsInformationSystem.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public Student(string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
