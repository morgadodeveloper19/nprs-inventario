using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Configuration;

namespace JQuery
{
    public partial class Cierre : System.Web.UI.Page
    {
        //JQuery.wsGM.Service1 ws = new JQuery.wsGM.Service1();
        //JQuery.wsNapresa.Service1 ws = new JQuery.wsNapresa.Service1();
        JQuery.NapresaLocalhost.Service1 ws = new JQuery.NapresaLocalhost.Service1();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dsDllSucursales.ConnectionString = getConexion("Intelisis");
                llenaDatasources(ddlSucursal, dsDllSucursales, "Nombre", "Sucursal", "Intelisis");
            }            
        }
               
        protected void llenaDatasources(DropDownList objeto, SqlDataSource ds, string text, string value, string conexion)
        {
            try
            {
                string connSuc = getConexion(conexion);
                objeto.DataSource = ds;
                objeto.Items.Clear();
                objeto.Items.Add("-- SELECCIONA --");
                objeto.DataTextField = text;
                objeto.DataValueField = value;
                objeto.DataBind();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            };
        }

        public string getConexion(string descripcion)
        {
            string res= null;
            string[] result = new string[5];
            try
            {
                //SqlConnection conn = new SqlConnection("Data Source=172.16.1.31;Initial Catalog=NapresaPar;Persist Security Info=True;User ID=sa;Password=Adminpwd20");
                //SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=napresaPar;Persist Security Info=True;User ID=sa;Password=NapresaPwd20");
                //SqlConnection conn = new SqlConnection("Data Source=192.168.0.229;Initial Catalog=NapresaPar;Persist Security Info=True;User ID=sa;Password=NapresaPwd20");
                //SqlConnection conn = new SqlConnection("Data Source=192.168.21.223;Initial Catalog=NapresaPar;Persist Security Info=True;User ID=sa;Password=Adminpwd20");
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["NapresaPar"].ConnectionString);
                conn.Open();
                string select = "Select * From Parametros where Descripcion = '"+descripcion+"'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {                                       
                    result[1] = reader.GetString(1);
                    result[2] = reader.GetString(2);
                    result[3] = reader.GetString(3);
                    result[4] = reader.GetString(4);
                    res = "Data Source=" + result[1] + "; Initial Catalog=" + result[4] + "; Persist Security Info=True; User ID=" + result[2] + "; Password=" + result[3] + "";
                }               
                conn.Close();
            }
            catch (Exception e)
            {
            }
            return res;

        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ddlSucursal.SelectedIndex > 0)
            {
                string valSucursal = ddlSucursal.SelectedValue.ToString();
                string command = "select Nombre, Almacen FROM Alm where Sucursal = '" + valSucursal + "'";
                lblPrueba2.Text = "IDSuc: " + valSucursal;
                dsDllAlmacen.ConnectionString = getConexion("Intelisis");
                dsDllAlmacen.SelectCommand = command;
                llenaDatasources(ddlAlmacen, dsDllAlmacen, "Nombre", "Almacen", "Intelisis");
            }
            else
            {
                string valSucursal = ddlSucursal.SelectedValue.ToString();
                lblPrueba2.Text = "IDSuc: " + valSucursal;
                System.Web.UI.WebControls.Label m = new System.Web.UI.WebControls.Label();
                m.Text = "<div class='alert alert-info' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Información!! </strong> Debe de seleccionar una sucursal.</div>";
                logerror.Controls.Add(m);
            }
  
        }

        protected void ddlAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAlmacen.SelectedIndex > 0)
            {
                string valAlmacen = "";
                valAlmacen = ddlAlmacen.SelectedValue.ToString();
                lblPrueba.Text = "IDAlm: " + valAlmacen;
                int numInvXCerrar = ws.getInt("SELECT count(*) FROM Zonas z " +
                "INNER JOIN racks r ON z.IdZona = r.IDZona " +
                "INNER JOIN niveles n ON n.IDRack = r.IDRack " +
                "INNER JOIN ventanas v ON v.IDNivel = n.IDNivel " +
                "INNER JOIN posiciones p ON p.IDVentana = v.IDVentana " +
                "INNER JOIN detalleInvCong dic ON dic.IDPosicion = p.IDPosicion " +
                "INNER JOIN InventarioCongelado inv ON inv.IDInv = dic.IDInv " +
                "WHERE inv.Estatus = 0 and z.idSucursal = '" + valAlmacen + "' ", "ConsolaAdmin");
                if (numInvXCerrar > 0)
                {
                    ddlPorCerrar.Visible = true;
                    lblinvPorCer.Visible = true;
                    string consulta = "SELECT DISTINCT inv.Clave, inv.IDInv FROM Zonas z " +
                    "INNER JOIN racks r ON z.IdZona = r.IDZona " +
                    "INNER JOIN niveles n ON n.IDRack = r.IDRack " +
                    "INNER JOIN ventanas v ON v.IDNivel = n.IDNivel " +
                    "INNER JOIN posiciones p ON p.IDVentana = v.IDVentana " +
                    "INNER JOIN detalleInvCong dic ON dic.IDPosicion = p.IDPosicion " +
                    "INNER JOIN InventarioCongelado inv ON inv.IDInv = dic.IDInv " +
                    "WHERE inv.Estatus = 0 and z.idSucursal = '" + valAlmacen + "' " +
                    "ORDER BY inv.IDInv";
                    dsDllInventarios.ConnectionString = getConexion("ConsolaAdmin");
                    dsDllInventarios.SelectCommand = consulta;
                    llenaDatasources(ddlPorCerrar,dsDllInventarios, "Clave", "IDInv","Intelisis");
                }
                else
                {
                    lblinvPorCer.Visible = false;
                    ddlPorCerrar.Visible = false;
                    System.Web.UI.WebControls.Label m = new System.Web.UI.WebControls.Label();
                    m.Text = "<div class='alert alert-info' style='margin-bottom: 5px;'>" +
                        "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                        "<strong>Información!! </strong> Este almacén no tiene Inventarios Pendientes por cerrar.</div>";
                    logerror.Controls.Add(m);
                }

            }
        }

    }
}