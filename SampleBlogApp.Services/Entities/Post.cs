using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "The title is required.")]
        [MaxLength(100, ErrorMessage = "The title cannot exceed 100 characters.")]
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public UserDTO? User { get; set; }
    }
}
