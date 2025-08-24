using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RolController : ControllerBase
{
    private readonly RoutesDbContext _db;
    private readonly IMapper _mapper;
    public RolController(RoutesDbContext db, IMapper mapper) { _db = db; _mapper = mapper; }

    [HttpGet]
    public async Task<IEnumerable<RolListDto>> Get()
 => await _db.Roller.AsNoTracking()
           .ProjectTo<RolListDto>(_mapper.ConfigurationProvider)
           .ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RolDetailDto>> Get(int id)
    {
        var e = await _db.Roller.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (e is null) return NotFound();
        return _mapper.Map<RolDetailDto>(e);
    }

    [HttpPost]
    public async Task<ActionResult<RolDetailDto>> Create(RolCreateDto dto)
    {
        var ent = _mapper.Map<Rol>(dto);
        _db.Roller.Add(ent);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = ent.Id }, _mapper.Map<RolDetailDto>(ent));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, RolUpdateDto dto)
    {
        var ent = await _db.Roller.FirstOrDefaultAsync(x => x.Id == id);
        if (ent is null) return NotFound();
        _mapper.Map(dto, ent);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ent = await _db.Roller.FindAsync(id);
        if (ent is null) return NotFound();
        _db.Roller.Remove(ent);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}