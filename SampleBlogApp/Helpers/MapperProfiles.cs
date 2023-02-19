using AutoMapper;
using SampleBlogApp.Data;
using SampleBlogApp.Services.Entities;

namespace SampleBlogApp.Helpers
{
    public class MapperProfiles: Profile
    {
        public MapperProfiles()
        {
            CreateMap<BlogPost, BlogPostDTO>();
            CreateMap<BlogPostDTO, BlogPost>();
            
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<UserDTO, ApplicationUser>();

            CreateMap<ApplicationRole, RoleDTO>();
            CreateMap<RoleDTO, ApplicationRole>();

            
            //CreateMap<ImagePost, ImagePostDTO>();
        }
    }
}
