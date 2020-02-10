using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlServerCe;
using System.Collections;
using Intermec.Device.Audio;
using Intermec.DataCollection.RFID;
using System.Threading;
using System.Diagnostics;
//using test_emulator;

namespace SmartDeviceProject1.Almacen
{
    public partial class Reportar_Mermas : Form
    {
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos ws = new cMetodos();
        
        string[] user;
        int qty = 0;
        string[] data;
        string id = "";
        string fol = "";
        string prod = "";
        int cant = 0;
        string[] rack;
        SqlCeConnection conexion = new SqlCeConnection("Data Source=\\Flash File Store\\SSPB\\GoMonitor.sdf;");
        PolyTone Ptone1 = new PolyTone();
        Tone tones = new Tone();
        string caf;
        string cafActivo, IdInv;
        string[] datos;
        BRIReader reader;
        Boolean b = false;
        Boolean RFID = false;
        String att1 = "100";
        String ant = "1";
        Thread hilo;
        int usu;
        String[] usuario;
        double factor, cantCong, cantCont;
        string articuloEpc = "";
        EventArgs ee = new EventArgs();
        ArrayList epcs = new ArrayList();
        ArrayList idArts = new ArrayList();
        ArrayList epcsNo = new ArrayList();
        ArrayList idArtsNo = new ArrayList();
        int tr = 0;
        string cod = "";
        int[] huecos;
        int size = 0;
        int identNivel = 0;
        int identVent = 0;
        int pos = 0;
        string epc = "";
        int ident = 0;
        string[] llenar = new string[15];
        //private delegate void UpdateStatusDelegate(int pos);
        int sizeDG = 0;
        int evnt;
        string newId;
        bool merma = false;
        int pzaCargadaEsc;
        string tipoMerma = "";


        public Reportar_Mermas(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
            cbMerma.Items.Insert(0,"SELECCIONAR MERMA");
            cbMerma.Items.Insert(1, "Merma Estiba");
            cbMerma.Items.Insert(2, "Merma Carga");
            cbMerma.Items.Insert(3, "Merma Produccion");
            cbMerma.Items.Insert(4, "Merma Corte");
            cbMerma.Items.Insert(5, "Muestras");
            cbMerma.Items.Insert(6, "Merma Prueba Lab");
        }

        public Reportar_Mermas(string[] usuario,string tag)
        {
            InitializeComponent();
            user = usuario;
            txtEPC.Text = tag;
           
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string cb = txtEPC.Text;
                //string[] info = ws.getArrayEtiquetasCodB(cb);
                string[] info = ws.getInfoReportMerma(epc, cod);
                data = info;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;

                lblCliente.Visible = true;
                lblCliente.Text = info[2];

                lblOP.Visible = true;
                lblOP.Text = info[0];

                lblPedido.Visible = true;
                lblPedido.Text = info[1];

                lblProducto.Visible = true;
                lblProducto.Text = info[3];

                txtCantidad.Visible = true;
                lbPzas.Text = info[4];
                //txtCantidad.Text = info[4];
                qty = int.Parse(info[4]);

                menuItem2.Enabled = true;
                btnReportar.Visible = true;
                btnBuscar.Enabled = false;
                txtEPC.Enabled = false;

                label6.Enabled = true;
                label6.Visible = true;

                btnReportar.Enabled = true;
                btnReportar.Visible = true;

                lbPzas.Enabled = true;
                lbPzas.Visible = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("No se ha encontrado ninguna escuadra asignada con ese codigo");
            }
        }

        private void bntBuscaNva_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;

            lblCliente.Visible = false;
            lblCliente.Text = "";

            lblOP.Visible = false;
            lblOP.Text = "";

            lblPedido.Visible = false;
            lblPedido.Text = "";

            lblProducto.Visible = false;
            lblProducto.Text = "";

            txtCantidad.Visible = false;
            txtCantidad.Text = "";
            qty = 0;

