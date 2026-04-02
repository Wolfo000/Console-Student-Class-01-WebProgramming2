using DAL.Data;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using System.Net.WebSockets;

namespace Console_Student_Class_01
{
    internal class Program
    {

        public static Student RegisterStudents()
        {

            Console.WriteLine("Enter student Id:");
            int studentId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter student name:");
            string studentName = Console.ReadLine();

            //Console.WriteLine("Enter student surename:");
            //string studentSurename = Console.ReadLine();

            //Console.WriteLine("Enter student email:");
            //string studentEmail = Console.ReadLine();

            Student student = new Student();

            student.StudentID = studentId;
            student.StudentName = studentName;
            //student.StudentSurename = studentSurename;
            //student.StudentEmail = studentEmail;

            Console.WriteLine("Registered Student Information:");
            Console.WriteLine($"Student ID = {student.StudentID} Student Name = {student.StudentName} " +
                $"Student Surename = {student.StudentSurename} Student Email = {student.StudentEmail}");

            return student;

        }

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
                Console.WriteLine("2 - Display All Students");
                Console.WriteLine("3 - Find Student");
                Console.WriteLine("4 - Exit");
                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    student = RegisterStudents();
                    studentList.Add(student);
                }
                else if (answer == "2")
                {
                    //ListStudents(studentList);
                    
                    studentService.DisplayStudetnListAsync().Wait();
                }
                else if (answer == "3")
                {
                    FindStudent(studentList);
                }
                else if (answer == "4")
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
