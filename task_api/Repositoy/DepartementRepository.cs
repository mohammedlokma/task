using task_api.Data;
using task_api.Models;
using task_api.Repositoy.Interfaces;

namespace task_api.Repositoy
{
    public class DepartementRepository : BaseRepository<Departement>, IDepartementRepository
    {
        private readonly ApplicationDbContext _db;
        public DepartementRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Departement> UpdateAsync(Departement entity)
        {
             _db.Departements.Update(entity);
           
            return entity;
        }
    }
}
