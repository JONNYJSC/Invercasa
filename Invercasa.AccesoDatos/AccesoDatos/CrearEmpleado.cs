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
    public class CrearEmpleado : ICrearEmpleado
    {
        private readonly AdministradorConexiones conexion = new();
        private readonly SqlCommand comando = new();

        public bool Registrar(Empleado empleado)
        {
            string query = @"dbo.SP_INS_Empleado";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@Nombre",
                    SqlDbType = SqlDbType.VarChar,
                    Value = empleado.Nombre
                },
                new SqlParameter()
                {
                    ParameterName = "@TipoIdentificacion",
                    SqlDbType = SqlDbType.VarChar,
                    Value = empleado.TipoIdentificacion
                },
                new SqlParameter()
                {
                    ParameterName = "@NumeroIdentificacion",
                    SqlDbType = SqlDbType.VarChar,
                    Value = empleado.NumeroIdentificacion
                },
                new SqlParameter()
                {
                    ParameterName = "@FechaIngreso",
                    SqlDbType = SqlDbType.DateTime,
                    Value = empleado.FechaIngreso
                },
                new SqlParameter()
                {
                    ParameterName = "@SalarioBaseMensual",
                    SqlDbType = SqlDbType.Decimal,
                    Value = empleado.SalarioBaseMensual
                },
                new SqlParameter()
                {
                    ParameterName = "@Direccion",
                    SqlDbType = SqlDbType.VarChar,
                    Value = empleado.Direccion
                },

            };

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = query;
            comando.Parameters.AddRange(parameters.ToArray());
            comando.CommandType = CommandType.StoredProcedure;

            var result = comando.ExecuteNonQuery();
            conexion.CerrarConexion();

            return false;
        }
    }
}
