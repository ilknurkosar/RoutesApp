using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class RotaTanimController : ControllerBase
{
    private readonly RoutesDbContext _db;
    private readonly IMapper _mapper;

    public RotaTanimController(RoutesDbContext db, IMapper mapper)
    {
        private readonly RoutesDbContext _db;
        private readonly IMapper _mapper;

        public RotaTanimController(RoutesDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        // GET: api/RotaTanim
        [HttpGet]
        public async Task<IEnumerable<RotaTanimListDto>> Get()
            => await _db.Rotalar.AsNoTracking()
                .ProjectTo<RotaTanimListDto>(_mapper.ConfigurationProvider) //projectTo kullandık çünkü Geometry yok, EF sorguyu direkt SQL’e çevirir
                .ToListAsync();


        // GET: api/RotaTanim/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RotaTanimDetailDto>> Get(int id)
        {
            var ent = await _db.Rotalar.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();
            return _mapper.Map<RotaTanimDetailDto>(ent);
        }

        // POST: api/RotaTanim
        [HttpPost]
        public async Task<ActionResult<RotaTanimDetailDto>> Create(RotaTanimCreateDto dto)
        {
            var ent = _mapper.Map<RotaTanim>(dto);

            // Geometry: WKT -> Geometry (varsa)
            if (!string.IsNullOrWhiteSpace(dto.GeometryWkt))
            {
                var reader = new WKTReader();
                ent.Geometry = reader.Read(dto.GeometryWkt);
            }

            _db.Rotalar.Add(ent);
            await _db.SaveChangesAsync();

            var detail = _mapper.Map<RotaTanimDetailDto>(ent);
            return CreatedAtAction(nameof(Get), new { id = ent.Id }, detail);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, RotaTanimUpdateDto dto)
        {
            var ent = await _db.Rotalar.FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();

            _mapper.Map(dto, ent);

            // Geometry: WKT -> Geometry (null/empty ise dokunma)
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

            await _db.SaveChangesAsync();
            return NoContent();
        }


        // DELETE: api/RotaTanim/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ent = await _db.Rotalar.FindAsync(id);
            if (ent is null) return NotFound();

            _db.Rotalar.Remove(ent);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
