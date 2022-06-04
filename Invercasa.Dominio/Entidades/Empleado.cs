using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.Dominio.Entidades
{
    public class Empleado
    {
        public Empleado(string nombre, string tipoIdentificacion, string numeroIdentificacion)
        {
            Nombre = nombre;
            TipoIdentificacion = tipoIdentificacion;
            NumeroIdentificacion = numeroIdentificacion;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal SalarioBaseMensual { get; set; }
    }
}
