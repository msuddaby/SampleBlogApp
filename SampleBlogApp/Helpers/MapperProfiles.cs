using AutoMapper;
using SampleBlogApp.Data;

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

            
            //CreateMap<ImagePost, ImagePostDTO>();
        }
    }
}
