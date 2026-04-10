using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Data.Interfaces;

namespace DAL.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' " +
                "was not found");
        }

        public async Task<IReadOnlyList<Models.Student>> GetAllByNameAsync()
        {
            var students = new List<Student>();
            const string sql = """
                           SELECT StudentID, StudentName, StudentSurename, StudentEmail
                           FROM dbo.Students
                           Order By StudentName ASC;
                           """;

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var student = new Student
                {
                    StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                    StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                    StudentSurename = reader.GetString(reader.GetOrdinal("StudentSurename")),
                    StudentEmail = reader.GetString(reader.GetOrdinal("StudentEmail"))
                };
                students.Add(student);
            }

            return students;
        }

        public async Task<IReadOnlyList<Models.Student>> GetAllByIDAsync()
        {
            var students = new List<Student>();
            const string sql = """
                           SELECT StudentID, StudentName, StudentSurename, StudentEmail
                           FROM dbo.Students
                           """;

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var student = new Student
                {
                    StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                    StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                    StudentSurename = reader.GetString(reader.GetOrdinal("StudentSurename")),
                    StudentEmail = reader.GetString(reader.GetOrdinal("StudentEmail"))
                };
                students.Add(student);
            }

            return students;
        }

        public async Task<int> CreateAsync(Student student)
        {
            const string sql = """
                           INSERT INTO dbo.Students (StudentName, StudentSurename, StudentEmail)
                           VALUES (@StudentName, @StudentSurename, @StudentEmail);
                           SELECT SCOPE_IDENTITY();
                           """;

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@StudentName", student.StudentName);
            command.Parameters.AddWithValue("@StudentSurename", student.StudentSurename);
            command.Parameters.AddWithValue("@StudentEmail", student.StudentEmail);
            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task DeleteAsync(int id)
        {
            const string sql = "DELETE FROM dbo.Students WHERE StudentID = @StudentID;";
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@StudentID", id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            const string sql = """
                UPDATE dbo.Students 
                SET StudentName = @StudentName, StudentSurename = @StudentSurename, 
                StudentEmail = @StudentEmail WHERE StudentID = @StudentID;
                """;

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@StudentID", student.StudentID);
            command.Parameters.AddWithValue("@StudentName", student.StudentName);
            command.Parameters.AddWithValue("@StudentSurename", student.StudentSurename);
            command.Parameters.AddWithValue("@StudentEmail", student.StudentEmail);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM dbo.Students WHERE StudentID = @StudentID;";

            var student = new Student();

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@StudentID", id);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                student = new Student
                {
                    StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                    StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                    StudentSurename = reader.GetString(reader.GetOrdinal("StudentSurename")),
                    StudentEmail = reader.GetString(reader.GetOrdinal("StudentEmail"))
                };
            }

            return student;
        }

        public async Task<Student> GetByNameAsync(string name)
        {
            const string sql = "SELECT * FROM dbo.Students WHERE StudentName = @StudentName;";

            var student = new Student();

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@StudentName", name);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                student = new Student
                {
                    StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                    StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                    StudentSurename = reader.GetString(reader.GetOrdinal("StudentSurename")),
                    StudentEmail = reader.GetString(reader.GetOrdinal("StudentEmail"))
                };
            }

            return student;
        }

        public async Task<Student> GetByEmailAsync(string email)
        {
            const string sql = "SELECT * FROM dbo.Students WHERE StudentEmail = @StudentEmail;";

            var student = new Student();

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@StudentEmail", email);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                student = new Student
                {
                    StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                    StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                    StudentSurename = reader.GetString(reader.GetOrdinal("StudentSurename")),
                    StudentEmail = reader.GetString(reader.GetOrdinal("StudentEmail"))
                };
            }

            return student;
        }
    }
}
