using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Intermec.DataCollection.RFID;

namespace SmartDeviceProject1.Produccion
{
    public partial class Paneles_Conteo : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.NapresaSitio.Service1 ws = new SmartDeviceProject1.NapresaSitio.Service1();
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos c = new cMetodos();

        string id = "";
        string[] folio;
        string fol = "";
        int tr = 0;
        string cod = "";
        int[] huecos;
        int size = 0;
        int identNivel = 0;
        int identVent = 0;
        int pos = 0;
        string epc = "";
        int ident = 0;
        EventArgs ee = new EventArgs();
        string[] llenar = new string[15];
        //private delegate void UpdateStatusDelegate(int pos);
        string[] user;
        int conteo = 0;
        BRIReader lector;
        int pv, diferencia;


        public Paneles_Conteo(int tipoRack, int id, string pedido, string tag, string[] usuario,string[] datos, BRIReader reader, int pxt)
        {
            InitializeComponent();
            tr = tipoRack;
            lector = reader;
            fol = pedido;
            epc = tag;
            folio = datos;
            cod = datos[8];
            user = usuario;
            pv = pxt;
            this.id = id.ToString();
            if (pv > 0)
            {
                switch (tr)
                {
                    case 1:
                        pbN1.Visible = true;
                        pbN2.Visible = true;
                        pbN3.Visible = true;
                        pbN4.Visible = true;
                        pbN5.Visible = true;
                        pbN6.Visible = true;
                        pbN7.Visible = true;
                        pbN8.Visible = false;
                        pbN9.Visible = false;
                        pbN1.Enabled = true;
                        pbN2.Enabled = true;
                        pbN3.Enabled = true;
                        pbN4.Enabled = true;
                        pbN5.Enabled = true;
                        pbN6.Enabled = true;
                        pbN7.Enabled = true;
                        pbN8.Enabled = false;
                        pbN9.Enabled = false;
                        pbV3.Enabled = false;
                        pbV3.Visible = false;
                        huecos = null;
                        huecos = new int[14];
                        break;
                    case 2:
                        pbN1.Visible = true;
                        pbN2.Visible = true;
                        pbN3.Visible = true;
                        pbN4.Visible = true;
                        pbN5.Visible = true;
                        pbN6.Visible = true;
                        pbN7.Visible = true;
                        pbN8.Visible = true;
                        pbN9.Visible = true;
                        pbN1.Enabled = true;
                        pbN2.Enabled = true;
                        pbN3.Enabled = true;
                        pbN4.Enabled = true;
                        pbN5.Enabled = true;
                        pbN6.Enabled = true;
                        pbN7.Enabled = true;
                        pbN8.Enabled = true;
                        pbN9.Enabled = true;
                        pbV3.Enabled = false;
                        pbV3.Visible = false;
                        huecos = null;
                        huecos = new int[18];
                        break;
                    case 3:
                        pbN1.Visible = true;
                        pbN2.Visible = true;
                        pbN3.Visible = true;
                        pbN4.Visible = true;
                        pbN5.Visible = true;
                        pbN6.Visible = true;
                        pbN7.Visible = true;
                        pbN8.Visible = false;
                        pbN9.Visible = false;
                        pbN1.Enabled = true;
                        pbN2.Enabled = true;
                        pbN3.Enabled = true;
                        pbN4.Enabled = true;
                        pbN5.Enabled = true;
                        pbN6.Enabled = true;
                        pbN7.Enabled = true;
                        pbN8.Enabled = false;
                        pbN9.Enabled = false;
                        huecos = null;
                        huecos = new int[21];
                        break;
                    case 4:
                        pbN1.Visible = true;
                        pbN2.Visible = true;
                        pbN3.Visible = true;
                        pbN4.Visible = true;
                        pbN5.Visible = true;
                        pbN6.Visible = true;
                        pbN7.Visible = false;
                        pbN8.Visible = false;
                        pbN9.Visible = false;
                        pbN1.Enabled = true;
                        pbN2.Enabled = true;
                        pbN3.Enabled = true;
                        pbN4.Enabled = true;
                        pbN5.Enabled = true;
                        pbN6.Enabled = true;
                        pbN7.Enabled = false;
                        pbN8.Enabled = false;
                        pbN9.Enabled = false;
                        huecos = null;
                        huecos = new int[18];
                        break;
                    default: break;
                }
            }
            else if (pv == 0)
            {
                pbN1.Visible = false;
                pbN2.Visible = false;
                pbN3.Visible = false;
                pbN4.Visible = false;
                pbN5.Visible = false;
                pbN6.Visible = false;
                pbN7.Visible = false;
                pbN8.Visible = false;
                pbN9.Visible = false;
                pbN1.Enabled = false;
                pbN2.Enabled = false;
                pbN3.Enabled = false;
                pbN4.Enabled = false;
                pbN5.Enabled = false;
                pbN6.Enabled = false;
                pbN7.Enabled = false;
                pbN8.Enabled = false;
                pbN9.Enabled = false;
                menuItem1.Enabled = true;
            }
        }

