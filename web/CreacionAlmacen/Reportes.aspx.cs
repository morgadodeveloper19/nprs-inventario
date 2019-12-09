using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JQuery
{
    public partial class Reportes : System.Web.UI.Page
    {
        //JQuery.wsGM.Service1 ws = new JQuery.wsGM.Service1();
        //JQuery.wsNapresa.Service1 ws = new JQuery.wsNapresa.Service1();
        JQuery.NapresaLocalhost.Service1 ws = new JQuery.NapresaLocalhost.Service1();

        protected void Page_Load(object sender, EventArgs e)
        {
         
        }      
    }
}