using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SmartDeviceProject1.Inventario
{
    public partial class Continuar_Inventario : Form
    {
        cMetodos cm = new cMetodos();
        string[] user;
        string idInv;
        string select = "";
        int conteo;
        int status;

        public Continuar_Inventario(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
            fillDataGrid();
            if (dataGrid1.VisibleRowCount > 0)
            {
            }
            else
            {
                MessageBox.Show("ESTE USUARIO NO TIENE CONTEOS PENDIENTES, FAVOR DE REVISAR CON JUAN PABLO VALDEZ");
                this.Close();
            }
        }

        public void fillDataGrid()
        {
            try
            {
                DataSet dt = consulta("SELECT idInv, almacen, fecha, cveInv from InvCongelado WHERE usuario = '" + user[4] + "' AND numConteo < 4"); //CAMBIE ESTO PARA QUE LE APAREZCAN AL USUARUI TODOS LOS CONTEOS ASIGNADOS AND status = 0");
                DataGridTableStyle tableStyle = new DataGridTableStyle();

                tableStyle.MappingName = dt.Tables[0].TableName;

                GridColumnStylesCollection columnStyles = tableStyle.GridColumnStyles;

                DataGridTextBoxColumn columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "idInv";
                columnStyle.HeaderText = "ID";
                columnStyle.Width = 30;
                columnStyles.Add(columnStyle);

                columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "almacen";
                columnStyle.HeaderText = "Ubicación";
                columnStyle.Width = 50;
                columnStyles.Add(columnStyle);

                columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "fecha";
                columnStyle.HeaderText = "Fecha";
                columnStyle.Width = 100;
                columnStyles.Add(columnStyle);

                columnStyle = new DataGridTextBoxColumn();
                columnStyle.MappingName = "cveInv";
                columnStyle.HeaderText = "Clave";
                columnStyle.Width = 54;
                columnStyles.Add(columnStyle);

                GridTableStylesCollection tableStyles = dataGrid1.TableStyles;
                tableStyles.Add(tableStyle);
                dataGrid1.PreferredRowHeight = 16;
                dataGrid1.RowHeadersVisible = false;

                //------------------------------
                dataGrid1.DataSource = dt.Tables[0];
                //------------------------------
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                MessageBox.Show("Hubo un asunto con la conexion. \nFavor de Reintentar en unos momentos", "Advertencia");
            }
        }

        public DataSet consulta(string select)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = cm.getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                SqlCommand command = new SqlCommand(select, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ds;
        }


        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {
            try
            {
                bool bandera = true;
                int rowIndex = dataGrid1.CurrentCell.RowNumber;
                string value = dataGrid1[rowIndex, 0].ToString();
                idInv = value;
                int id = Convert.ToInt32(idInv);
                conteo = cm.getConteo(id);
                status = cm.getStatusConteo(id);
                if (conteo == 0 && status == 0)
                {
                    MessageBox.Show("TIENES PENDIENTE DE CONCLUIR EL PRIMER CONTEO", "AVISO");
                    bandera = true;
                }
                else if (conteo == 1 && status == 1)
                {
                    MessageBox.Show("TIENES PENDIENTE DE CONCLUIR EL SEGUNDO CONTEO", "AVISO");
                    bandera = true;
                }
                else if (conteo == 2 && status == 2)
                {
                    MessageBox.Show("TIENES PENDIENTE DE CONCLUIR EL TERCER CONTEO", "AVISO");
                    bandera = true;
                }
                else if (conteo == 3 && status == 3)
                {
                    MessageBox.Show("TIENES PENDIENTE DE CONCLUIR EL CUARTO CONTEO", "AVISO");
                    bandera = true;
                }
                else if (conteo == 4 && status == 4)
                {
                    MessageBox.Show("ESTA UBICACIÓN YA NO PUEDE TENER MAS CONTEOS","ERROR");
                    bandera = false;
                }
                if (bandera == true)
                {
                    Leer_Inventario li = new Leer_Inventario(idInv, user);
                    li.Show();
                }

                
            }
            catch (Exception exce)
            {
                string error = "";
                error = exce.Message;
                MessageBox.Show("ERROR AL CONSULTAR LA BASE DE DATOS, REINTA POR FAVOR", "AVISO");
            }
        }
    }
}