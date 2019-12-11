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
        string descSalida = "";
        string codigoIngreso = "";
        string descIngreso = "";
        int pzaReclasificacion = 0;
        int pzaEsc = 0;

        public Reclasificacion(string[] usu)
        {
            InitializeComponent();
            user = usu;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            DialogResult usuElige = MessageBox.Show("VAS A PASAR "+txtReclasificacion.Text+" PIEZAS DE '"+codigoSalida+"' DEL TAG "+txtSalida.Text+" AL TAG "+txtIngreso.Text+" ¿DESEAS CONTINUAR?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            if (usuElige == DialogResult.Yes)
            {
                pzaReclasificacion = (Convert.ToInt32(txtReclasificacion.Text));
                pzaEsc = cm.pzaEscReclasificacion(Convert.ToInt32(txtSalida.Text));
                if (pzaReclasificacion <= pzaEsc)
                {
                    MessageBox.Show("RECLASIFICACION EXITOSA", "AVISO");
                    //hacer update en las escuadras seleccionadas. Y MOVIMIENTO EN INTELISIS
                }
                else
                {
                    MessageBox.Show("NO PUEDES RECLASIFICAR MAS DE LO QUE TIENES EN LA ESCUADRA DE SALIDA, VERIFICA LAS PIEZAS A RE-CLASIFICAR","ERROR");
                }
            }
            else
            {
            }
        }

        private void txtSalida_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtSalida.Text))
            {
                
                if (txtSalida.Text.Length > 0)
                {
                    codigoSalida = cm.getCodigoEsc(Convert.ToInt32(txtSalida.Text));
                    lbCodigo.Text = codigoSalida;
                    lbCodigo.Enabled = true;
                    lbCodigo.Visible = true;
                    descSalida = cm.getDescripcionCodigo(codigoSalida);
                    lbDescripcion.Text = descSalida;
                    lbDescripcion.Enabled = true;
                    lbDescripcion.Visible = true;
                }
                else
                {
                    MessageBox.Show("INGRESA UN NUMERO MAYOR A CERO", "ERROR");
                    lbCodigo.Text = "";
                    lbDescripcion.Text = "";
                }
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
                if (txtIngreso.Text.Length > 0)
                {
                    codigoIngreso = cm.getCodigoEsc(Convert.ToInt32(txtIngreso.Text));
                    lblCodigo.Text = codigoIngreso;
                    descIngreso = cm.getDescripcionCodigo(codigoIngreso);
                    lblDescripcion.Text = descIngreso;
                    lblDescripcion.Enabled = true;
                    lblDescripcion.Visible = true;
                }
                else
                {
                    MessageBox.Show("INGRESA UN NUMERO MAYOR A CERO", "ERROR");
                    lblCodigo.Text = "";
                }
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