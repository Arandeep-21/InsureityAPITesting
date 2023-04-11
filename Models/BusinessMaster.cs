using System.ComponentModel.DataAnnotations;

namespace InsureityAPI.Models
{
    public class BusinessMaster
    {
        [Key]
        public int BMId { get; set; }
        [Required]
        public string? BusinessType { get; set; }
    }
}