        public static void LiberarControles(System.Windows.Forms.Control control)
        {
            for (int i = 0; i <= control.Controls.Count - 1; i++)
            {
                if (control.Controls[i].Controls.Count > 0)
                    LiberarControles(control.Controls[i]);
                control.Controls[i].Dispose();
            }
        }

        private void pbN1_Click(object sender, EventArgs e)
        {
            clearPnl();
            pnlVentana.Visible = true;
            identNivel = 1;
            menuItem1.Enabled = true;
            label1.Text = "Usted esta contando las ventanas del\nNivel " + identNivel;
            conteo = 1;
        }

        private void pbN2_Click(object sender, EventArgs e)
        {
            clearPnl();
            pnlVentana.Visible = true;
            identNivel = 2;
            menuItem1.Enabled = true;
            label1.Text = "Usted esta contando las ventanas del\nNivel " + identNivel;
            conteo = 1;
        }

        private void pbN3_Click(object sender, EventArgs e)
        {
            clearPnl();
            pnlVentana.Visible = true;
            identNivel = 3;
            menuItem1.Enabled = true;
            label1.Text = "Usted esta contando las ventanas del\nNivel " + identNivel;
            conteo = 1;
        }

        private void pbN4_Click(object sender, EventArgs e)
        {
            clearPnl();
            pnlVentana.Visible = true;
            identNivel = 4;
            menuItem1.Enabled = true;
            label1.Text = "Usted esta contando las ventanas del\nNivel " + identNivel;
            conteo = 1;
        }

        private void pbN5_Click(object sender, EventArgs e)
        {
            clearPnl();
            pnlVentana.Visible = true;
            identNivel = 5;
            menuItem1.Enabled = true;
            label1.Text = "Usted esta contando las ventanas del\nNivel " + identNivel;
            conteo = 1;
        }

        private void pbN6_Click(object sender, EventArgs e)
        {
            clearPnl();
            pnlVentana.Visible = true;
            identNivel = 6;
            menuItem1.Enabled = true;
            label1.Text = "Usted esta contando las ventanas del\nNivel " + identNivel;
            conteo = 1;
        }

        private void pbN7_Click(object sender, EventArgs e)
        {
            clearPnl();
            pnlVentana.Visible = true;
            identNivel = 7;
            menuItem1.Enabled = true;
            label1.Text = "Usted esta contando las ventanas del\nNivel " + identNivel;
            conteo = 1;
        }

        private void pbN8_Click(object sender, EventArgs e)
        {
            clearPnl();
            pnlVentana.Visible = true;
            identNivel = 8;
            menuItem1.Enabled = true;
            label1.Text = "Usted esta contando las ventanas del\nNivel " + identNivel;
            conteo = 1;
        }

        private void pbN9_Click(object sender, EventArgs e)
        {
            clearPnl();
            pnlVentana.Visible = true;
            identNivel = 1;
            menuItem1.Enabled = true;
            label1.Text = "Usted esta contando las ventanas del\nNivel " + identNivel;
            conteo = 1;
        }

        private void clearPnl()
        {
            switch (tr)
            {
                case 1:
                    pbV1.Visible = true;
                    pbV2.Visible = true;
                    pbV1_Click(this.pbV1, ee);
                    txtHueco.Focus();
                    break;
                case 2:
                    pbV1.Visible = true;
                    pbV2.Visible = true;
                    pbV1_Click(this.pbV1, ee);
                    txtHueco.Focus();
                    break;
                case 3:
                    pbV1.Visible = true;
                    pbV2.Visible = true;
                    pbV3.Visible = true;
                    pbV1_Click(this.pbV1, ee);
                    txtHueco.Focus();
                    break;
                case 4:
                    pbV1.Visible = true;
                    pbV2.Visible = true;
                    pbV3.Visible = true;
                    pbV1_Click(this.pbV1, ee);
                    txtHueco.Focus();
                    break;
                default: break;
            }
        }

