using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace JQuery
{
    public partial class IniciarProduccion : System.Web.UI.Page
    {
        //JQuery.wsGM.Service1 ws = new JQuery.wsGM.Service1();
        //JQuery.NapresaLocalhost.Service1 ws = new NapresaLocalhost.Service1();
        JQuery.wsNapresa.Service1 ws = new JQuery.wsNapresa.Service1();
        int tipoR = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }        

        protected void btnTablas_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnZonas_Click(object sender, EventArgs e)
        {

            
            DataView dv = (DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);
            //DataView dv2 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            DataView dv2 = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            //Random r = new Random();
            //int calculo = r.Next(1, 10);
            //int size = calculo-1;
            //lblCalculo.Text = calculo + "";
            if(checarRacks() != 1){
                lblErrores.Text = "Favor de seleccionar solo un tipo de Rack";
            }else{

            try
            {
                decimal calculo = 0;

                if (dv2.Table.Rows.Count > 0)
                {
                    string cod = (string)dv.Table.Rows[0][0];
                    string desc = (string)dv.Table.Rows[0][1];
                    double cant = (double)dv.Table.Rows[0][2];
                    string ruta = (string)dv.Table.Rows[0][3];
                    string cent = (string)dv.Table.Rows[0][4];
                    lblCodigo.Text = cod;
                    lblDescricpcion.Text = desc;
                    lblCantidad.Text = cant + "";
                    lblRuta.Text = ruta;
                    lblCentro.Text = cent;

                    lblCodigo.Visible = true;
                    lblDescricpcion.Visible = true;
                    lblCantidad.Visible = true;
                    lblRuta.Visible = true;
                    lblCentro.Visible = true;
                    calculo = ws.calculaRacks(lblCodigo.Text, tipoR, int.Parse(lblCantidad.Text));
                    //int calculo = 9;
                    decimal size = calculo - 1;
                    lblCalculo.Text = calculo + "";
                }
                else
                {
                    lblErrores.Text = "La orden introducida no es valida/no existe";
                    lblErrores.Visible = true;
                }

                if (dv2.Table.Rows.Count >= calculo)
                {
                    lblCalculo.Visible = true;
                    lblErrores.Visible = false;
                    lblYolo.Visible = true;
                    lblYolo2.Visible = true;
                    GridView1.Visible = true;
                }
                else
                {
                    lblErrores.Text = "No hay suficientes racks disponibles para surtir esta orden";
                    lblErrores.Visible = true;
                }
            }
            catch (NullReferenceException er)
            {
                lblErrores.Text = "Favor de introducir algun valor";
                lblErrores.Visible = true;
            }
            }

        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Default.aspx");
            String weon = updateRacks(txtOP.Text);
            int calculo = int.Parse(lblCalculo.Text);
            int seleccion = seleccionRacks();
            int diferencia;
            if (seleccion > calculo)
            {
                diferencia = seleccion - calculo;
                lblErrores.Text = "Has seleccionado " + diferencia + " racks de más. Favor de corregir tu seleccion";
                lblErrores.Visible = true;
            }
            else if (seleccion < calculo)
            {
                diferencia = calculo - seleccion;
                lblErrores.Text = "Te ha faltado seleccionar " + diferencia + " racks. Favor de corregir tu seleccion";
                lblErrores.Visible = true;
            }else
            {
                lblErrores.Visible = false;
                realUpdate(weon);
                Response.Redirect(Request.RawUrl);
            }
        }

        public int checarRacks()
        {
            int count=0;
            if (CheckBox1.Checked)
            {
                count++;
                tipoR = 1;
            }
            if (CheckBox2.Checked)
            {
                count++;
                tipoR = 2;
            }
            if (CheckBox3.Checked)
            {
                count++;
                tipoR = 3;
            }
            if (CheckBox4.Checked)
            {
                count++;
                tipoR = 4;
            }
            return count;
        }

        public int seleccionRacks()
        {
            int seleccion = 0;
            CheckBox ch;
            foreach (GridViewRow row in GridView1.Rows){
                ch = row.FindControl("CheckBox1") as CheckBox;
                if(ch.Checked == true)
                    seleccion = seleccion + 1;
            }
            return seleccion;
        }

        public void realUpdate(string wea)
        {

            //SqlConnection conn = new SqlConnection("Data Source=192.168.0.229;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=NapresaPwd20");
            //SqlConnection conn = new SqlConnection("Data Source=172.16.1.31;Initial Catalog=DesarrolloNapresa;Persist Security Info=True;User ID=sa;Password=Adminpwd20");
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["napresaReal"].ConnectionString);
            string update = wea;
            checarRacks();
            string update2 = "UPDATE DetRProd set CantidadEstimada = "+ws.cantidad(tipoR,lblCodigo.Text)+", CodigoProducto= '"+lblCodigo.Text+"' , Contado = 0 where OrdenProduccion='"+txtOP.Text+"'";
            SqlCommand cmd = new SqlCommand(update, conn);
            SqlCommand cmd2 = new SqlCommand(update2, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException se)
            {
                string sm = se.Message;
            }
        }

        public String[] arrayEPC()
        {
            CheckBox ch;
            Label lblEPC;
            int size = seleccionRacks();
            String[] value = new String[size];
            int fila = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                ch = row.FindControl("CheckBox1") as CheckBox;
                lblEPC = row.FindControl("EPC") as Label;
                if (ch.Checked == true)
                {
                    value[fila] = lblEPC.Text;
                    fila = fila + 1;
                }
            }
            return value;
        }

        public String updateRacks (string orden){
            CheckBox ch;
            Label lblEPC;
            String value = "update DetRProd set OrdenProduccion = '"+orden+"' , Estado=1 where ";
            int size = seleccionRacks();
            //int fila = 0;
            foreach (GridViewRow row in GridView1.Rows){
                ch = row.FindControl("CheckBox1") as CheckBox;
                lblEPC = row.FindControl("EPC") as Label;
                if (size == 1)
                {
                        value += " EPC='" + lblEPC.Text + "'";
                    return value;
                }else
                {
                    break;
                }
            }
            if (size > 1)
            {
                String[] EPC = arrayEPC();
                for (int x = 0; x < EPC.Length; x++)
                {
                    if (x == 0)
                    {
                        value += "EPC in('" + EPC[x] + "'";
                    }else if (x == EPC.Length - 1)
                    {
                        value += ",'" + EPC[x] + "')";
                    }
                    else
                    {
                        value += ",'" + EPC[x] + "'";
                    }
            
            }
            }
            return value;
        }
    }
}