using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1
{
    public partial class frmMenu_Inventario : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        string[] user;
        cMetodos c = new cMetodos();

        public frmMenu_Inventario(string[] usuario)
        {
            InitializeComponent();
            user = usuario;
            /*
            int countRes = c.getInt("select count(*) from InvCongelado where status = 1 AND usuario = '" + user[4].ToString() + "'", "Solutia");
            if (countRes > 0)
            {
                DialogResult res = MessageBox.Show("Usted tiene inventarios pendientes, desea continuar con el último?", "Inventarios Pendientes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Yes)
                {
                    int idInvCongPen = c.getInt("select TOP(1)idInv from InvCongelado where status = 1 AND usuario = '" + user[4].ToString() + "'", "Solutia");
                    frmLectura frm = new frmLectura(user, idInvCongPen.ToString());
                    frm.Visible = true;
                    
                }
                
                

            }*/
        }

        private void mi_Regresar_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Inventario_Click(object sender, EventArgs e)
        {
            Inventario.Confirma_Inventario cInv = new SmartDeviceProject1.Inventario.Confirma_Inventario(user);
            cInv.Show();
        }

        private void btnContinuar_Inventario_Click(object sender, EventArgs e)
        {
            Inventario.Continuar_Inventario ici = new SmartDeviceProject1.Inventario.Continuar_Inventario(user);
            ici.Show();
        }

        private void btnBuscar_Tag_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Inventario.Buscar_Tag ibt = new SmartDeviceProject1.Inventario.Buscar_Tag("");
            ibt.Show();
            Cursor.Current = Cursors.Default;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMenu_Inventario_Closing(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Inventario.Reclasificacion rec = new SmartDeviceProject1.Inventario.Reclasificacion(user);
            rec.Show();
            //Inventario.Reporte_Inventario iri = new SmartDeviceProject1.Inventario.Reporte_Inventario(user);
            //iri.Show();
            Cursor.Current = Cursors.Default;
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            Inventario.Producto_Stock ps = new SmartDeviceProject1.Inventario.Producto_Stock(user[1], user[2]);
            ps.Show();
        }



        private void BtnInvIni_Click(object sender, EventArgs e)
        {
            //Inventario.Inventario_Inicial invi = new SmartDeviceProject1.Inventario.Inventario_Inicial(user);
            //invi.Show();
            Inventario.Carga_Inv cargaInv = new SmartDeviceProject1.Inventario.Carga_Inv(user);
            cargaInv.Show();
        }
    }
}