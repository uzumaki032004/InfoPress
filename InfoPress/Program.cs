using InfoPress.Data;
using InfoPress.Interfaces;
using InfoPress.Services;
using InfoPress.Proxy;
using InfoPress.Repositories;
using InfoPress.Observer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InfoPress.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Baza de date
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=infopress.db"));

builder.Services.AddSingleton<NewsSubject>();

// 2. Identity cu roluri
builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// 3. Repository + Service
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<NewsService>();
builder.Services.AddScoped<INewsService>(sp => {
    var realService = sp.GetRequiredService<NewsService>();
    var httpContext = sp.GetRequiredService<IHttpContextAccessor>()
                       .HttpContext;
    // PROXY: verificare Identity real, nu query string
    string role = httpContext?.User.IsInRole("Admin") == true
                  ? "Admin" : "Guest";
    return new NewsAccessProxy(realService, role);
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 4. Creare baza de date + seed la startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
        SeedData.Initialize(services).Wait();
        
        // Setup initial subscribers
        var subject = services.GetRequiredService<NewsSubject>();
        subject.Subscribe(new UserSubscriber("Admin Monitor"));
        subject.Subscribe(new UserSubscriber("Public Analytics"));
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // OBLIGATORIU înainte de UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");

app.Run();
