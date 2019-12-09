using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1.Produccion
{
    public partial class Buscar_OrdenProduccion : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos c = new cMetodos();

        
        int flag = 0;
        string[] user;

        public Buscar_OrdenProduccion(string[] usuario)
        {
            InitializeComponent();
            textBox1.Focus();
            user = usuario;
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                 int transferenciasIncompletas = c.verificaOP(textBox1.Text);
                int ordenesCompletas = c.checkComplete(textBox1.Text);
                int asignacionCompleta = c.asignadaOP(textBox1.Text);
                if (asignacionCompleta == 0)
                {
                    if (transferenciasIncompletas == 0)
                    {
                        if (ordenesCompletas == 0)
                        {
                            MessageBox.Show("La orden seleccionada puede comenzar a producirse");
                            fillDataGrid("APROBADO");
                            dgOrden.Visible = true;
                            menuItem2.Text = "AVANZAR";
                            btnVerificar.Visible = false;
                            menuItem2.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("La orden seleccionada ya se ha completado");
                            textBox1.Text = "";
                            textBox1.Focus();
                        }
                    }
                    else if (transferenciasIncompletas == 404)
                    {
                        MessageBox.Show("La orden seleccionada no existe");
                        textBox1.Text = "";
                        textBox1.Focus();
                    }
                    else
                    {
                        fillDataGrid("RECHAZADO");
                        //dgOrden.Enabled = false;
                        flag = 1;
                        dgOrden.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("La orden seleccionada no ha sido asignada aún");
                    textBox1.Text = "";
                    textBox1.Focus();
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
                MessageBox.Show("Hubo un pequeño detalle de comunicacion. Por favor intentelo de nuevo más tarde", "Advertencia");
            }
            Cursor.Current = Cursors.Default;
        }

        private void dgOrden_Click(object sender, EventArgs e)
        {
        }

        public void fillDataGrid(string tipo)
        {
            try
            {
                DataTable dt = new DataTable();
                if (tipo.Equals("RECHAZADO"))
                {
                    //ds = ws.getIncompletosTransfer(textBox1.Text);
                    dt = c.getIncompletosTransferWDR(textBox1.Text);
                }
                else
                {
                    //ds = ws.getAsignadas(textBox1.Text);
                    dt = c.getAsignadasWDR(textBox1.Text);
                    flag = 0;
                }
                //DataTable dt = ds.Tables[0];
                dgOrden.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Hubo un problema durante la conexion\nPor favor intentelo nuevamente más tarde", "Advertencia");
                //LiberarControles(this);
                this.Dispose();
                GC.Collect();
                frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                fmp.Show();
                dgOrden.Dispose();

            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            frmMenu_Produccion fmp = new frmMenu_Produccion(user);
            fmp.Show();
            dgOrden.Dispose();
        }

        private void Buscar_OrdenProduccion_Closing(object sender, CancelEventArgs e)
        {
            this.Dispose();
            GC.Collect();
            frmMenu_Produccion fmp = new frmMenu_Produccion(user);
            fmp.Show();
            dgOrden.Dispose();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgOrden.VisibleRowCount == 1)
            {
                string index = dgOrden.CurrentCell.ToString();
                int columnIndex = dgOrden.CurrentCell.ColumnNumber;
                int rowIndex = dgOrden.CurrentCell.RowNumber;
                string op = dgOrden[rowIndex, 1].ToString();
                string codigo = dgOrden[rowIndex, 8].ToString();
                string renglon = dgOrden[rowIndex, 11].ToString();
                int check = c.racksAsignados(codigo);
                if(check == 0)
                {
                    MessageBox.Show("Este producto no se maneja por racks.\nPuedes comenzar la producción.");
                    this.Dispose();
                    GC.Collect();
                    frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                    fmp.Show();
                    dgOrden.Dispose();
                }
                else if (check > 0)
                {
                    int res = c.getBautizoCompleto(op, codigo, renglon);
                    switch (res)
                    {
                        case 0:
                            MessageBox.Show("Todos los racks asignados a la orden ya han sido bautizados");
                            //LiberarControles(this);
                            this.Dispose();
                            GC.Collect();
                            frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                            fmp.Show();
                            dgOrden.Dispose();
                            break;
                        case 1:
                            MessageBox.Show("Hubo un problema durante la conexion\nPor favor intentelo nuevamente más tarde", "Advertencia");
                            //frmPRODUCCION_ORDEN fpo2 = new frmPRODUCCION_ORDEN();
                            //this.Dispose();
                            //fpo2.Visible = true;
                            break;
                        case 2:
                            //LiberarControles(this);
                            this.Dispose();
                            GC.Collect();
                            Bautizar_Racks br = new Bautizar_Racks(op, codigo, renglon, user);
                            br.Show();
                            dgOrden.Dispose();
                            break;
                        case 404:
                            MessageBox.Show("La orden no tiene racks asignados", "Advertencia");
                            break;
                    }
                }
                else if (check == -404)
                {
                    MessageBox.Show("El producto de esta orden no se encuentra en la base de TAGO");
                }
                else
                {
                    MessageBox.Show("Hay un problema con la comunicación. Por favor intentelo más tarde");
                }                
            }
            else
            {
                string index = dgOrden.CurrentCell.ToString();
                int columnIndex = dgOrden.CurrentCell.ColumnNumber;
                int rowIndex = dgOrden.CurrentCell.RowNumber;
                string op = dgOrden[rowIndex, 1].ToString();
                string codigo = dgOrden[rowIndex, 8].ToString();
                string renglon = dgOrden[rowIndex, 11].ToString();
                DialogResult dr = MessageBox.Show("¿Seguro que desea trabajar la orden para el producto\n" + codigo + "?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    int check = c.racksAsignados(codigo);
                    if (check == 0)
                    {
                        MessageBox.Show("Este producto no se maneja por racks.\nPuedes comenzar la producción.");
                        this.Dispose();
                        GC.Collect();
                        frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                        fmp.Show();
                        dgOrden.Dispose();
                    }
                    else if (check > 0)
                    {
                        int res = c.getBautizoCompleto(op, codigo, renglon);
                        switch (res)
                        {
                            case 0:
                                MessageBox.Show("Todos los racks asignados a la orden ya han sido bautizados");
                                //LiberarControles(this);
                                this.Dispose();
                                GC.Collect();
                                frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                                fmp.Show();
                                dgOrden.Dispose();
                                break;
                            case 1:
                                MessageBox.Show("Hubo un problema durante la conexion\nPor favor intentelo nuevamente más tarde", "Advertencia");
                                //frmPRODUCCION_ORDEN fpo2 = new frmPRODUCCION_ORDEN();
                                //this.Dispose();
                                //fpo2.Visible = true;
                                break;
                            case 2:
                                //LiberarControles(this);
                                this.Dispose();
                                GC.Collect();
                                Bautizar_Racks br = new Bautizar_Racks(op, codigo, renglon, user);
                                br.Show();
                                dgOrden.Dispose();
                                break;
                            case 404:
                                MessageBox.Show("La orden no tiene racks asignados", "Advertencia");
                                break;
                        }
                    }
                    else if (check == -404)
                    {
                        MessageBox.Show("El producto de esta orden no se encuentra en la base de TAGO");
                    }
                    else
                    {
                        MessageBox.Show("Hay un problema con la comunicación. Por favor intentelo más tarde");
                    }
                }
                else
                {
                    MessageBox.Show("Asegurese de seleccionar la partida con la que desea trabajar");
                }
            }                                                                                                                                                                                                                                                                                                                                                                                                                                       
            Cursor.Current = Cursors.Default;
        }
    
    }
}