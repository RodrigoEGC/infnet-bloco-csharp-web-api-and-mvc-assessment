using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupEntitiesController : ControllerBase
    {
        private readonly LibraryMusicalContext _context;

        public GroupEntitiesController(LibraryMusicalContext context)
        {
            _context = context;
        }
        // GET: api/GroupEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupEntity>>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        // GET: api/GroupEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupEntity>> GetGroupEntity (int id)
        {
            var groupEntity = await _context.Groups
                .Include(x => x.Albums)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (groupEntity == null)
            {
                return NotFound();
            }

            return groupEntity;
        }

        // POST: api/GroupEntities
        [HttpPost]
        public async Task<ActionResult<GroupEntity>> PostGroupEntity(GroupEntity groupEntity)
        {
            _context.Groups.Add(groupEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupEntity", new { id = groupEntity.Id }, groupEntity);
        }
        // PUT: api/GroupEntities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupEntity(int id, GroupEntity groupEntity)
        {
            if (id != groupEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(groupEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupEntityExists(id))
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


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroupEntity>> DeleteGroupEntity(int id)
        {
            var groupEntity = await _context.Groups.FindAsync(id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(groupEntity);
            await _context.SaveChangesAsync();

            return groupEntity;
        }
        private bool GroupEntityExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
