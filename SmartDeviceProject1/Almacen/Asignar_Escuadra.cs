using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SmartDeviceProject1.Almacen
{
    public partial class Asignar_Escuadra : Form
    {
        string[] user, booya;
        cMetodos c = new cMetodos();

        public Asignar_Escuadra(string[] sesion)
        {
            InitializeComponent();
            user = sesion;
            fillDataGrid();
        }

        public void fillDataGrid()
        {
            //el que estaba
            DataSet listas = consulta("SELECT de.EPC, ca.[Descripción] as Item, de.Piezas "
				+ "FROM DetEscuadras de INNER JOIN catArt ca "
				+ "ON ca.Clave = de.EPC " 
				+ "WHERE (de.piezas > 0)");
           
            

            if (listas == null)
                return;
            DataGridTableStyle tableStyle = new DataGridTableStyle();

            tableStyle.MappingName = listas.Tables[0].TableName;

            GridColumnStylesCollection columnStyles = tableStyle.GridColumnStyles;

            DataGridTextBoxColumn columnStyle = new DataGridTextBoxColumn();
            columnStyle.MappingName = "EPC";
            columnStyle.HeaderText = "EPC";
            columnStyle.Width = 80;
            columnStyles.Add(columnStyle);

            columnStyle = new DataGridTextBoxColumn();
            columnStyle.MappingName = "Item";
            columnStyle.HeaderText = "Articulo";
            columnStyle.Width = 80;
            columnStyles.Add(columnStyle);

            columnStyle = new DataGridTextBoxColumn();
            columnStyle.MappingName = "Piezas";
            columnStyle.HeaderText = "Cantidad";
            columnStyle.Width = 65;
            columnStyles.Add(columnStyle);

            GridTableStylesCollection tableStyles = dataGrid1.TableStyles;
            tableStyles.Add(tableStyle);
            dataGrid1.PreferredRowHeight = 16;
            dataGrid1.RowHeadersVisible = false;

            //------------------------------
            dataGrid1.DataSource = listas.Tables[0];
            //------------------------------
        }

        public DataSet consulta(string select)
        {
            DataSet ds = new DataSet();
            try
            {
                //string[] parametros = c.getParametros("Intelisis");
                string[] parametros = c.getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                SqlCommand command = new SqlCommand(select, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }        


        private void mi_Regresar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {
            int columns = ((DataTable)dataGrid1.DataSource).Columns.Count;
            booya = new string[columns];
            for (int x = 0; x < columns; x++)
            {
                string index = dataGrid1.CurrentCell.ToString();
                int columnIndex = dataGrid1.CurrentCell.ColumnNumber;
                int rowIndex = dataGrid1.CurrentCell.RowNumber;
                string value = dataGrid1[rowIndex, x].ToString();
                booya[x] = value;
            }
            panel1.Visible = true;
            lblCodProd.Text = booya[0];
            lblDescProd.Text = booya[1];
            lblCta.Text = booya[2];
            
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int completo = int.Parse(lblCta.Text);
            if (completo > 0)
            {
                panel1.Visible = false;
                //Nueva_Escuadra ne = new Nueva_Escuadra(user, booya);
                //ne.Show();
            }

            else
            {
                MessageBox.Show("No hay existencia de este producto en Inventario");
                panel1.Visible = false;
            }
        }
    }
}