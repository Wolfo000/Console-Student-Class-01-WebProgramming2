using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllByNameAsync();
        Task<IReadOnlyList<T>> GetAllByIDAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string name);
        Task<T> GetByEmailAsync(string email);
        Task<int> CreateAsync(T T);
        Task UpdateAsync(T T);
        Task DeleteAsync(int id);
    }
}
