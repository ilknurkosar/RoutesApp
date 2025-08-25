using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;
using RoutesService.API.DTOs;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaKategoriTanimController : ControllerBase
    {
        private readonly RoutesDbContext _context;
        private readonly IMapper _mapper;

        public RotaKategoriTanimController(RoutesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RotaKategoriTanimListDto>> GetRotaKategoriler()
           => await _context.RotaKategoriler
               .AsNoTracking()
               .ProjectTo<RotaKategoriTanimListDto>(_mapper.ConfigurationProvider)
               .ToListAsync();


        // GET: api/RotaKategoriTanim/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RotaKategoriTanimDetailDto>> GetRotaKategoriTanim(int id)
        {
            var ent = await _context.RotaKategoriler.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();
            return _mapper.Map<RotaKategoriTanimDetailDto>(ent);
        }


        // POST: api/RotaKategoriTanim
        [HttpPost]
        public async Task<ActionResult<RotaKategoriTanimDetailDto>> PostRotaKategoriTanim(RotaKategoriTanimCreateDto dto)
        {
            var ent = _mapper.Map<RotaKategoriTanim>(dto);
            _context.RotaKategoriler.Add(ent);
            await _context.SaveChangesAsync();

            var detail = _mapper.Map<RotaKategoriTanimDetailDto>(ent);
            return CreatedAtAction(nameof(GetRotaKategoriTanim), new { id = ent.Id }, detail);
        }


        // PUT: api/RotaKategoriTanim/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutRotaKategoriTanim(int id, RotaKategoriTanimUpdateDto dto)
        {
            var ent = await _context.RotaKategoriler.FirstOrDefaultAsync(x => x.Id == id);
            if (ent is null) return NotFound();

            _mapper.Map(dto, ent);
            await _context.SaveChangesAsync();
            return NoContent();
        }



        // DELETE: api/RotaKategoriTanim/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRotaKategoriTanim(int id)
        {
            var ent = await _context.RotaKategoriler.FindAsync(id);
            if (ent is null) return NotFound();

            _context.RotaKategoriler.Remove(ent);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}