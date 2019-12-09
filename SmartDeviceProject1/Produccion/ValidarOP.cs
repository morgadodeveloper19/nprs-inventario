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
    public partial class ValidarOP : Form
    {
        cMetodos c = new cMetodos();
        ValidateOP vop = new ValidateOP();

        string[] user;
        string ordenProduccion;
        string[] opInfo = null;

        int cantidad;

        bool valida = false;//si es true total si es false parcialidad

        public ValidarOP(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
            //vop.executeSP();

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)//TOTAL
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                textBox2.Enabled = false;
                textBox2.Visible = false;
                label1.Enabled = true;
                label1.Visible = true;
                label2.Enabled = false;
                label2.Visible = false;
                valida = true;

            }
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)//PARCIAL
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                textBox2.Enabled = true;
                textBox2.Visible = true;
                label1.Enabled = true;
                label1.Visible = true;
                label2.Enabled = true;
                label2.Visible = true;
                valida = false;
                cantidad = Int32.Parse(textBox2.Text);

            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            
        }

        private void menuItem1_Click(object sender, EventArgs e)//REGRESA AL MENU PRINCIPAL
        {
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
        }

        private void BTNValidaOP_Click(object sender, EventArgs e)
        {
            try
            {
                ordenProduccion = textBox1.Text;

                if (string.IsNullOrEmpty(ordenProduccion))
                {
                    MessageBox.Show("EL CAMPO ORDEN DE PRODUCCION NO PUEDE ESTAR EN BLANCO", "ADVERTENCIA");
                }
                else
                {
                    fillDataGrid(ordenProduccion);
                    dgOrdenP.Visible = true;
                    label1.Visible = true;
                    label1.Enabled = true;
                    checkBox1.Enabled = true;
                    checkBox1.Visible = true;
                    checkBox2.Enabled = true;
                    checkBox2.Visible = true;
                    label2.Enabled = true;
                    label2.Visible = true;
                    textBox2.Enabled = true;
                    textBox2.Visible = true;


                }
            }
            catch (Exception exc)
            {

            }
            
        }

        public void fillDataGrid(string op)
        {
            try
            {
                
                DataTable dt = vop.validaOrden(ordenProduccion);
                dgOrdenP.DataSource = dt;                
            }
            catch (Exception e)
            {
                MessageBox.Show("Hubo un asunto con la conexion. \nFavor de Reintentar en unos momentos", "Advertencia");
                this.Dispose();
                frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                fmp.Show();                
            }
        }

        private void dgOrdenP_Click(object sender, EventArgs e)
        {
            try
            {
                int columns;
                int columnas;

                string index;
                int columnIndex;
                int rowIndex;
                string value;

                columns = ((DataTable)dgOrdenP.DataSource).Columns.Count;
                columnas = ((DataTable)dgOrdenP.DataSource).Columns.Count;
                opInfo = new string[columnas];

                for (int x = 0; x < columnas; x++)
                {
                    index = dgOrdenP.CurrentCell.ToString();
                    columnIndex = dgOrdenP.CurrentCell.ColumnNumber;
                    rowIndex = dgOrdenP.CurrentCell.RowNumber;
                    value = dgOrdenP[rowIndex, x].ToString();
                    opInfo[x] = value;
                }

            }
            catch
            { 
            }
        }

        
    }
}