using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CreacionAlamacen;

namespace JQuery
{
    public partial class login : System.Web.UI.Page
    {
        Conex con = new Conex();
        public string ds;

        protected void Page_Load(object sender, EventArgs e)
        {            
            Session.Clear();
            ds = con.cadenaCon;
            Username.Focus();
        }

        protected void iniciar_sesion(object sender, EventArgs e)
        {
            try
            {
                if (Username.Text.Length <= 0) 
                {
                    System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                    l.Text = "<div class='alert' style='margin-bottom: 5px;'>" +
                        "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                        "<strong>Advertencia!!</strong>" + " Favor de introducir su usuario" + ".</div>";
                    logerror.Controls.Add(l);                    
                }
                else
                {
                    if (Password.Text.Length <= 0)
                    {
                        System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                        l.Text = "<div class='alert' style='margin-bottom: 5px;'>" +
                            "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                            "<strong>Advertencia!!</strong>" + "  Favor de introducir el password " + ".</div>";
                        logerror.Controls.Add(l);
                    }
                    else 
                    {
                        string Usr = "", Pwd = "";
                        Usr = Username.Text;
                        Pwd = Password.Text;
                        string[] datos = con.getUsuario(Usr,Pwd);
                        Session["Name"] = datos[0].ToString();
                        Session["FirstName"] = datos[0].ToString();
                        Session["LastName"] = datos[0].ToString();
                        Response.Redirect("Zona.aspx");
                    }
                    
                }
            }catch(Exception ex)
            {
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert alert-error' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Advertencia!!</strong>" + " Usuario Incorrecto, Favor de Intentar con otro" + ".</div>";
                logerror.Controls.Add(l);
            };

        }
    }
}