using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SmartDeviceProject1.Almacen
{
    public partial class Nuevo_Picking : Form
    {
        string[] user, booya;
        cMetodos c = new cMetodos();
        string remision;
        public DataTable dt = null;
        string sucursal;
        bool dtVacio = false;
        string query = "";
        string error = "";
        string usuario = "";
        public DataTable dtRemi = null;
        string updateDetremision = "";
        bool updtDets = false;
        string pedido = "";
        bool escVirtual = false;
        


        public List<EscuadraVirtual> listaTags = new List<EscuadraVirtual>();

        public Nuevo_Picking(string[] sesion)
        {
            try
            {
                InitializeComponent();
                user = sesion;
                sucursal = user[3];
                usuario = user[4];
                if (usuario == "UF-00170" || usuario == "UF-00142")
                {
                    cbOrdenProd.SelectedIndexChanged -= new EventHandler(cbOrdenProd_SelectedIndexChanged);
                    query = "SELECT " +
                                "v.MovID AS Items," +
                                "v.MovID AS ID " +
                            "FROM Venta v " +
                                "INNER JOIN (VentaD vd " +
                                                "INNER JOIN Art on art.Articulo = vd.Articulo " +
                                                "LEFT JOIN ArtUnidad on ArtUnidad.Articulo = vd.Articulo AND ArtUnidad.Unidad = art.Unidad)" +
                                                "on vd.ID = v.ID " +
                            "WHERE " +
                                   "Art.Tipo = 'NORMAL' " +
                                   "AND v.Estatus = 'PENDIENTE' " +
                                   "AND (FechaEmision >= DATEADD(DAY, -4,GETDATE()))" +
                                   "AND V.Mov = 'ARM-BUSTAMANTE' " +
                                   "AND v.Sucursal = 1402 " +
                            "ORDER BY V.Mov DESC";
                    llenaComboBox(cbOrdenProd, "Items", "ID", query, cMetodos.CONEXION_INTELISIS);
                    cbOrdenProd.SelectedIndexChanged += new EventHandler(cbOrdenProd_SelectedIndexChanged);
                }
                else
                {
                    MessageBox.Show("EL USUARIO '" + usuario + "' NO TIENE REMISIONES PENDIENTES POR VALIDAR", "ADVERTENCIA");
                    this.Close();
                }
            }
            catch (Exception exec)
            {
                error = exec.Message;
                MessageBox.Show("ERROR AL CONSULTAR INTELISIS FAVOR DE REVISAR", "ERROR DE RED");
                this.Close();
            }

        }

        public void fillDataGrid(string remision)
        {
            try
            {
                DataTable dt = c.showPaquetesRemision(remision, sucursal);
                dgPaquetes.DataSource = dt;
                if (dt.Rows.Count == 0)
                    dtVacio = false;
                else
                    dtVacio = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Hubo un asunto con la conexion. \nFavor de Reintentar en unos momentos", "Advertencia");
                this.Dispose();
                frmMenu_Almacen fma = new frmMenu_Almacen(user);
                fma.Show();
            }
        }

        public void llenaComboBox(ComboBox Objeto, string nomCve, string idCve, string consulta, string conex)
        {
            DataTable dt = c.getDatasetConexionWDR(consulta, conex);
            if (dt == null)
            {
                MessageBox.Show("NO SE PUEDE CONSULTAR LAS REMISIONES EN ESTE MOMENTO", "ERROR");
                this.Close();
                return;
            }

            Objeto.DataSource = null;
            Objeto.DataSource = dt;
            Objeto.DisplayMember = nomCve;
            Objeto.ValueMember = idCve;
            dt.Columns[0].MaxLength = 255;
            DataRow dr = dt.NewRow();
            string opcSelec = "SELECCIONAR REMISIÓN";
            dr[nomCve] = (dt.Rows.Count > 0) ? opcSelec : "SIN REMISIONES PENDIENTES";
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            remision = txtRemision.Text;
            if (string.IsNullOrEmpty(remision))
            {
                MessageBox.Show("El campo Remision esta en blanco", "ADVERTENCIA");
            }
            else
            {
                fillDataGrid(remision);
                if (dtVacio == true)
                {
                    menuItem2.Enabled = true;
                    dgPaquetes.Visible = true;
                }
                else
                {
                    DialogResult usuElige = MessageBox.Show("La Remision " + remision + " NO Existe", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    dgPaquetes.Enabled = false;
                    dgPaquetes.Visible = false;
                    txtRemision.Text = "";
                }
            }
            Cursor.Current = Cursors.Default;
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

        private void menuItem2_Click(object sender, EventArgs e)
        {
            //if (c.insertaRemision(remision))
            Cursor.Current = Cursors.WaitCursor;
            if (!c.validaRemisionExiste(remision))
            {
                menuItem1.Enabled = false;
                menuItem2.Enabled = false;

                panelStock.Enabled = true;
                panelStock.Visible = true;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("LA REMISION " + remision + " YA FUE EMBARCADA", "ALERTA");
                txtRemision.Text = "";
                txtRemision.Focus();

            }
            Cursor.Current = Cursors.Default;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRemision_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgPaquetes_Click(object sender, EventArgs e)
        {

            //    string codigoProd = "";
            //    int indiceFila = dgPaquetes.CurrentRowIndex;
            //    string Remision= "";
            //    string PzasRemision = "";

            //    indiceFila = dgPaquetes.CurrentRowIndex;
            //    Remision = dgPaquetes[indiceFila, 0].ToString();
            //    PzasRemision = dgPaquetes[indiceFila, 6].ToString();
            //    codigoProd = dgPaquetes[indiceFila, 2].ToString();

            //    Almacen.Nueva_Escuadra ne = new SmartDeviceProject1.Almacen.Nueva_Escuadra(Remision, PzasRemision, codigoProd);//AQUI PASAR PzasRemision Y codigoProd
            //    ne.Show();
            //    //Remisiones descontarProdRemision = new Remisiones(codigoProd, cantPiezasRemision);
            //    //descontarProdRemision.Show();
            //    //se debe de retornar la lista de objetos para que se descuenten
            //    //listaTags.AddRange(descontarProdRemision.listaTags);
            //    //listaTags.AddRange(NvaEscRemi.listaTags);
            //    //ne.Dispose();
            //    //descontarProdRemision.Dispose();

            //    DataTable prodsRemision = new DataTable();
            //    int indiceFilaDT;

            //    prodsRemision = (DataTable)dgPaquetes.DataSource;

            //    if (prodsRemision.Rows.Count > 0)
            //    {

            //        foreach (DataRow registro in prodsRemision.Rows)
            //        {
            //            indiceFilaDT = prodsRemision.Rows.IndexOf(registro);
            //            if (indiceFilaDT == indiceFila)
            //            {
            //                prodsRemision.Rows.Remove(registro);
            //                break;
            //            }
            //        }

            //        dgPaquetes.DataSource = prodsRemision;

            //    }
            //    else
            //        MessageBox.Show("Se ha asignado todo el producto a remisionar");
        }

        private void dgPaquetes_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void cbOrdenProd_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cbOrdenProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            remision = cbOrdenProd.SelectedValue.ToString();
            dgPaquetes.Enabled = true;
            dgPaquetes.Visible = true;
            menuItem2.Enabled = true;

            fillDataGrid(remision);
            if (dtVacio == true)
            {
                menuItem2.Enabled = true;
                dgPaquetes.Visible = true;
            }
            else
            {
                DialogResult usuElige = MessageBox.Show("La Remision " + remision + " NO Existe", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                dgPaquetes.Enabled = false;
                dgPaquetes.Visible = false;
                txtRemision.Text = "";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnToStock_Click(object sender, EventArgs e)
        {

        }

        private void btnToStock_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string tag = "";
            tag = txtCantToStock.Text.ToString();

            try
            {
                if (!(string.IsNullOrEmpty(tag)))
                {
                    escVirtual = c.validaEscVirtual(Convert.ToInt32(tag));
                    if (escVirtual == true)
                    {
                        DialogResult usuElige = MessageBox.Show("SELECCIONASTE EL TAG: '" + tag + "' ¿DESEAS CONTINUAR? ", "ALERTA", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        Cursor.Current = Cursors.WaitCursor;

                        if (usuElige == DialogResult.Yes)
                        {

                            dt = c.showPaquetesRemision(remision, sucursal);
                            if (c.insertaDataTable(dt, "detRemision"))
                            {
                                //dtRemi = c.remiforescvirtual(remision);
                                pedido = c.getPedidoRemi(remision);
                                updtDets = c.updateDetEscuadras(Convert.ToInt32(tag), remision, pedido);
                                if (updtDets == true)
                                {

                                    updateDetremision = c.updateDetremi(remision);//actualiza detremision

                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("SE VALIDO CORRECTAMENTE LA REMISIÓN", "EXITO");
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("SE VALIDO INCORRECTAMENTE LA REMISIÓN", "ERROR");
                                    this.Close();
                                }

                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("LA REMISIÓN SE VALIDO DE FORMA INCORRECTA", "ERROR");
                            }
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            txtCantToStock.Text = "";
                        }
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("LA ESCUADRA "+tag+" NO ESTA DISPONIBLE VERIFICA LA INFORMACION", "ERROR");
                        txtCantToStock.Text = "";
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("INGRESA EL NUMERO DE TAG POR FAVOR", "ERROR");
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("ERROR AL VALIDAR LA REMISION, INTENTA DE NUEVO POR FAVOR", "ERROR");
                this.Close();
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            panelStock.Enabled = false;
            panelStock.Visible = false;
        }

        private void txtCantToStock_TextChanged(object sender, EventArgs e)
        {
            if (isDigit(txtCantToStock.Text))
            {
            }
            else
            {
                MessageBox.Show("SOLO VALORES NUMERICOS", "ERROR");
                txtCantToStock.Text = "";
                txtCantToStock.Focus();
            }
        }

        //JUAN LUIS MORGADO QUIJANO ULTIMOS CAMBIOS 16 ENE 2020

    }
}