using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMSS.Models
{
    public class Crop
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string? Icon { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CropTypeId { get; set; }
        [ForeignKey("CropTypeId")]
        public CropType CropType { get; set; }

        public double PlantedArea { get; set; }

        public DateTime PlantedDate { get; set; }

        public DateTime ExpectedDate { get; set; }

        public int FieldId { get; set; }

        public string PlantedLocation { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
