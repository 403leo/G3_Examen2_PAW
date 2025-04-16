using Microsoft.EntityFrameworkCore;
using PAWMartes.Models;
using PAWMartes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PAWContext>(op =>
{ 
    op.UseSqlServer(builder.Configuration.GetConnectionString("PAW"),
		// PARA PODER HACER MIGRACIONES DESDE EL PAWMARTES
		x => x.MigrationsAssembly("PAWMartes"));
    
});

// Tipos de servicios
builder.Services.AddTransient<ITrasientServices, TrasientServices>();
builder.Services.AddScoped<IScopedServices, ScopedServices>();
builder.Services.AddSingleton<ISingeltonServices, SingeltonServices>();

builder.Services.AddScoped<IEventosServices, EventoServices>();

// Se agrega el servicio de carrito en donde se pone singleton para que me mantenga la instancia y el listado no lo pierda
//builder.Services.AddSingleton<ICarritoServices, CarritoServices>();

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

app.Run();
