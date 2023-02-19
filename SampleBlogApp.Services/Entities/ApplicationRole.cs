using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SampleBlogApp.Data;

namespace SampleBlogApp.Services.Entities
{
    public class ApplicationRole: IdentityRole
    {
        public string Style { get; set; }
        public List<UserRoles> Users { get; set; }
    }
}
