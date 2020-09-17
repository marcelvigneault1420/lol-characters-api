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
    public class ChampionListsController : ControllerBase
    {
        private readonly LolContext _context;

        public ChampionListsController(LolContext context)
        {
            _context = context;
        }

        // GET: api/ChampionLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChampionList>>> GetChampionLists()
        {
            return await _context.ChampionLists.ToListAsync();
        }

        // GET: api/ChampionLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChampionList>> GetChampionList(Guid id)
        {
            var championList = await _context.ChampionLists
                .Include(list => list.ChampionListChampions)
                .ThenInclude(x => x.Champion)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (championList == null)
            {
                return NotFound();
            }

            return championList;
        }

        //TO TEST: If I add a champion in the list, does entity update
        // PUT: api/ChampionLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChampionList(Guid id, ChampionList championList)
        {
            if (id != championList.Id)
            {
                return BadRequest();
            }

            _context.Entry(championList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChampionListExists(id))
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

        // POST: api/ChampionLists
        [HttpPost]
        public async Task<ActionResult<ChampionList>> PostChampionList(ChampionList championList)
        {
            _context.ChampionLists.Add(championList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChampionList", new { id = championList.Id }, championList);
        }

        // DELETE: api/ChampionLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ChampionList>> DeleteChampionList(Guid id)
        {
            var championList = await _context.ChampionLists.FindAsync(id);
            if (championList == null)
            {
                return NotFound();
            }

            _context.ChampionLists.Remove(championList);
            await _context.SaveChangesAsync();

            return championList;
        }

        //Add a champion to a list
        [HttpPut("{idList}/Champions/{idChamp}")]
        public async Task<IActionResult> AddChampionToList(Guid idList, Guid idChamp)
        {
            var list = await _context.ChampionLists.Include(list => list.ChampionListChampions).SingleOrDefaultAsync(x => x.Id == idList);

            if (list == null)
            {
                return NotFound($"List not found");
            }

            if (list.ChampionListChampions.Find(x => x.ChampionId == idChamp) != null) 
            {
                return Conflict("Champion already in that list");
            }

            var champion = await _context.Champions.FindAsync(idChamp);

            if (champion == null)
            {
                return NotFound($"Champion {idChamp} not found");
            }

            list.ChampionListChampions.Add(new ChampionListChampion { Champion = champion, ChampionList = list });

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete a champion from a list
        [HttpDelete("{idList}/Champions/{idChamp}")]
        public async Task<IActionResult> DeleteChampionToList(Guid idList, Guid idChamp)
        {
            var list = await _context.ChampionLists.Include(list => list.ChampionListChampions).SingleOrDefaultAsync(x => x.Id == idList);

            if (list == null)
            {
                return NotFound($"List not found");
            }

            if (list.ChampionListChampions.Find(x => x.ChampionId == idChamp) == null)
            {
                return NotFound("Champion not in that list");
            }

            list.ChampionListChampions.RemoveAll(x => x.ChampionId == idChamp);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChampionListExists(Guid id)
        {
            return _context.ChampionLists.Any(e => e.Id == id);
        }
    }
}
