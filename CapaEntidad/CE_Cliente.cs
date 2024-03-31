using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CapaEntidad
{
    public class CE_Cliente
    {
        private int idcliente;
        private string nombreCliente;
        private string direccion;
        private string estadoCliente;
        private DateTime fechaRegistro;


        public int IdCliente { get => idcliente; set => idcliente = value; }
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        public string DireccionCliente { get => direccion; set => direccion = value; }
        public string EstadoCliente { get => estadoCliente; set => estadoCliente = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
    }
}
