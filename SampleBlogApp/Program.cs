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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;

namespace SampleBlogApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //??
            

            // Add services to the container.
            Console.WriteLine($"ENV: {builder.Environment.EnvironmentName}");

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

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "YourAppCookieName";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.CanEdit, policy => policy.RequireClaim(Permissions.CanEdit));
                options.AddPolicy(Permissions.CanCreate, policy => policy.RequireClaim(Permissions.CanCreate));
                options.AddPolicy(Permissions.CanDelete, policy => policy.RequireClaim(Permissions.CanDelete));
            });
                
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
                var context = services.GetRequiredService<ApplicationDbContext>();
                var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
                var seeder = new SeedData(roleManager);
                await context.Database.MigrateAsync();

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