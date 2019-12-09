namespace SmartDeviceProject1.Almacen
{
    partial class Nueva_Escuadra
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
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtCodPord = new System.Windows.Forms.TextBox();
            this.lblCodProd = new System.Windows.Forms.Label();
            this.txtPza = new System.Windows.Forms.TextBox();
            this.lblPza = new System.Windows.Forms.Label();
            this.lblRemision = new System.Windows.Forms.Label();
            this.txtRemision = new System.Windows.Forms.TextBox();
            this.lblUbi = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnDetener = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboArticulo = new System.Windows.Forms.ComboBox();
            this.btnSalir = new System.Windows.Forms.Button();
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
            this.menuItem2.Enabled = false;
            this.menuItem2.Text = "";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(4, 172);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(233, 34);
            this.dataGrid1.TabIndex = 95;
            this.dataGrid1.Visible = false;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // btnLeer
            // 
            this.btnLeer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeer.BackColor = System.Drawing.Color.White;
            this.btnLeer.Enabled = false;
            this.btnLeer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnLeer.Location = new System.Drawing.Point(86, 133);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(75, 22);
            this.btnLeer.TabIndex = 94;
            this.btnLeer.Text = "Leer";
            this.btnLeer.Visible = false;
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConectar.BackColor = System.Drawing.Color.White;
            this.btnConectar.Enabled = false;
            this.btnConectar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnConectar.Location = new System.Drawing.Point(4, 133);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(76, 22);
            this.btnConectar.TabIndex = 93;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Visible = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // txtCodPord
            // 
            this.txtCodPord.Enabled = false;
            this.txtCodPord.Location = new System.Drawing.Point(145, 113);
            this.txtCodPord.Name = "txtCodPord";
            this.txtCodPord.Size = new System.Drawing.Size(77, 21);
            this.txtCodPord.TabIndex = 96;
            this.txtCodPord.Visible = false;
            this.txtCodPord.TextChanged += new System.EventHandler(this.txtCodPord_TextChanged);
            // 
            // lblCodProd
            // 
            this.lblCodProd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodProd.Location = new System.Drawing.Point(42, 62);
            this.lblCodProd.Name = "lblCodProd";
            this.lblCodProd.Size = new System.Drawing.Size(151, 20);
            this.lblCodProd.Text = "Producto";
            this.lblCodProd.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtPza
            // 
            this.txtPza.Enabled = false;
            this.txtPza.Location = new System.Drawing.Point(145, 34);
            this.txtPza.Name = "txtPza";
            this.txtPza.Size = new System.Drawing.Size(75, 21);
            this.txtPza.TabIndex = 103;
            // 
            // lblPza
            // 
            this.lblPza.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPza.Location = new System.Drawing.Point(147, 11);
            this.lblPza.Name = "lblPza";
            this.lblPza.Size = new System.Drawing.Size(75, 20);
            this.lblPza.Text = "Piezas";
            this.lblPza.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblRemision
            // 
            this.lblRemision.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblRemision.Location = new System.Drawing.Point(3, 11);
            this.lblRemision.Name = "lblRemision";
            this.lblRemision.Size = new System.Drawing.Size(119, 20);
            this.lblRemision.Text = "Remisión";
            this.lblRemision.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtRemision
            // 
            this.txtRemision.Location = new System.Drawing.Point(3, 34);
            this.txtRemision.Name = "txtRemision";
            this.txtRemision.Size = new System.Drawing.Size(119, 21);
            this.txtRemision.TabIndex = 98;
            this.txtRemision.TextChanged += new System.EventHandler(this.txtRemision_TextChanged);
            this.txtRemision.LostFocus += new System.EventHandler(this.txtRemision_LostFocus);
            // 
            // lblUbi
            // 
            this.lblUbi.Enabled = false;
            this.lblUbi.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblUbi.Location = new System.Drawing.Point(4, 110);
            this.lblUbi.Name = "lblUbi";
            this.lblUbi.Size = new System.Drawing.Size(118, 20);
            this.lblUbi.Text = "Ubicación";
            this.lblUbi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblUbi.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.Location = new System.Drawing.Point(86, 116);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(118, 22);
            this.comboBox1.TabIndex = 101;
            this.comboBox1.Visible = false;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnDetener
            // 
            this.btnDetener.BackColor = System.Drawing.Color.White;
            this.btnDetener.Enabled = false;
            this.btnDetener.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnDetener.Location = new System.Drawing.Point(167, 133);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(69, 22);
            this.btnDetener.TabIndex = 108;
            this.btnDetener.Text = "Detener";
            this.btnDetener.Visible = false;
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 113);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(117, 21);
            this.textBox1.TabIndex = 113;
            this.textBox1.Visible = false;
            // 
            // comboArticulo
            // 
            this.comboArticulo.Location = new System.Drawing.Point(42, 85);
            this.comboArticulo.Name = "comboArticulo";
            this.comboArticulo.Size = new System.Drawing.Size(151, 22);
            this.comboArticulo.TabIndex = 101;
            this.comboArticulo.SelectedIndexChanged += new System.EventHandler(this.comboArticulo_SelectedIndexChanged);
            // 
            // btnSalir
            // 
            this.btnSalir.Enabled = false;
            this.btnSalir.Location = new System.Drawing.Point(4, 231);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(72, 20);
            this.btnSalir.TabIndex = 118;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Visible = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // Nueva_Escuadra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.comboArticulo);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblUbi);
            this.Controls.Add(this.txtRemision);
            this.Controls.Add(this.lblRemision);
            this.Controls.Add(this.lblPza);
            this.Controls.Add(this.txtPza);
            this.Controls.Add(this.lblCodProd);
            this.Controls.Add(this.txtCodPord);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnConectar);
            this.Menu = this.mainMenu1;
            this.Name = "Nueva_Escuadra";
            this.Text = "Nueva Escuadra";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox txtCodPord;
        private System.Windows.Forms.Label lblCodProd;
        private System.Windows.Forms.TextBox txtPza;
        private System.Windows.Forms.Label lblPza;
        private System.Windows.Forms.Label lblRemision;
        private System.Windows.Forms.TextBox txtRemision;
        private System.Windows.Forms.Label lblUbi;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboArticulo;
        private System.Windows.Forms.Button btnSalir;
    }
}