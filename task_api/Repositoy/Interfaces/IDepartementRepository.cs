using task_api.Models;

namespace task_api.Repositoy.Interfaces
{
    public interface IDepartementRepository : IBaseRepository<Departement>
    {
        Task<Departement> UpdateAsync(Departement entity);
    }
}
