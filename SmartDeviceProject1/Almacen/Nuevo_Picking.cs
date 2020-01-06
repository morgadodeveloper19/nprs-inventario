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

        public List<EscuadraVirtual> listaTags = new List<EscuadraVirtual>();

        public Nuevo_Picking(string[] sesion)
        {
            InitializeComponent();
            user = sesion;
            sucursal = user[3];
            
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
                    DialogResult usuElige = MessageBox.Show("La Remision "+remision+" NO Existe", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    dgPaquetes.Enabled = false;
                    dgPaquetes.Visible = false;
                    txtRemision.Text = "";
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            //if (c.insertaRemision(remision))
            Cursor.Current = Cursors.WaitCursor;
            if (!c.validaRemisionExiste(remision))
            {
                dt = c.showPaquetesRemision(remision,sucursal);
                if (c.insertaDataTable(dt, "detRemision"))
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("SE VALIDO CORRECTAMENTE LA REMISIÓN", "EXITO");
                    this.Dispose();
                    Almacen.Nueva_Escuadra ne = new SmartDeviceProject1.Almacen.Nueva_Escuadra(user, remision);
                    ne.Show();

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
                MessageBox.Show("LA REMISION "+ remision +" YA FUE EMBARCADA","ALERTA");
                txtRemision.Text = "";
                txtRemision.Focus();

            }
            Cursor.Current = Cursors.Default;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
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

    }
}