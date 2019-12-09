using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SmartDeviceProject1
{
    public class cRack
    {
        //dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real]
        public string EPC;
        public int numero;
        public string modelo;
        public string ordenProduccion;
        public int cantidadEstimada;
        public int cantidadReal;
    }
}
