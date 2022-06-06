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
        private readonly AdministradorConexiones conexion;
        private readonly SqlCommand comando = new SqlCommand();

        public MostrarEmpleado(AdministradorConexiones conexion)
        {
            this.conexion = conexion;
        }

        public List<Empleado> Mostrar()
        {
            List<Empleado> empleados = new List<Empleado>();

            string query = "dbo.SP_SEL_Empleado";

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = query;
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            conexion.CerrarConexion();

            Empleado empleado;
            foreach (DataRow row in dataTable.Rows)
            {
                empleado = new Empleado();
                empleado.Id = Convert.ToInt32(row["IdEmpleado"]);
                empleado.Nombre = Convert.ToString(row["Nombre"])!;
                empleado.TipoIdentificacion = Convert.ToString(row["TipoIdentificacion"])!;
                empleado.NumeroIdentificacion = Convert.ToString(row["NumeroIdentificacion"])!;
                empleado.FechaIngreso = Convert.ToDateTime(row["FechaIngreso"]);
                empleado.SalarioBaseMensual = Convert.ToInt32(row["SalarioBaseMensual"]);
                empleado.Direccion = Convert.ToString(row["Direccion"])!;
                empleados.Add(empleado);
            }

            return empleados;
        }
    }
}
