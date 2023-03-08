using StudentsInformationSystem.Models;
using StudentsInformationSystem.Repositories;
using StudentsInformationSystem.Services;

namespace StudentsInformationSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            var menu = new MainMenu();
            menu.StartMenu();
        }
    }
}
