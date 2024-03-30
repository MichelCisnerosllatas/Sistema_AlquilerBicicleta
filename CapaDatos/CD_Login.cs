using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Login
    {
        string Conexion = BD_Conexion.ConectarBD();

        public DataSet Login(int Documento, string Clave)
        {
            DataSet Datos = new DataSet();
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("SELECT * FROM USUARIO AS U WHERE U.num_documento = @NumDoc AND U.clave = @Clave");

                    using (SqlCommand Comando = new SqlCommand(Consulta.ToString(), CON))
                    {
                        Comando.Parameters.Add("@NumDoc", SqlDbType.Int).Value = Documento;
                        Comando.Parameters.Add("@Clave", SqlDbType.VarChar).Value = Clave;
                        Comando.CommandType = CommandType.Text;

                        using (SqlDataReader LeeConsulta = Comando.ExecuteReader())
                        {
                            DataTable Tabla = new DataTable();
                            Tabla.Load(LeeConsulta);

                            Datos.Tables.Add(Tabla);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    CON.Close();
                }
                return Datos;
            }
        }

        //public DataSet Login(int Documento, string Clave)
        //{
        //    DataSet Datos = new DataSet();            
        //    using (SqlConnection CON = new SqlConnection(Conexion))
        //    {
        //        try
        //        {
        //            CON.Open();
        //            StringBuilder Consulta = new StringBuilder();
        //            Consulta.AppendLine("SELECT * FROM USUARIO AS U WHERE U.num_documento = @NumDoc AND U.clave = @Clave");

        //            using (MySqlCommand Comando = new MySqlCommand(Consulta.ToString(), CON))
        //            {
        //                Comando.Parameters.Add("@NumDoc", MySqlDbType.Int32).Value = Documento;
        //                Comando.Parameters.Add("@Clave", MySqlDbType.VarChar).Value = Clave;
        //                Comando.CommandType = CommandType.Text;

        //                using (MySqlDataReader LeeConsulta = Comando.ExecuteReader())
        //                {
        //                    DataTable Tabla = new DataTable();
        //                    Tabla.Load(LeeConsulta);

        //                    Datos.Tables.Add(Tabla);
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //        finally
        //        { 
        //            CON.Close();
        //        }
        //        return Datos;
        //    }
        //}
    }
}
