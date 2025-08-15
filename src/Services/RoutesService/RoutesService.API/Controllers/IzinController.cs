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
    public class IzinController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public IzinController(RoutesDbContext context)
        {
            _context = context;
        }

        // GET: api/Izin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Izin>>> GetIzinler()
        {
            return await _context.Izinler.ToListAsync();
        }

        // GET: api/Izin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Izin>> GetIzin(int id)
        {
            var izin = await _context.Izinler.FindAsync(id);

            if (izin == null)
            {
                return NotFound();
            }

            return izin;
        }

        // PUT: api/Izin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIzin(int id, Izin izin)
        {
            if (id != izin.Id)
            {
                return BadRequest();
            }

            _context.Entry(izin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IzinExists(id))
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

        // POST: api/Izin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Izin>> PostIzin(Izin izin)
        {
            _context.Izinler.Add(izin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIzin", new { id = izin.Id }, izin);
        }

        // DELETE: api/Izin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIzin(int id)
        {
            var izin = await _context.Izinler.FindAsync(id);
            if (izin == null)
            {
                return NotFound();
            }

            _context.Izinler.Remove(izin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IzinExists(int id)
        {
            return _context.Izinler.Any(e => e.Id == id);
        }
    }
}
