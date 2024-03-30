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
    public class CD_Repuesto
    {
        string Mensaje;
        string Conexion = BD_Conexion.ConectarBD();

        public DataSet Mostrar_Repuesto(int Fila, int Indice)
        {
            DataSet Datos = new DataSet();
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT COUNT(*) AS TOTAL FROM REPUESTO WHERE EstadoRepuesto = 'DISPONIBLE'");
                    sb.AppendLine("SELECT IdRepuesto,NombreRepuesto,EstadoRepuesto,FechaRegistro FROM REPUESTO WHERE EstadoRepuesto = 'DISPONIBLE'");
                    sb.AppendLine("ORDER BY IdRepuesto ASC");
                    sb.AppendLine("OFFSET (@Indice - 1) * @Fila ROWS");
                    sb.AppendLine("FETCH NEXT @Fila ROWS ONLY");
                    
                    using (SqlCommand Comando = new SqlCommand(sb.ToString(), CON))
                    {
                        Comando.Parameters.Add("@Indice", SqlDbType.Int).Value = Indice;
                        Comando.Parameters.Add("@Fila", SqlDbType.Int).Value = Fila;
                        Comando.CommandType = CommandType.Text;
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

        public string InsertarRepuesto(CE_Repuesto objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_REPUESTO_REGISTRO", CON))
                    {
                        Comando.Parameters.Add("@nombrerepuesto", SqlDbType.VarChar, 150).Value = objeto.NombreRepuesto; 
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

        public string ActualizaRepuesto(CE_Repuesto objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_REPUESTO_ACTUALIZAR", CON))
                    {
                        Comando.Parameters.Add("@IdRepuesto", SqlDbType.Int).Value = objeto.IdRepuesto;
                        Comando.Parameters.Add("@NombreRepuesto", SqlDbType.VarChar, 150).Value = objeto.NombreRepuesto; 
                        Comando.Parameters.Add("@Estado", SqlDbType.VarChar, 100).Value = objeto.EstadoRepuesto;
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

        public string EliminarRepuesto(int IdRepuesto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_REPUESTO_ELIMINAR", CON))
                    {
                        Comando.Parameters.Add("@IdRepuesto", SqlDbType.Int).Value = IdRepuesto;
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.ExecuteNonQuery();

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

        public DataSet MostrarRepuesto(int Fila, int Indice)
        {
            SqlDataReader resultado;
            DataSet datos = new DataSet();

            using (SqlConnection con = new SqlConnection(Conexion))
            {
                using (SqlCommand comando = new SqlCommand("SP_REPUESTO_MOSTRAR", con))
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


        public void ActualizarRepuestoDisponible(int idRepuesto)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                StringBuilder Consultas = new StringBuilder();
                Consultas.AppendLine("UPDATE REPUESTO SET EstadoRepuesto = 'DISPONIBLE' WHERE IdRepuesto = @IdRepuesto");

                using (SqlCommand Comando = new SqlCommand(Consultas.ToString(), CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.CommandType = CommandType.Text;
                        Comando.Parameters.AddWithValue("@IdRepuesto", idRepuesto);
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

        public void ActualizarRepuestoNODisponible(int idRpuesto)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                StringBuilder Consultas = new StringBuilder();
                Consultas.AppendLine("UPDATE REPUESTO SET EstadoRepuesto = 'NO DISPONIBLE' WHERE IdRepuesto = @IdRepuesto");

                using (SqlCommand Comando = new SqlCommand(Consultas.ToString(), CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.CommandType = CommandType.Text;
                        Comando.Parameters.AddWithValue("@IdRepuesto", idRpuesto);
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
