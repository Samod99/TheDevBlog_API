using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheDevBlog_API.Data;
using TheDevBlog_API.Models.DTO;
using TheDevBlog_API.Models.Entites;

namespace TheDevBlog_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly TheDevBlogDbContext dbContext;

        public PostsController(TheDevBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await dbContext.Posts.ToListAsync();

            return Ok(posts);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post != null)
            {
                return Ok(post);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {
            // Convert DTO to Entity

            var post = new Post()
            {
                Title = addPostRequest.Title,
                Content = addPostRequest.Content,
                Author = addPostRequest.Author,
                FeaturedImageUrl = addPostRequest.FeaturedImageUrl,
                PublishDate = addPostRequest.PublishDate,
                UpdateDate = addPostRequest.UpdateDate,
                Summary = addPostRequest.Summary,
                UrlHandler = addPostRequest.UrlHandler,
                Visible = addPostRequest.Visible
            };

            post.Id = Guid.NewGuid();
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest updatePostRequest)
        {
            // Check if exists

            var existsPost = await dbContext.Posts.FindAsync(id);

            if (existsPost != null)
            {
                existsPost.Title = updatePostRequest.Title;
                existsPost.Content = updatePostRequest.Content;
                existsPost.Author = updatePostRequest.Author;
                existsPost.FeaturedImageUrl = updatePostRequest.FeaturedImageUrl;
                existsPost.PublishDate = updatePostRequest.PublishDate;
                existsPost.UpdateDate = updatePostRequest.UpdateDate;
                existsPost.Summary = updatePostRequest.Summary;
                existsPost.UrlHandler = updatePostRequest.UrlHandler;
                existsPost.Visible = updatePostRequest.Visible;

                await dbContext.SaveChangesAsync();

                return Ok(existsPost);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var existsPost = await dbContext.Posts.FindAsync(id);

            if (existsPost != null)
            {
                dbContext.Remove(existsPost);
                await dbContext.SaveChangesAsync();

                return Ok(existsPost);
            }

            return NotFound();
        }
    }
}
