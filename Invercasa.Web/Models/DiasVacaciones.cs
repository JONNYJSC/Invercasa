using System.ComponentModel.DataAnnotations;

namespace Invercasa.Web.Models
{
    public class DiasVacaciones
    {
        [Required(ErrorMessage = "El campo Fecha Inicio es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = "El campo Fecha Fin es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }
    }
}
