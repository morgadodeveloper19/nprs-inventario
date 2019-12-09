using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

namespace JQuery
{
    public partial class AsignarRacks : System.Web.UI.Page
    {
        //JQuery.wsGM.Service1 ws = new JQuery.wsGM.Service1();
        //JQuery.wsNapresa.Service1 ws = new JQuery.wsNapresa.Service1();
        JQuery.NapresaLocalhost.Service1 ws = new JQuery.NapresaLocalhost.Service1();
        string[] values;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)dataSourceOrdenes.Select(DataSourceSelectArguments.Empty);
                if (dv.Table.Rows.Count > 0)
                {
                    btnBuscar.Text = "Verificar Orden";
                    gvOP.Visible = true;
                    btnSeleccionar.Visible = true;
                    btnSeleccionar.Text = "Seleccionar Orden";
                }
                else
                {
                    btnBuscar.Width = 800;
                    btnBuscar.Text = "La orden introducida aun no se encuentra en proceso de producción.";
                }
            }
            catch (Exception err)
            {
                btnBuscar.Text = err.Message;
            }
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (gvOP.SelectedIndex >= 0)
            {
                GridViewRow row = gvOP.SelectedRow;
                int huecos = int.Parse(row.Cells[9].Text);
                if (huecos > 0)
                {
                    lblOP.Text = row.Cells[2].Text;
                    lblHuecos.Text = huecos + "";
                    lblRenglon.Text = gvOP.DataKeys[gvOP.SelectedIndex].Value.ToString();
                    lblCodigo.Text = row.Cells[4].Text;
                    int tr = ws.getTipoRackOP(txtOP.Text);
                    lblTR.Text = tr + "";
                    btnSeleccionar.Text = "Seleccionar Orden";
                    int cantidad = int.Parse(row.Cells[6].Text);
                    decimal calculo = ws.calculaRacks(row.Cells[4].Text, tr, huecos);
                    lblCalculo.Text = calculo + "";
                    fillRacksAsignados();
                    tblRacks.Visible = true;
                    tblRacks.Visible = true;
                }
                else
                {
                    btnSeleccionar.Width = 800;
                    btnSeleccionar.Text = "No puedes asignar un rack extra a una orden de producción sin huecos";
                }
            }
            else
            {
                btnSeleccionar.Width = 400;
                btnSeleccionar.Text = "Por favor seleccione una orden";
            }
        }

        public void fillRacksAsignados()
        {
            DataView dv = (DataView)datasourceRacksAsignados.Select(DataSourceSelectArguments.Empty);
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            if (gvRacksDisponibles.SelectedIndex >= 0)
            {
                //GridViewRow row = gvRacksDisponibles.SelectedRow;
                //string numero = row.Cells[1].Text;
                //bool result = false;
                //SqlConnection conn = new SqlConnection("Data Source=192.168.0.229;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=NapresaPwd20;MultipleActiveResultSets=True");
                ////SqlConnection conn = new SqlConnection("Data Source=172.16.1.31;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=Adminpwd20;MultipleActiveResultSets=True");
                //string update = "UPDATE DetRProd set Estado = 1, Verificado = 1, CodigoProducto = ";
            }
            else
            {
                btnSeleccionar.Width = 400;
                btnSeleccionar.Text = "Por favor seleccione el Rack";
            }
        }

        protected void btnNumero_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNumero.Text.Equals("*"))
                {
                    datasourceRacksDisponibles.SelectCommand = "SELECT [DetRProd].[Numero], [RProduccion].[Modelo] FROM [DetRProd] INNER JOIN [RProduccion] on [RProduccion].[IdRack] = [DetRProd].[IdRProd] WHERE ([Estado] = 0) AND ([IdRProd] =" + lblTR.Text + ")";
                    gvRacksDisponibles.DataBind();
                }
                if (txtNumero.Text.Contains(','))
                {
                    values = txtNumero.Text.Split(',');
                    string select = "SELECT [DetRProd].[Numero], [RProduccion].[Modelo] FROM [DetRProd] INNER JOIN [RProduccion] on [RProduccion].[IdRack] = [DetRProd].[IdRProd] WHERE ([Estado] = 0) AND ([IdRProd] =" + lblTR.Text + ") AND (";
                    for (int x = 0; x < values.Length; x++)
                    {
                        if (x == 0)
                        {
                            select += "[Numero] IN (" + values[x] + "";
                        }
                        else if (x == values.Length - 1)
                        {
                            select += "," + values[x] + "))";
                        }
                        else
                        {
                            select += "," + values[x] + "";
                        }
                    }
                    datasourceRacksDisponibles.SelectCommand = select;
                    gvRacksDisponibles.DataBind();
                }
                else
                {
                    int value = int.Parse(txtNumero.Text);
                    DataView dv = (DataView)datasourceRacksDisponibles.Select(DataSourceSelectArguments.Empty);
                }
                lblTexto.Visible = true;
                txtCantidad.Visible = true;
                btnCantidad.Visible = true;
            }
            catch (Exception err)
            {
            }
        }

        protected void btnCantidad_Click(object sender, EventArgs e)
        {
            int seleccion = seleccionRacks();
            int calculado = int.Parse(lblCalculo.Text);
            if (seleccion > calculado)
            {
                btnCantidad.Width = 800;
                btnCantidad.Text = "No puedes seleccionar mas racks de los necesarios para los huecos que tienes";
            }
            else
            {
                try
                {
                    values = NumerosRack();
                    if (values.Length > 1)
                    {
                        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["napresaReal"].ConnectionString);//"Data Source=192.168.0.229;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=NapresaPwd20;MultipleActiveResultSets=True");
                        //SqlConnection conn = new SqlConnection("Data Source=172.16.1.31;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=Adminpwd20;MultipleActiveResultSets=True");
                        string update = "UPDATE DetRProd set Estado = 1, Verificado = 1, CodigoProducto = '" + lblCodigo.Text + "', CantidadEstimada = " + txtCantidad.Text + ", Renglon = " + lblRenglon.Text + ", OrdenProduccion = '" + lblOP.Text + "' WHERE IdRProd = " + lblTR.Text + " and ";
                        string[] racks = NumerosRack();
                        for (int x = 0; x < values.Length; x++)
                        {
                            if (x == 0)
                            {
                                update += "[Numero] IN (" + racks[x] + "";
                            }
                            else if (x == values.Length - 1)
                            {
                                update += "," + racks[x] + ")";
                            }
                            else
                            {
                                update += "," + racks[x] + "";
                            }
                        }
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(update, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        System.Windows.Forms.MessageBox.Show("Asignación Completada");
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["napresaReal"].ConnectionString);//Data Source=192.168.0.229;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=NapresaPwd20;MultipleActiveResultSets=True");
                        //SqlConnection conn = new SqlConnection("Data Source=172.16.1.31;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=Adminpwd20;MultipleActiveResultSets=True");
                        string update = "UPDATE DetRProd set Estado = 1, Verificado = 1, CodigoProducto = '" + lblCodigo.Text + "', CantidadEstimada = " + txtCantidad.Text + ", Renglon = " + lblRenglon.Text + ", OrdenProduccion = '" + lblOP.Text + "' WHERE IdRProd = " + lblTR.Text + " and Numero = " + values[0] + "";
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(update, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        System.Windows.Forms.MessageBox.Show("Asignación Completada");
                        Response.Redirect(Request.RawUrl);
                    }
                }
                catch (Exception err)
                {
                }
            }
        }

        public String[] NumerosRack()
        {
            CheckBox ch;
            Label lblNumero;
            String[] racks = new String[seleccionRacks()];
            int fila = 0;
            foreach (GridViewRow row in gvRacksDisponibles.Rows)
            {
                ch = row.FindControl("cbSeleccion") as CheckBox;
                lblNumero = row.FindControl("Numero") as Label;
                if (ch.Checked == true)
                {
                    racks[fila] = lblNumero.Text;
                    fila = fila + 1;
                }
            }
            return racks;
        }
        public int seleccionRacks()
        {
            int seleccion = 0;
            CheckBox ch;
            foreach (GridViewRow row in gvRacksDisponibles.Rows)
            {
                ch = row.FindControl("cbSeleccion") as CheckBox;
                if (ch.Checked == true)
                    seleccion = seleccion + 1;
            }
            return seleccion;
        }
    }
}
