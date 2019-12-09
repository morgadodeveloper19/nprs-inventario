using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Intermec.DataCollection.RFID;
using Intermec.Device.Audio;
using System.Threading;
using System.Data.SqlServerCe;
using System.Collections;
using Intermec.DataCollection;

namespace SmartDeviceProject1.Produccion
{
    public partial class Asignar_Escuadra : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos met = new cMetodos();
        
        string fol = "";
        string result = "";
        int pxt = 0;
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
        string[] folios = new string[15];
        int CXT = 0;
        //private delegate void UpdateStatusDelegate(int pos);
        int choice;
        int llegada = 0;
        string epece;
        string[] user;
        int recibido;
        int cantiti = 0;
        string newId;

        public Asignar_Escuadra(string[] detalle, string[] usuario,int cTarima, string newIdSql)
        {
            InitializeComponent();
            newId = newIdSql;
            txtPXT.Enabled = true;
            user = usuario;
            recibido = cTarima;
            folios = detalle;
            //fillDataGridEsc(); para pruebas jlmq            
            CXT = met.cantidadPorTarimaW(detalle[8]);
            //txtPXT.Text = CXT + "";
            rbProduccion.Checked = true;
            if (cTarima < CXT)
            {
                txtPXT.Text = cTarima.ToString();
                rbProduccion.Checked = true;
            }
            else
            {
                txtPXT.Text = cTarima.ToString();
                //txtPXT.Text = CXT.ToString();
                llegada = 1;
            }
            dataGrid1.Visible = true;
            cantiti = int.Parse(txtPXT.Text);
            fillDataGridEsc();
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

        public void fillDataGridEsc()
        {
            
            DataTable dt = met.getEscuadraWDR();
            if (dt.Rows.Count == 0)//JLMQ SE AGREGO ESTA VALIDACION PARA QUE EN CASO DE QUE NO EXISTAN ESCUADRAS VIRTUALES SE NOTIFIQUE AL USUARIO
            {
                
                MessageBox.Show("No hay escuadras Disponibles", "Advertencia");
                //LiberarControles(this);
                this.Dispose();
                GC.Collect();
                frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                fmp.Visible = true;
            }
            else
            {

                try
                {
                    dataGrid1.DataSource = dt;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("No hay escuadras disponible", "Advertencia");
                    //LiberarControles(this);
                    this.Dispose();
                    GC.Collect();
                    frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                    fmp.Visible = true;
                }
            }
            
            
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                pxt = int.Parse(txtPXT.Text.ToString());
                if (rbMerma.Checked == true)
                {
                    if (pxt > met.maximoPorTarima(folios[8]))
                    {
                        MessageBox.Show("La cantidad indicada supera el limite por tarima (Pegostes incluidos)\nPor favor revise que la cantidad sea la correcta", "Aviso");
                    }
                    else
                    {
                        conectarAccion();
                        //if (b)
                        //    MermaEstiba();
                        //else
                        //    ;
                    }
                }
                else
                {
                    conectarAccion();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ee)
            {
               MessageBox.Show("El campo de cantidad no puede ir en blanco", "Aviso");
            }
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
                        menuItem2.Enabled = true;
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

        private void MermaEstiba()
        {
            int nvaCxt = int.Parse(txtPXT.Text);
            int diferencia = 0;
            if (cantiti > nvaCxt)
            {
                diferencia = cantiti - nvaCxt;                
                met.MermaEstiba(diferencia, int.Parse(folios[0]), folios[8], folios[1], int.Parse(folios[11]), user[4], user[3]);
                //ws.insertMovinMerma(4, folios[1], int.Parse(folios[0]), 0, epece, folios[8], nvaCxt, "APT-BUS");
            }
            else
            {
                MessageBox.Show("La cantidad a mermar no puede ser mayor a la cantidad inicial de la tarima.", "Aviso");
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
                Cursor.Current = Cursors.Default;
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
                int columnIndex = 1;
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
                            for (int x = 0; x < booya.Length; x++)
                            {
                                if (!epcs.Contains(tag))
                                {
                                    if (tag.Equals(booya[x]))
                                    {
                                        epece = tag;
                                        epcs.Add(epece);
                                        cierre = true;
                                        Ptone1.Play();                                        
                                        result = met.verificarEscuadra(epece, folios[1], folios[8], pxt,folios[2], newId);
                                        if (dataGrid1.InvokeRequired)
                                        {
                                            btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee)));
                                            dataGrid1.Invoke((Action)(() => fillDataGridEsc()));
                                            btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee)));
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
                    Cursor.Current = Cursors.Default;
                }
                //MessageBox.Show("Lectura Finalizada", "FIN");                
            }
            catch (ObjectDisposedException odex)
            {
                Cursor.Current = Cursors.Default;
                //MessageBox.Show(odex.Message);
                //MessageBox.Show(odex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);               
            }
            catch (ThreadAbortException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            catch (Exception eex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(eex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            menuItem2.Enabled = true;
            btnLeer.Visible = true;
            btnLeer.Enabled = true;
            btnDetener.Visible = false;
            btnDetener.Enabled = false;
            RFID = false;
            //LiberarControles(this);
            //this.Dispose();
            //GC.Collect();
            //frmMenu_Principal fmp = new frmMenu_Principal(user);
            //fmp.Show();
            //dataGrid1.Dispose();
            finalizarEscuadra();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            
        }

        public void finalizarEscuadra()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (result.Contains("Asignada"))
            {
                //string res = met.avanzarEstado(folios[1], folios[10], folios[0], folios[11], int.Parse(folios[16]), user[4], newId);
                //if (res.Contains("Exitoso"))
                //{
                    //ws.insertMovinEntrada(2, folios[1], int.Parse(folios[2]), 0, epece, folios[8], int.Parse(txtPXT.Text), "APT-BUS");
                    MessageBox.Show("Escuadra Asignada Exitosamente", "Exito");
                    GC.Collect();
                    frmMenu_Principal fmp = new frmMenu_Principal(user);
                    fmp.Show();
                    //LiberarControles(this);
                    this.Dispose();
                    dataGrid1.Dispose();
                //}
                //else
                //{
                //    Cursor.Current = Cursors.Default;
                //    MessageBox.Show(res, "Advertencia");
                //}
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(result, "Advertencia");
            }
        }


        private void menuItem1_Click(object sender, EventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
            dataGrid1.Dispose();
        }

        private void rbMerma_CheckedChanged(object sender, EventArgs e)
        {
            txtPXT.Enabled = true;
        }

        private void rbProduccion_CheckedChanged(object sender, EventArgs e)
        {
            if (llegada == 0)
            {
                txtPXT.Text = recibido.ToString();
                //txtPXT.Enabled = false;
            }
            else
            {
                txtPXT.Text = CXT.ToString();
                //txtPXT.Enabled = false;
            }
        }

        private void Asignar_Escuadra_Closing(object sender, CancelEventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
            dataGrid1.Dispose();
        }
    }
}