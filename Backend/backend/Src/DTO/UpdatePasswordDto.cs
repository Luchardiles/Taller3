using System.ComponentModel.DataAnnotations;

namespace backend.Src.DTO
{
    public class UpdatePasswordDto
    {
        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New password is required")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must have a length between 6 and 15 characters.")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm new Password is required")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must have a length between 6 and 15 characters.")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}