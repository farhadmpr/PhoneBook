using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Domain.Contracts.People;
using PhoneBook.Domain.Contracts.Phones;
using PhoneBook.Domain.Contracts.Tags;
using PhoneBook.EndPoints.WebUI.Models.AAA;
using PhoneBook.Infrastructures.DataLayer.Common;
using PhoneBook.Infrastructures.DataLayer.People;
using PhoneBook.Infrastructures.DataLayer.Phones;
using PhoneBook.Infrastructures.DataLayer.Tags;

namespace PhoneBook.EndPoints.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            int minPasswordLenght = int.Parse(Configuration["MinPasswordLength"]);
            services.AddMvc();
            services.AddDbContext<PhoneBookContext>(c => c.UseSqlServer(Configuration.GetConnectionString("phoneBook")));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddDbContext<UserDbContext>(c => c.UseSqlServer(Configuration.GetConnectionString("aaa")));
            services.AddScoped<IPasswordValidator<AppUser>, MyPasswordValidator2>();
            services.AddScoped<IUserValidator<AppUser>, MyUserValidator>();
            
            services.AddIdentity<AppUser, IdentityRole>(c=>
            {
                c.User.RequireUniqueEmail = true;
                //c.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnmPOIUYTREWQLKJHGFDSAMNBVCXZ";
                c.Password.RequireDigit = false;
                c.Password.RequiredLength = minPasswordLenght;
                c.Password.RequireNonAlphanumeric = false;
                c.Password.RequireUppercase = false;
                c.Password.RequiredUniqueChars = 1;
                c.Password.RequireLowercase = false;
                

            }).AddEntityFrameworkStores<UserDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Exception");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
