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

        public CatApiController(AnimalApiContext context, IMapper mapper) //Convention over Configuration: The program knows to omit Controller from the name
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/CatApi/FindByName
        [HttpGet("{name}"), Route("api/CatApi/FindByName")]
        public async Task<ActionResult<IEnumerable<AnimalCrossing.Models.Cat>>> FindByName(string name)
        {
            var cats = from m in _context.Cats.Include(cat => cat.Species) //if you want to include a species in the LINQ query you can use the include method to eager load it
                       select m;

            if (!String.IsNullOrEmpty(name))
            {
                cats = cats.Where(cat =>
                cat.Name.Contains(name));
            }

            return cats.ToList();  //instead of putting it after the LINQ statement, since that would create several calls to the database context
        }

        // GET: api/CatApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatVM>>> GetCat()
        {
            var cats = await _context.Cats.ToListAsync();
            return _mapper.Map<List<AnimalCrossing.Models.Cat>, List<CatVM>>(cats);
        }

        // GET: api/CatApi/5
        [HttpGet("{id:int}")] //If you have multiple GET methods you need to give one of them an attribute with a type like this
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
