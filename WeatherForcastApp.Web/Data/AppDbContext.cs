using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using WeatherForcastApp.Web.Model;

namespace WeatherForcastApp.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            try
            {
                var databaseCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(databaseCreater != null) 
                {
                    if(!databaseCreater.CanConnect())
                        databaseCreater.Create();

                    if(!databaseCreater.HasTables())
                        databaseCreater.CreateTables();
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<Post> Post { get; set; }

        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var posts = new List<Post>
            {
                new Post { PostId = 1, Title = "Introduction to ASP.NET Core", Content = "Content for post 1" },
                new Post { PostId = 2, Title = "Deep Dive into EF Core", Content = "Content for post 2" },
                new Post { PostId = 3, Title = "Understanding Dependency Injection", Content = "Content for post 3" },
                new Post { PostId = 4, Title = "Exploring C# 9 Features", Content = "Content for post 4" },
                new Post { PostId = 5, Title = "Building RESTful APIs with ASP.NET Core", Content = "Content for post 5" }
            };

            modelBuilder.Entity<Post>().HasData(posts);

            // Seed Comments
            var comments = new List<Comment>
            {
                new Comment { CommentId = 1, PostId = 1, Author = "Jane Doe", Text = "Great introduction!" },
                new Comment { CommentId = 2, PostId = 1, Author = "John Doe", Text = "Looking forward to more!" },
                new Comment { CommentId = 3, PostId = 2, Author = "Alice Johnson", Text = "Very informative." },
                new Comment { CommentId = 4, PostId = 3, Author = "Bob Smith", Text = "Helped me understand DI better." },
                new Comment { CommentId = 5, PostId = 4, Author = "Charlie Brown", Text = "C# 9 is awesome!" }
            };

            modelBuilder.Entity<Comment>().HasData(comments);
        }
    }
}
