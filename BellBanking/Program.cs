using BellBankingApp.Infrastructure.Identity;
using BellBankingApp.Infrastructure.Persistence;
using BellBankingApp.Core.Application;
using BellBankingApp.Infrastructure.Identity.Entities;
using BellBankingApp.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using BellBanking.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Authentication/Authorization DI
builder.Services.AddIdentityInfrastructure(builder.Configuration);

// Infrastructure
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

//Application DI
builder.Services.AddApplicationLayer();

//Presentation DI
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
        await DefaultCustomerUser.SeedAsync(userManager, roleManager);

    }
    catch (Exception ex)
    {

    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
