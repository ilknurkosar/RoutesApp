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
    public class RotaYorumTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public RotaYorumTanimController(RoutesDbContext context)
        {
            _context = context;
        }

        // GET: api/RotaYorumTanim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RotaYorumTanim>>> GetRotaYorumlar()
        {
            return await _context.RotaYorumlar.ToListAsync();
        }

        // GET: api/RotaYorumTanim/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RotaYorumTanim>> GetRotaYorumTanim(int id)
        {
            var rotaYorumTanim = await _context.RotaYorumlar.FindAsync(id);

            if (rotaYorumTanim == null)
            {
                return NotFound();
            }

            return rotaYorumTanim;
        }

        // PUT: api/RotaYorumTanim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRotaYorumTanim(int id, RotaYorumTanim rotaYorumTanim)
        {
            if (id != rotaYorumTanim.Id)
            {
                return BadRequest();
            }

            _context.Entry(rotaYorumTanim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RotaYorumTanimExists(id))
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

        // POST: api/RotaYorumTanim
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RotaYorumTanim>> PostRotaYorumTanim(RotaYorumTanim rotaYorumTanim)
        {
            _context.RotaYorumlar.Add(rotaYorumTanim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRotaYorumTanim", new { id = rotaYorumTanim.Id }, rotaYorumTanim);
        }

        // DELETE: api/RotaYorumTanim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRotaYorumTanim(int id)
        {
            var rotaYorumTanim = await _context.RotaYorumlar.FindAsync(id);
            if (rotaYorumTanim == null)
            {
                return NotFound();
            }

            _context.RotaYorumlar.Remove(rotaYorumTanim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RotaYorumTanimExists(int id)
        {
            return _context.RotaYorumlar.Any(e => e.Id == id);
        }
    }
}
