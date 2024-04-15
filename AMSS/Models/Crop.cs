using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        public double CultivatedArea { get; set; }

        public DateTime PlantedDate { get; set; }

        public DateTime ExpectedDate { get; set; }

        [Required]
        public int CropTypeId { get; set; }

        [ForeignKey("CropTypeId")]
        [ValidateNever]
        public CropType CropType { get; set; }

        public int Quantity { get; set; }

        [Required]
        public int FieldId { get; set; }

        [ForeignKey("FieldId")]
        [ValidateNever]
        public Field Field { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
