using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.EndPoints.WebUI.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}