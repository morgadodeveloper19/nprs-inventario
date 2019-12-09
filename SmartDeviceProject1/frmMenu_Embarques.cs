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
    public partial class frmMenu_Embarques : Form
    {
        string[] user;

        public frmMenu_Embarques(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
        }

        private void btnAlmacen_Click(object sender, EventArgs e)
        {
                this.Dispose();
            GC.Collect();
            frmPicking_Escuadras pe = new frmPicking_Escuadras(2, user);
            pe.Show();
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
            frmPicking_Escuadras pe = new frmPicking_Escuadras(3, user);
            pe.Show();
        }

        private void frmMenu_Embarques_Closing(object sender, CancelEventArgs e)
        {
            this.Dispose();
            GC.Collect();
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
        }
    }
}