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
        private readonly AdministradorConexiones conexion;
        private readonly SqlCommand comando = new SqlCommand();
        private readonly SqlCommand comandoFn = new SqlCommand();

        public CrearEmpleado(AdministradorConexiones conexion)
        {
            this.conexion = conexion;
        }

        public void Registrar(Empleado empleado)
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
            comando.Parameters.Clear();
            comando.Parameters.AddRange(parameters.ToArray());
            comando.CommandType = CommandType.StoredProcedure;

            comando.ExecuteNonQuery();
            conexion.CerrarConexion();

        }

        public string ValidarCedula(string numeroIdentificacion)
        {
            string query = "SELECT dbo.FnValidarCedula(@NumeroIdentificacion);";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@NumeroIdentificacion",
                    SqlDbType = SqlDbType.VarChar,
                    Value = numeroIdentificacion
                },
            };

            comandoFn.Connection = conexion.AbrirConexion();
            comandoFn.CommandText = query;
            comandoFn.Parameters.Clear();
            comandoFn.Parameters.AddRange(parameters.ToArray());

            string functionResult = (string)comandoFn.ExecuteScalar();

            comandoFn.ExecuteNonQuery();

            conexion.CerrarConexion();

            return functionResult;
        }
    }
}
