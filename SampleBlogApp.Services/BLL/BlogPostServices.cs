using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleBlogApp.Data;
using AutoMapper;
using SampleBlogApp.Services.Helpers;
using SampleBlogApp.Services.Models;

namespace SampleBlogApp.Services.BLL
{
    public class BlogPostServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BlogPostServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<BlogPostDTO>>> GetBlogPostsAsync()
        {
            try
            {
                var result = await _mapper.ProjectTo<BlogPostDTO>(_context.BlogPosts.OrderByDescending(x => x.Date).Include(x => x.User))
                    .ToListAsync();

                if (result.Count == 0)
                {
                    return ActionResultHelper.Fail("No items found.");
                }
                return ActionResultHelper.Success(result);
            }
            catch (Exception e)
            {
                return ActionResultHelper.Fail($"Error getting blog posts: {e.Message}");
            }

            
            
        }

        public async Task<ActionResult<int>> CreateBlogPostAsync(BlogPostDTO blogPostDTO)
        {
            if (blogPostDTO.User == null)
            {
                return ActionResultHelper.Fail("User is required.");
            }
            blogPostDTO.Date = DateTime.Now;
            var blogPost = _mapper.Map<BlogPost>(blogPostDTO);
            blogPost.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == blogPostDTO.User.Id);

            if (blogPost.User == null)
            {
                return ActionResultHelper.Fail("User not found.");
            }

            try
            {
                await _context.BlogPosts.AddAsync(blogPost);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return ActionResultHelper.Fail(e.Message);
            }
            

            return ActionResultHelper.Success(blogPost.Id);
        }

        public async Task<ActionResult<BlogPostDTO>> GetBlogPostByIdAsync(int id)
        {
            var result = await _mapper.ProjectTo<BlogPostDTO>(_context.BlogPosts.Include(x => x.User)).FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return ActionResultHelper.Fail("Blog post not found.");
            }

            return ActionResultHelper.Success(result);
        }

        public async Task<ActionResult> UpdateBlogPost(BlogPostDTO blogPostDTO)
        {
            if (blogPostDTO.User == null)
            {
                return ActionResultHelper.Fail("User not found.");
            }

            var post = await _context.BlogPosts.Where(x => x.Id == blogPostDTO.Id).Include(x => x.User).FirstOrDefaultAsync();

            if (post == null)
            {
                return ActionResultHelper.Fail("That blog post was not found");
            }

            var mapped = _mapper.Map(blogPostDTO, post);
            if (mapped != null)
            {
                try
                {
                    _context.Attach(post.User);

                    _context.Entry(post).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return ActionResultHelper.Success();
                }
                catch (Exception e)
                {
                    return ActionResultHelper.Fail("The database update failed: " + e.Message);
                }
                
            }

            return ActionResultHelper.Fail("There was an internal server error.");

        }
    }
}
