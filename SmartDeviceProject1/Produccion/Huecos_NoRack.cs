using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Intermec.DataCollection.RFID;

namespace SmartDeviceProject1.Produccion
{
    public partial class Huecos_NoRack : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        //SmartDeviceProject1.NapresaSitio.Service1 ws = new SmartDeviceProject1.NapresaSitio.Service1();
        cMetodos c = new cMetodos();
        
        string[] detalle;
        string[] user;
        string codigo;
        string[] data;
        string tag;
        int evento;
        BRIReader lector;

        public Huecos_NoRack(string[] folio, string[] usuario,int evnt)
        {
            InitializeComponent();
            user = usuario;
            detalle = folio;
            evento = evnt;
            lblFolio.Text = folio[1];
            lblOP.Text = folio[2];
            lblId.Text = folio[0];
            lblProd.Text = folio[4];
            if(evento == 0)
                txtActual.Text = folio[14];
            if(evento == 1)
                txtActual.Text = folio[15];
            txtMerma.Focus();
            data = folio;
            
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
            frmMenu_Produccion fmp = new frmMenu_Produccion(user);
            fmp.Show();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //Cursor.Current = Cursors.WaitCursor;
            //    int estimado = int.Parse(txtActual.Text), huecos = int.Parse(txtMerma.Text);
            //    if (huecos > estimado)
            //    {
            //        MessageBox.Show("La cantidad de huecos no puede ser mayor a la cantidad por Rack", "Aviso");
            //    }
            //    else
            //    {
            //        Cursor.Current = Cursors.WaitCursor;
            //        int real = estimado - huecos;
            //        if (evento == 0)
            //        {
            //            MessageBox.Show(c.avanzarEstado(detalle[1], "PRODUCCION", detalle[0], detalle[11], real, user[4]));
            //            this.Dispose();
            //            GC.Collect();
            //            Revisar_Avance ra = new Revisar_Avance(user);
            //            ra.Show();
            //        }
            //        if (evento == 1)
            //        {
            //            label4.Text = "Mermas";//JLMQ PARA PRUEBAS
            //            int res = c.noHuecos(huecos, int.Parse(txtActual.Text.ToString()), int.Parse(detalle[0]), detalle[1], detalle[8], user[4], user[3]);
            //            switch (res)
            //            {
            //                case 0:
            //                    MessageBox.Show("Cambio Exitoso");
            //                    this.Dispose();
            //                    GC.Collect();   
            //                    Revisar_Avance ra = new Revisar_Avance(user);
            //                    ra.Show();
            //                    break;
            //                case 1:
            //                    MessageBox.Show("Hubo un problema al guardar.\n Intente de nuevo");
            //                    break;
            //                case 2:
            //                    MessageBox.Show("La cantidad a mermar no puede superar el maximo permitido para este rack");
            //                    break;
            //                case 3:
            //                    MessageBox.Show("Cambio Exitoso");
            //                    this.Dispose();
            //                    GC.Collect();
            //                    Revisar_Avance ra1 = new Revisar_Avance(user);
            //                    ra1.Show();
            //                    break;
            //                default: break;
            //            }
            //        }
            //    }
                
            //    Cursor.Current = Cursors.Default;
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show("Hubo un error al momento de generar mermas. Revise que:\n-No existan letras en el campo Merma.\n-El campo Merma no este en blanco.", "Aviso");
            //    return;
                
            //}
        }
    }
}