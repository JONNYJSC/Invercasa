using Invercasa.Servicios.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.Servicios.CasoUso
{
    public interface IGenerarReporte
    {
        Reporte Generar(int idEmpleado);
    }
}
