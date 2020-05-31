using System.Threading.Tasks;
using Domain.Model.Entities;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Mvc.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        // GET: Group
        public async Task<IActionResult> Index()
        {
            var groups = await _groupService.GetAllAsync();
            if (groups == null)
                return Redirect("/Identity/Account/Login");
            return View(groups);
        }

        // GET: Group/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var groupEntity = await _groupService.GetByIdAsync(id.Value);
            if (groupEntity == null)
            {
                return NotFound();
            }
            return View(groupEntity);
        }

        // GET: Group/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Genre,Formed,City,Nation")] GroupEntity groupEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here

                    await _groupService.InsertAsync(groupEntity);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }
            return View(groupEntity);
        }

        // GET: Group/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _groupService.GetByIdAsync(id.Value);
            if (groupEntity == null)
            {
                return NotFound();
            }
            return View(groupEntity);
        }

        // POST: Group/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,Formed,City,Nation")] GroupEntity groupEntity)
        {
            if (id != groupEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _groupService.UpdateAsync(groupEntity);
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                    return View(groupEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _groupService.GetByIdAsync(id) == null)
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
            return View(groupEntity);
        }

        // GET: Group/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _groupService.GetByIdAsync(id.Value);
            if (groupEntity == null)
            {
                return NotFound();
            }

            return View(groupEntity);
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _groupService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckName(string name, int id)
        {
            if (await _groupService.CheckNameAsync(name, id))
            {
                return Json($"Group Name: {name} já existe!");
            }

            return Json(true);
        }
    }
}