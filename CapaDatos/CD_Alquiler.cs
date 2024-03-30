using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Alquiler
    {
        string Mensaje = string.Empty;
        string Conexion = BD_Conexion.ConectarBD();

        public DataSet Mostrar(int IdUsuario, int Fila, int Indice)
        {
            DataSet Datos = new DataSet();
            
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_ALQUILER_MOSTRAR", CON))
                    {
                        Comando.Parameters.Add("@IdUsuario",SqlDbType.Int).Value = IdUsuario;
                        Comando.Parameters.Add("@Indice", SqlDbType.Int).Value = Indice;
                        Comando.Parameters.Add("@Fila", SqlDbType.Int).Value = Fila;
                        Comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader LeerResultado = Comando.ExecuteReader())
                        {
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());

                            Datos.Tables[0].Load(LeerResultado);
                            Datos.Tables[1].Load(LeerResultado);
                            Datos.Tables[2].Load(LeerResultado);
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

        //PARA MOSTRAR TODOS LOS ALQUILERES EN MI REPORTE

        public DataSet MostrarTodo(int IdUsuario, int Fila, int Indice)
        {
            DataSet Datos = new DataSet();

            using(SqlConnection con = new SqlConnection(Conexion))
            {
                try
                {
                    con.Open();
                    using(SqlCommand Comando = new  SqlCommand("SP_ALQUILER_REPORTE_TODOS", con))
                    {
                        Comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;
                        Comando.Parameters.Add("@Indice", SqlDbType.Int).Value = Indice;
                        Comando.Parameters.Add("@Fila", SqlDbType.Int).Value = Fila;
                        Comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader LeerResultado = Comando.ExecuteReader())
                        {
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables.Add(new DataTable());

                            Datos.Tables[0].Load(LeerResultado);
                            Datos.Tables[1].Load(LeerResultado);
                            Datos.Tables[2].Load(LeerResultado);
                            Datos.Tables[3].Load(LeerResultado);
                            Datos.Tables[4].Load(LeerResultado);
                            Datos.Tables[5].Load(LeerResultado);
                            Datos.Tables[6].Load(LeerResultado);
                            Datos.Tables[7].Load(LeerResultado);
                            Datos.Tables[8].Load(LeerResultado);
                            Datos.Tables[9].Load(LeerResultado);
                            Datos.Tables[10].Load(LeerResultado);
                        }

                    }
                }
                catch(Exception ex)
                {
                    throw new ArgumentException(ex.Message.ToString());
                }
                finally
                {
                    if(con != null)
                    {
                        con.Close();
                    }
                }

                return Datos;
            }
        }

        public string Insertar(CE_Alquiler Objeto)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                Mensaje = string.Empty;
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_ALQUILERA_INSERTAR", CON))
                    {
                        Comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Objeto.IdUsuario;
                        Comando.Parameters.Add("@IdBicicleta", SqlDbType.Int).Value = Objeto.IdBicicletas;
                        Comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = Objeto.IdCliente;
                        Comando.Parameters.Add("@IdPrecioAlquiler", SqlDbType.Int).Value = Objeto.IdPrecioAlquiler;
                        Comando.Parameters.Add("@MontoPago", SqlDbType.Decimal).Value = Objeto.MontoPago;
                        Comando.Parameters.Add("@MontoCambio", SqlDbType.Decimal).Value = Objeto.MontoCambio;
                        Comando.Parameters.Add("@MontoAlquiler", SqlDbType.Decimal).Value = Objeto.MontoAlquiler;
                        Comando.Parameters.Add("@Observacion", SqlDbType.VarChar).Value = Objeto.Observacion;
                        Comando.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = Objeto.FechaFin;
                        Comando.Parameters.Add("@FechaProloga", SqlDbType.DateTime).Value = Objeto.FechaProloga;
                        Comando.Parameters.Add("@Tiempo", SqlDbType.Int).Value = Objeto.Tiempo;
                        Comando.Parameters.Add("@Prologa", SqlDbType.Int).Value = Objeto.Prologa;
                        SqlParameter Parametro = new SqlParameter();
                        Parametro.ParameterName = "@Mensaje";
                        Parametro.SqlDbType = SqlDbType.VarChar;
                        Parametro.Size = 500;
                        Parametro.Direction = ParameterDirection.Output;
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.Parameters.Add(Parametro);
                        Comando.ExecuteNonQuery();

                        Mensaje = Parametro.Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = "Error en la Programacion" + ex.Message.ToString();
                }
                finally
                {
                    if (CON != null)
                    {
                        CON.Close();
                    }
                }
                return Mensaje;
            }
        }

        public string Editar(CE_Alquiler Objeto)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                Mensaje = string.Empty;
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_ALQUILER_EDITAR", CON))
                    {
                        Comando.Parameters.Add("@IdAlquiler", SqlDbType.Int).Value = Objeto.IdAlquiler;
                        Comando.Parameters.Add("@IdTiempoProloga", SqlDbType.Int).Value = Objeto.Prologa;
                        Comando.Parameters.Add("@FechaProloga", SqlDbType.DateTime).Value = Objeto.FechaProloga;
                        Comando.Parameters.Add("@IdBicicleta", SqlDbType.Int).Value = Objeto.IdBicicletas;
                        Comando.Parameters.Add("@Observacion", SqlDbType.VarChar).Value = Objeto.Observacion;
                        SqlParameter Parametro = new SqlParameter();
                        Parametro.ParameterName = "@Mensaje";
                        Parametro.SqlDbType = SqlDbType.VarChar;
                        Parametro.Size = 1000;
                        Parametro.Direction = ParameterDirection.Output;
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.Parameters.Add(Parametro);
                        Comando.ExecuteNonQuery();

                        Mensaje = Parametro.Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = "Error en la Programacion" + ex.Message.ToString();
                }
                finally
                {
                    if (CON != null)
                    {
                        CON.Close();
                    }
                }
                return Mensaje;
            }
        }

        public string CirreAlquiler(CE_Alquiler Objeto)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                Mensaje = string.Empty;
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_ALQUILER_CIERRE", CON))
                    {
                        Comando.Parameters.Add("@IdAlquiler", SqlDbType.Int).Value = Objeto.IdAlquiler;
                        Comando.Parameters.Add("@IdBicicleta", SqlDbType.Int).Value = Objeto.IdBicicletas;
                        SqlParameter Parametro = new SqlParameter();
                        Parametro.ParameterName = "@Mensaje";
                        Parametro.SqlDbType = SqlDbType.VarChar;
                        Parametro.Size = 500;
                        Parametro.Direction = ParameterDirection.Output;
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.Parameters.Add(Parametro);
                        Comando.ExecuteNonQuery();

                        Mensaje = Parametro.Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = "Error en la Programacion" + ex.Message.ToString();
                }
                finally
                {
                    if (CON != null)
                    {
                        CON.Close();
                    }
                }
                return Mensaje;
            }
        }

        public string AnularAlquiler(int IdAlquiler)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                Mensaje = string.Empty;
                try
                {
                    CON.Open();
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("DECLARE @IdBicicleta INT = (SELECT idbicicleta FROM ALQUILERES WHERE idalquiler = @IdAlquiler)");
                    Consulta.AppendLine("UPDATE ALQUILERES SET estado = 'ANULADOS' WHERE idalquiler = @IdAlquiler");
                    Consulta.AppendLine("UPDATE BICICLETAS SET estado = 'DISPONIBLE' WHERE idbicicleta = @IdBicicleta");
                    
                    using (SqlCommand Comando = new SqlCommand(Consulta.ToString(), CON))
                    {
                        Comando.Parameters.Add("@IdAlquiler", SqlDbType.Int).Value = IdAlquiler;
                        Comando.CommandType = CommandType.Text;
                        Mensaje = Comando.ExecuteNonQuery() == 2 ? "OK" : "Ocurrio un Error al Anular";
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = "Error en la Programacion" + ex.Message.ToString();
                }
                finally
                {
                    if (CON != null)
                    {
                        CON.Close();
                    }
                }
                return Mensaje;
            }
        }

        public byte[] FotoBicicleta(int IdBicicleta)
        {
            byte[] Imagen = null;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SELECT foto FROM BICICLETAS  WHERE idbicicleta = @IdBicicleta", CON))
                    {
                        Comando.Parameters.Add("@IdBicicleta", SqlDbType.Int).Value = IdBicicleta;
                        Comando.CommandType = CommandType.Text;
                        using (SqlDataReader LeerResultado = Comando.ExecuteReader())
                        {
                            while (LeerResultado.Read())
                            {
                                Imagen = LeerResultado["foto"] != DBNull.Value ? (byte[])(LeerResultado["foto"]) : new byte[0];
                            }
                            
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
                return Imagen;
            }
        }
    }
}
