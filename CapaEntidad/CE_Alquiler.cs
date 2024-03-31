using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Alquiler
    {
        private int idAlquiler;
        private int idUsuario;
        private int idBicicletas;
        private int idCliente;
        private int idPrecioAlquiler;
        private double montoPago;
        private double montoCambio;
        private double montoAlquiler;
        private string observacion;
        private DateTime fechaFin;
        private DateTime fechaProloga;
        private int tiempo;
        private int prologa;

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public int IdBicicletas { get => idBicicletas; set => idBicicletas = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public int IdPrecioAlquiler { get => idPrecioAlquiler; set => idPrecioAlquiler = value; }
        public double MontoPago { get => montoPago; set => montoPago = value; }
        public double MontoCambio { get => montoCambio; set => montoCambio = value; }
        public double MontoAlquiler { get => montoAlquiler; set => montoAlquiler = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public DateTime FechaProloga { get => fechaProloga; set => fechaProloga = value; }
        public int Tiempo { get => tiempo; set => tiempo = value; }
        public int IdAlquiler { get => idAlquiler; set => idAlquiler = value; }
        public int Prologa { get => prologa; set => prologa = value; }
    }
}
