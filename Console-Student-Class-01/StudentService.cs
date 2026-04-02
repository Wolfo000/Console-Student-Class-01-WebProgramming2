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

        public async Task DisplayStudetnListAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            foreach (Student s in students)
            {
                Console.WriteLine("Studen List Item");
                Console.WriteLine("Student ID: " + s.StudentID);
                Console.WriteLine("Studen Name: " + s.StudentName);
                Console.WriteLine("Student Surename: " + s.StudentSurename);
                Console.WriteLine("Student Email: " + s.StudentEmail);
                Console.WriteLine("--------------------------------------------");
            }
        }
    }
}
