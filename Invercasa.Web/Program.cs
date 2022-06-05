using Invercasa.AccesoDatos.AccesoDatos;
using Invercasa.Servicios.CasoUso;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICalcularVacaciones, CalcularVacaciones>();
builder.Services.AddSingleton<ICrearEmpleado, CrearEmpleado>();
builder.Services.AddSingleton<IMostrarEmpleado, MostrarEmpleado>();
builder.Services.AddSingleton<IRegistrarVacaciones, RegistrarVacaciones>();

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
    pattern: "{controller=Empleado}/{action=Index}/{id?}");

app.Run();
