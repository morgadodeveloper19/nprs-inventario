using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1
{
    public partial class frmMenu_Principal : Form
    {
        cMetodos c = new cMetodos();
        string[] user;




        public frmMenu_Principal(string[] usuario)
        {
            
            InitializeComponent();
            user = usuario;
            Cursor.Current = Cursors.Default;
        }

        private void mi_Salir_Click(object sender, EventArgs e)
        {
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {   
            frmMenu_Inventario fmi = new frmMenu_Inventario(user);
            fmi.Show();
        }

        private void btnProduccion_Click(object sender, EventArgs e)
        {
            frmMenu_Produccion fmp = new frmMenu_Produccion(user);
            fmp.Show();
        }

        private void btnAlmacen_Click(object sender, EventArgs e)
        {
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
        }

        private void btnEmbarques_Click(object sender, EventArgs e)
        {
            //frmMenu_Embarques fme = new frmMenu_Embarques(user);
            //fme.Show();
            this.Dispose();
            GC.Collect();
            frmPicking_Escuadras pe = new frmPicking_Escuadras(3, user);
            pe.Show();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}