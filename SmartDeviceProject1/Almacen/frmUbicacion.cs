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
using System.Threading;
using Intermec.DataCollection.RFID;
//using test_emulator;

namespace SmartDeviceProject1
{
    public partial class frmUbicacion : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();

        cMetodos ws = new cMetodos();
        ValidateOP vop = new ValidateOP();

        string bandera = "";
        string[] zr = new string[2];
        string[] detalle = new string[12];

        string consultaGeneral = "", consultaFinal = "";
        public Label[] etiqueta;
        string[] nom;
        string[] s1;
        string[] s2;
        string[] s3;
        string[] s4;
        string[] s5;
        string caf;
        string[] tmp;
        string[] usuario;

        SqlCeConnection conexion = new SqlCeConnection("Data Source=\\Flash File Store\\SSPB\\GoMonitor.sdf;");
        PolyTone Ptone1 = new PolyTone();
        PolyTone Ptone2 = new PolyTone(
        500, 300, Tone.VOLUME.VERY_LOUD);
        Tone tones = new Tone();
        //string caf;
        //string cafActivo, IdInv;
        string[] datos;
        BRIReader reader;
        Boolean b = false;
        Boolean RFID = false;
        String att1 = "100";
        String ant = "1";
        Thread hilo;
        int usu;
        //String[] usuario;
        double factor, cantCong, cantCont;
        string articuloEpc = "";
        EventArgs ee = new EventArgs();
        ArrayList epcs = new ArrayList();
        ArrayList idArts = new ArrayList();
        ArrayList epcsNo = new ArrayList();
        ArrayList idArtsNo = new ArrayList();
        string[] folios = new string[12];
        int CXT = 0;
        //private delegate void UpdateStatusDelegate(int pos);
        string epcEscuadra = "";
        string codigoProd = "";
        int cantProd = 0;
        int qty = 0; 
        string[] user;
        int noRack = 0;
        string newId;
        string UbicacionAlm;
        bool elimina = false;
        string lote = "";

        public void fillDataGrid()
        {
            try
            {
                DataTable dt = ws.getZonaEPCWDR(((DataRowView)cbSucursales.SelectedItem)["Nombre"].ToString());
				dataGrid1.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("NO HAY ESCUADRAS DISPONIBLESS",e.Message);
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

        public frmUbicacion(string epc, string codigo, string[] folio, int cantidad,string[] usuario, bool tarima, string loteP)
        {
            try
            {
                InitializeComponent();
                lote = loteP;
                user = usuario;
                llenaCBConexion(cbSucursales, "Nombre", "Sucursal", "SELECT Descripcion AS Nombre, Descripcion AS Sucursal " +
                                                                    "FROM ZonaBustamante WHERE Sucursal = " + user[3] + "", "Solutia", 1);
                cbSucursales.Focus();
                epcEscuadra = epc;
                codigoProd = codigo;
                cantProd = cantidad;
                detalle = folio;
                newId = "68B1E286-846C-4165-94F1-243DE7F14CA4";
                qty = cantidad;
                if (tarima)
                {
                }
                else
                {
                    noRack = 1;
                }
            }
            catch
            {
            }
        }

        public frmUbicacion(string codigo, string[] folio, int cantidad, string[] usuario)
        {
            InitializeComponent();
            user = usuario;
            llenaCBConexion(cbSucursales, "Nombre", "Sucursal", "SELECT Nombre, Sucursal from Sucursal where Sucursal = " + user[3] + "", "Solutia", 1);
            cbSucursales.Focus();
            epcEscuadra = randomEPC();
            codigoProd = codigo;
            cantProd = cantidad;
            detalle = folio;
            qty = cantidad;
            noRack = 1;
        }
        
        public void llenaCB(ComboBox Objeto,string nomCve, string idCve, string consulta) 
        {           
            //DataTable zonas = ws.getDatasetConexionWDR(consulta, "Intelisis");
            //Objeto.DataSource = null;
            //Objeto.DataSource = zonas;
            //Objeto.DisplayMember = nomCve;
            //Objeto.ValueMember = idCve;
            //DataRow dr = zonas.NewRow();
            //dr[nomCve] = "SELECCIONAR";
            //dr[idCve] = 0;
            //zonas.Rows.InsertAt((dr), 0);
            //Objeto.SelectedValue = 0;
        }

        public void llenaCBAlmacen(ComboBox Objeto, string nomCve, string idCve, string consulta)
        {
            //DataTable cosas = ws.getDatasetConexionWDR(consulta, "Intelisis");
            //DataTable zonas = limpiaTablaAlmacen(cosas);            
            //Objeto.DataSource = null;
            //Objeto.DataSource = zonas;
            //Objeto.DisplayMember = nomCve;
            //Objeto.ValueMember = idCve;
            //DataRow dr = zonas.NewRow();
            //dr[nomCve] = "SELECCIONAR";
            //dr[idCve] = 0;
            //zonas.Rows.InsertAt((dr), 0);
            //Objeto.SelectedValue = 0;
        }

        public DataTable limpiaTablaAlmacen(DataTable dt)
        {
                int cuantos = dt.Rows.Count;            
            for (int x = 0; x < cuantos; x++)
            {                     
                string old = dt.Rows[x][0].ToString();
                string nuevo = old.Replace("ALMACEN", "");
                dt.Rows[x][0] = nuevo;
            }
            return dt;
        }

        public void llenaCBConexion(ComboBox Objeto, string nomCve, string idCve, string consulta, string conexion,int sel)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DataTable zonas = ws.getDatasetConexionWDR(consulta, conexion);
                Objeto.DataSource = null;
                Objeto.DataSource = zonas;
                Objeto.DisplayMember = nomCve;
                Objeto.ValueMember = idCve;
                if (sel == 1)
                {
                    DataRow dr = zonas.NewRow();
                    dr[nomCve] = "SELECCIONAR";
                    dr[idCve] = 0;
                    zonas.Rows.InsertAt((dr), 0);
                    Objeto.SelectedIndex = 0;
                }
            }
            catch (Exception exp)
            {
                string error;
                error = exp.Message;
            }
            Cursor.Current = Cursors.Default;
        }

