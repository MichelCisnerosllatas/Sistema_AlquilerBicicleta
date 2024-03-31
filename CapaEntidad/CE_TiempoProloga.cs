using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_TiempoProloga
    {
        private int Idtiempoprologa;
        private string nombretiempo;
        private string estado;
        private DateTime fecharegistro;


        public int IdTiempoProloga { get => Idtiempoprologa; set => Idtiempoprologa = value; }
        public string NombreTiempo { get => nombretiempo; set => nombretiempo = value; }
        public string Estado { get => estado; set => estado = value; }
        public DateTime Fecha { get => fecharegistro; set => fecharegistro = value; }
    }
}
