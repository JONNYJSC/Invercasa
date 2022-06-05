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
        private readonly AdministradorConexiones conexion = new();
        private readonly SqlCommand comando = new();
        DataTable dt = new();
        public bool Registrar(int id, DateTime fechaInicio, DateTime fechaFin)
        {
            Vacaciones v = new();
            v.IdEmpleado = id;
            v.FechaInicio = fechaInicio;
            v.FechaFin = fechaFin;

            string query = "dbo.SP_INS_Vacaciones";

            var parameters = new List<SqlParameter>()
            {   
                new SqlParameter()
                {
                    ParameterName = "@IdEmplado",
                    SqlDbType = SqlDbType.Int,
                    Value = v.IdEmpleado
                },
                new SqlParameter()
                {
                    ParameterName = "@FechaInicio",
                    SqlDbType = SqlDbType.Date,
                    Value = v.FechaInicio  
                },
                new SqlParameter()
                {
                    ParameterName = "@FechaFin",
                    SqlDbType = SqlDbType.Date,
                    Value = v.FechaFin
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


        public List<Empleado> GetAll()
        {

            conexion.AbrirConexion();

            List<Empleado> lst = new();
            string query = @"SELECT * FROM dbo.Empleado";

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = query;
            //comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new(comando);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Empleado emp = new();
                emp.Id = Convert.ToInt32(dr["IdEmpleado"]);
                emp.Nombre = Convert.ToString(dr["Nombre"])!;
                lst.Add(emp);
            }

            conexion.CerrarConexion();

            return lst;

        }
    }
}
