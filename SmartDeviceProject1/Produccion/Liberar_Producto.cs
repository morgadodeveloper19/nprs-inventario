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
    public partial class Liberar_Producto : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.NapresaSitio.Service1 ws = new SmartDeviceProject1.NapresaSitio.Service1();
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos c = new cMetodos();
        
        string[] user;
        string[] detalle;
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

        public Liberar_Producto(string[] usuario, string[] folio, string newIdSql)
        {
            InitializeComponent();
            try
            {
                newId = newIdSql;
                detalle = folio;
                user = usuario;
                lblPedido.Text = folio[2];
                lblOrdProd.Text = folio[1];
                lblCliente.Text = folio[3];
                int current = c.TarimasAsignadas(folio[1]);
                int total = c.calculaTarima(folio[8], int.Parse(folio[9]));
                lblTarima.Text = "Tarima " + current + " de " + total;
                lbTipo.Text = "Tipo: " + folio[5];
                lblMedida.Text = "Medida: " + folio[6];
                lblColor.Text = "Color: " + folio[7];
                lblResistencia.Text = "";// folio[8];
                lblCodigo.Text = "Codigo: " + folio[8];
                lblQty.Text = folio[16];
                label5.Text = folio[4];
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                MessageBox.Show("Hubo un problema con la carga de información.\nPor favor intentelo nuevamente.", "Advertencia");
                //LiberarControles(this);
                this.Dispose();
                GC.Collect();
                frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                fmp.Visible = true;
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int res3 = c.clearRack(detalle[1], detalle[11],newId);
            if (res3 == 0)
            {
                Asignar_Escuadra ae = new Asignar_Escuadra(detalle, user, int.Parse(lblQty.Text), newId);
                //LiberarControles(this);
                this.Dispose();
                GC.Collect();
                ae.Show();
            }
            else
            {
                MessageBox.Show("Por el momento no se pudo liberar el producto.\nIntentelo de nuevo después");
            }
            Cursor.Current = Cursors.Default;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            Revisar_Avance ra = new Revisar_Avance(user);
            ra.Show();
        }

        private void Liberar_Producto_Closing(object sender, CancelEventArgs e)
        {
            this.Dispose();
            GC.Collect();
            Revisar_Avance ra = new Revisar_Avance(user);
            ra.Show();
        }
    }
}