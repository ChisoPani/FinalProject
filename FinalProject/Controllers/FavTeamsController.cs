﻿using System;
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
    public class FavTeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavTeamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FavTeams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavTeam>>> GetFavTeam()
        {
          if (_context.FavTeam == null)
          {
              return NotFound();
          }
            return await _context.FavTeam.ToListAsync();
        }

        // GET: api/FavTeams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavTeam>> GetFavTeam(int id)
        {
          if (_context.FavTeam == null)
          {
              return NotFound();
          }
            var favTeam = await _context.FavTeam.FindAsync(id);

            if (favTeam == null)
            {
                return NotFound();
            }

            return favTeam;
        }

        // PUT: api/FavTeams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavTeam(int id, FavTeam favTeam)
        {
            if (id != favTeam.Id)
            {
                return BadRequest();
            }

            _context.Entry(favTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavTeamExists(id))
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

        // POST: api/FavTeams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavTeam>> PostFavTeam(FavTeam favTeam)
        {
          if (_context.FavTeam == null)
          {
              return Problem("Entity set 'AppDbContext.FavTeam'  is null.");
          }
            _context.FavTeam.Add(favTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavTeam", new { id = favTeam.Id }, favTeam);
        }

        // DELETE: api/FavTeams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavTeam(int id)
        {
            if (_context.FavTeam == null)
            {
                return NotFound();
            }
            var favTeam = await _context.FavTeam.FindAsync(id);
            if (favTeam == null)
            {
                return NotFound();
            }

            _context.FavTeam.Remove(favTeam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavTeamExists(int id)
        {
            return (_context.FavTeam?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
