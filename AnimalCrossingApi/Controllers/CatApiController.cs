using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalCrossing.Models;
using AnimalCrossingApi.Models;
using AnimalCrossing.Models.ViewModels;
using AutoMapper;

namespace AnimalCrossingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatApiController : ControllerBase
    {
        private readonly AnimalApiContext _context;
        private readonly IMapper _mapper;

        public CatApiController(AnimalApiContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/CatApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatVM>>> GetCat()
        {
            var cats = await _context.Cats.ToListAsync();
            return _mapper.Map<List<AnimalCrossing.Models.Cat>, List<CatVM>>(cats);
        }

        // GET: api/CatApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalCrossing.Models.Cat>> GetCat(int id)
        {
            var cat = await _context.Cats.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }

        // PUT: api/CatApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCat(int id, AnimalCrossing.Models.Cat cat)
        {
            if (id != cat.CatId)
            {
                return BadRequest();
            }

            _context.Entry(cat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatExists(id))
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

        // POST: api/CatApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AnimalCrossing.Models.Cat>> PostCat(AnimalCrossing.Models.Cat cat)
        {
            _context.Cats.Add(cat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCat", new { id = cat.CatId }, cat);
        }

        // DELETE: api/CatApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnimalCrossing.Models.Cat>> DeleteCat(int id)
        {
            var cat = await _context.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();

            return cat;
        }

        private bool CatExists(int id)
        {
            return _context.Cats.Any(e => e.CatId == id);
        }
    }
}
