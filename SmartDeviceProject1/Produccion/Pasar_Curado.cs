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
    public partial class Pasar_Curado : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.NapresaSitio.Service1 ws = new SmartDeviceProject1.NapresaSitio.Service1();
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos c = new cMetodos();

        int cant = 0;
        string[] user;
        string fol = "";

        public static void LiberarControles(System.Windows.Forms.Control control)
        {
            for (int i = 0; i <= control.Controls.Count - 1; i++)
            {
                if (control.Controls[i].Controls.Count > 0)
                    LiberarControles(control.Controls[i]);
                control.Controls[i].Dispose();
            }
        }

        public Pasar_Curado(string[] usuario, string[] folio)
        {
            InitializeComponent();
            try
            {
                fol = folio[1];
                lblFolio.Text = fol;
                lblOP.Text = folio[2];
                lblProd.Text = folio[4];
                txtCant.Text = folio[15];
                cant = int.Parse(folio[14]);
                user = usuario;
                fol = folio[1];
                LblRenglon.Text = folio[11];
                lblId.Text = folio[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Hubo un problema con la carga de información.\nPor favor intentelo nuevamente.", "Advertencia");
                //LiberarControles(this);
                this.Dispose(); 
                GC.Collect();
                frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                fmp.Show();
            }
        }

        private void lblMod_Click(object sender, EventArgs e)
        {
            txtCant.Enabled = true;
        }

        private void txtCant_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtCant.Text))
            {
            }
            else
            {
                MessageBox.Show("Este campo solo acepta valores númericos", "Advertencia");
            }
        }

        public bool isDigit(string text)
        {
            char[] cArray = text.ToCharArray();
            int x = 0;
            try
            {
                while (x < cArray.Length)
                {
                    Int32.Parse(cArray[x].ToString());
                    x++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //LiberarControles(this);
            Revisar_Avance ra = new Revisar_Avance(user);
            ra.Show();
            this.Dispose();
            GC.Collect();
            
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string res = c.avanzarEstado(fol, "CURADO", lblId.Text, LblRenglon.Text, int.Parse(txtCant.Text), user[4]);
            //    MessageBox.Show(res);
            //    //LiberarControles(this);
            //    this.Dispose(); GC.Collect();
            //    frmMenu_Produccion fpo = new frmMenu_Produccion(user);
            //    fpo.Visible = true;
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show("Hubo un error al momento de generar el Avance. Revise que:\n-No existan letras en el campo Cantidad.\n-El campo Cantidad no este en blanco.", "Aviso");
            //}
        }

        private void Pasar_Curado_Closing(object sender, CancelEventArgs e)
        {
            Revisar_Avance ra = new Revisar_Avance(user);
            ra.Show();
            this.Dispose();
            GC.Collect();
        }
    }
}