        public int totalHuecos(int[] merma)
        {
            int total = 0;
            for (int i = 0; i < merma.Length; i++)
            {
                total = total + merma[i];
            }
            return total;
        }

        private void pbV1_Click(object sender, EventArgs e)
        {
            label11.Refresh();
            label11.Text = "Ventana 1";
            identVent = 1;
        }

        private void pbV2_Click(object sender, EventArgs e)
        {
            label11.Refresh();
            label11.Text = "Ventana 2";
            identVent = 2;
        }

        private void pbV3_Click(object sender, EventArgs e)
        {
            label11.Refresh();
            label11.Text = "Ventana 3";
            identVent = 3;
        }

        private void btnConteo_Click(object sender, EventArgs e)
        {
            //if (pv > 0)
            //{
            //    if (conteo == 0)
            //    {
            //        try
            //        {
            //            if (checkNiveles(tr))
            //            {
            //                int res = c.setContado(epc,0);
            //                if (res == 0)
            //                {
            //                    int res2 = totalHuecos(huecos);
            //                    int res3 = 0;//c.contarHuecos(res2, int.Parse(id), epc, tr, cod, user[4], user[3]);
            //                    switch (res3)
            //                    {
            //                        case 0:
            //                            var proceso = Process.GetCurrentProcess();
            //                            //proceso.Close();
            //                            this.Dispose();
            //                            GC.Collect();
            //                            Contar_Huecos ch1 = new Contar_Huecos(user, folio, true, lector,1);
            //                            ch1.Show();
            //                            pnlVentana.Dispose();
            //                            break;
            //                        case 1:
            //                            MessageBox.Show("Hubo un problema al guardar.\n Intente de nuevo");
            //                            break;
            //                        case 2:
            //                            MessageBox.Show("La cantidad a mermar no puede superar el maximo permitido para este rack");
            //                            break;
            //                        case 3:
            //                            var proceso2 = Process.GetCurrentProcess();
            //                            //proceso2.Close();
            //                            this.Dispose();
            //                            GC.Collect();
            //                            Contar_Huecos ch2 = new Contar_Huecos(user, folio, true, lector,1);
            //                            ch2.Show();
            //                            pnlVentana.Dispose();
            //                            break;
            //                        default: break;
            //                    }
            //                }
            //                else
            //                    MessageBox.Show("Hubo un problema al guardar.\n Intente de nuevo");
            //            }
            //            else
            //                MessageBox.Show("Falta Niveles por Contabilizar");
            //        }
            //        catch (Exception ee)
            //        {
            //            MessageBox.Show(ee.Message);
            //            MessageBox.Show("Hubo un problema con información.\nEl campo no acepta valores no numericos.");
            //        }
            //    }
            //    else
            //    {
            //        if (checkVentanas(tr))
            //        {
            //            switch (identNivel)
            //            {
            //                case 1:
            //                    pbN1.Visible = false;
            //                    break;
            //                case 2:
            //                    pbN2.Visible = false;
            //                    break;
            //                case 3:
            //                    pbN3.Visible = false;
            //                    break;
            //                case 4:
            //                    pbN4.Visible = false;
            //                    break;
            //                case 5:
            //                    pbN5.Visible = false;
            //                    break;
            //                case 6:
            //                    pbN6.Visible = false;
            //                    break;
            //                case 7:
            //                    pbN7.Visible = false;
            //                    break;
            //                case 8:
            //                    pbN8.Visible = false;
            //                    break;
            //                case 9:
            //                    pbN9.Visible = false;
            //                    break;
            //                default: break;
            //            }
            //            pnlVentana.Visible = false;
            //            identVent = 0;
            //            identNivel = 0;
            //            menuItem1.Enabled = false;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Faltan ventanas por Contabilizar");
            //        }
            //    }
            //}
            //else if (pv == 0)
            //{
            //    if (conteo == 0)
            //    {
            //        try
            //        {
            //            if (checkNiveles(tr))
            //            {
            //                int res = c.setContado(epc,0);
            //                if (res == 0)
            //                {
            //                    int res2 = diferencia;
            //                    int res3 = 0;//c.contarHuecos(res2, int.Parse(id), epc, tr, cod, user[4], user[3]);
            //                    switch (res3)
            //                    {
            //                        case 0:
            //                            var proceso = Process.GetCurrentProcess();
            //                            //proceso.Close();
            //                            this.Dispose();
            //                            GC.Collect();
            //                            Contar_Huecos ch1 = new Contar_Huecos(user, folio, true, lector,1);
            //                            ch1.Show();
            //                            pnlVentana.Dispose();
            //                            break;
            //                        case 1:
            //                            MessageBox.Show("Hubo un problema al guardar.\n Intente de nuevo");
            //                            break;
            //                        case 2:
            //                            MessageBox.Show("La cantidad a mermar no puede superar el maximo permitido para este rack");
            //                            break;
            //                        case 3:
            //                            var proceso2 = Process.GetCurrentProcess();
            //                            //proceso2.Close();
            //                            this.Dispose();
            //                            GC.Collect();
            //                            Contar_Huecos ch2 = new Contar_Huecos(user, folio, true, lector,1);
            //                            ch2.Show();
            //                            pnlVentana.Dispose();
            //                            break;
            //                        default: break;
            //                    }
            //                }
            //                else
            //                    MessageBox.Show("Hubo un problema al guardar.\n Intente de nuevo");
            //            }
            //            else
            //                MessageBox.Show("Falta Niveles por Contabilizar");
            //        }
            //        catch (Exception ee)
            //        {
            //            MessageBox.Show(ee.Message);
            //            MessageBox.Show("Hubo un problema con información.\nEl campo no acepta valores no numericos.");
            //        }
            //    }
            //    else
            //    {
            //        if (checkVentanas(tr))
            //        {
            //            switch (identNivel)
            //            {
            //                case 1:
            //                    pbN1.Visible = false;
            //                    break;
            //                case 2:
            //                    pbN2.Visible = false;
            //                    break;
            //                case 3:
            //                    pbN3.Visible = false;
            //                    break;
            //                case 4:
            //                    pbN4.Visible = false;
            //                    break;
            //                case 5:
            //                    pbN5.Visible = false;
            //                    break;
            //                case 6:
            //                    pbN6.Visible = false;
            //                    break;
            //                case 7:
            //                    pbN7.Visible = false;
            //                    break;
            //                case 8:
            //                    pbN8.Visible = false;
            //                    break;
            //                case 9:
            //                    pbN9.Visible = false;
            //                    break;
            //                default: break;
            //            }
            //            pnlVentana.Visible = false;
            //            identVent = 0;
            //            identNivel = 0;
            //            menuItem1.Enabled = false;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Faltan ventanas por Contabilizar");
            //        }
            //    }
            //}JLMQ SE COMENTA PARA PARCIALIDADES NEWID 13SEPT2018
        }

