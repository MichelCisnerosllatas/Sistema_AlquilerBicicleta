using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_TipoComprobante
    {
        string Conexion = BD_Conexion.ConectarBD();
        string Mensaje;
        DataTable Datos = new DataTable();

        public DataTable MostrarTipoComprobanteCB()
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    SqlDataReader LeerResultado;
                    CON.Open();
                    string Consulta = "SELECT IdTipoComprobante, Nombre_Comprobante FROM TIPO_COMPROBANTE WHERE Estado_Comprobante = 'DISPONIBLE'";
                    using (SqlCommand Comando = new SqlCommand(Consulta.ToString(), CON))
                    {
                        Comando.CommandType = CommandType.Text;
                        LeerResultado = Comando.ExecuteReader();
                        Datos.Load(LeerResultado);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message.ToString());
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
