using AMSS.Models.Dto.CropType;
using AMSS.Models.Dto.Field;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMSS.Models.Dto.Crop
{
    public class CreateCropDto
    {
        [StringLength(500)]
        public string? Icon { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public double CultivatedArea { get; set; }

        public DateTime PlantedDate { get; set; }

        public DateTime ExpectedDate { get; set; }


        public int Quantity { get; set; }

        public int? FieldId { get; set; }
        public virtual FieldDto Field { get; set; }


        public int? CropTypeId { get; set; }
        public virtual CropTypeDto CropType { get; set; }

        public IFormFile File { get; set; }
    }
}
