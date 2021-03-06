﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1
{
    public partial class frmMenu_Almacen : Form
    {
        string[] user, booya;
        string remision = "";
        
        public frmMenu_Almacen(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
        }

        private void btnAlmacen_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Dispose();
            GC.Collect();
            Produccion.Asignar_Rack paso1 = new SmartDeviceProject1.Produccion.Asignar_Rack(user);
            paso1.Show();
            Cursor.Current = Cursors.Default;

        }

        private void btnPicking_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Dispose();
            GC.Collect();
            Almacen.Picking_Almacen ape = new SmartDeviceProject1.Almacen.Picking_Almacen(user);
            ape.Show();
            Cursor.Current = Cursors.Default;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Dispose();
            GC.Collect();
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
            Cursor.Current = Cursors.Default;
        }

        private void btnReporteMermas_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Dispose();
            Almacen.Re_Ubicacion reu = new SmartDeviceProject1.Almacen.Re_Ubicacion(user);
            reu.Show();
            Cursor.Current = Cursors.Default;
        }

        private void frmMenu_Almacen_Closing(object sender, CancelEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Dispose();
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
            Cursor.Current = Cursors.Default;
        }

        private void bntNvoPicking_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Dispose();
            Almacen.Reportar_Mermas rm = new SmartDeviceProject1.Almacen.Reportar_Mermas(user);
            rm.Show();
            Cursor.Current = Cursors.Default;
        }

        private void btnEscuadra_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Dispose();
            Almacen.Nuevo_Picking np = new SmartDeviceProject1.Almacen.Nuevo_Picking(user);
            np.Show();
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Almacen.CicloRemisiones cr = new SmartDeviceProject1.Almacen.CicloRemisiones(user);
            cr.Show();
            Cursor.Current = Cursors.Default;

        }

    }
}