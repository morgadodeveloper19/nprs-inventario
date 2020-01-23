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

namespace SmartDeviceProject1
{
    public partial class frmPicking_Escuadras : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();        
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos c = new cMetodos();
        List<cMetodos.Producto> listaEPCS = null;


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
        string remi = "";
        int res = 0;
        string epc = "";
        string codigo = "" , op = "", codprod = "" ;
        string[] user;
        string newId;

        public frmPicking_Escuadras(int enviado, string[] usuario)
        {
            InitializeComponent();
            res = enviado;
            user = usuario;
            switch (res)
            {
                case 1: this.Text = "Picking Almacen"; break;
                case 2: this.Text = "Picking Embarque"; break;
                case 3: this.Text = "Salida de Embarque"; break;
                default: break;
            }

            try
            {
                cbRemision.SelectedIndexChanged -= new EventHandler(cbRemision_SelectedIndexChanged);
                string query = "SELECT DISTINCT Remision as Items, Remision as ID FROM detRemision where conEscuadra = 1 AND PzasRemiCompletas = 1";
                llenaCB(cbRemision, "Items", "ID", query, cMetodos.CONEXION);       
                cbRemision.Enabled = true;
                cbRemision.Visible = true;
                cbRemision.SelectedIndexChanged += new EventHandler(cbRemision_SelectedIndexChanged);
                
                listaEPCS = new List<cMetodos.Producto>();
                
            }
            catch (Exception ps)
            {

                MessageBox.Show("Hubo un pequeño detalle de comunicacion. Por favor intentelo de nuevo más tarde", "Advertencia");
            }
        }


        public void llenaCB(ComboBox Objeto, string nomCve, string idCve, string consulta, string conex)
        {
            DataTable dt = c.getDatasetConexionWDR(consulta, conex);
            if (dt == null)
            {
                MessageBox.Show("Error en la BD, intentalo más tarde");
                this.Close();
                return;
            }

            Objeto.DataSource = null;
            Objeto.DataSource = dt;
            Objeto.DisplayMember = nomCve;
            Objeto.ValueMember = idCve;
            dt.Columns[0].MaxLength = 255;
            DataRow dr = dt.NewRow();
            string opcSelec = "SELECCIONAR";
            dr[nomCve] = (dt.Rows.Count > 0) ? opcSelec : "NO HAY REMISIONES DISPONIBLES";
            dr[idCve] = 0;
            try
            {
                dt.Rows.InsertAt((dr), 0);
            }
            catch (Exception e)
            {
                dt.Columns[0].MaxLength = 255;
                dt.Rows.InsertAt((dr), 0);
                //throw;
            }
            Objeto.SelectedValue = 0;
        }

