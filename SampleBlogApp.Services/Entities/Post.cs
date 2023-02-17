namespace SampleBlogApp.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUser? User { get; set; }
    }

    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public UserDTO? User { get; set; }
    }
}
