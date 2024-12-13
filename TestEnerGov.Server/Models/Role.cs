using System.ComponentModel.DataAnnotations.Schema;

namespace EnerGovSolutions.Models
{
    public class Role
    {
        public int Id { get; set; }
        public int RoleMasterFileId { get; set; }
        public int EmployeeId { get; set; } 
    }
}