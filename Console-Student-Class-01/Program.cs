using DAL.Data;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using System.Net.WebSockets;

namespace Console_Student_Class_01
{
    internal class Program
    {

        public static void ListStudents(List<Student> studentList)
        {
            Console.WriteLine("Student List");
            foreach (var item in studentList)
            {
                Console.WriteLine($"Student ID = {item.StudentID} Student Name = {item.StudentName} " +
                    $"Student Surename = {item.StudentSurename} Student Email = {item.StudentEmail}");
            }
        }

        public static void FindStudent(List<Student> studentList)
        {
            Console.WriteLine("Enter student ID to find:");
            int studentId = Convert.ToInt32(Console.ReadLine());
            Student foundStudent = studentList.FirstOrDefault(s => s.StudentID == studentId);
            if (foundStudent != null)
            {
                Console.WriteLine($"Student found: ID = {foundStudent.StudentID}, " +
                    $"Name = {foundStudent.StudentName}, Surename = {foundStudent.StudentSurename}, " +
                    $"Email = {foundStudent.StudentEmail}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
                
        }

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
                Console.WriteLine("4 - Edit Student Details By Id");
                Console.WriteLine("5 - Find Student");
                Console.WriteLine("6 - Delete Student");
                Console.WriteLine("7 - Exit");
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
                    Console.WriteLine("\n\nDisplaying all students...\n");
                    studentService.DisplayStudetnListByIDAsync().Wait();
                }
                else if (answer == "3")
                {
                    //ListStudents(studentList);
                    Console.WriteLine("\n\nDisplaying all students...\n");
                    studentService.DisplayStudetnListByNameAsync().Wait();
                }
                else if (answer == "4")
                {
                    studentService.UpdateStudentAsync(student).Wait();
                }
                else if (answer == "5")
                {
                    FindStudent(studentList);
                }
                else if (answer == "6")
                {
                    studentService.DeleteStudentAsync(student.StudentID).Wait();

                }
                else if (answer == "7")
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
