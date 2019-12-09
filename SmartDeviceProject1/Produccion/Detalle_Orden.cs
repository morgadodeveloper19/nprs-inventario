using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1.Produccion
{
    public partial class Detalle_Orden : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        //SmartDeviceProject1.NapresaSitio.Service1 ws = new SmartDeviceProject1.NapresaSitio.Service1();
        
        string[] detalle;
        string[] user;
        int asignado;
        int cantidadParcialidad;
        string newId;

        public static void LiberarControles(System.Windows.Forms.Control control)
        {
            for (int i = 0; i <= control.Controls.Count - 1; i++)
            {
                if (control.Controls[i].Controls.Count > 0)
                    LiberarControles(control.Controls[i]);
                control.Controls[i].Dispose();
            }
        }

        public Detalle_Orden(string[] folio, string[] usuario, int racks, string newidSQL)
        {
            InitializeComponent();
            newId = newidSQL;
            user = usuario;
            detalle = folio;
            asignado = racks;
            try
            {
                textBox1.Text = folio[1];
                lblProd.Text = folio[4];
                if (folio[10].Trim() == "PRODUCCION" || folio[10].Trim() == "PENDIENTE")
                    lblCant.Text = folio[14];
                else if (folio[10] == "CURADO")
                {
                    lblCant.Text = folio[15];
                    lblCantidad.Text = "Piezas en CURADO:";
                }
                else if (folio[10] == "LIBERADO")
                {
                    lblCant.Text = folio[16];
                    lblCantidad.Text = "Piezas LIBERADAS:";
                }
                lblEstatus.Text = folio[10];
                lblOP.Text = folio[2];
                detalle = folio;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Ocurrio un problema con la carga de datos.\nFavor de intentarlo más tarde", "Advertencia");
                //LiberarControles(this);
                this.Dispose();
                GC.Collect();
                frmMenu_Produccion fpo = new frmMenu_Produccion(user);
                fpo.Show();
            }
            cantidadParcialidad = Convert.ToInt32(lblCant.Text);
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            Revisar_Avance ra = new Revisar_Avance(user);
            ra.Show();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;//AQUI PASAR INFORMACION PARA QUE ACTUALICE PARCIALIDADES
            string status = lblEstatus.Text.Trim();
            if (status == "PRODUCCION" || status == "PENDIENTE")
            {
                if (asignado > 0)
                {
                    //LiberarControles(this);
                    this.Dispose();
                    GC.Collect();
                    Contar_Huecos ch = new Contar_Huecos(user, detalle, 0, cantidadParcialidad, newId);
                    ch.Show();
                    //Pasar_Curado pc = new Pasar_Curado(user,detalle);
                    //pc.Show();
                }
                else
                {
                    //LiberarControles(this);
                    this.Dispose();
                    GC.Collect();
                    Huecos_NoRack hn = new Huecos_NoRack(detalle, user,0);
                    hn.Show();
                    //Pasar_Curado pc = new Pasar_Curado(user,detalle);
                    //pc.Show();
                }
                
            }
            if (status == "CURADO")
            {
                if (asignado > 0)//se cambia a < para probar el formulario de Huecos_NoRack
                {
                    this.Dispose();
                    GC.Collect();
                    Contar_Huecos ch = new Contar_Huecos(user, detalle, 1,cantidadParcialidad, newId);
                    ch.Show();
                }
                else
                {
                    this.Dispose();
                    GC.Collect();
                    Huecos_NoRack hn = new Huecos_NoRack(detalle, user, 1);
                    hn.Show();
                }
            }
            if (status == "LIBERADO")
            {
                /*if (asignado == 2) JLMQ SE COMENTA ESTO PARA QUE TODAS LAS ORDENES DE PRODUCCION PASEN A LIBERAR PRODUCTO Y NO A LIBERAR GRANEL
                {
                    this.Dispose();
                    GC.Collect();
                    //frmUbicacion fu = new frmUbicacion(detalle[8], detalle, int.Parse(detalle[16]), user);
                    //fu.Show();
                    Liberar_Granel lg = new Liberar_Granel(detalle, user);
                    lg.Show();
                }
                else
                {*/
                    //LiberarControles(this);
                    this.Dispose();
                    GC.Collect();
                    //Liberar_Producto lp = new Liberar_Producto(detalle, user); se quita para probar
                    Liberar_Producto lp = new Liberar_Producto(user, detalle,newId);//SE AGREGO NEWID JLMQ 15NOV2018
                    lp.Show();
                //}
            }
            Cursor.Current = Cursors.Default;
        }

        private void Detalle_Orden_Closing(object sender, CancelEventArgs e)
        {
            this.Dispose();
            GC.Collect();
            Revisar_Avance ra = new Revisar_Avance(user);
            ra.Show();
        }
    }
}