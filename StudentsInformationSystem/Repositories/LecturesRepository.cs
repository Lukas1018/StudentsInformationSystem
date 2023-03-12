using Microsoft.IdentityModel.Tokens;
using StudentsInformationSystem.Data;
using StudentsInformationSystem.Models;


namespace StudentsInformationSystem.Repositories
{
    internal class LecturesRepository : IRepository<Lecture>
    {
        public List<Lecture> LecturesList { get; private set; } = new List<Lecture>();

        public LecturesRepository()
        {
            AddLecturesToDatabase();
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
            LecturesList = dbContext.Lectures.ToList();
        }
        public void Update(Lecture entity)
        {
            using var dbContext = new StudentContext();
            dbContext.Lectures.Update(entity);

            dbContext.SaveChanges();
            
        }
        private void AddLecturesToDatabase()
        {
            using var dbContext = new StudentContext();
            if (dbContext.Lectures.IsNullOrEmpty())
            {
                dbContext.Lectures.AddRange(
                new List<Lecture>(){
                new Lecture("Business management"),
                new Lecture("C# programming"),
                new Lecture("JavaScript"),
                new Lecture("Mathematics")
                    });
                dbContext.SaveChanges();
            }
        }
        public void ShowLecturesByDept(Department dept)
        {
            using var dbContext = new StudentContext();
            var lecturesList = dbContext.Lectures.Where(l => l.Departments.Contains(dept)).ToList();
            int i = 0;
            foreach (var l in lecturesList)
            {
                Console.WriteLine($"{i+1}.{l.Title}");
                i++;
            }
            if (i == 0) Console.WriteLine("Not found!");
        }
    }
}
