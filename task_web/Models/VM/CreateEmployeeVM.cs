using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using task_web.Models.DTO;

namespace task_web.Models.VM
{
    public class CreateEmployeeVM
    {
        public CreateEmployeeVM()
        {
            Employee = new EmployeeCreateDto();
        }
        public EmployeeCreateDto Employee { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DepartementList { get; set; }
    }
}
