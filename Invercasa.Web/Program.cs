using Invercasa.AccesoDatos.AccesoDatos;
using Invercasa.AccesoDatos.Conexion;
using Invercasa.AccesoDatos.Configuraciones;
using Invercasa.Servicios.CasoUso;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var conexionString = builder.Configuration.GetConnectionString("DefaultConnection");
var baseDatosConfiuracion = new BaseDatosConfiguracion()
{
    CadenaConexion = conexionString
};
var conexion = new AdministradorConexiones(baseDatosConfiuracion);
builder.Services.AddSingleton(conexion);
builder.Services.AddSingleton<ICalcularVacaciones, CalcularVacaciones>();
builder.Services.AddSingleton<ICrearEmpleado, CrearEmpleado>();
builder.Services.AddSingleton<IMostrarEmpleado, MostrarEmpleado>();
builder.Services.AddSingleton<IRegistrarVacaciones, RegistrarVacaciones>();
builder.Services.AddSingleton<IGenerarReporte, GenerarReporte>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Empleado}/{action=Index}/{id?}");

app.Run();
