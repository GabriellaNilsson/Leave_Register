using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Leave_Register.Models
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }
        public string LeaveReason { get; set; }
        public DateTime StartDateOfLeave { get; set; }
        public DateTime EndDateOfLeave { get; set; }
        [ForeignKey("Employee")]
        public int FkEmployeeId { get; set; }
        public DateTime LeaveRequestRegistrationTime { get; set; }
        public Employee? Employee { get; set; }
    }
}
