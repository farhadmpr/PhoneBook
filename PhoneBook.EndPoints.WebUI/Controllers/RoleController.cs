using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.EndPoints.WebUI.Models.AAA;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBook.EndPoints.WebUI.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<MyIdentityRole> roleManager;

        public RoleController(RoleManager<MyIdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create(string roleName)
        {
            MyIdentityRole role = new MyIdentityRole
            {
                Name = roleName
            };
            var result = roleManager.CreateAsync(role).Result;
            return RedirectToAction("Index");
        }
    }
}
