using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SmartDeviceProject1
{
    public class DetRemision
    {
        public string Remision { get; set; }

        public string Pedido { get; set; }

        public string CodigoProducto { get; set; }

        public int CantPiezas { get; set; }

        public int CtaPzaCargada { get; set; }

        public int CtaPzaFaltante { get; set; }

        public int PzaRemision { get; set; }
    }
}
