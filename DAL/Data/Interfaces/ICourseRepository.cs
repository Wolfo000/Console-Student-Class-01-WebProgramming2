using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Interfaces
{
    public interface ICourseRepository
    {
        Task<IReadOnlyList<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course?> GetByNameAsync(string courseName);
        Task<Course?> GetByCodeAsync(string courseCode);
        Task<int> CreateAsync(Course course);
        Task<bool> UpdateAsync(Course course);
        Task<bool> DeleteAsync(int id);
    }
}
