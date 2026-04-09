using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Interfaces;
using DAL.Data;
using DAL.Models;

namespace Console_Student_Class_01
{
    public class StudentService
    {

        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task DisplayStudetnListByNameAsync()
        {
            var students = await _studentRepository.GetAllByNameAsync();

            foreach (Student s in students)
            {
                Console.WriteLine("Studen List Item");
                Console.WriteLine("Student ID: " + s.StudentID);
                Console.WriteLine("Studen Name: " + s.StudentName);
                Console.WriteLine("Student Surename: " + s.StudentSurename);
                Console.WriteLine("Student Email: " + s.StudentEmail);
                Console.WriteLine("--------------------------------------------");
            }

            Console.WriteLine($"\nTotal number of students students: {students.Count}\n\n\n");
        }

        public async Task DisplayStudetnListByIDAsync()
        {
            var students = await _studentRepository.GetAllByIDAsync();

            foreach (Student s in students)
            {
                Console.WriteLine("Studen List Item");
                Console.WriteLine("Student ID: " + s.StudentID);
                Console.WriteLine("Studen Name: " + s.StudentName);
                Console.WriteLine("Student Surename: " + s.StudentSurename);
                Console.WriteLine("Student Email: " + s.StudentEmail);
                Console.WriteLine("--------------------------------------------");
            }

            Console.WriteLine($"\nTotal number of students students: {students.Count}\n\n\n");
        }

        public async Task CreateStudentAsync(Student student)
        {
            int newStudentId = await _studentRepository.CreateAsync(student);
            Console.WriteLine($"New student created with ID: {newStudentId}");
            //Console.WriteLine("New student created with ID: " + newStudentId);
            Console.WriteLine("--------------------------------------------");
        }

        public static Student RegisterStudents()
        {
            Console.WriteLine("Enter student name:");
            string studentName = Console.ReadLine();

            Console.WriteLine("Enter student surename:");
            string studentSurename = Console.ReadLine();

            Console.WriteLine("Enter student email:");
            string studentEmail = Console.ReadLine();

            return new Student
            {
                StudentName = studentName,
                StudentSurename = studentSurename,
                StudentEmail = studentEmail
            };
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            Console.WriteLine("Enter Student ID to delete:");
            studentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Are you sure you want to delete student with ID {studentId}? (y/n)");
            string answer = Console.ReadLine();
            if (answer?.ToLower() == "y")
            {
                await _studentRepository.DeleteAsync(studentId);
                Console.WriteLine($"Student with ID {studentId} has been deleted.");
                Console.WriteLine("--------------------------------------------");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
                Console.WriteLine("--------------------------------------------");
            }
        }

        public async Task UpdateStudentAsync(Student student)
        {
            Console.WriteLine("Enter Student ID to update:");
            int studentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new student name:");
            string studentName = Console.ReadLine();
            Console.WriteLine("Enter new student surename:");
            string studentSurename = Console.ReadLine();
            Console.WriteLine("Enter new student email:");
            string studentEmail = Console.ReadLine();

            student.StudentID = studentId;
            student.StudentName = studentName;
            student.StudentSurename = studentSurename;
            student.StudentEmail = studentEmail;

            await _studentRepository.UpdateAsync(student);
            Console.WriteLine($"Student with ID {student.StudentID} and name {student.StudentName} has been updated.");
            Console.WriteLine("--------------------------------------------");
        }
    }
}
