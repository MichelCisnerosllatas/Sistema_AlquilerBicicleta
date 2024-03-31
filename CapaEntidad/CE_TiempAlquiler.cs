using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_TiempAlquiler
    {
        private int idtiempoalquiler;
        private string hora;
        private string minutos;
        private string estadoTiempo;        
        private DateTime fechaRegistro;


        public int IdTiempoAlquiler { get => idtiempoalquiler; set => idtiempoalquiler = value; }
        public string Hora { get => hora; set => hora = value; }
        public string Minutos { get => minutos; set => minutos = value; }
        public string EstadoTiempoAlquiler { get => estadoTiempo; set => estadoTiempo = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }

        public double Monto { get; set; }
    }
}