        private void cbSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSucursales.SelectedIndex > 0) 
            {
                llenaCBAlmacen(cbAlmacen, "Nombre", "Almacen", "SELECT Nombre, Almacen FROM Alm WHERE Sucursal =" + cbSucursales.SelectedValue.ToString() + " AND Estatus = 'ALTA' and Nombre LIKE '%ALMACEN PRODUCTO TERMINADO%' AND Almacen = 'APT-BUS'");
                cbAlmacen.Enabled = true;
                btnConectar.Enabled = true;
                btnConectar.Visible = true;
            }      
        }       

        private void ckbRack_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbZona.Checked == true)
                {
                    if (ckbRack.Checked == true)
                    {
                        llenaCBConexion(cbRacks, "Clave", "IdRack", "select Clave, IdRack FROM racks where IDZona = '" + cbZonas.SelectedValue.ToString() + "'", "ConsolaAdmin", 0);
                        ckbPosicion.Enabled = true;
                    }
                    if (ckbRack.Checked == false)
                    {
                        this.cbRacks.DataSource = null;
                        ckbPosicion.Checked = false;
                        cbPosiciones.DataSource = null;
                    }
                }
                else
                {
                    MessageBox.Show("Debe de habilitar y seleccionar una zona primero");
                }
            }
            catch (NullReferenceException nre) { Console.Write(nre.Message); }
            catch (Exception ex) { Console.Write(ex.Message); }
        }

        private void ckbZona_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbAlmacen.SelectedIndex > 0)
            {
                if (ckbZona.Checked == true)
                {
                    llenaCBConexion(cbZonas, "ClaveZona", "IdZona", "SELECT Descripcion AS ClaveZona, Descripcion AS IdZona FROM ZonaBustamante WHERE idAlmacen = '" + cbAlmacen.SelectedValue.ToString() + "'", "Solutia", 0);
                    ckbRack.Enabled = true;
                }
                if (ckbZona.Checked == false)
                {
                    ckbRack.Checked = false;
                    ckbPosicion.Checked = false;
                    cbZonas.DataSource = null;
                    cbRacks.DataSource = null;
                    cbPosiciones.DataSource = null;
                }
            }
            else 
            {
                MessageBox.Show("Debe de seleccionar un ALMACEN para cargar sus ZONAS");
                ckbZona.Checked = false;
            }
        }

        private void cbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckbZona.Checked = false;
        }

        private void frmUbicacion_Closing(object sender, CancelEventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            //LiberarControles(this);
            //this.Dispose();
            GC.Collect();
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                fillDataGrid();
                
                //zr[0] = ((DataRowView)cbZonas.SelectedItem)["ClaveZona"].ToString();
                UbicacionAlm = cbSucursales.SelectedValue.ToString();
                //zr[1] = ((DataRowView)cbRacks.SelectedItem)["Clave"].ToString();
                //bandera = zr[0]; //+zr[1];
                conectarAccion();
                if (b == true)
                {
                    btnLeer.Enabled = true;
                    btnLeer.Visible = true;
                }
            }
            catch (Exception ee)
            {
                
                MessageBox.Show(ee.Message);
            }
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
                        btnFinalizar.Enabled = true;
                        b = true;
                    }
                    else
                    {
                        MessageBox.Show("FALLA CON EL READER. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        b = false;
                    }
                }
                else
                {
                    MessageBox.Show("FALLA CON EL READER. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    b = false;
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("FALLA CON EL READER. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                b = false;
            }

        }        

        private void btnLeer_Click(object sender, EventArgs e)
        {
            leerAccion();
        }

        public void leerAccion()
        {
            Cursor.Current = Cursors.WaitCursor;
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
                Cursor.Current = Cursors.Default;
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
                // MessageBox.Show("Lectura Iniciada", "INICIO");//VERIFICAR ESTE PUTO METODO QUE DUPLICO LECTURA HE HIZO 2 VECES UPDATE
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
                                        cierre = true;
                                        epcs.Add(tag);
                                        Ptone1.Play();
                                        noRack = 1;
                                        if (noRack == 1)
                                        {
                                            if (RFID == true)
                                            {
                                                int idtag = 4;//se puso esto para que compilara 19nov2019


                                                if (ws.ubicaEscuadra(idtag, detalle, UbicacionAlm)) //epcEscuadra, codigoProd, cantProd.ToString(), tag, bandera, 1) == 0)//int.Parse(zr[1]) se quito JLMQ
                                                {
                                                    if (dataGrid1.InvokeRequired)
                                                    {

                                                        btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee)));
                                                        dataGrid1.Invoke((Action)(() => fillDataGrid()));
                                                        btnFinalizar.Invoke((Action)(() => btnFinalizar_Click(btnFinalizar, ee)));
                                                        RFID = false;
                                                        cierre = false;
                                                        break;
                                                    }
                                                    //break;
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Hubo un problema durante la asignación. Por favor intentelo nuevamente.");
                                                    if (dataGrid1.InvokeRequired)
                                                    {
                                                        btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee)));
                                                        epcs.Remove(tag);
                                                        RFID = false;
                                                        cierre = false;
                                                    }
                                                }
                                            }
                                            RFID = false;
                                            cierre = false;
                                            break;
                                            
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
                    }
                    if (cierre == true)//estaba true
                        break;
                }               
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
        
        private void btnDetener_Click(object sender, EventArgs e)
        {
            btnFinalizar.Enabled = true;
            //btnLeer.Visible = true;
            //btnLeer.Enabled = true;
            btnConectar.Enabled = true;
            btnConectar.Visible = true;
            btnDetener.Visible = false;
            btnDetener.Enabled = false;
            RFID = false;
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                int res;
                res = ws.execEP(detalle, user[4]);//SE CREA LA ENTRADA DE PRODUCCION
                if (res == 1)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Se realizo la entrada de produccion en Intelisis", "EXITO");
                    frmMenu_Almacen fma = new frmMenu_Almacen(user);
                    fma.Show();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("REPITE DE NUEVO EL PROCESO", "ERROR");
                    frmMenu_Almacen fma = new frmMenu_Almacen(user);
                    fma.Show();
                }
            }
            catch (Exception ExcpEP)
            {
                string error;
                error = ExcpEP.Message;
                Cursor.Current = Cursors.Default;
            }
            Cursor.Current = Cursors.Default;
        }

        public string randomEPC()
        {
            var chars = "ABCDEF0123456789";
            var stringChars = new char[24];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        private void label_ParentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}