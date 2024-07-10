

using System.ComponentModel.DataAnnotations;

namespace TestIgnatov.Models.ViewModels
{
    public class UserRegister
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

    }
}