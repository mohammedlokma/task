using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using task.Services;
using task_web.Models.DTO;
using task_web.Models.VM;
using task_web.Services.Interfaces;

namespace task_web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartementService _departementService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService,IDepartementService departementService,IMapper mapper)
        {
            _employeeService = employeeService;
            _departementService = departementService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<EmployeeDto> list = new();

            var response = await _employeeService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<EmployeeDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> EmployeeCreate()
        {
            CreateEmployeeVM createEmployeeVm = new();
            var response = await _departementService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                createEmployeeVm.DepartementList = JsonConvert.DeserializeObject<List<DepartementDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value=i.Id
                    });
            }
            return View(createEmployeeVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeCreate(CreateEmployeeVM model)
        {
            if (ModelState.IsValid)
            {

                var response = await _employeeService.CreateAsync<APIResponse>(model.Employee);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Employee created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
        public async Task<IActionResult> EmployeeUpdate(int employeeId)
        {
            UpdateEmployeeVM updateEmployeeVM = new();
            var response = await _employeeService.GetAsync<APIResponse>(employeeId);
            if (response != null && response.IsSuccess)
            {
                EmployeeDto model = JsonConvert.DeserializeObject<EmployeeDto>(Convert.ToString(response.Result));
                updateEmployeeVM.Employee = _mapper.Map<EmployeeUpdateDto>(model);
            }

            response = await _departementService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                updateEmployeeVM.DepartementList = JsonConvert.DeserializeObject<List<EmployeeDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(updateEmployeeVM);
            }


            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeUpdate(UpdateEmployeeVM model)
        {
            if (ModelState.IsValid)
            {

                var response = await _employeeService.UpdateAsync<APIResponse>(model.Employee);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _employeeService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.DepartementList = JsonConvert.DeserializeObject<List<EmployeeDto>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void DeleteEmployee(int employeeId)
        {

            var response = await _employeeService.DeleteAsync<APIResponse>(employeeId);
           
            // return RedirectToAction(nameof(Index));
            
        }
    }
}
