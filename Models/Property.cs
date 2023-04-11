using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsureityAPI.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        [Required]
        public string? PropertyName { get; set; }
        [Required]
        public int PropertyAge { get; set; }
        [Required]
        public string? OwnershipType { get; set;}
        [Required]
        [Range(0, double.MaxValue)]
        public double AssetCost { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double SalvageValue { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int UsefulLife { get; set; }
        [Range(0, double.MaxValue)]
        public double? DepreciationExpense { get; set; }
        [Range(0, 10)]
        public int PropertyScore { get; set; }
        [ForeignKey("BusinessID")]
        [Required]
        public int BusinessID { get; set; }
        public bool IsInsured { get; set; } = false;
        public virtual Business? Business { get; set; }
        [ForeignKey("PropertyMaster")]
        [Required]
        public int PMId { get; set; }
        public virtual PropertyMaster? PropertyMaster { get; set; }

    }
}
