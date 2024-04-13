using System.ComponentModel.DataAnnotations;

namespace AMSS.Models.Dto.Farm
{
    public class CreateFarmDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Area { get; set; }
        [Required]
        public int LocationId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
