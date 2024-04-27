using AMSS.Models.Polygon;

namespace AMSS.Models.Dto.Field
{
    public class UpdateFieldDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public  double? Area { get; set; }
        public IEnumerable<Position>? Positions { get; set; }
    }
}
