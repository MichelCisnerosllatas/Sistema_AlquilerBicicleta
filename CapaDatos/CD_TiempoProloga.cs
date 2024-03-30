using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_TiempoProloga
    {
        string Conexion = BD_Conexion.ConectarBD();
        string Mensaje;
        DataTable Datos = new DataTable();

        public DataSet MostrarTiempoProloga(int Fila, int Indice)
        {
            SqlDataReader resultado;
            DataSet datos = new DataSet();

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                using(SqlCommand comando = new SqlCommand("LISTAR_TIEMPOPROLOGA", CON))
                {
                    try
                    {
                        CON.Open();
                        comando.Parameters.Add("@Fila", SqlDbType.Int).Value = Fila;
                        comando.Parameters.Add("@Indice", SqlDbType.Int).Value = Indice;
                        comando.CommandType = CommandType.StoredProcedure;

                        using (resultado = comando.ExecuteReader())
                        {
                            datos.Tables.Add(new DataTable());
                            datos.Tables.Add(new DataTable());
                            datos.Tables.Add(new DataTable());
                            datos.Tables.Add(new DataTable());

                            datos.Tables[0].Load(resultado);
                            datos.Tables[1].Load(resultado);
                            datos.Tables[2].Load(resultado);
                            datos.Tables[3].Load(resultado);
                        }
                    }
                    catch(Exception ex)
                    {
                        throw new ArgumentException("NO SE PUDO LEER " + ex.Message.ToString());
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

        }
  
        public string InsertarTiempoProloga(CapaEntidad.CE_TiempoProloga objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using(SqlCommand comando = new SqlCommand("TIEMPOPROLOGA_INSERTAR", CON))
                    {
                        comando.Parameters.Add("@NombreTiempo", SqlDbType.VarChar, 50).Value = objeto.NombreTiempo;
                        SqlParameter parametro = new SqlParameter();
                        parametro.ParameterName = "@Mensaje";
                        parametro.SqlDbType = SqlDbType.VarChar;
                        parametro.Size = 500;
                        parametro.Direction = ParameterDirection.Output;
                        comando.Parameters.Add(parametro);
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.ExecuteNonQuery();

                        Mensaje = parametro.Value.ToString();
                        
                    }
                }
                catch(Exception ex)
                {
                    Mensaje = ex.Message;
                }
                finally
                {
                    if(CON.State == ConnectionState.Open)
                    {
                        CON.Close();
                    }
                }

                return Mensaje;
            }
        }

        public string ActualizarTiempoProloga(CapaEntidad.CE_TiempoProloga objeto)
        {
            Mensaje = string.Empty;

            using (SqlConnection Con = new SqlConnection(Conexion))
            {
                try
                {
                    Con.Open();

                    using(SqlCommand comando = new SqlCommand("TIEMPOPROLOGA_EDITAR", Con))
                    {
                        comando.Parameters.Add("@IdTiempoProloga", SqlDbType.Int).Value = objeto.IdTiempoProloga;
                        comando.Parameters.Add("@NombreTiempo", SqlDbType.VarChar).Value = objeto.NombreTiempo;
                        comando.Parameters.Add("@Estado", SqlDbType.VarChar).Value = objeto.Estado;
                        SqlParameter Parametro = new SqlParameter();
                        Parametro.ParameterName = "@mensaje";
                        Parametro.SqlDbType = SqlDbType.VarChar;
                        Parametro.Size = 500;
                        Parametro.Direction = ParameterDirection.Output;
                        comando.Parameters.Add(Parametro);
                        comando.CommandType = CommandType.StoredProcedure;

                        comando.ExecuteNonQuery();

                        Mensaje = Parametro.Value.ToString();
                        //Mensaje =  comando.ExecuteNonQuery() !=1 ? "ERROR": "OK";
                    }
                }
                catch(Exception ex)
                {
                    Mensaje = ex.Message;
                }
                finally
                {
                    if(Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
                return Mensaje;
            }
        }


        public string EliminarTiempoProloga(int IdTiempoProloga)
        {
            Mensaje = string.Empty;
            using (SqlConnection con = new SqlConnection(Conexion))
            {
                try
                {
                    con.Open();
                    using(SqlCommand comando = new SqlCommand("TIEMPOPROLOGA_ELIMINAR", con))
                    {
                        comando.Parameters.Add("@IdTiempoProloga", SqlDbType.Int).Value = IdTiempoProloga;
                        SqlParameter parametro = new SqlParameter();
                        parametro.ParameterName = "@Mensaje";
                        parametro.SqlDbType = SqlDbType.VarChar;
                        parametro.Size = 100;
                        parametro.Direction = ParameterDirection.Output;
                        comando.Parameters.Add(parametro);
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.ExecuteNonQuery();

                        Mensaje = parametro.Value.ToString();
                    }
                }
                catch(Exception ex)
                {
                    Mensaje = ex.Message;
                }
                finally
                {
                    if(con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                return Mensaje;
            }
        }

        public DataTable MostrarTiempoPrologaCB()
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    SqlDataReader LeerResultado;
                    CON.Open();
                    string Consulta = "SELECT IdTiempoProloga, NombreTiempo FROM TIEMPO_PROLOGA WHERE ESTADO = 'DISPONIBLE'";
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
