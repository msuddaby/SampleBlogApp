using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleBlogApp.Services.Entities;

namespace SampleBlogApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, UserRoles, ApplicationUserLogins, ApplicationRoleClaims, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
                {
                    b.HasMany(u => u.Roles)
                        .WithOne(ur => ur.User)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
                });

            builder.Entity<ApplicationRole>(b =>
            {
                b.HasMany(e => e.Users)
                    .WithOne(e => e.Role)
                    .HasForeignKey(e => e.RoleId)
                    .IsRequired();
            });

        }

        
        public DbSet<Post> Posts { get; set; }
        public DbSet<ImagePost> ImagePosts { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}