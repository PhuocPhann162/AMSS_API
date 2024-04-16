
using AMSS.Models.Polygon;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMSS.Models
{
    public class Farm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Area { get; set; }

        [Required]
        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        [ValidateNever]
        public Location Location { get; set; }

        [Required]
        public int PolygonAppId { get; set; }

        [ForeignKey("PolygonAppId")]
        [ValidateNever]
        public PolygonApp? PolygonApp { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        
    }
}
