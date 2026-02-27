using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoRide.Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> ExistsAsync(string id);
    }
}
