using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Threading;
using ZebraBluetoothAdapter;
using ZSDK_API;
using ZSDK_API.ApiException;
using ZSDK_API.Comm;
using ZSDK_API.Comm.Internal;
using ZSDK_API.Discovery;
using ZSDK_API.Discovery.Internal;
using ZSDK_API.Graphics;
using ZSDK_API.Graphics.Internal;
using ZSDK_API.Printer;
using ZSDK_API.Printer.Internal;
using ZSDK_API.Sgd;
using ZSDK_API.Util;
using ZSDK_API.Util.Internal;
using System.IO;
using System.Runtime.InteropServices;

namespace SmartDeviceProject1.Almacen
{
    public partial class Impresion_Etiqueta : Form
    {
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();
        //SmartDeviceProject1.NapresaSitio.Service1 ws = new SmartDeviceProject1.NapresaSitio.Service1();
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();

        cMetodos ws = new cMetodos();

        string[] folio = new string[17];
        string tag = "";
        int ct = 0;
        String macAdd;
        string[] user;
        String zpl = null;

        public string EPC;
        public int CantidadTarima;
        public string[] Detalle;

        int total = 0;

        string newid;
        int pedido = 0;


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

        public Impresion_Etiqueta(string[] detalle, int cantidadTarima, string epc, string[] usuario, string newIdEsc)
        {
            InitializeComponent();

            Detalle = detalle;
            CantidadTarima = cantidadTarima;
            EPC = epc;
            newid = newIdEsc;

            Cursor.Current = Cursors.Default;
            folio = detalle;
            //lblEPC.Text = epc;
            tag = epc;
            user = usuario;
            ct = cantidadTarima;
            //pedido = int.Parse(detalle[9]);//cantidad total o parcial de la OP
            pedido = ws.CalculaPedidoParcialidadImprimir(newid);
            lblPedido.Text = detalle[2];
            lblOrdProd.Text = detalle[1];
            lblCliente.Text = detalle[3];
            int current = ws.TarimasImpresas(detalle[1],detalle[8]);
            total = ws.calculaTarima(detalle[8], pedido);
            lblTarima.Text = current + " de " + total;//TARIMA 1 DE N
            lbTipo.Text = detalle[5];
            lblMedida.Text = detalle[6];
            lblColor.Text = detalle[7];
            lblResistencia.Text = cantidadTarima + "";
            lblCodigo.Text = detalle[8];
            lblCantidad.Text = detalle[9];//CANTIDAD TARIMA 
            label5.Text = detalle[4];
            string colaimpresion = ws.guardarColaImpresion(lblPedido.Text, lblOrdProd.Text, lblCliente.Text, folio[4], int.Parse(lblCantidad.Text), ct, lblMedida.Text, lblCodigo.Text, lblColor.Text, lbTipo.Text, tag, total);
            zpl = "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR8,8~SD15^JUS^LRN^CI0^XZ" +
                "~DG000.GRF,05120,040," +
                ",:::::::::::::::::J040T040P040,,::::::R040L040H040404040H040,,::::J01DFFDFDFDD0O017C17D05041FC07D,J02ANAQ0A80AA020H0HA0AA,J07F7H7F7H7F545H5454007471C7C7445C714740,J02ANAP0202082820228208280,J01DNDM1I07001C185041851C180,J02ANAP020H082820H08208080,J0P705454544007001D707041D71C5C0,J02ANAP020H0HA06022EE08280,J01DNDM1I071D1DD05041DC18180,J02ANAP020A082820H08008080,J07F7L7F5L54007071C5C7445C01C5C0,J02ANAP0202080820A28008280,J01DNDM1I07071C185041801C1,J02ANAP028A0808208080082,J0P705454544005771C1C77414017F,J02ANAQ0AE08082A80I06E,J01DNDM1J050J015010H010,L02AKA8,J0404040404040404040H040L040N04040H04040404040P040,,K0O105FDFDFD,U0MA,K0545H545H547L7I0407C40H07F40H0405C0H04004007F0I017C40H05F4,U0MAH02E0EA0H02AHAI0E02A0H0A02802AA80H06AA0I0HAE,K0P15DKDH0755DDC015DD70015DHDC01D85C055DD001DHDC005DD580,U0MAH02AIA800AIA800AIA800A8A80AIA802AIAH02AHA80,K0P5M7407F7F7405FF77C057I74017DFC57I74077FF701FF7760,U0MAH02AJA02AA2A802AJAH0IA82AIA802AIAH0HA2AA0,K0P1MDH05D755D07D81DC01DD1DD01DHD83DD15C05D15D815C15D0,U0MAH02A80AA02A00A800A80AA00AHA82A80A80AA02A80A802A0,K054545454547L7H0H7C1F707F0074017C4770177F4770176077417C1740770,U0MAH02A80AA02A00AA02A80AA00AA806A00AA02A00A80A806A8,K0P15DKDH05D01D505D01DC01DC05D01D7005D005D05D01F81F005D8,U0MAH02A00AA0J0A800A80AA00A8002A00AA02A0M0HA8,J045N5477F7H7F007F007F0040774057C07F417C40774176477C0I0405F78,U0MAH02A00AA0I0IA02A802A00A8006AJA02AA0K02AA8,K0O105DKDH05D01DD0H01DDC01DC05D01DC005DJD015D0J01DHD8,U02AJA8002A00AA0H0JAH0A80AA00A8002AJAH0HA80I02AHA8,O040L04040I0H7H0F7007I74017C07701F4007J7601F7F0H017F778,gJ02A00AA002AIA02A80AA00A8006AJAH02AA0H02AIA8,J01DND01K1I05D01DD01DD95C01D805D01DC005DDFDD001D5C0055D5D8,J02ANAP02A00AA00AA0A800A80AA00A8002A80K0IAH0HA82A8,J0P715K54007F0077077C5F4057C077017C00770040I0H7F01F7477840,J02ANAP02A00AA02A80AA02A802A00A8006A0L02AA00A802A8,J01DNDM1I05D01DD05D01DC01DC05D01DC005D0L01DD81DC05D8,J02ANAP02A00AA02A00AA00A80AA00A8002A0M02A82A802A8,J0P705454544007700F7077017C017C07701740077007E07C077C77407780554,J02ANAP02A00AA06A00AA02A80AA00A8002A00AA0AA02A82A802A8,J015DLD51L1I05D01DD05D01DC01DC05D01DC005D015C05D015C3DC05D80144,J02ANAP02A00AA02A80A800A80AA00A8002A80AA0AA02A82A80AA80022,J0O7F15I545400774077077C77405771FF41FC407FC17C47707FC77C57781151,J02ANAP02A00AA02AA2AA02AJAH0A8002AA2A802A8AA80AA2AA800A0,J01DNDM1I05D01DD05DIDC01DJD01DC001DIDC07DFDD81DHD5D81171,J02ANAP02A00AA02AJAH0JA800A80H0JA802AIAH0KA80020,J0P705H54544007700F705F7H74017FF7C05FC0H0J7405F7H7017J780414,J02ANAP02A006A00AA82A02A8AA800A80H02AHAI0IA8002AA2A8,J01FDFDFDFHF01K1I0150014007F01001D87D00110I017FC0H01FF0H01F81F00310,gY0A80gP080,L040H040L040X057C040P040,gX02A80,gX01D80,gY0A80,gX017C0,gX02A80,gX01D80,gY0A80,gX05F40,gY0A,,:::::::::::::::::::::::::::^XA" +
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
                "^FT437,147^A0N,23,24^FH\\^FD" + DateTime.Now.ToShortDateString() + "^FS" +
                "^FT384,128^XG000.GRF,1,1^FS" +
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
                "^FT40,386^A0N,23,24^FH\\^FDCantidad Tarima:^FS" +
                "^FT412,384^A0N,23,24^FH\\^FDCantidad Pedido:^FS" +
                "^FT369,145^A0N,23,24^FH\\^FDFecha:^FS" +
                "^PQ1,0,1,Y^XZ" +
                "^XA^ID000.GRF^FS^XZ";
            string zplAnterior = "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR8,8~SD15^JUS^LRN^CI0^XZ"+
                "~DG000.GRF,05120,040,"+
                ",:::::::::::::::J020T020P020,,::::::R020L020H020202020H020,,::::K0EFFEFEFEE80O0BE0BE82820FE03E80,J015N5Q054055010H0H5055,J03FBHBFBHBFAA2AHA2A003A38E3E3A22E38A3A0,J015N5P0101041410114104140,K0OEN8H03800E0C2820C28E0C0,J015N5P010H041410H04104040,J03BNB82A2A2A2003800EB83820EB8E2E0,J015N5P010H0H5030117704140,K0OEN8H038E8EE82820EE0C0C0,J015N5P0105041410H04004040,J03FBLBFAMAH03838E2E3A22E00E2E0,J015N5P0101040410514004140,K0OEN8H03838E0C2820C00E080,J015N5P01450404104040041,J03BNB82A2A2A2002BB8E0E3BA0A00BF80,J015N5Q05704041540I037,K0OEN8I0280J0A8080H08,L015K54,J0202020202020202020H020L020N02020H02020202020P020,,L0O82FEFEFE80,U0M5,K02A2AHA2AHA3BKB800203E20H03FA0H0202E0H02002003F80I0BE20H02FA,U0M5H0170750H015H5I070150H05014015540H03550I0H57,L0O8AEKE803AAEHEH0AEEB800AEIEH0EC2E02AEE800EIEH02EEAC0,U0M5H015I54005I54005I5400545405I54015I5H015H540,K02ANAMBA03FBFBA02FFBBE02BIBA00BEFE2BIBA03BFFB80FFBHB0,U0M5H015J50155154015J5H0I5415I54015I5H0H51550,L0O8ME802EBAAE83EC0EE00EE8EE80EHEC1EE8AE02E8AEC0AE0AE8,U0M5H015405501500540054055005H5415405405501540540150,K02A2A2A2A2A3BKB803BE0FB83F803A00BE23B80BBFA3B80BB03BA0BE0BA03B8,U0M5H01540550150055015405500554035005501500540540354,L0O8AEKE802E80EA82E80EE00EE02E80EB802E802E82E80FC0F802EC,U0M5H01500550J05400540550054001500550150M0H54,J0H2OA3BFBHBF803F803F80203BA02BE03FA0BE203BA0BB23BE0I0202FBC,U0M5H01500550I0I5015401500540035J501550K01554,L0O82EKE802E80EE80H0IEH0HE02E80EE002EJE80AE80J0IEC,U015J54001500550H0J5H05405500540015J5H0H540I015H54,O020L02020I03B807B803BHBA00BE03B80FA003BJBH0FBF80H0BFBBC,gJ01500550015I5015405500540035J5H01550H015I54,K0OE808K8H02E80EE80EECAE00EC02E80EE002EEFEE800EAE002AEAEC,J015N5P0150055005505400540550054001540K0I5H0H54154,J03BNB8ALAH03F803B83BE2FA02BE03B80BE003B8020I03BF80FBA3BC20,J015N5P015005501540550154015005400350L015500540154,K0OEN8H02E80EE82E80EE00EE02E80EE002E80L0HEC0EE02EC,J015N5P015005501500550054055005400150M01541540154,J03BNB82A2A2A2003B807B83B80BE00BE03B80BA003B803F03E03BE3BA03BC02AA,J015N5P01500550350055015405500540015005505501541540154,K0AELEA8M8H02E80EE82E80EE00EE02E80EE002E80AE02E80AE1EE02EC00A2,J015N5P015005501540540054055005400154055055015415405540011,J03BMBF8AJA2A003BA03B83BE3BA02BB8FFA0FE203FE0BE23B83FE3BE2BBC08A880,J015N5P01500550155155015J5H05400155154015455405515540050,K0OEN8H02E80EE82EJEH0KE80EE0H0KE03EFEEC0EHEAEC08B880,J015N5P0150055015J5H0J5400540H0J54015I5H0K540010,J03BNB82AHA2A2003B807B82FBHBA00BFFBE02FE0H03BHBA02FBHB80BJBC020A,J015N5P01500350055415015455400540H015H5I0I5400155154,K0FEFEFEFHF808K8I0A800A003F80800EC3E800880I0BFE0I0HF80H0FC0F80188,gY0540gP040,L020H020L020X02BE020P020,gX01540,gY0EC0,gY0540,gY0BE0,gX01540,gY0EC0,gY0540,gX02FA0,gY05,,:::::::::::::::::::::::::::::^XA"+
                "^MMT"+
                "^PW812"+
                "^LL0406"+
                "^LS0"+
                "^FT89,29^A0N,23,24^FH\\^FD"+lblPedido.Text+"^FS"+
                "^FT250,70^A0N,23,24^FH\\^FD"+lblOrdProd.Text+"^FS"+
                "^FT92,116^A0N,23,24^FH\\^FD"+lblCliente.Text+"^FS"+
                "^FT109,148^A0N,23,24^FH\\^FD"+lblTarima.Text+"^FS"+
                "^FT44,178^A0N,23,24^FH\\^FD"+detalle[4]+"^FS"+
                "^FT579,383^A0N,23,24^FH\\^FD"+lblCantidad.Text+"^FS"+
                "^FT204,385^A0N,23,24^FH\\^FD"+cantidadTarima+"^FS"+
                "^FT93,320^A0N,23,24^FH\\^FD"+lblMedida.Text+"^FS"+
                "^FT90,282^A0N,23,24^FH\\^FD"+lblCodigo.Text+"^FS"+
                "^FT74,249^A0N,23,24^FH\\^FD"+lblColor.Text+"^FS"+
                "^FT68,217^A0N,23,24^FH\\^FD"+lbTipo.Text+"^FS"+
                "^FT600,147^A0N,23,24^FH\\^FD"+DateTime.Now.ToShortDateString()+"^FS"+
                "^FT612,128^XG000.GRF,1,1^FS"+
                "^BY2,3,123^FT265,322^BAN,,Y,N"+
                "^FD"+epc+"^FS"+
                "^FT14,31^A0N,23,24^FH\\^FDPedido:^FS"+
                "^FT12,70^A0N,23,24^FH\\^FD# Orden de Producci\\A2n:^FS"+
                "^FT14,114^A0N,23,24^FH\\^FDCliente:^FS"+
                "^FT14,145^A0N,23,24^FH\\^FD# Tarima:^FS"+
                "^FT16,217^A0N,23,24^FH\\^FDTipo:^FS"+
                "^FT14,250^A0N,23,24^FH\\^FDColor:^FS"+
                "^FT13,282^A0N,23,24^FH\\^FDC\\A2digo:^FS"+
                "^FT14,320^A0N,23,24^FH\\^FDMedida:^FS"+
                "^FT40,386^A0N,23,24^FH\\^FDPzas x Tarima:^FS"+
                "^FT412,384^A0N,23,24^FH\\^FDCantidad Pedido:^FS"+
                "^FT369,145^A0N,23,24^FH\\^FDFecha Entrada Almacen :^FS"+
                "^PQ1,0,1,Y^XZ"+
                "^XA^ID000.GRF^FS^XZ";
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
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

        public void actualizarZpl()
        {
            zpl = "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR8,8~SD15^JUS^LRN^CI0^XZ" +
                "~DG000.GRF,05120,040," +
                ",:::::::::::::::::J040T040P040,,::::::R040L040H040404040H040,,::::J01DFFDFDFDD0O017C17D05041FC07D,J02ANAQ0A80AA020H0HA0AA,J07F7H7F7H7F545H5454007471C7C7445C714740,J02ANAP0202082820228208280,J01DNDM1I07001C185041851C180,J02ANAP020H082820H08208080,J0P705454544007001D707041D71C5C0,J02ANAP020H0HA06022EE08280,J01DNDM1I071D1DD05041DC18180,J02ANAP020A082820H08008080,J07F7L7F5L54007071C5C7445C01C5C0,J02ANAP0202080820A28008280,J01DNDM1I07071C185041801C1,J02ANAP028A0808208080082,J0P705454544005771C1C77414017F,J02ANAQ0AE08082A80I06E,J01DNDM1J050J015010H010,L02AKA8,J0404040404040404040H040L040N04040H04040404040P040,,K0O105FDFDFD,U0MA,K0545H545H547L7I0407C40H07F40H0405C0H04004007F0I017C40H05F4,U0MAH02E0EA0H02AHAI0E02A0H0A02802AA80H06AA0I0HAE,K0P15DKDH0755DDC015DD70015DHDC01D85C055DD001DHDC005DD580,U0MAH02AIA800AIA800AIA800A8A80AIA802AIAH02AHA80,K0P5M7407F7F7405FF77C057I74017DFC57I74077FF701FF7760,U0MAH02AJA02AA2A802AJAH0IA82AIA802AIAH0HA2AA0,K0P1MDH05D755D07D81DC01DD1DD01DHD83DD15C05D15D815C15D0,U0MAH02A80AA02A00A800A80AA00AHA82A80A80AA02A80A802A0,K054545454547L7H0H7C1F707F0074017C4770177F4770176077417C1740770,U0MAH02A80AA02A00AA02A80AA00AA806A00AA02A00A80A806A8,K0P15DKDH05D01D505D01DC01DC05D01D7005D005D05D01F81F005D8,U0MAH02A00AA0J0A800A80AA00A8002A00AA02A0M0HA8,J045N5477F7H7F007F007F0040774057C07F417C40774176477C0I0405F78,U0MAH02A00AA0I0IA02A802A00A8006AJA02AA0K02AA8,K0O105DKDH05D01DD0H01DDC01DC05D01DC005DJD015D0J01DHD8,U02AJA8002A00AA0H0JAH0A80AA00A8002AJAH0HA80I02AHA8,O040L04040I0H7H0F7007I74017C07701F4007J7601F7F0H017F778,gJ02A00AA002AIA02A80AA00A8006AJAH02AA0H02AIA8,J01DND01K1I05D01DD01DD95C01D805D01DC005DDFDD001D5C0055D5D8,J02ANAP02A00AA00AA0A800A80AA00A8002A80K0IAH0HA82A8,J0P715K54007F0077077C5F4057C077017C00770040I0H7F01F7477840,J02ANAP02A00AA02A80AA02A802A00A8006A0L02AA00A802A8,J01DNDM1I05D01DD05D01DC01DC05D01DC005D0L01DD81DC05D8,J02ANAP02A00AA02A00AA00A80AA00A8002A0M02A82A802A8,J0P705454544007700F7077017C017C07701740077007E07C077C77407780554,J02ANAP02A00AA06A00AA02A80AA00A8002A00AA0AA02A82A802A8,J015DLD51L1I05D01DD05D01DC01DC05D01DC005D015C05D015C3DC05D80144,J02ANAP02A00AA02A80A800A80AA00A8002A80AA0AA02A82A80AA80022,J0O7F15I545400774077077C77405771FF41FC407FC17C47707FC77C57781151,J02ANAP02A00AA02AA2AA02AJAH0A8002AA2A802A8AA80AA2AA800A0,J01DNDM1I05D01DD05DIDC01DJD01DC001DIDC07DFDD81DHD5D81171,J02ANAP02A00AA02AJAH0JA800A80H0JA802AIAH0KA80020,J0P705H54544007700F705F7H74017FF7C05FC0H0J7405F7H7017J780414,J02ANAP02A006A00AA82A02A8AA800A80H02AHAI0IA8002AA2A8,J01FDFDFDFHF01K1I0150014007F01001D87D00110I017FC0H01FF0H01F81F00310,gY0A80gP080,L040H040L040X057C040P040,gX02A80,gX01D80,gY0A80,gX017C0,gX02A80,gX01D80,gY0A80,gX05F40,gY0A,,:::::::::::::::::::::::::::^XA" +
                "^MMT" +
                "^PW812" +
                "^LL0406" +
                "^LS0" +
                "^FT89,29^A0N,23,24^FH\\^FD" + lblPedido.Text + "^FS" +//pedido
                "^FT250,70^A0N,23,24^FH\\^FD" + lblOrdProd.Text + "^FS" +//OP
                "^FT92,116^A0N,23,24^FH\\^FD" + lblCliente.Text + "^FS" +//Cliente
                "^FT109,148^A0N,23,24^FH\\^FD" + lblTarima.Text + "^FS" +//Tarima
                "^FT44,178^A0N,23,24^FH\\^FD" + Detalle[4] + "^FS" +//Tipo
                "^FT579,383^A0N,23,24^FH\\^FD" + CantidadTarima + "^FS" +//total de pedido 
                "^FT204,385^A0N,23,24^FH\\^FD" + lblCantidad.Text + "^FS" +//cantidad en tarima
                "^FT93,320^A0N,23,24^FH\\^FD" + lblMedida.Text + "^FS" +
                "^FT90,282^A0N,23,24^FH\\^FD" + lblCodigo.Text + "^FS" +
                "^FT74,249^A0N,23,24^FH\\^FD" + lblColor.Text + "^FS" +
                "^FT68,217^A0N,23,24^FH\\^FD" + lbTipo.Text + "^FS" +
                "^FT437,147^A0N,23,24^FH\\^FD" + DateTime.Now.ToShortDateString() + "^FS" +
                "^FT384,128^XG000.GRF,1,1^FS" +
                "^BY2,3,123^FT265,322^BAN,,Y,N" +
                "^FD" + EPC + "^FS" +
                "^FT14,31^A0N,23,24^FH\\^FDPedido:^FS" +
                "^FT12,70^A0N,23,24^FH\\^FD# Orden de Producci\\A2n:^FS" +
                "^FT14,114^A0N,23,24^FH\\^FDCliente:^FS" +
                "^FT14,145^A0N,23,24^FH\\^FD# Tarima:^FS" +
                "^FT16,217^A0N,23,24^FH\\^FDTipo:^FS" +
                "^FT14,250^A0N,23,24^FH\\^FDColor:^FS" +
                "^FT13,282^A0N,23,24^FH\\^FDC\\A2digo:^FS" +
                "^FT14,320^A0N,23,24^FH\\^FDMedida:^FS" +
                "^FT40,386^A0N,23,24^FH\\^FDCantidad Tarima:^FS" +
                "^FT412,384^A0N,23,24^FH\\^FDCantidad Pedido:^FS" +
                "^FT369,145^A0N,23,24^FH\\^FDFecha:^FS" +
                "^PQ1,0,1,Y^XZ" +
                "^XA^ID000.GRF^FS^XZ";
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            int current = 1;
            try
            {
                conectarAccion();
                obtenMacAdress();
                DataTable colaImpresion = ws.cargarColaImpresion(lblPedido.Text, lblOrdProd.Text, EPC);

                foreach (DataRow impresion in colaImpresion.Rows)
                {
                    lblTarima.Text = current.ToString() + " de " + total;
                    //cálculo de piezas por tarima
                    lblCantidad.Text = impresion["CantidadTarima"].ToString();
                    actualizarZpl();
                    printLabel(zpl);
                    current++;
                }
                
              
               
                
                //LiberarControles(this);
                this.Dispose();
                GC.Collect();
                MessageBox.Show("Impresión de Etiquetas:\nExitosa");
     
                Cursor.Current = Cursors.Default;
                frmMenu_Principal fmp = new frmMenu_Principal(user);
                fmp.Show();
            
            }
            catch (Exception ee)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ee.Message);
                MessageBox.Show("Hubo un problema con la impresora\n Por favor intente de nuevo");
            }
        }

