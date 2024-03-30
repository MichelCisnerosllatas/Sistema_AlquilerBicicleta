using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaDatos
{
    public class BD_Conexion
    {
        public static string ConectarBD()
        {
            
            //return ConfigurationManager.ConnectionStrings["conexionMysql"].ConnectionString.ToString();
            return ConfigurationManager.ConnectionStrings["cadena_conexion"].ConnectionString.ToString();
            //return ConfigurationManager.ConnectionStrings["cadena"].ConnectionString.ToString();
        }
    }
}
