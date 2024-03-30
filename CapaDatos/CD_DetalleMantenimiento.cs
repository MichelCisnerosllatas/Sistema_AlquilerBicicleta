using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_DetalleMantenimiento
    {
        string Conexion = BD_Conexion.ConectarBD();
        string Mensaje;

        public string InsertarDetalleMantenimiento(CE_DetalleMantenimiento objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("INSERT INTO DETALLE_MANTENIMIENTO(IdMantenimiento,IdRepuesto,SubTotal)");
                    sb.AppendLine("VALUES (@IdMantenimiento,@IdRepuesto,@SubTotal))");

                    SqlCommand Comando = new SqlCommand(sb.ToString(), CON);
                    Comando.Parameters.Add("@IdMantenimiento", System.Data.SqlDbType.Int).Value = objeto.IdMantenimiento;
                    Comando.Parameters.Add("@IdRepuesto", System.Data.SqlDbType.Int).Value = objeto.IdRepuesto; 
                    Comando.Parameters.Add("@SubTotal", System.Data.SqlDbType.Decimal).Value = objeto.SubTotal;
                    Mensaje = Comando.ExecuteNonQuery() == 1 ? "OK" : "Ocurrio un Error";

                }
                catch (Exception ex)
                {
                    Mensaje += ex.Message;
                }
                finally { CON.Close(); }
                return Mensaje;
            }
        }


    }
}
