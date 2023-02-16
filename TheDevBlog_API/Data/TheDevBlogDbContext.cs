using Microsoft.EntityFrameworkCore;
using TheDevBlog_API.Models.Entites;

namespace TheDevBlog_API.Data
{
    public class TheDevBlogDbContext : DbContext
    {
        public TheDevBlogDbContext(DbContextOptions options) : base(options)
        {

        }

        //DbSet
        public DbSet<Post> Posts { get; set; }   
    }
}
