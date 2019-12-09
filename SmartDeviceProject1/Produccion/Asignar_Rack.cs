using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Intermec.DataCollection;
using Intermec.DataCollection.RFID;
using Intermec.Device.Audio;
using System.Threading;

namespace SmartDeviceProject1.Produccion
{
    public partial class Asignar_Rack : Form
    {
        ValidateOP vop = new ValidateOP();
        BRIReader reader;
        Boolean b = false;
        PolyTone Ptone1 = new PolyTone();
        PolyTone Ptone2 = new PolyTone(300, 100, Tone.VOLUME.VERY_LOUD);
        Boolean RFID = false;
        String att1 = "100";
        String ant = "1";
        string tag = null;
        Thread hilo;
        string op;
        string lote; 
        string epc;
        cMetodos cm = new cMetodos();
        string[] usu;
        CalculoRacks cr = new CalculoRacks();
        Decimal calculo = 0;
        string codigo;
        int tipoRack;
        string pedido;
        string[] opInfo = null;
        int renglon;
        Decimal cantParcialidad;
        string newId;
        int id;
        int cantidad;
        string descripcion;
        string tipo;
        string color;
        string estatus;
        string cliente;
        bool respuesta = false;
        bool dtVacio = false;
        bool elimina = true;
        bool EscDisponible = false;
        string ubicacionTag = "";



        
        


        public Asignar_Rack(string [] user)
        {
            usu = user;
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {

        }

        private void btnLeer_Click(object sender, EventArgs e)
        {

        }

        public void leer_tag()
        {
            
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            op = txtOP.Text;            
            if (string.IsNullOrEmpty(op))
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("EL CAMPO ORDEN DE PRODUCCION NO PUEDE ESTAR EN BLANCO", "ADVERTENCIA");
                dgInfoProd.Visible = false;
                dgInfoProd.Enabled = false;
                dgPaquetes.Visible = false;
                dgPaquetes.Enabled = false;                
                
            }
            else
            {
                fillDataGrid(op);
                if (dtVacio == true)
                {
                    dgPaquetes.Visible = true;
                    dgPaquetes.Enabled = true;
                    txtOP.Text = "";
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    dgPaquetes.Visible = false;
                    dgPaquetes.Enabled = false;
                    txtOP.Text = "";
                    MessageBox.Show("La Orden de Producción "+op+" no existe","VERIFICAR");
                    
                }
            }
            Cursor.Current = Cursors.Default;
        }


        public void fillDataGrid(string op)
        {
            try
            {
                DataTable dt = vop.validaOrden(op);
                DataTable dt2 = vop.validaOrden(op);
                dgPaquetes.DataSource = dt;
                dgInfoProd.DataSource = dt2;
                if (dt.Rows.Count ==  0)
                    dtVacio = false;
                else
                    dtVacio = true;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Hubo un asunto con la conexion. \nFavor de Reintentar en unos momentos", "Advertencia");
                this.Dispose();
                frmMenu_Almacen fma = new frmMenu_Almacen(usu);
                fma.Show();
            }
        }

        //private void dgPaquetes_CurrentCellChanged(object sender, EventArgs e)
        //{
        //    DataRow row = dgPaquetes.selec            
        //}

