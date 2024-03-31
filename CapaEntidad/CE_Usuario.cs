using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Usuario
    {
        private int idUsuario;
        private int idrol;
        private string nombreUsuario;
        private int tipoDocumento;
        private int numdocumento;
        private string claveUsuario;
        private byte[] fotoUsuario;
        private string estadoUsuario;
        private DateTime fechaRegistro;

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public int Idrol { get => idrol; set => idrol = value; }
        public int TipoDocumento { get => tipoDocumento; set => tipoDocumento = value; }
        public string ClaveUsuario { get => claveUsuario; set => claveUsuario = value; }
        public byte[] FotoUsuario { get => fotoUsuario; set => fotoUsuario = value; }
        public string EstadoUsuario { get => estadoUsuario; set => estadoUsuario = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public int Numdocumento { get => numdocumento; set => numdocumento = value; }
    }
}
