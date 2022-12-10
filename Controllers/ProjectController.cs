using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWebsiteWebAPI.Models;

namespace PersonalWebsite.Controllers
{
    #region snippet_Route
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    #endregion
    {
        private readonly ProjectContext _context;

        public ProjectController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Project
        #region snippet_Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjects()
        {
            return await _context.Projects.Include(project => project.ImgSrcs).ToListAsync();
        }
        #endregion

        // GET: api/Project/"ID"
        #region snippet_GetByID
        [HttpGet("byID/{id}")]
        public async Task<ActionResult<ProjectModel>> GetProjectById(long id)
        {
            var project = await _context.Projects.Where(c => c.Id == id).Include(project => project.ImgSrcs).ToListAsync();

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }
        #endregion

        // GET: api/Project/"Category"
        #region snippet_GetByCategory
        [HttpGet("byCategory/{category}")]
        public async Task<ActionResult<ProjectModel>> GetProjectsByCategory(string category)
        {
            var projects = await _context.Projects.Include(project => project.ImgSrcs).Where(c => c.Category == category).Include(project => project.ImgSrcs).ToListAsync();

            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);
        }
        #endregion

        // PUT: api/Changelogs/"ID"
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_UpdateChangelog
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(long id, [FromBody] ProjectModel project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            var entity = _context.Projects.FirstOrDefault(e => e.Id == id);
            //TODO update each entity property individually

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Project
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region snippet_CreateProject
        [HttpPost]
        public async Task<ActionResult<ProjectModel>> PostProject(ProjectModel project)
        {
            _context.Projects.Add(project);

            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, project);
        }
        #endregion

        // DELETE: api/Project/"ID"
        #region snippet_DeleteProject
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}