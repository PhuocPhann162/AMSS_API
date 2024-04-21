using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using AMSS.Models.Dto.Field;
using AMSS.Models.Dto.CropType;

namespace AMSS.Models.Dto.Crop
{
    public class CropDto
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string? Icon { get; set; }

        [Required]
        public string Name { get; set; }

        public double CultivatedArea { get; set; }

        public DateTime PlantedDate { get; set; }

        public DateTime ExpectedDate { get; set; }


        public int Quantity { get; set; }

        public int? FieldId { get; set; }
        public virtual FieldDto Field { get; set; }


        public int? CropTypeId { get; set; }
        public virtual CropTypeDto CropType { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
