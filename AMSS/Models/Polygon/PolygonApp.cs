using System.ComponentModel.DataAnnotations;

namespace AMSS.Models.Polygon
{
    public class PolygonApp
    {
        [Key]
        public int Id { get; set; }
        public string? Color { get; set; }
    }
}
