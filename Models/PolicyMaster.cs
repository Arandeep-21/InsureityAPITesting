using System.ComponentModel.DataAnnotations;

namespace InsureityAPI.Models
{
    public class PolicyMaster
    {
        [Key]
        public int PlId { get; set; }
        [Required]
        public string? PolicyType { get; set; }
        [Required]
        public double AssuredSum { get; set; }
        [Required]
        public string? PropertyType { get; set; }
        [Required]
        public double BasePremium { get; set; }
        [Required]
        public double BusinessPenalty { get; set; }
        [Required]
        public double PropertyPenalty { get; set; }

    }
}
