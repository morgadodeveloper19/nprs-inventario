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
    public partial class GetInfoOP : Form
    {
        cMetodos c = new cMetodos();
        ValidateOP vop = new ValidateOP();

        string op;
        string error;
        string[] user;
        string[] opInfo = null;

        int columnas;
        int columns;

        public GetInfoOP(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            op = txtOP.Text;

            try
            {
                if (string.IsNullOrEmpty(op))
                {
                    MessageBox.Show("EL CAMPO ORDEN DE PRODUCCION NO PUEDE ESTAR EN BLANCO", "ADVERTENCIA");
                }
                else
                {
                    fillDataGrid(op);
                    dgOrden.Enabled = true;
                    dgOrden.Visible = true;
                    label1.Enabled = true;
                    label1.Visible = true;
                }
            }
            catch (Exception Excp)
            {
                error = Excp.Message;
            }
        }

        public void fillDataGrid(string op)
        {
            try
            {

                DataTable dt = vop.validaOrden(op);
                dgOrden.DataSource = dt;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Hubo un asunto con la conexion. \nFavor de Reintentar en unos momentos", "Advertencia");
                error = exc.Message;
                this.Dispose();
                frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                fmp.Show();
            }
        }

        private void dgOrden_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        

        public void dgOrden_Click(object sender, EventArgs e)
        {
            try
            {
                columns = ((DataTable)dgOrden.DataSource).Columns.Count;
                columnas = ((DataTable)dgOrden.DataSource).Columns.Count;
                opInfo = new string[columnas];

                for (int x = 0; x < columnas; x++)
                {
                    string index = dgOrden.CurrentCell.ToString();
                    int columnIndex = dgOrden.CurrentCell.ColumnNumber;
                    int rowIndex = dgOrden.CurrentCell.RowNumber;
                    string value = dgOrden[rowIndex, x].ToString();
                    opInfo[x] = value;
                }
                string codigo= "bhl0200";
                
                int cantidad = 2;
                ubicacionAlmacenEsc ubica = new ubicacionAlmacenEsc(codigo, opInfo, cantidad, user);
                ubica.Show();
                this.Dispose();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception excep)
            {
                error = excep.Message;
            }
        }
    }
}