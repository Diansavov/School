using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestIgnatov.Data;
using TestIgnatov.Models;
using TheSchizoGamblers.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProductsDbContext>(options =>
 options.UseMySql(builder.Configuration.GetConnectionString("ProductConString"), new MySqlServerVersion(new Version(10, 0, 1))));

builder.Services.AddDefaultIdentity<Users>(options => options.SignIn.RequireConfirmedAccount = false)
.AddRoles<IdentityRole>().AddEntityFrameworkStores<ProductsDbContext>();

var app = builder.Build();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

RolesSeed.Seed(app);

app.Run();