        public frmPicking_Escuadras(int enviado, string[] usuario, string remision)
        {
            InitializeComponent();
            res = enviado;
            user = usuario;
            textBox1.Text = remision;
            remi = remision;
            switch (res)
            {
                case 1: this.Text = "Picking Almacen"; break;
                case 2: this.Text = "Picking Embarque"; break;
                case 3: this.Text = "Picking Salida"; break;
                default: break;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            remi = textBox1.Text;
            if (!(string.IsNullOrEmpty(remi)))//valida que el campo remi no este vacio.
            {
                Cursor.Current = Cursors.WaitCursor;
                switch (res)
                {
                    case 1:
                        //remi = textBox1.Text;
                        fillDataGrid(remi, res); break;
                    case 2:
                        remi = textBox1.Text;
                        fillDataGrid(remi, res); break;

                    case 3:
                        int rem = c.remisionEmbarque(textBox1.Text);
                        if (rem == 0)
                        {
                            //int emb = c.escuadrafalta(textBox1.Text);
                            int emb = c.checkEscFalta(textBox1.Text);
                            if (emb == 0)
                            {
                            remi = textBox1.Text;
                            fillDataGrid(remi, res);
                            Cursor.Current = Cursors.Default;
                            }
                            else if (emb == 1)
                            {
                                Cursor.Current = Cursors.Default;
                                DialogResult dr = MessageBox.Show("AUN FALTA MERCANCIA POR CARGAR\n AVISA A PERSONAL DE EMBARQUES", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                break;
                            } 
                                  
                            else if (emb == 2)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("LA REMISION NO PUEDE SER CONSULTADA VERIFICA BIEN LA INFORMACIÓN", "ADVERTENCIA");
                                break;
                            }
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("LA REMISIÓN "+remi+" NO ESTA RELACIONADA A NINGUN EMBARQUE.\nEMBARQUE DESDE INTELISIS LA REMISIÓN O SELECCIONE OTRA PARA CONTINUAR.", "AVISO");
                        }
                        break;
                    default: break;
                }
                Cursor.Current = Cursors.Default;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("El campo Remision esta en blanco","VERIFICAR");
            }
        }

        public void fillDataGrid(string remision, int valor)//muestra la informacion de la escuadra asignada a la remision de acuerdo a el proceso donde esta.
        {
            try
            {
                if (valor == 1)
                {
                    DataTable dt = c.pickingEscuadraWDR(remision, 1);
                    dataGrid1.DataSource = dt;
                    dataGrid1.Visible = true;
                    btnConectar.Enabled = true;
                }
                else if (valor == 2)
                {
                    
                        DataTable dt = c.pickingEscuadraWDR(remision, 2);
                        dataGrid1.DataSource = dt;
                        dataGrid1.Visible = true;
                        btnConectar.Enabled = true;
                    
                                      
                }
                else if (valor == 3)
                {
                    DataTable dt = c.pickingEscuadraWDR(remision, 3);
                    dataGrid1.DataSource = dt;
                    dataGrid1.Visible = true;
                    btnConectar.Enabled = true;
                }
                else
                {
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
                        btnLeer.Visible = true;
                        btnFinalizar.Enabled = true;
                        b = true;
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
                //MessageBox.Show(ex.Message);
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
                btnConectar.Enabled = true;
                btnConectar.Visible = true;
                hilo.Start();
            }
            else
            {
                MessageBox.Show("No hay conexión con el reader", "Sin conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
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
                //codigo = dataGrid1[x,3].ToString(); se quito para probar
                codigo = dataGrid1[x, 4].ToString();
                op = dataGrid1[x,1].ToString();
                codprod = dataGrid1[x, 3].ToString();

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
                                        epc = tag;

                                        if (dataGrid1.InvokeRequired)
                                        {
                                            //dataGrid1.Invoke((Action)(() => fillDataGrid(remi,res)));
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
            btnLeer.Visible = true;
            btnLeer.Enabled = true;
            btnDetener.Visible = false;
            btnDetener.Enabled = false;
            RFID = false;
            if (res == 1)
            {

                if (c.pickEscuadra(epc,codigo,op) == 0)
                {
                    MessageBox.Show("Tarima Lista para su Embarcado");
                    fillDataGrid(remi, res);
                    
                }
                else
                {
                    MessageBox.Show("Hubo un pequeño problema con la selección, por favor intente más tarde");
                }
            }
            else if (res == 2)
            {
                try
                {
                    newId = c.getNewIdEsc(epc);
                    string codigo = c.getcodigoembarque(epc);
                    string[] info = c.getArrayData(epc, codigo);


                    Embarques.Detalle_Recepcion edr = new SmartDeviceProject1.Embarques.Detalle_Recepcion(info, 2, epc, user, remi, newId);
                    edr.Show();
                    this.Dispose();
                    GC.Collect();
                    dataGrid1.Dispose();
                    RFID = false;
                }
                catch
                {
                    MessageBox.Show("Ningun Producto Asignado a ese TAG");
                    return;
                }
                                               
            }
            else if (res == 3)
            {
                /*string[] info = c.getArrayEtiquetasCB(epc,remi);
                this.Dispose();
                GC.Collect();
                Embarques.Detalle_Recepcion edr = new SmartDeviceProject1.Embarques.Detalle_Recepcion(info, 3, epc,user,remi);
                edr.Show();
                dataGrid1.Dispose();*/
                if (c.clearEscuadra(epc,codigo,op) == 0)
                {
                    MessageBox.Show("PROCESO COMPLETADO, RETIRA LA ESCUADRA LEIDA DEL CAMION","EXITO");
                    fillDataGrid(remi, 3);
                    RFID = false;
                }
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (res == 1)
            {
                if (dataGrid1.VisibleRowCount > 0)
                {
                    DialogResult dr = MessageBox.Show("Aún falta Mercancía por Recoger\n¿Seguro que desea salir?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.OK)
                    {
                        frmMenu_Principal fmp = new frmMenu_Principal(user);
                        fmp.Show();
                        this.Dispose();
                        dataGrid1.Dispose();

                    }
                    else
                    {
                    }
                }
                else
                {
                    frmMenu_Principal fmp = new frmMenu_Principal(user);
                    fmp.Show();
                    this.Dispose();
                    dataGrid1.Dispose();
                    RFID = false;
                }
            }
            else if (res == 2)
            {
                if (dataGrid1.VisibleRowCount > 0)
                {
                    DialogResult dr = MessageBox.Show("Aún falta Mercancía por Embarcar\n¿Seguro que desea salir?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.OK)
                    {
                        frmMenu_Principal fmp = new frmMenu_Principal(user);
                        fmp.Show();
                        this.Dispose();
                        dataGrid1.Dispose();
                        RFID = false;
                    }
                    else
                    {
                    }
                }
                else
                {
                    
                    frmMenu_Principal fmp = new frmMenu_Principal(user);
                    fmp.Show();
                    this.Dispose();
                    dataGrid1.Dispose();
                    RFID = false;
                }
            }
            else if (res == 3)
            {
                if (dataGrid1.VisibleRowCount > 0)
                {
                    DialogResult dr = MessageBox.Show("Aún falta Mercancía por Verificar\n¿Seguro que desea salir?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.OK)
                    {
                        RFID = false;
                        this.Close();
                    }
                    else
                    {
                    }
                }
                else
                {
                    RFID = false;
                    this.Close();
                }
            }
            else
            {
                RFID = false;
            }
        }

        private void frmPicking_Escuadras_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cbRemision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                remi = cbRemision.SelectedValue.ToString();

                int rem = c.remisionEmbarque(remi);
                if (rem == 0)
                {
                    //int emb = c.escuadrafalta(textBox1.Text);
                    int emb = c.checkEscFalta(remi);
                    if (emb == 0)
                    {
                        
                        fillDataGrid(remi, res);
                        Cursor.Current = Cursors.Default;
                        btnConectar.Enabled = true;
                        btnConectar.Visible = true;
                    }
                    else if (emb == 1)
                    {
                        Cursor.Current = Cursors.Default;
                        DialogResult dr = MessageBox.Show("AUN FALTA MERCANCIA POR CARGAR\n AVISA A PERSONAL DE EMBARQUES", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                      
                    }

                    else if (emb == 2)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("LA REMISION NO PUEDE SER CONSULTADA VERIFICA BIEN LA INFORMACIÓN", "ADVERTENCIA");
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("LA REMISIÓN " + remi + " NO ESTA RELACIONADA A NINGUN EMBARQUE.\nEMBARQUE DESDE INTELISIS LA REMISIÓN O SELECCIONE OTRA PARA CONTINUAR.", "AVISO");
                }
            }
            catch (Exception expc)
            {
                string error1;
                error1 = expc.Message;
            }
          
        }


    }
}
