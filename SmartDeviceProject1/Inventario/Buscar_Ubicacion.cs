using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Collections;
using Intermec.DataCollection;

namespace SmartDeviceProject1.Inventario
{
    public partial class Buscar_Ubicacion : Form
    {
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos ws = new cMetodos();
        SqlCeConnection conexion = new SqlCeConnection("Data Source=\\Flash File Store\\SSPB\\GoMonitor.sdf;");
        DataTable dataTable = new DataTable();
        
        public Buscar_Ubicacion(ArrayList EPCS)
        {
            InitializeComponent();
            llenaDataTable();
            llenaGrid(EPCS);
        }

        public void llenaGrid(ArrayList EPC)
        {
            int cuantos = EPC.Count;
            for (int y = 0; y < cuantos; y++)
            {
                DataRow dr = dataTable.NewRow();
                dr["EPCArt"] = EPC[y].ToString();
                dataTable.Rows.Add(dr);
            }
        }

        public void llenaDataTable()
        {
            dataTable.Columns.Add("EPCArt", typeof(String));

            DataGridTableStyle tableStyle = new DataGridTableStyle();
            GridColumnStylesCollection columnStyles = tableStyle.GridColumnStyles;
            DataGridTextBoxColumn columnStyle = new DataGridTextBoxColumn();
            columnStyle.MappingName = "EPCArt";
            columnStyle.HeaderText = "EPC";
            columnStyle.Width = 200;
            columnStyles.Add(columnStyle);

            GridTableStylesCollection tableStyles = dataGrid1.TableStyles;
            tableStyles.Add(tableStyle);
            dataGrid1.PreferredRowHeight = 16;
            dataGrid1.RowHeadersVisible = false;
            dataGrid1.DataSource = dataTable;
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            string epc;
            try
            {
                int rowIndex = dataGrid1.CurrentRowIndex;
                epc = dataGrid1[rowIndex, 0].ToString();
                Buscar_Tag bt = new Buscar_Tag(epc);
                bt.Show();
                //string consulta = "select z.ClaveZona as Zona, r.Clave as Rack, n.Clave as Nivel, v.Clave as Vent, p.Clave as Pos from posiciones p INNER JOIN ventanas v ON p.IDVentana = v.IDVentana INNER JOIN niveles n ON n.IDNivel = v.IDNivel INNER JOIN racks r ON r.IDRack = n.IDRack INNER JOIN Zonas z on z.IdZona = r.IDZona where p.EPCArt = '" + epc + "'";
                //DataTable todo = ws.getDatasetWDR(consulta);
                //DataRow datos = todo.Rows[0];
                //MessageBox.Show("Zona:" + datos[0].ToString() + "\nRack:" + datos[1].ToString() + "\nVentana:" + datos[3].ToString() + "\nPos:" + datos[4].ToString(), "UBICACION");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
        }

    }
}