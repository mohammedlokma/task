using task_web.Models.DTO;

namespace task_web.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<T> CreateAsync<T>(EmployeeCreateDto dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> UpdateAsync<T>(EmployeeUpdateDto dto);
    }
}
