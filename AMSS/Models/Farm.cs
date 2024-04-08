using System.ComponentModel.DataAnnotations;

namespace AMSS.Models
{
    public class Farm
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public double Area { get; set; }
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
