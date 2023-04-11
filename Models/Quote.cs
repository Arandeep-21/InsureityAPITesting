
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsureityAPI.Models
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }
        [Required]
        [ForeignKey("ConsumerId")]
        public int? ConsumerId { get; set; }
        [ForeignKey("BusinessID")]
        [Required]
        public int BusinessID { get; set; }
        public virtual Business? Business { get; set; }
        public double QuoteAmount { get; set; } = 0;
        public int Tenure { get; set; } = 1;
        public ICollection<Policy>? Policies { get; set; }
        [NotMapped]
        public virtual LoginDetails? Agent { get; set;}
        [NotMapped]
        public virtual Consumer? Consumer { get; set; }
    }
}
