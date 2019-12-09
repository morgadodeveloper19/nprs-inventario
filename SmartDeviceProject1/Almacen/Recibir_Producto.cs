using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class Recibir_Producto : Form
    {
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
       // SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();

        cMetodos ws = new cMetodos();


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
        string oprod = "";
        string epc = "";
        string[] lectura = new string[6];
        string[] user;

        public Recibir_Producto(string[] usuario)
        {
            InitializeComponent();
            fillDataGeneral();
            user = usuario;
            int count = dataGrid1.VisibleRowCount;
            if (count > 0)
            {
            }
            else
            {
                //LiberarControles(this);
                this.Dispose();
                GC.Collect();
                MessageBox.Show("No hay mercancia pendiente por recibir");
                frmMenu_Almacen fma = new frmMenu_Almacen(user);
                fma.Show();
            }
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

        public void fillDataGrid()
        {
            try
            {               
                DataTable ds = ws.showProdCompWDR();
                //DataTable dt = ds.Tables[0];
                dataGrid1.DataSource = ds;
            }
            catch (Exception e)
            {
                //MessageBox.Show("Hubo un problema con la conexión\nPor favor intentelo de nuevo más tarde", "Advertencia");
                MessageBox.Show(e.Message);
                //LiberarControles(this);
                this.Dispose();
                frmMenu_Almacen fma = new frmMenu_Almacen(user);
                fma.Show();
                GC.Collect();
            }
        }

        public void fillDataGranel()
        {
            try
            {
                DataTable ds = ws.showProdGranelWDR();
                dataGrid1.DataSource = ds;
            }
            catch (Exception e)
            {
                MessageBox.Show("Hubo un problema con la conexión\nPor favor intentelo de nuevo más tarde", "Advertencia");
                this.Dispose();
                frmMenu_Almacen fma = new frmMenu_Almacen(user);
                fma.Show();
                GC.Collect();
            }
        }

        public void fillDataGeneral()
        {
            try
            {
               DataTable ds = ws.showProdGeneralWDR();//AQUI
                dataGrid1.DataSource = ds;
            }
            catch (Exception e)
            {
                MessageBox.Show("Hubo un problema con la conexión\nPor favor intentelo de nuevo más tarde", "Advertencia");
                this.Dispose();
                frmMenu_Almacen fma = new frmMenu_Almacen(user);
                fma.Show();
                GC.Collect();
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
                                        epcs.Add(tag);
                                        cierre = true;
                                        Ptone1.Play();
                                        lectura = ws.detalleEscuadra(tag);
                                        epc = tag;
                                        if (dataGrid1.InvokeRequired)
                                        {
                                            dataGrid1.Invoke((Action)(() => fillDataGrid()));
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
            btnLeer.Visible = true;
            btnLeer.Enabled = true;
            btnDetener.Visible = false;
            btnDetener.Enabled = false;
            RFID = false;
            GC.Collect();
            
            
            try
            {

                Detalle_Recepcion dr = new Detalle_Recepcion(lectura, epc, user, true);
                dr.Show();
                //LiberarControles(this);
                this.Dispose();
                dataGrid1.Dispose();
            }
            catch
            {
                MessageBox.Show("Ningun TAG leido");
                return;
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (dataGrid1.VisibleRowCount > 0)
            {
                DialogResult dr = MessageBox.Show("Aun falta Mercancia por Recoger\n¿Esta seguro que desea salir?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.OK)
                {
                    
                    frmMenu_Almacen fma = new frmMenu_Almacen(user);
                    fma.Show();
                    GC.Collect();
                    //LiberarControles(this);
                    this.Dispose();
                    dataGrid1.Dispose();
                }
                else
                {
                }
            }
            else
            {
                frmMenu_Almacen fpo = new frmMenu_Almacen(user);
                fpo.Show();
                //LiberarControles(this);
                this.Dispose();
                GC.Collect();
                dataGrid1.Dispose();
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

        private void mi_NoFiltro_Click(object sender, EventArgs e)
        {
            mi_NoFiltro.Checked = true;
            mi_Tarima.Checked = false;
            mi_Granel.Checked = false;
            fillDataGeneral();
        }

        private void mi_Tarima_Click(object sender, EventArgs e)
        {
            mi_NoFiltro.Checked = false;
            mi_Tarima.Checked = true;
            mi_Granel.Checked = false;
            fillDataGrid();
        }

        private void mi_Granel_Click(object sender, EventArgs e)
        {
            mi_NoFiltro.Checked = false;
            mi_Tarima.Checked = false;
            mi_Granel.Checked = true;
            btnConectar.Enabled = false;
            fillDataGranel();
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {
            int columns = ((DataTable)dataGrid1.DataSource).Columns.Count;
            string[] booya = new string[columns];
            for (int x = 0; x < columns; x++)
            {
                string index = dataGrid1.CurrentCell.ToString();
                int columnIndex = dataGrid1.CurrentCell.ColumnNumber;
                int rowIndex = dataGrid1.CurrentCell.RowNumber;
                string value = dataGrid1[rowIndex, x].ToString();
                booya[x] = value;
            }
            if (booya[1].Length == 24)
            {
                MessageBox.Show("Este producto cuenta con un TAG", "Aviso");
            }
            else
            {
                lectura = ws.detalleEscuadra(booya[1]);
                Detalle_Recepcion dr = new Detalle_Recepcion(lectura, booya[1], user,false);
                dr.Show();
            }
        }



    }
}