using Microsoft.EntityFrameworkCore;
using Mission06_Blake.Models;

var builder = WebApplication.CreateBuilder(args);

// 1) Register your database context
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MovieDbConnection")));

// 2) Register MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3) Run migrations and then log the DB path + categories
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MovieContext>();
    context.Database.Migrate();

    // Get a logger to print info
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    // (A) Log the actual path to the SQLite file
    var dbPath = context.Database.GetDbConnection().DataSource;
    logger.LogInformation("EF is actually pointing to the database at path: {dbPath}", dbPath);

    // (B) Log all categories in that DB
    var allCategories = context.Categories.ToList();
    if (allCategories.Any())
    {
        logger.LogInformation("Categories in DB: {cats}", 
            string.Join(", ", allCategories.Select(c => $"{c.CategoryId}={c.CategoryName}")));
    }
    else
    {
        logger.LogWarning("No categories found in the database!");
    }
}

// 4) Usual middleware pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();