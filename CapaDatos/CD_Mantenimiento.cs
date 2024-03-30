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
    public class CD_Mantenimiento
    {
        string Conexion = BD_Conexion.ConectarBD();
        string Mensaje;

        public string EnviarMantenimiento(CE_Mantenimiento objeto)
        {
            Mensaje = string.Empty;

            using (SqlConnection con = new SqlConnection(Conexion))
            {
                try
                {
                    con.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_ENVIAR_MANTENIMIENTO", con))
                    {
                        Comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = objeto.IdUsuario;
                        Comando.Parameters.Add("@IdBicicleta", SqlDbType.Int).Value = objeto.IdBicicleta;
                        Comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = objeto.IdCliente;
                        Comando.Parameters.Add("@DetalleMantenimieto", SqlDbType.VarChar, 200).Value = objeto.DetalleMantenimiento;

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
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                return Mensaje;
            }
        }

        public string RetiroMantenimiento(CE_Mantenimiento objeto)
        {
            Mensaje = string.Empty;

            using (SqlConnection con = new SqlConnection(Conexion))
            {
                try
                {
                    con.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_RETIRAR_MANTENIMIENTO", con))
                    {
                        Comando.Parameters.Add("@IdMantenimiento", SqlDbType.Int).Value = objeto.IdMantenimiento;
                        Comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = objeto.IdUsuario;
                        Comando.Parameters.Add("@IdBicicleta", SqlDbType.Int).Value = objeto.IdBicicleta; 

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
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                return Mensaje;
            }
        }

        public DataSet ListarMantenimiento(int Fila, int Indeci)
        {
            SqlDataReader resultado;
            DataSet cargar_Datos = new DataSet();

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                using (SqlCommand Comando = new SqlCommand("SP_MANTENIMIENTO_MOSTRAR", CON))
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

        public DataTable MostrarMantenimiento()
        {
            SqlDataReader resultado;
            DataTable cargar_Datos = new DataTable();

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                StringBuilder Consultas = new StringBuilder();
                Consultas.AppendLine("SELECT M.IdMantenimiento,U.nombre AS nombre_usuario,B.nombre AS nombre_bicicleta,C.nombrecliente, M.DetalleMantenimiento,M.Repuesto,M.Estado_Compra,");
                Consultas.AppendLine("CONCAT(CONVERT(CHAR(10), M.Fecha_Compra, 103), ' ', ");
                Consultas.AppendLine("CASE WHEN DATEPART(HOUR, M.Fecha_Compra) < 12 THEN FORMAT(M.Fecha_Compra, 'hh:mm') + ' AM'");
                Consultas.AppendLine("ELSE FORMAT(M.Fecha_Compra, 'hh:mm') + ' PM' END) AS Fecha_Mantenimiento  FROM MANTENIMIENTO M");
                Consultas.AppendLine("INNER JOIN USUARIO U ON M.IdUsuario = U.idusuario");
                Consultas.AppendLine("INNER JOIN BICICLETAS B ON M.IdBicicleta=B.idbicicleta");
                Consultas.AppendLine("INNER JOIN CLIENTE C ON M.NombreMecanico=C.idcliente");

                using (SqlCommand Comando = new SqlCommand(Consultas.ToString(), CON))
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

        public DataTable CB_Detalle_Filtro(int OPCION)
        {
            SqlDataReader resultado;
            DataTable cargar_Datos = new DataTable();

            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                using (SqlCommand Comando = new SqlCommand("SP_MANTENIMIENTO_CBDETALLECOMPRA", CON))
                {
                    try
                    {
                        CON.Open();
                        Comando.Parameters.Add("@OPCION",SqlDbType.Int).Value = OPCION;
                        Comando.CommandType = CommandType.StoredProcedure;
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
    }
}
