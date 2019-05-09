using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.EndPoints.WebUI.Models.AAA;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBook.EndPoints.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> userMgr,
        SignInManager<AppUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;

        }


        public ViewResult Login(string returnUrl)
        {
            return View(new MyLoginModel
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MyLoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user =
                await userManager.FindByNameAsync(loginModel.Name);
                if (user == null)
                {
                    user = await userManager.FindByEmailAsync(loginModel.Name);
                }
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
