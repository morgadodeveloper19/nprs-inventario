using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CreacionAlamacen;

namespace JQuery
{
    public partial class loadGrid : System.Web.UI.Page
    {
        SqlCommand com, com2;
        SqlConnection con, con2;
        Conex hola = new Conex();        
        public string ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FirstName"] == null || Session["LastName"] == null || Session["Name"] == null)
            {
                Response.Redirect("login.aspx");
            }        
            int hayZonas = hola.getInt("select count(*) from Zonas");
            if (hayZonas == 0) 
            {
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Advertencia!!</strong>" + "  Debe de generar zonas y Racks, antes de este paso, pulse <strong>Zonas</strong> en el menu para continuar " + ".</div>";
                logerror.Controls.Add(l);
            }
            ds = hola.cadenaCon;
            SqlDSRacks.ConnectionString = hola.cadenaCon;
            SqlDSZonas.ConnectionString = hola.cadenaCon;
        }

        public void insertRack()
        {
            string icom = "", f = "";
            int count = 0, ok = 0, niveles = 3, ventanas = 10, total = 0, i = 0, c = 1;
            int insertid = 0;
            total = niveles * ventanas;
            try
            {
                con = new SqlConnection(ds);
                con.Open();
                for (i = 1; i <= niveles; i++)
                {
                    icom = "insert INTO NIVEL(clave, rack) VALUES(" + i + ",'1')";
                    com = new SqlCommand(icom, con);
                    count = com.ExecuteNonQuery();
                    for (int j = 1; j <= ventanas; j++)
                    {
                        icom = "INSERT INTO VENTANAS(clave,nivel,tipo) VALUES('" + String.Format("{0:00}", j) + "'," + i + ",1)";
                        com.CommandText = icom;
                        count = com.ExecuteNonQuery();
                        // ---
                        com.CommandText = "SELECT @@IDENTITY";
                        insertid = Convert.ToInt32(com.ExecuteScalar());
                        // posiciones a 1 cuando se crea D:
                        icom = "INSERT INTO POSICIONES(clave,ventana,estatus,imo,peso,altura) VALUES(" + c + "," + insertid + ",0,0,0,0)";
                        com.CommandText = icom;
                        count = com.ExecuteNonQuery();
                        c++;
                    }
                }
            }
            catch (Exception ex)
            {
                //Response.Write("ERROR-> " +  ex.Message);
                ok = -1;
                MessageBox.Show("ERROR-> " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            insertRack();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            button2Click();
        }

        public void button2Click() 
        {
            try
            {
                string zona = ddlZona.SelectedValue.ToString();
                string rack = ddlRack.SelectedItem.ToString();
                fillTable(rack, zona);
                showlista();
                
            }
            catch (Exception ex)
            {
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Error!.</strong>" +" Favor de seleccionar una zona que tenga racks"+ ".</div>";
                logerror.Controls.Add(l);
            }
        }

        private void showlista() {
            lista.Style.Add("display", "inline");
        }


        private void fillTable(string rack, string zona)
        {
            
            HtmlTableRow r = new HtmlTableRow();
            HtmlTableCell c = new HtmlTableCell();

            string scom = "", text = "", vent = "", id = "", values = "";
            List<int> niveles = new List<int>(), ventanas = new List<int>();
            int n = 0, v = 0, pos = 0;

            try
            {
                con = new SqlConnection(ds);
                con2 = new SqlConnection(ds);
                con.Open();
                con2.Open();
                scom = "SELECT N.Clave, count(V.idVentana) AS VENTANAS FROM VENTANAS V " +
                    "inner JOIN NIVELES N ON N.idNivel = V.IDnivel " +
                    "INNER JOIN RACKS R ON R.idRack = N.IDrack " +
                    "INNER JOIN Zonas z ON z.IdZona = R.IDZona " +
                    "WHERE  R.Clave = '" + rack + "' and r.IDZona = '" + zona + "'" +
                    "GROUP by N.Clave  ORDER by N.Clave DESC";
                com = new SqlCommand(scom, con);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    n = (int.Parse(dr[0].ToString()));
                    v = (int.Parse(dr[1].ToString()));
                    r = new HtmlTableRow(); // new row
                    for (int i = 0; i < 1; i++)
                    {
                        scom = "SELECT R.idRack as idRack,R.clave AS rackClave, N.idNivel AS idNivel, V.idVentana AS idVentana,V.clave AS claveVentana, V.tipo, P.IDPosicion AS idPosicion " +
                        ",P.imo, P.peso, P.altura, P.IdTipoPos " +
                        "FROM RACKS R " +
                        "INNER JOIN NIVELES N ON N.IDrack = R.idRack " +
                        "INNER JOIN VENTANAS V ON V.IDnivel = N.idNivel " +
                        "INNER JOIN POSICIONES P on P.IDventana = V.idVentana " +
                        "INNER JOIN Zonas z on z.IdZona = R.IDZona " +
                        //"WHERE N.idNivel = " + n + " AND P.clave = 1 ORDER BY v.idVentana";
                        "WHERE N.idNivel = " + n + " AND R.IDZona = " + ddlZona.SelectedValue.ToString() + " AND R.IDRack = '" + ddlRack.SelectedItem.ToString() + "' AND P.clave = 1 ORDER BY v.idVentana";
                        com2 = new SqlCommand(scom, con2);
                        SqlDataReader dr2 = com2.ExecuteReader();
                        while (dr2.Read())
                        {                            
                            vent = String.Format("{0:00}", dr2[4]);
                            pos = int.Parse(dr2[5].ToString());
                            id = ddlZona.SelectedItem.ToString()+dr2[1].ToString() + n + vent + pos;
                            // values = imo,peso, altura, idTipoPos
                            values = ","+dr2[7].ToString() +","+dr2[8].ToString() +","+dr2[9].ToString()+"," +dr2[10];
                            c = new HtmlTableCell(); // cells                            

                            System.Web.UI.WebControls.Button b = new System.Web.UI.WebControls.Button();
                            text = "Nivel " + n + " - Ventana " + vent;

                            if (int.Parse(dr2[10].ToString()) == 3)
                            {
                                b.Attributes.Add("class", "btn btn-small btn-danger btn-block");
                            }
                            else if (pos == 1)
                                b.Attributes.Add("class", "btn btn-small btn-block");
                            else if (pos == 2)
                                b.Attributes.Add("class", "btn btn-small btn-success btn-block");
                            else if (pos == 3)
                                b.Attributes.Add("class", "btn btn-small btn-warning btn-block");
                            else if (pos == 4)
                                b.Attributes.Add("class", "btn btn-small btn-info btn-block");
                           
                            b.ID = id + values;
                            b.Text = "N" + n + " V" + vent;     
                            // -- tooltip
                            b.Attributes.Add("data-toggle", "tooltip");
                            b.Attributes.Add("title", "");
                            b.Attributes.Add("data-original-title", text);
                            b.Attributes.Add("UseSubmitBehavior", "false"); // le das en la madre al submit
                            b.Attributes.Add("onclick", "return JSFunction(this);");                            
                            c.Controls.Add(b);                                                        
                            r.Cells.Add(c);
                        }

                        dr2.Close();
                    } // ventanas por nivel
                    t1.Rows.Add(r);

                } // fin while 
                dr.Close();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                if (con2.State == ConnectionState.Open)
                    con2.Close();
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Myscript", "getMessage();");
            //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Myscript", "config_click();", true);
        }
            
        public void revisaPosiciones() 
        {
            string icom = "", f = "";
            int count = 0, ok = 0, niveles = 3, ventanas = 10, total = 0, i = 0, c = 1;
            int insertid = 0;
            total = niveles * ventanas;
            try
            {
                con = new SqlConnection(ds);
                con.Open();
                for (i = 1; i <= niveles; i++)
                {
                    icom = "insert INTO NIVEL(clave, rack) VALUES(" + i + ",'1')";
                    com = new SqlCommand(icom, con);
                    count = com.ExecuteNonQuery();
                    for (int j = 1; j <= ventanas; j++)
                    {
                        icom = "INSERT INTO VENTANAS(clave,nivel,tipo) VALUES('" + String.Format("{0:00}", j) + "'," + i + ",1)";
                        com.CommandText = icom;
                        count = com.ExecuteNonQuery();
                        // ---
                        com.CommandText = "SELECT @@IDENTITY";
                        insertid = Convert.ToInt32(com.ExecuteScalar());
                        // posiciones a 1 cuando se crea D:
                        icom = "INSERT INTO POSICIONES(clave,ventana,estatus,imo,peso,altura) VALUES(" + c + "," + insertid + ",0,0,0,0)";
                        com.CommandText = icom;
                        count = com.ExecuteNonQuery();
                        c++;
                    }
                }
            }
            catch (Exception ex)
            {
                //Response.Write("ERROR-> " +  ex.Message);
                ok = -1;
                MessageBox.Show("ERROR-> " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
                   
        }

        private string getMessage()
        {
            throw new NotImplementedException();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CALLJS", "JSFunction();", true);
        }

        private int updateRack()
        {
            return 1;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //Guardar los valores nuevos o modificados
            //Request.Form[]; // for POST
            string  imo     = Request.QueryString["imo"];
            float   peso    = float.Parse(Request.QueryString["peso"]);
            float   altura  = float.Parse(Request.QueryString["altura"]);
            string  clave   = Request.QueryString["clave"];
            string  nivel   = clave.Substring(3, 1);
            string  ventana = clave.Substring(4, 2);
            int     posiciones = int.Parse(Request.QueryString["posiciones"]);
            string  tipoPos = Request.QueryString["selectTipo"];
            setPosiciones(nivel, ventana, posiciones, imo, peso, altura, tipoPos);
            string  rack = Request.QueryString["rack"];
            //-------------------------------------------------
            string zona = ddlZona.SelectedValue.ToString();
            fillTable(rack, zona);
            //-------------------------------------------------
            System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
            l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                "<strong>Correcto!! </strong> Se ha actualizado la información del RACK. [Nivel: <strong>"+nivel+"</strong>] [Ventana: <strong>"+ventana+"</strong>]</div>";
            //logsuccess.Style.Add("display", "block");
            logsuccess.Controls.Add(l);
        }

        private void setPosiciones(string nivel, string ventana, int pos, string imo, float peso, float altura, string tipoPos) {
            string icom = "", scom = "", ucom = "", dcom = "";
            // tabla posicion
            int _id=0, _clave = 0, _ventana = 0, _estatus, row = 0, count = 1, _tipoPos=0, tipoPos2=0; 
            string _imo;
            float _peso, _altura;                      
            try
            {                
                scom = "SELECT top(1) p.idPosicion,p.clave,p.IDventana,p.estatus,p.imo,p.peso,p.altura,p.IdTipoPos FROM POSICIONES p " + 
                "inner join ventanas v on v.IDVentana =  p.IDVentana " + 
                "inner join niveles n on n.IDNivel = v.IDNivel " + 
                "where v.Clave = '"+ventana+"' AND n.clave = "+nivel+" ORDER BY clave desc";
                con = new SqlConnection(ds);
                com = new SqlCommand(scom, con);
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    _id = int.Parse(dr[0].ToString());
                    _clave = int.Parse(dr[1].ToString());
                    _ventana = int.Parse(dr[2].ToString());
                    _estatus = int.Parse(dr[3].ToString());
                    _imo = dr[4].ToString();
                    _peso = float.Parse(dr[5].ToString());
                    _altura = float.Parse(dr[6].ToString());
                    _tipoPos = int.Parse(dr[7].ToString());
                }
                dr.Close();
                switch (tipoPos) 
                {
                    case "E": tipoPos2 = 1; 
                        break;
                    case "A": tipoPos2 = 2;
                        break;
                    case "P": tipoPos2 = 3;
                        break;
                }

                if (_tipoPos != tipoPos2) 
                {
                    icom = "UPDATE POSICIONES set IdTipoPos = " + tipoPos2 + " WHERE IDPosicion = "+_id+" and IDVentana = "+_ventana;
                    com.CommandText = icom;
                    row = com.ExecuteNonQuery();                              
                }
                if (_clave < pos)
                {
                    for (int i = _clave; i < pos; i++)
                    {
                        icom = "INSERT INTO POSICIONES(clave,IDventana,estatus,imo,peso,altura,IdTipoPos) values(" + (i + 1) + "," + _ventana + ",0,'" + imo + "'," + peso + "," + altura + ", "+tipoPos2+")";
                        com.CommandText = icom;
                        row = com.ExecuteNonQuery();
                    }                    
                }
                else if (_clave > pos) {
                    dcom = "DELETE FROM POSICIONES WHERE clave > " + pos + " AND IDventana = " + _ventana;
                    com.CommandText = dcom;
                    row = com.ExecuteNonQuery();
                }
                else if (_clave == pos) { }
                // actualizar valores
                ucom = "UPDATE POSICIONES set imo = '" + imo + "' , peso = " + peso + ", altura = " + altura + " WHERE IDventana = " + _ventana;
                com.CommandText = ucom;
                row = com.ExecuteNonQuery();
                // actualizar ventana
                ucom = "UPDATE VENTANAS set tipo = " + pos + " WHERE idVentana =" + _ventana;
                com.CommandText = ucom;
                row = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert' style='margin-bottom: 5px;'>"+
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Error!.</strong>" + ex.Message + ".</div>";
                //logerror.Style.Add("display", "block");
                logerror.Controls.Add(l);
            }
            finally {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        protected void ddlZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IdZona = ddlZona.SelectedValue.ToString();
            int racksEx = hola.getInt("select count(*) from racks where IDZona =" + IdZona);
            if (racksEx > 0)
            {
                ddlRack.Enabled = true;
                ddlRack.DataBind();
            }
            else 
            {
                ddlRack.Enabled = false;
            }
        }

    }
}
