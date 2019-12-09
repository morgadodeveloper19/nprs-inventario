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
using System.Diagnostics;

namespace SmartDeviceProject1.Almacen
{
    public partial class Validar_Remision : Form
    {
        /*
        string remision = "";
        String ant = "1";
        String att1 = "100";
        Boolean RFID = false;
        Thread hilo;
        BRIReader reader;
        Boolean b = false;
        
        */
        
        cMetodos cm = new cMetodos();
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
        string pzaRemi = "";
        string remi = "";
        string codigo = "";
        string oprod = "";
        string epc = "";
        string[] lectura = new string[4];
        string[] user;
        string remision = "";

        public Validar_Remision(string remision, string codprod, string piezas, string[] user)
        {
            InitializeComponent();
            //fillDataGeneral();//invocar este metodo hasta que se vaya a leer la escuadra
            dataGrid1.Enabled = false;
            lblRemi.Text = remision;
            codigo = codprod;
            pzaRemi = piezas;
            remi = lblRemi.Text;
            //pzaRemi = lblPza.Text;
            lblCodProd.Text = codigo;
            lblPza.Text = piezas;
            user = user;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                reader = new BRIReader(this, null);
                if (reader.Attributes.SetTAGTYPE("EPCC1G2") == true)
                {
                    if (reader.IsConnected == true)
                    {
                        MessageBox.Show("READER CONECTADO","Exito");
                        btnConectar.Enabled = false;
                        btnDetener.Enabled = true;
                        b = true;
                    }
                    else
                    {
                        MessageBox.Show("Sin conexion al reader. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        b = false;
                    }
                }
                else
                {
                    MessageBox.Show("Sin conexion al reader. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    b = false;
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Sin conexion al reader. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
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
                reader.Execute("Atrib ants = " + ant);
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
            }
        }


        public void fillDataGrid()
        {
            try
            {
                DataTable ds = cm.showProdCompWDR();
                //DataTable dt = ds.Tables[0];
                dataGrid1.DataSource = ds;
            }
            catch (Exception e)
            {
                
                MessageBox.Show(e.Message);
                this.Dispose();
                frmMenu_Almacen fma = new frmMenu_Almacen(user);
                fma.Show();
                GC.Collect();
            }
        }


        public void leer_tag()
        {
            bool cierre = false;
            
            string[] booya = cm.getEpcEsc(remi, codigo);
            if (booya != null)
            {
                try
                {
                    while (RFID == true)
                    {
                        reader.Read();
                        if (reader.TagCount > 0)
                        {
                            foreach (Tag eti in reader.Tags)
                            {
                                string tag = eti.ToString();
                                for (int x = 0; x < booya.Length; x++)
                                {
                                    if (!epcs.Contains(tag))
                                    {
                                        if (tag.Equals(booya[x]))
                                        {
                                            epcs.Add(tag);//AQUI AGREGAR FUNCIONALIDAD DE CICLAR LAS ESCUADRAS HASTA QUE SE CUMPLA EL TOTAL DE PZAS DE UNA REMISION
                                            cierre = true;
                                            Ptone1.Play();
                                            bool UpdtNewEsc = false;
                                            UpdtNewEsc = cm.ActualizaEscInvInicial(codigo, pzaRemi, tag);
                                            if (UpdtNewEsc == true)
                                            {
                                                //lectura = cm.detalleEscuadra(tag); 
                                                epc = tag;
                                                if (dataGrid1.InvokeRequired)
                                                {
                                                    cierre = false;
                                                    MessageBox.Show("Se desconto la cantidad en la escudra de inventario inicial","EXITO");
                                                    menuItem2.Enabled = true;
                                                    dataGrid1.Invoke((Action)(() => fillDataGrid()));
                                                    btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee)));
                                                }
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
                                break;
                            }
                        }
                        if (cierre == true)
                            break;
                    }
                }
                catch (ObjectDisposedException odex)
                {
                    MessageBox.Show(odex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
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
            else
            {
                RFID = false;
                MessageBox.Show("No hay escuadras disponibles para este producto","Alerta");
                this.Close();
            }
        }

        public string[] getEPCS()
        {
            int rows = ((DataTable)dataGrid1.DataSource).Rows.Count;// aqui dio error
            string[] booya = new string[rows];
            for (int x = 0; x < rows; x++)
            {
                string index = dataGrid1.CurrentCell.ToString();
                int columIndex = 1;
                int rowIndex = dataGrid1.CurrentCell.RowNumber;
                string value = dataGrid1[x, columIndex].ToString();
                booya[x] = value;
            }
            return booya;         
        }


        public void fillDataGeneral()
        {
            try
            {
                DataTable ds = cm.EscForInvInicial();
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

        private void btnDetener_Click(object sender, EventArgs e)
        {
            btnLeer.Visible = true;
            btnLeer.Enabled = true;
            btnDetener.Visible = false;
            btnDetener.Enabled = false;
            RFID = false;
            GC.Collect();


            //try
            //{
            //    MessageBox.Show("Aqui agrega funcionalidad","Advertencia");//PARA QUE REASIGNE Y RESTE LA CANTIDAD DE LA OTRA ESCUADRA
            //    /*Detalle_Recepcion dr = new Detalle_Recepcion(lectura, epc, user, true);
            //    dr.Show();
            //    //LiberarControles(this);
            //    this.Dispose();
            //    dataGrid1.Dispose();*/
            //}
            //catch
            //{
            //    MessageBox.Show("Ningun TAG leido");
            //    return;
            //}

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            bool actEscNva = false;
            actEscNva = cm.ActNvaEsc(pzaRemi, txtEsc.Text);
            if (actEscNva == true)
            {
                MessageBox.Show("Se actualizo Correctamente la informacion en la nueva escuadra", "Exito");
                frmMenu_Almacen fma = new frmMenu_Almacen(user);
                fma.Show();
                
            }
        }

        private void txtEsc_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtEsc.Text))
            {
            }
            else
            {
                MessageBox.Show("Este campo solo acepta valores númericos", "Advertencia");
                txtEsc.Text = "";
                txtEsc.Focus();
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

        

       

        


    }

        
}
