using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        _db = db;
        _mapper = mapper;
    }

    // 🔹 Listeleme herkese açık
    [HttpGet]
    [AllowAnonymous]
    public async Task<IEnumerable<RotaTanimListDto>> Get()
    {
        return await _db.Rotalar
                        .AsNoTracking()
                        .ProjectTo<RotaTanimListDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<RotaTanimDetailDto>> Get(int id)
    {
        var ent = await _db.Rotalar.FindAsync(id);
        if (ent == null) return NotFound();
        return _mapper.Map<RotaTanimDetailDto>(ent);
    }

    // 🔹 Ekleme sadece Admin
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RotaTanimDetailDto>> Create(RotaTanimCreateDto dto)
    {
        var ent = _mapper.Map<RotaTanim>(dto);
        _db.Rotalar.Add(ent);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = ent.Id }, _mapper.Map<RotaTanimDetailDto>(ent));
    }

    // 🔹 Güncelleme sadece Admin
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, RotaTanimUpdateDto dto)
    {
        var ent = await _db.Rotalar.FindAsync(id);
        if (ent == null) return NotFound();

        _mapper.Map(dto, ent);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // 🔹 Silme sadece Admin
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var ent = await _db.Rotalar.FindAsync(id);
        if (ent == null) return NotFound();

        _db.Rotalar.Remove(ent);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
