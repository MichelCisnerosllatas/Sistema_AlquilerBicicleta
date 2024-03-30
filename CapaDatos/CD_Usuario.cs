using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaEntidad;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaDatos
{
    public class CD_Usuario
    {
        string Conexion = BD_Conexion.ConectarBD();
        string Mensaje;

        public byte[] MostrarFoto(int IdUsuario)
        {
            byte[] Foto = null;            

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SELECT foto FROM USUARIO WHERE idusuario = @idusuario", CON))
                    {
                        Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = IdUsuario;
                        Comando.CommandType = CommandType.Text;

                        using (SqlDataReader Leer = Comando.ExecuteReader())
                        {
                            while(Leer.Read())
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

        public string InsertarUsuarioBasico(CE_Usuario objeto)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_USUARIOREGISTROBASICO", CON))
                    {
                        Comando.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = objeto.NombreUsuario;
                        Comando.Parameters.Add("@idtipodoc", SqlDbType.Int).Value = objeto.TipoDocumento;
                        Comando.Parameters.Add("@num_documento", SqlDbType.Int).Value = objeto.Numdocumento;
                        Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = objeto.ClaveUsuario;
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

        public string InsertarUsuario(CE_Usuario objeto)
        {
            Mensaje = string.Empty;

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("USUARIO_REGISTRAR", CON))
                    {
                        Comando.Parameters.Add("@IdRol", SqlDbType.Int).Value = objeto.Idrol;
                        Comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = objeto.NombreUsuario;
                        Comando.Parameters.Add("@IdTipDocumento", SqlDbType.Int).Value = objeto.TipoDocumento;
                        Comando.Parameters.Add("@NumDocumento", SqlDbType.Int).Value = objeto.Numdocumento;
                        Comando.Parameters.Add("@Clave", SqlDbType.VarChar).Value = objeto.ClaveUsuario;
                        Comando.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = objeto.FotoUsuario;
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

        public string ActualizarUsuario(CE_Usuario objeto)
        {
            Mensaje = string.Empty;

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("USUARIO_EDITAR", CON))
                    {
                        Comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = objeto.IdUsuario;
                        Comando.Parameters.Add("@IdRol", SqlDbType.Int).Value = objeto.Idrol;
                        Comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 500).Value = objeto.NombreUsuario;
                        Comando.Parameters.Add("@IdTipDocumento", SqlDbType.Int).Value = objeto.TipoDocumento;
                        Comando.Parameters.Add("@NumDocumento", SqlDbType.Int).Value = objeto.Numdocumento;
                        Comando.Parameters.Add("@Clave", SqlDbType.VarChar, 500).Value = objeto.ClaveUsuario;
                        Comando.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = objeto.FotoUsuario;
                        Comando.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Value = objeto.EstadoUsuario;
                        Comando.CommandType = CommandType.StoredProcedure;
                        Mensaje = Comando.ExecuteNonQuery() != 1 ? "ERROR" : "OK";

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

        public string EliminarUsuario(int IdUsuario)
        {
            Mensaje = string.Empty;
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("USUARIO_ELIMINAR", CON))
                    {
                        Comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;
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

        //public DataTable MostrarUsuario()
        //{
        //    SqlDataReader resultado;
        //    DataTable cargar_Datos = new DataTable();

        //    using (SqlConnection CON = new SqlConnection(Conexion))
        //    {
        //        StringBuilder Consultas = new StringBuilder();
        //        Consultas.AppendLine("SELECT u.foto, u.idusuario,R.idrol,R.rol,U.nombre,TD.[idtipodoc],TD.tipo, U.num_documento,U.clave,U.estado, ");
        //        Consultas.AppendLine("CONCAT(CONVERT(CHAR(10), u.fecha_registro, 103), ' ', ");
        //        Consultas.AppendLine("CASE WHEN DATEPART(HOUR, u.fecha_registro) < 12 THEN FORMAT(u.fecha_registro, 'hh:mm') + ' AM'");
        //        Consultas.AppendLine("ELSE FORMAT(u.fecha_registro, 'hh:mm') + ' PM' END) AS Fecha FROM USUARIO U");
        //        Consultas.AppendLine("INNER JOIN ROL R ON U.idrol=R.idrol");
        //        Consultas.AppendLine("INNER JOIN TIPODOC TD ON U.idtipodoc=TD.idtipodoc");

        //        using (SqlCommand Comando = new SqlCommand(Consultas.ToString(), CON))
        //        {
        //            try
        //            {
        //                CON.Open();
        //                Comando.CommandType = CommandType.Text;
        //                resultado = Comando.ExecuteReader();
        //                cargar_Datos.Load(resultado);
        //                return cargar_Datos;
        //            }
        //            catch (Exception error)
        //            {
        //                throw new ArgumentException("NO SE PUDO leer " + error.Message.ToString());
        //            }
        //            finally
        //            {
        //                if (CON.State == ConnectionState.Open)
        //                {
        //                    CON.Close();
        //                }
        //            }
        //        }
        //    }
        //}

        public DataTable MostrarUsuriosCB()
        {
            SqlDataReader resultado;
            DataTable cargar_Datos = new DataTable();

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                using (SqlCommand Comando = new SqlCommand("SELECT idusuario,nombre FROM USUARIO WHERE estado = 'DISPONIBLE'", CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.CommandType = CommandType.Text;
                        resultado = Comando.ExecuteReader();
                        cargar_Datos.Load(resultado);
                        return cargar_Datos;
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
                }
            }
        }

        public DataSet ListarUsuario(int Fila, int Indeci)
        {
            SqlDataReader resultado;
            DataSet cargar_Datos = new DataSet();

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                using (SqlCommand Comando = new SqlCommand("SP_USUARIO_MOSTRAR", CON))
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
                using (SqlCommand Comando = new SqlCommand("SP_USUARIO_BUSCAR", CON))
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
                            cargar_Datos.Tables.Add(new DataTable());

                            cargar_Datos.Tables[0].Load(resultado);
                            cargar_Datos.Tables[1].Load(resultado);
                            cargar_Datos.Tables[2].Load(resultado);
                            cargar_Datos.Tables[3].Load(resultado);
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

        public void ActualizarUsuarioDisponible(int idUsuario)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                StringBuilder Consultas = new StringBuilder();
                Consultas.AppendLine("UPDATE USUARIO SET estado = 'DISPONIBLE' WHERE idusuario = @IdUsuario");

                using (SqlCommand Comando = new SqlCommand(Consultas.ToString(), CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.CommandType = CommandType.Text;
                        Comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
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

        public void ActualizarUsuarioNODisponible(int idUsuario)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                StringBuilder Consultas = new StringBuilder();
                Consultas.AppendLine("UPDATE USUARIO SET estado = 'NO DISPONIBLE' WHERE idusuario = @IdUsuario");

                using (SqlCommand Comando = new SqlCommand(Consultas.ToString(), CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.CommandType = CommandType.Text;
                        Comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
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
