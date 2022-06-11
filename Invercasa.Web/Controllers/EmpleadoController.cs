using Invercasa.AccesoDatos.AccesoDatos;
using Invercasa.Dominio.Entidades;
using Invercasa.Servicios.CasoUso;
using Invercasa.Servicios.Modelos;
using Invercasa.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Invercasa.Web.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ICalcularVacaciones _calcularVacaciones;
        private readonly ICrearEmpleado _crearEmpleado;
        private readonly IMostrarEmpleado _mostrarEmpleado;
        private readonly IRegistrarVacaciones _registrarVacaciones;
        private readonly IGenerarReporte _generarReporte;

        public EmpleadoController(ICalcularVacaciones calcularVacaciones, ICrearEmpleado crearEmpleado, IMostrarEmpleado mostrarEmpleado, IRegistrarVacaciones registrarVacaciones, IGenerarReporte generarReporte)
        {
            _calcularVacaciones = calcularVacaciones;
            _crearEmpleado = crearEmpleado;
            _mostrarEmpleado = mostrarEmpleado;
            _registrarVacaciones = registrarVacaciones;
            _generarReporte = generarReporte;
        }

        public ActionResult Index()
        {
            var result = _mostrarEmpleado.Mostrar();
            if (result == null)
            {
                return View();
            }
            else
            {
                return View(result);
            }

        }

        // GET: EmpleadoController/Create
        public ActionResult CrearEmpleado()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: EmpleadoController/Create
        public ActionResult CrearEmpleado(Empleado empleado)
        {
            empleado.ValidarNIdentidad = _crearEmpleado.ValidarCedula(empleado.NumeroIdentificacion);

            _crearEmpleado.Registrar(empleado);

            if (empleado.ValidarNIdentidad.Equals("No"))
            {
                ViewBag.Message = empleado.NumeroIdentificacion;
                return View();
            }
            else
                return RedirectToAction(nameof(Index));
        }

        public ActionResult Reporte(int id)
        {
            var result = _generarReporte.Generar(id);
            return View(result);
        }

        public ActionResult CalcularVacaciones()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CalcularVacaciones(DiasVacaciones diasVacaciones)
        {
            var vacaciones = _calcularVacaciones.Calcular(diasVacaciones.FechaInicio, diasVacaciones.FechaFin);
            ViewBag.Message = vacaciones;
            return View();
        }

        public ActionResult RegistrarVacaciones()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarVacaciones(int id, Vacaciones vacaciones)
        {
            var resut = _registrarVacaciones.Registrar(id, vacaciones.FechaInicio, vacaciones.FechaFin);
            return RedirectToAction(nameof(Index));
        }
    }
}
