using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IzinController : ControllerBase
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public IzinController(RoutesDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // GET: api/Izin
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<IzinListDto>> Get()
        {
            return await _db.Izinler
                            .AsNoTracking()
                            .ProjectTo<IzinListDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        // GET: api/Izin/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<IzinDetailDto>> Get(int id)
        {
            var izin = await _db.Izinler.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (izin == null) return NotFound();
            return _mapper.Map<IzinDetailDto>(izin);
        }

        // POST: api/Izin
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IzinDetailDto>> Create(IzinCreateDto dto)
        {
            var izin = _mapper.Map<Izin>(dto);
            _db.Izinler.Add(izin);
            await _db.SaveChangesAsync();

            var izinDetail = _mapper.Map<IzinDetailDto>(izin);
            return CreatedAtAction(nameof(Get), new { id = izin.Id }, izinDetail);
        }

        // PUT: api/Izin/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, IzinUpdateDto dto)
        {
            var izin = await _db.Izinler.FirstOrDefaultAsync(x => x.Id == id);
            if (izin == null) return NotFound();

            _mapper.Map(dto, izin);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Izin/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var izin = await _db.Izinler.FindAsync(id);
            if (izin == null) return NotFound();

            _db.Izinler.Remove(izin);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
