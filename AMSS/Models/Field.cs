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
        public double Area { get; set; }
        public int FarmId { get; set; }
        public int LocationId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        [ForeignKey("LocationId")]
        public Farm Location { get; set; }
        [ForeignKey("FarmId")]
        public Farm Farm { get; set; }
    }
}
