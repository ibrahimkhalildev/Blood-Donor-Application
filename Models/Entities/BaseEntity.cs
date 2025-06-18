using System.ComponentModel.DataAnnotations;

namespace BloodDonar.MVC.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
