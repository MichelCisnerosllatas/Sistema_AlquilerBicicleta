using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_MostrarRol
    {
        string Conexion = BD_Conexion.ConectarBD();

        public DataSet MostrarRol()
        {
            DataSet Datos = new DataSet();            
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand comando = new SqlCommand("SELECT idrol, rol FROM ROL WHERE estado = 'DISPONIBLE'", CON))
                    {
                        comando.CommandType = CommandType.Text;
                        using (SqlDataReader Leer = comando.ExecuteReader())
                        {
                            DataTable Tabla = new DataTable();
                            Tabla.Load(Leer);
                            Datos.Tables.Add(Tabla);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException(ex.Message.ToString());
                }
                finally
                {
                    if (CON.State == ConnectionState.Open)
                    {
                        CON.Close();
                    }
                }
                return Datos;
            }
        }
    }
}
