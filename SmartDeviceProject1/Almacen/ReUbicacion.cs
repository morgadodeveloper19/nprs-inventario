using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1.Almacen
{
    public partial class Re_Ubicacion : Form
    {
        cMetodos cm = new cMetodos();
        string error = "";
        string codigo = "";
        string descripcion = "";
        string ubicacion = "";
        int piezas = 0;
        string[] user;


        public Re_Ubicacion(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
            string query = "SELECT Descripcion AS Items, Descripcion AS ID FROM ZonaBustamante";
            llenaComboBox(cbZonas, "Items", "ID", query, cMetodos.CONEXION);
        }

        

        public void llenaComboBox(ComboBox Objeto, string nomCve, string idCve, string consulta, string conex)
        {
            DataTable dt = cm.getDatasetConexionWDR(consulta, conex);
            if (dt == null)
            {
                MessageBox.Show("NO SE PUEDE CONSULTAR LAS UBICACIONES EN ESTE MOMENTO", "ERROR");
                this.Close();
                return;
            }

            Objeto.DataSource = null;
            Objeto.DataSource = dt;
            Objeto.DisplayMember = nomCve;
            Objeto.ValueMember = idCve;
            dt.Columns[0].MaxLength = 255;
            DataRow dr = dt.NewRow();
            string opcSelec = "SELECCIONAR UBICACIÓN";
            dr[nomCve] = (dt.Rows.Count > 0) ? opcSelec : "NO HAY UBICACIONES DISPONIBLES";
            dr[idCve] = 0;
            try
            {
                dt.Rows.InsertAt((dr), 0);
            }
            catch (Exception e)
            {
                dt.Columns[0].MaxLength = 255;
                dt.Rows.InsertAt((dr), 0);
                //throw;
            }
            Objeto.SelectedValue = 0;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (isDigit(textBox1.Text))
                {
                    if (textBox1.Text.Length > 0)
                    {
                        codigo = cm.getCodigoEsc(Convert.ToInt32(textBox1.Text));
                        if (codigo == "SIN CODIGO")
                        {
                            lbCodigo.Enabled = true;
                            lbCodigo.Visible = true;
                            lbDescripcion.Enabled = true;
                            lbDescripcion.Visible = true;
                            lbPzas.Enabled = true;
                            lbPzas.Visible = true;
                            lbCodigo.Text = "SIN CODIGO";
                            lbDescripcion.Text = "SIN DESCRIPCIÓN";
                            lbPzas.Text = "SIN PIEZAS";
                        }
                        else
                        {
                            lbCodigo.Enabled = true;
                            lbCodigo.Visible = true;
                            lbDescripcion.Enabled = true;
                            lbDescripcion.Visible = true;
                            lbPzas.Enabled = true;
                            lbPzas.Visible = true;
                            lbSelecciona.Enabled = true;
                            lbSelecciona.Visible = true;

                            lbCodigo.Text = codigo;
                            descripcion = cm.getDescripcionCodigo(codigo);
                            lbDescripcion.Text = descripcion;
                            piezas = cm.pzaEscReclasificacion(Convert.ToInt32(textBox1.Text));
                            lbPzas.Text = piezas.ToString();
                            cbZonas.Enabled = true;
                            cbZonas.Visible = true;
                            menuItem2.Enabled = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ESTE CAMPO SOLO ACEPTA VALORES NUMERICOS", "ERROR");
                    textBox1.Text = "";
                }
            }
            catch (Exception exep)
            {
                error = exep.Message;
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                ubicacion = cbZonas.SelectedValue.ToString();
                if (ubicacion == "0")
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("PRIMERO ELIGE UNA UBICACIÓN POR FAVOR", "ERROR");
                }
                else
                {
                    DialogResult usuElige = MessageBox.Show("VAS A REUBICAR UN '"+codigo+"' AL '"+ubicacion+"'¿DESEAS CONTINUAR?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    if (usuElige == DialogResult.Yes)
                    { 
                        //HACER UPDATE
                        Cursor.Current = Cursors.Default;
                        DialogResult exito = MessageBox.Show("MATERIAL REUBICADO CON EXITO", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        this.Close();
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }

            }
            catch(Exception exep)
            {
                error = exep.Message;
                Cursor.Current = Cursors.Default;
                DialogResult exito = MessageBox.Show("NO SE PUDO REUBICAR EL MATERIAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            }
        }
    }
}