using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaleInvoicesApp.Data;
using SaleInvoicesApp.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SaleAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr") ?? throw new InvalidOperationException("Connection string 'ConnStr' not found.")));

builder.Services.AddIdentity<SalesStaff, IdentityRole>().AddEntityFrameworkStores<SaleAppDbContext>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<SaleAppDbContext>();
        var userManager = services.GetRequiredService<UserManager<SalesStaff>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //Add roles in Role table.
        await SalesAppSeeds.SeedRolesAsync(userManager, roleManager);

        //Create admin user
        await SalesAppSeeds.AddAdminUserAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}


//var roleManager = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//var userManager = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<UserManager<SalesStaff>>();
//Add roles in database
//await SalesAppSeeds.SeedRolesAsync(userManager, roleManager);
//await SalesAppSeeds.AddAdminUserAsync(userManager, roleManager);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
