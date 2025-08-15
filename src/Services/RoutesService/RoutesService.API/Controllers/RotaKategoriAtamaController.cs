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
    public class RotaKategoriAtamaController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public RotaKategoriAtamaController(RoutesDbContext context)
        {
            _context = context;
        }

        // GET: api/RotaKategoriAtama
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RotaKategoriAtama>>> GetRotaKategoriAtama()
        {
            return await _context.RotaKategoriAtama.ToListAsync();
        }

        // GET: api/RotaKategoriAtama/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RotaKategoriAtama>> GetRotaKategoriAtama(int id)
        {
            var rotaKategoriAtama = await _context.RotaKategoriAtama.FindAsync(id);

            if (rotaKategoriAtama == null)
            {
                return NotFound();
            }

            return rotaKategoriAtama;
        }

        // PUT: api/RotaKategoriAtama/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRotaKategoriAtama(int id, RotaKategoriAtama rotaKategoriAtama)
        {
            if (id != rotaKategoriAtama.RotaId)
            {
                return BadRequest();
            }

            _context.Entry(rotaKategoriAtama).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RotaKategoriAtamaExists(id))
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

        // POST: api/RotaKategoriAtama
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RotaKategoriAtama>> PostRotaKategoriAtama(RotaKategoriAtama rotaKategoriAtama)
        {
            _context.RotaKategoriAtama.Add(rotaKategoriAtama);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RotaKategoriAtamaExists(rotaKategoriAtama.RotaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRotaKategoriAtama", new { id = rotaKategoriAtama.RotaId }, rotaKategoriAtama);
        }

        // DELETE: api/RotaKategoriAtama/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRotaKategoriAtama(int id)
        {
            var rotaKategoriAtama = await _context.RotaKategoriAtama.FindAsync(id);
            if (rotaKategoriAtama == null)
            {
                return NotFound();
            }

            _context.RotaKategoriAtama.Remove(rotaKategoriAtama);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RotaKategoriAtamaExists(int id)
        {
            return _context.RotaKategoriAtama.Any(e => e.RotaId == id);
        }
    }
}
