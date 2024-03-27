using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AMSS.Enums;
using System.Text.Json.Serialization;

namespace AMSS.Models.Dto.User
{
    public class UserDto
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        [StringLength(500)]
        public string? Avatar { get; set; }

        public string Role { get; set; }
    }
}
