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
            return await _context.KullaniciRolleri
                .Include(kr => kr.Kullanici) // Kullanıcı bilgilerini yükle
                .Include(kr => kr.Rol) // Rol bilgilerini yükle
                .ToListAsync();
        }

        // GET: api/KullaniciRolleri/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KullaniciRolleri>> GetKullaniciRolleri(int id)
        {
            var kullaniciRolleri = await _context.KullaniciRolleri
                .Include(kr => kr.Kullanici)
                .Include(kr => kr.Rol)
                .FirstOrDefaultAsync(kr => kr.Id == id);

            if (kullaniciRolleri == null)
            {
                return NotFound();
            }

            return kullaniciRolleri;
        }

        // PUT: api/KullaniciRolleri/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKullaniciRolleri(int id, KullaniciRolleri kullaniciRolleri)
        {
            if (id != kullaniciRolleri.Id)
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
        [HttpPost]
        public async Task<ActionResult<KullaniciRolleri>> PostKullaniciRolleri(KullaniciRolleri kullaniciRolleri)
        {
            _context.KullaniciRolleri.Add(kullaniciRolleri);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKullaniciRolleri", new { id = kullaniciRolleri.Id }, kullaniciRolleri);
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
            return _context.KullaniciRolleri.Any(e => e.Id == id);
        }
    }
}
