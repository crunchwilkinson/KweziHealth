using KweziHealth.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KweziHealth.Web.Data;

// Update the inheritance to include <SystemAdmin>
    public class ApplicationDbContext : IdentityDbContext<SystemAdmin>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<StaffMember> StaffMembers { get; set; }
    }
