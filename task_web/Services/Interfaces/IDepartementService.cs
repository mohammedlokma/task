using task_web.Models.DTO;

namespace task_web.Services.Interfaces
{
    public interface IDepartementService
    {
        Task<T> CreateAsync<T>(DepartementCreateDto dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> GetAllAsync<T>();
    }
}
