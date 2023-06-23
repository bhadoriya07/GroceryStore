using DomainModelLayer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class SeedData
    {
        public static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Seed admin user
            var adminUser = new ApplicationUser
            {
                Email = "admin@gmail.com",
                FullName = "Admin",
                PhoneNumber = "1234567890",
                IsAdmin = true // Set the IsAdmin field to true
            };
            var adminPassword = "Admin@1234"; // Set the admin password

            var adminUserExists = await userManager.FindByEmailAsync(adminUser.Email);
            if (adminUserExists == null)
            {
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // User created successfully
                }
            }
        }
    }

}
