using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using SampleBlogApp.Areas.Identity;
using SampleBlogApp.Data;
using AutoMapper;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Internal;
using SampleBlogApp.Helpers;
using SampleBlogApp.Services.BLL;
using SampleBlogApp.Services.Entities;
using SampleBlogApp.Services.Helpers;

namespace SampleBlogApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("SampleBlogApp.Services")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddTransient<BlogPostServices>();
            builder.Services.AddTransient<AdminServices>();
            builder.Services.AddScoped<PageHistoryState>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            

            

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
                var seeder = new SeedData(roleManager);

                await seeder.SeedRolesAsync();
                await seeder.SeedRoleClaimsAsync();

            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            await app.RunAsync();
        }
    }
}