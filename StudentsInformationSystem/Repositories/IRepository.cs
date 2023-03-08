using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsInformationSystem.Models;

namespace StudentsInformationSystem.Repositories
{
    public interface IRepository<in T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void GetAll();
    }
}
