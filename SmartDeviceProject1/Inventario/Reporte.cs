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
    public partial class Reporte : Form
    {
        string[] user;
        string idInv;
        string epc;
        cMetodos cm = new cMetodos();

        public Reporte(string idInven,string[] usuario)
        {
            InitializeComponent();
            user = usuario;
            idInv = idInven;
            fillDataGrid();
        }

        public void fillDataGrid()
        {
            try
            {   //JLMQ En las siguientes dos lineas se cambia EPC por ProdSKU
                DataSet dtLeidos = consulta("SELECT EPC as Codigo, ProdCB as Ubicacion, Cantidad as Piezas FROM DetalleCongelado WHERE idInvCong= " + idInv + " AND Estatus = 0");
                DataSet dtNoLeidos = consulta("SELECT EPC as Codigo, ProdCB as Ubicacion, Cantidad as Piezas FROM DetalleCongelado WHERE idInvCong= " + idInv + " AND Estatus = 1");

                DataGridTableStyle tableStyleLeidos = new DataGridTableStyle();
                tableStyleLeidos.MappingName = dtLeidos.Tables[0].TableName;
                
                GridColumnStylesCollection columnStylesLeidos = tableStyleLeidos.GridColumnStyles;

                DataGridTextBoxColumn columnStyleLeidos = new DataGridTextBoxColumn();
                columnStyleLeidos.MappingName = "Codigo";
                columnStyleLeidos.HeaderText = "EPC";
                columnStyleLeidos.Width = 130;
                columnStylesLeidos.Add(columnStyleLeidos);

                columnStyleLeidos = new DataGridTextBoxColumn();
                columnStyleLeidos.MappingName = "Ubicacion";
                columnStyleLeidos.HeaderText = "Ubicacion";
                columnStyleLeidos.Width = 50;
                columnStylesLeidos.Add(columnStyleLeidos);

                columnStyleLeidos = new DataGridTextBoxColumn();
                columnStyleLeidos.MappingName = "Piezas";
                columnStyleLeidos.HeaderText = "Cantidad";
                columnStyleLeidos.Width = 54;
                columnStylesLeidos.Add(columnStyleLeidos);

                GridTableStylesCollection tableStylesLeidos = dgLeidos.TableStyles;
                tableStylesLeidos.Add(tableStyleLeidos);
                dgLeidos.PreferredRowHeight = 16;
                dgLeidos.RowHeadersVisible = false;


                //------------------------------
                dgLeidos.DataSource = dtLeidos.Tables[0];
                //------------------------------

                DataGridTableStyle tableStyleNoLeidos = new DataGridTableStyle();
                tableStyleNoLeidos.MappingName = dtNoLeidos.Tables[0].TableName;

                GridColumnStylesCollection columnStylesNoLeidos = tableStyleNoLeidos.GridColumnStyles;

                DataGridTextBoxColumn columnStyleNoLeidos = new DataGridTextBoxColumn();
                columnStyleNoLeidos.MappingName = "Codigo";
                columnStyleNoLeidos.HeaderText = "EPC";
                columnStyleNoLeidos.Width = 130;
                columnStylesNoLeidos.Add(columnStyleNoLeidos);

                columnStyleNoLeidos = new DataGridTextBoxColumn();
                columnStyleNoLeidos.MappingName = "Ubicacion";
                columnStyleNoLeidos.HeaderText = "Ubicacion";
                columnStyleNoLeidos.Width = 50;
                columnStylesNoLeidos.Add(columnStyleNoLeidos);

                columnStyleNoLeidos = new DataGridTextBoxColumn();
                columnStyleNoLeidos.MappingName = "Piezas";
                columnStyleNoLeidos.HeaderText = "Cantidad";
                columnStyleNoLeidos.Width = 54;
                columnStylesNoLeidos.Add(columnStyleNoLeidos);

                GridTableStylesCollection tableStylesNoLeidos = dgNoLeidos.TableStyles;
                tableStylesNoLeidos.Add(tableStyleNoLeidos);
                dgNoLeidos.PreferredRowHeight = 16;
                dgNoLeidos.RowHeadersVisible = false;


                //------------------------------
                dgNoLeidos.DataSource = dtNoLeidos.Tables[0];
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

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
        }

        private void dgNoLeidos_CurrentCellChanged(object sender, EventArgs e)
        {
            //JLMQ SE AGREGO TODO ESTO PARA PERMITIR QUE UN TAG SEA FIJADO Y BUSCADO EN BUSCAR TAG
            int rowIndex = dgNoLeidos.CurrentCell.RowNumber;
            string value = dgNoLeidos[rowIndex, 0].ToString();
            epc = value;
            Inventario.Buscar_Tag ibt = new SmartDeviceProject1.Inventario.Buscar_Tag(epc);
            ibt.Show();

        }
    }
}