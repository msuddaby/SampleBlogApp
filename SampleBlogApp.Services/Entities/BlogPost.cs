using System.ComponentModel.DataAnnotations;

namespace SampleBlogApp.Data
{
    public class BlogPost: Post
    {
        
        public string Content { get; set; }
    }

    public class BlogPostDTO : PostDTO
    {
        [Required]
        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                return Content.Length > 100 ? Content.Substring(0, 500) + "..." : Content;
            }
        }
    }
}
