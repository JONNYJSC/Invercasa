using Invercasa.AccesoDatos.Conexion;
using Invercasa.Servicios.CasoUso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.AccesoDatos.AccesoDatos
{
    public class CalcularVacaciones : ICalcularVacaciones
    {
        private readonly AdministradorConexiones conexion;
        private readonly SqlCommand comando = new SqlCommand();

        public CalcularVacaciones(AdministradorConexiones conexion)
        {
            this.conexion = conexion;
        }

        public decimal Calcular(DateTime fechaInicio, DateTime fechaFin)
        {
            string query = "SELECT [dbo].FnCalcularVacaciones(@fechaInicial, @fechaFinal);";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@fechaInicial",
                    SqlDbType = SqlDbType.Date,
                    Value = fechaInicio
                },
                new SqlParameter()
                {
                    ParameterName = "@fechaFinal",
                    SqlDbType = SqlDbType.Date,
                    Value = fechaFin
                },
            };

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = query;
            comando.Parameters.Clear();
            comando.Parameters.AddRange(parameters.ToArray());

            decimal functionResult = (decimal)comando.ExecuteScalar();
            conexion.CerrarConexion();

            return functionResult;
        }
    }
}
