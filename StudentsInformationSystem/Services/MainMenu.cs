using StudentsInformationSystem.Models;
using StudentsInformationSystem.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace StudentsInformationSystem.Services
{
    public class MainMenu
    {
        private Department _selectedDept;
        private Lecture _selectedLecture;
        private Student _tempStudent;
        private Lecture _tempLecture;
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
                Console.WriteLine("\nMeniu:\n1.Create Departament/Add Students/Add Lectures\n2.Add Student/Lecture to Departament\n3.Create Lecture/Add to existing Department\n4.Create Student/Add to existing Department\n5.Move Student to another Department\n6.Show all Students by Department\n7.Show all Lectures by Department\n8.Show Lectures by Student\n9.Exit");
                var isCorrect = int.TryParse(Console.ReadLine(), out int choise);
                while(!isCorrect == true || choise > 9)
                {
                    Console.WriteLine("\nerror: wrong input.\n");
                    Console.WriteLine("\nMeniu:\n1.Create Departament/Add Students/Add Lectures\n2.Add Student/Lecture to Departament\n3.Create Lecture/Add to existing Department\n4.Create Student/Add to existing Department\n5.Move Student to another Department\n6.Show all Students by Department\n7.Show all Lectures by Department\n8.Show Lectures by Student\n9.Exit");
                     isCorrect = int.TryParse(Console.ReadLine(), out choise);
                }
                switch (choise)
                {
                    case 1:
                        var tempDepartment = CreateDepartment();
                        departments.Add(tempDepartment);
                        bool isAdding = true;
                        while (isAdding)
                        {
                            Console.WriteLine("\n1.Add Student\n2.Add Lecture\n3.Exit");
                            int.TryParse(Console.ReadLine(), out choise);
                            switch (choise)
                            {
                                case 1:
                                    var tempStudent = CreateStudent();
                                    tempStudent.DepartmentId = tempDepartment.Id;
                                    students.Add(tempStudent);
                                    tempDepartment.Students.Add(tempStudent);
                                    departments.Update(tempDepartment);
                                    
                                    Console.WriteLine("\nStudent added succsessfuly!");
                                    break;
                                case 2:
                                    lectures.GetAll();
                                    Console.WriteLine("\nSelect Lecture:");
                                    var num = 1;
                                    foreach (var lecture in lectures.LecturesList)
                                    {
                                        Console.WriteLine($"{num}.{lecture.Title}");
                                        num++;
                                    }
                                    int.TryParse(Console.ReadLine(), out choise);
                                    if (choise <= lectures.LecturesList.Count)
                                    {
                                        Console.WriteLine("\nLecture added successfully!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nWrong choise! Try Again!");
                                    }
                                    lectures.LecturesList[choise - 1].Departments.Add(tempDepartment);
                                    tempDepartment.Lectures.Add(lectures.LecturesList[choise - 1]);
                                    Console.WriteLine("\nLecture added successfully!");
                                    break;
                                case 3:
                                    departments.Update(tempDepartment);
                                    isAdding = false;
                                    break;
                                default:
                                    Console.WriteLine("Wrong choise!");
                                    break;
                            }
                        }
                        Console.WriteLine("\nDepartment added successfully!");
                        break;
                    case 2:
                        isAdding = true;
                        while (isAdding)
                        {
                            Console.WriteLine("1.Add Student\n2.Add Lecture\n3.Exit");
                            int.TryParse(Console.ReadLine(), out choise);
                            switch (choise)
                            {
                                case 1:
                                    _tempStudent = CreateStudent();
                                    departments.GetAll();
                                    if (!departments.DepartmentsList.IsNullOrEmpty())
                                    {
                                        _selectedDept = GetSelectedDepartment(departments.DepartmentsList);
                                        _tempStudent.DepartmentId = _selectedDept.Id;
                                        students.Add(_tempStudent);
                                        Console.WriteLine("\nStudent added successfully!");
                                    }
                                    else { Console.WriteLine("\nNo created Departments!"); }
                                    break;
                                case 2:
                                    lectures.GetAll();
                                    _selectedLecture = GetSelectedLecture(lectures.LecturesList);
                                    departments.GetAll();
                                    if (!departments.DepartmentsList.IsNullOrEmpty())
                                    {
                                        _selectedDept = GetSelectedDepartment(departments.DepartmentsList);
                                        _selectedDept.Lectures.Add(_selectedLecture);
                                        departments.Update(_selectedDept);
                                        Console.WriteLine("\nLecture added successfully!");
                                    }
                                    else { Console.WriteLine("\nNo created Departments!"); }
                                    break;
                                case 3:
                                    isAdding = false;
                                    break;
                            }
                        };
                        break;
                    case 3:
                        var tempLecture = CreateLecture();
                        lectures.Add(tempLecture);
                        departments.GetAll();
                        if (!departments.DepartmentsList.IsNullOrEmpty())
                        {
                            _selectedDept = GetSelectedDepartment(departments.DepartmentsList);
                            _selectedDept.Lectures.Add(tempLecture);
                            departments.Update(_selectedDept);
                            Console.WriteLine("\nLecture added successfully!");
                        }
                        else { Console.WriteLine("\nNo created Departments!"); }
                        break;
                    case 4:
                        _tempStudent = CreateStudent();
                        departments.GetAll();
                        _selectedDept = GetSelectedDepartment(departments.DepartmentsList);
                        _tempStudent.DepartmentId = _selectedDept.Id;
                        students.Add(_tempStudent);
                        Console.WriteLine("\nStudent added succsessfuly!");
                        break;
                    case 5:
                        departments.GetAll();
                        students.GetAll();
                        _tempStudent = SelectStudent(students.StudentsList);
                        Console.WriteLine($"{_tempStudent.FirstName} {_tempStudent.LastName} is from {departments.DepartmentsList.Where(d => d.Id == _tempStudent.DepartmentId).First().DeptName} Department. Where you want to move?");
                        _selectedDept = GetSelectedDepartment(departments.DepartmentsList);
                        _tempStudent.DepartmentId = _selectedDept.Id;
                        students.Update(_tempStudent);
                        Console.WriteLine("\nStudent moved successfuly!");
                        break;
                    case 6:
                        departments.GetAll();
                        students.GetAll();
                        _selectedDept = GetSelectedDepartment(departments.DepartmentsList);
                        int i = 0;
                        Console.WriteLine($"\nStudents from {_selectedDept.DeptName} Department:");
                        foreach(var student in students.StudentsList)
                        {
                            if(student.DepartmentId == _selectedDept.Id)
                            {
                                i++;
                                Console.WriteLine($"{i}.{student.FirstName} {student.LastName}");
                            } 
                        }
                        if(i == 0) Console.WriteLine("Not found!");
                        break;
                    case 7:
                        departments.GetAll();
                        _selectedDept = GetSelectedDepartment(departments.DepartmentsList);
                        Console.WriteLine($"\nLectures in {_selectedDept.DeptName} Department:");
                        lectures.ShowLecturesByDept(_selectedDept);
                        break;
                    case 8:
                        Console.WriteLine("Enter students first name:");
                        var tempFirstName = Console.ReadLine();
                        Console.WriteLine("Enter students last name:");
                        var tempLastName = Console.ReadLine();
                        students.GetLecturesByStudent(tempFirstName, tempLastName);
                        break;
                    case 9:
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
            Console.WriteLine("Enter Lecture title:");
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
        public Lecture GetSelectedLecture(List<Lecture> lectureList)
        {
            Console.WriteLine("\nSelect Lecture:");
            var num = 1;
            foreach (var lecture in lectureList)
            {
                Console.WriteLine($"{num}.{lecture.Title}");
                num++;
            }
            int.TryParse(Console.ReadLine(), out int choise);
            if (choise <= lectureList.Count)
            {
                var selectedLecture = lectureList[choise - 1];
                return selectedLecture;
            }
            else
            {
                Console.WriteLine("\nWrong choise! Try Again!");
                return new Lecture("Empty");
            }
        }
        public Student SelectStudent(List<Student> students)
        {
            Console.WriteLine("Select Student from list:");
            int i = 1;
            foreach (var student in students)
            {
                Console.WriteLine($"{i}.{student.FirstName} {student.LastName}");
                i++;
            }
            int.TryParse(Console.ReadLine(), out int choise);
            return students[choise-1];
        }
        public void ExitMenu()
        {
            Environment.Exit(0);
        }
    }
}
