using StudentsInformationSystem.Data;
using StudentsInformationSystem.Models;

namespace StudentsInformationSystem.Repositories
{
    public class DepartmentsRepository : IRepository<Department>
    {
        public List<Department> Departments { get; private set; }
        public DepartmentsRepository() 
        { 
            Departments = new List<Department>();
        }

        public void Add(Department entity)
        {
            using var dbContext = new StudentContext();
            dbContext.Departments.Add(entity);
            dbContext.SaveChanges();
        }
        public void Update(Department entity)
        {
            using var dbContext = new StudentContext();
            dbContext.Departments.Update(entity);
            dbContext.SaveChanges();
        }
        public void GetAll()
        {
            using var dbContext = new StudentContext();
            Departments = dbContext.Departments.ToList();
        }
    }
}
