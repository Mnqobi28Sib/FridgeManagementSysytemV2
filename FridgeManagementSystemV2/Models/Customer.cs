using System.ComponentModel.DataAnnotations;

namespace FridgeManagementSystemV2.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }  

        [Required]
        [StringLength(50)]
        public string ContactInfo { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }
        public List<Fridge> Fridges { get; set; }
    }
}
