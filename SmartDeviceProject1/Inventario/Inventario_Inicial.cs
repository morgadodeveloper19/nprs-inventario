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
    public partial class Inventario_Inicial : Form
    {
        string[] user;
        cMetodos cm = new cMetodos();

        public Inventario_Inicial(string [] usuario)
        {
            InitializeComponent();
            user = usuario;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmMenu_Inventario fmi = new frmMenu_Inventario(user);
            fmi.Show();
        }

        /*private void ckbZona_CheckStateChanged(object sender, EventArgs e)
        {
            if (ckbZona.Checked == true)
            {
                llenarCombo(ComboZona);
                ckbRack.Enabled = true;

            }

        }*/
        private void ckbZona_CheckStateChanged(object sender, EventArgs e)
        {
            
            if (ckbZona.Checked == true)
            {
                //llenaCBConexion(cbZonas, "ClaveZona", "ClaveZona", "select ClaveZona, ClaveZona from zonas where idAlmacen = '" + cbAlmacen.SelectedValue.ToString() + "'", "ConsolaAdmin", 0);
                //ckbRack.Enabled = true;
                
            }
            if (ckbZona.Checked == false)
            {
                ckbRack.Checked = false;
                //ckbPosicion.Checked = false;
                //cbZonas.DataSource = null;
                //cbRacks.DataSource = null;
                //cbPosiciones.DataSource = null;
            }
            
        }

        private void ckbRack_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (ckbRack.Checked == true)
                {
                    //llenaCBConexion(cbRacks, "Clave", "Clave", "SELECT r.Clave, r.Clave FROM racks as r INNER JOIN Zonas as z on r.IDZona = z.IdZona WHERE z.claveZona = '" + cbZonas.SelectedValue.ToString() + "'", "ConsolaAdmin", 0);
                    //ckbPosicion.Enabled = true;
                }
                if (ckbRack.Checked == false)
                {
                    //this.cbRacks.DataSource = null;
                    //ckbPosicion.Checked = false;
                    //cbPosiciones.DataSource = null;
                }
                
            }
            catch (NullReferenceException nre) { Console.Write(nre.Message); }
            catch (Exception ex) { Console.Write(ex.Message); }
        }

        

        public void llenarCombo(ComboBox cb)
        {
            try
            {
                string consulta = "";
                string[] parametros = cm.getParametros("Soluitia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                conn.Open();
                consulta = "select ClaveZona from zonas";
                SqlCommand cmdDestino = new SqlCommand(consulta, conn);
                SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                while (readerDestino.Read())
                {
                    
                    cb.Items.Add(readerDestino["ClaveZona"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se lleno el ComboBox: " + ex.ToString());
            }
        }

        
    }
}