using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    public class CD_TiempoAlquiler
    {
        string Conexion = BD_Conexion.ConectarBD();
        string Mensaje;
        DataTable Datos = new DataTable();
        
        
        public DataTable MostrarTiempoAlquilerCB()
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    SqlDataReader LeerResultado;
                    CON.Open();
                    string Consulta = "SELECT IdTiempoAlquiler,Minutos as NombreTiempo FROM TIEMPO_ALQUILER WHERE EstadoAlquiler = 'DISPONIBLE'";
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

        public DataSet MostrarTiempoAlquiler(int Fila, int Indeci)
        {
            SqlDataReader resultado;
            DataSet Dt = new DataSet();
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                using (SqlCommand Comando = new SqlCommand("LISTAR_TIEMPOALQUILER", CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.Parameters.Add("@Fila", SqlDbType.Int).Value = Fila;
                        Comando.Parameters.Add("@Indice", SqlDbType.Int).Value = Indeci;
                        Comando.CommandType = CommandType.StoredProcedure; 

                        using (resultado = Comando.ExecuteReader())
                        {
                            Dt.Tables.Add(new DataTable());
                            Dt.Tables.Add(new DataTable());
                            Dt.Tables.Add(new DataTable());
                            Dt.Tables.Add(new DataTable());
                            Dt.Tables.Add(new DataTable());

                            Dt.Tables[0].Load(resultado);
                            Dt.Tables[1].Load(resultado);
                            Dt.Tables[2].Load(resultado);
                            Dt.Tables[3].Load(resultado);
                            Dt.Tables[4].Load(resultado);
                        }
                        
                    }
                    catch (Exception error)
                    {
                        throw new ArgumentException("NO SE PUDO leer " + error.Message.ToString());
                    }
                    finally
                    {
                        if (CON.State == ConnectionState.Open)
                        {
                            CON.Close();
                        }
                    }

                    return Dt;
                }
            }
        }

        public string InsertarTiempoAlquiler(CapaEntidad.CE_TiempAlquiler objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("TIEMPOALQUILER_INSERTAR", CON))
                    {
                        Comando.Parameters.Add("@Hora", SqlDbType.VarChar, 50).Value = objeto.Hora;
                        Comando.Parameters.Add("@Minutos", SqlDbType.VarChar, 50).Value = objeto.Minutos;
                        Comando.Parameters.Add("@Monto", SqlDbType.Decimal).Value = objeto.Monto;
                        SqlParameter Parametro = new SqlParameter();
                        Parametro.ParameterName = "@Mensaje";
                        Parametro.SqlDbType = SqlDbType.VarChar;
                        Parametro.Size = 500;
                        Parametro.Direction = ParameterDirection.Output;
                        Comando.Parameters.Add(Parametro);
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.ExecuteNonQuery();

                        Mensaje = Parametro.Value.ToString();

                    }
                }
                catch (Exception ex)
                {
                    Mensaje = ex.Message;
                }
                finally
                {
                    if (CON.State == ConnectionState.Open)
                    {
                        CON.Close();
                    }
                }
                return Mensaje;
            }
        }

        public string ActualizarTiempoAlquiler(CapaEntidad.CE_TiempAlquiler objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("TIEMPOALQUILER_EDITAR", CON))
                    {
                        Comando.Parameters.Add("@IdTiempoAlquiler", SqlDbType.Int).Value = objeto.IdTiempoAlquiler;
                        Comando.Parameters.Add("@Hora", SqlDbType.VarChar, 50).Value = objeto.Hora;
                        Comando.Parameters.Add("@Minutos", SqlDbType.VarChar, 50).Value = objeto.Minutos;
                        Comando.Parameters.Add("@Monto", SqlDbType.Decimal).Value = objeto.Monto;
                        Comando.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Value = objeto.EstadoTiempoAlquiler;

                        SqlParameter Parametro = new SqlParameter();
                        Parametro.ParameterName = "@mensaje";
                        Parametro.SqlDbType = SqlDbType.VarChar;
                        Parametro.Size = 500;
                        Parametro.Direction = ParameterDirection.Output;
                        Comando.Parameters.Add(Parametro);
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.ExecuteNonQuery();

                        Mensaje = Parametro.Value.ToString();
                        //Comando.CommandType = CommandType.StoredProcedure;
                        //Mensaje = Comando.ExecuteNonQuery() != 1 ? "ERROR" : "OK";

                    }
                }
                catch (Exception ex)
                {
                    Mensaje = ex.Message;
                }
                finally
                {
                    if (CON.State == ConnectionState.Open)
                    {
                        CON.Close();
                    }
                }
                return Mensaje;
            }
        }

        public string EliminarTiempoAlquiler(int IdTiempoAlquiler)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("TIEMPOALQUILER_ELIMINAR", CON))
                    {
                        Comando.Parameters.Add("@IdTiempoAlquiler", SqlDbType.Int).Value = IdTiempoAlquiler;
                        SqlParameter Parametro = new SqlParameter();
                        Parametro.ParameterName = "@Mensaje";
                        Parametro.SqlDbType = SqlDbType.VarChar;
                        Parametro.Size = 100;
                        Parametro.Direction = ParameterDirection.Output;
                        Comando.Parameters.Add(Parametro);
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.ExecuteNonQuery();

                        Mensaje = Parametro.Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = ex.Message;
                }
                finally
                {
                    if (CON.State == ConnectionState.Open)
                    {
                        CON.Close();
                    }
                }
                return Mensaje;
            }
        }

    }
}
