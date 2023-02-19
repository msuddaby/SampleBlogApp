using Microsoft.AspNetCore.Identity;
using SampleBlogApp.Services.Entities;

namespace SampleBlogApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<UserRoles> Roles { get; set; }
    }
}
