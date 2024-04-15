using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AMSS.Models.Dto.Location;

namespace AMSS.Models.Dto.Farm
{
    public class FarmDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Area { get; set; }
        [Required]
        public int LocationId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public LocationDto Location { get; set; }
       
    }
}
