using task.Const;
using task.Services;
using task_web.Models.DTO;
using task_web.Services.Interfaces;

namespace task_web.Services
{
    public class EmployeeService: BaseService,IEmployeeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string employeeUrl;
        public EmployeeService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            employeeUrl = configuration.GetValue<string>("ServiceUrls:TaskAPI");

        }
        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = employeeUrl + "/api/Employee/GetEmployess",
            });
           
        }
        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = employeeUrl + "/api/Employee/GetEmployee?id=" + id,
            });
        }
        public Task<T> UpdateAsync<T>(EmployeeUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = employeeUrl + "/api/Employee/UpdateEmployee?id=" + dto.Id ,
            });
        }
        public Task<T> CreateAsync<T>(EmployeeCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = employeeUrl + "/api/Employee/CreateEmployee"

            });
        }
        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = employeeUrl + "/api/Employee/DeleteEmployee?id=/" + id,
            });
        }
    }
}
