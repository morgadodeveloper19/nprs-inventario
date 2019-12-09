using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SmartDeviceProject1
{
   public class Trazabilidad
    {

       SmartDeviceProject1.localhost.Service1 wsGM = new SmartDeviceProject1.localhost.Service1();

       public DbQueryResult trazabillidad(string epcpos, string epcart, string claveConceptoAux)
       {

           SmartDeviceProject1.localhost.MovimientoInventario MovInveAux = new SmartDeviceProject1.localhost.MovimientoInventario();

           SmartDeviceProject1.localhost.MovimientoInventario[] arregloAux = new SmartDeviceProject1.localhost.MovimientoInventario[1];
           arregloAux[0] = MovInveAux;
           SmartDeviceProject1.localhost.DetalleMovimiento detalleMov = new SmartDeviceProject1.localhost.DetalleMovimiento();
           detalleMov.cantidad = 1;
           //detalleMov.idAlmacen = GlobalDataSingleton.Instance.AlmacenID;
           detalleMov.EPCArt = epcart;
           detalleMov.EPCPos = epcpos;

           SmartDeviceProject1.localhost.DetalleMovimiento[] arreglodetallesMov = new SmartDeviceProject1.localhost.DetalleMovimiento[1];
           arreglodetallesMov[0] = detalleMov;
           
           //SmartDeviceProject1.localhost.DbQueryResultWS res = wsGM.InsertaMovInventario(claveConceptoAux, GlobalDataSingleton.Instance.userID, arregloAux, arreglodetallesMov);
           SmartDeviceProject1.localhost.DbQueryResultWS resultado = new SmartDeviceProject1.localhost.DbQueryResultWS();
           //resultado.Success = res.Success;
           //resultado.ErrorMessage = res.ErrorMessage;
           return resultado;
       }
    }
}
