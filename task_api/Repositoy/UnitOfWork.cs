using AutoMapper;
using Microsoft.AspNetCore.Identity;
using task_api.Data;
using task_api.Repositoy.Interfaces;

namespace task_api.Repositoy
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        


        public UnitOfWork(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            
            Employees = new EmployeeRepository(_db,_mapper);
            Departements = new DepartementRepository(_db);
        }
        public IEmployeeRepository Employees { get; private set; }

        public IDepartementRepository Departements { get; private set; }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }


        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
