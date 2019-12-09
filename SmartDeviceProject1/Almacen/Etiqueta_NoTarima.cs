using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ZSDK_API.Comm;
using ZSDK_API.ApiException;
using System.IO;
using System.Threading;

namespace SmartDeviceProject1.Almacen
{
    public partial class Etiqueta_NoTarima : Form
    {
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos ws = new cMetodos();

        string[] folio = new string[17];
        string tag = "";
        int ct = 0;
        String macAdd;
        string[] user;
        String zpl = null;
        [DllImport("kernell32.dll", SetLastError = true)]
        private static extern void SetProcess();

        [DllImport("CoreDll.DLL", SetLastError = true)]
        private static extern int CreateProcess(String imageName, String cmdLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, Int32 boolInheritHandles, Int32 dwCreationFlags, IntPtr lpEnvironment, IntPtr lpszCurrentDir, byte[] si, ProcessInfo pi);

        [DllImport("coredll")]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("coredll")]
        private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int GetExitCodeProcess(IntPtr hProcess, ref int lpExitCode);

        public class ProcessInfo
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public IntPtr ProcessID;
            public IntPtr ThreadID;
        }

        public Etiqueta_NoTarima(string[] detalle, int cantidadTarima, string epc, string[] usuario, string newIdEsc)
        {
            InitializeComponent();
            Cursor.Current = Cursors.Default;
            folio = detalle;
            //lblEPC.Text = epc;
            tag = epc;
            user = usuario;
            ct = cantidadTarima;
            lblPedido.Text = detalle[2];
            lblOrdProd.Text = detalle[1];
            lblCliente.Text = detalle[3];
            int current = ws.TarimasImpresas(detalle[1],detalle[8]);
            //int total = ws.calculaTarima(detalle[8], int.Parse(detalle[9]));
            int total = int.Parse(detalle[9]);
            lblTarima.Text = current + " de " + total;
            lbTipo.Text = detalle[5];
            lblMedida.Text = detalle[6];
            lblColor.Text = detalle[7];
            lblResistencia.Text = cantidadTarima + "";
            lblCodigo.Text = detalle[8];
            lblCantidad.Text = detalle[9];
            label5.Text = detalle[4];
            zpl = "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR8,8~SD15^JUS^LRN^CI0^XZ" +
                "~DG000.GRF,05120,040," +
                ",:::::::::::::::J020T020P020,,::::::R020L020H020202020H020,,::::K0EFFEFEFEE80O0BE0BE82820FE03E80,J015N5Q054055010H0H5055,J03FBHBFBHBFAA2AHA2A003A38E3E3A22E38A3A0,J015N5P0101041410114104140,K0OEN8H03800E0C2820C28E0C0,J015N5P010H041410H04104040,J03BNB82A2A2A2003800EB83820EB8E2E0,J015N5P010H0H5030117704140,K0OEN8H038E8EE82820EE0C0C0,J015N5P0105041410H04004040,J03FBLBFAMAH03838E2E3A22E00E2E0,J015N5P0101040410514004140,K0OEN8H03838E0C2820C00E080,J015N5P01450404104040041,J03BNB82A2A2A2002BB8E0E3BA0A00BF80,J015N5Q05704041540I037,K0OEN8I0280J0A8080H08,L015K54,J0202020202020202020H020L020N02020H02020202020P020,,L0O82FEFEFE80,U0M5,K02A2AHA2AHA3BKB800203E20H03FA0H0202E0H02002003F80I0BE20H02FA,U0M5H0170750H015H5I070150H05014015540H03550I0H57,L0O8AEKE803AAEHEH0AEEB800AEIEH0EC2E02AEE800EIEH02EEAC0,U0M5H015I54005I54005I5400545405I54015I5H015H540,K02ANAMBA03FBFBA02FFBBE02BIBA00BEFE2BIBA03BFFB80FFBHB0,U0M5H015J50155154015J5H0I5415I54015I5H0H51550,L0O8ME802EBAAE83EC0EE00EE8EE80EHEC1EE8AE02E8AEC0AE0AE8,U0M5H015405501500540054055005H5415405405501540540150,K02A2A2A2A2A3BKB803BE0FB83F803A00BE23B80BBFA3B80BB03BA0BE0BA03B8,U0M5H01540550150055015405500554035005501500540540354,L0O8AEKE802E80EA82E80EE00EE02E80EB802E802E82E80FC0F802EC,U0M5H01500550J05400540550054001500550150M0H54,J0H2OA3BFBHBF803F803F80203BA02BE03FA0BE203BA0BB23BE0I0202FBC,U0M5H01500550I0I5015401500540035J501550K01554,L0O82EKE802E80EE80H0IEH0HE02E80EE002EJE80AE80J0IEC,U015J54001500550H0J5H05405500540015J5H0H540I015H54,O020L02020I03B807B803BHBA00BE03B80FA003BJBH0FBF80H0BFBBC,gJ01500550015I5015405500540035J5H01550H015I54,K0OE808K8H02E80EE80EECAE00EC02E80EE002EEFEE800EAE002AEAEC,J015N5P0150055005505400540550054001540K0I5H0H54154,J03BNB8ALAH03F803B83BE2FA02BE03B80BE003B8020I03BF80FBA3BC20,J015N5P015005501540550154015005400350L015500540154,K0OEN8H02E80EE82E80EE00EE02E80EE002E80L0HEC0EE02EC,J015N5P015005501500550054055005400150M01541540154,J03BNB82A2A2A2003B807B83B80BE00BE03B80BA003B803F03E03BE3BA03BC02AA,J015N5P01500550350055015405500540015005505501541540154,K0AELEA8M8H02E80EE82E80EE00EE02E80EE002E80AE02E80AE1EE02EC00A2,J015N5P015005501540540054055005400154055055015415405540011,J03BMBF8AJA2A003BA03B83BE3BA02BB8FFA0FE203FE0BE23B83FE3BE2BBC08A880,J015N5P01500550155155015J5H05400155154015455405515540050,K0OEN8H02E80EE82EJEH0KE80EE0H0KE03EFEEC0EHEAEC08B880,J015N5P0150055015J5H0J5400540H0J54015I5H0K540010,J03BNB82AHA2A2003B807B82FBHBA00BFFBE02FE0H03BHBA02FBHB80BJBC020A,J015N5P01500350055415015455400540H015H5I0I5400155154,K0FEFEFEFHF808K8I0A800A003F80800EC3E800880I0BFE0I0HF80H0FC0F80188,gY0540gP040,L020H020L020X02BE020P020,gX01540,gY0EC0,gY0540,gY0BE0,gX01540,gY0EC0,gY0540,gX02FA0,gY05,,:::::::::::::::::::::::::::::^XA" +
                "^MMT" +
                "^PW812" +
                "^LL0406" +
                "^LS0" +
                "^FT89,29^A0N,23,24^FH\\^FD" + lblPedido.Text + "^FS" +
                "^FT250,70^A0N,23,24^FH\\^FD" + lblOrdProd.Text + "^FS" +
                "^FT92,116^A0N,23,24^FH\\^FD" + lblCliente.Text + "^FS" +
                "^FT109,148^A0N,23,24^FH\\^FD" + lblTarima.Text + "^FS" +
                "^FT44,178^A0N,23,24^FH\\^FD" + detalle[4] + "^FS" +
                "^FT579,383^A0N,23,24^FH\\^FD" + lblCantidad.Text + "^FS" +
                "^FT204,385^A0N,23,24^FH\\^FD" + cantidadTarima + "^FS" +
                "^FT93,320^A0N,23,24^FH\\^FD" + lblMedida.Text + "^FS" +
                "^FT90,282^A0N,23,24^FH\\^FD" + lblCodigo.Text + "^FS" +
                "^FT74,249^A0N,23,24^FH\\^FD" + lblColor.Text + "^FS" +
                "^FT68,217^A0N,23,24^FH\\^FD" + lbTipo.Text + "^FS" +
                "^FT600,147^A0N,23,24^FH\\^FD" + DateTime.Now.ToShortDateString() + "^FS" +
                "^FT612,128^XG000.GRF,1,1^FS" +
                "^BY2,3,123^FT265,322^BAN,,Y,N" +
                "^FD" + epc + "^FS" +
                "^FT14,31^A0N,23,24^FH\\^FDPedido:^FS" +
                "^FT12,70^A0N,23,24^FH\\^FD# Orden de Producci\\A2n:^FS" +
                "^FT14,114^A0N,23,24^FH\\^FDCliente:^FS" +
                "^FT14,145^A0N,23,24^FH\\^FD# Tarima:^FS" +
                "^FT16,217^A0N,23,24^FH\\^FDTipo:^FS" +
                "^FT14,250^A0N,23,24^FH\\^FDColor:^FS" +
                "^FT13,282^A0N,23,24^FH\\^FDC\\A2digo:^FS" +
                "^FT14,320^A0N,23,24^FH\\^FDMedida:^FS" +
                "^FT40,386^A0N,23,24^FH\\^FDPzas x Tarima:^FS" +
                "^FT412,384^A0N,23,24^FH\\^FDCantidad Pedido:^FS" +
                "^FT369,145^A0N,23,24^FH\\^FDFecha Entrada Almacen :^FS" +
                "^PQ1,0,1,Y^XZ" +
                "^XA^ID000.GRF^FS^XZ";
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                bool res = ws.asignaEscuadraVirtual(lblCodigo.Text,int.Parse(lblResistencia.Text));
                if (res)
                {
                    this.Dispose();
                    GC.Collect();
                    MessageBox.Show("Proceso Exitoso");
                    Cursor.Current = Cursors.Default;
                    frmMenu_Principal fmp = new frmMenu_Principal(user);
                    fmp.Show();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Hubo un problema con la asignación\n Por favor intente de nuevo");
                }
            }
            catch (Exception ee)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ee.Message);
                MessageBox.Show("Hubo un problema con la asignación\n Por favor intente de nuevo");
            }
        }

        private void Etiqueta_NoTarima_Load(object sender, EventArgs e)
        {

        }
    }
}