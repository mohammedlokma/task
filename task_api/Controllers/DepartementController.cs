using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using task_api.Models.Dtos;
using task_api.Models;
using task_api.Repositoy.Interfaces;

namespace task_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        protected ApiRespose _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public DepartementController(IUnitOfWork unit, IMapper mapper)
        {
            _unitOfWork = unit;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet("GetDepartements")]
        //[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiRespose>> GetDepartements()
        {
            try
            {

                IEnumerable<Departement> departements = await _unitOfWork.Departements.GetAllAsync();
                _response.Result = departements;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpGet("GetDepartementEmployees")]
        //[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiRespose>> GetDepartementEmployees(int id)
        {
            try
            {
                IEnumerable<Employee> employees = await _unitOfWork.Employees.FindAllAsync(i=>i.DepartementId == id);
                IEnumerable<EmployeeDTO> employessDto = _mapper.Map<List<EmployeeDTO>>(employees);
                _response.Result = employessDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpPost("CreateDepartement")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiRespose>> CreateDepartement([FromBody] DepartementDTO departementDTO)
        {
            try
            {

                if (await _unitOfWork.Departements.FindAsync(u => u.Name.ToLower() == departementDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Name already Exists!");
                    return BadRequest(ModelState);
                }

                if (departementDTO == null)
                {
                    return BadRequest(departementDTO);
                }

                Departement departement = _mapper.Map<Departement>(departementDTO);


                await _unitOfWork.Departements.CreateAsync(departement);
                await _unitOfWork.SaveAsync();
                _response.Result = _mapper.Map<DepartementDTO>(departement);
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("DeleteDepartement")]
        public async Task<ActionResult<ApiRespose>> DeleteDepartement(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var departement = await _unitOfWork.Departements.FindAsync(u => u.Id == id);
                if (departement == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Departements.RemoveAsync(departement);
                await _unitOfWork.SaveAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        
    }
}
