using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Domain.Contracts.People;
using PhoneBook.Domain.Contracts.Tags;
using PhoneBook.Domain.Core.People;
using PhoneBook.EndPoints.WebUI.Models.People;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBook.EndPoints.WebUI.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IPersonRepository personRepository;

        public PeopleController(ITagRepository tagRepository, IPersonRepository personRepository)
        {
            this.tagRepository = tagRepository;
            this.personRepository = personRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var people = personRepository.GetAll().ToList();
            return View(people);
        }

        public IActionResult Add()
        {
            AddNewPersonDisplayViewModel model = new AddNewPersonDisplayViewModel();
            model.TagsForDisplay = tagRepository.GetAll().ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddNewPersonGetViewModel model)
        {


            if (ModelState.IsValid)
            {
                Person person = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Address = model.Address,
                    Tags = new List<PersonTag>(model.SelectedTag.Select(c => new PersonTag
                    {
                        TagId = c
                    }).ToList())
                };
                if (model?.Image?.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.Image.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        person.Image = Convert.ToBase64String(fileBytes);
                    }
                }
                personRepository.Add(person);
                return RedirectToAction("Index");
            }

            AddNewPersonDisplayViewModel modelforDisplay = new AddNewPersonDisplayViewModel
            {
                Address = model.Address,
                Email = model.Email,
                LastName = model.LastName,
                FirstName = model.FirstName
            };
            modelforDisplay.TagsForDisplay = tagRepository.GetAll().ToList();
            return View(modelforDisplay);
        }
    }
}
