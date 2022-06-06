using Invercasa.AccesoDatos.Configuraciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invercasa.AccesoDatos.Conexion
{
    public class AdministradorConexiones
    {
        private readonly SqlConnection Conexion;
        private readonly BaseDatosConfiguracion _baseDatosConfiguracion;

        public AdministradorConexiones(BaseDatosConfiguracion baseDatosConfiguracion)
        {
            _baseDatosConfiguracion = baseDatosConfiguracion;
            Conexion = new SqlConnection(_baseDatosConfiguracion.CadenaConexion);
        }

        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
    }
}
