using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleBlogApp.Data;
using SampleBlogApp.Services.Entities;
using SampleBlogApp.Services.Helpers;
using SampleBlogApp.Services.Models;

namespace SampleBlogApp.Services.BLL
{
    public class AdminServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public AdminServices(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<ActionResult<List<UserDTO>>> GetUsersAsync()
        {
            try
            {
                var testResult = await _context.Users.Include(x => x.Roles).ToListAsync();
                var result = await _mapper.ProjectTo<UserDTO>(_context.Users.Include(x => x.Roles)).ToListAsync();
                if (result.Count == 0)
                {
                    return ActionResultHelper.Fail("No users found.");
                }
                return ActionResultHelper.Success(result);
            }
            catch (Exception e)
            {
                return ActionResultHelper.Fail($"Error getting users: {e.Message}");
            }
        }

        public async Task<ActionResult<List<RoleDTO>>> GetAllRolesAsync()
        {
            try
            {
                var result = await _mapper.ProjectTo<RoleDTO>(_roleManager.Roles).ToListAsync();
                if (result.Count == 0)
                {
                    return ActionResultHelper.Fail("No roles found.");
                }

                return ActionResultHelper.Success(result);
            }
            catch (Exception e)
            {
                return ActionResultHelper.Fail($"Error getting roles: {e.Message}");
            }
        }

        public async Task<ActionResult<List<string>>> GetUserRolesAsync(UserDTO inuser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(inuser.Id);
                if (user == null)
                {
                    return ActionResultHelper.Fail("User not found.");
                }
                var result = await _userManager.GetRolesAsync(user);

                return ActionResultHelper.Success(result.ToList());
            }
            catch (Exception e)
            {
                return ActionResultHelper.Fail($"Error getting user roles: {e.Message}");
            }
        }

        public async Task<ActionResult> AddUserToRoleAsync(UserDTO inuser, RoleDTO inrole)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(inuser.Id);
                if (user == null)
                {
                    return ActionResultHelper.Fail("User not found.");
                }

                var result = await _userManager.AddToRoleAsync(user, inrole.Name);
                if (result.Succeeded)
                {
                    return ActionResultHelper.Success();
                }

                return ActionResultHelper.Fail("Could not add to role.");
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
                //return ActionResultHelper.Fail(e.Message);
            }
        }
        public async Task<ActionResult> CreateRoleAsync(RoleDTO roleDTO)
        {
            try
            {
                var role = _mapper.Map<ApplicationRole>(roleDTO);
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return ActionResultHelper.Fail("Error creating role.");
                }
                return ActionResultHelper.Success();
            }
            catch (Exception e)
            {
                return ActionResultHelper.Fail($"Error creating role: {e.Message}");
            }
        }
    }
}
