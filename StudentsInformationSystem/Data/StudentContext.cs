using Microsoft.EntityFrameworkCore;
using StudentsInformationSystem.Models;

namespace StudentsInformationSystem.Data
{
    public class StudentContext :DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=test;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
