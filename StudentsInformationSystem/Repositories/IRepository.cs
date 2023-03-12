
namespace StudentsInformationSystem.Repositories
{
    public interface IRepository<in T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void GetAll();
    }
}
