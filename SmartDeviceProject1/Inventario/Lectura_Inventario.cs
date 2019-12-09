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

namespace SmartDeviceProject1
{
    public partial class frmLectura : Form
    {
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();    
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos ws = new cMetodos();
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
        //private delegate void UpdateStatusDelegate(int pos);

        public frmLectura(string[] User,string IDInv)
        {
           
            usuario = User;
            IdInv = IDInv;
            Ptone1.CurrentVolume = PolyTone.VOLUME.NORMAL;
            InitializeComponent();
            llenaPantalla(IDInv);                      
        }

        public void llenaPantalla(string IDInventario)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                limpiaSDF();
                DataTable ds = ws.getDatasetInventarioWDR("select * from detalleInvCong WHERE Estatus = 0 and IDInv = " + IDInventario);
                llenaBase(ds);
                llenaListas();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
        }

        public void llenaBase(DataTable ds)
        {
            try
            {
                string idDetInv, IDInv, IDArt, EPCArt, EPCPos, IDPos, Cont1, Cont2, ExCong, Estatus;
                DataTable registros = ds;
                int regCount = registros.Rows.Count;
                DataRow currentRow;
                for (int x = 0; x < regCount; x++)
                {
                    currentRow = registros.Rows[x];
                    idDetInv = currentRow[0].ToString();
                    IDInv = currentRow[1].ToString();
                    IDArt = currentRow[2].ToString();
                    EPCArt = currentRow[3].ToString();
                    EPCPos = currentRow[4].ToString();
                    IDPos = currentRow[5].ToString();
                    Cont1 = currentRow[6].ToString();
                    Cont2 = currentRow[7].ToString();
                    ExCong = currentRow[8].ToString();
                    Estatus = currentRow[9].ToString();
                    insertaSDF("INSERT INTO detalleInvCong VALUES(" + idDetInv + "," + IDInv + ",'" + IDArt + "','" + EPCArt + "','" + EPCPos + "'," + IDPos + "," + Cont1 + "," + Cont2 + "," + ExCong + ", " + Estatus + ")");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
        }

        public void limpiaSDF() 
        {
            try
            {
                conexion.Open();
                String command = "DELETE FROM detalleInvCong";
                SqlCeCommand updat = new SqlCeCommand(command, conexion);
                updat.ExecuteNonQuery();                    
                conexion.Close();               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }        
        }

        public void llenaListas()
        {
            DataSet listas = consulta("SELECT IDInv, EPCArt, IDArticulo FROM detalleInvCong WHERE (Estatus = 0)");
            DataGridTableStyle tableStyle = new DataGridTableStyle();

            tableStyle.MappingName = listas.Tables[0].TableName;

            GridColumnStylesCollection columnStyles = tableStyle.GridColumnStyles;

            DataGridTextBoxColumn columnStyle = new DataGridTextBoxColumn();
            columnStyle.MappingName = "EPCArt";
            columnStyle.HeaderText = "EPC";
            columnStyle.Width = 150;
            columnStyles.Add(columnStyle);

            columnStyle = new DataGridTextBoxColumn();
            columnStyle.MappingName = "IDArticulo";
            columnStyle.HeaderText = "Articulo";
            columnStyle.Width = 100;
            columnStyles.Add(columnStyle);

            GridTableStylesCollection tableStyles = dataGrid1.TableStyles;
            tableStyles.Add(tableStyle);
            dataGrid1.PreferredRowHeight = 16;
            dataGrid1.RowHeadersVisible = false;  
    
            //------------------------------
            dataGrid1.DataSource = listas.Tables[0];
            //------------------------------
            lstNum.DataSource = listas.Tables[0];
            lstNum.DisplayMember = "IDDetInvCong";
            lstSerie.DataSource = listas.Tables[0];
            lstSerie.DisplayMember = "EPCArt";
            lstDescripcion.DataSource = listas.Tables[0];
            lstDescripcion.DisplayMember = "IDArticulo";
            foreach (DataRow dtRow in listas.Tables[0].Rows)
            {
                epcs.Add(dtRow[1]);
                idArts.Add(dtRow[2]);                
            }               
        }

        public void llenaGrid() 
        {
            System.Threading.Thread.Sleep(250);
            DataSet listas = consulta("SELECT IDInv, EPCArt, IDArticulo FROM detalleInvCong WHERE (Estatus = 0)");
            BindingSource source = new BindingSource();
            source.DataSource = listas;
            dataGrid1.DataSource = source;        
        }

        public void insertaSDF(string consulta)
        {
            try
            {
                SqlCeCommand create = new SqlCeCommand(consulta, conexion);
                conexion.Open();
                create.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error interno", "ERROR");
            }
        }
     
        public DataSet consulta(string command)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(command, conexion);
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ds;
        }        

        private void button2_Click(object sender, EventArgs e)
        {
            conectarAccion();
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
                        button2.Enabled = false;
                        button1.Enabled = true;
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            leerAccion();
        }

        public void leerAccion()
        {
            if (b == true)
            {
                reader.Execute("Attrib ants=" + ant);
                reader.Execute("Attrib fieldstrength=" + att1);
                hilo = new Thread(leer_tag);
                RFID = true;
                button1.Visible = false;
                button1.Enabled = false;
                button3.Visible = true;
                button3.Enabled = true;
                hilo.Start();
            }
            else
            {
                MessageBox.Show("No hay conexión con el reader", "Sin conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                //button2.Enabled = true;
            }
        }

        public int getIntSDF(string consulta)
        {
            int result = 0;
            conexion.Open();
            try
            {
                String sql = consulta;
                SqlCeCommand comm = new SqlCeCommand(sql, conexion);
                SqlCeDataReader rdr;
                rdr = comm.ExecuteReader();
                if (rdr.Read())
                {
                    result = rdr.GetInt32(0);
                }
                else
                {
                    result = -1;
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                conexion.Close();
            }
            return result;
        }

        public bool actualizaInfoServer()
        {
            bool res = false;
            try
            {
                DataSet registros = consulta("Select * from detalleInvCong where Estatus = 1");
                DataTable tablaReg = registros.Tables[0];
                int regCount = tablaReg.Rows.Count;
                DataRow currentRow;
                for (int x = 0; x < regCount; x++)
                {
                    string idDetInv, IDInv;
                    currentRow = tablaReg.Rows[x];
                    idDetInv = currentRow[0].ToString();
                    IDInv = currentRow[1].ToString();
                    ws.inserta("UPDATE detalleInvCong SET Estatus = 1 WHERE IDDetInvCong = " + idDetInv, "ConsolaAdmin");
                }
            }
            catch (Exception ex)
            {
                res = false;
                MessageBox.Show(ex.Message);
            };
            return res;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlDetalles.Dispose();
            pnlDetalles.Visible = false;
        }

        public void leer_tag()
        {
            bool cierre = false;
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
                            Ptone1.Play();
                            if (epcs.Contains(eti.ToString()) == false) 
                            {
                                if (epcsNo.Contains(eti.ToString()) == false) 
                                {
                                    epcsNo.Add(eti.ToString());
                                }
                            }
                            for (int i = 0; i < lstSerie.Items.Count; i++)
                            {                                
                                DataRowView dr = lstSerie.Items[i] as DataRowView;
                                String tmp = dr[1].ToString();
                                if (tmp.Equals(eti.ToString()))
                                {                                   
                                    articuloEpc = eti.ToString();
                                    //update_SDF();
                                    if (pnlDetalles.InvokeRequired)
                                    {
                                        pnlDetalles.Invoke((Action)(() => update_SDF()));                                        
                                    }
                                    //ANterior
                                    //this.button3.Click += new EventHandler(button3_Click);
                                    //cierre = true;
                                    //if (pnlDetalles.InvokeRequired)
                                    //{
                                    //    pnlDetalles.Invoke((Action)(() => llenaPanelDet(articuloEpc)));
                                    //    pnlDetalles.Invoke((Action)(() => button3_Click(this.button3, ee)));
                                    //}
                                    break;                                                                                          
                                }
                            }
                            if (cierre == true)
                                break;
                        }
                    }
                }
                //MessageBox.Show("Lectura Finalizada", "FIN");                
            }
            catch (ObjectDisposedException odex)
            {
                //MessageBox.Show(odex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                //button3_Click(this.button3, ee);
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

        public Boolean updateLeido(String epc)
        {
            Boolean exito = false;
            try
            {
                conexion.Open();

                String command = "UPDATE detalleInvCong SET Estatus = 1 WHERE EPCArt = '" + epc + "'";
                SqlCeCommand updat = new SqlCeCommand(command, conexion);

                if (updat.ExecuteNonQuery() > 0)
                    exito = true;

                conexion.Close();
                return exito;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                return exito;
            }
        }

        public void update_SDF()
        {
            try
            {
                string update = "UPDATE detalleInvCong SET Estatus = 1 WHERE (EPCArt = '" + articuloEpc + "')";
                insertaSDF(update);
                articuloEpc = "";
                //updateLeido(articuloEpc);
                DataSet ds = consulta("SELECT IDInv, EPCArt, IDArticulo FROM detalleInvCong WHERE (Estatus = 0)");
                DataTable dt = ds.Tables[0];
                dataGrid1.DataSource = null;
                dataGrid1.DataSource = dt;
                pnlDetalles.Visible = false;
                lstSerie.DataSource = null;
                lstSerie.DataSource = ds.Tables[0];
                lstSerie.DisplayMember = "EPCArt";                          
            }
            catch (Exception ex)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnFinalizar.Enabled = true;
            button1.Visible = true;
            button1.Enabled = true;
            button3.Visible = false;
            button3.Enabled = false;
            RFID = false;                     
        }

        public void detenerAccion() 
        {
            btnFinalizar.Enabled = true;
            button1.Visible = true;
            button1.Enabled = true;
            button3.Visible = false;
            button3.Enabled = false;
            RFID = false;
            llenaPanelDet(articuloEpc);
        }

        public void limpia()
        {
            hilo.Abort();
            reader.Dispose();
            button2.Enabled = true;
            b = false;
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            limpia();
            List<string> listaUpdate = new List<string>();            
            List<string> listaCont = new List<string>();
            SqlCeDataReader rs = null;
            Stack st = new Stack();
            int contador =0;
            try
            {
                int cuenta = getIntSDF("select count(*)FROM detalleInvCong WHERE (Estatus = 1)");
                if (cuenta > 0)
                {
                    SqlCeCommand select = new SqlCeCommand("SELECT IDDetInvCong, Conteo1  FROM detalleInvCong  WHERE (Estatus = 1)", conexion);
                    conexion.Open();
                    rs = select.ExecuteReader();
                    while (rs.Read())
                    {
                        listaUpdate.Add(rs.GetValue(0).ToString());                        
                        listaCont.Add(rs.GetValue(1).ToString());
                    }

                    int registros = listaUpdate.Count;
                    for (int z = 0; z < registros; z++)
                    {
                        int cosa = ws.inserta("UPDATE detalleInvCong SET Estatus = 1, Conteo1 =  " + listaCont[z] +" WHERE IDDetInvCong = " + listaUpdate[z], "ConsolaAdmin");
                        if (cosa == 1)
                        {
                            contador += 1;
                        }
                    }

                    conexion.Close();
                    if ((contador > 0))
                    {
                        /////*
                        if (ws.inserta("update InventarioCongelado SET estatus = 1 WHERE IDInv =" + IdInv, "ConsolaAdmin") == 1)
                        {
                            MessageBox.Show("Transferencia completa", "EXITO");
                        }

                        /////*
                        ///// solo pruebas
                        MessageBox.Show("Transferencia completa-//Tabla InventarioCongelado omitida", "EXITO");
                        
                        //this.Dispose();
                        this.Close();
                        return;
                    }
                }
                else 
                {
                    MessageBox.Show("No se realizó ningun cambio","Informacion");
                    frmMenu_Inventario fmi = new frmMenu_Inventario(usuario);
                    fmi.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }     

        private void frmLectura_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void llenaPanelDet(string epc) 
        {
            try
            {
                pnlDetalles.Visible = true;
                //From .sdf local file
                DataSet datosIn = consulta("select idArticulo, ExCongelada from detalleInvCong where EPCArt = '" + epc + "'");
                DataRow colIn = datosIn.Tables[0].Rows[0];
                string articulo = colIn[0].ToString();
                cantCong = Convert.ToDouble(colIn[1].ToString());
                cantCont = cantCong;
                //From WS GoManager
                string consulta2 = "SELECT Articulo, Descripcion1, Categoria, Familia, Linea, unidad, Factor, Peso, Estatus, CantidadTarima, UnidadTarima FROM Art where Articulo = '" + articulo + "'";
                DataTable todo = ws.getDatasetConexionWDR(consulta2, "Intelisis");
                if (todo.Rows.Count > 0)
                {
                    DataRow datos = todo.Rows[0];
                    lblArticulo.Text = datos[0].ToString();
                    lblDescrip.Text = datos[1].ToString();
                    lblCat.Text = datos[2].ToString();
                    lblFamilia.Text = datos[3].ToString();
                    lblLinea.Text = datos[4].ToString();
                    btnUnidad.Text = datos[5].ToString();
                    factor = Convert.ToDouble(datos[6].ToString()) / 10000;
                    lblCantTar.Text = datos[9].ToString();
                    lblUnTar.Text = datos[10].ToString();
                    lblCantCong.Text = cantCong.ToString();
                    txtCantCont.Text = cantCont.ToString();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Intente leer de nuevo", "Error de lectura");
            }            
        }

        private void btnUnidad_Click(object sender, EventArgs e)
        {
            lblCantCong.Text = (cantCong / factor).ToString();
            txtCantCont.Text = (cantCont / factor).ToString();
            btnPiezas.Enabled = true;
            btnUnidad.Enabled = false;           
        }

        private void btnPiezas_Click(object sender, EventArgs e)
        {
            lblCantCong.Text = cantCong.ToString();
            txtCantCont.Text = cantCont.ToString();
            btnUnidad.Enabled = true;
            btnPiezas.Enabled = false;            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string update = "UPDATE detalleInvCong SET Estatus = 1, Conteo1 = " + txtCantCont.Text + " WHERE (EPCArt = '" + articuloEpc + "')";
            insertaSDF(update);
            articuloEpc = "";
            //updateLeido(articuloEpc);
            DataSet ds = consulta("SELECT IDInv, EPCArt, IDArticulo FROM detalleInvCong WHERE (Estatus = 0)");            
            DataTable dt = ds.Tables[0];
            dataGrid1.DataSource = null;
            dataGrid1.DataSource = dt;
            pnlDetalles.Visible = false;
            lstSerie.DataSource = null;
            lstSerie.DataSource = ds.Tables[0];
            lstSerie.DisplayMember = "EPCArt";            
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int rowIndex = dataGrid1.CurrentRowIndex;
            //    string idPos, idart, epc;
            //    epc = dataGrid1[rowIndex, 1].ToString();
            //    idart = dataGrid1[rowIndex, 2].ToString();
            //    idPos = getIntSDF("SELECT IDPosicion FROM detalleInvCong WHERE (IDArticulo = '" + idart + "')").ToString();
            //    DataTable todo = ws.getDatasetWDR("select z.ClaveZona as Zona, r.Clave as Rack, n.Clave as Nivel, v.Clave as Vent, p.Clave as Pos from posiciones p INNER JOIN ventanas v ON p.IDVentana = v.IDVentana INNER JOIN niveles n ON n.IDNivel = v.IDNivel INNER JOIN racks r ON r.IDRack = n.IDRack INNER JOIN Zonas z on z.IdZona = r.IDZona where p.IDPosicion = " + idPos);
            //    DataRow datos = todo.Rows[0];
            //    MessageBox.Show("Zona:" + datos[0].ToString() + "\nRack:" + datos[1].ToString() + "\nVentana:" + datos[3].ToString() + "\nPos:" + datos[4].ToString(), "UBICACION");
            //    articuloEpc = epc;
            //    ////llenaPanelDet(epc);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //};
            string epc;
            try
            {
                int rowIndex = dataGrid1.CurrentRowIndex;
                epc = dataGrid1[rowIndex, 0].ToString();
                SmartDeviceProject1.Inventario.Buscar_Tag bt = new SmartDeviceProject1.Inventario.Buscar_Tag(epc);
                bt.Show();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
        }

        private void btnTagsNo_Click(object sender, EventArgs e)
        {
            if (epcsNo.Count > 0)
            {
                Inventario.Buscar_Ubicacion bu = new SmartDeviceProject1.Inventario.Buscar_Ubicacion(epcsNo);
                bu.Visible = true;
            }
            else 
            {
                MessageBox.Show("No se encontraron tags distintos","INFORMACION");
            }
        }
    }
}