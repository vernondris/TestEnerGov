using System.ComponentModel.DataAnnotations.Schema;

namespace EnerGovSolutions.Models
{
    public class RoleMasterFile
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}