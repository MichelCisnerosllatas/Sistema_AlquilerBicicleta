using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_TipoDoc
    {
        string Conexion = BD_Conexion.ConectarBD();

        public DataSet MostrarTipoDoc()
        {
            DataSet Datos = new DataSet();            

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand comando = new SqlCommand("SELECT idtipodoc, tipo FROM TIPODOC WHERE estado = 'DISPONIBLE'",CON))
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
                catch(Exception ex)
                {
                    throw new ArgumentNullException(ex.Message.ToString());
                }
                finally
                {
                    if(CON.State == ConnectionState.Open)
                    {
                        CON.Close();
                    }
                }
                return Datos;
            }
        }
    }
}
