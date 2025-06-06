using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfDbCarService.Models;
using Microsoft.VisualBasic;

namespace EfDbCarService
{
    public class IdentityInitializator
    {
        public static void Initialize (UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@gmail.com";
            var password = "123456Aa/";
            if (roleManager.FindByNameAsync(Constants.AdminRoleName).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName)).Wait();
            }
            if (roleManager.FindByNameAsync(Constants.UserRoleName).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.UserRoleName)).Wait();
            }
            if (userManager.FindByNameAsync(adminEmail).Result == null)
            {
                var admin = new User { Email = adminEmail, UserName = adminEmail };
                var result = userManager.CreateAsync(admin, password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, Constants.AdminRoleName).Wait();
                }
        }
    }
}}
