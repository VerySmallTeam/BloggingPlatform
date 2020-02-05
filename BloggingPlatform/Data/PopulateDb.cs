using BloggingPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Data
{
    public static class PopulateDb
    {
        public static void PopulateDbWithUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/PopulateData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                //create roles
                var roles = new List<Role>
                {
                    new Role {Name = "Member"},
                    new Role {Name = "Administrator"}
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                foreach (var user in users)
                {
                    userManager.CreateAsync(user, "L0ngP@$$w0rd").Wait();
                    userManager.AddToRoleAsync(user, "Member").Wait();
                }

                //create admin
                var adminUser = new User
                {
                    UserName = "Admin",
                    Email = "adminemail@adminblogemail.com",
                    FirstName = "John",
                    LastName = "TheAdmin"
                };

                var result = userManager.CreateAsync(adminUser, "AdminPa$$w0rd").Result;

                if (result.Succeeded)
                {
                    var admin = userManager.FindByEmailAsync("adminemail@adminblogemail.com").Result;
                    userManager.AddToRoleAsync(admin, "Administrator").Wait();
                }
            }
        }
    }
}
