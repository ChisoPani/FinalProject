using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavFoodsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavFoodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FavFoods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavFoods>>> GetFavFood()
        {
          if (_context.FavFoods == null)
          {
              return NotFound();
          }
            return await _context.FavFoods.ToListAsync();
        }

        // GET: api/FavFoods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FavFoods>>> GetFavFoods(int? id)
        {
            if (id == null || id == 0)
            {
                return await _context.FavFoods.Take(5).ToListAsync();
            }
            else
            {
                var FavFoods = await _context.FavFoods.FindAsync(id);
                if (FavFoods == null)
                {
                    return NotFound();
                }
                return new List<FavFoods> { FavFoods };
            }
        }

        // PUT: api/FavFoods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavFood(int id, FavFoods FavFoods)
        {
            if (id != FavFoods.Id)
            {
                return BadRequest();
            }

            _context.Entry(FavFoods).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavFoodExists(id))
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

        // POST: api/FavFoods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavFoods>> PostFavFood(FavFoods FavFoods)
        {
          if (_context.FavFoods == null)
          {
              return Problem("Entity set 'AppDbContext.FavFoods'  is null.");
          }
            _context.FavFoods.Add(FavFoods);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavFood", new { id = FavFoods.Id }, FavFoods);
        }

        // DELETE: api/FavFoods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavFood(int id)
        {
            if (_context.FavFoods == null)
            {
                return NotFound();
            }
            var FavFoods = await _context.FavFoods.FindAsync(id);
            if (FavFoods == null)
            {
                return NotFound();
            }

            _context.FavFoods.Remove(FavFoods);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavFoodExists(int id)
        {
            return (_context.FavFoods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
