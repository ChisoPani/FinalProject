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
        public async Task<ActionResult<IEnumerable<FavFood>>> GetFavFood()
        {
          if (_context.FavFood == null)
          {
              return NotFound();
          }
            return await _context.FavFood.ToListAsync();
        }

        // GET: api/FavFoods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavFood>> GetFavFood(int id)
        {
          if (_context.FavFood == null)
          {
              return NotFound();
          }
            var favFood = await _context.FavFood.FindAsync(id);

            if (favFood == null)
            {
                return NotFound();
            }

            return favFood;
        }

        // PUT: api/FavFoods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavFood(int id, FavFood favFood)
        {
            if (id != favFood.Id)
            {
                return BadRequest();
            }

            _context.Entry(favFood).State = EntityState.Modified;

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
        public async Task<ActionResult<FavFood>> PostFavFood(FavFood favFood)
        {
          if (_context.FavFood == null)
          {
              return Problem("Entity set 'AppDbContext.FavFood'  is null.");
          }
            _context.FavFood.Add(favFood);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavFood", new { id = favFood.Id }, favFood);
        }

        // DELETE: api/FavFoods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavFood(int id)
        {
            if (_context.FavFood == null)
            {
                return NotFound();
            }
            var favFood = await _context.FavFood.FindAsync(id);
            if (favFood == null)
            {
                return NotFound();
            }

            _context.FavFood.Remove(favFood);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavFoodExists(int id)
        {
            return (_context.FavFood?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
