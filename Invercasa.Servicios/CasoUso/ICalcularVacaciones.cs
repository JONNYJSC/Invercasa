using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.Servicios.CasoUso
{
    public interface ICalcularVacaciones
    {
        decimal Calcular(DateTime fechaInicio, DateTime fechaFin);
    }
}
