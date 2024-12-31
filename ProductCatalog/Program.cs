using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCatalog.DAL.Data;
using ProductCatalog.DAL.Data.Interfaces;
using ProductCatalog.DAL.Data.Repositories;
using ProductCatalog.PL;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductsConnectionString"));
});

builder.Services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ProductDbContext>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddRazorPages();

var app = builder.Build();

using var scoped = app.Services.CreateScope();
var services = scoped.ServiceProvider;
try
{
    var dbContext = services.GetRequiredService<ProductDbContext>();
    await dbContext.Database.MigrateAsync();
    var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await ProductDbSeed.SeedAsync(dbContext, userManager, roleManager);
}
catch
{
    Debug.WriteLine("Error occurred while migrating database");
}

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

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
