using BamApps.Identity.WebApi.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BamApps.Identity.WebApi.Infrastructure {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Audience> Audiences { get; set; }

        public ApplicationDbContext()
            : base("IdentityConnection", throwIfV1Schema: false) {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }

    }
}