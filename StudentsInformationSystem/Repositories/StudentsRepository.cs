using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using StudentsInformationSystem.Data;
using StudentsInformationSystem.Models;

namespace StudentsInformationSystem.Repositories
{
    public class StudentsRepository : IRepository<Student>
    {
        public List<Student> Students { get; private set; } 
        public StudentsRepository()
        {
            Students = new List<Student>();
        }

        public void Add(Guid deptId, Student entity)
        {
            using var dbContext = new StudentContext();
            dbContext.Departments.SingleOrDefault(dept => dept.Id == deptId).Students.Add(entity);
            dbContext.Students.Add(entity);
            dbContext.SaveChanges();
        }

        public void Add(Student entity)
        {
            throw new NotImplementedException();
        }

        public void GetAll()
        {
            using var dbContext = new StudentContext();
            Students = dbContext.Students.ToList();
        }

        public void Update(Student entity)
        {
            throw new NotImplementedException();
        }
    }
}
