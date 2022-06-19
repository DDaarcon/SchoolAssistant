using AppConfigurationEFCore.Setup;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using JavaScriptEngineSwitcher.V8;
using Microsoft.EntityFrameworkCore;
using React.AspNet;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.InjectablePattern;
using SchoolAssistant.Logic.Help;
using SchoolAssistant.Web.PageFilters;

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
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    PasswordHelper.ApplyDefaultOptionsTo(options.Password);
})
    .AddEntityFrameworkStores<SADbContext>();

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
    })
    .AddMvcOptions(options =>
    {
        options.Filters.Add<EnableReactLessonConductionAsyncPageFilter>();
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

#endregion

#region Configure ISession

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    //options.IdleTimeout = default 20 minutes;
    //options.Cookie.HttpOnly = true;
    //options.Cookie.IsEssential = true;
});

#endregion

builder.Services.AddAppConfiguration<SADbContext, AppConfigRecords>();

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
        .SetReuseJavaScriptEngines(true)
        .SetLoadBabel(false)
        .SetLoadReact(false)
        .AddScriptWithoutTransform("~/dist/runtime.bundle.js")
        .AddScriptWithoutTransform("~/dist/vendor.bundle.js")
        .AddScriptWithoutTransform("~/dist/react_lib.bundle.js")
        .AddScriptWithoutTransform("~/dist/shared.bundle.js")
        .AddScriptWithoutTransform("~/dist/data_management.bundle.js")
        .AddScriptWithoutTransform("~/dist/schedule_shared.bundle.js")
        .AddScriptWithoutTransform("~/dist/schedule_arranger.bundle.js")
        .AddScriptWithoutTransform("~/dist/schedule_display.bundle.js")
        .AddScriptWithoutTransform("~/dist/users_management.bundle.js")
        .AddScriptWithoutTransform("~/dist/scheduled_lessons_list.bundle.js");

});

#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

#region Seeding default data

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var seeder = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IDefaultDataSeeder>();
await seeder.SeedRolesAndAdminAsync();
await seeder.SeedAppConfigAsync();

#endregion

await app.RunAsync().ConfigureAwait(false);
