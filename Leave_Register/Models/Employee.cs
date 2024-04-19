using System.ComponentModel.DataAnnotations;

namespace Leave_Register.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public string Gender { get; set; }
    }
}
