using System.Threading.Tasks;
using Domain.Model.Entities;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Mvc.ViewModels;

namespace Presentation.Mvc.Controllers
{
    [Authorize]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IGroupService _groupService;

        public AlbumController(
            IGroupService groupService,
            IAlbumService albumService)
        {
            _albumService = albumService;
            _groupService = groupService;
        }
        // GET: Album
        public async Task<IActionResult> Index()
        {
            var albuns = await _albumService.GetAllAsync();

            if (albuns == null)
                return Redirect("/Identity/Account/Login");
            return View(albuns);
        }

        // GET: Album/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var albumModel = await _albumService.GetByIdAsync(id.Value);
            if(albumModel == null)
            {
                return NotFound();
            }
            return View(albumModel);
        }

        // GET: Album/Create
        public async Task<IActionResult> Create()
        {
            var albumModel = new AlbumViewModel(await _groupService.GetAllAsync());

            return View(albumModel);
        }

        // POST: Album/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlbumEntity albumEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _albumService.InsertAsync(albumEntity);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }
            return View(new AlbumViewModel(albumEntity));
        }

        // GET: Album/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumModel = await _albumService.GetByIdAsync(id.Value);
            if (albumModel == null)
            {
                return NotFound();
            }

            var albumViewModel = new AlbumViewModel(albumModel, await _groupService.GetAllAsync());

            return View(albumViewModel);
        }

        // POST: Album/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlbumEntity albumEntity)
        {

            if (id != albumEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _albumService.UpdateAsync(albumEntity);
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                    return View(new AlbumViewModel(albumEntity));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _albumService.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(new AlbumViewModel(albumEntity));
        }

        // GET: Album/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumModel = await _albumService.GetByIdAsync(id.Value);
            if (albumModel == null)
            {
                return NotFound();
            }

            return View(albumModel);
        }

        // POST: Album/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _albumService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}