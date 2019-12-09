using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Intermec.DataCollection.RFID;

namespace SmartDeviceProject1.Produccion
{
    public partial class Huecos_Racks : Form
    {

        SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos met = new cMetodos();
        string tag;
        string[] racks;
        string[] user;
        string[] folio;
        BRIReader lector;
        int evnt;
        int tr;
        string reng;
        int pzasTotalParcialidad = 0;
        string op;
        string id;
        int cantidad;
        string usuario;
        string newId;



        public Huecos_Racks(string[] detalle, string[] usuario, string[] rack, string epc, BRIReader reader,int evento, int tipoRack, string newIdSQL)
        {
            InitializeComponent();
            newId = newIdSQL;
            txtHuecos.Focus();
            lblNiveles.Text = lblNiveles.Text + rack[6];
            lblVentanas.Text = lblVentanas.Text + rack[7];
            if (evento==0)
                txtEstimado.Text = rack[3];
            if (evento == 1)
                txtEstimado.Text = rack[4];
            lblOP.Text = rack[2];
            lblArticulo.Text = rack[8];
            tag = epc;
            racks = rack;// VER QUE PASA RACK
            user = usuario;
            folio = detalle;
            reng = detalle[11];
            lector = reader;
            evnt = evento;
            if (evnt == 0)
            {
                txtHuecos.Enabled = false;
                label4.Text = "Huecos";
            }
            else
            {
                txtHuecos.Enabled = true;
                label4.Text = "Mermas";
            }
            tr = tipoRack;
            epc = tag;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                int estimado = int.Parse(txtEstimado.Text), huecos = int.Parse(txtHuecos.Text);
                if (huecos > estimado)
                {
                    MessageBox.Show("La cantidad de huecos no puede ser mayor a la cantidad por Rack", "Aviso");
                }
                else
                {
                    int real = estimado - huecos;
                    op = folio[1];
                    id = folio[0];
                    reng = folio[11];
                    usuario = user[4];
                    pzasTotalParcialidad = Convert.ToInt32(folio[14]);
                    if (evnt == 0)//JLMQ CONTEO DE HUECOS
                    {
                        txtHuecos.Enabled = false;
                        int flag = met.setContado(tag, real);//JLMQ AQUI  marca como contado el rack recien leido
                        if (flag == 0)
                        {
                            //string op, string estado, string id, string renglon, int cantidad, string user
                            //PASAR LA LINEA DE ABAJO PARA QUE SE HAGA SOLO UNA VEZ.
							//String msg = met.avanzarEstadoHuecos(op, "PRODUCCION", id, reng, real, usuario,pzasTotalParcialidad, newId);//SE AGREGA LA CANTIDAD DE LA PARCIALIDAD TOTAL
                            //MessageBox.Show(msg);
                            this.Dispose();
                            GC.Collect();
                            Contar_Huecos ch1 = new Contar_Huecos(user, folio, true, lector, 0, newId,tag);
                            ch1.Show();
                        }
                        else
                            MessageBox.Show("Hubo un problema al guardar.\n Intente de nuevo");
                    } 
                    if (evnt == 1)//JLMQ CONTEO DE MERMAS
                    {
                        int flag = met.setContadoCurado(tag, real);//JLMQ marca como contado el rack recien leido. (conteo de huecos)
                        if (flag == 0)
                        {
                            //int res1 = met.contarHuecos(huecos, int.Parse(folio[0]), tag, folio[8], user[4], user[3], reng, newId);
                            bool res;
                            int mermaProd = 0;
                            mermaProd = int.Parse(txtHuecos.Text);
                            res = met.reportaMermaProd(newId, mermaProd);
                            switch (res)
                            {
                                case true:
                                    MessageBox.Show("Cambio Exitoso");
                                    this.Dispose();
                                    GC.Collect();
                                    Contar_Huecos ch1 = new Contar_Huecos(user, folio, true, lector, 1, newId,tag);
                                    ch1.Show();
                                    break;
                                case false:
                                    MessageBox.Show("Hubo un problema al guardar.\n Intente de nuevo");
                                    break;
                                
                                default: break;
                            }
                        }
                        else
                            MessageBox.Show("Hubo un problema al guardar.\n Intente de nuevo");
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (FormatException ee)
            {
                MessageBox.Show("El campo de huecos no puede llevar caracteres no numericos", "Aviso");
                txtHuecos.Text = "0";
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Produccion.Revisar_Avance ra = new Revisar_Avance(user);
            ra.Show();
            this.Close();
            GC.Collect();
        }
    }
}