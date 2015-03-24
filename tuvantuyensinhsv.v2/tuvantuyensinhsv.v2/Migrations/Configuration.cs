namespace tuvantuyensinhsv.v2.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using tuvantuyensinhsv.v2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<tuvantuyensinhsv.v2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "tuvantuyensinhsv.v2.Models.ApplicationDbContext";
        }

        protected override void Seed(tuvantuyensinhsv.v2.Models.ApplicationDbContext context)
        {
            IntRole();

            var manager = new UserManager<ApplicationUser>(
               new UserStore<ApplicationUser>(
                   new ApplicationDbContext()));

            // Create 4 test users:
            var user = new ApplicationUser()
            {
                UserName = string.Format("admin@aa.aa"),
                Email = string.Format("admin@aa.aa")
            };
            manager.Create(user, string.Format("ngahieu"));

            AddUserToRole("admin@aa.aa", "Admin");
        }

        private void AddUserToRole(string userName, string roleName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            try
            {
                var user = UserManager.FindByName(userName);
                UserManager.AddToRole(user.Id, roleName);
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        private void IntRole()
        {
            AddRole("Admin");
            AddRole("Moderate");
            AddRole("User");
        }

        private void AddRole(string roleName)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (!roleManager.RoleExists(roleName))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = roleName;
                roleManager.Create(role);
            }
        }
    }
}
