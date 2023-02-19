using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SampleBlogApp.Services.Entities;

namespace SampleBlogApp.Services.Helpers
{
    public class SeedData
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public SeedData(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public static readonly List<ApplicationRole> Roles = new List<ApplicationRole>
        {
            new ApplicationRole { Name = "Admin", NormalizedName = "ADMIN",Style = "color: red" },
        };

        public static readonly Dictionary<string, List<Permissions>> RoleClaims = new Dictionary<string, List<Permissions>>
        {
            { "Admin", new List<Permissions> {Permissions.CanCreate, Permissions.CanDelete, Permissions.CanEdit} }
        };

        public async Task SeedRolesAsync()
        {
            foreach (var role in Roles)
            {
                if (await _roleManager.FindByNameAsync(role.Name) == null)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }

        public async Task SeedRoleClaimsAsync()
        {
            foreach (var roleClaim in RoleClaims)
            {
                var role = await _roleManager.FindByNameAsync(roleClaim.Key);
                if (role == null)
                {
                    continue;
                }
                var claims = await _roleManager.GetClaimsAsync(role);

                foreach (var claim in roleClaim.Value)
                {
                    var exists = claims.Any(c => c.Type == claim.ToString());
                    if (exists)
                    {
                        continue;
                    }
                    
                    await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(claim.ToString(), "true"));
                }
            }
        }

    }
}
