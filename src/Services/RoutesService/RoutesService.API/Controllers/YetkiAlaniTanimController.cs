using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class YetkiAlaniTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public YetkiAlaniTanimController(RoutesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<YetkiAlaniTanim>>> GetYetkiAlanlari()
        {
            return await _context.YetkiAlanlari.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<YetkiAlaniTanim>> GetYetkiAlaniTanim(int id)
        {
            var yetkiAlaniTanim = await _context.YetkiAlanlari.FindAsync(id);
            if (yetkiAlaniTanim == null) return NotFound();
            return yetkiAlaniTanim;
        }

        // PUT: api/YetkiAlaniTanim/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYetkiAlaniTanim(int id, YetkiAlaniTanim yetkiAlaniTanim)
        {
            if (id != yetkiAlaniTanim.Id) return BadRequest();

            _context.Entry(yetkiAlaniTanim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.YetkiAlanlari.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/YetkiAlaniTanim
        [HttpPost]
        public async Task<ActionResult<YetkiAlaniTanim>> PostYetkiAlaniTanim(YetkiAlaniTanim yetkiAlaniTanim)
        {
            _context.YetkiAlanlari.Add(yetkiAlaniTanim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYetkiAlaniTanim", new { id = yetkiAlaniTanim.Id }, yetkiAlaniTanim);
        }

        // DELETE: api/YetkiAlaniTanim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYetkiAlaniTanim(int id)
        {
            var yetkiAlaniTanim = await _context.YetkiAlanlari.FindAsync(id);
            if (yetkiAlaniTanim == null) return NotFound();

            _context.YetkiAlanlari.Remove(yetkiAlaniTanim);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
