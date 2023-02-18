using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleBlogApp.Data;
using SampleBlogApp.Services.Helpers;
using SampleBlogApp.Services.Models;

namespace SampleBlogApp.Services.BLL
{
    public class AdminServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdminServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<UserDTO>>> GetUsersAsync()
        {
            try
            {
                var result = await _mapper.ProjectTo<UserDTO>(_context.Users).ToListAsync();
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
    }
}
