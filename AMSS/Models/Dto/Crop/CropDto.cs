using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMSS.Models.Dto.Crop
{
    public class CropDto
    {
        public int Id { get; set; }
        public string? Icon { get; set; }

        public string Name { get; set; }

        public int CropTypeId { get; set; }

        public int Quantity { get; set; }

        public double CultivatedArea { get; set; }

        public DateTime PlantedDate { get; set; }

        public DateTime ExpectedDate { get; set; }

        public int FieldId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