        public Boolean checkVentanas(int free)
        {
            Boolean flag = false;
            switch (free)
            {
                case 1:
                    if (pbV1.Visible == false && pbV2.Visible == false)
                    {
                        flag = true;
                        conteo = 0;
                    }
                    else
                    {
                    }
                    break;
                case 2:
                    if (pbV1.Visible == false && pbV2.Visible == false)
                    {
                        flag = true;
                        conteo = 0;
                    }
                    else
                    {
                    }
                    break;
                case 3:
                    if (pbV1.Visible == false && pbV2.Visible == false && pbV3.Visible == false)
                    {
                        flag = true;
                        conteo = 0;
                    }
                    else
                    {
                    }
                    break;
                case 4:
                    if (pbV1.Visible == false && pbV2.Visible == false && pbV3.Visible == false)
                    {
                        flag = true;
                        conteo = 0;
                    }
                    else
                    {
                    }
                    break;
            }

            return flag;
        }

        public Boolean checkNiveles(int free)
        {
            Boolean flag = false;
            switch (free)
            {
                case 1:
                    if (pbN1.Visible == false && pbN2.Visible == false && pbN3.Visible == false &&
                        pbN4.Visible == false && pbN5.Visible == false && pbN6.Visible == false && pbN7.Visible == false)
                    {
                        flag = true;
                    }
                    else
                    {
                    }
                    break;
                case 2:
                    if (pbN1.Visible == false && pbN2.Visible == false && pbN3.Visible == false &&
                        pbN4.Visible == false && pbN5.Visible == false && pbN6.Visible == false &&
                        pbN7.Visible == false && pbN8.Visible == false && pbN9.Visible == false)
                    {
                        flag = true;
                    }
                    else
                    {
                    }
                    break;
                case 3:
                    if (pbN1.Visible == false && pbN2.Visible == false && pbN3.Visible == false &&
                        pbN4.Visible == false && pbN5.Visible == false && pbN6.Visible == false && pbN7.Visible == false)
                    {
                        flag = true;
                    }
                    else
                    {
                    }
                    break;
                case 4:
                    if (pbN1.Visible == false && pbN2.Visible == false && pbN3.Visible == false &&
                        pbN4.Visible == false && pbN5.Visible == false && pbN6.Visible == false)
                    {
                        flag = true;
                    }
                    else
                    {
                    }
                    break;
            }

            return flag;

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (pv > 0)
                {
                    if (txtHueco.Text.Length > 0)
                    {
                        int res = int.Parse(c.checkCantidad(tr, cod, Int32.Parse(txtHueco.Text.ToString())).ToString());
                        if (res == -2)
                        {
                            MessageBox.Show("La cantidad de huecos es mayor al producto calculado por ventana");
                        }
                        else if (res == -1)
                        {
                            MessageBox.Show("La cantidad de huecos es mayor al producto calculado por rack");
                        }
                        else
                        {
                            switch (identVent)
                            {
                                case 1:
                                    pbV1.Visible = false;
                                    if (pbV2.Visible == true)
                                    {
                                        pbV2_Click(this.pbV2, ee);
                                        txtHueco.Focus();
                                        huecos[pos] = int.Parse(txtHueco.Text.ToString());
                                        txtHueco.Text = "";
                                        pos++;
                                    }
                                    else if (pbV3.Visible == true)
                                    {
                                        pbV3_Click(this.pbV3, ee);
                                        txtHueco.Focus();
                                        huecos[pos] = int.Parse(txtHueco.Text.ToString());
                                        txtHueco.Text = "";
                                        pos++;
                                    }
                                    else
                                    {
                                        btnConteo_Click(this.btnConteo, ee);
                                        huecos[pos] = int.Parse(txtHueco.Text.ToString());
                                        txtHueco.Text = "";
                                        pos++;
                                    }
                                    break;
                                case 2:
                                    pbV2.Visible = false;
                                    if (pbV1.Visible == true)
                                    {
                                        pbV1_Click(this.pbV1, ee);
                                        txtHueco.Focus();
                                        huecos[pos] = int.Parse(txtHueco.Text.ToString());
                                        txtHueco.Text = "";
                                        pos++;
                                    }
                                    else if (pbV3.Visible == true)
                                    {
                                        pbV3_Click(this.pbV3, ee);
                                        txtHueco.Focus();
                                        huecos[pos] = int.Parse(txtHueco.Text.ToString());
                                        txtHueco.Text = "";
                                        pos++;
                                    }
                                    else
                                    {
                                        btnConteo_Click(this.btnConteo, ee);
                                        huecos[pos] = int.Parse(txtHueco.Text.ToString());
                                        txtHueco.Text = "";
                                        pos++;
                                    }
                                    break;
                                case 3:
                                    pbV3.Visible = false;
                                    if (pbV1.Visible == true)
                                    {
                                        pbV1_Click(this.pbV1, ee);
                                        txtHueco.Focus();
                                        huecos[pos] = int.Parse(txtHueco.Text.ToString());
                                        txtHueco.Text = "";
                                        pos++;
                                    }
                                    else if (pbV2.Visible == true)
                                    {
                                        pbV2_Click(this.pbV2, ee);
                                        txtHueco.Focus();
                                        huecos[pos] = int.Parse(txtHueco.Text.ToString());
                                        txtHueco.Text = "";
                                        pos++;
                                    }
                                    else
                                    {
                                        btnConteo_Click(this.btnConteo, ee);
                                        huecos[pos] = int.Parse(txtHueco.Text.ToString());
                                        txtHueco.Text = "";
                                        pos++;
                                    }
                                    break;
                                default: break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo no puede ir en blanco");
                    }
                }
                else if (pv == 0)
                {
                    
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                MessageBox.Show("Hubo un error durante el conteo.\nPor favor intentelo nuevamente.");
            }
        }

    }
}