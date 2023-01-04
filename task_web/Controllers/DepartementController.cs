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
    public class DepartementController : Controller
    {
        private readonly IDepartementService _departementService;
        private readonly IMapper _mapper;
        public DepartementController(IDepartementService departementService,IMapper mapper)
        {
            _departementService = departementService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<DepartementDto> list = new();

            var response = await _departementService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<DepartementDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> DepartementCreate()
        {
            DepartementCreateDto departementDto = new();
            return View(departementDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DepartementCreate(DepartementCreateDto model)
        {
           // var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {

                var response = await _departementService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Departement created successfully";
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
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void DeleteDepartement(int departementId)
        {

            var response = await _departementService.DeleteAsync<APIResponse>(departementId);
           
            // return RedirectToAction(nameof(Index));
            
        }
    }
}