        private void dgPaquetes_Click(object sender, EventArgs e)
        {
            dgPaquetes.Enabled = false;
            dgPaquetes.Visible = false;
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                int columns;
                int columnas;

                //REPLICAR FUNCION QUE CONSULTA INTELISIS
                //GUARDAR INFORMACION EN 229 Y MOSTRAR EN DG


                columns = ((DataTable)dgPaquetes.DataSource).Columns.Count;
                columnas = ((DataTable)dgPaquetes.DataSource).Columns.Count;

                opInfo = new string[columnas];

                for (int x = 0; x < columnas; x++)
                {
                    string index = dgPaquetes.CurrentCell.ToString();
                    int columnIndex = dgPaquetes.CurrentCell.ColumnNumber;
                    int rowIndex = dgPaquetes.CurrentCell.RowNumber;
                    string value = dgInfoProd[rowIndex, x].ToString();
                    opInfo[x] = value;
                }
                
                op = opInfo[0];
                codigo = opInfo[1];
                cantidad = Convert.ToInt32(opInfo[2]);
                color = opInfo[3];
                lote = opInfo[4];
                id = Convert.ToInt32(opInfo[5]);
                descripcion = opInfo[6];
                tipo = opInfo[7];                
                estatus = opInfo[8];
                renglon = Convert.ToInt32(opInfo[9]);
                

                DialogResult usuElige = MessageBox.Show("¿ELEGISTE LA ORDEN: "+op+", LOTE:"+lote+", CODIGO:"+codigo+", CANTIDAD: "+cantidad+"?","ALERTA", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
                if (usuElige == DialogResult.Yes)
                {
                    respuesta = vop.insertOP(opInfo);//INSERT en la tabla catProdD   
                    if (respuesta == true)
                    {
                        panelStock.Enabled = true;
                        panelStock.Visible = true;
                        string query = "SELECT Descripcion AS Items, Descripcion AS ID FROM ZonaBustamante";
                        llenaComboBox(cbZonas, "Items", "ID", query, cMetodos.CONEXION);
                    }
                    else
                    {
                        elimina = cm.deleteOP(lote);                    
                        MessageBox.Show("ERROR AL CONSULTAR EN LA BASE DE DATOS","ERROR");
                    }             
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    dgPaquetes.Enabled = true;
                    dgPaquetes.Visible = true;
                    //this.Close();
                }
                Cursor.Current = Cursors.Default;

            }
            catch (Exception expp)
            {
                elimina = cm.deleteOP(lote);
                Cursor.Current = Cursors.Default;
                string error = expp.Message;
                MessageBox.Show("ERROR AL CONSULTAR INTELISIS, VERIFICAR INFORMACIÓN","ERROR");
            }                       
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            frmMenu_Principal fmp = new frmMenu_Principal(usu);
            fmp.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnToStock_Click(object sender, EventArgs e)
        {

        }


        public void llenaComboBox(ComboBox Objeto, string nomCve, string idCve, string consulta, string conex)
        {
            DataTable dt = cm.getDatasetConexionWDR(consulta, conex);
            if (dt == null)
            {
                MessageBox.Show("NO SE PUEDE CONSULTAR LAS UBICACIONES EN ESTE MOMENTO", "ERROR");
                this.Close();
                return;
            }

            Objeto.DataSource = null;
            Objeto.DataSource = dt;
            Objeto.DisplayMember = nomCve;
            Objeto.ValueMember = idCve;
            dt.Columns[0].MaxLength = 255;
            DataRow dr = dt.NewRow();
            string opcSelec = "SELECCIONAR UBICACIÓN";
            dr[nomCve] = (dt.Rows.Count > 0) ? opcSelec : "NO HAY UBICACIONES DISPONIBLES";
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

        private void btnToStock_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int idEsc;
            string idTag = "";
            idTag = txtNumTag.Text.ToString();
            ubicacionTag = cbZonas.SelectedValue.ToString();

            
            DialogResult usuElige = MessageBox.Show("ELEGISTE LA UBICACIÓN: "+ubicacionTag+" PARA EL ID DEL TAG NUMERO: "+idTag+" ¿DESEAS CONTINUAR? ","ALERTA", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
            Cursor.Current = Cursors.Default;

            if (usuElige == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    if (ubicacionTag == "0")
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("ELIGE UNA UBICACIÓN POR FAVOR", "ERROR");
                        //btnToStock.Enabled = false;
                        //btnToStock.Enabled = false;
                    }
                    else
                    {


                        if (!(string.IsNullOrEmpty(idTag)))
                        {
                            idEsc = Convert.ToInt32(idTag);
                            if (idEsc > 0)
                            {
                                EscDisponible = cm.validaEscDisp(idEsc);
                                if (EscDisponible == true)
                                {
                                    int res;
                                    res = cm.execEP(opInfo, usu[4]);//SE CREA LA ENTRADA DE PRODUCCION
                                    if (res == 1)
                                    {
                                        
                                        cm.ubicaEscuadra(idEsc, opInfo, ubicacionTag);
                                        //ubicaEscuadra ubicar escuadra
                                        Cursor.Current = Cursors.Default;
                                        MessageBox.Show("Se realizo la entrada de produccion en Intelisis", "EXITO");
                                        frmMenu_Almacen fma = new frmMenu_Almacen(usu);
                                        fma.Show();
                                    }
                                    else
                                    {
                                        Cursor.Current = Cursors.Default;
                                        elimina = cm.deleteOP(lote);
                                        MessageBox.Show("REPITE DE NUEVO EL PROCESO", "ERROR");
                                        frmMenu_Almacen fma = new frmMenu_Almacen(usu);
                                        fma.Show();
                                    }
                                }
                                else
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("LA ESCUADRA QUE ELEGISTE NO ESTA DISPONIBLE VERIFICA LA INFORMACION", "Informacion");

                                }
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("EL IDENTIFICADOR DE LA ESCUADRA DEBE SER MAYOR A CERO", "ERROR");
                            }
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("EL CAMPO ID ESCUADRA ESTA VACIO, INGRESA UN ID POR FAVOR", "ERROR");
                        }
                    }
                }
                catch (Exception ExcpEP)
                {
                    Cursor.Current = Cursors.Default;
                    elimina = cm.deleteOP(lote);
                    string error;
                    error = ExcpEP.Message;

                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                dgPaquetes.Enabled = true;
                dgPaquetes.Visible = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            panelStock.Visible = false;
            panelStock.Enabled = false;
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

        private void txtNumTag_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtNumTag.Text))
            {
            }
            else
            {
                MessageBox.Show("EL CAMPO ID TAG SOLO ACEPTA VALORES NUMERICOS", "ERROR");
                txtNumTag.Text = "";
                txtNumTag.Focus();
            }
        }
        



    }
}