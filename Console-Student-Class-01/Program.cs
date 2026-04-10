using DAL.Data;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using System.Net.WebSockets;

namespace Console_Student_Class_01
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var studentRepository = new StudentRepository(configuration);

            var studentService = new StudentService(studentRepository);

            List<Course> courseList = new List<Course>();
            List<Student> studentList = new List<Student>();

            Student student = new Student();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Student Registration System");
                Console.WriteLine("1 - Do you want to add a new student");
                Console.WriteLine("2 - Display All Students By ID");
                Console.WriteLine("3 - Display All Students By Name");
                Console.WriteLine("4 - Edit Student Details By ID");
                Console.WriteLine("5 - Find Student By ID");
                Console.WriteLine("6 - Find Student By Name");
                Console.WriteLine("7 - Find Student By Email");
                Console.WriteLine("8 - Delete Student");
                Console.WriteLine("9 - Exit");
                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    // student = RegisterStudents();
                    student = StudentService.RegisterStudents();

                    // studentList.Add(student);
                    studentService.CreateStudentAsync(student).Wait();
                }
                else if (answer == "2")
                {
                    studentService.DisplayStudetnListByIDAsync().Wait();
                }
                else if (answer == "3")
                {
                    //ListStudents(studentList);
                    studentService.DisplayStudetnListByNameAsync().Wait();
                }
                else if (answer == "4")
                {
                    studentService.UpdateStudentAsync(student).Wait();
                }
                else if (answer == "5")
                {
                    //FindStudent(studentList);
                    studentService.GetStudentByIdAsync(student.StudentID).Wait();
                }
                else if (answer == "6")
                {
                    studentService.GetStudentByNameAsync(student.StudentName).Wait();
                }
                else if (answer == "7")
                {
                    studentService.GetStudentByEmailAsync(student.StudentEmail).Wait();
                }
                else if (answer == "8")
                {
                    studentService.DeleteStudentAsync(student.StudentID).Wait();

                }
                else if (answer == "9")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }
    }
}
