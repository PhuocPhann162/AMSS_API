using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMSS.Models.Polygon
{
    public class PolygonApp
    {
        [Key]
        public int Id { get; set; }
        public string? Color { get; set; }

        public int? FarmId { get; set; }
        [ForeignKey("FarmId")]
        [ValidateNever]
        public Farm Farm { get; set; }

        public int? FieldId { get; set; }
        [ForeignKey("FieldId")]
        [ValidateNever]
        public Field Field { get; set; }

        public IEnumerable<Position> Positions { get; set; }
    }
}
