using task.Const;
using task.Services;
using task_web.Models.DTO;
using task_web.Services.Interfaces;

namespace task_web.Services
{
    public class DepartementService: BaseService, IDepartementService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string employeeUrl;
        public DepartementService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            employeeUrl = configuration.GetValue<string>("ServiceUrls:TaskAPI");

        }
        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = employeeUrl + "/api/Departement/GetDepartements",
            });

        }
        public Task<T> CreateAsync<T>(DepartementCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = employeeUrl + "/api/Departement/CreateDepartement"

            });
        }
        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = employeeUrl + "/api/Departement/DeleteDepartement?id=/" + id,
            });
        }
    }
}
