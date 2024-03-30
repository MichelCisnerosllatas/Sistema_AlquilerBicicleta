using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using CapaEntidad;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_Cliente
    {
        string Conexion = BD_Conexion.ConectarBD();
        string Mensaje;
        DataTable Datos = new DataTable();

        public string InsertarCliente(CE_Cliente objeto)
        {
            Mensaje = string.Empty;            
            using (MySqlConnection CON = new MySqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (MySqlCommand Comando = new MySqlCommand("SP_CLIENTEREGISTRO", CON))
                    {
                        Comando.Parameters.Add("@nombrecliente", MySqlDbType.VarChar, 50).Value = objeto.NombreCliente;  
                        Comando.Parameters.Add("@direccion", MySqlDbType.VarChar, 100).Value = objeto.DireccionCliente;  
                        MySqlParameter Parametro = new MySqlParameter();
                        Parametro.ParameterName = "@mensaje";
                        Parametro.MySqlDbType = MySqlDbType.VarChar;
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

        public string ActualizaeCliente(CE_Cliente objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_CLIENTE_ACTUALIZAR", CON))
                    {
                        Comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = objeto.IdCliente;
                        Comando.Parameters.Add("@NombreCliente", SqlDbType.VarChar, 50).Value = objeto.NombreCliente;
                        Comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 100).Value = objeto.DireccionCliente;
                        Comando.Parameters.Add("@Estado", SqlDbType.VarChar, 100).Value = objeto.EstadoCliente;
                        SqlParameter Parametro = new SqlParameter();
                        Parametro.ParameterName = "@mensaje";
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

        public string EliminarCliente(int IdCliente)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_CLIENTE_ELIMINAR", CON))
                    {
                        Comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = IdCliente;
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

        public List<CE_Cliente> Lista()
        {
            List<CE_Cliente> ListaCliente = new List<CE_Cliente>();
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    string Consulta = "SELECT idcliente,nombrecliente,direccion,estado,CONVERT(CHAR(20),fecha_registro,103) AS Fecha FROM CLIENTE";
                    using (SqlCommand Comando = new SqlCommand(Consulta.ToString(), CON))
                    {
                        Comando.CommandType = CommandType.Text;
                        using (SqlDataReader LeerConsulta = Comando.ExecuteReader())
                        {
                            while (LeerConsulta.Read())
                            {
                                ListaCliente.Add(new CE_Cliente()
                                {
                                    IdCliente = int.Parse(LeerConsulta["idcliente"].ToString()),
                                    NombreCliente = LeerConsulta["nombrecliente"].ToString(),
                                    DireccionCliente = LeerConsulta["direccion"].ToString(),
                                    EstadoCliente = LeerConsulta["estado"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(LeerConsulta["Fecha"])
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ListaCliente = new List<CE_Cliente>();
                    throw new ArgumentException(ex.Message.ToString());
                }
                finally
                {
                    if (CON.State == ConnectionState.Open)
                    {
                        CON.Close();
                    }
                }
                return ListaCliente;
            }
        }

        public DataTable MostrarClienteModal()
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    SqlDataReader LeerResultado;
                    CON.Open();
                    string Consulta = "SELECT idcliente,nombrecliente,direccion,estado,CONVERT(CHAR(20),fecha_registro,103) AS Fecha FROM CLIENTE";
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

        public DataSet MostrarCliente(int Fila, int Indice)
        {
            SqlDataReader resultado;
            DataSet datos = new DataSet();

            using(SqlConnection con = new SqlConnection(Conexion))
            {
                using(SqlCommand comando = new SqlCommand("SP_CLIENTE_MOSTRAR", con))
                {
                    try
                    {
                        con.Open();
                        comando.Parameters.Add("@Fila", SqlDbType.Int).Value = Fila;
                        comando.Parameters.Add("@Indice", SqlDbType.Int).Value = Indice;
                        comando.CommandType = CommandType.StoredProcedure;

                        using (resultado = comando.ExecuteReader())
                        {
                            datos.Tables.Add(new DataTable());
                            datos.Tables.Add(new DataTable());
                            datos.Tables.Add(new DataTable());
                            datos.Tables.Add(new DataTable());
                            datos.Tables.Add(new DataTable());
                            datos.Tables.Add(new DataTable());

                            datos.Tables[0].Load(resultado);
                            datos.Tables[1].Load(resultado);
                            datos.Tables[2].Load(resultado);
                            datos.Tables[3].Load(resultado);
                            datos.Tables[4].Load(resultado);
                            datos.Tables[5].Load(resultado);
                        }
                    }
                    catch(Exception ex)
                    {
                        throw new ArgumentException("NO SE PUDO LEER" + ex.Message.ToString());
                    }
                    finally
                    {
                        if(con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }

                    return datos;
                }
            }
        }

        public DataSet Buscar(int Opcion, string Buscar, int Fila, int Indice)
        {
            SqlDataReader resultado;
            DataSet datos = new DataSet();

            using (SqlConnection con = new SqlConnection(Conexion))
            {
                using(SqlCommand comando = new SqlCommand("SP_CLIENTE_BUSCAR", con))
                {
                    try
                    {
                        con.Open();
                        comando.Parameters.Add("@opcion", SqlDbType.Int).Value = Opcion;
                        comando.Parameters.Add("@buscar", SqlDbType.VarChar).Value = Buscar;
                        comando.Parameters.Add("@Fila", SqlDbType.Int).Value = Fila;
                        comando.Parameters.Add("@Indice", SqlDbType.Int).Value = Indice;
                        comando.CommandType = CommandType.StoredProcedure;

                        using(resultado = comando.ExecuteReader())
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
                        throw new ArgumentException("ERROR"+ ex.Message.ToString());
                    }
                    finally
                    {
                        if(con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }

                    return datos;
                }
            }

        }

        public void ActualizarClienteDisponible(int idCliente)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                StringBuilder Consultas = new StringBuilder();
                Consultas.AppendLine("UPDATE CLIENTE SET estado = 'DISPONIBLE' WHERE idcliente = @IdCliente");

                using (SqlCommand Comando = new SqlCommand(Consultas.ToString(), CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.CommandType = CommandType.Text;
                        Comando.Parameters.AddWithValue("@IdCliente", idCliente);
                        Comando.ExecuteNonQuery();
                    }
                    catch (Exception error)
                    {
                        throw new ArgumentException("NO SE PUDO actualizar " + error.Message.ToString());
                    }
                    finally
                    {
                        if (CON.State == ConnectionState.Open)
                        {
                            CON.Close();
                        }
                    }
                }
            }
        }

        public void ActualizarClienteNODisponible(int idCliente)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                StringBuilder Consultas = new StringBuilder();
                Consultas.AppendLine("UPDATE CLIENTE SET estado = 'NO DISPONIBLE' WHERE idcliente = @IdCliente");

                using (SqlCommand Comando = new SqlCommand(Consultas.ToString(), CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.CommandType = CommandType.Text;
                        Comando.Parameters.AddWithValue("@IdCliente", idCliente);
                        Comando.ExecuteNonQuery();
                    }
                    catch (Exception error)
                    {
                        throw new ArgumentException("NO SE PUDO actualizar " + error.Message.ToString());
                    }
                    finally
                    {
                        if (CON.State == ConnectionState.Open)
                        {
                            CON.Close();
                        }
                    }
                }
            }
        }

    }
}
