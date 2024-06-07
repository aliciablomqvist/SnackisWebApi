// Controllers/DiscussionsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackisWebbAPI.Data;
using SnackisWebbAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisWebbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiscussionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Retrieve all discussions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetDiscussions()
        {
            return await _context.Post.Include(d => d.User).ToListAsync();
        }

        // GET: Retrieve a specific discussion by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetDiscussion(int id)
        {
            var post = await _context.Post.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // POST: Add a new discussion
        [HttpPost]
        public async Task<ActionResult<Post>> PostDiscussion(Post post)
        {
            post.Date = DateTime.UtcNow;
            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDiscussion), new { id = post.Id }, post);
        }

        // PUT: Update an existing discussion
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscussion(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscussionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: Remove a discussion by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscussion(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Check if a discussion exists by ID
        private bool DiscussionExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
