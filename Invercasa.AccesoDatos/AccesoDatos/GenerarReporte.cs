using Invercasa.AccesoDatos.Conexion;
using Invercasa.Servicios.CasoUso;
using Invercasa.Servicios.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.AccesoDatos.AccesoDatos
{
    public class GenerarReporte : IGenerarReporte
    {
        private readonly AdministradorConexiones conexion;
        private readonly SqlCommand comando = new SqlCommand();

        public GenerarReporte(AdministradorConexiones conexion)
        {
            this.conexion = conexion;
        }

        public Reporte Generar(int idEmpleado)
        {
            string query = "dbo.SP_ReporteVacaciones";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@IdEmpleado",
                    SqlDbType = SqlDbType.Int,
                    Value = idEmpleado
                },

            };

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = query;
            comando.Parameters.Clear();
            comando.Parameters.AddRange(parameters.ToArray());
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            conexion.CerrarConexion();

            Reporte reporte = new Reporte();
            foreach (DataRow row in dataTable.Rows)
            {
                reporte.IdEmpleado = Convert.ToInt32(row["IdEmpleado"]);
                reporte.Nombre = Convert.ToString(row["Nombre"])!;
                reporte.FechaIngreso = Convert.ToDateTime(row["FechaIngreso"]);
                reporte.DiasTomados = Convert.ToDecimal(row["DiasTomados"])!;
                reporte.DiasGenerados = Convert.ToDecimal(row["DiasGenerados"])!;
                reporte.Saldos.ToString();
            }

            return reporte;
        }
    }
}