        public void obtenMacAdress()
        {
            StreamReader objReader = new StreamReader("\\My Documents\\config.txt");
            string sLine = "";
            string[] sLine2;
            sLine = objReader.ReadLine();
            while (sLine != null)
            {
                sLine2 = null;
                sLine2 = sLine.Split('=');
                if (sLine2[0].Equals("printer"))
                    macAdd = sLine2[1];
                sLine = objReader.ReadLine();
            }
            objReader.Close();
        }

        public void printLabel(string zpl)
        {
            try
            {
                ZebraPrinterConnection thePrinterConn = new BluetoothPrinterConnection(macAdd);
                thePrinterConn.Open();
                String zplData = zpl;
                thePrinterConn.Write(Encoding.Default.GetBytes(zplData));
                Thread.Sleep(500);
                thePrinterConn.Close();
            }
            catch (ZebraException ex)
            {
                MessageBox.Show(ex.Message, "ZEBRA EXCEPTION");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EXCEPTION");
            }
        }

        public void conectarAccion()
        {
            StreamWriter writer = new StreamWriter("\\My Documents\\config.txt");
            writer.WriteLine("ftp=NULL");
            writer.WriteLine("usuario=NULL");
            writer.WriteLine("password=NULL");
            writer.WriteLine("printer=00225832FBCE");
            writer.Close();
            obtenMacAdress();
            validaConexion();
        }

        public void validaConexion()
        {
            try
            {
                ZebraPrinterConnection thePrinterConn = new BluetoothPrinterConnection(macAdd);
                thePrinterConn.Open();
                MessageBox.Show("Conexion exitosa, impresora lista para imprimir.", "IMPRESORA CONECTADA");
                thePrinterConn.Close();
            }
            catch (ZebraException ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("1-Favor de revisar el valor de MacAdress de la impresora.\n2-Conectar la impresora al dispositivo.\n3-Validar la conexion inalambrica con el dispositivo. ", "ERROR DE CONEXION");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("1-Favor de revisar el valor de MacAdress de la impresora.\n2-Conectar la impresora al dispositivo.\n3-Validar la conexion inalambrica con el dispositivo. ", "ERROR DE CONEXION");
            }
        }

        private void Impresion_Etiqueta_Closing(object sender, CancelEventArgs e)
        {
            //LiberarControles(this);
            this.Dispose();
            GC.Collect();
            frmMenu_Almacen fma = new frmMenu_Almacen(user);
            fma.Show();
        }

    }
}