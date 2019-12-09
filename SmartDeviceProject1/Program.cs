using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;

namespace SmartDeviceProject1
{
    static class Program
    {
		public static string[] usuario;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            string[] arr1 = new string[] { "one", "two", "three" };
            Application.Run(new frmLogin());
			//Application.Run(new Inventario.Producto_Stock());
            //Application.Run(new Prueba_WS());
            //Application.Run(new Almacen.Reimpresion_Etiqueta(null,null));
            //Application.Run(new Produccion.Bautizar_Racks("AB3199", "BLH0311", "2048", arr1));
        }
    }
} 