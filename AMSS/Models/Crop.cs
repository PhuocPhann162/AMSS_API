using System.ComponentModel.DataAnnotations;

namespace AMSS.Models
{
    public class Crop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        public DateTime PlanDate { get; set; }
        public DateTime HarvestDate { get; set; }
    }
}
