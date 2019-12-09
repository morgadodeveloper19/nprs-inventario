using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace test_emulator
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string epc = "";
            if(TestEmulator.isEmulator()){
                Dialog frm = new Dialog();
                frm.ShowDialog();
                epc = frm.EPC;
                MessageBox.Show(epc);
            }
            
        }
    }
}