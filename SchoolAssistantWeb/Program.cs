using AppConfigurationEFCore.Setup;
using Azure.Identity;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using JavaScriptEngineSwitcher.V8;
using Microsoft.EntityFrameworkCore;
using React.AspNet;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.AzureKeyVault;
using SchoolAssistant.Infrastructure.InjectablePattern;
using SchoolAssistant.Logic.Help;
using SchoolAssistant.Web.PagesRelated.Filters;

var builder = WebApplication.CreateBuilder(args);

#region Secrets

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri(Environment.GetEnvironmentVariable("KEYVAULT_ENDPOINT") ?? throw new EntryPointNotFoundException("Envitonment variable KEYVAULT_ENDPOINT not found")),
        new DefaultAzureCredential(/*new DefaultAzureCredentialOptions
        {
            ManagedIdentityClientId = builder.Configuration["KeyVault:ClientId"]
        }*/),
        new PrefixKeyVaultSecretManager("SchoolAssistant"));
}

#endregion

#region Database

var connectionString = builder.Configuration["ConnectionString:DefaultConnection"];
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
        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
    })
    .AddMvcOptions(options =>
    {
        options.Filters.Add<EnableLessonConductionPanelAsyncPageFilter>();
        options.Filters.Add<NavbarLinksAsyncPageFilter>();
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Index";
    options.AccessDeniedPath = $"/Index";
});

builder.Services.AddAuthorization();

#endregion

#region Configure ISession

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    //options.Cookie.HttpOnly = true;
    //options.Cookie.IsEssential = true;
});

#endregion

builder.Services.AddAppConfiguration<SADbContext, AppConfigRecords>(opt =>
{
    opt.Add(new DaysOfWeekRecordHandlerRule());
});

builder.Services.AddAllInjectable();

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
        .AddScriptWithoutTransform("~/dist/scheduled_lessons_list.bundle.js")
        .AddScriptWithoutTransform("~/dist/lesson_conduction_panel.bundle.js");

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
await seeder.SeedRolesAndAdminAsync().ConfigureAwait(false);
await seeder.SeedAppConfigAsync().ConfigureAwait(false);

#endregion

await app.RunAsync().ConfigureAwait(false);
