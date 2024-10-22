using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystemV2.Models
{
    public class Technician
    {
        [Key]
        public int TechnicianId { get; set; }

        [Required]
        [StringLength(100)]
        public string TechName { get; set; }

        [Required]
        [StringLength(100)]
        public string TechContactDetails { get; set; }

        [Required]
        [StringLength(255)]
        public string TechCertificationDetails { get; set; }
        public List<FaultReport> FaultReports { get; set; }
    }
}
