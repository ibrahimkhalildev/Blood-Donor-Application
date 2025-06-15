using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace BloodDonar.MVC.Models.Entities
{
    public class BloodDonor
    {
        internal object bloodGroup;

        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        [Phone, Length(10,50)] 
        public required string ContactNumber { get; set; }
        [Display(Name="Date of Birth")]
        public required DateTime DateOFBirth { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required BloodGroup BloodGroup { get; set; }
        [Range(50,150)]
        [Display(Name = "Weight (kg)")]
        public float Weight { get; set; }
        public string? Address { get; set; } 
        [Display(Name ="Last Donation Date")]
        public DateTime? LastDonationDate { get; set; }
        public string? ProfilePicture { get; set; }
        public Collection<Donation> Donation { get; set; } = new Collection<Donation>();
    }
}
