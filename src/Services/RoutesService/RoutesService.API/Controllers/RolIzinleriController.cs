using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolIzinleriController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public RolIzinleriController(RoutesDbContext context)
        {
            _context = context;
        }

        // GET: api/RolIzinleri
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolIzinleri>>> GetRolIzinleri()
        {
            return await _context.RolIzinleri
                .Include(ri => ri.Rol) // Rol bilgilerini yükle
                .Include(ri => ri.Izin) // İzin bilgilerini yükle
                .ToListAsync();
        }

        // GET: api/RolIzinleri/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolIzinleri>> GetRolIzinleri(int id)
        {
            var rolIzinleri = await _context.RolIzinleri
                .Include(ri => ri.Rol)
                .Include(ri => ri.Izin)
                .FirstOrDefaultAsync(ri => ri.Id == id);

            if (rolIzinleri == null)
            {
                return NotFound();
            }

            return rolIzinleri;
        }

        // PUT: api/RolIzinleri/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolIzinleri(int id, RolIzinleri rolIzinleri)
        {
            if (id != rolIzinleri.Id)
            {
                return BadRequest();
            }

            _context.Entry(rolIzinleri).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolIzinleriExists(id))
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

        // POST: api/RolIzinleri
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RolIzinleri>> PostRolIzinleri(RolIzinleri rolIzinleri)
        {
            _context.RolIzinleri.Add(rolIzinleri);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRolIzinleri", new { id = rolIzinleri.Id }, rolIzinleri);
        }

        // DELETE: api/RolIzinleri/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolIzinleri(int id)
        {
            var rolIzinleri = await _context.RolIzinleri.FindAsync(id);
            if (rolIzinleri == null)
            {
                return NotFound();
            }

            _context.RolIzinleri.Remove(rolIzinleri);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolIzinleriExists(int id)
        {
            return _context.RolIzinleri.Any(e => e.Id == id);
        }
    }
}
