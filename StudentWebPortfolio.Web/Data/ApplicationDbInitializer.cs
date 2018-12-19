using Microsoft.AspNetCore.Identity;
using StudentWebPortfolio.Common;
using StudentWebPortfolio.Data;
using StudentWebPortfolio.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWebPortfolio.Web.Data
{
    public class ApplicationDbInitializer
    {
        private const string ADMIN_PASSWORD = "qweertyg17Z++";

        public static void Initialize(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (context.Users.Any())
                return; // DB has been seeded.

            // Seed roles.
            foreach (string userRole in UserRole.AllRoles)
            {
                var role = new Role
                {
                    Name = userRole,
                    NormalizedName = userRole,
                };
                var identityResult = roleManager.CreateAsync(role).Result;
                if (!identityResult.Succeeded)
                    throw new Exception($"Can not seed role '{role.ToString()}'");
            }

            // Seed admin.
            var admin = new User
            {
                UserName = Constants.AdminEmail,
                Email = Constants.AdminEmail,
                FirstName = "Admin",
                LastName = "Admin",
            };
            var result = userManager.CreateAsync(admin, ADMIN_PASSWORD).Result;
            if (!result.Succeeded)
                throw new Exception($"Can not seed user {admin.Email}");
            result = userManager.AddToRoleAsync(admin, UserRole.Admin).Result;
            if (!result.Succeeded)
                throw new Exception($"Can not add user {admin.Email} to role {UserRole.Admin}");

            // Seed Skills.
            var skills = new string[]
                { "C#", "Java", "C++", "Kotlin" }
                .Select(_ => new Skill { Name = _, ValidatedByUserId = admin.Id })
                .ToArray();

            context.AddRange(skills);
            context.SaveChanges();
        }
    }
}
