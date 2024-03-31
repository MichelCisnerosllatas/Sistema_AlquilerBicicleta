using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Mantenimiento
    {
        private int idmantenimiento;
        private int idsusario;
        private int idbicicleta;
        private int idcliente;
        private string detallemantenimiento;
        private string estadomantenimiento ;
        private DateTime fecha;


        public int IdMantenimiento{ get => idmantenimiento; set => idmantenimiento = value; }
        public int IdUsuario{ get => idsusario; set => idsusario = value; }
        public int IdBicicleta{ get => idbicicleta; set => idbicicleta = value; }
        public int IdCliente{ get => idcliente; set => idcliente = value; }
        public string DetalleMantenimiento{ get => detallemantenimiento; set => detallemantenimiento = value; }
        public string EstadoMantenimiento{ get => estadomantenimiento; set => estadomantenimiento = value; }
        public DateTime FechaMantenimiento{ get => fecha; set => fecha = value; } 
    }
}
