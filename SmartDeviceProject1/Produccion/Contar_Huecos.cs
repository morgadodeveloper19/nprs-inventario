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

namespace SmartDeviceProject1.Produccion
{
    public partial class Contar_Huecos : Form
    {

        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.NapresaSitio.Service1 ws = new SmartDeviceProject1.NapresaSitio.Service1();
        SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos met = new cMetodos();

        string id = "";
        string op;
        string fol = "";
        string renglon = "";//JLMQ SE AGREGA PARA QUE FILTRE POR RENGLON
        string codprod = "";//JLMQ SE AGREGA PARA QUE SE FILTRE DE ACUERDO A CODIGO DE PRODUCTO
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
        string[] user;
        string[] info;
        int sizeDG = 0;
        int evnt;
        int pzasParcialidad = 0;
        int ultimoRack;
        string newId;
        int ActivaBoton = 5;
        string epcMerma;

        public Contar_Huecos(string[] usuario, string[] folio, int evento, int piezasParc, string newIdSQL)
        {
            InitializeComponent();
            newId = newIdSQL;
            op = folio[1];
            id = folio[0];
            renglon = folio[11];
            codprod = folio[8];
            fol = folio[1];
            //pzasParcialidad = Convert.ToInt32(folio[14]);
            pzasParcialidad = piezasParc;
            user = usuario;
            info = folio;
            evnt = evento;
            fillDataGrid();
            switch (evento)//AQUI SE DECIDE SI CUENTA HUECOS O CUENTA MERMAS
            {
                case 0: this.Text = "Contar Huecos"; break;
                case 1: this.Text = "Contar Mermas"; break;
                default: break;
            }
            //CStr(datagridview1.Row.Count)
            ActivaBoton = (dataGrid1.VisibleRowCount);
            if (ActivaBoton == 0)
            {
                menuItem2.Enabled = true;
            }
            else
            {
                menuItem2.Enabled = false;
            }
        }

        public Contar_Huecos(string[] usuario, string[] folio,bool c,BRIReader lector, int evento, string newid, string epcRFID)
        {
            InitializeComponent();
            epcMerma = epcRFID;
            newId = newid;
            op = folio[1];
            id = folio[0];
            pzasParcialidad = Convert.ToInt32(folio[14]);
            fol = folio[1];
            user = usuario;
            codprod = folio[8];
            renglon = folio[11];
            info = folio;
            b = c;
            btnConectar.Enabled = false;
            reader = lector;
            evnt = evento;
            switch (evento)
            {
                case 0: this.Text = "Contar Huecos"; break;
                case 1: this.Text = "Contar Mermas"; menuItem2.Text = "Siguiente"; break;
                default: break;
            }
            fillDataGrid();
            ActivaBoton = (dataGrid1.VisibleRowCount);
            if (ActivaBoton == 0)
            {
                menuItem2.Enabled = true;
            }
            else
            {
                menuItem2.Enabled = false;
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
            //JLMQ para huecos hacer un Row.Count al dataGrid1, si el resultado es igual a *1 que se reporte hueco como merma pasando un valor como bandera
            if (evnt == 0)
            {
                dataGrid1.DataSource = null;
                dataGrid1.Refresh();
				// getPersonas() trae el DataSet de racks que no han sido contados por primera vez... Geez... =/
				// why dafuq does he did this for?
                dataGrid1.DataSource = met.getPersonas(fol,codprod, renglon,pzasParcialidad,newId);
                dataGrid1.Refresh();
                //ultimoRack = ((DataTable)dataGrid1.DataSource).Rows.Count;//JLMQ se cuenta cuantos racks sobran
                
                
            }
            if (evnt == 1)
            {
                dataGrid1.DataSource = null;
                dataGrid1.Refresh();
                dataGrid1.DataSource = met.getCuradoRacks(fol,codprod, renglon,pzasParcialidad,newId);//AQUI VERIFICAR CUANDO SEAN MAS RACKS JLMQ 13SEP2018
                dataGrid1.Refresh();
            }
            //try
            //{
            //    DataSet ds = ws.getRacksLlenado(fol);
            //    DataTable dt = ds.Tables[0];
            //    dataGrid1.DataSource = dt;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Hubo un problema con la carga de información.\nPor favor intentelo nuevamente.", "Advertencia");
            //    //LiberarControles(this);
            //    var proceso = Process.GetCurrentProcess();
            //    proceso.Refresh();
            //    this.Dispose();
            //    GC.Collect();
            //    Revisar_Avance ra = new Revisar_Avance(user);
            //    ra.Show();
            //    dataGrid1.Dispose();
            //}
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
            //int rows = ((DataTable)dataGrid1.DataSource).Rows.Count;
            int sizeDG = met.sizeList(fol, evnt, codprod, renglon, newId);
            string[] booya = new string[sizeDG];
            for (int x = 0; x < sizeDG; x++)
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
                                        epc = eti.ToString();
                                        epcs.Add(epc);
                                        cierre = true;
                                        tr = met.getTipoRack(tag);
                                        rack = met.rackInfo(tag);//AQUI SACA EL ARREGLO DE RACK
                                        if (dataGrid1.InvokeRequired)
                                        {
                                            //dataGrid1.Invoke((Action)(() => fillDataGrid()));
                                            btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee)));
                                            Ptone1.Play();
                                            btnFinalizar.Invoke((Action)(() => btnFinalizar_Click(btnFinalizar, ee)));
                                            //pnlNiveles.Invoke((Action)(() => llenaPanelNiv(tr)));
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

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //LiberarControles(this);
            var proceso = Process.GetCurrentProcess();
            proceso.Refresh();
            //dataGrid1.Dispose();
            Revisar_Avance ra = new Revisar_Avance(user);
            ra.Show();
            this.Close();
            GC.Collect();
            

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            string Estatus;
            if (evnt == 0)//huecos
            {
                Estatus = "CURADO";
                String msg = met.avanzarEstadoHuecos(op, "PRODUCCION", id, renglon, pzasParcialidad, usuario, pzasParcialidad, newId);//SE AGREGA LA CANTIDAD DE LA PARCIALIDAD TOTAL
            }
            else 
            {
                Estatus = "LIBERADO";
                int res = met.contarHuecos(id, op,codprod, user[4], user[3], renglon, newId);
            }

