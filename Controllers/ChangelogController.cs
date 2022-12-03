using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDPDocumentationWebAPI.Models;

namespace RDPDocumentationWebAPI.Controllers
{
    #region snippet_Route
    [Route("api/[controller]")]
    [ApiController]
    public class ChangelogController : ControllerBase
    #endregion
    {
        private readonly ChangelogContext _context;

        public ChangelogController(ChangelogContext context)
        {
            _context = context;
        }

        // GET: api/Changelogs
        #region snippet_Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Changelog>>> GetChangelogs()
        {
            return await _context.ChangelogItems.Include(x => x.ChangeLineItems).ToListAsync();
        }
        #endregion

        // GET: api/Changelogs/"ID"
        #region snippet_GetByID
        [HttpGet("byID/{id}")]
        public async Task<ActionResult<Changelog>> GetTodoItem(long id)
        {
            var todoItem = await _context.ChangelogItems.Where(c => c.Id == id).ToListAsync();

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }
        #endregion

        // GET: api/Changelogs/"TEAM"
        #region snippet_GetByTEAM
        [HttpGet("byTEAM/{team}")]
        public async Task<ActionResult<Changelog>> GetTodoItem(TeamEnum team)
        {
            var todoItem = await _context.ChangelogItems.Where(c => c.Team == team).ToListAsync();

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }
        #endregion

        // PUT: api/Changelogs/"ID"
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_UpdateChangelog
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChangelog(long id, [FromBody]Changelog changelog)
        {
            if (id != changelog.Id)
            {
                return BadRequest();
            }

            var entity = _context.ChangelogItems.FirstOrDefault(e => e.Id == id);
            entity.Team = changelog.Team;
            entity.Release = changelog.Release;
            entity.ChangeLineItems = changelog.ChangeLineItems;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChangelogExists(id))
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
        #endregion

        // POST: api/Changelogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_CreateChangelog
        [HttpPost]
        public async Task<ActionResult<Changelog>> PostTodoItem(Changelog changelog)
        {
            _context.ChangelogItems.Add(changelog);

            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetChangelogs), new { id = changelog.Id }, changelog);
        }
        #endregion

        // POST: api/Changelogs/"ID"/addChangeLineItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_AddChangeLineItem
        [HttpPut("AddChangeLineItem/{id}")]
        public async Task<ActionResult<Changelog>> PostNewChangeLineItem(long id)
        {
            Changelog c = await _context.ChangelogItems.Where(c => c.Id == id).FirstAsync();
            if (c == null)
                return BadRequest();

            c.AddChangeLineItem();

            _context.Entry(c).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChangelogExists(id))
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
        #endregion

        // DELETE: api/Changelogs/"ID"
        #region snippet_DeleteChangelog
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChangelog(long id)
        {
            var changelog = await _context.ChangelogItems.FindAsync(id);
            if (changelog == null)
            {
                return NotFound();
            }

            _context.ChangelogItems.Remove(changelog);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool ChangelogExists(long id)
        {
            return _context.ChangelogItems.Any(e => e.Id == id);
        }
    }
}