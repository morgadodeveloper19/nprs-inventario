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



namespace SmartDeviceProject1.Almacen
{
    public partial class Nueva_Escuadra : Form
    {
        string[] user, folio, booya,remision;
        string remi, pos, result, epece;
        cMetodos cm = new cMetodos();
        int completo, qtyTarima, qtyRemision;
        int posicionEsc;
        string qtyRemTar;
        string updateDetremision;
        string vremi;
        string qtyRemTar2;
        string sucursal;
        string ArtRemision;
        int pzaArtRemi;
        string ubicacion;
        SqlCeConnection conexion = new SqlCeConnection("Data Source=\\Flash File Store\\SSPB\\GoMonitor.sdf;");
        PolyTone Ptone1 = new PolyTone();
        PolyTone Ptone2 = new PolyTone(
        500, 300, Tone.VOLUME.VERY_LOUD);
        Tone tones = new Tone();
        BRIReader reader;
        Boolean b = false;
        Boolean RFID = false;
        String att1 = "100";
        String ant = "1";
        Thread hilo;
        EventArgs ee = new EventArgs();
        ArrayList epcs = new ArrayList();
        ArrayList idArts = new ArrayList();
        ArrayList epcsNo = new ArrayList();
        ArrayList idArtsNo = new ArrayList();
        string[] folios = new string[15];
        public DataTable dtRemi = null;
        public DataTable dtRemi2 = null;//PARA GUARDAR EL NUMERO DE PRODUCTOS QUE VIENEN
        int numProdRemi;
        string remition;
        bool elimina = true;
        string error;
        string newid = "";

        public Nueva_Escuadra(string[] usu, string remi)
        {
            InitializeComponent();
            remition = remi;
            user = usu;
            sucursal = user[3];
            txtCodPord.Text = "";
            txtPza.Text = "";
            txtRemision.Focus();
            txtRemision.Text = remition;
            llenaCBConexion(comboBox1, "Posicion", "Posicion", "SELECT Descripcion AS Posicion FROM ZonaBustamante where Sucursal = '" + usu[3] + "'", "Solutia", 1);
            fillDataGridEsc();
        }

