using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KweziHealth.Web.Data;
using KweziHealth.Web.Models;
using KweziHealth.Web.Services;

// Deliverable 4

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework Core to use an In-Memory database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("KweziHealthEsopDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure ASP.NET Core Identity using our custom SystemAdmin class.
// Links Identity to our ApplicationDbContext and adds default token providers
builder.Services.AddIdentity<SystemAdmin, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Override the default Identity cookie settings to route unauthorized users 
// to our custom AccessController instead of the default Identity Razor Pages.
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Access/Login";
    options.LogoutPath = "/Access/Logout";
});

// Register the StaffService so it can be injected into the StaffController.
// AddScoped ensures a new instance is created per HTTP request.
builder.Services.AddScoped<IStaffService, StaffService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Create a temporary dependency injection scope to execute our database seeder on startup.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    // Resolve the required services
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<SystemAdmin>>();
    
    // Instantiate the seeder and call SeedAsync
    var seeder = new DataSeeder(context, userManager);
    await seeder.SeedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Staff/Error");
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

app.Run();
