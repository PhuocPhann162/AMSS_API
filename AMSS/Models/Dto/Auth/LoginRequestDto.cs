using System.ComponentModel.DataAnnotations;

namespace AMSS.Models.Dto.Auth
{
    public class LoginRequestDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
