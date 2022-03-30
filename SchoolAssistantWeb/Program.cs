using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.InjectablePattern;

var builder = WebApplication.CreateBuilder(args);

#region Database

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SADbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

#endregion

#region Identity & Razor pages

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<SADbContext>();

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

#endregion

builder.Services.AddAllInjectable();

// TODO: EntityFramework might be unnecessarily referenced

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

#region Seeding default data

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var seeder = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IDefaultDataSeeder>();
await seeder.SeedAllAsync();

#endregion

app.Run();
