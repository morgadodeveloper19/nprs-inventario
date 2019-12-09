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
    public partial class Liberar_Granel : Form
    {
        string[] user, folios;
        int contador = 0,actual;
        cMetodos cm = new cMetodos();
        bool res;

        public Liberar_Granel(string[] detalle, string[] usuario)
        {
            InitializeComponent();
            Cursor.Current = Cursors.Default;
            folios = detalle;
            user = usuario;
            lblPedido.Text = detalle[2];
            lblOrdProd.Text = detalle[1];
            lblCliente.Text = detalle[3];
            txtMerma.Text = detalle[16];
            label5.Text = detalle[4];
            actual = int.Parse(detalle[16]);
        }

        private void btnMerma_Click(object sender, EventArgs e)
        {
            if (contador == 0)
            {
                txtMerma.Enabled = true;
                txtMerma.Focus();
                menuItem2.Enabled = false;
                contador++;
            }
            else if (contador == 1)
            {
                Cursor.Current = Cursors.WaitCursor;
                MermaEstiba();
                if (res)
                {
                    MessageBox.Show("Merma Reportada Satsfactoriamente");
                    contador = 0;
                    txtMerma.Enabled = false;
                    menuItem2.Enabled = true;
                }
                else 
                {
                    MessageBox.Show("Hubo un problema al reportar la merma. Por favor intentelo nuevamente", "Advertencia");
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void MermaEstiba()
        {
            int nvaCant = int.Parse(txtMerma.Text);
            int diferencia = 0;
            if (actual > nvaCant)
            {
                diferencia = actual - nvaCant;
                cm.MermaEstibaTarima(diferencia, int.Parse(folios[0]), folios[8], folios[1], int.Parse(folios[11]), user[4], user[3],actual);
                res = true;
                //ws.insertMovinMerma(4, folios[1], int.Parse(folios[0]), 0, epece, folios[8], nvaCxt, "APT-BUS");
            }
            else
            {
                MessageBox.Show("La cantidad a mermar no puede ser mayor a la cantidad inicial de la tarima.", "Aviso");
                res = false;
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            //if (cm.clearRack(folios[1], folios[11]) == 0)
            //{
            //    if (cm.liberaGranel(folios[8], folios[1], folios[8], int.Parse(txtMerma.Text)))
            //    {
            //        string res = cm.avanzarEstado(folios[1], folios[10], folios[0], folios[11], int.Parse(folios[16]), user[4]);
            //        if (res.Contains("Exitoso"))
            //        {
            //            //ws.insertMovinEntrada(2, folios[1], int.Parse(folios[2]), 0, epece, folios[8], int.Parse(txtPXT.Text), "APT-BUS");
            //            MessageBox.Show(res, "Exito");
            //            GC.Collect();
            //            frmMenu_Principal fmp = new frmMenu_Principal(user);
            //            fmp.Show();
            //            this.Dispose();
            //        }
            //        else
            //        {
            //            Cursor.Current = Cursors.Default;
            //            MessageBox.Show(res, "Advertencia");
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Hubo un problema al liberar el producto. Por favor intentelo nuevamente", "Advertencia");
            //}
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}