using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMSS.Models
{
    public class Field
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public double Area { get; set; }

        [Required]
        public int FarmId { get; set; }

        [ForeignKey("FarmId")]
        [ValidateNever]
        public Farm Farm { get; set; }

        [Required]
        public int LocationId { get; set; }

        [NotMapped]
        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
