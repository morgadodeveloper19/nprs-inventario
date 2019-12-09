using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using Intermec.Device.Audio;
using System.Collections;
using Intermec.DataCollection.RFID;
using System.Threading;
namespace SmartDeviceProject1.Almacen
{
    public partial class Picking_Almacen : Form
    {
        cMetodos c = new cMetodos();

        SqlCeConnection conexion = new SqlCeConnection("Data Source=\\Flash File Store\\SSPB\\GoMonitor.sdf;");
        PolyTone Ptone1 = new PolyTone();
        PolyTone Ptone2 = new PolyTone(
        500, 300, Tone.VOLUME.VERY_LOUD);
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
        int usu, nuevo;
        String[] usuario;
        double factor, cantCong, cantCont;
        string articuloEpc = "";
        EventArgs ee = new EventArgs();
        ArrayList epcs = new ArrayList();
        ArrayList idArts = new ArrayList();
        ArrayList epcsNo = new ArrayList();
        ArrayList idArtsNo = new ArrayList();
        string remi = "";
        int res = 0,diferencia;
        string epc = "";
        string codigo = "", op = "";
        string[] user;

        public Picking_Almacen(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            remi = txtRemision.Text;
            fillDataGrid(remi, 0);
            Cursor.Current = Cursors.Default;
        }

        public void fillDataGrid(string remision, int filtro)
        {
            res = filtro;
            try
            {
                if (filtro == 0)
                {
                    DataTable dt = c.pickingEscuadraWDR(remision, 1);
                    dataGrid1.DataSource = dt;
                    dataGrid1.Visible = true;
                    btnConectar.Enabled = true;
                }
                else if (filtro == 1)
                {
                    DataTable dt = c.getStockRemision(remision);
                    dataGrid1.DataSource = dt;
                    dataGrid1.Visible = true;
                    btnConectar.Enabled = true;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("Hubo un problema con la conexión\nPor favor intentelo de nuevo más tarde", "Advertencia");
                MessageBox.Show(e.Message, "Advertencia");
            }
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
                        btnFinalizar.Enabled = true;
                        b = true;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo establecer el protocolo Gen2. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        b = false;
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo establecer el protocolo Gen2. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    b = false;
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("No se pudo establecer el protocolo Gen2. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                b = false;
            }
        }
        
        public void leerAccion()
        {
            if (b == true)
            {
                reader.Execute("Attrib ants=" + ant);
                reader.Execute("Attrib fieldstrength=" + att1);
                hilo = new Thread(leer_tag);
                btnLeer.Visible = false;
                btnLeer.Enabled = false;
                btnDetener.Visible = true;
                btnDetener.Enabled = true;
                hilo.Start();
            }
            else
            {
                MessageBox.Show("No hay conexión con el reader", "Sin conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            RFID = true;
            leerAccion();
            Cursor.Current = Cursors.Default;
        }

        public string[] getEPCS()
        {
            int rows = ((DataTable)dataGrid1.DataSource).Rows.Count;
            string[] booya = new string[rows];
            for (int x = 0; x < rows; x++)
            {
                string index = dataGrid1.CurrentCell.ToString();
                int columnIndex = 0;
                int rowIndex = dataGrid1.CurrentCell.RowNumber;
                string value = dataGrid1[x, columnIndex].ToString();
                booya[x] = value;
                codigo = dataGrid1[x, 3].ToString();
                op = dataGrid1[x, 1].ToString();
            }
            return booya;

        }

        public void leer_tag()
        {
            bool cierre = false;
            string[] booya = getEPCS();
            try
            {
                // MessageBox.Show("Lectura Iniciada", "INICIO");
                while (RFID == true)
                {
                    reader.Read();
                    if (reader.TagCount > 0)
                    {
                        do
                        {
                            Tag[] eti = reader.Tags;
                            //Ptone1.Play();
                            for(int x = 0; x < booya.Length; x++)
                            {
                                for (int y = 0; y < eti.Length; y++)
                                {
                                    string tag = eti[y].ToString();
                                    if (!epcs.Contains(tag))
                                    {
                                        if (tag.Equals(booya[x]))
                                        {
                                            epcs.Add(tag);
                                            cierre = true;
                                            Ptone1.Play();
                                            epc = tag;

                                            if (dataGrid1.InvokeRequired)
                                            {
                                                //dataGrid1.Invoke((Action)(() => fillDataGrid(remi,res)));
                                                btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee)));
                                                RFID = false;
                                                x = booya.Length;
                                                y = eti.Length;
                                            }
                                        }
                                        else
                                        {
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                            }
                        } while (RFID == true);
                    }
                    if (cierre == true)
                        break;
                }
                //MessageBox.Show("Lectura Finalizada", "FIN");                
            }
            catch (ObjectDisposedException odex)
            {
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

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
            this.Dispose();
            dataGrid1.Dispose();
        }

        private void mi_NoFiltro_Click(object sender, EventArgs e)
        {
            fillDataGrid(remi, 0);
            mi_NoFiltro.Checked = true;
            mi_Stock.Checked = false;
        }

        private void mi_Stock_Click(object sender, EventArgs e)
        {
            fillDataGrid(remi,1);
            mi_NoFiltro.Checked = false;
            mi_Stock.Checked = true;
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            RFID = false;
            btnFinalizar.Enabled = true;
            btnLeer.Visible = true;
            btnLeer.Enabled = true;
            btnDetener.Visible = false;
            btnDetener.Enabled = false;
            int qtyPend = c.cantidadPendienteRemision(remi, codigo);
            int qtyEscuadra = c.getCantidadEscuadra(epc);
            int qtyActual = c.currentEmbarcado(remi, codigo);
            diferencia = qtyPend - qtyEscuadra;
            nuevo = qtyActual + qtyEscuadra;
            if(qtyPend!=0)
            {
                if (qtyEscuadra <= qtyPend)
                {
                    if (c.pickEscuadra(epc, codigo, op, nuevo, remi,diferencia) == 0)
                    {
                        MessageBox.Show("Tarima Lista para su Embarcado");
                        fillDataGrid(remi, res);
                    }
                    else
                    {
                        MessageBox.Show("Hubo un pequeño problema con la selección, por favor intente más tarde");
                    }
                }
                else if (qtyEscuadra > qtyPend)
                {
                    panel1.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Ya se han seleccionado todas las tarimas necesarias para esta remisión.");
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
        }

        private void rbPedido_CheckedChanged(object sender, EventArgs e)
        {
            rbStock.Checked = false;
        }

        private void rbStock_CheckedChanged(object sender, EventArgs e)
        {
            rbPedido.Checked = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbPedido.Checked)
            {

            }
            else if (rbStock.Checked)
            {
                if (c.Escuadra2Stock(epc,nuevo,diferencia))
                {
                    MessageBox.Show("Asignacion Exitosa");
                    panel1.Visible = false;
                }
                else
                {
                    MessageBox.Show("Hubo un problema durante la asignacion. Intentelo nuevamente.");
                    panel1.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una opción.");
            }
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}