using System.ComponentModel.DataAnnotations;
namespace FridgeManagementSystemV2.Models
{
    public class MaintenanceVisit
    {
        [Key]
        public int VisitId { get; set; }

        [Required]
        public int TechnicianId { get; set; }

        [Required]
        public int FridgeId { get; set; }

        [Required]
        public DateTime MainDate { get; set; }

        [Required]
        public TimeSpan MainTime { get; set; }

        public Technician Technician { get; set; }
        public Fridge Fridge { get; set; }
    }

}
