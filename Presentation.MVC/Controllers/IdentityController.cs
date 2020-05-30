﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Mvc.Controllers
{
    public class IdentityController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-3.1
        public async Task<IActionResult> AddAdminClaim(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddClaimAsync(user, new Claim("AdminClaim", string.Empty));
            await _signInManager.RefreshSignInAsync(user);

            return RedirectToAction("Index", "Group");
        }
    }
}