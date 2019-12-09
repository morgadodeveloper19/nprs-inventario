using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SmartDeviceProject1.Almacen
{
    public partial class Detalle_Recepcion : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();        
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();

        cMetodos ws = new cMetodos();
        bool escuadra;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        
        //I'd double check this constant, just in case
        static uint WM_CLOSE = 0x10;
        
        public void CloseWindow(IntPtr hWindow)
        {
            SendMessage(hWindow, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        int actual = 0;
        int nuevo = 0;
        string tag = "";
        string code = "";
        string[] detalle = new string[15];
        string[] info = new string[18];
        int x = -1;
        string[] user;
        bool mEstiba = true;
        string newId;
        
        public Detalle_Recepcion(string[] folio, string epc, string[] usuario,bool tarima)
        {
            InitializeComponent();
            user = usuario;
            escuadra = tarima;
            detalle = folio;
                labelOrdenProd.Text = detalle[0];
            lblProd.Text = detalle[2];
            int CxEsc = ws.CxEsc(epc, detalle[0], detalle[4]);
            if (CxEsc >= 0)
            {
                txtCantidad.Text = ws.CxEsc(epc, detalle[0], detalle[4]) + "";
                lblOP.Text = detalle[1];
                code = detalle[4];
                tag = epc;
            }
            else
            {
                MessageBox.Show("Hubo un problema con la comunicación. Por favor intentelo nuevamente");
            }
            newId = detalle[5];
            actual = int.Parse(txtCantidad.Text);
        }

        public static void LiberarControles(System.Windows.Forms.Control control)
        {
            for (int i = 0; i <= control.Controls.Count - 1; i++)
            {
                if (control.Controls[i].Controls.Count > 0)
                    LiberarControles(control.Controls[i]);
                control.Controls[i].Dispose();
            }
        }

        private void btnMerma_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                nuevo = int.Parse(txtCantidad.Text);
                if (nuevo > actual)
                {
                    MessageBox.Show("La nueva cantidad no puede ser mayor a la cantidad inicial", "Advertencia");
                }
                else
                {
                    int total = actual - nuevo;
                    int id = ws.getProdId(detalle[0], detalle[4], newId);//pasa como parametros OP y Codigo del Producto
                    int reng = ws.getRenglon(detalle[0], detalle[4],newId);
                    int res;
                    int bandera = 1;
                    if(escuadra)
                        res = ws.mermaRecepcion(total, id, detalle[4], detalle[0], reng, user[4], user[3],nuevo,tag, bandera, newId);
                    else
                        res = ws.mermaRecepcionTarima(total,id,detalle[4],detalle[0],reng,user[4],user[3],nuevo,tag,newId);
                    //int res = ws.mermaRecepcion(id, total, nuevo, detalle[1], detalle[4], reng, user[4], user[3]);
                    if (res == 0)
                    {
                        btnMerma.Enabled = false;
                        txtCantidad.Enabled = false;
                        mEstiba = false;
                        MessageBox.Show("Merma Reportada Satisfactoriamente");
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al momento de reportar la merma\nPor favor intentelo nuevamente", "Advertencia");
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("El campo de cantidad no puede ir en blanco", "Aviso");
            }
            Cursor.Current = Cursors.Default;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtCantidad.Text))
            {
            }
            else
            {
                MessageBox.Show("Este campo solo acepta valores númericos", "Advertencia");
                txtCantidad.Text = "";
                txtCantidad.Focus();
            }
        }
    
        public bool isDigit(string text)
        {
            char[] cArray = text.ToCharArray();
            int x = 0;
            try

            {
                while (x < cArray.Length)
                {
                    Int32.Parse(cArray[x].ToString());
                    x++;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (txtCantidad.Text.Length > 0)
            {
                
                btnMerma.Enabled = false;
                txtCantidad.Enabled = false;
                MessageBox.Show("Movimiento Correcto","Exito");
                info = ws.detalleProd(detalle[0], detalle[4], newId);
                // escuadra = true o false

                this.Dispose();
                Cursor.Current = Cursors.Default;
                
            }
            
        }

        private void lblMod_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            actual = int.Parse(txtCantidad.Text);
            txtCantidad.Enabled = true;
            txtCantidad.Focus();
            btnMerma.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void label3_ParentChanged(object sender, EventArgs e)
        {

        }

        private void lblCantidad_ParentChanged(object sender, EventArgs e)
        {

        }

    }
}