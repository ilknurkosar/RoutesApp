using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

[Route("api/[controller]")]
[ApiController]
public class RotaResimTanimController : ControllerBase
{
    private readonly RoutesDbContext _db;
    private readonly IMapper _mapper;

    public RotaResimTanimController(RoutesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<RotaResimTanimListDto>> Get()
    {
        return await _db.RotaResimler
                        .AsNoTracking()
                        .ProjectTo<RotaResimTanimListDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RotaResimTanimDetailDto>> Get(int id)
    {
        var ent = await _db.RotaResimler.FindAsync(id);
        if (ent == null) return NotFound();
        return _mapper.Map<RotaResimTanimDetailDto>(ent);
    }

    [HttpPost]
    public async Task<ActionResult<RotaResimTanimDetailDto>> Create(RotaResimTanimCreateDto dto)
    {
        var ent = _mapper.Map<RotaResimTanim>(dto);
        _db.RotaResimler.Add(ent);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = ent.Id }, _mapper.Map<RotaResimTanimDetailDto>(ent));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, RotaResimTanimUpdateDto dto)
    {
        var ent = await _db.RotaResimler.FindAsync(id);
        if (ent == null) return NotFound();

        _mapper.Map(dto, ent);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ent = await _db.RotaResimler.FindAsync(id);
        if (ent == null) return NotFound();

        _db.RotaResimler.Remove(ent);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
