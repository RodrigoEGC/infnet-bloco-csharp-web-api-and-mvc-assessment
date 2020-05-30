using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Entities;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        // GET: api/Group
        [HttpGet]
        public async Task<IEnumerable<GroupEntity>> GetGroupEntity()
        {
            var groups = await _groupService.GetAllAsync();
            return groups.ToList();
        }

        // GET: api/Group/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupEntity>> GetGroupEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var groupEntity = await _groupService.GetByIdAsync(id);
            if (groupEntity == null)
            {
                return NotFound();
            }
            return groupEntity;
        }

        // POST: api/Group
        [HttpPost]
        public async Task<ActionResult<GroupEntity>> PostGroupEntity(GroupEntity groupEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _groupService.InsertAsync(groupEntity);

                return CreatedAtAction(
                    "GetGroupEntity",
                    new { id = groupEntity.Id }, groupEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Group/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupEntity(int id, GroupEntity groupEntity)
        {
            if (id != groupEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _groupService.UpdateAsync(groupEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
            catch (RepositoryException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        // DELETE: api/Group/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroupEntity>> DeleteGroupEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var groupEntity = await _groupService.GetByIdAsync(id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            await _groupService.DeleteAsync(id);

            return groupEntity;
        }
        //[HttpGet("CheckName/{name}/{id}")]
        //public async Task<ActionResult<bool>> CheckIsbnAsync(string name, int id)
        //{
        //    var isNameValid = await _groupService.CheckNameAsync(name, id);

        //    return isNameValid;
        //}
    }
}
