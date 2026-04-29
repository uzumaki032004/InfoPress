using InfoPress.Interfaces;
using InfoPress.Services;
using InfoPress.Proxy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// PROXY DI: Înregistrare condiționată a Proxy-ului
builder.Services.AddScoped<NewsService>();
builder.Services.AddScoped<INewsService>(sp => {
    var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
    var realService = sp.GetRequiredService<NewsService>();
    
    // Verificăm rolul din query string pentru demonstrație: ?role=Admin
    string role = httpContext?.Request.Query["role"].ToString() ?? "Guest";
    
    return new NewsAccessProxy(realService, role);
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");

app.Run();
