using System.ComponentModel.DataAnnotations;

namespace AMSS.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Address { get; set; }
        public float? Lat { get; set; }
        public float? Lng { get; set; }

    }
}
