using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public int CropTypeId { get; set; }
        

        public int Quantity { get; set; }

        public double CultivatedArea { get; set; }

        public DateTime PlantedDate { get; set; }

        public DateTime ExpectedDate { get; set; }

        public int FieldId { get; set; }
        

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        [ForeignKey("FieldId")]
        public Field Field { get; set; }

        [NotMapped]
        [ForeignKey("CropTypeId")]
        public CropType CropType { get; set; }
    }
}
