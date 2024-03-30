using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Principal
    {
        string Conexion = BD_Conexion.ConectarBD();
        DataSet Datos = new DataSet();
        DataTable Datos2 = new DataTable();

        public DataSet Resumen_Principal()
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_PRINCIPAL_RESUMENES",CON))
                    {
                        Comando.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader LeerResultado = Comando.ExecuteReader())
                        {
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables[0].Load(LeerResultado);
                        }
                    }
                }
                catch( Exception ex)
                {
                    throw new ArgumentException(ex.Message.ToString());
                }
                finally 
                { 
                    CON.Close(); 
                }
                return Datos;
            }
        }

        public DataSet TopBicicletas()
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("SELECT TOP(5) B.nombre as Bicicleta, COUNT(B.nombre) AS Total FROM ALQUILERES AS A");
                    Consulta.AppendLine("LEFT JOIN BICICLETAS AS B ON A.idbicicleta = B.idbicicleta");
                    Consulta.AppendLine("WHERE A.idbicicleta >= 8 AND YEAR(A.fecha_registro) = YEAR(GETDATE()) AND MONTH(A.fecha_registro) = MONTH(GETDATE()) AND A.estado = 'ENTREGADO' OR A.estado = 'ALQUILADO'");
                    Consulta.AppendLine("GROUP BY B.nombre");
                    Consulta.AppendLine("ORDER BY COUNT(B.nombre) DESC");

                    using (SqlCommand Comando = new SqlCommand(Consulta.ToString(), CON))
                    {
                        Comando.CommandType = CommandType.Text;
                        using (SqlDataReader LeerResultado = Comando.ExecuteReader())
                        {
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables[0].Load(LeerResultado);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message.ToString());
                }
                finally
                {
                    CON.Close();
                }
                return Datos;
            }
        }

        public DataSet TopCliente()
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("SELECT TOP(5) C.nombrecliente as Cliente, COUNT(C.nombrecliente) AS Total FROM ALQUILERES AS A");
                    Consulta.AppendLine("LEFT JOIN CLIENTE AS C ON A.idcliente = C.idcliente");
                    Consulta.AppendLine("WHERE A.idcliente >= 8 AND YEAR(A.fecha_registro) = YEAR(GETDATE()) AND MONTH(A.fecha_registro) = MONTH(GETDATE()) AND (A.estado = 'ENTREGADO' OR A.estado = 'ALQUILADO') AND A.idcliente <> 36");
                    Consulta.AppendLine("GROUP BY C.nombrecliente");
                    Consulta.AppendLine("ORDER BY COUNT(C.nombrecliente) DESC");

                    using (SqlCommand Comando = new SqlCommand(Consulta.ToString(), CON))
                    {
                        Comando.CommandType = CommandType.Text;
                        using (SqlDataReader LeerResultado = Comando.ExecuteReader())
                        {
                            Datos.Tables.Add(new DataTable());
                            Datos.Tables[0].Load(LeerResultado);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message.ToString());
                }
                finally
                {
                    CON.Close();
                }
                return Datos;
            }
        }

        public DataTable MostrarMeses_EA(int ANIO)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    StringBuilder Consulta = new StringBuilder();
                    Consulta.AppendLine("SET LANGUAGE Spanish;");
                    Consulta.AppendLine("SELECT DISTINCT DATENAME(MONTH, fecha_registro) AS Mes, MONTH(fecha_registro) AS ID, COUNT(idalquiler) AS REGISTRO, SUM(montoalquiler) AS MONTO FROM ALQUILERES");
                    Consulta.AppendLine("WHERE YEAR(fecha_registro) = @anio AND estado <> 'ANULADOS'");
                    Consulta.AppendLine("GROUP BY MONTH(fecha_registro), DATENAME(MONTH, fecha_registro) ORDER BY Mes DESC;");

                    using (SqlCommand Comando = new SqlCommand(Consulta.ToString(), CON))
                    {
                        Comando.Parameters.Add("@anio",SqlDbType.Int).Value = ANIO;
                        Comando.CommandType = CommandType.Text;
                        SqlDataReader Resutado = Comando.ExecuteReader();
                        Datos2.Load(Resutado);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message.ToString());
                }
                finally
                {
                    CON.Close();
                }
                return Datos2;
            }                
        }

        public DataSet Estadistica_Compra(int Mes, int Ano)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_PRINCIPAL_ESTADISTICACOMPRA", CON))
                    {
                        Comando.Parameters.Add("@Mes",SqlDbType.Int).Value = Mes;
                        Comando.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
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
                    CON.Close();
                }
                return Datos;
            }
        }

        public DataSet Estadistica_Alquiler(int Mes, int Ano)
        {
            using (SqlConnection CON = new SqlConnection(Conexion))
            {
                try
                {
                    CON.Open();
                    using (SqlCommand Comando = new SqlCommand("SP_PRINCIPAL_ESTADISTICA_ALQUILER", CON))
                    {
                        Comando.Parameters.Add("@Mes", SqlDbType.Int).Value = Mes;
                        Comando.Parameters.Add("@Ano", SqlDbType.Int).Value = Ano;
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
                    CON.Close();
                }
                return Datos;
            }
        }
    }
}
