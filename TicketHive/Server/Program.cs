using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Server.Models;
using TicketHive.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("UsersDbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var secondConnectionString = builder.Configuration.GetConnectionString("EventDbConnection");
builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlServer(secondConnectionString));

builder.Services.AddScoped<IEventRepo, EventRepo>();

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
    {
        options.IdentityResources["openid"].UserClaims.Add("role");
        options.ApiResources.Single().UserClaims.Add("role");
    });

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

using (var serviceProvider = builder.Services.BuildServiceProvider())
{
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
    var signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    context.Database.Migrate();

    ApplicationUser? user = signInManager.UserManager.FindByNameAsync("user").GetAwaiter().GetResult();

    if (user == null)
    {
        user = new()
        {
            UserName = "user"
        };

        signInManager.UserManager.CreateAsync(user, "Password1234!").GetAwaiter().GetResult();
    }

    ApplicationUser? adminUser = signInManager.UserManager.FindByNameAsync("admin").GetAwaiter().GetResult();

    if (adminUser == null)
    {
        adminUser = new()
        {
            UserName = "admin",
        };

        signInManager.UserManager.CreateAsync(adminUser, "Password1234!");
    }
    IdentityRole? adminRole = roleManager.FindByNameAsync("Admin").GetAwaiter().GetResult();

    if (adminRole == null)
    {
        adminRole = new()
        {
            Name = "Admin",
        };

        roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
    }

    signInManager.UserManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
