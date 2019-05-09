using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.EndPoints.WebUI.Models.AAA
{
    public class CreateUserViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]

        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]

        public string Password { get; set; }
    }

    public class UpdateUserViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
