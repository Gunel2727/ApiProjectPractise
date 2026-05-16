using Microsoft.AspNetCore.Identity;

namespace ApiProjectPractise.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; } = null!;
    }
}
