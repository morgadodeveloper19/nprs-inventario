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
    public partial class Dialog : Form
    {
        private String _epc;
    
        public Dialog()
        {
            InitializeComponent();
        }

        public String EPC
        {
            get
            {
                return _epc;
            }
            set
            {
                _epc = value;
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            EPC = "";
            this.Close();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            EPC = txtEpc.Text;
            this.Close();
        }
    }
}