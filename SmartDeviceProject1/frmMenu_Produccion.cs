using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SmartDeviceProject1
{
    public partial class frmMenu_Produccion : Form
    {
        
        string[] user;

        public frmMenu_Produccion(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
        }

        private void btnValidar_Orden_Click(object sender, EventArgs e)
        {
            Produccion.ValidarOP ValidateOP = new SmartDeviceProject1.Produccion.ValidarOP(user);
            ValidateOP.Show();
        }

        private void mi_Regresar_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnRevisar_Avance_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Produccion.Revisar_Avance pra = new SmartDeviceProject1.Produccion.Revisar_Avance(user);
            pra.Show();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
            
        }

        private void frmMenu_Produccion_Closing(object sender, CancelEventArgs e)
        {
            this.Dispose();
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Produccion.Asignar_Rack asignarRacks = new SmartDeviceProject1.Produccion.Asignar_Rack(user);
            asignarRacks.Show();
        }
    }
}