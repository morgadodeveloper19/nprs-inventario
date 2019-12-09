namespace SmartDeviceProject1.Almacen
{
    partial class Reportar_Mermas
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.bntBuscaNva = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblOP = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPedido = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtEPC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReportar = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnDetener = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lbPzas = new System.Windows.Forms.Label();
            this.cbMerma = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "SALIR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Enabled = false;
            this.menuItem2.Text = " ";
            // 
            // bntBuscaNva
            // 
            this.bntBuscaNva.Location = new System.Drawing.Point(130, 55);
            this.bntBuscaNva.Name = "bntBuscaNva";
            this.bntBuscaNva.Size = new System.Drawing.Size(105, 20);
            this.bntBuscaNva.TabIndex = 47;
            this.bntBuscaNva.Text = "Volver a Buscar";
            this.bntBuscaNva.Click += new System.EventHandler(this.bntBuscaNva_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(78, 243);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(75, 21);
            this.txtCantidad.TabIndex = 46;
            this.txtCantidad.Visible = false;
            this.txtCantidad.TextChanged += new System.EventHandler(this.txtCantidad_TextChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(4, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 20);
            this.label5.Text = "Merma: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label5.Visible = false;
            // 
            // lblProducto
            // 
            this.lblProducto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblProducto.Location = new System.Drawing.Point(5, 164);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(213, 52);
            this.lblProducto.Text = "Producto";
            this.lblProducto.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblProducto.Visible = false;
            // 
            // lblCliente
            // 
            this.lblCliente.Location = new System.Drawing.Point(148, 109);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(87, 20);
            this.lblCliente.Text = "Cliente";
            this.lblCliente.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(109, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 20);
            this.label4.Text = "Color:";
            this.label4.Visible = false;
            // 
            // lblOP
            // 
            this.lblOP.Location = new System.Drawing.Point(86, 144);
            this.lblOP.Name = "lblOP";
            this.lblOP.Size = new System.Drawing.Size(58, 20);
            this.lblOP.Text = "OP";
            this.lblOP.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(4, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 35);
            this.label3.Text = "Orden de Producción:";
            this.label3.Visible = false;
            // 
            // lblPedido
            // 
            this.lblPedido.Location = new System.Drawing.Point(47, 109);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(70, 20);
            this.lblPedido.Text = "Pedido";
            this.lblPedido.Visible = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.Text = "Tipo:";
            this.label2.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(0, 55);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(105, 20);
            this.btnBuscar.TabIndex = 45;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtEPC
            // 
            this.txtEPC.Location = new System.Drawing.Point(47, 32);
            this.txtEPC.Name = "txtEPC";
            this.txtEPC.Size = new System.Drawing.Size(178, 21);
            this.txtEPC.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.Text = "TAG:";
            // 
            // btnReportar
            // 
            this.btnReportar.Enabled = false;
            this.btnReportar.Location = new System.Drawing.Point(159, 243);
            this.btnReportar.Name = "btnReportar";
            this.btnReportar.Size = new System.Drawing.Size(80, 20);
            this.btnReportar.TabIndex = 57;
            this.btnReportar.Text = "Reportar";
            this.btnReportar.Visible = false;
            this.btnReportar.Click += new System.EventHandler(this.btnReportar_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(164, 5);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(0, 0);
            this.btnFinalizar.TabIndex = 70;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnLeer
            // 
            this.btnLeer.Enabled = false;
            this.btnLeer.Location = new System.Drawing.Point(86, 5);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(72, 20);
            this.btnLeer.TabIndex = 69;
            this.btnLeer.Text = "Leer";
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(164, 5);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(0, 0);
            this.btnDetener.TabIndex = 68;
            this.btnDetener.Text = "Detener";
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(5, 5);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(72, 20);
            this.btnConectar.TabIndex = 67;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // label6
            // 
            this.label6.Enabled = false;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(5, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 20);
            this.label6.Text = "Piezas:";
            this.label6.Visible = false;
            // 
            // lbPzas
            // 
            this.lbPzas.Location = new System.Drawing.Point(64, 220);
            this.lbPzas.Name = "lbPzas";
            this.lbPzas.Size = new System.Drawing.Size(100, 20);
            // 
            // cbMerma
            // 
            this.cbMerma.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbMerma.DisplayMember = "sdsfs";
            this.cbMerma.Enabled = false;
            this.cbMerma.Location = new System.Drawing.Point(19, 82);
            this.cbMerma.Name = "cbMerma";
            this.cbMerma.Size = new System.Drawing.Size(199, 22);
            this.cbMerma.TabIndex = 91;
            this.cbMerma.ValueMember = "sdsfs";
            this.cbMerma.Visible = false;
            // 
            // Reportar_Mermas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.cbMerma);
            this.Controls.Add(this.lbPzas);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.btnReportar);
            this.Controls.Add(this.bntBuscaNva);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblOP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPedido);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtEPC);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Reportar_Mermas";
            this.Text = "Reportar Mermas";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bntBuscaNva;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblOP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtEPC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReportar;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbPzas;
        private System.Windows.Forms.ComboBox cbMerma;
    }
}