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
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(
            IAlbumService albumService)
        {
            _albumService = albumService;
        }
        // GET: api/Album
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumEntity>>> GetAlbumEntity()
        {
            var albums = await _albumService.GetAllAsync();
            return Ok(albums.ToList());
        }

        // GET: api/Album/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumEntity>> GetAlbumEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var albumEntity = await _albumService.GetByIdAsync(id);

            if (albumEntity == null)
            {
                return NotFound();
            }

            return Ok(albumEntity);
        }

        // POST: api/Album
        [HttpPost]
        public async Task<ActionResult<AlbumEntity>> PostAlbumEntity(AlbumEntity albumEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _albumService.InsertAsync(albumEntity);

                return Ok(albumEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Album/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbumEntity(int id, AlbumEntity albumEntity)
        {
            if (id != albumEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _albumService.UpdateAsync(albumEntity);
                return Ok();
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

        // DELETE: api/Album/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AlbumEntity>> DeleteAlbumEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var albumEntity = await _albumService.GetByIdAsync(id);
            if (albumEntity == null)
            {
                return NotFound();
            }

            await _albumService.DeleteAsync(id);

            return Ok(albumEntity);
        }
    }
}
