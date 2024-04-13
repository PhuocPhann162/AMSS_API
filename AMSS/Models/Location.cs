using System.ComponentModel.DataAnnotations;

namespace AMSS.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Address { get; set; }
        public float? Lat { get; set; }
        public float? Lng { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
