using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1.Inventario
{
    public partial class Reclasificacion : Form
    {
        cMetodos cm = new cMetodos();
        string[] user;
        string codigoSalida = "";
        public Reclasificacion(string[] usu)
        {
            InitializeComponent();
            user = usu;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {

        }

        private void txtSalida_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtSalida.Text))
            {
                codigoSalida = cm.getCodigoEsc(Convert.ToInt32(txtSalida.Text));                
            }
            else
            {
                MessageBox.Show("ESTE CAMPO SOLO ACEPTA VALORES NUMERICOS", "ERROR");
                txtSalida.Text = "";
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
                //MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        private void txtIngreso_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtIngreso.Text))
            {
            }
            else
            {
                MessageBox.Show("ESTE CAMPO SOLO ACEPTA VALORES NUMERICOS", "ERROR");
                txtIngreso.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtReclasificacion.Text))
            {
            }
            else
            {
                MessageBox.Show("ESTE CAMPO SOLO ACEPTA VALORES NUMERICOS", "ERROR");
                txtReclasificacion.Text = "";
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}