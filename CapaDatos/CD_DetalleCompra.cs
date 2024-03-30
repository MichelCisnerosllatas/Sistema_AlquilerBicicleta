using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_DetalleCompra
    {
        string Conexion = BD_Conexion.ConectarBD();
        string Mensaje;

        public string InsertarDetalle(CE_DetalleCompra objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("INSERT INTO DETALLE_COMPRA(IdCompra,IdRepuesto,Cantidad,SubTotal)");
                    sb.AppendLine("VALUES(@IdCompra,@IdRepuesto,@Cantidad,@SubTotal)");

                    SqlCommand Comando = new SqlCommand(sb.ToString(),CON);
                    Comando.Parameters.Add("@IdCompra", System.Data.SqlDbType.Int).Value = objeto.IdCompra;
                    Comando.Parameters.Add("@IdRepuesto", System.Data.SqlDbType.Int).Value = objeto.IdRepuesto;
                    Comando.Parameters.Add("@Cantidad", System.Data.SqlDbType.Int).Value = objeto.Cantidad;
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

        public DataSet SP_COMPRA_DETALLE(int Fila, int Indice, int IdCompra)
        {
            SqlDataReader resultado;
            DataSet datos = new DataSet();

            using (SqlConnection con = new SqlConnection(Conexion))
            {
                using (SqlCommand comando = new SqlCommand("SP_COMPRA_DETALLE", con))
                {
                    try
                    {
                        con.Open();
                        comando.Parameters.Add("@Filas", SqlDbType.Int).Value = Fila;
                        comando.Parameters.Add("@Paginas", SqlDbType.Int).Value = Indice;
                        comando.Parameters.Add("@IdCompra", SqlDbType.Int).Value = Indice;
                        comando.CommandType = CommandType.StoredProcedure;

                        using (resultado = comando.ExecuteReader())
                        {
                            datos.Tables.Add(new DataTable());
                            datos.Tables.Add(new DataTable());

                            datos.Tables[0].Load(resultado);
                            datos.Tables[1].Load(resultado);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("NO SE PUDO LEER" + ex.Message.ToString());
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }

                    return datos;
                }
            }
        }
    }
}
