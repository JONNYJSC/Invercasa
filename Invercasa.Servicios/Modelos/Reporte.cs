using Invercasa.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.Servicios.Modelos
{
    public class Reporte
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }
        public decimal DiasGenerados { get; set; }
        public decimal DiasTomados { get; set; }
        public decimal Saldos => DiasGenerados - DiasTomados;
    }
}
