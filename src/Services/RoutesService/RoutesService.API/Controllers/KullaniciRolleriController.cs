using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciRolleriController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public KullaniciRolleriController(RoutesDbContext context)
        {
            _context = context;
        }

        // GET: api/KullaniciRolleri
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KullaniciRolleri>>> GetKullaniciRolleri()
        {
            return await _context.KullaniciRolleri.ToListAsync();
        }

        // GET: api/KullaniciRolleri/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KullaniciRolleri>> GetKullaniciRolleri(int id)
        {
            var kullaniciRolleri = await _context.KullaniciRolleri.FindAsync(id);

            if (kullaniciRolleri == null)
            {
                return NotFound();
            }

            return kullaniciRolleri;
        }

        // PUT: api/KullaniciRolleri/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKullaniciRolleri(int id, KullaniciRolleri kullaniciRolleri)
        {
            if (id != kullaniciRolleri.KullaniciId)
            {
                return BadRequest();
            }

            _context.Entry(kullaniciRolleri).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KullaniciRolleriExists(id))
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

        // POST: api/KullaniciRolleri
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KullaniciRolleri>> PostKullaniciRolleri(KullaniciRolleri kullaniciRolleri)
        {
            _context.KullaniciRolleri.Add(kullaniciRolleri);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KullaniciRolleriExists(kullaniciRolleri.KullaniciId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKullaniciRolleri", new { id = kullaniciRolleri.KullaniciId }, kullaniciRolleri);
        }

        // DELETE: api/KullaniciRolleri/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullaniciRolleri(int id)
        {
            var kullaniciRolleri = await _context.KullaniciRolleri.FindAsync(id);
            if (kullaniciRolleri == null)
            {
                return NotFound();
            }

            _context.KullaniciRolleri.Remove(kullaniciRolleri);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KullaniciRolleriExists(int id)
        {
            return _context.KullaniciRolleri.Any(e => e.KullaniciId == id);
        }
    }
}
