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
    public class RotaKategoriTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public RotaKategoriTanimController(RoutesDbContext context)
        {
            _context = context;
        }

        // GET: api/RotaKategoriTanim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RotaKategoriTanim>>> GetRotaKategoriler()
        {
            return await _context.RotaKategoriler.ToListAsync();
        }

        // GET: api/RotaKategoriTanim/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RotaKategoriTanim>> GetRotaKategoriTanim(int id)
        {
            var rotaKategoriTanim = await _context.RotaKategoriler.FindAsync(id);

            if (rotaKategoriTanim == null)
            {
                return NotFound();
            }

            return rotaKategoriTanim;
        }

        // PUT: api/RotaKategoriTanim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRotaKategoriTanim(int id, RotaKategoriTanim rotaKategoriTanim)
        {
            if (id != rotaKategoriTanim.Id)
            {
                return BadRequest();
            }

            _context.Entry(rotaKategoriTanim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RotaKategoriTanimExists(id))
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

        // POST: api/RotaKategoriTanim
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RotaKategoriTanim>> PostRotaKategoriTanim(RotaKategoriTanim rotaKategoriTanim)
        {
            _context.RotaKategoriler.Add(rotaKategoriTanim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRotaKategoriTanim", new { id = rotaKategoriTanim.Id }, rotaKategoriTanim);
        }

        // DELETE: api/RotaKategoriTanim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRotaKategoriTanim(int id)
        {
            var rotaKategoriTanim = await _context.RotaKategoriler.FindAsync(id);
            if (rotaKategoriTanim == null)
            {
                return NotFound();
            }

            _context.RotaKategoriler.Remove(rotaKategoriTanim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RotaKategoriTanimExists(int id)
        {
            return _context.RotaKategoriler.Any(e => e.Id == id);
        }
    }
}
