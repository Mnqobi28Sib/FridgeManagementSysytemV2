using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystemV2.Models
{
    

    public class ServicePerformed
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        public int VisitId { get; set; }

        [Required]
        [StringLength(255)]
        public string ServiceDesc { get; set; }

        public MaintenanceVisit MaintenanceVisit { get; set; }
    }

}
