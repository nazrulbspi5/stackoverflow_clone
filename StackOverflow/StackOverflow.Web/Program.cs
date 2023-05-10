using Autofac;
using Autofac.Extensions.DependencyInjection;
using log4net;
using Microsoft.AspNetCore.Identity;
using StackOverflow.DAL;
using StackOverflow.DAL.Entities.Authentication;
using FluentNHibernate.AspNetCore.Identity;
using StackOverflow.Services;
using StackOverflow.Services.Authentication;
using StackOverflow.Web;
using StackOverflow.DAL.NHibernate;
using FluentNHibernate.AspNetCore.Identity.Mappings;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using Autofac.Core;
using StackOverflow.DAL.Mapping.Membership;
using StackOverflow.DAL.Mapping.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Configure Log4Net
builder.WebHost.ConfigureLogging(builder =>
{
    builder.AddLog4Net("log4net.config");
});
var log = LogManager.GetLogger(typeof(Program));

//Autofac Configuration
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new WebModule());
    containerBuilder.RegisterModule(new DalModule());
    containerBuilder.RegisterModule(new ServiceModule());
});

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(x => new SessionManagerFactory(connectionString).OpenSession());


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
      .ExtendConfiguration()
      .AddNHibernateStores(t => t.SetSessionAutoFlush(true))
       .AddUserManager<ApplicationUserManager>()
        .AddRoleManager<ApplicationRoleManager>()
     .AddSignInManager<ApplicationSignInManager>()
     .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    // SignIn settings
    options.SignIn.RequireConfirmedAccount = false;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();
//log.Info("Application Starting up");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapRazorPages();

app.Run();
