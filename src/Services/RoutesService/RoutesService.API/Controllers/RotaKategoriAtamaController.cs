using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RotaKategoriAtamaController : ControllerBase
{
    private readonly RoutesDbContext _db;
    private readonly IMapper _mapper;

    public RotaKategoriAtamaController(RoutesDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    // GET: api/RotaKategoriAtama
    [HttpGet]
    public async Task<IEnumerable<RotaKategoriAtamaListDto>> Get()
        => await _db.RotaKategoriAtama
            .AsNoTracking()
            .Include(rk => rk.Rota)
            .Include(rk => rk.Kategori)
            .ProjectTo<RotaKategoriAtamaListDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

    // GET: api/RotaKategoriAtama/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RotaKategoriAtamaDetailDto>> Get(int id)
    {
        var ent = await _db.RotaKategoriAtama
            .AsNoTracking()
            .Include(rk => rk.Rota)
            .Include(rk => rk.Kategori)
            .FirstOrDefaultAsync(rk => rk.Id == id);

        if (ent is null) return NotFound();
        return _mapper.Map<RotaKategoriAtamaDetailDto>(ent);
    }

    // POST: api/RotaKategoriAtama
    [HttpPost]
    public async Task<ActionResult<RotaKategoriAtamaDetailDto>> Create(RotaKategoriAtamaCreateDto dto)
    {
        var ent = _mapper.Map<RotaKategoriAtama>(dto);

        _db.RotaKategoriAtama.Add(ent);
        await _db.SaveChangesAsync();

        var detail = await _db.RotaKategoriAtama
            .Include(rk => rk.Rota)
            .Include(rk => rk.Kategori)
            .FirstAsync(rk => rk.Id == ent.Id);

        return CreatedAtAction(nameof(Get), new { id = ent.Id }, _mapper.Map<RotaKategoriAtamaDetailDto>(detail));
    }

    // PUT: api/RotaKategoriAtama/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, RotaKategoriAtamaUpdateDto dto)
    {
        var ent = await _db.RotaKategoriAtama.FindAsync(id);
        if (ent is null) return NotFound();

        _mapper.Map(dto, ent);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/RotaKategoriAtama/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ent = await _db.RotaKategoriAtama.FindAsync(id);
        if (ent is null) return NotFound();

        _db.RotaKategoriAtama.Remove(ent);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}