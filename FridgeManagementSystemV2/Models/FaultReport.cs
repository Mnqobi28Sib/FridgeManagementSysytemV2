using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystemV2.Models
{
    

    public class FaultReport
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public int FridgeId { get; set; }

        [Required]
        public DateTime ReportDate { get; set; }

        [Required]
        [StringLength(255)]
        public string ReportDesc { get; set; }

        public Fridge Fridge { get; set; }
        public bool IsResolved { get; set; } = false;
    }

}
