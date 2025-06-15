using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonar.MVC.Models.Entities
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }
        public required DateTime DonationDate { get; set; }
        [MaxLength(50)]
        public required string HospitalName { get; set; }
        [MaxLength(100)]
        public required string Location { get; set; }
        [ForeignKey("BloodDonor")]
        public required int BloodDonationId { get; set; }
    }
}
