// Controllers/DailyphilosophersController.cs
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
    public class DailyphilosophersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DailyphilosophersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Dailyphilosophers
[HttpGet]
public async Task<ActionResult<IEnumerable<Philosopher>>> GetDailyphilosophers()
{
    return await _context.Philosopher.ToListAsync();
}


        // GET: api/Dailyphilosophers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Philosopher>> GetDailyphilosopher(int id)
        {
            var philosopher = await _context.Philosopher.FindAsync(id);

            if (philosopher == null)
            {
                return NotFound();
            }

            return philosopher;
        }

        // POST: api/Dailyphilosophers
        [HttpPost]
        public async Task<ActionResult<Philosopher>> PostDailyphilosopher(Philosopher philosopher)
        {
            _context.Philosopher.Add(philosopher);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDailyphilosopher), new { id = philosopher.Id }, philosopher);
        }

        // PUT: api/Dailyphilosophers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailyphilosopher(int id, Philosopher philosopher)
        {
            if (id != philosopher.Id)
            {
                return BadRequest();
            }

            _context.Entry(philosopher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyphilosopherExists(id))
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

        // DELETE: api/Dailyphilosophers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyphilosopher(int id)
        {
            var philosopher = await _context.Philosopher.FindAsync(id);
            if (philosopher == null)
            {
                return NotFound();
            }

            _context.Philosopher.Remove(philosopher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DailyphilosopherExists(int id)
        {
            return _context.Philosopher.Any(e => e.Id == id);
        }
    }
}
