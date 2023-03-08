using StudentsInformationSystem.Models;
using StudentsInformationSystem.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace StudentsInformationSystem.Services
{
    public class MainMenu
    {
        private Department _selectedDept;
        public MainMenu() 
        {
        }
        public void StartMenu()
        {
            var departments = new DepartmentsRepository();
            var lectures = new LecturesRepository();
            var students = new StudentsRepository();
            Console.WriteLine("\t\t--Students Information System--");
            bool isProgramRunning = true;
            while (isProgramRunning)
            {
                Console.WriteLine("\nMeniu:\n1.Create Departament\n2.Add Lecture to Department\n3.Add Student to Departament\n4.Show all Students from selected Department\n5.Show Lectures by Student\n6.Exit");
                var isCorrect = int.TryParse(Console.ReadLine(), out int choise);
                while(!isCorrect == true || choise > 6)
                {
                    Console.WriteLine("\nerror: wrong input.\n");
                    Console.WriteLine("\nMeniu:\n1.Create Departament\n2.Create Lecture\n3.Add Student to Departament\n4.Show all Students from selected Department\n5.Show Lectures by Student\n6.Exit");
                    isCorrect = int.TryParse(Console.ReadLine(), out choise);
                }
                switch (choise)
                {
                    case 1:
                        var tempDepartment = CreateDepartment();
                        departments.Add(tempDepartment);
                        Console.WriteLine("Department added successfully!");
                        break;
                    case 2:
                        var tempLecture = CreateLecture();
                        lectures.Add(tempLecture);
                        departments.GetAll();
                        if (!departments.Departments.IsNullOrEmpty())
                        {
                            _selectedDept = GetSelectedDepartment(departments.Departments);
                            _selectedDept.Lectures.Add(tempLecture);
                            departments.Update(_selectedDept);
                            Console.WriteLine("Lecture added successfully!");
                        }
                        else { Console.WriteLine("No created Departments!"); }
                        
                        break;
                    case 3:
                        var tempStudent = CreateStudent();
                        departments.GetAll();
                        if (!departments.Departments.IsNullOrEmpty())
                        {
                            _selectedDept = GetSelectedDepartment(departments.Departments);
                            students.Add(_selectedDept.Id, tempStudent);
                            Console.WriteLine("Student added successfully!");
                        }
                        else {Console.WriteLine("No created Departments!");}
                        break;
                    case 4:
                        departments.GetAll();
                        students.GetAll();
                        _selectedDept = GetSelectedDepartment(departments.Departments);
                        int i = 0;
                        Console.WriteLine($"Students from {_selectedDept.DeptName}:");
                        foreach(var student in students.Students)
                        {
                            if(student.DepartmentId == _selectedDept.Id)
                            {
                                i++;
                                Console.WriteLine($"{i}.{student.FirstName} {student.LastName}");
                            } 
                        }
                        if(i == 0) Console.WriteLine("No Students in Department!");
                        Console.WriteLine();
                        break;
                    case 5:
                        Console.WriteLine("Enter students first name:");
                        var tempFirstName = Console.ReadLine();
                        Console.WriteLine("Enter students last name:");
                        var tempLastName = Console.ReadLine();
                        showLecturesByNameAndLastName(tempFirstName, tempLastName);
                        break;
                    case 6:
                        ExitMenu();
                        break;              
                }
            }     
        }
        public Department CreateDepartment()
        {
            Console.WriteLine("Enter Dapartments name: ");
            string deptName = Console.ReadLine();
            var tempDepartment = new Department($"{deptName}");
            return tempDepartment;
        }
        public Student CreateStudent()
        {
            Console.WriteLine("Enter students first name:");
            var tempFirstName = Console.ReadLine();
            Console.WriteLine("Enter students last name:");
            var tempLastName = Console.ReadLine();
            var tempStudent = new Student(tempFirstName, tempLastName);
            return tempStudent;
        }
        public Lecture CreateLecture()
        {
            Console.WriteLine("Enter lectures title:");
            var tempTitle = Console.ReadLine();
            var tempLecture = new Lecture($"{tempTitle}");
            return tempLecture;
        }
        public Department GetSelectedDepartment(List<Department> deptList)
        {
            Console.WriteLine("Select Department:");
            for (int i = 0; i < deptList.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{deptList[i].DeptName}");
            }
            int.TryParse(Console.ReadLine(), out int selection);
            var selectedDept = deptList[selection - 1];
            return selectedDept;
        }
        public void showLecturesByNameAndLastName(string firstName, string lastName)
        {
            var connectionString = "Server=localhost;Database=test;Trusted_Connection=True;TrustServerCertificate=True";
            using SqlConnection sqlService = new SqlConnection(connectionString);
            sqlService.Open();
            var values = new { FirsNAme = "First Name", LastName = "Last Name", DeptName = "Department", Title = "Lecture" };
            string sql = "SELECT Students.FirstName, Students.LastName, Departments.DeptName, Lectures.Title FROM Students JOIN Departments ON Students.DepartmentId = Departments.ID JOIN DepartmentLecture ON DepartmentLecture.DepartmentsId = Departments.Id JOIN Lectures ON Lectures.Id = DepartmentLecture.LecturesId;";
            var queryData = sqlService.Query(sql, values);
            var studentLectures = queryData.Where(student => student.FirstName ==  firstName && student.LastName == lastName).ToList();
            if (studentLectures.Count != 0)
            {
                Console.WriteLine($"{firstName} {lastName} from {studentLectures.First().DeptName} Department has lectures:");
                foreach (var student in studentLectures)
                {
                    int i = 1;
                    Console.WriteLine($"{i}.{student.Title}");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("No Student founded!");
            }
        }
        public void ExitMenu()
        {
            Environment.Exit(0);
        }
    }
}
