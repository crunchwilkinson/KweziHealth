using Microsoft.AspNetCore.Identity;
using KweziHealth.Web.Models;

namespace KweziHealth.Web.Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<SystemAdmin> _userManager;

        public DataSeeder(ApplicationDbContext context, UserManager<SystemAdmin> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            // Seed Staff Members
            if (!_context.StaffMembers.Any())
            {
                _context.StaffMembers.AddRange(
                    new StaffMember
                    {
                        FullName = "Kwezi Tester",
                        Email = "tester@kwezihealth.co.za",
                        Position = "Lead Systems Developer",
                        Unit = "Digital Health IT"
                    },
                    new StaffMember
                    {
                        FullName = "Dr. Thabo Mokoena",
                        Email = "t.mokoena@kwezihealth.co.za",
                        Position = "Chief Medical Officer",
                        Unit = "Cardiology"
                    },
                    new StaffMember
                    {
                        FullName = "Sarah van der Merwe",
                        Email = "s.vandermerwe@kwezihealth.co.za",
                        Position = "Head Nurse",
                        Unit = "Emergency Department"
                    },
                    new StaffMember
                    {
                        FullName = "Sipho Ndlovu",
                        Email = "s.ndlovu@kwezihealth.co.za",
                        Position = "IT Support Specialist",
                        Unit = "Digital Health IT"
                    },
                    new StaffMember
                    {
                        FullName = "Dr. Fatima Desai",
                        Email = "f.desai@kwezihealth.co.za",
                        Position = "Senior Pediatrician",
                        Unit = "Pediatrics Wing"
                    }
                );
                
                await _context.SaveChangesAsync();
            }

            // Test Identity Setup & SystemAdmin Inheritance
            var adminEmail = "admin@kwezihealth.co.za";
            var existingAdmin = await _userManager.FindByEmailAsync(adminEmail);
            
            if (existingAdmin == null)
            {
                var admin = new SystemAdmin
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(admin, "Admin@123456!");
                if (result.Succeeded)
                {
                    Console.WriteLine("[SUCCESS] SystemAdmin created via Identity!");
                }
                else
                {
                    Console.WriteLine($"[ERROR] Failed to create admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            // Print Verification Output
            var staffCount = _context.StaffMembers.Count();
            var adminCount = _context.Users.Count();
            
            Console.WriteLine("==================================================");
            Console.WriteLine($"[SUCCESS] Database Verification Successful!");
            Console.WriteLine($"[INFO] Total Staff Records in Memory: {staffCount}");
            Console.WriteLine($"[INFO] Total System Admins in Identity: {adminCount}");
            Console.WriteLine("==================================================");
        }
    }
}