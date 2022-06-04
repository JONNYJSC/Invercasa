using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.Dominio.Entidades
{
    public class Vacaciones
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdEmpleado { get; set; }
    }
}
