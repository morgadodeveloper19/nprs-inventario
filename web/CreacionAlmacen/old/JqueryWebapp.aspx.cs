using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;

namespace JQuery
{
    public partial class JqueryWebapp : System.Web.UI.Page
    {
        public static System.Web.UI.WebControls.CheckBox[] cbPos;
        public static ArrayList valores = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string request =String.Format("{0}", Request.Form["ventanas"]); post
            //string request = Request.QueryString["ventanas"]; // get
            //MessageBox.Show("click ->" + request);
        }

       
        protected void Button1_Click1(object sender, EventArgs e)
        {
            //string request = Request.Form["ventanas"];
            string request = Request.QueryString["ventanas"];
            System.Web.UI.WebControls.Button b = (System.Web.UI.WebControls.Button)sender;
            

            string checks = "";
            for(int i= 0;i<cbPos.Length;i++){
                request = Request.QueryString[cbPos[i].ID];
                if (request == "on")
                    checks += cbPos[i].ID + ", ";
            }
            MessageBox.Show(checks);

            //MessageBox.Show("click ->" + request + " checkbox!!-> " + CheckBox1.Checked);

            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (txt1.Text.Length == 0 || txt2.Text.Length == 0)
            {
                MessageBox.Show("Llena los campos animal!!");
            }
            else
            {
                dynamic row = null;
                dynamic numrows = null;
                dynamic numcells = null;
                dynamic j = null;
                dynamic i = null;
                row = 0;
                numrows = Convert.ToInt32(txt1.Text);
                numcells = Convert.ToInt32(txt2.Text);
                int count = 0;
                cbPos = new System.Web.UI.WebControls.CheckBox[numrows * numcells];
                for (j = numrows; j > 0; j--)
                {
                    HtmlTableRow r = new HtmlTableRow();
                    row = row + 1;

                    for (i = 1; i <= numcells; i++)
                    {
                        HtmlTableCell c = new HtmlTableCell();
                        cbPos[count] = new System.Web.UI.WebControls.CheckBox();
                        cbPos[count].Text = "N" + j + "," + "V" + i;
                        cbPos[count].ID = "cbN" + i + "V" + j;
                        valores.Add("cbN" + i + "V" + j);
                        cbPos[count].Font.Name = "Verdana";
                        cbPos[count].Font.Size = 9;
                        cbPos[count].Checked = false;
                        //cbPos[count].AutoPostBack = true;
                        //cbPos[count].CheckedChanged += new System.EventHandler(cbPos_CheckedChanged);
                        cbPos[count].EnableViewState = true;
                        c.Controls.Add(cbPos[count]);
                        c.Controls.Add(new LiteralControl("<br>"));
                        //c.Controls.Add(new LiteralControl("<input id='cbN" + j +"V" + i + "' runat='server' type='checkbox' text='hola' value ='5'/>" + "N" + j + "," + "V" + i + "<br>"));
                        r.Cells.Add(c);
                        count++;
                    }
                    t1.Rows.Add(r);
                    t1.Visible = true;
                }
            }
        }

        protected void on_client() {            
            string request = "";
            string checks = "";
            for (int i = 0; i < cbPos.Length; i++)
            {
                request = Request.QueryString[cbPos[i].ID];
                if (request == "on")
                    checks += cbPos[i].ID + ", ";
            }
            MessageBox.Show(checks);
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

    }
}