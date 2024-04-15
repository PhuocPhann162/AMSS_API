using System.ComponentModel.DataAnnotations;

namespace AMSS.Models
{
    public class CropType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
