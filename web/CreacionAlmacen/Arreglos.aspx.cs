using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreacionAlamacen;

namespace JQuery
{
    public partial class Arreglos : System.Web.UI.Page
    {
        Conex hola = new Conex();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["FirstName"] == null || Session["LastName"] == null || Session["Name"] == null)
            //{
            //    Response.Redirect("login.aspx");
            //}    
            DSRacks2.ConnectionString = hola.cadenaCon;
            DSZonas2.ConnectionString = hola.cadenaCon;
            DSNiveles2.ConnectionString = hola.cadenaCon;
            DSVentanas2.ConnectionString = hola.cadenaCon;
            DSPosiciones.ConnectionString = hola.cadenaCon;
        }

        protected void ddlZonas2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IdZona = ddlZonas2.SelectedValue.ToString();
            int rackEx = hola.getInt("select count(*) from Racks where IDZona =" + IdZona);
            if (rackEx > 0)
            {
                ddlRacks2.Enabled = true;
                ddlRacks2.DataBind();
                ddlNiveles2.Enabled = true;
                ddlVentanas2.Enabled = true;
                ddlNiveles2.DataBind();
                ddlVentanas2.DataBind();                
            }
            else
            {
                ddlRacks2.Enabled = false;
                ddlNiveles2.DataBind();
                ddlNiveles2.Enabled = false;
                ddlVentanas2.DataBind();
                ddlVentanas2.Enabled = false;
            }

        }

        protected void ddlRacks2_SelectedIndexChanged(object sender, EventArgs e)
        {            
            string Idrack = ddlRacks2.SelectedValue.ToString();
            int nivEx = hola.getInt("SELECT count(*) from niveles where IDRack = " + Idrack);
            if (nivEx > 0)
            {
                ddlNiveles2.Enabled = true;
                ddlNiveles2.DataBind();
                ddlVentanas2.DataBind();
            }
            else
            {
                ddlNiveles2.Enabled = false;                
                ddlVentanas2.Enabled = false;
            }
        }

        protected void ddlNiveles2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IdVent = ddlRacks2.SelectedValue.ToString();
            int ventEx = hola.getInt("SELECT count(*) from ventanas where IDNivel = " + IdVent);
            if (ventEx > 0)
            {
                ddlNiveles2.Enabled = true;          
                ddlVentanas2.DataBind();
            }
            else
            {
                ddlNiveles2.Enabled = false;
                ddlVentanas2.Enabled = false;
            }
        }

        protected void ddlVentanas2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IdVent = ddlRacks2.SelectedValue.ToString();
            int posEx = hola.getInt("SELECT count(*) FROM posiciones where IDVentana = " + IdVent);
            if (posEx > 0)
            {
                ddlPosiciones.Enabled = true;
                ddlPosiciones.DataBind();                
            }
            else
            {
                ddlPosiciones.Enabled = false;                
            }

        }

        protected void ddlPosiciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
            l.Text = "<div class='alert' style='margin-bottom: 5px;'>" +
                "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                "<strong>Advertencia!!</strong>   </div>";
            logerror.Controls.Add(l);
        }

        protected void DSRacks2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }      
             
    }
}