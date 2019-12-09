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
        Conex hola = new Conex();
        public string ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            ds = hola.cadenaCon;
        }

        protected void iniciar_sesion(object sender, EventArgs e)
        {
            try
            {
                if (Username.Text.Length <= 0 || Password.Text.Length <= 0) 
                {
                    lblErrors.Text = "Favor de llenar ambos campos";
                }
                else
                {
                    string Usr = "", Pwd = "";
                    Usr = Username.Text;
                    Pwd = Password.Text;

                }
            }catch(Exception ex)
            {
            };

        }
    }
}