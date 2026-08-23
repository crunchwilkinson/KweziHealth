using KweziHealth.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KweziHealth.Web.Data;
    // Deliverable 1

    /// <summary>
    /// Represents the application's database context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<SystemAdmin>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Represents the StaffMembers table in the database.
        /// </summary>
        public DbSet<StaffMember> StaffMembers { get; set; }
    }
