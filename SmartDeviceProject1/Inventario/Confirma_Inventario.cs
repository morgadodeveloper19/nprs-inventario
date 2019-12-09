using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1.Inventario
{
    public partial class Confirma_Inventario : Form
    {

        cMetodos ws = new cMetodos();        
        string[] usuario;
        string consultar;
        string ubicacion;
        

        public Confirma_Inventario(string[] usu)
        {
            usuario = usu;
            //consultar = consulta;
            //ubicacion = ubi;
            InitializeComponent();
            string query = "SELECT Descripcion AS Items, Descripcion AS ID FROM ZonaBustamante";
            llenaComboBox(cbZonas, "Items", "ID", query, cMetodos.CONEXION);
        }

         public void llenaComboBox(ComboBox Objeto, string nomCve, string idCve, string consulta, string conex)
         {
             DataTable dt = ws.getDatasetConexionWDR(consulta, conex);
             if (dt == null)
             {
                 MessageBox.Show("NO SE PUEDE CONSULTAR LAS UBICACIONES EN ESTE MOMENTO", "ERROR");
                 this.Close();
                 return;
             }

             Objeto.DataSource = null;
             Objeto.DataSource = dt;
             Objeto.DisplayMember = nomCve;
             Objeto.ValueMember = idCve;
             dt.Columns[0].MaxLength = 255;
             DataRow dr = dt.NewRow();
             string opcSelec = "SELECCIONAR UBICACIÓN";
             dr[nomCve] = (dt.Rows.Count > 0) ? opcSelec : "NO HAY UBICACIONES DISPONIBLES";
             dr[idCve] = 0;
             try
             {
                 dt.Rows.InsertAt((dr), 0);
             }
             catch (Exception e)
             {
                 dt.Columns[0].MaxLength = 255;
                 dt.Rows.InsertAt((dr), 0);
                 //throw;
             }
             Objeto.SelectedValue = 0;
         }

        private void mi_Siguiente_Click(object sender, EventArgs e)
        {
            
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                mi_Siguiente.Enabled = false;
                if (txtClave.Text.Length <= 5)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Debe de indicar una clave de 6-25 caracteres, sin espacios.");
                    mi_Siguiente.Enabled = true;
                }
                else
                {
                    if (repetido(txtClave.Text))
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Favor de cambiar la clave", "Clave repetida.");
                        mi_Siguiente.Enabled = true;
                    }
                    else
                    {



                        string clave, idInvCong;
                        clave = txtClave.Text.Trim();
                        ubicacion = cbZonas.SelectedValue.ToString();

                        if (ubicacion == "0")
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("SELECCIONA UNA UBICACIÓN POR FAVOR", "ERROR");
                            mi_Siguiente.Enabled = true;
                        }
                        else
                        {

                            string[] consulta = obtenDatosInv(ubicacion);
                            string cuantos = consulta[1];
                            int numEsc;
                            if (Convert.ToUInt32(cuantos) > 0)
                            {
                                numEsc = Convert.ToInt32(cuantos);
                                consultar = consulta[0];

                                if (ubicacionExiste(ubicacion))//SI ESTA FALSE NO HAY INFORMACION DE ESA UBICACION
                                {
                                    Cursor.Current = Cursors.WaitCursor;
                                    MessageBox.Show("ESTA UBICACIÓN YA FUE SELECCIONADA PROCEDE A LOS CONTEOS", "ERROR");
                                    Reporte_Inventario ri = new Reporte_Inventario(usuario);
                                    ri.Show();
                                    Cursor.Current = Cursors.Default;
                                }
                                else
                                {

                                    if (ws.insertaInvCong(ubicacion, usuario[4], clave, ubicacion))
                                    {
                                        if (ws.insertaDetalleCongelado(consultar))
                                        {
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("Ubicación congelada exitosamente con clave: " + clave);
                                            Cursor.Current = Cursors.WaitCursor;
                                            idInvCong = ws.getIdInvSol().ToString();
                                            Leer_Inventario li = new Leer_Inventario(idInvCong, usuario);
                                            li.Show();
                                            Cursor.Current = Cursors.Default;
                                        }
                                        else
                                        {
                                            Cursor.Current = Cursors.Default;
                                            MessageBox.Show("Ha ocurrido un problema, revise los campos y intente de nuevo.");
                                            mi_Siguiente.Enabled = true;
                                        }
                                    }
                                    else
                                    {
                                        Cursor.Current = Cursors.Default;
                                        MessageBox.Show("Ha ocurrido un problema, revise los campos y intente de nuevo.");
                                        mi_Siguiente.Enabled = true;
                                    }//despues de aqui
                                }

                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("NO CUENTAS CON MATERIAL EN LA UBICACION SELECCIONADA", "ADVERTENCIA");
                                mi_Siguiente.Enabled = true;
                            }
                        }
                    }
                        
                    }//arriba de aqui
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
                MessageBox.Show("OCURRIO UN ERROR, VERIFICA LA INFORMACION", "ERROR");
                mi_Siguiente.Enabled = true;
            }
        }


        public string[] obtenDatosInv(string ubicacion)
        {
            try
            {
                string[] arreglo = { "", "" };
                
                string consultaInvCount = "SELECT COUNT(*) FROM detEscuadras WHERE Posicion = '" + ubicacion + "'";
                string consultaInvGeneral = "SELECT EPC, CodigoProducto, Piezas, Posicion FROM detEscuadras WHERE Posicion = '" + ubicacion + "'";
                
                Cursor.Current = Cursors.WaitCursor;
                int cuantos = ws.getInt(consultaInvCount, "Solutia");
                
                arreglo[0] = consultaInvGeneral;
                arreglo[1] = cuantos.ToString();
                Cursor.Current = Cursors.Default;
                return arreglo;
            }
            catch (Exception e)
            {
                Cursor.Current = Cursors.Default;
                //MessageBox.Show(e.Message); 
                return null;
            }
        }


        public int getIdInv(string clave, string descripcion, string IdUsuario)
        {
            int insertados = 0, idInv = 0;
            try
            {
                insertados = ws.inserta("Insert INTO InventarioCongelado(Clave,Descripcion,Fecha,Estatus,IDUsuario) VALUES('" + clave + "','" + descripcion + "',getdate(),0," + IdUsuario + ")", "ConsolaAdmin");
                if (insertados > 0)
                {
                    idInv = ws.getInt("SELECT max(IDInv) from InventarioCongelado", "ConsolaAdmin");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            };
            return idInv;
        }

        private void mi_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool repetido(string nombre) 
        {
            int count = ws.getInt("select count(*) FROM InvCongelado where cveInv = '"+nombre+"'", "Solutia");                        
            if (count > 0)//Clave repetida
            {
                return true;
            }
            else //Clave no repetida
            {
                return false;
            }
        }

        private bool ubicacionExiste(string ubicacion)
        {
            int count = ws.getInt("SELECT COUNT(*) FROM InvCongelado WHERE ubicacionConteo = ' " + ubicacion + "' ", "Solutia");
            if (count > 0)//Clave repetida
            {
                return true;
            }
            else //Clave no repetida
            {
                return false;
            }
        }

        private void cbSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}