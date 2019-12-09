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
using System.Data.SqlClient;
//using test_emulator;


namespace SmartDeviceProject1.Inventario
{
    public partial class Leer_Inventario : Form
    {
        cMetodos c = new cMetodos();

        SqlCeConnection conexion = new SqlCeConnection("Data Source=\\Flash File Store\\SSPB\\GoMonitor.sdf;");
        PolyTone Ptone1 = new PolyTone();
        PolyTone Ptone2 = new PolyTone(
        500, 300, Tone.VOLUME.VERY_LOUD);
        Tone tones = new Tone();
        string caf, epece;
        string cafActivo, IdInv;
        string[] datos;
        BRIReader reader;
        Boolean b = false;
        Boolean RFID = false;
        String att1 = "100";
        String ant = "1";
        Thread hilo;
        int usu, nuevo;
        double factor, cantCong, cantCont;
        string articuloEpc = "";
        EventArgs ee = new EventArgs();
        ArrayList epcs = new ArrayList();
        ArrayList idArts = new ArrayList();
        ArrayList epcsNo = new ArrayList();
        ArrayList idArtsNo = new ArrayList();
        string remi = "";
        int res = 0, diferencia,ubi;
        string epc = "";
        string codigo = "", op = "";
        string[] user, booya;
        string[] opInfo = null;
        bool iConteo = true;
        string updateC = "";

        int idConteo;
        int numConteo;
        int cantidadConteo;

        int tag;
        string ubicacion = "";


        string select;
        cMetodos cm = new cMetodos();
        int idInv;
        int numEsc = 0;
        string ubiConteo = "";
        string descripcion = "";
        int conteo;
        int status;
            
        public Leer_Inventario(string idInven,string[] usuario)
        {
            InitializeComponent();
            
            idInv = Convert.ToInt32(idInven);//EN LA LINEA DE ABAJO SE AGREGO EPC EN LUGAR DE ProdSKU
            conteo = cm.getConteo(idInv);
            status = cm.getStatusConteo(idInv);
            select = "SELECT dc.Codigo as Codigo, dc.ProdCB as Ubicacion, de.idEscuadra AS Piezas, dc.EPC AS EPC FROM DetalleCongelado dc INNER JOIN DetEscuadras de ON DC.EPC = de.EPC WHERE dc.idInvCong= " + idInv + " AND dc.Estatus = "+status+" ORDER BY de.idEscuadra ASC";
            user = usuario;
            ubi = c.getUbicacion(idInv);
            
            switch (ubi)
            {
                case 0:
                    MessageBox.Show("Hubo un asunto con la conexion. \nFavor de Reintentar en unos momentos", "Advertencia");
                    break;
                case 1:
                    fillDataGrid();                   
                    break;
                case 2:
                    fillDataGrid();
                    break;
            }
            idInv = Convert.ToInt32(idInven);

            ubiConteo = c.getUbiConteo(idInv);
            ubiConteo = ubiConteo.Trim();
            numEsc = c.getNumConteo(ubiConteo,idInv);
            lbInventario.Text = "TOTAL DE ESCUADRAS POR CONTAR PARA LA UBICACION ELEGIDA: " + numEsc + "";
        }

