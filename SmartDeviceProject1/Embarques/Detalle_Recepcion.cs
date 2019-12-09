using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1.Embarques
{
    public partial class Detalle_Recepcion : Form
    {
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos ws = new cMetodos();
        int res;
        int qty = 0;
        string[] data;
        string tag;
        string[] user;
        string remi;
        int actual = 0;
        int nuevo = 0;
        string code = "";
        string[] detalle = new string[15];
        string[] info = new string[17];
        int x = -1;
        string codigo = "", op = "";
        string newId;
        
        public Detalle_Recepcion(string[] info,int envio,string epc,string[] usuario, string remision, string newIdEsc)
        {
            InitializeComponent();
                newId = newIdEsc;
                labelOrdenProd.Text = info[0];
                lblOP.Text = info[1];//pedido
                lblProd.Text = info[3];
                txtCantidad.Text = info[4];
                qty = int.Parse(info[4]);
                codigo = info[5];
                op = info[0];
                data = info;
                res = envio;
                tag = epc;
                user = usuario;
                remi = remision;
                btnMerma.Enabled = false;
                   
        }

        private void btnMerma_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (res == 2)
                {
                    int cantidad = int.Parse(txtCantidad.Text);
                    if (cantidad > 0)
                    {
                        if (cantidad < qty)
                        {
                            
                            data[4] = cantidad.ToString();
                            int diferencia = qty - cantidad;
                            int prodId = ws.getProdId(op, codigo, newId);
                            int renglon = ws.getRenglon(op, codigo, newId);
                            int result = ws.MermaEmbarques(prodId, cantidad, diferencia, op, codigo, renglon, tag, user[4], user[3], codigo);
                            //int result = ws.MermaEmbarques(prodId, cantidad, diferencia, op, codigo, renglon, tag, UA-00058, 1402, codigo);
                            int nvapzaremi = ws.Pzaremimerma(remi, diferencia);
                            //MessageBox.Show("Resultado " + res);
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Merma Reportada Satisfactoriamente");
                            btnMerma.Enabled = false;
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Las mermas no pueden ser mayores al contenido de la tarima", "Atención");
                            txtCantidad.Text = qty + "";
                            txtCantidad.Focus();
                        }
                    }                                                                                                                     
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Cantidad Invalida", "Atención");
                        txtCantidad.Text = qty + "";
                        txtCantidad.Focus();
                    }
                }
                else if (res == 3)
                {
                    int cantidad = int.Parse(txtCantidad.Text);
                    if (cantidad > 0)
                    {
                        if (cantidad < qty)
                        {
                            data[4] = cantidad.ToString();
                            int prodId = ws.getProdId(data[1], data[8], newId);
                            int renglon = ws.getRenglon(data[1], data[8], newId);
                            int result = ws.MermaEstiba(cantidad, prodId, codigo, op, renglon, user[4], user[3]);
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Resultado " + res);
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Las mermas no pueden ser mayores al contenido de la tarima", "Atención");
                            txtCantidad.Text = qty + "";
                            txtCantidad.Focus();
                        }
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Cantidad Invalida", "Atención");
                        txtCantidad.Text = qty + "";
                        txtCantidad.Focus();
                    }
                }
            }
            catch (Exception ee)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("El campo de cantidad no puede ir en blanco", "Aviso");
            }

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            if (res == 2)
            {
                frmPicking_Escuadras fpe = new frmPicking_Escuadras(2, user);
                fpe.Show();
                GC.Collect();
                this.Dispose();
            }
            else if (res == 3)
            {
                frmPicking_Escuadras fpe = new frmPicking_Escuadras(3, user);
                fpe.Show();
                GC.Collect();
                this.Dispose();
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (res == 2)
            {
                if (ws.embarcaEscuadra(tag,codigo,op,remi,int.Parse("0")) == 0)
                {
                    MessageBox.Show("Tarima embarcada exitosamente");
                    frmPicking_Escuadras fpe = new frmPicking_Escuadras(2, user,remi);
                    fpe.Show();
                    GC.Collect();
                    this.Dispose();
                }
            }
            else if (res == 3)
            {
                if (ws.clearEscuadra(tag, codigo, op) == 0)
                {
                    MessageBox.Show("Tarima verificada para su salida");
                    frmPicking_Escuadras fpe = new frmPicking_Escuadras(3, user,remi);
                    fpe.Show();
                    GC.Collect();
                    this.Dispose();
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void lblMod_Click(object sender, EventArgs e)
        {
            actual = int.Parse(txtCantidad.Text);
            txtCantidad.Enabled = true;
            txtCantidad.Focus();
            btnMerma.Enabled = true;
        }

        private void Detalle_Recepcion_Load(object sender, EventArgs e)
        {
            if (res == 3)
            {
                lblMod.Visible = false;
                btnMerma.Visible = false;
            }
        }        
    }
}