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
    public class RotaResimTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public RotaResimTanimController(RoutesDbContext context)
        {
            _context = context;
        }

        // GET: api/RotaResimTanim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RotaResimTanim>>> GetRotaResimler()
        {
            return await _context.RotaResimler.ToListAsync();
        }

        // GET: api/RotaResimTanim/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RotaResimTanim>> GetRotaResimTanim(int id)
        {
            var rotaResimTanim = await _context.RotaResimler.FindAsync(id);

            if (rotaResimTanim == null)
            {
                return NotFound();
            }

            return rotaResimTanim;
        }

        // PUT: api/RotaResimTanim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRotaResimTanim(int id, RotaResimTanim rotaResimTanim)
        {
            if (id != rotaResimTanim.Id)
            {
                return BadRequest();
            }

            _context.Entry(rotaResimTanim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RotaResimTanimExists(id))
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

        // POST: api/RotaResimTanim
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RotaResimTanim>> PostRotaResimTanim(RotaResimTanim rotaResimTanim)
        {
            _context.RotaResimler.Add(rotaResimTanim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRotaResimTanim", new { id = rotaResimTanim.Id }, rotaResimTanim);
        }

        // DELETE: api/RotaResimTanim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRotaResimTanim(int id)
        {
            var rotaResimTanim = await _context.RotaResimler.FindAsync(id);
            if (rotaResimTanim == null)
            {
                return NotFound();
            }

            _context.RotaResimler.Remove(rotaResimTanim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RotaResimTanimExists(int id)
        {
            return _context.RotaResimler.Any(e => e.Id == id);
        }
    }
}
