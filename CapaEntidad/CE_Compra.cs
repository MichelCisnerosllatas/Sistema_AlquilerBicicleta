using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Compra
    {
        private int idUsuario;
        private int idProveedor;
        private int idTipoComprobante;
        private long numComprobante;
        private int correlativo;
        private string serie;
        private double importe;
        private double iGV;
        private double montoTotal;

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public int IdTipoComprobante { get => idTipoComprobante; set => idTipoComprobante = value; }
        public long NumComprobante { get => numComprobante; set => numComprobante = value; }
        public int Correlativo { get => correlativo; set => correlativo = value; }
        public double Importe { get => importe; set => importe = value; }
        public double IGV { get => iGV; set => iGV = value; }
        public double MontoTotal { get => montoTotal; set => montoTotal = value; }
        public string Serie { get => serie; set => serie = value; }
    }
}
