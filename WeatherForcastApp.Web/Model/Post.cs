using System.ComponentModel.DataAnnotations;

namespace WeatherForcastApp.Web.Model
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        // navigation property
        public List<Comment> Comments { get; set; }
    }
}
