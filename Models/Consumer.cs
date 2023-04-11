using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsureityAPI.Models
{
    public class Consumer
    {
        [Key]
        public int ConsumerId { get; set; }
        [Required]
        public string? ConsumerName { get; set; }
        [Required]
        public string? ConsumerEmail { get; set; }
        [Required]
        public string? PAN { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [ForeignKey("Agent")]
        public int? UserId { get; set; }
        public virtual LoginDetails? Agent { get; set; }
    }
}
