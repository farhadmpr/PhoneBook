using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.EndPoints.WebUI.Models.AAA;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBook.EndPoints.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var users = userManager.Users.Take(50).ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.Email,
                    UserName = model.UserName
                };
                var result = userManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
            }
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var user = userManager.FindByIdAsync(id.ToString()).Result;
            if (user != null)
            {
                UpdateUserViewModel model = new UpdateUserViewModel
                {
                    Email = user.Email
                };
                return View(user);
            }
            return NotFound();
        }
        [HttpPut]
        public IActionResult Update(int id, UpdateUserViewModel updateUserViewModel)
        {
            var user = userManager.FindByIdAsync(id.ToString()).Result;
            if (user != null)
            {
                user.Email = updateUserViewModel.Email;
                var result = userManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
                return View(updateUserViewModel);
            }
            return NotFound();
        }
        public IActionResult Delete(string id)
        {
            var user = userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                var result = userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
            }
            return View();
        }

        public IActionResult AddToRole(string id, string roleName)
        {
            var user = userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
               var result = userManager.AddToRoleAsync(user, roleName).Result;
                
            }
            return RedirectToAction("Index");
        }


    }
}
