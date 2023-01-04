using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace task_api.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }

        [ForeignKey("Departement")]
        public int DepartementId { get; set; }

        public Departement Departement { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }

    }
}