            string res2 = met.Libera(Estatus, info[11], newId);//SI ES EVNT= 0 CURADO SI ES 1 = LIBERADO
            if (res2.Contains("Exitoso"))
            {
                MessageBox.Show(res2);
                //LiberarControles(this);
                var proceso = Process.GetCurrentProcess();
                proceso.Refresh();
                //dataGrid1.Dispose();
                frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                fmp.Visible = true;
                this.Close();
                GC.Collect();
                

            }
            else
            {
                MessageBox.Show("Hubo un problema al momento de Liberar el producto.\n Por favor intentelo nuevamente más tarde.", "Aviso");
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (evnt == 0)
                {
                    //dataGrid1.Dispose();
                    int PxT = met.PiezasVentana(epc);
                    if (PxT == 0)
                    {
                        Huecos_NoRack hnr = new Huecos_NoRack(info, user,0);
                        hnr.Show();
                        this.Close();
                        GC.Collect();
                    }
                    else if (PxT > 0)
                    {
                        //Paneles_Conteo pc = new Paneles_Conteo(tr, int.Parse(id), fol, epc, user, info, reader,PxT);
                        //pc.Show();
                        //if(ultimoRack==1) JLMQ 
                        Huecos_Racks hr = new Huecos_Racks(info, user, rack, epc, reader, evnt, tr, newId);
                        hr.Show();
                        this.Close();
                        GC.Collect();
                    }
                    else
                    {

                    }
                }
                else if (evnt == 1)
                {
                    int PxT = met.PiezasVentana(epc);
                    if (PxT == 0)
                    {
                        Huecos_NoRack hnr = new Huecos_NoRack(info, user,1);
                        hnr.Show();
                        this.Close();
                        GC.Collect();
                    }
                    else if (PxT > 0)
                    {
                        //Paneles_Conteo pc = new Paneles_Conteo(tr, int.Parse(id), fol, epc, user, info, reader,PxT);
                        //pc.Show();
                        Huecos_Racks hr = new Huecos_Racks(info, user, rack, epc, reader, evnt, tr, newId);
                        hr.Show();
                        this.Close();
                        GC.Collect();
                    }
                    else
                    {
                    }
                }
            }
            catch (ObjectDisposedException odex)
            {
                //MessageBox.Show(odex.Message);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void Contar_Huecos_Closing(object sender, CancelEventArgs e)
        {
            //LiberarControles(this);
            var proceso = Process.GetCurrentProcess();
            proceso.Refresh();
            //dataGrid1.Dispose();
            Revisar_Avance ra = new Revisar_Avance(user);
            ra.Show();
            this.Close();
            GC.Collect();
        }
    }
}
