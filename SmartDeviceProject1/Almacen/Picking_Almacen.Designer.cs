namespace SmartDeviceProject1.Almacen
{
    partial class Picking_Almacen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Picking_Almacen));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mi_NoFiltro = new System.Windows.Forms.MenuItem();
            this.mi_Stock = new System.Windows.Forms.MenuItem();
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtRemision = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDetener = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnEsc = new System.Windows.Forms.Button();
            this.lblStock = new System.Windows.Forms.Label();
            this.rbStock = new System.Windows.Forms.RadioButton();
            this.lblPedido = new System.Windows.Forms.Label();
            this.rbPedido = new System.Windows.Forms.RadioButton();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "REGRESAR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.mi_NoFiltro);
            this.menuItem2.MenuItems.Add(this.mi_Stock);
            this.menuItem2.Text = "FILTRO";
            // 
            // mi_NoFiltro
            // 
            this.mi_NoFiltro.Checked = true;
            this.mi_NoFiltro.Text = "Escuadras";
            this.mi_NoFiltro.Click += new System.EventHandler(this.mi_NoFiltro_Click);
            // 
            // mi_Stock
            // 
            this.mi_Stock.Text = "Stock";
            this.mi_Stock.Click += new System.EventHandler(this.mi_Stock_Click);
            // 
            // btnLeer
            // 
            this.btnLeer.Enabled = false;
            this.btnLeer.Location = new System.Drawing.Point(83, 245);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(72, 20);
            this.btnLeer.TabIndex = 70;
            this.btnLeer.Text = "Leer";
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(163, 245);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(72, 20);
            this.btnFinalizar.TabIndex = 69;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Enabled = false;
            this.btnConectar.Location = new System.Drawing.Point(3, 245);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(72, 20);
            this.btnConectar.TabIndex = 68;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(148, 88);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(87, 21);
            this.btnBuscar.TabIndex = 66;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtRemision
            // 
            this.txtRemision.Location = new System.Drawing.Point(3, 88);
            this.txtRemision.Name = "txtRemision";
            this.txtRemision.Size = new System.Drawing.Size(139, 21);
            this.txtRemision.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 20);
            this.label1.Text = "Folio de Remisión";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(83, 219);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(72, 20);
            this.btnDetener.TabIndex = 73;
            this.btnDetener.Text = "Detener";
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(2, 115);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(233, 124);
            this.dataGrid1.TabIndex = 74;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.btnEsc);
            this.panel2.Controls.Add(this.lblStock);
            this.panel2.Controls.Add(this.rbStock);
            this.panel2.Controls.Add(this.lblPedido);
            this.panel2.Controls.Add(this.rbPedido);
            this.panel2.Controls.Add(this.lblHeader);
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(160, 178);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(85, 148);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 20);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Aceptar";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnEsc
            // 
            this.btnEsc.Location = new System.Drawing.Point(4, 148);
            this.btnEsc.Name = "btnEsc";
            this.btnEsc.Size = new System.Drawing.Size(72, 20);
            this.btnEsc.TabIndex = 5;
            this.btnEsc.Text = "Cancelar";
            this.btnEsc.Visible = false;
            this.btnEsc.Click += new System.EventHandler(this.btnEsc_Click);
            // 
            // lblStock
            // 
            this.lblStock.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblStock.Location = new System.Drawing.Point(35, 109);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(122, 33);
            this.lblStock.Text = "Pasar la Tarima a Stock";
            // 
            // rbStock
            // 
            this.rbStock.Location = new System.Drawing.Point(8, 114);
            this.rbStock.Name = "rbStock";
            this.rbStock.Size = new System.Drawing.Size(22, 20);
            this.rbStock.TabIndex = 3;
            this.rbStock.CheckedChanged += new System.EventHandler(this.rbStock_CheckedChanged);
            // 
            // lblPedido
            // 
            this.lblPedido.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPedido.Location = new System.Drawing.Point(35, 49);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(122, 48);
            this.lblPedido.Text = "Mantener la Tarima asociada al Pedido";
            // 
            // rbPedido
            // 
            this.rbPedido.Location = new System.Drawing.Point(7, 61);
            this.rbPedido.Name = "rbPedido";
            this.rbPedido.Size = new System.Drawing.Size(22, 21);
            this.rbPedido.TabIndex = 1;
            this.rbPedido.CheckedChanged += new System.EventHandler(this.rbPedido_CheckedChanged);
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(4, 4);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(153, 39);
            this.lblHeader.Text = "Selecciona la acción a ejecutar:";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(38, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(164, 183);
            this.panel1.Visible = false;
            // 
            // Picking_Almacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtRemision);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Picking_Almacen";
            this.Text = "Picking";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtRemision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem mi_NoFiltro;
        private System.Windows.Forms.MenuItem mi_Stock;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnEsc;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.RadioButton rbStock;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.RadioButton rbPedido;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panel1;
    }
}