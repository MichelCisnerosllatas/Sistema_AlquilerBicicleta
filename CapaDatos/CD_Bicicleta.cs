using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Bicicleta
    {
        string Mensaje;
        string Conexion = BD_Conexion.ConectarBD();

        public byte[] MostrarFoto(int IdBicicleta)
        {
            byte[] Foto = null;

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SELECT foto FROM BICICLETAS WHERE idbicicleta = @idbicicleta", CON))
                    {
                        Comando.Parameters.Add("@idbicicleta", SqlDbType.Int).Value = IdBicicleta;
                        Comando.CommandType = CommandType.Text;

                        using (SqlDataReader Leer = Comando.ExecuteReader())
                        {
                            while (Leer.Read())
                            {
                                Foto = Leer["foto"] != DBNull.Value ? (byte[])(Leer["foto"]) : new byte[0];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    if (CON.State == ConnectionState.Open)
                    {
                        CON.Close();
                    }
                }
                return Foto;
            }
        }

        public string InsertarBicicleta(CE_Bicicleta objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("BICICLETA_REGISTRAR", CON))
                    {
                        Comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = objeto.NombreBicicleta;
                        Comando.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = objeto.FotoBicicleta;
                        Comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = objeto.Descripcion;
                        Comando.Parameters.Add("@Precio_diario", SqlDbType.Decimal).Value = objeto.PrecioDiario;
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

        public string ActualizaeBicicleta(CE_Bicicleta objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("BICICLETA_EDITAR", CON))
                    {
                        Comando.Parameters.Add("@IdBicicleta", SqlDbType.Int).Value = objeto.IdBicicleta;
                        Comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = objeto.NombreBicicleta;
                        Comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = objeto.Descripcion;
                        Comando.Parameters.Add("@Precio_diario", SqlDbType.Decimal, 32).Value = objeto.PrecioDiario;
                        Comando.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = objeto.FotoBicicleta;
                        Comando.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Value = objeto.EstadoBicicleta;
                        SqlParameter Parametro = new SqlParameter();
                        Parametro.ParameterName = "@mensaje";
                        Parametro.SqlDbType = SqlDbType.VarChar;
                        Parametro.Size = 500;
                        Parametro.Direction = ParameterDirection.Output;
                        Comando.Parameters.Add(Parametro);
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.ExecuteNonQuery();

                        Mensaje = Parametro.Value.ToString();
                        //Mensaje = Comando.ExecuteNonQuery() == 1 ? "ERROR" : "OK";

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

        public string ActualizarBicicletaSinFoto(CE_Bicicleta objeto)
        {
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection CON = new SqlConnection(Conexion))
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("BICICLETA_EDITAR_SIN_FOTO", CON)) // Reemplaza con el nombre de tu procedimiento almacenado
                    {
                        Comando.Parameters.Add("@IdBicicleta", SqlDbType.Int).Value = objeto.IdBicicleta;
                        Comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = objeto.NombreBicicleta;
                        Comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = objeto.Descripcion;
                        Comando.Parameters.Add("@Precio_diario", SqlDbType.Decimal).Value = objeto.PrecioDiario;
                        Comando.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Value = objeto.EstadoBicicleta;
                        SqlParameter Parametro = new SqlParameter();
                        Parametro.ParameterName = "@mensaje";
                        Parametro.SqlDbType = SqlDbType.VarChar;
                        Parametro.Size = 500;
                        Parametro.Direction = ParameterDirection.Output;
                        Comando.Parameters.Add(Parametro);
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.ExecuteNonQuery();
                        Mensaje = Parametro.Value.ToString();

                        //Mensaje = Comando.ExecuteNonQuery() == 1 ? "ERROR" : "OK";
                    }
                }
                 
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return Mensaje;
        }

        public string EliminarBicicleta(int IdBicicleta)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("BICICLETA_ELIMINAR", CON))
                    {
                        Comando.Parameters.Add("@IdBicicleta", SqlDbType.Int).Value = IdBicicleta;
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

        public DataSet MostrarBicicleta(int Fila, int Indeci)
        {
            SqlDataReader resultado;
            DataSet cargar_Datos = new DataSet();

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                using (SqlCommand Comando = new SqlCommand("SP_BICICLETA_MOSTRAR", CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.Parameters.Add("@Fila", SqlDbType.Int).Value = Fila;
                        Comando.Parameters.Add("@Indice", SqlDbType.Int).Value = Indeci;
                        Comando.CommandType = CommandType.StoredProcedure;

                        using (resultado = Comando.ExecuteReader())
                        {
                            cargar_Datos.Tables.Add(new DataTable());
                            cargar_Datos.Tables.Add(new DataTable());
                            cargar_Datos.Tables.Add(new DataTable());
                            cargar_Datos.Tables.Add(new DataTable());
                            cargar_Datos.Tables.Add(new DataTable());
                            cargar_Datos.Tables.Add(new DataTable());

                            cargar_Datos.Tables[0].Load(resultado);
                            cargar_Datos.Tables[1].Load(resultado);
                            cargar_Datos.Tables[2].Load(resultado);
                            cargar_Datos.Tables[3].Load(resultado);
                            cargar_Datos.Tables[4].Load(resultado);
                            cargar_Datos.Tables[5].Load(resultado);
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

                    return cargar_Datos;
                }
            }
        }

        public DataSet Buscar(int opcion, string buscar, int Fila, int Indeci)
        {
            SqlDataReader resultado;
            DataSet cargar_Datos = new DataSet();

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                using (SqlCommand Comando = new SqlCommand("SP_BICICLETA_BUSCAR", CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.Parameters.Add("@opcion", SqlDbType.Int).Value = opcion;
                        Comando.Parameters.Add("@buscar", SqlDbType.VarChar, 50).Value = buscar;
                        Comando.Parameters.Add("@Fila", SqlDbType.Int).Value = Fila;
                        Comando.Parameters.Add("@Indice", SqlDbType.Int).Value = Indeci;
                        Comando.CommandType = CommandType.StoredProcedure;

                        using (resultado = Comando.ExecuteReader())
                        {
                            cargar_Datos.Tables.Add(new DataTable());
                            cargar_Datos.Tables.Add(new DataTable());
                            cargar_Datos.Tables.Add(new DataTable());

                            cargar_Datos.Tables[0].Load(resultado);
                            cargar_Datos.Tables[1].Load(resultado);
                            cargar_Datos.Tables[2].Load(resultado);
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

                    return cargar_Datos;
                }
            }
        }

        public void ActualizarBicicletaDisponible(int idBicicleta)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                StringBuilder Consultas = new StringBuilder();
                Consultas.AppendLine("UPDATE BICICLETAS SET estado = 'DISPONIBLE' WHERE idbicicleta = @IdBicicleta");

                using (SqlCommand Comando = new SqlCommand(Consultas.ToString(), CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.CommandType = CommandType.Text;
                        Comando.Parameters.AddWithValue("@IdBicicleta", idBicicleta);
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


        public void ActualizarBicicletaNODisponible(int idBicicleta)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                StringBuilder Consultas = new StringBuilder();
                Consultas.AppendLine("UPDATE BICICLETAS SET estado = 'NO DISPONIBLE' WHERE idbicicleta = @IdBicicleta");

                using (SqlCommand Comando = new SqlCommand(Consultas.ToString(), CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.CommandType = CommandType.Text;
                        Comando.Parameters.AddWithValue("@IdBicicleta", idBicicleta);
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
