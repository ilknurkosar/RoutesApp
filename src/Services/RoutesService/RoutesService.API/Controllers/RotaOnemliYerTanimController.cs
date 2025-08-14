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
    public class RotaOnemliYerTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;

        public RotaOnemliYerTanimController(RoutesDbContext context)
        {
            _context = context;
        }

        // GET: api/RotaOnemliYerTanim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RotaOnemliYerTanim>>> GetRotaOnemliYerler()
        {
            return await _context.RotaOnemliYerler.ToListAsync();
        }

        // GET: api/RotaOnemliYerTanim/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RotaOnemliYerTanim>> GetRotaOnemliYerTanim(int id)
        {
            var rotaOnemliYerTanim = await _context.RotaOnemliYerler.FindAsync(id);

            if (rotaOnemliYerTanim == null)
            {
                return NotFound();
            }

            return rotaOnemliYerTanim;
        }

        // PUT: api/RotaOnemliYerTanim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRotaOnemliYerTanim(int id, RotaOnemliYerTanim rotaOnemliYerTanim)
        {
            if (id != rotaOnemliYerTanim.Id)
            {
                return BadRequest();
            }

            _context.Entry(rotaOnemliYerTanim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RotaOnemliYerTanimExists(id))
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

        // POST: api/RotaOnemliYerTanim
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RotaOnemliYerTanim>> PostRotaOnemliYerTanim(RotaOnemliYerTanim rotaOnemliYerTanim)
        {
            _context.RotaOnemliYerler.Add(rotaOnemliYerTanim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRotaOnemliYerTanim", new { id = rotaOnemliYerTanim.Id }, rotaOnemliYerTanim);
        }

        // DELETE: api/RotaOnemliYerTanim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRotaOnemliYerTanim(int id)
        {
            var rotaOnemliYerTanim = await _context.RotaOnemliYerler.FindAsync(id);
            if (rotaOnemliYerTanim == null)
            {
                return NotFound();
            }

            _context.RotaOnemliYerler.Remove(rotaOnemliYerTanim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RotaOnemliYerTanimExists(int id)
        {
            return _context.RotaOnemliYerler.Any(e => e.Id == id);
        }
    }
}
