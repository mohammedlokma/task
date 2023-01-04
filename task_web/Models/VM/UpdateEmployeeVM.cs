using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using task_web.Models.DTO;

namespace task_web.Models.VM
{
    public class UpdateEmployeeVM
    {
        public UpdateEmployeeVM()
        {
            Employee = new EmployeeUpdateDto();
        }
        public EmployeeUpdateDto Employee { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DepartementList { get; set; }
    }
}
