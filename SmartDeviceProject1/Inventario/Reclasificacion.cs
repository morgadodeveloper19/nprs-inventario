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
        int pzasEscSalida = 0;
        int pzasEscIngreso = 0;
        int difSalida = 0;
        int difIngreso = 0;
        int tagSalida = 0;
        int tagIngreso = 0;
        bool reclasificacionExitosa = false;
        string recla = "";
        bool reclasificacion = false;
        string pedido = "";


        public Reclasificacion(string[] usu)
        {
            InitializeComponent();
            user = usu;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            txtSalida.Enabled = false;
            txtIngreso.Enabled = false;
            txtReclasificacion.Enabled = false;
            pedido = txtPedido.Text;

            try
            {
                recla = txtReclasificacion.Text;
                if (!(string.IsNullOrEmpty(recla)))
                {
                    pzaReclasificacion = (Convert.ToInt32(txtReclasificacion.Text));
                    if (pzaReclasificacion > 0)
                    {
                        tagSalida = Convert.ToInt32(txtSalida.Text);
                        tagIngreso = Convert.ToInt32(txtIngreso.Text);
                        if (tagSalida == tagIngreso)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("EL TAG DE SALIDA Y EL TAG DE INGRESO SON IGUALES REVISA POR FAVOR","ERROR");
                            txtSalida.Enabled = true;
                            txtIngreso.Enabled = true;
                            txtReclasificacion.Enabled = true;
                        }
                        else
                        {
                            DialogResult usuElige = MessageBox.Show("VAS A PASAR " + txtReclasificacion.Text + " PIEZAS DE '" + codigoSalida + "' DEL TAG " + txtSalida.Text + " AL TAG " + txtIngreso.Text + " ¿DESEAS CONTINUAR?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            if (usuElige == DialogResult.Yes)
                            {


                                pzasEscSalida = cm.pzaEscReclasificacion(tagSalida);
                                pzasEscIngreso = cm.pzaEscReclasificacion(tagIngreso);
                                if (pzaReclasificacion <= pzasEscSalida)
                                {
                                    difSalida = pzasEscSalida - pzaReclasificacion;
                                    difIngreso = pzasEscIngreso + pzaReclasificacion;
                                    reclasificacion = cm.reclasificacion(pzaReclasificacion, user[3], codigoSalida, codigoIngreso, user[4], pedido);
                                    if (reclasificacion == true)
                                    {
                                        reclasificacionExitosa = cm.updateReclasificacion(tagSalida, tagIngreso, difSalida, difIngreso);
                                        if (reclasificacionExitosa == true)
                                        {
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("RECLASIFICACION EXITOSA", "AVISO");
                                            this.Close();
                                        }
                                        else
                                        {
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("ERROR AL ACTUALIZAR EL SERVIDOR RFID", "AVISO");
                                            this.Close();
                                        }
                                    }
                                    else
                                    {
                                        Cursor.Current = Cursors.Default;
                                        MessageBox.Show("NO SE PUDO REALIZAR LA RE CLASIFICACIÓN", "ERROR");
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("NO PUEDES RECLASIFICAR MAS DE LO QUE TIENES EN LA ESCUADRA DE SALIDA, VERIFICA LAS PIEZAS A RE-CLASIFICAR", "ERROR");
                                    txtReclasificacion.Enabled = true;
                                    txtReclasificacion.Text = "";
                                }
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                txtSalida.Enabled = true;
                                txtIngreso.Enabled = true;
                                txtReclasificacion.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("NO PUEDES RE-CLASIFICAR CERO PIEZAS, INGRESA UNA CANTIDAD VALIDA PARA RE-CLASIFICAR", "ERROR");
                        txtReclasificacion.Enabled = true;
                        txtReclasificacion.Text = "";
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("EL CAMPO RE-CLASIFICAR ESTA VACIO INGRESA UN VALOR VALIDO POR FAVOR", "ERROR");                    
                    txtReclasificacion.Enabled = true;
                }
            }
            catch (Exception ejec)
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtSalida_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (isDigit(txtSalida.Text))
                {

                    if (txtSalida.Text.Length > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        codigoSalida = cm.getCodigoEsc(Convert.ToInt32(txtSalida.Text));
                        if (codigoSalida == "SIN CODIGO")
                        {
                            lbCodigo.Text = codigoSalida;
                            descSalida = cm.getDescripcionCodigo(codigoSalida);
                            lbDescripcion.Text = descSalida;
                            label2.Enabled = false;
                            label2.Visible = false;
                            txtIngreso.Enabled = false;
                            txtIngreso.Visible = false;
                        }
                        else
                        {
                            lbCodigo.Text = codigoSalida;
                            lbCodigo.Enabled = true;
                            lbCodigo.Visible = true;
                            descSalida = cm.getDescripcionCodigo(codigoSalida);
                            lbDescripcion.Text = descSalida;
                            lbDescripcion.Enabled = true;
                            lbDescripcion.Visible = true;
                            label2.Enabled = true;
                            label2.Visible = true;
                            txtIngreso.Enabled = true;
                            txtIngreso.Visible = true;
                        }

                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        lbCodigo.Text = "";
                        lbDescripcion.Text = "";
                        label2.Enabled = false;
                        label2.Visible = false;
                        txtIngreso.Enabled = false;
                        txtIngreso.Visible = false;
                        Cursor.Current = Cursors.Default;

                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("ESTE CAMPO SOLO ACEPTA VALORES NUMERICOS", "ERROR");
                    txtSalida.Text = "";
                }
            }
            catch (Exception epc)
            {
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
            try
            {
                if (isDigit(txtIngreso.Text))
                {
                    if (txtIngreso.Text.Length > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        codigoIngreso = cm.getCodigoEsc(Convert.ToInt32(txtIngreso.Text));
                        if (codigoIngreso == "SIN CODIGO")
                        {
                            lblCodigo.Text = codigoIngreso;
                            descIngreso = cm.getDescripcionCodigo(codigoIngreso);
                            lblDescripcion.Text = descIngreso;
                            label3.Enabled = false;
                            label3.Visible = false;
                            txtReclasificacion.Enabled = false;
                            txtReclasificacion.Visible = false;
                            menuItem2.Enabled = false;
                        }
                        else
                        {
                            lblCodigo.Text = codigoIngreso;
                            descIngreso = cm.getDescripcionCodigo(codigoIngreso);
                            lblDescripcion.Text = descIngreso;
                            lblDescripcion.Enabled = true;
                            lblDescripcion.Visible = true;
                            label3.Enabled = true;
                            label3.Visible = true;
                            txtReclasificacion.Enabled = true;
                            txtReclasificacion.Visible = true;
                            menuItem2.Enabled = true;
                        }

                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {                       
                        lblCodigo.Text = "";
                        label3.Enabled = false;
                        label3.Visible = false;
                        txtReclasificacion.Enabled = false;
                        txtReclasificacion.Visible = false;
                        menuItem2.Enabled = false;
                        lblDescripcion.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("ESTE CAMPO SOLO ACEPTA VALORES NUMERICOS", "ERROR");
                    txtIngreso.Text = "";
                }
            }
            catch (Exception exp)
            {
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