using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_DetalleMantenimiento
    {
        private int iddetalle_mantenimiento;
        private int idmantenimiento;
        private int idrepuesto;
        private double subtotal;

        public int IdDetalleMantenimiento { get => iddetalle_mantenimiento;set => iddetalle_mantenimiento = value; }
        public int IdMantenimiento { get => idmantenimiento; set => idmantenimiento = value; }
        public int IdRepuesto { get => idrepuesto; set => idrepuesto = value; }
        public double SubTotal { get => subtotal; set => subtotal = value; }

 
    }
}

 