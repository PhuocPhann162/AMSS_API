using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMSS.Models.Dto.Field
{
    public class FieldDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Area { get; set; }
        [Required]
        public int FarmId { get; set; }
        [Required]  
        public int LocationId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
