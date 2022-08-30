using Invercasa.AccesoDatos.Conexion;
using Invercasa.Dominio.Entidades;
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
    public class RegistrarVacaciones : IRegistrarVacaciones
    {
        private readonly AdministradorConexiones conexion;
        private readonly SqlCommand comando = new SqlCommand();

        public RegistrarVacaciones(AdministradorConexiones conexion)
        {
            this.conexion = conexion;
        }

        public bool Registrar(int id, DateTime fechaInicio, DateTime fechaFin)
        {

            string query = "dbo.SP_INS_Vacaciones";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@IdEmplado",
                    SqlDbType = SqlDbType.Int,
                    Value = id
                },
                new SqlParameter()
                {
                    ParameterName = "@FechaInicio",
                    SqlDbType = SqlDbType.Date,
                    Value = fechaInicio
                },
                new SqlParameter()
                {
                    ParameterName = "@FechaFin",
                    SqlDbType = SqlDbType.Date,
                    Value = fechaFin
                },
            };

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = query;
            comando.Parameters.Clear();
            comando.Parameters.AddRange(parameters.ToArray());
            comando.CommandType = CommandType.StoredProcedure;

            comando.ExecuteNonQuery();
            conexion.CerrarConexion();

            return false;
        }
    }
}
