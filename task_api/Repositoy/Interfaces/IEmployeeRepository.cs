using task_api.Models;
using task_api.Models.Dtos;

namespace task_api.Repositoy.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee> UpdateAsync(Employee employee, int id);
    }
}
