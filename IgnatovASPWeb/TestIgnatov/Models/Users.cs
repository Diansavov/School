
using Microsoft.AspNetCore.Identity;

namespace TestIgnatov.Models
{
    public class Users : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
    }
}