using AutoMapper;
using Microsoft.EntityFrameworkCore;
using task_api.Data;
using task_api.Models;
using task_api.Models.Dtos;
using task_api.Repositoy.Interfaces;

namespace task_api.Repositoy
{
    public class EmployeeRepository: BaseRepository<Employee>, IEmployeeRepository
    {

        private readonly ApplicationDbContext _db;
       
        private readonly IMapper _mapper;

        public EmployeeRepository(ApplicationDbContext db, IMapper mapper):base(db)
        {
            _db = db;
            _mapper = mapper;
          
        }
        public async Task<Employee> UpdateAsync(Employee employee, int id)
        {
            var entity = _db.Employess.AsNoTracking().FirstOrDefault(c => c.Id == id);
            employee.Id = id;
            employee.username = entity.username;
            employee.password = entity.password; 
             _db.Employess.Update(employee);

            return employee;
        }

    }
}
