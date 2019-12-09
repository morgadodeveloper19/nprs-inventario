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
    public partial class Prueba_WS : Form
    {

        SmartDeviceProject1.maiku.Service1 ws = new SmartDeviceProject1.maiku.Service1();

        public Prueba_WS()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Insertadas: " + ws.fillProd(), "Prueba 1:");
            MessageBox.Show("Insertadas: " + ws.fillProd(), "Prueba 2:");
            MessageBox.Show("Insertadas: " + ws.fillProd(), "Prueba 3:");
            MessageBox.Show("Insertadas: " + ws.fillProd(), "Prueba 4:");
            MessageBox.Show("Insertadas: " + ws.fillProd(), "Prueba 5:");
            MessageBox.Show("Insertadas: " + ws.fillProd(), "Prueba 6:");
            MessageBox.Show("Insertadas: " + ws.fillProd(), "Prueba 7:");
            MessageBox.Show("Insertadas: " + ws.fillProd(), "Prueba 8:");
            MessageBox.Show("Insertadas: " + ws.fillProd(), "Prueba 9:");
            MessageBox.Show("Insertadas: " + ws.fillProd(), "Prueba 10:");
        }
    }
}