        public void llenaCBConexion(ComboBox Objeto, string nomCve, string idCve, string consulta, string conexion, int sel)
        {
            DataTable zonas = cm.getDatasetConexionWDR(consulta, conexion);
            Objeto.DataSource = null;
            Objeto.DataSource = zonas;
            Objeto.DisplayMember = nomCve;
            Objeto.ValueMember = idCve;
            DataRow dr = zonas.NewRow();
            //zonas.Rows.InsertAt((dr), 0);
            Objeto.SelectedValue = 0;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            remi = txtRemision.Text;
           
            try
            {
                if (remi.Length > 0)
                {
                    conectarAccion();
                }
                else
                {
                    MessageBox.Show("No se puede dejar el campo REMISION en blanco.");
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ee)
            {
               MessageBox.Show("Verifica informaciòn", "Aviso");
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
                        comboArticulo.Enabled = true;
                        btnConectar.Enabled = true;
                        btnLeer.Enabled = true;
                        btnLeer.Visible = true;
                        //menuItem2.Enabled = true;
                        b = true;
                    }
                    else
                    {
                        MessageBox.Show("Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        b = false;
                        elimina = cm.deleteRemision(remition);
                    }
                }
                else
                {
                    MessageBox.Show("Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    elimina = cm.deleteRemision(remition);
                    b = false;
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                elimina = cm.deleteRemision(remition);
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
            booya = new string[rows];
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
            //bool cierre = false;
            string[] booya = getEPCS();
            int posicion = 0;
            try
            {
                // MessageBox.Show("Lectura Iniciada", "INICIO");
                while (RFID == true)
                {
                    reader.Read();
                    if (reader.TagCount > 0)
                    {
                        bool cierre = false;
                        foreach (Tag eti in reader.Tags)
                        {
                            if (cierre)
                            {
                                break;
                            }

                            //dtRemi2 = cm.InfoRemision(remi);
                            //numProdRemi = dtRemi2.Rows.Count;
                            string tag = eti.ToString();
                            //for (int x = 0; x < numProdRemi; x++)
                            for (int x = 0; x < booya.Length; x++)
                            {
                                if (!epcs.Contains(tag))
                                {
                                    if (tag.Equals(booya[x]))
                                    {
                                        epece = tag;
                                        epcs.Add(epece);

                                        Ptone1.Play();
                                        //result = cm.verificarEscuadra(epece, folios[1], folios[8], pxt, folios[12]);
                                        //string pedidoOrigen = cm.getPedidoRemision(remi);
                                        //string codigoprod = cm.getcodiremi(remi);
                                        //result = cm.asignaEscuadraFisica(epece, qtyTarima, completo, remi, pos, codigoprod, pedidoOrigen, remision);
                                        dtRemi = cm.remiforescvirtual(remi, ArtRemision);
                                        dtRemi2 = cm.InfoRemision(remi); 
                                        //posicionEsc = Convert.ToInt32(ubicacion);
                                        newid = cm.newIdEscVirtual(remi, ArtRemision, pzaArtRemi);
                                        result = cm.remiEscuadraVirtual(epece, dtRemi, posicion,newid);//aqui pasar newid
                                        updateDetremision = cm.updateDetremi(remi, ArtRemision);
                                        posicion++;

                                        if (posicion >= dtRemi2.Rows.Count)
                                        {
                                            MessageBox.Show("SE ASIGNARON LAS ESCUADRAS PARA TODOS LOS ARTICULOS DE LA REMISION '" + remi + "'", "EXITO");
                                            comboArticulo.Invoke((Action)(() => comboArticulo.Enabled = true));
                                            btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee)));
                                            btnSalir.Invoke((Action)(() => btnSalir_Click(btnSalir, ee)));
                                            cierre = true;
                                            RFID = false;
                                            break;
                                        }
                                        else
                                        {
                                            btnDetener.Invoke((Action)(() => btnDetener_Click(btnDetener, ee))); 
                                            comboArticulo.Invoke((Action)(() => comboArticulo.Enabled = true));
                                            MessageBox.Show("Escuadra Asignada para '" + ArtRemision + "'", "EXITO");
                                            txtPza.Invoke((Action)(() => txtPza.Text = ""));
                                            cierre = true;
                                            break;
                                            //dataGrid1.Invoke((Action)(() => fillDataGridEsc()));
                                        }
                                        //break;//de aqui regresa al foreach despues de que asigno la escuadra virtual
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
                        //if (cierre == true)
                        break;
                        Cursor.Current = Cursors.Default;
                    }
                }
            }

            catch (ObjectDisposedException odex)
            {
                Cursor.Current = Cursors.Default;
                elimina = cm.deleteRemision(remition);
            }
            catch (ThreadAbortException ex)
            {
                Cursor.Current = Cursors.Default;
                elimina = cm.deleteRemision(remition);
                MessageBox.Show(ex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            catch (Exception eex)
            {
                Cursor.Current = Cursors.Default;
                elimina = cm.deleteRemision(remition);
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
        }

        public void fillDataGridEsc()
        {
            DataTable dt = cm.getEscuadraWDR();
            if (dt.Rows.Count > 0)////JLMQ SE AGREGO ESTA VALIDACION PARA QUE EN CASO DE QUE NO EXISTAN ESCUADRAS VIRTUALES SE NOTIFIQUE AL USUARIO
            {
                try
                {
                    dataGrid1.DataSource = dt;
                }
                catch (Exception e)
                {
                    error = e.Message;
                    elimina = cm.deleteRemision(remition);
                    MessageBox.Show("No hay escuadras Disponibles", "Advertencia");
                    //LiberarControles(this);
                    this.Dispose();
                    GC.Collect();
                    frmMenu_Almacen fmp = new frmMenu_Almacen(user);
                    fmp.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("No hay escuadras Disponibles", "Advertencia");
                elimina = cm.deleteRemision(remition);
                this.Dispose();
                GC.Collect();
                frmMenu_Almacen fmp = new frmMenu_Almacen(user);
                fmp.Visible = true;
            }
        }

        private void txtRemision_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (txtRemision.Text.Length > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    remi = txtRemision.Text;
                    llenaCBConexion(comboArticulo, "CodigoArticulo", "CodigoArticulo", "SELECT CodigoProducto AS CodigoArticulo FROM detRemision WHERE Remision = '" + remi + "' AND conEscuadra = 0", "Solutia", 1);
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception excp)
            {
                //MessageBox.Show(excp.Message, "ERROR 1");
            }
           
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //this.Close();ASI ESTABA PERO NO CERRABA BIEN 
            try
            {
                DialogResult usuElige = MessageBox.Show("¿ESTAS SEGURO QUE DESEAS SALIR DE ESTE PROCESO? AUN NO SE CONCLUYE", "ALERTA", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.Dispose();
                if (usuElige == DialogResult.Yes)
                {
                    elimina = cm.deleteRemision(remi);
                    frmMenu_Almacen fma = new frmMenu_Almacen(user);
                    fma.Show();
                }
            }
            catch (Exception delete)
            {
                error = delete.Message;
            }
        }

        private void txtRemision_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void txtCodPord_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox senderComboBox = (ComboBox)sender;
            try
            {
                
                ArtRemision = comboArticulo.SelectedValue.ToString();
                pzaArtRemi = cm.PzaXCodigoRemi(remi, ArtRemision, sucursal);
                txtPza.Text = pzaArtRemi.ToString();
                btnConectar.Enabled = true;
                btnConectar.Visible = true;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message, "Error 2");
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox senderComboBox = (ComboBox)sender;
            try
            {
                ubicacion = comboBox1.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "ERROR 3");
 
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            frmMenu_Almacen fma2 = new frmMenu_Almacen(user);
            fma2.Show();

        }
        
    }
}