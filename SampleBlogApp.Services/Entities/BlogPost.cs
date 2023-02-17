namespace SampleBlogApp.Data
{
    public class BlogPost: Post
    {
        public string Content { get; set; }
    }

    public class BlogPostDTO : PostDTO
    {
        public string Content { get; set; }
    }
}
