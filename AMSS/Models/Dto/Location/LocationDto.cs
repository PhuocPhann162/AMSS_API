﻿using System.ComponentModel.DataAnnotations;

namespace AMSS.Models.Dto.Location
{
    public class LocationDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Address { get; set; }
        public float? Lat { get; set; }
        public float? Lng { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
