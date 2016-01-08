namespace BamApps.Identity.WebApi.Migrations {
    using Entities;
    using Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BamApps.Identity.WebApi.Infrastructure.ApplicationDbContext> {

        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BamApps.Identity.WebApi.Infrastructure.ApplicationDbContext context) {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser() {
                UserName = "BillFugina",
                Email = "bill@dogspots.com",
                EmailConfirmed = true,
                FirstName = "Bill",
                LastName = "Fugina",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "password");

            if (roleManager.Roles.Count() == 0) {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("BillFugina");

            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });

            if (context.Clients.Count() == 0) {
                context.Clients.AddRange(BuildClientsList());
                context.SaveChanges();
            }

            if (context.Audiences.Count() == 0) {
                context.Audiences.AddRange(BuildAudienceList());
                context.SaveChanges();
            }
        }



        private static List<Client> BuildClientsList() {

            List<Client> ClientsList = new List<Client>
            {
                new Client
                { Id = "excido",
                    Secret= Helper.GetHash("^ 41LNHg"),
                    Name="Excido front-end Application",
                    ApplicationType =  Models.ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "http://www.excido.net"
                },
                new Client
                { Id = "local",
                    Secret= Helper.GetHash("^ 41LNHg"),
                    Name="Excido front-end Application",
                    ApplicationType =  Models.ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "https://localhost:44300"
                }

            };

            return ClientsList;
        }

        public static List<Audience> BuildAudienceList() {
            List<Audience> result = new List<Audience> {
                new Audience {
                    Id = Helper.CreateStringId(),
                    Secret = Helper.CreateNewSecret(),
                    Name = "Authorization Server"
                },
                new Audience {
                    Id = Helper.CreateStringId(),
                    Secret = Helper.CreateNewSecret(),
                    Name = "Excido Api Server"
                }

            };
            return result;
        }


    }

}
