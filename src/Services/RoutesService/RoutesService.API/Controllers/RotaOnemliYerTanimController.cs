using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RotaOnemliYerTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;
        private readonly IMapper _mapper;

        public RotaOnemliYerTanimController(RoutesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RotaOnemliYerTanimListDto>> GetRotaOnemliYerler()
             => await _context.RotaOnemliYerler
                 .AsNoTracking()
                 .ProjectTo<RotaOnemliYerTanimListDto>(_mapper.ConfigurationProvider)
                 .ToListAsync();

        // GET: api/RotaOnemliYerTanim/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RotaOnemliYerTanimDetailDto>> GetRotaOnemliYerTanim(int id)
        {
            var ent = await _context.RotaOnemliYerler.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();
            return _mapper.Map<RotaOnemliYerTanimDetailDto>(ent);
        }


        // POST: api/RotaOnemliYerTanim
        [HttpPost]
        public async Task<ActionResult<RotaOnemliYerTanimDetailDto>> PostRotaOnemliYerTanim(RotaOnemliYerTanimCreateDto dto)
        {
            var ent = _mapper.Map<RotaOnemliYerTanim>(dto);

            if (!string.IsNullOrWhiteSpace(dto.GeometryWkt))
            {
                var reader = new WKTReader();
                ent.Geometry = reader.Read(dto.GeometryWkt);
            }

            _context.RotaOnemliYerler.Add(ent);
            await _context.SaveChangesAsync();

            var detail = _mapper.Map<RotaOnemliYerTanimDetailDto>(ent);
            return CreatedAtAction(nameof(GetRotaOnemliYerTanim), new { id = ent.Id }, detail);
        }


        // PUT: api/RotaOnemliYerTanim/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutRotaOnemliYerTanim(int id, RotaOnemliYerTanimUpdateDto dto)
        {
            var ent = await _context.RotaOnemliYerler.FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();

            _mapper.Map(dto, ent);

            if (dto.GeometryWkt != null)
            {
                if (string.IsNullOrWhiteSpace(dto.GeometryWkt))
                {
                    ent.Geometry = null;
                }
                else
                {
                    var reader = new WKTReader();
                    ent.Geometry = reader.Read(dto.GeometryWkt);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/RotaOnemliYerTanim/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRotaOnemliYerTanim(int id)
        {
            var ent = await _context.RotaOnemliYerler.FindAsync(id);
            if (ent is null) return NotFound();

            _context.RotaOnemliYerler.Remove(ent);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
