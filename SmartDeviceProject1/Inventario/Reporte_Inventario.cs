using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SmartDeviceProject1.Inventario
{
    public partial class Reporte_Inventario : Form
    {
        string[] user;
        cMetodos cm = new cMetodos();
        string idInv;

        public Reporte_Inventario(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
            fillDataGrid();
            if (dataGrid1.VisibleRowCount > 0)
            {
            }
            else
            {
                MessageBox.Show("Usted no cuenta con Inventarios para revisar");
                this.Close();
            }
        }

        public void fillDataGrid()
        {
            try
            {
                DataSet dt = consulta("SELECT idInv, almacen, fecha, cveInv from InvCongelado WHERE usuario = '" + user[4] + "' AND status = 0");
                DataGridTableStyle tableStyle = new DataGridTableStyle();

                tableStyle.MappingName = dt.Tables[0].TableName;

                GridColumnStylesCollection columnStyles = tableStyle.GridColumnStyles;

                DataGridTextBoxColumn columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "idInv";
                columnStyle.HeaderText = "ID";
                columnStyle.Width = 30;
                columnStyles.Add(columnStyle);

                columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "almacen";
                columnStyle.HeaderText = "Ubicación";
                columnStyle.Width = 50;
                columnStyles.Add(columnStyle);

                columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "fecha";
                columnStyle.HeaderText = "Fecha";
                columnStyle.Width = 100;
                columnStyles.Add(columnStyle);

                columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "cveInv";
                columnStyle.HeaderText = "Clave";
                columnStyle.Width = 54;
                columnStyles.Add(columnStyle);

                GridTableStylesCollection tableStyles = dataGrid1.TableStyles;
                tableStyles.Add(tableStyle);
                dataGrid1.PreferredRowHeight = 16;
                dataGrid1.RowHeadersVisible = false;

                //------------------------------
                dataGrid1.DataSource = dt.Tables[0];
                //------------------------------
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Hubo un asunto con la conexion. \nFavor de Reintentar en unos momentos", "Advertencia");
            }
        }

        public DataSet consulta(string select)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = cm.getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                SqlCommand command = new SqlCommand(select, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ds;
        }


        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {
            int columns = ((DataTable)dataGrid1.DataSource).Columns.Count;
            string[] booya = new string[columns];
            for (int x = 0; x < columns; x++)
            {
                string index = dataGrid1.CurrentCell.ToString();
                int columnIndex = dataGrid1.CurrentCell.ColumnNumber;
                int rowIndex = dataGrid1.CurrentCell.RowNumber;
                string value = dataGrid1[rowIndex, x].ToString();
                booya[x] = value;
            }
            idInv = booya[0];
            Reporte r = new Reporte(idInv, user);
            r.Show();
            Cursor.Current = Cursors.Default;
        }

        private void menuItem1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}