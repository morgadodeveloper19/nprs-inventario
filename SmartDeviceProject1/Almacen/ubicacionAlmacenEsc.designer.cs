namespace SmartDeviceProject1
{
    partial class ubicacionAlmacenEsc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ubicacionAlmacenEsc));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cbAlmacen = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbZonas = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRacks = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPosiciones = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSucursales = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.ckbRack = new System.Windows.Forms.CheckBox();
            this.ckbPosicion = new System.Windows.Forms.CheckBox();
            this.ckbZona = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnDetener = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(165, 206);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 24);
            this.button2.TabIndex = 38;
            this.button2.Text = "Cancelar";
            this.button2.Visible = false;
            // 
            // cbAlmacen
            // 
            this.cbAlmacen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAlmacen.Enabled = false;
            this.cbAlmacen.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.cbAlmacen.Location = new System.Drawing.Point(15, 85);
            this.cbAlmacen.Name = "cbAlmacen";
            this.cbAlmacen.Size = new System.Drawing.Size(211, 24);
            this.cbAlmacen.TabIndex = 33;
            this.cbAlmacen.SelectedIndexChanged += new System.EventHandler(this.cbAlmacen_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(89, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.Text = "ALMACEN";
            // 
            // cbZonas
            // 
            this.cbZonas.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.cbZonas.Location = new System.Drawing.Point(89, 122);
            this.cbZonas.Name = "cbZonas";
            this.cbZonas.Size = new System.Drawing.Size(137, 24);
            this.cbZonas.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(39, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.Text = "ZONA";
            // 
            // cbRacks
            // 
            this.cbRacks.Enabled = false;
            this.cbRacks.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.cbRacks.Location = new System.Drawing.Point(150, 145);
            this.cbRacks.Name = "cbRacks";
            this.cbRacks.Size = new System.Drawing.Size(44, 24);
            this.cbRacks.TabIndex = 35;
            this.cbRacks.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(39, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.Text = "POSICION";
            this.label3.Visible = false;
            // 
            // cbPosiciones
            // 
            this.cbPosiciones.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.cbPosiciones.Location = new System.Drawing.Point(114, 173);
            this.cbPosiciones.Name = "cbPosiciones";
            this.cbPosiciones.Size = new System.Drawing.Size(118, 24);
            this.cbPosiciones.TabIndex = 36;
            this.cbPosiciones.Visible = false;
            // 
            // label5
            // 
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(39, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.Text = "RACK";
            this.label5.Visible = false;
            // 
            // cbSucursales
            // 
            this.cbSucursales.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.cbSucursales.Location = new System.Drawing.Point(15, 35);
            this.cbSucursales.Name = "cbSucursales";
            this.cbSucursales.Size = new System.Drawing.Size(211, 24);
            this.cbSucursales.TabIndex = 39;
            this.cbSucursales.SelectedIndexChanged += new System.EventHandler(this.cbSucursales_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(26, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 24);
            this.button1.TabIndex = 37;
            this.button1.Text = "Siguiente";
            this.button1.Visible = false;
            // 
            // label
            // 
            this.label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label.Location = new System.Drawing.Point(87, 12);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(81, 20);
            this.label.Text = "SUCURSAL";
            // 
            // ckbRack
            // 
            this.ckbRack.Enabled = false;
            this.ckbRack.Location = new System.Drawing.Point(10, 147);
            this.ckbRack.Name = "ckbRack";
            this.ckbRack.Size = new System.Drawing.Size(21, 20);
            this.ckbRack.TabIndex = 45;
            this.ckbRack.Visible = false;
            this.ckbRack.CheckStateChanged += new System.EventHandler(this.ckbRack_CheckStateChanged);
            // 
            // ckbPosicion
            // 
            this.ckbPosicion.Enabled = false;
            this.ckbPosicion.Location = new System.Drawing.Point(10, 173);
            this.ckbPosicion.Name = "ckbPosicion";
            this.ckbPosicion.Size = new System.Drawing.Size(21, 20);
            this.ckbPosicion.TabIndex = 46;
            this.ckbPosicion.Visible = false;
            // 
            // ckbZona
            // 
            this.ckbZona.Location = new System.Drawing.Point(10, 126);
            this.ckbZona.Name = "ckbZona";
            this.ckbZona.Size = new System.Drawing.Size(21, 20);
            this.ckbZona.TabIndex = 47;
            this.ckbZona.CheckStateChanged += new System.EventHandler(this.ckbZona_CheckStateChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnConectar);
            this.panel1.Controls.Add(this.btnLeer);
            this.panel1.Controls.Add(this.btnDetener);
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.dataGrid1);
            this.panel1.Controls.Add(this.ckbZona);
            this.panel1.Controls.Add(this.ckbPosicion);
            this.panel1.Controls.Add(this.ckbRack);
            this.panel1.Controls.Add(this.label);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cbSucursales);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbPosiciones);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbRacks);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbZonas);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbAlmacen);
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 240);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(3, 200);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(72, 30);
            this.btnConectar.TabIndex = 76;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnLeer
            // 
            this.btnLeer.Enabled = false;
            this.btnLeer.Location = new System.Drawing.Point(80, 200);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(73, 30);
            this.btnLeer.TabIndex = 75;
            this.btnLeer.Text = "Leer";
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(158, 200);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(0, 0);
            this.btnDetener.TabIndex = 74;
            this.btnDetener.Text = "Detener";
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(163, 200);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(0, 0);
            this.btnFinalizar.TabIndex = 73;
            this.btnFinalizar.Text = "Finalizar";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(0, 167);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(237, 33);
            this.dataGrid1.TabIndex = 53;
            this.dataGrid1.Visible = false;
            // 
            // frmUbicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmUbicacion";
            this.Text = "Seleccion de Almacen";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.ComboBox cbAlmacen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbZonas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbRacks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPosiciones;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbSucursales;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.CheckBox ckbRack;
        private System.Windows.Forms.CheckBox ckbPosicion;
        private System.Windows.Forms.CheckBox ckbZona;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.Button btnFinalizar;
    }
}