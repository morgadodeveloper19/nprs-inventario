using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1.Produccion
{
    public partial class Revisar_Avance : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.NapresaSitio.Service1 ws = new SmartDeviceProject1.NapresaSitio.Service1();
        cMetodos c = new cMetodos();
        
        string[] booya = null;
        string[] user;
        int check1 = 0;
        int check2 = 0;
        int[] check3 = new int[2];
        int check4 = 0;
        int pzasParcialidad;
        string op;
        string codigo;
        int renglon;
        int columns;
        int columnas;
        //Guid newIdSql = Guid.NewGuid();//NEWID()
        string index;
        int columnIndex;
        int rowIndex;
        string value;
        string newId;

        public static void LiberarControles(System.Windows.Forms.Control control)
        {
            for (int i = 0; i <= control.Controls.Count - 1; i++)
            {
                if (control.Controls[i].Controls.Count > 0)
                    LiberarControles(control.Controls[i]);
                control.Controls[i].Dispose();
            }
        }

        public Revisar_Avance(string[] usuario)
        {
            InitializeComponent();
            fillDataGrid(0);
            user = usuario;
            Cursor.Current = Cursors.Default;
        }

        public void fillDataGrid(int filtro)
        {
            try
            {
                DataTable dt = c.hiddenProdWDR(filtro);
                DataTable dt2 = c.showProdWDR(filtro);
                dgOrdenesProd.DataSource = dt;
                dgInfoProd.DataSource = dt2;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Hubo un asunto con la conexion. \nFavor de Reintentar en unos momentos", "Advertencia");
                //LiberarControles(this);       
                this.Dispose();
                GC.Collect();
                frmMenu_Produccion fmp = new frmMenu_Produccion(user);
                fmp.Show();
                dgInfoProd.Dispose();
                dgOrdenesProd.Dispose();
            }
        }
        
        private void dgOrdenesProd_Click(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            try
            {
                columns = ((DataTable)dgOrdenesProd.DataSource).Columns.Count;
                columnas = ((DataTable)dgInfoProd.DataSource).Columns.Count;
                booya = new string[columnas];
               for (int x = 0; x < columnas; x++)
               {
                   index = dgOrdenesProd.CurrentCell.ToString();
                   columnIndex = dgOrdenesProd.CurrentCell.ColumnNumber;
                   rowIndex = dgOrdenesProd.CurrentCell.RowNumber;
                   value = dgInfoProd[rowIndex, x].ToString();
                   booya[x] = value;
               }
                //for (int x = 0; x < columnas; x++)
                //{
                //    index = dgInfoProd.CurrentCell.ToString();
                //    columnIndex = dgInfoProd.CurrentCell.ColumnNumber;
                //    rowIndex = dgInfoProd.CurrentCell.RowNumber;
                //    value = dgInfoProd[rowIndex, x].ToString();
                //    booya[x] = value;
                //}

                op = booya[1];
                //newId = newIdSql.ToString(booya[7]);
                newId = booya[7];
                Guid newIdSql = new Guid(newId);

                string  actualizaCantidadParc;
                //tualizaCantidadParc = c.ActualizaCantidadParcialidad(newId,

                codigo = (booya[8]).Trim();
                renglon = Convert.ToInt32(booya[11]);
                pzasParcialidad = Convert.ToInt32(booya[14]);
                

                check1 = c.checkRacks(op, codigo, renglon, newId); //SE AGREGAN booya[11](RENGLON) Y booya[8](Codigo de Producto) y booya[14]
                 check2 = 0;
                 check3 = c.checkAsignados(op, codigo, renglon, newId);
                 check4 = c.racksAsignados(codigo);//PxT
                //PRIMERO REVISEMOS COMO SE MANEJA EL PRODUCTO:
                if (booya[10] != "LIBERADO")
                {
                    if (check4 > 0)
                    {
                        if (check3[0] == 0)
                        {
                            if (check1 == 0 && check2 == 0)
                            {
                                if (booya[10] == "PENDIENTE" && int.Parse(booya[15]) == 0 && int.Parse(booya[16]) == 0)
                                {//esto se activa cuando en ocasiones no sale el cuadro que dice ¿Que operacion desea realizar?
                                    this.Dispose();
                                    GC.Collect();
                                    Detalle_Orden fp = new Detalle_Orden(booya, user, check3[1], newId);//AQUI PASAR NEWID
                                    fp.Show();
                                    dgInfoProd.Dispose();
                                    dgOrdenesProd.Dispose();
                                }
                                else
                                {
                                    panel1.Visible = true;
                                    panel2.Visible = true;
                                }
                            }
                            else if (check1 == 1 && check2 == 0)
                            {
                                if (booya[10] == "PENDIENTE" && int.Parse(booya[15]) == 0 && int.Parse(booya[16]) == 0)
                                {//esto se activa cuando en ocasiones no sale el cuadro que dice ¿Que operacion desea realizar?
                                    this.Dispose();
                                    GC.Collect();
                                    Detalle_Orden fp = new Detalle_Orden(booya, user, check3[1], newId);//AQUI PASAR NEWID
                                    fp.Show();
                                    dgInfoProd.Dispose();
                                    dgOrdenesProd.Dispose();
                                }
                                else
                                {
                                    panel1.Visible = true;
                                    panel2.Visible = true;
                                }
                            }
                            else if (check1 == 0 && check2 == 1)
                            {
                                //Cursor.Current = Cursors.Default;
                                MessageBox.Show("Aun hay ordenes de transferencia incompletas");
                            }
                            else
                            {
                                //Cursor.Current = Cursors.Default;
                                MessageBox.Show("Preparaciones faltantes:\n-Asignar Racks\n-Concluir Tranferencias");
                            }
                        }
                        else
                        {
                            //Cursor.Current = Cursors.Default;
                            MessageBox.Show("La orden seleccionada no tiene Racks Asignados");
                        }
                    }
                    else if (check4 == 0)
                    {
                        if (check2 == 0)
                        {
                            if (booya[10].Trim() == "PENDIENTE" && int.Parse(booya[15]) == 0 && int.Parse(booya[16]) == 0)
                            {
                                //LiberarControles(this);
                                //Cursor.Current = Cursors.Default;
                                this.Dispose();
                                GC.Collect();
                                Detalle_Orden fp = new Detalle_Orden(booya, user, 0,newId);
                                fp.Show();
                                dgInfoProd.Dispose();
                                dgOrdenesProd.Dispose();
                            }

                            else
                            {
                                panel1.Visible = true;
                                panel2.Visible = true;
                            }
                        }
                        else
                        {
                            //Cursor.Current = Cursors.Default;
                            MessageBox.Show("Aun hay ordenes de transferencia incompletas");
                        }
                    }
                    else if (check4 == -404)
                    {
                        MessageBox.Show("El producto de esta orden no se encuentra en la base de TAGO");
                    }
                    else if (check4 == -1)
                    {
                        //Cursor.Current = Cursors.Default;
                        MessageBox.Show("Hay un problema con la comunicación. Por favor intentelo más tarde");
                    }
                }
                else
                {
                    panel1.Visible = true;
                    panel2.Visible = true;
                }
            }
            catch (Exception ee)
            {
                //Cursor.Current = Cursors.Default;
                MessageBox.Show(ee.Message);
                MessageBox.Show("Hay un problema con la comunicación. Por favor intentelo más tarde");
            }
        }


        private void menuItem2_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rbCurado.Checked == true)//Huecos
            {
				// booya[1] es la Orden Produccion...
                int racksPendientes = c.getRacksPendientes(booya[1], booya[8], booya[11], newId);//JLMQ AGREGAR NEWID A ESTE METODO
                if (racksPendientes == 0)
                {//JLMQ SI RACKSPENDIENTES = 0 YA SE PASO LA OP POR CONTEO DE HUECOS
                    MessageBox.Show("Ya ha pasado esta Orden de Producción por esta etapa.");
                    return;
                }//JLMQ SE AGREGA ESTA PARTE PARA HACER PRUEBAS 17 ENE 2016
               if (racksPendientes > 0)
               {
                   if (!c.validaParciExist(newId))
                   {
                       string actualizaCantidadParc;
                       actualizaCantidadParc = c.ActualizaCantidadParcialidad(newId, int.Parse(booya[14]));
                       booya[10] = "PENDIENTE";
                       this.Dispose();
                       GC.Collect();
                       Detalle_Orden fp = new Detalle_Orden(booya, user, 1, newId);
                       fp.Show();
                   }
                   else
                   {
                       booya[10] = "PENDIENTE";
                       this.Dispose();
                       GC.Collect();
                       Detalle_Orden fp = new Detalle_Orden(booya, user, 1, newId);
                       fp.Show();
                   }
               }
            }
            else if (rbHuecos.Checked == true)//Merma
            {
                int current = c.currentValueMermas(booya[1], "CURADO", booya[0], booya[11], newId);//JLMQ MERMAS currentValueMermas
                if (current == 0)
                {
                    //Cursor.Current = Cursors.Default;
                    MessageBox.Show("No tienes piezas en CURADO.\nPrimero da entrada al cuarto de Curado para poder contar huecos", "Aviso");
					return;					
                }
                else if (current < 0)
                {
                    MessageBox.Show("No se pudo tomar la información. \nIntentelo de nuevo más tarde", "Aviso");
                }
                else
                {
                    if (c.racksAsignados(booya[8]) > 0)
                    {
                        booya[10] = "CURADO";
                        //LiberarControles(this);
                        this.Dispose();
                        GC.Collect();
                        if (check4 > 0)
                        {
                            Detalle_Orden fp = new Detalle_Orden(booya, user, 1,newId);
                            fp.Show();
                        }
                        else if (check4 == 0)
                        {
                            Detalle_Orden fp = new Detalle_Orden(booya, user, 0, newId);
                            fp.Show();
                        }
                        else
                        {
                            MessageBox.Show("Hay un problema con la comunicación. Por favor intentelo más tarde");
                        }
                        //Cursor.Current = Cursors.Default;
                        panel1.Visible = false;
                        panel2.Visible = false;
                        dgInfoProd.Dispose();
                        dgOrdenesProd.Dispose();
                    } if (c.racksAsignados(booya[8]) == 0)
                    {
                        booya[10] = "CURADO";
                        //LiberarControles(this);
                        this.Dispose();
                        GC.Collect();
                        Detalle_Orden fp = new Detalle_Orden(booya, user,0, newId);
                        fp.Show();
                        //Cursor.Current = Cursors.Default;
                        panel1.Visible = false;
                        panel2.Visible = false;
                        dgInfoProd.Dispose();
                        dgOrdenesProd.Dispose();
                    }
                }
            }
            else if (rbLiberar.Checked == true)//Liberar (Asignar Escuadra)
            {
                int current = c.currentValueLiberar(booya[1], "LIBERADO", booya[0], booya[11], newId);
                if (current == 0)
                {
                    //Cursor.Current = Cursors.Default;
                    MessageBox.Show("No tienes piezas en LIBERADO.\nPrimero libera piezas para poder Asignar Escuadra", "Aviso");
                    rbLiberar.Checked = false;
                    rbHuecos.Checked = true;                                      
                }
                else if (current < 0)
                {
                    MessageBox.Show("No se pudo tomar la información. \nIntentelo de nuevo más tarde", "Aviso");
                }
                else
                {
                    if (c.tarimasAsignadas(booya[8]) > 0)
                    {
                        booya[10] = "LIBERADO";
                        //LiberarControles(this);
                        this.Dispose();
                        GC.Collect();
                        Detalle_Orden fp = new Detalle_Orden(booya, user, 0, newId);
                        fp.Show();
                        //Cursor.Current = Cursors.Default;
                        panel1.Visible = false;
                        panel2.Visible = false;
                        dgInfoProd.Dispose();
                        dgOrdenesProd.Dispose();
                    }
                    else if (c.tarimasAsignadas(booya[8]) == 0)
                    {
                        booya[10] = "LIBERADO";
                        this.Dispose();
                        GC.Collect();
                        Detalle_Orden fp = new Detalle_Orden(booya, user, 2, newId);
                        fp.Show();
                        //Cursor.Current = Cursors.Default;
                        panel1.Visible = false;
                        panel2.Visible = false;
                        dgInfoProd.Dispose();
                        dgOrdenesProd.Dispose();
                    }
                    else if (c.tarimasAsignadas(booya[8]) == -2)
                    {
                        booya[10] = "LIBERADO";
                        this.Dispose();
                        GC.Collect();
                        Detalle_Orden fp = new Detalle_Orden(booya, user, 2, newId);
                        fp.Show();
                        //Cursor.Current = Cursors.Default;
                        panel1.Visible = false;
                        panel2.Visible = false;
                        dgInfoProd.Dispose();
                        dgOrdenesProd.Dispose();
                    }
                }
            }
            else
            {
                //Cursor.Current = Cursors.Default;
                MessageBox.Show("Favor de seleccionar alguna opción de la lista", "Aviso");
            }
        }

        private void rbCurado_CheckedChanged(object sender, EventArgs e)
        {
            rbHuecos.Checked = false;
            rbLiberar.Checked = false;
        }

        private void rbHuecos_CheckedChanged(object sender, EventArgs e)
        {
            rbCurado.Checked = false;
            rbLiberar.Checked = false;
        }

        private void rbLiberar_CheckedChanged(object sender, EventArgs e)
        {
            rbCurado.Checked = false;
            rbHuecos.Checked = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.Default;
            panel1.Visible = false;
            panel2.Visible = false;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.Default;
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            frmMenu_Produccion fmp = new frmMenu_Produccion(user);
            fmp.Show();
            dgInfoProd.Dispose();
            dgOrdenesProd.Dispose();
        }

        private void Revisar_Avance_Closing(object sender, CancelEventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            frmMenu_Produccion fmp = new frmMenu_Produccion(user);
            fmp.Show();
            dgInfoProd.Dispose();
            dgOrdenesProd.Dispose();
        }

        private void mi_NoFiltro_Click(object sender, EventArgs e)
        {
            fillDataGrid(0);
            mi_NoFiltro.Checked = true;
            mi_FiltroPendiente.Checked = false;
            mi_FiltroLiberado.Checked = false;
            mi_FiltroCurado.Checked = false;
        }

        private void mi_FiltroPendiente_Click(object sender, EventArgs e)
        {
            fillDataGrid(1);
            mi_NoFiltro.Checked = false;
            mi_FiltroPendiente.Checked = true;
            mi_FiltroCurado.Checked = false; 
            mi_FiltroLiberado.Checked = false;
        }

        private void mi_FiltroCurado_Click(object sender, EventArgs e)
        {
            fillDataGrid(2);
            mi_NoFiltro.Checked = false;
            mi_FiltroPendiente.Checked = false;
            mi_FiltroCurado.Checked = true;
            mi_FiltroLiberado.Checked = false;
        }

        private void mi_FiltroLiberado_Click(object sender, EventArgs e)
        {
            fillDataGrid(3);
            mi_NoFiltro.Checked = false;
            mi_FiltroPendiente.Checked = false;
            mi_FiltroCurado.Checked = false; 
            mi_FiltroLiberado.Checked = true;
        }

        private void label2_ParentChanged(object sender, EventArgs e)
        {

        }

        private void dgOrdenesProd_CurrentCellChanged(object sender, EventArgs e)
        {

        }

    }
}