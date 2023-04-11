using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsureityAPI.Models
{
    public class Business
    {
        [Key]
        public int BusinessId { get; set; }
        [Required]
        public string? BusinessName { get; set; }
        [Required]
        public string? BusinessType { get; set;}
        [Required]
        public string? BusinessLocation { get; set;}
        [Required]
        [Range(0, double.MaxValue)]
        public double BusinessTurnover { get; set;}
        [Required]
        [Range(0, double.MaxValue)]
        public double CapitalInvested { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int TotalEmployees { get; set; }
        [Range(0,100)]
        public double? ROI { get; set; }
        [Range(0, 10)]
        public int? BusinessScore { get; set; }
        [Required]
        [ForeignKey("ConsumerId")]
        public int? ConsumerId { get; set; }
        public virtual Consumer? Consumer { get; set; }
        [Required]
        [ForeignKey("BusinessMaster")]
        public int? BMId { get; set; }
        public virtual BusinessMaster? BusinessMaster { get; set; }
        public ICollection<Property>? Properties { get; set; }

    }
}
