using Invercasa.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.Servicios.Modelos
{
    public class Reporte
    {
        public Empleado Empleado { get; set; } = null!;
        public decimal DiasGenerados { get; set; }
        public decimal DiasTomados { get; set; }
        public decimal Saldos => DiasGenerados - DiasTomados;
    }
}
