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
    public class RotaTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public RotaTanimController(RoutesDbContext context)
        {
            _context = context;
        }

        // GET: api/RotaTanim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RotaTanim>>> GetRotalar()
        {
            return await _context.Rotalar.ToListAsync();
        }

        // GET: api/RotaTanim/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RotaTanim>> GetRotaTanim(int id)
        {
            var rotaTanim = await _context.Rotalar.FindAsync(id);

            if (rotaTanim == null)
            {
                return NotFound();
            }

            return rotaTanim;
        }

        // PUT: api/RotaTanim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRotaTanim(int id, RotaTanim rotaTanim)
        {
            if (id != rotaTanim.Id)
            {
                return BadRequest();
            }

            _context.Entry(rotaTanim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RotaTanimExists(id))
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

        // POST: api/RotaTanim
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RotaTanim>> PostRotaTanim(RotaTanim rotaTanim)
        {
            _context.Rotalar.Add(rotaTanim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRotaTanim", new { id = rotaTanim.Id }, rotaTanim);
        }

        // DELETE: api/RotaTanim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRotaTanim(int id)
        {
            var rotaTanim = await _context.Rotalar.FindAsync(id);
            if (rotaTanim == null)
            {
                return NotFound();
            }

            _context.Rotalar.Remove(rotaTanim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RotaTanimExists(int id)
        {
            return _context.Rotalar.Any(e => e.Id == id);
        }
    }
}
