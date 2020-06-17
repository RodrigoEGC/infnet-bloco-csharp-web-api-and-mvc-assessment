using System.Net;
using System.Threading.Tasks;
using Domain.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Mvc.HttpServices;

namespace Presentation.Mvc.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly IGroupHttpService _groupHttpService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public GroupController(
            IGroupHttpService groupHttpService,
            SignInManager<IdentityUser> signInManager)
        {
            _groupHttpService = groupHttpService;
            _signInManager = signInManager;
        }
        // GET: Group
        public async Task<IActionResult> Index()
        {
            var groups = await _groupHttpService.GetAllAsync();
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

            var httpResponseMessage = await _groupHttpService.GetByIdHttpAsync(id.Value);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
                {
                    await _signInManager.SignOutAsync();
                    return Redirect("/Identity/Account/Login");
                }
                else
                {
                    var message = await httpResponseMessage.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, message);

                    var groups = await _groupHttpService.GetAllAsync();
                    return View(nameof(Index), groups);
                }
            }

            var groupModel = JsonConvert.DeserializeObject<GroupEntity>(await httpResponseMessage.Content.ReadAsStringAsync());
            if (groupModel == null)
            {
                return NotFound();
            }

            return View(groupModel);
        }

        // GET: Group/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupEntity groupEntity)
        {
            if (ModelState.IsValid)
            {
                await _groupHttpService.InsertAsync(groupEntity);
                return RedirectToAction(nameof(Index));
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

            var groupEntity = await _groupHttpService.GetByIdAsync(id.Value);
            if (groupEntity == null)
            {
                return NotFound();
            }
            return View(groupEntity);
        }

        // POST: Group/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GroupEntity groupEntity)
        {
            if (id != groupEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _groupHttpService.UpdateAsync(groupEntity);

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

            var groupEntity = await _groupHttpService.GetByIdAsync(id.Value);
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
            await _groupHttpService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckName(string name, int id)
        {
            if (await _groupHttpService.CheckNameAsync(name, id))
            {
                return Json($"Group Name: {name} já existe!");
            }

            return Json(true);
        }
    }
}