using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Bicicleta
    {
        private int idbicicleta;
        private string nombreBicicleta;
        private byte[] fotoBicicleta;
        private string descripcion;
        private decimal precioDiario;
        private string estadoBicileta;
        private DateTime fechaRegistro;


        public int IdBicicleta { get => idbicicleta; set => idbicicleta = value; }
        public string NombreBicicleta { get => nombreBicicleta; set => nombreBicicleta = value; }
        public byte[] FotoBicicleta { get => fotoBicicleta; set => fotoBicicleta = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public decimal PrecioDiario { get => precioDiario; set => precioDiario = value; }
        public string EstadoBicicleta { get => estadoBicileta; set => estadoBicileta = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
    }
}
