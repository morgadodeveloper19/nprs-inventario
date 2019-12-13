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

        public Re_Ubicacion()
        {
            InitializeComponent();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        
        }
    }
}