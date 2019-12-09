using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SmartDeviceProject1.Inventario
{
    public partial class Carga_Inv : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();        
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos ws = new cMetodos();

        string consultaGeneral = "", consultaFinal = "";
        public Label[] etiqueta;
        string[] nom;
        string[] s1;
        string[] s2;
        string[] s3;
        string[] s4;
        string[] s5;
        string caf,ubicacion;
        string[] tmp;
        string[] usuario;

        public Carga_Inv(string[] usu)
        {
            usuario = usu;
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            llenaCBConexion(cbSucursales, "Nombre", "Sucursal", "SELECT Descripcion AS Nombre, Descripcion AS Sucursal FROM ZonaBustamante where Sucursal =" + usuario[3] + "", "Solutia", 1);
            Cursor.Current = Cursors.Default;
            //llenaCBConexion(cbZonas, "Descripcion", "Descripcion", "SELECT Descripcion, Descripcion FROM ZonaBustamante", "Solutia", 1);

        }

        private void mi_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        public void llenaCBConexion(ComboBox Objeto, string nomCve, string idCve, string consulta, string conexion, int sel)
        {            
            DataTable zonas = ws.getDatasetConexionWDR(consulta, conexion);
            if (zonas == null) {
                MessageBox.Show("Se perdio la conexion a la Base de Datos, intentalo más tarde");
                this.Close();           
                return;
            }
            Objeto.DataSource = null;
            Objeto.DataSource = zonas;
            Objeto.DisplayMember	= nomCve;
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

        private void cbSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        public void llenaCBAlmacen(ComboBox Objeto, string nomCve, string idCve, string consulta)
        {
            //DataTable cosas = ws.getDatasetConexionWDR(consulta, "Solutia");//estaba solutia
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
            try
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
            catch (NullReferenceException excep)
            {
                string error = excep.Message;
                return null;
            }
        }

        private void cbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckbZona.Checked = false;
        }

        private void ckbZona_CheckStateChanged(object sender, EventArgs e)
        {
            //if (cbAlmacen.SelectedIndex > 0)
            //{
                if (ckbZona.Checked == true)
                {
                    //llenaCBConexion(cbZonas, "ClaveZona", "ClaveZona", "select ClaveZona, ClaveZona from zonas where idAlmacen = '" + cbAlmacen.SelectedValue.ToString() + "'", "ConsolaAdmin", 0);
                    //llenaCBConexion(cbZonas, "ClaveZona", "ClaveZona", "SELECT Descripcion, Descripcion FROM ZonaBustamante", "Solutia", 0);
                    
                    
                }
                //if (ckbZona.Checked == false)
                //{
                //    ckbRack.Checked = false;
                //    //ckbPosicion.Checked = false;
                //    cbZonas.DataSource = null;
                //    cbRacks.DataSource = null;
                //    //cbPosiciones.DataSource = null;
                //}
            //}
            //else
            //{
            //    MessageBox.Show("Debe de seleccionar un ALMACEN para cargar sus ZONAS");
            //    ckbZona.Checked = false;
            //}
        }

        private void ckbRack_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckbZona.Checked == true)
                {
                    if (ckbRack.Checked == true)
                    {
                        llenaCBConexion(cbRacks, "Clave", "Clave", "SELECT r.Clave, r.Clave FROM racks as r INNER JOIN Zonas as z on r.IDZona = z.IdZona WHERE z.claveZona = '" + cbZonas.SelectedValue.ToString() + "'", "ConsolaAdmin", 0);
                        //ckbPosicion.Enabled = true;
                    }
                    if (ckbRack.Checked == false)
                    {
                        this.cbRacks.DataSource = null;
                        //ckbPosicion.Checked = false; 
                        //cbPosiciones.DataSource = null;
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

        private void ckbPosicion_CheckStateChanged(object sender, EventArgs e)
        {
            /*if (ckbRack.Checked == true)
            {
                if (ckbPosicion.Checked == true)
                {
                    cbRacks.SelectedIndex = 0;
                    string completa = "SELECT pos.IDPosicion, z.ClaveZona+' '+r.Clave+' '+n.Clave+' '+v.Clave +' '+pos.Clave as Clave " +
                    "from Zonas z INNER JOIN racks r ON z.IdZona = r.IDZona INNER JOIN niveles n ON n.IDRack = r.IDRack INNER JOIN ventanas v ON v.IDNivel = n.IDNivel " +
                    "INNER JOIN posiciones pos ON pos.IDVentana = v.IDVentana WHERE z.IdAlmacen = '" + cbAlmacen.SelectedValue.ToString() + "' and z.IdZona = '" + cbZonas.SelectedValue.ToString() + "' AND r.IDRack = '" + cbRacks.SelectedValue.ToString() + "'";
                    llenaCBConexion(cbPosiciones, "Clave", "IDPosicion", completa, "ConsolaAdmin", 0);

                }
                if (ckbPosicion.Checked == false)
                {
                    cbPosiciones.DataSource = null;
                }
            }
            else
            {
                MessageBox.Show("Debe de habilitar y seleccionar un rack primero");
            }*/
        }

        private void mi_Siguiente_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!(string.IsNullOrEmpty(textBox1.Text)))
            {
                if (!(string.IsNullOrEmpty(textBox2.Text)))
                {
                    if (!(string.IsNullOrEmpty(txtEsc.Text)))
                    {
                        bool EscDisponible = false;
                        int idescuadra = Convert.ToInt32(txtEsc.Text);
                        string codProd = textBox1.Text;
                        int piezas = Convert.ToInt32(textBox2.Text);
                        string posicion = ubicacion = cbSucursales.SelectedValue.ToString();
                        EscDisponible = ws.validaEscDisp(idescuadra);
                        if (piezas > 0)
                        {
                            if (idescuadra > 0)
                            {
                                if (EscDisponible == true)
                                {
                                    Cursor.Current = Cursors.Default;
                                    DialogResult dr = MessageBox.Show("SE CARGARA AL INVENTARIO LA SIGUIENTE INFORMACION: CODIGO DEL PRODUCTO:'" + textBox1.Text + "' , PIEZAS: '" + textBox2.Text + "' , UBICACION: '" + posicion + "' , #ESCUADRA: '" + txtEsc.Text + "' ¿DESEAS GUARDAR ESTA INFORMACION?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                    if (dr == DialogResult.Yes)
                                    {
                                        Cursor.Current = Cursors.WaitCursor;
                                        bool actualizaEscuadra = false;
                                        actualizaEscuadra = ws.cargaInvInicial(codProd, piezas, posicion, idescuadra);
                                        if (actualizaEscuadra == true)
                                        {
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("ESCUADRA ASIGNADA EXITOSAMENTE", "EXITO");
                                            frmMenu_Inventario mi = new frmMenu_Inventario(usuario);
                                            mi.Show();
                                        }
                                        else
                                        {
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("No se pudo cargar la informacion al inventario inicial", "Error");
                                            frmMenu_Inventario mi = new frmMenu_Inventario(usuario);
                                            mi.Show();
                                        }
                                    }
                                    else
                                    {
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
                                MessageBox.Show("EL IDENTIFICADOR DE LA ESCUADRA DEBE SER MAYOR A CERO","ERROR");
                            }
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("LA CANTIDAD DE PIEZAS DEBE SER MAYOR A CERO","ERROR");
                        }
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("EL CAMPO ID ESCUADRA ESTA VACIO, INGRESA UN ID POR FAVOR", "ERROR");
                    }
                
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("CAMPO CANTIDA VACIO, INGRESA LA CANTIDAD A REGISTRAR POR FAVOR", "ERROR");
                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("EL CODIGO DEL PRODUCTO ESTA VACIO, INGRESA ALGUN CODIGO","ERROR");
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

        public string[] obtenDatosInv()
        {
            try
            {
                string[] arreglo = { "", "" };
                string filtro;
                string zona = " ", rack = " ", pos = " ";

                if (ckbZona.Checked == true)
                {
                    if (cbZonas != null)
                    {
                        zona = " and z.IdZona = " + cbZonas.SelectedValue.ToString();
                    }
                    else { MessageBox.Show("Seleccione una ZONA, de lo contrario deseleccione ZONA"); }
                }
                if (ckbRack.Checked == true)
                {
                    if (cbRacks != null)
                    {
                        rack = " and r.IDRack = " + cbRacks.SelectedValue.ToString();
                    }
                    else { MessageBox.Show("Seleccione un RACK, de lo contrario deseleccione RACK"); }
                }
                //if (ckbPosicion.Checked == true)
                //{
                //    if (cbPosiciones != null)
                //    {
                //        pos = " and p.IDPosicion = " + cbPosiciones.SelectedValue.ToString();
                //    }
                //    else { MessageBox.Show("Seleccione una POSICION, de lo contrario deseleccione POSICION"); }
                //}
                ubicacion = cbZonas.SelectedValue.ToString() + cbRacks.SelectedValue.ToString();
                filtro = zona + rack + pos;
                string consultaInvCount = "SELECT COUNT(*) FROM detEscuadras WHERE Posicion = '" + ubicacion + "'";
                string consultaInvGeneral = "SELECT EPC, CodigoProducto, Piezas, Posicion FROM detEscuadras WHERE Posicion = '" + ubicacion + "'";
                consultaFinal = "";
                Cursor.Current = Cursors.WaitCursor;
                int cuantos = ws.getInt(consultaInvCount, "Solutia");
                consultaFinal = consultaInvGeneral;
                arreglo[0] = consultaFinal;
                arreglo[1] = cuantos.ToString();
                Cursor.Current = Cursors.Default;
                return arreglo;
            }
            catch (Exception e)
            {
                Cursor.Current = Cursors.Default;
                //MessageBox.Show(e.Message); 
                return null;
            }
        }

        

        private void cbPosiciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_ParentChanged(object sender, EventArgs e)
        {

        }

        private void cbRacks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbZonas_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(textBox2.Text))
            {
            }
            else
            {
                MessageBox.Show("Este campo solo acepta valores númericos", "Advertencia");
                textBox2.Text = "";
                textBox2.Focus();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        

    }
}