using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KweziHealth.Web.Data;
using KweziHealth.Web.Models;
using KweziHealth.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("KweziHealthEsopDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<SystemAdmin>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Access/Login";
    options.LogoutPath = "/Access/Logout";
});

builder.Services.AddScoped<IStaffService, StaffService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Staff/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Staff}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<SystemAdmin>>();

    // 1. Test In-Memory Database & StaffMember Model
    if (!context.StaffMembers.Any())
    {
        context.StaffMembers.Add(new StaffMember
        {
            FullName = "Kwezi Tester",
            Email = "tester@kwezihealth.co.za",
            Position = "Lead Systems Developer",
            Unit = "Digital Health IT"
        });
        context.SaveChanges();
    }

    // 2. Test Identity Setup & SystemAdmin Inheritance
    var adminEmail = "admin@kwezihealth.co.za";
    var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
    if (existingAdmin == null)
    {
        var admin = new SystemAdmin
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, "Admin@123456!");
        if (result.Succeeded)
        {
            Console.WriteLine("--> [SUCCESS] SystemAdmin created via Identity!");
        }
        else
        {
            Console.WriteLine($"--> [ERROR] Failed to create admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }

    // 3. Print verified data to Console
    var staffCount = context.StaffMembers.Count();
    var adminCount = context.Users.Count();
    
    Console.WriteLine("==================================================");
    Console.WriteLine($"--> Database Verification Successful!");
    Console.WriteLine($"--> Total Staff Records in Memory: {staffCount}");
    Console.WriteLine($"--> Total System Admins in Identity: {adminCount}");
    Console.WriteLine("==================================================");
}

app.Run();
