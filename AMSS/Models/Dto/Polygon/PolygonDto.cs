﻿using AMSS.Models.Polygon;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AMSS.Models.Dto.Farm;
using AMSS.Models.Dto.Field;

namespace AMSS.Models.Dto.Polygon
{
    public class PolygonDto
    {
        [Key]
        public int Id { get; set; }
        public string? Color { get; set; }

        [Required]
        public int FarmId { get; set; }
      
        public FarmDto Farm { get; set; }

        [Required]
        public int FieldId { get; set; }
        public FieldDto Field { get; set; }

        public IEnumerable<Position> Positions { get; set; }
    }
}
