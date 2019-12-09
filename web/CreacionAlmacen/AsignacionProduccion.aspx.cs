using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace JQuery
{
    public partial class AsignacionProduccion : System.Web.UI.Page
    {

        //JQuery.wsGM.Service1 ws = new JQuery.wsGM.Service1();
        //JQuery.wsNapresa.Service1 ws = new JQuery.wsNapresa.Service1();
        JQuery.NapresaLocalhost.Service1 ws = new JQuery.NapresaLocalhost.Service1();
        
        
        int tipoR = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            int a = ws.fillProd();
            int aa = a;
        }



        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DataView dv = (DataView)datasourceRenglones.Select(DataSourceSelectArguments.Empty);
            try
            {
                if (dv.Table.Rows.Count > 0)
                {
                    btnBuscar.Text = "Verificar Orden";
                    gridOP.Visible = true;
                    btnSeleccionar.Visible = true;
                    btnSeleccionar.Text = "Seleccionar Orden";
                }
                else
                {
                    btnBuscar.Width = 800;
                    btnBuscar.Text = "La orden introducida no es valida. Por favor intentelo nuevamente";
                }
            }
            catch (Exception err)
            {
            }
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (gridOP.SelectedIndex >= 0)
            {
                GridViewRow row = gridOP.SelectedRow;
                string ordenProduccion = row.Cells[2].Text;
                string codigo = row.Cells[4].Text;
                //string renglon = row.Cells[7].Text;
                string renglon = gridOP.DataKeys[gridOP.SelectedIndex].Value.ToString();
                lblRenglon.Text = renglon;
                tempCod.Text = codigo;
                fillLabels();
                btnSeleccionar.Text = "Seleccionar Orden";
            }
            else
            {
                btnSeleccionar.Width = 400;
                btnSeleccionar.Text = "Por favor seleccione una orden";
            }
        }

        public int checarRacks()
        {
            int count = 0;
            if (CheckBox5.Checked)
            {
                count++;
                tipoR = 1;
            }
            if (CheckBox6.Checked)
            {
                count++;
                tipoR = 2;
            }
            if (CheckBox7.Checked)
            {
                count++;
                tipoR = 3;
            }
            if (CheckBox8.Checked)
            {
                count++;
                tipoR = 4;
            }
            return count;
        }


        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            btnCalcular.Text = "Calculando...";
            try
            {
                Decimal calculo = 0;
                if (checarRacks() == 0)
                {
                    btnCalcular.Width = 600;
                    btnCalcular.Text = "Por favor seleccione un tipo de Rack";
                }
                else if (checarRacks() > 1)
                {
                    btnCalcular.Width = 600;
                    btnCalcular.Text = "Por favor seleccione SOLO UN TIPO DE RACK";
                }
                else
                {
                    if (int.Parse(txtCantidad.Text) <= int.Parse(lblCantidad.Text))
                    {
                        lblTR.Text = tipoR + "";
                        calculo = ws.calculaRacks(lblCodigo.Text, tipoR, int.Parse(txtCantidad.Text));
                        Decimal size = calculo - 1;
                        lblCalculo.Text = calculo + "";
                        DataView dv = (DataView)dataSourceRacks.Select(DataSourceSelectArguments.Empty);
                        if (dv.Table.Rows.Count >= calculo)
                        {
                            lblParte1.Visible = true;
                            lblCalculo.Visible = true;
                            lblParte2.Visible = true;
                            gridRacks.Visible = true;
                            btnFinalizar.Visible = true;
                            gridRacks.PageSize = int.Parse(calculo.ToString());
                            btnCalcular.Text = "Calcular Racks";
                        }
                        else
                        {
                            btnCalcular.Width = 800;
                            btnCalcular.Text = "No hay suficientes Racks Disponibles";
                        }
                    }
                    else
                    {
                        btnCalcular.Width = 900;
                        btnCalcular.Text = "No pueden asignarse cantidades mayores al total del pedido";
                    }
                    }
                }
            
            catch (Exception ee)
            {
                btnCalcular.Text = "Por favor reintentelo más tarde";
            }
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                string update = updateRacks(txtOP.Text);
                int calculo = int.Parse(lblCalculo.Text);
                int seleccion = seleccionRacks();
                int diferencia;
                if (seleccion > calculo)
                {
                    diferencia = seleccion - calculo;
                    btnFinalizar.Text = "Has seleccionado " + diferencia + " racks de más.\n Favor de corregir tu seleccion";
                    btnFinalizar.Height = 75;
                }
                else if (seleccion < calculo)
                {
                    diferencia = calculo - seleccion;
                    btnFinalizar.Text = "Te ha faltado seleccionar " + diferencia + " racks.\n Favor de corregir tu seleccion";
                    btnFinalizar.Height = 75;
                }
                else
                {
                    if (calculo > 0)
                    {
                        if (RealUpdate(update))
                        {
                            System.Windows.Forms.MessageBox.Show("Asignación Completada");
                            Response.Redirect(Request.RawUrl);
                        }
                        else
                        {
                            //System.Windows.Forms.MessageBox.Show("Asignación Fallida.\nPor favor revise la información e intentelo nuevamente.");
                        }
                    }else if (calculo == 0)
                    {
                        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["napresaReal"].ConnectionString);//"Data Source=192.168.21.223;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=Adminpwd20;MultipleActiveResultSets=True"); //"Data Source=192.168.0.229;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=NapresaPwd20;MultipleActiveResultSets=True");
                        conn.Open();
                        string updateCatProd = "update catProd set Asignado = 1, prodAsignado = " + txtCantidad.Text + " where OrdenProduccion ='" + txtOP.Text + "' and Codigo ='" + lblCodigo.Text + "'";
                        SqlCommand cmdUCP = new SqlCommand(updateCatProd, conn);
                        cmdUCP.ExecuteNonQuery();
                        conn.Close();
                        System.Windows.Forms.MessageBox.Show("Asignación Completada");
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show("Asignación Fallida.\nPor favor revise la información e intentelo nuevamente.");
            }
        }

        public int seleccionRacks()
        {
            int seleccion = 0;
            CheckBox ch;
            foreach (GridViewRow row in gridRacks.Rows)
            {
                ch = row.FindControl("CheckBox1") as CheckBox;
                if (ch.Checked == true)
                    seleccion = seleccion + 1;
            }
            return seleccion;
        }

        public bool RealUpdate(string query)
        {
            bool result = false;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["napresaReal"].ConnectionString);//"Data Source=192.168.21.223;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=Adminpwd20;MultipleActiveResultSets=True"); //"Data Source=192.168.0.229;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=NapresaPwd20;MultipleActiveResultSets=True");
            //SqlConnection conn = new SqlConnection("Data Source=172.16.1.31;Initial Catalog=napresaws;Persist Security Info=True;User ID=sa;Password=Adminpwd20;MultipleActiveResultSets=True");
            string update = query;
            checarRacks();
            try
            {
            Decimal estimada = ws.cantidad(tipoR, lblCodigo.Text);
            Decimal total = Decimal.Parse(txtCantidad.Text);
            int calculo = int.Parse(lblCalculo.Text);
            if (calculo >= 1)
            {
                string[] arreglo = arrayEPC();
                for (int posActual = 0; posActual < arreglo.Length; posActual++)
                {
                    if (estimada <= total)
                    {   
                        conn.Open();
                        string update2 = "UPDATE DetRProd set CantidadEstimada =" + estimada + ", CodigoProducto = '" + lblCodigo.Text + "', Renglon = " + lblRenglon.Text + " where EPC ='" + arreglo[posActual] + "'";
                        SqlCommand cmd2 = new SqlCommand(update2, conn);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                        total = total - estimada;
                        result = true;
                    }
                    else if (total < estimada)
                    {
                        conn.Open();
                        string update2 = "UPDATE DetRProd set CantidadEstimada =" + total + ", CodigoProducto = '" + lblCodigo.Text + "', Renglon = " + lblRenglon.Text + " where EPC ='" + arreglo[posActual] + "'";
                        SqlCommand cmd2 = new SqlCommand(update2, conn);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                        total = 0;
                        result = true;
                    }
                }
            }
            else
            {
            }
            conn.Open();
                SqlCommand cmd = new SqlCommand(update, conn);
                string updateCatProd = "update catProd set Asignado = 1, prodAsignado = " + txtCantidad.Text + " where OrdenProduccion ='" + txtOP.Text + "' and Codigo ='" + lblCodigo.Text + "'";
                SqlCommand cmdUCP = new SqlCommand(updateCatProd, conn);
                cmd.ExecuteNonQuery();
                cmdUCP.ExecuteNonQuery();
                conn.Close();
                result = true;
            }
            catch (Exception e)
            {
                conn.Close();
                result = false;
            }
            return result;
        }

        public String[] arrayEPC()
        {
            CheckBox ch;
            Label lblEPC;
            int size = seleccionRacks();
            String[] value = new String[size];
            int fila = 0;
            foreach (GridViewRow row in gridRacks.Rows)
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

        public String updateRacks(string orden)
        {
            CheckBox ch;
            Label lblEPC;
            String value = "UPDATE DetRProd set OrdenProduccion = '" + orden + "', Estado = 1 where ";
            int size = seleccionRacks();
            foreach (GridViewRow row in gridRacks.Rows)
            {
                ch = row.FindControl("Checkbox1") as CheckBox;
                lblEPC = row.FindControl("EPC") as Label;
                if (size == 1)
                {
                    value += " EPC='" + lblEPC.Text + "'";
                    return value;
                }
                else
                {
                    break;
                }
            }
            if(size > 1)
            {
                String[] EPC = arrayEPC();
                for(int x=0;x<EPC.Length;x++)
                {
                    if(x==0)
                    {
                        value += "EPC in ('" + EPC[x]+"'";
                    }else if(x == EPC.Length-1)
                    {
                        value += ",'"+EPC[x]+"')";
                    }else
                    {
                        value += ",'"+EPC[x]+"'";
                    }

                }
            }
            return value;
            }
        
        private void fillLabels()
        {
            DataView dv = (DataView)dataSourceLabels.Select(DataSourceSelectArguments.Empty);
            if (dv.Table.Rows.Count > 0)
            {
                string cod = (string)dv.Table.Rows[0][0];
                string desc = (string)dv.Table.Rows[0][1];
                double cant = (double)dv.Table.Rows[0][2];
                string ruta = (string)dv.Table.Rows[0][3];
                string cent = (string)dv.Table.Rows[0][4];

                lblCodigo.Text = cod;
                lblDescripcion.Text = desc;
                txtCantidad.Text = cant + "";
                lblCantidad.Text = cant + "";
                lblRuta.Text = ruta;
                lblCentro.Text = cent;

                tblLabels.Visible = true;
                tblRacks.Visible = true;
            }
            else
            {
            }
        }

        protected void rbComp_CheckedChanged(object sender, EventArgs e)
        {
            txtCantidad.Text = lblCantidad.Text;
            txtCantidad.Enabled = false;
        }

        protected void rbParc_CheckedChanged(object sender, EventArgs e)
        {
            txtCantidad.Enabled = true;
        }

        protected void gridRacks_PageIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}