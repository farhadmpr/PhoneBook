using Microsoft.AspNetCore.Identity;
using System;

namespace PhoneBook.EndPoints.WebUI.Models.AAA
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
    public class MyIdentityRole:IdentityRole<int>
    {

    }
}
