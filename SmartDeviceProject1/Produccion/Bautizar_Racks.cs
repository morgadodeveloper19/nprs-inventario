using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Collections;
using Intermec.DataCollection;
using Intermec.DataCollection.RFID;
using Intermec.Device.Audio;
using System.Threading;
//using test_emulator;

namespace SmartDeviceProject1.Produccion
{
    public partial class Bautizar_Racks : Form
    {

        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
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
        int usu;
        String[] usuario;
        double factor, cantCong, cantCont;
        string articuloEpc = "";
        EventArgs ee = new EventArgs();
        ArrayList epcs = new ArrayList();
        ArrayList idArts = new ArrayList();
        ArrayList epcsNo = new ArrayList();
        ArrayList idArtsNo = new ArrayList();
        string code = "";
        string ren = "";
        //private delegate void UpdateStatusDelegate(int pos);
        string[] user;

        public Bautizar_Racks(string OP, string codigo, string renglon, string[] usuario)
        {
            InitializeComponent();
            textBox1.Text = OP;
            code = codigo;
            ren = renglon;
            user = usuario;
        }

        public void fillDataGrid()
        {
            try
            {
                string epc = textBox1.Text;
                //DataSet ds = ws.getRacks(epc, code, ren);
                //DataTable dt = ds.Tables[0];
                DataTable dt = c.getRacksWDR(epc, code, ren);
                dataGrid1.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Hubo un problema con la conexión\nPor favor intentelo de nuevo más tarde", "Advertencia");
                //LiberarControles(this);
                this.Dispose();
                GC.Collect();
                frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                fmp.Show();
            }

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            conectarAccion();
            btnLeer.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        public void conectarAccion()
        {
            try
            {


                /*
                // DETECCION DE EMULADOR PRUEBAS
                if (TestEmulator.isEmulator())
                {
                    MessageBox.Show("READER CONECTADO", "EXITO");
                    btnConectar.Enabled = false;
                    btnFinalizar.Enabled = true;
                    b = true;


                    return;
                }
                // FIN DETECCION DE EMULADOR PRUEBAS
                /**/





                reader = new BRIReader(this, null);
                if (reader.Attributes.SetTAGTYPE("EPCC1G2") == true)
                {
                    if (reader.IsConnected == true)
                    {
                        MessageBox.Show("READER CONECTADO", "EXITO");
                        btnConectar.Enabled = false;
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
                MessageBox.Show(ex.Message);
                MessageBox.Show("No se pudo establecer el protocolo Gen2. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                b = false;
            }
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            btnFinalizar.Enabled = true;
            btnLeer.Visible = true;
            btnLeer.Enabled = true;
            btnDetener.Visible = false;
            btnDetener.Enabled = false;
            RFID = false;
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
            }
            return booya;
        }

        public void leer_tag()
        {
            bool cierre = false;
            string[] booya = getEPCS();
            try
            {  
                while (RFID == true)
                {
                    reader.Read();
                    if (reader.TagCount > 0)
                    {
                        foreach (Tag eti in reader.Tags)
                        {
                            //Ptone1.Play();
                            string tag = eti.ToString();
                            for (int x = 0; x < booya.Length; x++)
                            {
                                if (!epcs.Contains(tag))
                                {
                                    if (tag.Equals(booya[x]))
                                    {
                                        epcs.Add(tag);
                                        cierre = true;
                                        Ptone1.Play();
                                        //ws.verificaRacks(tag);
                                        c.verificaRacks(tag);
                                        if (dataGrid1.InvokeRequired)
                                        {
                                            //btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee)));
                                            dataGrid1.Invoke((Action)(() => fillDataGrid()));
                                        }
                                        break;
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
            if (dataGrid1.VisibleRowCount > 0)
            {
                DialogResult dr = MessageBox.Show("Aún faltan Racks por Bautizar\n¿Seguro que desea salir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    
                    GC.Collect();
                    frmMenu_Produccion fpo = new frmMenu_Produccion(user);
                    fpo.Show();
                    //LiberarControles(this);
                    this.Dispose();
                }
                else
                {
                }
            }
            else
            {
                
                GC.Collect();
                frmMenu_Produccion fpo = new frmMenu_Produccion(user);
                fpo.Show();
                //LiberarControles(this);
                this.Dispose();
                dataGrid1.Dispose();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                fillDataGrid();
                dataGrid1.Visible = true;
                btnConectar.Enabled = true;
                btnFinalizar.Visible = true;
                btnLeer.Visible = true;
                btnDetener.Enabled = false;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            Buscar_OrdenProduccion bop = new Buscar_OrdenProduccion(user);
            bop.Show();
            dataGrid1.Dispose();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (dataGrid1.VisibleRowCount > 0)
            {
                DialogResult dr = MessageBox.Show("Aún faltan Racks por Bautizar\n¿Seguro que desea salir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    GC.Collect();
                    frmMenu_Produccion fpo = new frmMenu_Produccion(user);
                    fpo.Show();
                    //LiberarControles(this);
                    this.Dispose();
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                }
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                GC.Collect();
                frmMenu_Produccion fpo = new frmMenu_Produccion(user);
                fpo.Show();
                //LiberarControles(this);
                this.Dispose();
                dataGrid1.Dispose();
                Cursor.Current = Cursors.Default;
            }
        }

    }
}