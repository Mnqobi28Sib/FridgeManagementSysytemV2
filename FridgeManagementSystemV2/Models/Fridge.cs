using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystemV2.Models
{
    public class Fridge
    {
        [Key]
        public int FridgeId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string ModelNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; }

        [Required]
        public DateTime InstallationDate { get; set; }

        public Customer Customer { get; set; }
        public virtual ICollection<FaultReport> FaultReports { get; set; } = new List<FaultReport>();
    }
}


