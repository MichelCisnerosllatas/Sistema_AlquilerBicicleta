using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Compra
    {
        string Conexion = BD_Conexion.ConectarBD();
        string Mensaje;
        DataTable Datos = new DataTable();

        public int NumeroCorrelativo()
        {
            int datos = 0;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    string Consulta = "SELECT COUNT(*) + 1 FROM COMPRA";
                    using (SqlCommand Comando = new SqlCommand(Consulta.ToString(), CON))
                    {
                        Comando.CommandType = CommandType.Text;
                        datos = Convert.ToInt32(Comando.ExecuteScalar());
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
                return datos;
            }
        }

        public DataSet Mostrar_Compra(int IdUsuario, int Fila, int Indice)
        {
            DataSet Datos = new DataSet();
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_COMPRA_MOSTRAR", CON))
                    {
                        Comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;
                        Comando.Parameters.Add("@Indice", SqlDbType.Int).Value = Indice;
                        Comando.Parameters.Add("@Fila", SqlDbType.Int).Value = Fila;
                        Comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader LeerResultado = Comando.ExecuteReader())
                        {
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());

                            Datos.Tables[0].Load(LeerResultado);
                            Datos.Tables[1].Load(LeerResultado);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message.ToString());
                }
                finally
                {
                    if (CON != null)
                    {
                        CON.Close();
                    }
                }
                return Datos;
            }
        }

        public int InsertarCompra(CE_Compra Objeto)
        {
            int IdCompra = 0;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_COMPRA_INSERTAR", CON))
                    {
                        Comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Objeto.IdUsuario;
                        Comando.Parameters.Add("@IdProveedor", SqlDbType.Int).Value = Objeto.IdProveedor;
                        Comando.Parameters.Add("@IdTipoComprobante", SqlDbType.Int).Value = Objeto.IdTipoComprobante;
                        Comando.Parameters.Add("@Serie", SqlDbType.VarChar).Value = Objeto.Serie;
                        Comando.Parameters.Add("@NumComprobante", SqlDbType.Int).Value = Objeto.NumComprobante;
                        Comando.Parameters.Add("@Correlativo", SqlDbType.Int).Value = Objeto.Correlativo;
                        Comando.Parameters.Add("@Importe", SqlDbType.Decimal).Value = Objeto.Importe;
                        Comando.Parameters.Add("@Igv", SqlDbType.Decimal).Value = Objeto.IGV;
                        Comando.Parameters.Add("@MontoTotal ", SqlDbType.Decimal).Value = Objeto.MontoTotal;
                        SqlParameter parametro = new SqlParameter();
                        parametro.ParameterName = "@IdCompra";
                        parametro.SqlDbType = SqlDbType.Int;
                        parametro.Direction = ParameterDirection.Output;
                        Comando.Parameters.Add(parametro);
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.ExecuteNonQuery();
                        IdCompra = Convert.ToInt32(parametro.Value);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message.ToString());
                }
                finally
                {
                    if (CON != null)
                    {
                        CON.Close();
                    }
                }
                return IdCompra;
            }
        }
    }
}
