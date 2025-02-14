using System.ComponentModel.DataAnnotations;

namespace WeatherForcastApp.Web.Model
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string Author { get; set; }

        public string Text { get; set; }

        public int PostId { get; set; }

        // navigation property
        //public Post Post { get; set; }
    }
}
