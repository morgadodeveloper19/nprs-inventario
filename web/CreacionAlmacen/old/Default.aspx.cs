using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace CreacionAlamacen
{
    public partial class _Default : System.Web.UI.Page
    {
        Conex con = new Conex();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FirstName"] == null || Session["LastName"] == null || Session["Name"] == null)
            {
                Response.Redirect("login.aspx");
            }
            int hayZonas = con.getInt("select count(*) from Zonas");
            if (hayZonas == 0)
            {
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Advertencia!!</strong>" + "  Debe de generar zonas y Racks, antes de este paso, pulse <strong>Zonas</strong> en el menu para continuar " + ".</div>";
                logerror.Controls.Add(l);
            }         
            SQLDS_Ricsa.ConnectionString = con.cadenaCon;
        }

        protected void btnGenerar0_Click(object sender, EventArgs e)
        {
            if (txtNiveles.Text.Length == 0 || txtVentanas.Text.Length == 0 || txtRacks.Text.Length == 0)
            {
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Advertencia!!</strong>" + " Favor de llenar los 3 campos necesarios" + ".</div>";
                logerror.Controls.Add(l);
            }
            else
            {
                dynamic row = null;
                dynamic numrows = null;
                dynamic numcells = null;
                dynamic j = null;
                dynamic i = null;
                row = 0;
                numrows = Convert.ToInt32(txtNiveles.Text);
                numcells = Convert.ToInt32(txtVentanas.Text);
                for (j = numrows; j > 0; j--)
                {
                    HtmlTableRow r = new HtmlTableRow();
                    row = row + 1;
                    for (i = 1; i <= numcells; i++)
                    {
                        HtmlTableCell c = new HtmlTableCell();
                        c.Controls.Add(new LiteralControl("N" + j + "," + "V" + i));
                        r.Cells.Add(c);
                    }
                    t1.Rows.Add(r);
                    t1.Visible = true;
                    lblCosas.Visible = true;
                    lblCosas.Text = txtRacks.Text + "    X";
                    lblZona.Visible = true;
                    lblZona.Text = "Z" + ddlZonas.SelectedItem.ToString();
                    lblMessage.Visible = true;
                }
            }   
        }

        public int checkRacks(string IdZona) 
        {
            int hayZonas = con.getInt("select count(*) from Racks where IdZona = "+IdZona);
            return hayZonas;
        }

        public void crearAlmacen(int racks, int niveles, int ventanas) 
        {
            t1.Visible = false;
            lblCosas.Text = "";
            lblZona.Text = "";
            lblMessage.Visible = false;
            try
            {
                int IDRack = 0;
                int IDNivel = 0;
                int IDVentana = 0;
               // int countRacks = checkRacks(ddlZonas.SelectedValue.ToString());                
                for (int x = 1; x <= racks; x++)
                {                    
                    con.insert("INSERT INTO racks VALUES('" + dosDig(x) + "', "+ddlZonas.SelectedValue.ToString()+")");
                    IDRack = con.getInt("SELECT SCOPE_IDENTITY()");
                    for (int y = 1; y <= niveles; y++)
                    {
                        con.insert("INSERT INTO niveles VALUES('" + y + "', " + IDRack + ")");
                        IDNivel = con.getInt("SELECT SCOPE_IDENTITY()");
                        for (int z = 1; z <= ventanas; z++)
                        {
                            con.insert("INSERT INTO ventanas VALUES('" + dosDig(z) + "', " + IDNivel + ", 1)");
                            IDVentana = con.getInt("SELECT SCOPE_IDENTITY()");
                            con.insert("INSERT INTO posiciones VALUES('1',"+IDVentana+", 1, 1, 25.0, 1.5,1)");
                        }
                    }
                }
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Correcto!! </strong>El almacén ha sido creado satisfactoriamente.</div>";
                logsuccess.Controls.Add(l);
            }
            catch (SqlException ex)
            {
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Advertencia!!</strong>" + " Ha ocurrido un error al generar, favor de probar mas tarde" + ".</div>";
                logsuccess.Controls.Add(l);                
            };
        }

        public string dosDig(int cosa) 
        {
            string res="";
            if (cosa.ToString().Length == 1) 
            {
                res = "0" + cosa;
            }
            return res;
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            if (txtNiveles.Text.Length == 0 || txtVentanas.Text.Length == 0 || txtRacks.Text.Length == 0)
            {
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Advertencia!!</strong>" + " Favor de llenar los 3 campos necesarios" + ".</div>";
                logerror.Controls.Add(l);
            }
            else
            {                
                crearAlmacen(Convert.ToInt32(txtRacks.Text), Convert.ToInt32(txtNiveles.Text), Convert.ToInt32(txtVentanas.Text));
            }
        }
    }
}
