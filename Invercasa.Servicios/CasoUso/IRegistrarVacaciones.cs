using Invercasa.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.Servicios.CasoUso
{
    public interface IRegistrarVacaciones
    {
        bool Registrar(int id, DateTime fechaInicio, DateTime fechaFin);
    }
}
