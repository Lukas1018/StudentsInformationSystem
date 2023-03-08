using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsInformationSystem.Data;
using StudentsInformationSystem.Models;

namespace StudentsInformationSystem.Repositories
{
    internal class LecturesRepository : IRepository<Lecture>
    {
        public List<Lecture> Lectures { get; private set; }

        public LecturesRepository()
        {
        }
        public void Add(Lecture entity)
        {
            using var dbContext = new StudentContext();
            dbContext.Lectures.Add(entity);
            dbContext.SaveChanges();
        }

        public void GetAll()
        {
            using var dbContext = new StudentContext();
            Lectures = dbContext.Lectures.ToList();
        }
        public void Update(Lecture entity)
        {
            throw new NotImplementedException();
        }
    }
}
