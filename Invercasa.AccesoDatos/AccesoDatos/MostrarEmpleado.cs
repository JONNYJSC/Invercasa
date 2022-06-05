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
    public class MostrarEmpleado : IMostrarEmpleado
    {
        private readonly AdministradorConexiones conexion = new();
        private readonly SqlCommand comando = new();
        DataTable dt = new();

        public List<Empleado> Mostrar()
        {
            List<Empleado> lst = new();
            string query = "dbo.SP_SEL_Empleado";

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = query;
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new(comando);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Empleado emp = new();
                emp.Id = Convert.ToInt32(dr["IdEmpleado"]);
                emp.Nombre = Convert.ToString(dr["Nombre"])!;
                emp.TipoIdentificacion = Convert.ToString(dr["TipoIdentificacion"])!;
                emp.NumeroIdentificacion = Convert.ToString(dr["NumeroIdentificacion"])!;
                emp.FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]);
                emp.Direccion = Convert.ToString(dr["Direccion"])!;
                lst.Add(emp);
            }

            conexion.CerrarConexion();

            return lst;
        }
    }
}
