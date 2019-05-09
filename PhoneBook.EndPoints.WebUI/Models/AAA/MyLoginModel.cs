using System.ComponentModel.DataAnnotations;

namespace PhoneBook.EndPoints.WebUI.Models.AAA
{
    public class MyLoginModel
    {
        [Required]
        [Display(Name = "User Name / Email ")]
        public string Name { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
