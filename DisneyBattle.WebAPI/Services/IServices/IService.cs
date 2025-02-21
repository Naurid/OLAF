using DisneyBattle.WebAPI.Models;

namespace DisneyBattle.WebAPI.Services.IServices
{
    public interface IService<T>

    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}