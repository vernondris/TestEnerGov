using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnerGovSolutions.Models
{
    public class Hierarchy
    {
        [Key]
        public int Id { get; set; }
        public int ReportingByEmployeeId { get; set; }
        public int ReportingToEmployeeId { get; set; }
    }
}