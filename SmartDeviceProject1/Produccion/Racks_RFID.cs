using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Intermec.DataCollection;
using Intermec.DataCollection.RFID;
using Intermec.Device.Audio;
using System.Threading;

namespace SmartDeviceProject1.Produccion
{
    public partial class Racks_RFID : Form
    {
        BRIReader reader;
        Boolean b = false;
        PolyTone Ptone1 = new PolyTone();
        PolyTone Ptone2 = new PolyTone(300, 100, Tone.VOLUME.VERY_LOUD);
        Boolean RFID = false;
        Boolean opcion = false;//CON ESTA VARIABLE TIENES LA OPCION DE SEGUIR BAUTIZANDO RACKS UNO A UNO
        String att1 = "100";
        String ant = "1";
        string tag = null;
        Thread hilo;
        string op;
        string epc;
        cMetodos cm = new cMetodos();
        string[] usu;
        CalculoRacks cr = new CalculoRacks();
        int racksCalculados = 0;
        string codigo;
        int tipoRack;//PASAR EL TIPO DE RACK AUTOMATICAMENTE
        Decimal pedido = 0;
        int renglon;
        EventArgs ee = new EventArgs();
        bool updateR = false;
        int RacksAsignados = 0;
        string lote;
        string conteoRacks;
        int numRacks;
        int racksFaltantes;
        string completoRacks;
        bool validaEPC = false;
        Decimal estimada;
        Decimal total;
        Decimal cantidadParcialidad;
        string newId;