            menuItem2.Enabled = false;
            btnReportar.Visible = false;
            btnBuscar.Enabled = true;

            txtEPC.Text = "";
            txtEPC.Enabled = true;
            txtEPC.Focus();
            Cursor.Current = Cursors.Default;
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtCantidad.Text))
            {
            }
            else
            {
                MessageBox.Show("Este campo solo acepta valores númericos", "Advertencia");
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

        private void btnReportar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string mrm = "";
                btnReportar.Enabled = false;
                btnReportar.Visible = false;

                tipoMerma = cbMerma.SelectedItem.ToString();
                if (string.IsNullOrEmpty(tipoMerma))
                {
                    MessageBox.Show("SELECCIONA UN TIPO DE MERMA POR FAVOR", "ERROR");
                    btnReportar.Enabled = true;
                    btnReportar.Visible = true;
                }
                else
                {
                    mrm = txtCantidad.Text.ToString();
                    if (string.IsNullOrEmpty(mrm))
                    {
                        MessageBox.Show("EL CAMPO MERMA NO PUEDE ESTAR VACIO, INGRESA UN VALOR NUMERICO", "ERROR");
                        btnReportar.Enabled = true;
                        btnReportar.Visible = true;
                    }
                    else
                    {
                        int mermaCantidad = int.Parse(txtCantidad.Text);
                        pzaCargadaEsc = qty;

                        if (mermaCantidad > 0)
                        {
                            if (mermaCantidad < pzaCargadaEsc)//merma<cantidad en Escuadra
                            {
                                newId = ws.getNewIdEsc(epc);//obtiene newid de la escuadra leida
                                int diferencia = pzaCargadaEsc - mermaCantidad;//diferencia sera el valor que se pone en el update para actualizar la escuadra.
                                //data[4] = cantidad.ToString();
                                int Id = ws.getProdId(data[0], data[5], newId);//ID
                                int renglon = ws.getRenglon(data[0], data[5], newId);//RENGLON

                                //int res = ws.MermaAlmacen(prodId, cantidad, data[1], data[8], renglon, data[11], user[4], user[3]);
                                //int res = ws.MermaAlmacen(Id, cantidad, diferencia, data[0], data[5], renglon, txtEPC.Text, user[4], user[3]);//movimiento en intelisis

                                merma = ws.reportarMerma(Id, mermaCantidad, diferencia, renglon, epc, user[4], user[3], data[5], data[0], pzaCargadaEsc, tipoMerma);

                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("MERMA REPORTADA CORRECTAMENTE", "EXITO");
                                label2.Visible = false;
                                label3.Visible = false;
                                label4.Visible = false;
                                label5.Visible = false;

                                lblCliente.Visible = false;
                                lblCliente.Text = "";

                                lblOP.Visible = false;
                                lblOP.Text = "";

                                lblPedido.Visible = false;
                                lblPedido.Text = "";

                                lblProducto.Visible = false;
                                lblProducto.Text = "";

                                txtCantidad.Visible = false;
                                txtCantidad.Text = "";
                                qty = 0;

                                menuItem2.Enabled = false;
                                btnReportar.Visible = false;
                                btnBuscar.Enabled = true;

                                txtEPC.Text = "";
                                txtEPC.Enabled = true;
                                txtEPC.Focus();

                                label6.Enabled = false;
                                label6.Visible = false;

                                lbPzas.Enabled = false;
                                lbPzas.Visible = false;

                                frmMenu_Almacen malm = new frmMenu_Almacen(user);
                                malm.Show();
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("LAS MERMAS NO PUEDEN SER MAYORES AL CONTENIDO DE LA ESCUADRA", "ERROR");
                                txtCantidad.Text = qty + "";
                                txtCantidad.Focus();
                            }
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("CANTIDAD DE MERMA INVALIDAD", "ERROR");
                            txtCantidad.Text = qty + "";
                            txtCantidad.Focus();
                        }
                    }//  
                }
                
            }
            catch (Exception ee)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("HUBO UN ERROR AL REPORTAR LA MERMA", "Aviso");
                txtCantidad.Text = "";
                btnReportar.Enabled = true;
                btnReportar.Visible = true;
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
        }

        private void Reportar_Mermas_Closing(object sender, CancelEventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            conectarAccion();
            Cursor.Current = Cursors.Default;
        
        }

        public void conectarAccion()
        {
            try
            {
                reader = new BRIReader(this, null);
                if (reader.Attributes.SetTAGTYPE("EPCC1G2") == true)
                {
                    if (reader.IsConnected == true)
                    {
                        MessageBox.Show("READER CONECTADO", "EXITO");
                        btnConectar.Enabled = false;
                        btnLeer.Enabled = true;
                        b = true;
                        btnConectar.Enabled = false;
                        cbMerma.Enabled = true;
                        cbMerma.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        b = false;
                    }
                }
                else
                {
                    MessageBox.Show("Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    b = false;
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                b = false;
            }
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            leerAccion();
            Cursor.Current = Cursors.Default;
        }

        public void leerAccion()
        {
            if (b == true)
            {
                reader.Execute("Attrib ants=" + ant);
                reader.Execute("Attrib fieldstrength=" + att1);
                hilo = new Thread(leer_tag);
                RFID = true;
                btnLeer.Visible = false;
                btnLeer.Enabled = false;
                btnDetener.Visible = true;
                btnDetener.Enabled = true;
                hilo.Start();
            }
            else
            {
                MessageBox.Show("No hay conexión con el reader", "Sin conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                //button2.Enabled = true;
            }
        }
		
        public void leer_tag()
        {
			// Bloque nuevo
			bool cierre = false;            
            
            try
            {
                // MessageBox.Show("Lectura Iniciada", "INICIO");
                while (RFID == true)
                {
                    reader.Read();
                    if (reader.TagCount > 0)
                    {
                        foreach (Tag eti in reader.Tags)
                        {
                            //Ptone1.Play();
                            string tag = eti.ToString();
                            epc = tag;
                            if (txtEPC.InvokeRequired)
                            {
                                //dataGrid1.Invoke((Action)(() => fillDataGrid()));
                                btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee)));
                                Ptone1.Play();
                                btnFinalizar.Invoke((Action)(() => btnFinalizar_Click(btnFinalizar, ee)));
                                //pnlNiveles.Invoke((Action)(() => llenaPanelNiv(tr)));
                            }
                            break;
                        }
                    }
					if (cierre == true)
						break;
                } 
                //MessageBox.Show("Lectura Finalizada", "FIN");                
            }
            catch (ObjectDisposedException odex)
            {
                //MessageBox.Show(odex.Message);
                //MessageBox.Show(odex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);               
            }
            catch (ThreadAbortException ex)
            {
                MessageBox.Show(ex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            catch (Exception eex)
            {
                MessageBox.Show(eex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

		}
	
        private void btnDetener_Click(object sender, EventArgs e)
        {
            detenerAccion();
        }

        public void detenerAccion()
        {
            btnFinalizar.Enabled = true;
            btnLeer.Visible = true;
            btnLeer.Enabled = true;
            btnDetener.Visible = false;
            btnDetener.Enabled = false;
            RFID = false;
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            txtEPC.Text = epc;  
            btnBuscar.Invoke((Action)(() => btnBuscar_Click(btnBuscar,ee)));
        }

        private void mermaEstiba_CheckStateChanged(object sender, EventArgs e)
        {
            //mermaCarga.Checked = false;
            //tipoMerma = "Merma Estiba";
            //btnReportar.Enabled = true;
            //btnReportar.Visible = true;
        }

        private void mermaCarga_CheckStateChanged(object sender, EventArgs e)
        {
            //mermaEstiba.Checked = false;
            //tipoMerma = "Merma Carga";
            //btnReportar.Enabled = true;
            //btnReportar.Visible = true;
        }

    }
}