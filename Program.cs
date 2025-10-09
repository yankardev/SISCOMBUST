using SISCOMBUST.Data;
using Microsoft.EntityFrameworkCore;
using SISCOMBUST.Filters;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Conexión a la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

// 🔹 Agregar controladores y filtro global
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<SessionAuthorizeAttribute>(); // Requiere login para todo
});

// 🔹 Agregar soporte para sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // ⏳ Duración de sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 🔹 Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔹 Activar sesión ANTES de autorización
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
