using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace database2.Data
{
    public class SampleData
    {
        public static decimal Price { get; private set; }

        public static async Task InitializaAsync(IServiceProvider serviceProvider)
        {
            //var serviceScope = serviceProvider.GetService<IServiceScopefactory>().CreateScope();
            var dbContext = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            string[] roles = new string[] { "Administrator", "User" };

            foreach (var role in roles)
            {
                var isExist = await roleManager.RoleExistsAsync(role);
                if (!isExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var adminUser = new IdentiyUser
            {
                Email = "not.supachai091@gmail.com",
                UserName = "not.supachai091@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var currentUser = await userManager.FindByEmailAsync(adminUser.Email);
            if (currentUser == null)
            {
                await userManager.CreateAsync(adminUser, "Secret123!");
                currentUser = await userManager.FindByEmailAsync(adminUser.Email);
            }
            var isAdmin = await userManager.IsInRoleAsync(currentUser, "Administrator");
            if (!isAdmin)
            {
                await userManager.AddToRoleAsync(currentUser, roles);
            }
            var containSampleBook = await userManager.IsInRoleAsync(currentUser, "Administrator");
            if (!containSampleBook)
            {
                dbContext.Books.Add(new Models.Book
                { 
                Name = "SampleBook",
                Price = 100m
                });
            }
            await dbContext.ServeChngesAsync(); 
        }
       
    }
}
