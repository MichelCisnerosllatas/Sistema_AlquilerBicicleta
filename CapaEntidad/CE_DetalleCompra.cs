using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_DetalleCompra
    {
        private int idCompra;
        private int idRepuesto;
        private int cantidad;
        private double subTotal;

        public int IdCompra { get => idCompra; set => idCompra = value; }
        public int IdRepuesto { get => idRepuesto; set => idRepuesto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double SubTotal { get => subTotal; set => subTotal = value; }
    }
}