        public void fillDataGrid()
        {
            try
            {
                DataSet dt = consulta(select);
                DataGridTableStyle tableStyle = new DataGridTableStyle();

                tableStyle.MappingName = dt.Tables[0].TableName;

                GridColumnStylesCollection columnStyles = tableStyle.GridColumnStyles;

                DataGridTextBoxColumn columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "Codigo";
                columnStyle.HeaderText = "CODIGO";
                columnStyle.Width = 89;
                columnStyles.Add(columnStyle);

                columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "Ubicacion";
                columnStyle.HeaderText = "UBICACIÓN";
                columnStyle.Width = 89;
                columnStyles.Add(columnStyle);

                columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "Piezas";
                columnStyle.HeaderText = "TAG";
                columnStyle.Width = 38;
                columnStyles.Add(columnStyle);

                columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "EPC";
                columnStyle.HeaderText = "EPC";
                columnStyle.Width = 38;
                columnStyles.Add(columnStyle);

                dataGrid1.TableStyles.Clear();//se agrego para limpiar antes de agregar
                GridTableStylesCollection tableStyles = dataGrid1.TableStyles;
                tableStyles.Add(tableStyle);
                dataGrid1.PreferredRowHeight = 16;
                dataGrid1.RowHeadersVisible = false;

                //------------------------------
                dataGrid1.DataSource = dt.Tables[0];
                //------------------------------
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                MessageBox.Show("Hubo un asunto con la conexion. \nFavor de Reintentar en unos momentos", "Advertencia");
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
        }

        public void conectarAccion()
        {

        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            
        }

        public void leerAccion()
        {
        }

        public void leer_tag()
        {
        }

        public string[] getEPCS()
        {
            int rows = ((DataTable)dataGrid1.DataSource).Rows.Count;
            string[] booya = new string[rows];
            for (int x = 0; x < rows; x++)
            {
                string index = dataGrid1.CurrentCell.ToString();
                int rowIndex = dataGrid1.CurrentCell.RowNumber;
                string value = dataGrid1[x, 0].ToString();
                booya[x] = value;
            }
            return booya;
        }

        private void fillPanel(string cb)
        {
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
           
        }
        
        public DataSet consulta(string select)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = c.getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                SqlCommand command = new SqlCommand(select, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ds;
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
          
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {

            dataGrid1.Enabled = false;
            dataGrid1.Visible = false;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //int columns;
                int columnas;
                


                //columns = ((DataTable)dgPaquetes.DataSource).Columns.Count;
                columnas = ((DataTable)dataGrid1.DataSource).Columns.Count;

                opInfo = new string[columnas];

                for (int x = 0; x < columnas; x++)
                {
                    string index = dataGrid1.CurrentCell.ToString();
                    int columnIndex = dataGrid1.CurrentCell.ColumnNumber;
                    int rowIndex = dataGrid1.CurrentCell.RowNumber;
                    string value = dataGrid1[rowIndex, x].ToString();
                    opInfo[x] = value;
                }
                codigo = opInfo[0];
                ubicacion = opInfo[1];
                tag = Convert.ToInt32(opInfo[2]);
                epc = opInfo[3];
                descripcion = c.getDescripcionCodigo(codigo);

                DialogResult usuElige = MessageBox.Show("ELEGISTE EL TAG: " + tag + " CON CODIGO " + codigo + " ¿DESEAS CONTINUAR?", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (usuElige == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.Default;
                    lbArticulo.Text = codigo;
                    lbDescripcion.Text = descripcion;
                    panelStock.Enabled = true;
                    panelStock.Visible = true;                   
                
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    dataGrid1.Enabled = true;
                    dataGrid1.Visible = true;
                    //this.Close();
                }

            }
            catch(Exception exe)
            {
                string excError = "";
                excError = exe.Message;
                MessageBox.Show("ERROR AL OBTENER INFORMACION DE LA BASE DE DATOS, REPITE EL PROCESO POR FAVOR","ERROR");
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (dataGrid1.VisibleRowCount > 0)
            {
                DialogResult dr = MessageBox.Show("¿Seguro que desea salir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    //c.finalizaInventario(idInv);//MODIFICAR PARA FINALIZAR EL INVENTARIO

                    Cursor.Current = Cursors.WaitCursor;
                    frmMenu_Principal fmp = new frmMenu_Principal(user);
                    fmp.Show();
                    this.Dispose();
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                }
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                frmMenu_Almacen fma = new frmMenu_Almacen(user);
                fma.Show();
                this.Dispose();
                Cursor.Current = Cursors.Default;
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            frmMenu_Principal fmp = new frmMenu_Principal(user);
            fmp.Show();
            this.Dispose();
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void dgPaquetes_Click(object sender, EventArgs e)
        {

        }

        private void txtNumTag_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnToStock_Click_1(object sender, EventArgs e)
        {

        }

        private void btnToStock_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            { 
                
                string conteo = "";
                int intConteo;
                int numConteo;
                int finalizar;

                conteo = txtConteo.Text.ToString();
                intConteo = Convert.ToInt32(conteo);
                if (intConteo > 0)
                {
                    if (!(string.IsNullOrEmpty(conteo)))
                    {
                        updateC = "";
                        numConteo = c.getConteo(idInv);
                        iConteo = c.updateConteos(idInv, epc, numConteo, intConteo);
                        if (iConteo == true)
                        {
                            
                            MessageBox.Show("Realizaste el conteo del codigo '" + codigo + "'", "EXITO");
                            panelStock.Enabled = false;
                            panelStock.Visible = false;
                            txtConteo.Text = "";
                            ubiConteo = c.getUbiConteo(idInv);
                            ubiConteo = ubiConteo.Trim();
                            numEsc = c.getNumConteo(ubiConteo,idInv);
                            lbInventario.Text = "TOTAL DE ESCUADRAS POR CONTAR PARA LA UBICACION ELEGIDA: " + numEsc + "";
                            fillDataGrid();
                            dataGrid1.Enabled = true;
                            dataGrid1.Visible = true;
                            finalizar = dataGrid1.VisibleRowCount;
                            if (finalizar == 0)
                            {
                                if (numConteo == 4)
                                {
                                }
                                else
                                {
                                    bool updtInv = false;
                                    updtInv = c.updateStatusInv(idInv, numConteo);
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("CONTEO FINALIZADO CON EXITO, FAVOR DE AVISAR A EL ENCARGADO", "EXITO");
                                    frmMenu_Inventario fmi = new frmMenu_Inventario(user);
                                    fmi.Show();
                                }
                            }
                            Cursor.Current = Cursors.Default;

                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("ERROR AL GUARDAR CONTEO, REPITE EL PROCESO POR FAVOR", "ERROR");
                        }
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("EL CONTEO ESTA VACIO, INGRESA EL CONTEO ADECUADO POR FAVOR", "ERROR");
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("INGRESA UN NUMERO VALIDO", "ERROR");
                }
            }
            catch (Exception excep)
            {
                Cursor.Current = Cursors.Default;
                string error = "";
                error = excep.Message;
                MessageBox.Show("REPITE EL PROCESO POR FAVOR","ERROR");
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

        private void txtConteo_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtConteo.Text))
            {
            }
            else
            {
                MessageBox.Show("ESTE CAMPO SOLO ACEPTA VALORES NUMERICOS", "ERROR");
                txtConteo.Text = "";
                txtConteo.Focus();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            panelStock.Enabled = false;
            panelStock.Visible = false;
            dataGrid1.Visible = true;
            dataGrid1.Enabled = true;
            txtConteo.Text = "";
        }


    }
}