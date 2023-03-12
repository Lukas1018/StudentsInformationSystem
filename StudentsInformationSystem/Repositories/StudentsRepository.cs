using Microsoft.IdentityModel.Tokens;
using StudentsInformationSystem.Data;
using StudentsInformationSystem.Models;

namespace StudentsInformationSystem.Repositories
{
    public class StudentsRepository : IRepository<Student>
    {
        public List<Student> StudentsList { get; private set; }
        public StudentsRepository()
        {
            StudentsList = new List<Student>();
        }
        public void Add(Student entity)
        {
            using var dbContext = new StudentContext();
            dbContext.Students.Add(entity);
            dbContext.SaveChanges();
        }

        public void GetAll()
        {
            using var dbContext = new StudentContext();
            StudentsList = dbContext.Students.ToList();
        }

        public void Update(Student entity)
        {
            using var dbContext = new StudentContext();
            dbContext.Students.Update(entity);
            dbContext.SaveChanges();
        }
        public void GetLecturesByStudent(string firstName, string lastName)
        {
            using var dbcontext = new StudentContext();
            var studentList = dbcontext.Students
                .Where(student => student.FirstName == firstName && student.LastName == lastName)
                .Join(dbcontext.Departments, s => s.DepartmentId, d => d.Id, (s, d) => new
                {
                    s.FirstName,
                    s.LastName,
                    d.DeptName,
                    d.Lectures
                }).ToList();
            if (!studentList.IsNullOrEmpty())
            {
                foreach (var student in studentList)
                {
                    Console.WriteLine($"\n{student.FirstName} {student.LastName} from {student.DeptName} has got lectures:");
                    for (int i = 0; i < student.Lectures.Count(); i++)
                    {  
                        Console.WriteLine($"{i + 1}.{student.Lectures[i].Title}");    
                    }
                    if (student.Lectures.IsNullOrEmpty())
                    {
                        Console.WriteLine("Not found!");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nStudent not found!");
            }
        }
    }
}
