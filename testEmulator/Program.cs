using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace test_emulator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }
    }
}


/*
 
 using test_emulator;


// DETECCION DE EMULADOR PRUEBAS
string epc = "";
if (TestEmulator.isEmulator())
{
    Dialog frm = new Dialog();
    frm.ShowDialog();
    epc = frm.EPC;
    MessageBox.Show(epc);
}
// FIN DETECCION DE EMULADOR PRUEBAS





// DETECCION DE EMULADOR PRUEBAS
if (TestEmulator.isEmulator())
{

    return;
}
// FIN DETECCION DE EMULADOR PRUEBAS
                
 */