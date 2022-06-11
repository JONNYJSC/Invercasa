using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.Dominio.Entidades
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string TipoIdentificacion { get; set; } = null!;
        public string NumeroIdentificacion { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }
        public decimal SalarioBaseMensual { get; set; }
        public string Direccion { get; set; } = null!;

        //Recibe el valor de la funcion FnValidarCedula (AccesoDatos)
        public string ValidarNIdentidad = null!;
    }
}
