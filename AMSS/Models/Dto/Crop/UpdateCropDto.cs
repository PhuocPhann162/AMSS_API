using System.ComponentModel.DataAnnotations;

namespace AMSS.Models.Dto.Crop
{
    public class UpdateCropDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int CropTypeId { get; set; }

        public int Quantity { get; set; }

        public double CultivatedArea { get; set; }

        public DateTime PlantedDate { get; set; }

        public DateTime ExpectedDate { get; set; }

        public int FieldId { get; set; }

        public IFormFile File { get; set; }
    }
}
