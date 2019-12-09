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

namespace SmartDeviceProject1.Inventario
{
    public partial class Buscar_Tag : Form
    {
        
        BRIReader reader;
        Boolean b = false;
        PolyTone Ptone1 = new PolyTone();
        PolyTone Ptone2 = new PolyTone(
        300, 100, Tone.VOLUME.VERY_LOUD);
        Boolean RFID = false;
        String att1 = "100";
        String ant = "1";
        string tag = "";
        Thread hilo;
        EventArgs ee = new EventArgs();

        public Buscar_Tag(string epcTag)
        {
            InitializeComponent();
            tag = epcTag.ToString();
            txtTagEpc.Text = tag;
        }

        private void btnFijar_Click(object sender, EventArgs e)
        {
            if (txtTagEpc.Text.Length > 0)
                if (txtTagEpc.Text.Length == 24)
                {
                    tag = txtTagEpc.Text;
                    MessageBox.Show("Tag/Fijado para su búsqueda", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                }
                else
                    MessageBox.Show("Los tags contienen una cadena de 24 caracteres", "Informacion");
            else
                MessageBox.Show("Favor de teclear la informacion de un tag existente.", "Informacion");
        }

        private void txtTagEpc_TextChanged(object sender, EventArgs e)
        {
            //btnFijar_Click(this.btnFijar, e);
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                // DETECCION DE EMULADOR PRUEBAS
                if (TestEmulator.isEmulator())
                {
                    lblEstatus.Text = "READER CONECTADO";
                    btnConectar.Enabled = false;
                    btnDetener.Enabled = true;
                    b = true;

                    return;
                }*/
                // FIN DETECCION DE EMULADOR PRUEBAS

                reader = new BRIReader(this, null);
                if (reader.Attributes.SetTAGTYPE("EPCC1G2") == true)
                {
                    if (reader.IsConnected == true)
                    {
                        lblEstatus.Text = "READER CONECTADO";
                        btnConectar.Enabled = false;
                        btnDetener.Enabled = true;
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

        private void menuItem2_Click(object sender, EventArgs e)
        {
            limpia();                        
        }

        public void limpia()
        {
            if (hilo != null) 
            {
                hilo.Abort();
            }
            if (reader != null)
            {
                reader.Dispose();
            }
            if (btnConectar.Enabled == false)
            {
                btnConectar.Enabled = true;
            }
            b = false;
            this.Close();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            lstEpc.Items.Clear();         
        }

        public bool isNumeric(string texto)
        {
            bool res = false;
            int count = 0;
            char[] array = texto.ToCharArray();
            char[] numeros = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int x = 0; x < array.Length; x++)
            {
                if (numeros.Contains(array[x]))
                    res = true;
                else
                {
                    count++;
                    break;
                }
            }
            if (count > 0)
                res = false;
            return res;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (tag.Trim().Length > 0)
            {
                if (b == true)
                {
                    lstEpc.Items.Clear();
                    reader.Execute("Attrib ants=" + ant);
                    reader.Execute("Attrib fieldstrength=" + att1);
                    lblEstatus.Text = "Lectura iniciada";
                    hilo = new Thread(leer_tag);
                    RFID = true;
                    btnBuscar.Visible = false;
                    btnBuscar.Enabled = false;
                    btnDetener.Visible = true;
                    btnDetener.Enabled = true;
                    hilo.Start();
                }
                else
                {
                    //MessageBox.Show("No hay conexión con el reader", "Sin conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    lblEstatus.Text = "Sin conexión";
                }
            }
            else
            {
                MessageBox.Show("No se ha fijado ningun Tag para su búsqueda", "Informacion");
            }
        }
        //HASTA AQUI
        public void leer_tag()
        {
            bool cierre = false;
            try
            {
                while (RFID == true)
                {
                    reader.Read();
                    if (reader.TagCount > 0)
                    {
                        foreach (Tag eti in reader.Tags)
                        {
                            Ptone1.Play();
                            if (lstEpc.InvokeRequired)
                                lstEpc.Invoke((Action)(() => lstEpc.Items.Add(eti)));
                            if (eti.ToString().Equals(tag))
                            {
                                cierre = true;
                                this.btnDetener.Click += new EventHandler(btnDetener_Click);
                                if (btnDetener.InvokeRequired)
                                    btnDetener.Invoke((Action)(() => btnDetener_Click(this.btnDetener, ee)));
                                if (lblEstatus.InvokeRequired)
                                    lblEstatus.Invoke((Action)(() => lblEstatus.Text = "Tag Encontrado!!"));
                                for (int x = 0; x < 5; x++)
                                {
                                    Ptone2.Play();
                                }
                            }
                            if (cierre == true)
                                break;
                        }
                    }
                }
                if (cierre == false)
                {
                    if (lblEstatus.InvokeRequired)
                    {
                        lblEstatus.Invoke((Action)(() => lblEstatus.Text = "Lectura Finalizada"));
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

        private void btnDetener_Click(object sender, EventArgs e)
        {
            menuItem2.Enabled = true;
            btnBuscar.Visible = true;
            btnBuscar.Enabled = true;
            btnDetener.Visible = false;
            btnDetener.Enabled = false;
            RFID = false;  
        }

    }
}