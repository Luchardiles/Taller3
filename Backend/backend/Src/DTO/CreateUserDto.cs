using System.ComponentModel.DataAnnotations;
using backend.Src.DataAnnotations;

namespace backend.Src.DTO
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "The fullname is required")]
        [MaxLength(150, ErrorMessage = "The fullname must be less than 150 characters")]
        [MinLength(10, ErrorMessage = "The fullname must be at least 10 characters")]
        public string Fullname { get; set; } = string.Empty;
        [Required(ErrorMessage = "The rut is required")]
        [Rut(ErrorMessage = "The rut is not valid")]
        public string Rut { get; set; } = string.Empty;
        [Required(ErrorMessage = "The email is required")]
        [UCNEmailAddress(ErrorMessage = "The email is not valid")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "The birthday is required")]
        [BirthdateRange(ErrorMessage = "The birthday is not valid")]
        public int Birthday { get; set; }

        public string Password { get; set; } = string.Empty;

    }
}