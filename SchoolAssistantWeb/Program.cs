using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using JavaScriptEngineSwitcher.V8;
using Microsoft.EntityFrameworkCore;
using React.AspNet;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.InjectablePattern;

var builder = WebApplication.CreateBuilder(args);

#region Database

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SADbContext>(options =>
    options.UseSqlServer(connectionString).UseLazyLoadingProxies());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

#endregion

#region ReactJS.NET

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddReact();

// Make sure a JS engine is registered, or you will get an error!
builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
  .AddV8();

#endregion

#region Identity & Razor pages

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<SADbContext>();

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation()
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

#region ReactJS.NET pt 2

// Initialise ReactJS.NET. Must be before static files.
app.UseReact(config =>
{
    config
       .AddScript("~/js/shared/server-connection.tsx")
       .AddScript("~/js/shared/loader.tsx")
       .AddScript("~/js/shared/validators.tsx")
       .AddScript("~/js/shared/form-controls.tsx")
       .AddScript("~/js/shared/modal.tsx");


    config
        .AddScript("~/js/data-management/main.tsx")
        .AddScript("~/js/data-management/shared-table.tsx")
        .AddScript("~/js/data-management/table.tsx")
        .AddScript("~/js/data-management/rooms.tsx")
        .AddScript("~/js/data-management/students.tsx")
        .AddScript("~/js/data-management/register-records.tsx")
        .AddScript("~/js/data-management/subjects.tsx")
        .AddScript("~/js/data-management/staff.tsx")
        .AddScript("~/js/data-management/classes.tsx")
        .AddScript("~/js/data-management/navigation.tsx")
        .AddScript("~/js/data-management/enums.tsx");

    config
        .AddScript("~/js/schedule/schedule-arranger-timeline-components.tsx")
        .AddScript("~/js/schedule/schedule-arranger-selector-components.tsx")
        .AddScript("~/js/schedule/schedule-arranger-page.tsx")
        .AddScript("~/js/schedule/schedule-data-service.tsx")
        .AddScript("~/js/schedule/class-selector.tsx")
        .AddScript("~/js/schedule/main.tsx");

});

#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

#region Seeding default data

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var seeder = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IDefaultDataSeeder>();
await seeder.SeedRolesAndAdminAsync();
await seeder.SeedAppConfigAsync();

#endregion

app.Run();
