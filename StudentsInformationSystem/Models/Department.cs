using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsInformationSystem.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string DeptName { get; set; }
        public List<Student> Students { get; set; }
        public List<Lecture> Lectures { get; set; }
        
        public Department(string deptName)
        {
            Id = Guid.NewGuid();
            DeptName = deptName;
            Students = new List<Student>();
            Lectures = new List<Lecture>();
        }
    }
}