        public Racks_RFID(string [] user, string ordProd, string codprod, Decimal cantidad, int reng, int calculo,string loteProd, int tipoMaquina, Decimal cantParcialidad, string newIdSQL)
        {
            InitializeComponent();
            newId = newIdSQL;
            cantidadParcialidad = cantParcialidad;
            codigo = codprod;
            pedido = cantidad;
            lote = loteProd;
            op = ordProd;
            usu = user;
            renglon = reng;
            tipoRack = tipoMaquina;
            //calculo = cr.calculaRacks(codigo, tipoRack, pedido);
            racksCalculados = calculo;
            RacksAsignados = cr.RacksAsignados(op, pedido, codigo, renglon, lote);           
            lblCalculoRacks.Text = calculo.ToString();
            lblRacksAsignados.Text = "Racks Asignados: " + RacksAsignados + "";
            racksFaltantes = (racksCalculados - RacksAsignados);
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                reader = new BRIReader(this, null);
                if (reader.Attributes.SetTAGTYPE("EPCC1G2") == true)
                {
                    if (reader.IsConnected == true)
                    {
                        lblEstatus.Text = "READER CONECTADO";
                        btnConectar.Enabled = false;
                        btnLeer.Enabled = true;
                        btnLeer.Visible = true;
                        b = true;
                        btnLeer.Focus();
                        opcion = false;
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
            catch (Exception Ex)
            {
                MessageBox.Show("No se pudo establecer el protocolo Gen2. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                b = false;
            }
            Cursor.Current = Cursors.Default;

        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (b == true)
                {
                    epc = "";
                    reader.Execute("Attrib ants=" + ant);
                    reader.Execute("Attrib fieldstrength=" + att1);
                    lblEstatus.Text = "Lectura iniciada";
                    hilo = new Thread(leer_tag);
                    RFID = true;
                    // whats this for?
                    btnLeer.Visible = false;
                    btnLeer.Enabled = false;
                    //
                    hilo.Start();
                }
                else
                {
                    lblEstatus.Text = "Sin conexión del reader";
                }
            }
            catch (Exception exp)
            {
                string men = exp.Message;
                string algo = "";
            }
            Cursor.Current = Cursors.Default;
        }

        public void leer_tag()
        {
            
            int calculo;

            string[] parametros = cm.getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            estimada = cr.cantidad(tipoRack, codigo);
            total = cr.cantidadReal(op, racksCalculados, codigo, renglon, lote);
            //PONER BIEN LA VALIDACION PARA QUE PASE DE RACK EN RACK LA CANTIDAD CORRECTA
            //calculo = Convert.ToInt32(racksCalculados);
            if (estimada <= total)
            {
                estimada = cr.cantidad(tipoRack, codigo);
            }
            else if (total < estimada)
            {
                estimada = total;
            }

            try
            {
                while (RFID == true)
                {
                    for (int posActual = 0; posActual < racksFaltantes; posActual++)
                    {
                        if (opcion == false)
                        {
                            reader.Read();
                            if (reader.TagCount > 0)
                            {
                                foreach (Tag eti in reader.Tags)
                                {
                                    Ptone1.Play();
                                    epc = eti.ToString();
                                    validaEPC = cr.validaEPC(epc, tipoRack);
                                    if (validaEPC == true)
                                    {
                                        updateR = cr.RealUpdate(op, estimada, tipoRack, codigo, renglon, epc, total, lote, cantidadParcialidad, newId);
                                        pedido = cr.cantidadReal(op, racksCalculados, codigo, renglon, lote);
                                        RacksAsignados = cr.RacksAsignados(op, pedido, codigo, renglon, lote);
                                        numRacks = (RacksAsignados + 1);
                                        total = cr.cantidadReal(op, racksCalculados, codigo, renglon, lote);
                                        conteoRacks = cr.contRAcksAsign(op, total, codigo, renglon, numRacks, lote);

                                        RFID = false;

                                        if (lblRacksAsignados.InvokeRequired)
                                        {
                                            lblRacksAsignados.Invoke((Action)(() => lblRacksAsignados.Text = "Racks Asignados: " + numRacks + ""));
                                        }

                                        if (btnConectar.InvokeRequired)
                                        {
                                            btnConectar.Invoke((Action)(() => btnConectar.Visible = true));
                                            btnConectar.Invoke((Action)(() => btnConectar.Enabled = true));
                                        }
                                        //if (btnLeer.InvokeRequired)
                                        //{
                                        //    btnLeer.Invoke((Action)(() => btnLeer.Visible = true));
                                        //    btnLeer.Invoke((Action)(() => btnLeer.Enabled = true));
                                        //}

                                        if (lblEstatus.InvokeRequired)
                                        {
                                            lblEstatus.Invoke((Action)(() => lblEstatus.Text = "Rack Asignado Correctamente"));
                                        }

                                        if (btnCalcula.InvokeRequired)
                                        {
                                            btnCalcula.Invoke((Action)(() => btnCalcula_Click(btnCalcula, ee)));
                                        }
                                        opcion = true;
                                        break;
                                    }
                                    else
                                    {
                                        if (btnValidaciones.InvokeRequired)
                                        {
                                            btnValidaciones.Invoke((Action)(() => btnValidaciones_Click(btnValidaciones, ee)));
                                            RFID = false;
                                            break;
                                        }                                        
                                    }
                                }



                            }
                        }
                        break;
                    }
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



        

        private void btnCalcula_Click(object sender, EventArgs e)
        {
            try
            {
                if (racksCalculados == numRacks)
                {
                    total = cr.cantidadReal(op, racksCalculados, codigo, renglon, lote);
                    pedido = total;
                    completoRacks = cr.racksCompletados(op, pedido, codigo, renglon, lote);
                    string ex = "";
                    MessageBox.Show("Has Asignado el total del Racks Calculados para esta parcialidad de la Orden de Produccion " + op + "");
                    frmMenu_Produccion fmp = new frmMenu_Produccion(usu);
                    fmp.Show();
                }
                else
                {
                    menuItem1.Enabled = true;
                }                
            }
            catch (Exception exp2)
            {
                string men = exp2.Message;
                string algo = "";
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            frmMenu_Produccion fmp = new frmMenu_Produccion(usu);
            fmp.Show();
        }

        private void btnValidaciones_Click(object sender, EventArgs e)
        {
            validaEPC = cr.validaEPC(epc, tipoRack);
            if (validaEPC == false)
            {
                MessageBox.Show("Este EPC ya tiene informacion o NO esta registrado como Rack","ALERTA");
                frmMenu_Produccion fmp = new frmMenu_Produccion(usu);
                fmp.Show();
            }

        
        }
        
    }
}