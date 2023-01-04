using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using task_api.Models;
using task_api.Models.Dtos;
using task_api.Repositoy.Interfaces;

namespace task_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        protected ApiRespose _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public EmployeeController(IUnitOfWork unit,IMapper mapper)
        {
            _unitOfWork = unit;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet("GetEmployess")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiRespose>> GetEmployees()
        {
            try
            {

                IEnumerable<Employee> employees = await _unitOfWork.Employees.GetAllAsync();
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

        [HttpGet("GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiRespose>> GetEmployee(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var employee = await _unitOfWork.Employees.FindAsync(u => u.Id == id);
                if (employee == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<EmployeeDTO>(employee);
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
        [HttpPost("CreateEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiRespose>> CreateEmployee([FromBody] EmployeeCreateDTO employeeDto)
        {
            try
            {
                
                if (await _unitOfWork.Employees.FindAsync(u => u.Name.ToLower() == employeeDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "UserName already Exists!");
                    return BadRequest(ModelState);
                }

                if (employeeDto == null)
                {
                    return BadRequest(employeeDto);
                }
               
                Employee employee = _mapper.Map<Employee>(employeeDto);

                
                await _unitOfWork.Employees.CreateAsync(employee);
               await  _unitOfWork.SaveAsync();
                _response.Result = _mapper.Map<EmployeeDTO>(employee);
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
        [HttpPut("UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiRespose>> UpdateEmployee(int id, [FromBody] EmployeeUpdateDTO updateDTO)
        {
            try
            {
                var checkEmployee = await _unitOfWork.Employees.FindAsync(u => u.Id == id);
                if (updateDTO == null || checkEmployee == null  )
                {
                    return BadRequest();
                }

                Employee employee = _mapper.Map<Employee>(updateDTO);
                 await _unitOfWork.Employees.UpdateAsync(employee,id);
                await _unitOfWork.SaveAsync();
                _response.Result = _mapper.Map<EmployeeDTO>(employee);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("DeleteEmployee")]
        public async Task<ActionResult<ApiRespose>> DeleteEmployee(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var employee = await _unitOfWork.Employees.FindAsync(u => u.Id == id);
                if (employee == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Employees.RemoveAsync(employee);
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
