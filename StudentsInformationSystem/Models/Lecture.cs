using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsInformationSystem.Models
{
    public class Lecture
    {
        public  Guid Id { get; set; }
        public string Title { get; set; }
        public List<Department> Departments { get; set; }

        public Lecture(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            Departments = new List<Department>();  
        }
    }
}
