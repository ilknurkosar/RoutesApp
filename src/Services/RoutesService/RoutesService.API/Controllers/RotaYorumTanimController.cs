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
    public class RotaYorumTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public RotaYorumTanimController(RoutesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RotaYorumTanim>>> GetRotaYorumlar()
        {
            return await _context.RotaYorumlar.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RotaYorumTanim>> GetRotaYorumTanim(int id)
        {
            var rotaYorumTanim = await _context.RotaYorumlar.FindAsync(id);
            if (rotaYorumTanim == null) return NotFound();
            return rotaYorumTanim;
        }

        [HttpPost]
        public async Task<ActionResult<RotaYorumTanim>> PostRotaYorumTanim(RotaYorumTanim rotaYorumTanim)
        {
            _context.RotaYorumlar.Add(rotaYorumTanim);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRotaYorumTanim", new { id = rotaYorumTanim.Id }, rotaYorumTanim);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRotaYorumTanim(int id, RotaYorumTanim rotaYorumTanim)
        {
            if (id != rotaYorumTanim.Id) return BadRequest();

            _context.Entry(rotaYorumTanim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.RotaYorumlar.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRotaYorumTanim(int id)
        {
            var rotaYorumTanim = await _context.RotaYorumlar.FindAsync(id);
            if (rotaYorumTanim == null) return NotFound();

            _context.RotaYorumlar.Remove(rotaYorumTanim);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
