using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.EndPoints.WebUI.Models.AAA
{
    public class MyPasswordValidator2 : PasswordValidator<AppUser>
    {
        private readonly UserDbContext dbContext;

        public MyPasswordValidator2(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public override Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            var parentResult = base.ValidateAsync(manager, user, password).Result;

            List<IdentityError> errors = new List<IdentityError>();
            if (!parentResult.Succeeded)
            {
                errors = parentResult.Errors.ToList();
            }
            if (user.UserName == password || user.UserName.Contains(password))
            {
                errors.Add(new IdentityError
                {
                    Code = "Password",
                    Description = "Password is equal to username"
                });
            }
            if(dbContext.BadPasswords.Any(c=>c.Passwrod == password))
            {
                errors.Add(new IdentityError
                {
                    Code = "Password",
                    Description = "You can not select password from bad password List"
                });
            }
            return Task.FromResult(errors.Any() ?
                IdentityResult.Failed(errors.ToArray()) :
                IdentityResult.Success
                );

        }
    }
}
