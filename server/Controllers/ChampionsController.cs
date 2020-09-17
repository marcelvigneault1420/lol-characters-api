using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Database.Entities;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionsController : ControllerBase
    {
        private readonly LolContext _context;

        public ChampionsController(LolContext context)
        {
            _context = context;
        }

        // GET: api/Champions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Champion>>> GetChampions()
        {
            return await _context.Champions.ToListAsync();
        }

        // GET: api/Champions/5
        [HttpGet("{id}")]
        [HttpGet("get/{id}")]
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Champion>> GetChampion(Guid id)
        {
            var champion = await _context.Champions.FindAsync(id);

            if (champion == null)
            {
                return NotFound();
            }

            return champion;
        }

        // PUT: api/Champions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChampion(Guid id, Champion champion)
        {
            if (id != champion.Id)
            {
                return BadRequest();
            }

            _context.Entry(champion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChampionExists(id))
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

        [HttpPost("test/{id}")]
        public async Task<ActionResult<Champion>> PostChampionTestWithID(int id, Champion champion)
        {
            _context.Champions.Add(champion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChampion", new { id = champion.Id }, champion);
        }

        [HttpPost("test/6")]
        public async Task<ActionResult<Champion>> PostChampionTest(Champion champion)
        {
            _context.Champions.Add(champion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChampion", new { id = champion.Id }, champion);
        }

        // POST: api/Champions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Champion>> PostChampion(Champion champion)
        {
            _context.Champions.Add(champion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChampion", new { id = champion.Id }, champion);
        }

        // DELETE: api/Champions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Champion>> DeleteChampion(Guid id)
        {
            var champion = await _context.Champions.FindAsync(id);
            if (champion == null)
            {
                return NotFound();
            }

            return Forbid();

            //_context.Champions.Remove(champion);
            //await _context.SaveChangesAsync();

            //return champion;
        }

        private bool ChampionExists(Guid id)
        {
            return _context.Champions.Any(e => e.Id == id);
        }
    }
}
