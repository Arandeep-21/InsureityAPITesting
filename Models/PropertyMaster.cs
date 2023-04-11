using System.ComponentModel.DataAnnotations;

namespace InsureityAPI.Models
{
    public class PropertyMaster
    {
        [Key]
        public int PMId { get; set; }
        [Required]
        public string? PropertyType { get; set; }
    }
}
