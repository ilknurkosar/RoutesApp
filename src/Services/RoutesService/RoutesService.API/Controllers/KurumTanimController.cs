using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KurumTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public KurumTanimController(RoutesDbContext context)
        {
            _context = context;
        }

        // GET: api/KurumTanim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KurumTanim>>> GetKurumlar()
        {
            return await _context.Kurumlar.ToListAsync();
        }

        // GET: api/KurumTanim/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KurumTanim>> GetKurumTanim(int id)
        {
            var kurumTanim = await _context.Kurumlar.FindAsync(id);

            if (kurumTanim == null)
            {
                return NotFound();
            }

            return kurumTanim;
        }

        // PUT: api/KurumTanim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKurumTanim(int id, KurumTanim kurumTanim)
        {
            if (id != kurumTanim.Id)
            {
                return BadRequest();
            }

            _context.Entry(kurumTanim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KurumTanimExists(id))
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

        // POST: api/KurumTanim
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KurumTanim>> PostKurumTanim(KurumTanim kurumTanim)
        {
            _context.Kurumlar.Add(kurumTanim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKurumTanim", new { id = kurumTanim.Id }, kurumTanim);
        }

        // DELETE: api/KurumTanim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKurumTanim(int id)
        {
            var kurumTanim = await _context.Kurumlar.FindAsync(id);
            if (kurumTanim == null)
            {
                return NotFound();
            }

            _context.Kurumlar.Remove(kurumTanim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KurumTanimExists(int id)
        {
            return _context.Kurumlar.Any(e => e.Id == id);
        }
    }
}
