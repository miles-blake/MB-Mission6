using Microsoft.EntityFrameworkCore;
using Mission06_Blake.Models;

var builder = WebApplication.CreateBuilder(args);

// **🔹 Add Database Context (EF Core)**
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MovieDbConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// **🔹 Ensure the Database is Created on Startup**
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MovieContext>();

    context.Database.EnsureCreated(); // **Create DB if it doesn't exist**
}

app.Run();