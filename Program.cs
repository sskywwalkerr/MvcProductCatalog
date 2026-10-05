using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // регистрация

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") // ConnectionStrings
                       ?? "Data Source=webapplication1.db";

builder.Services.AddDbContext<AppDbContext>(options => // регистрация в di AppDbContext
    options.UseSqlite(connectionString));

var app = builder.Build();

// создать, заполнить
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // получить экземпляр AppDbContext
    db.Database.EnsureCreated();
    DbSeeder.Seed(db);
}

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
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();


