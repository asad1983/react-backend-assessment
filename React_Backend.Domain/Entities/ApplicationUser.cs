

using Microsoft.AspNetCore.Identity;

namespace React_Backend.Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
