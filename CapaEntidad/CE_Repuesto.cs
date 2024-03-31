using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Repuesto
    {
        private int idrepuesto;
        private string nombreRepuesto; 
        private string estadorepuesto;
        private DateTime fechaRegistro;


        public int IdRepuesto { get => idrepuesto; set => idrepuesto = value; }
        public string NombreRepuesto { get => nombreRepuesto; set => nombreRepuesto = value; } 
        public string EstadoRepuesto { get => estadorepuesto; set => estadorepuesto = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
    }
}
