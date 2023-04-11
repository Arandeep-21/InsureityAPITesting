using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsureityAPI.Models
{
    public class Policy
    {
        [Key]
        public int PolicyId { get; set; }
        [Required]
        public string? PolicyName { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double AssuredSum { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double PremiumRate { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double PremiumAmount { get; set; }
        [Required]
        public string? PolicyStatus { get; set; }
        [Required]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? IssuedAt { get; set; } = null;
        [ForeignKey("PolicyMaster")]
        [Required]
        public int PlId { get; set; }
        public virtual PolicyMaster? PolicyMaster { get; set; }

        [ForeignKey("PropertyId")]
        [Required]
        public int PropertyId { get; set; }
        public virtual Property? Property { get; set; }

        [ForeignKey("QuoteId")]
        public int? QuoteId { get; set; } = null;

        [ForeignKey("AgentId")]
        public int? AgentId { get; set; }

        

    }
}
