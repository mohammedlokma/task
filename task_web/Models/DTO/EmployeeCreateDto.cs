namespace task_web.Models.DTO
{
    public class EmployeeCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartementId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
