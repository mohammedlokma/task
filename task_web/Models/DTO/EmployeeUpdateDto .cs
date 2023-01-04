namespace task_web.Models.DTO
{
    public class EmployeeUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartementId { get; set; }
    }
}
