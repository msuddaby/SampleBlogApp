using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SampleBlogApp.Data;

namespace SampleBlogApp.Services.Entities
{
    public class UserRoles: IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual string UserId { get; set; }
        public virtual ApplicationRole Role { get; set; }
        public virtual string RoleId { get; set; }
    }
}
