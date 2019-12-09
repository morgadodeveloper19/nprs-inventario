namespace SmartDeviceProject1.Inventario
{
    partial class Carga_Inv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Carga_Inv));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mi_Cancelar = new System.Windows.Forms.MenuItem();
            this.mi_Siguiente = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSucursales = new System.Windows.Forms.ComboBox();
            this.cbAlmacen = new System.Windows.Forms.ComboBox();
            this.ckbZona = new System.Windows.Forms.CheckBox();
            this.ckbRack = new System.Windows.Forms.CheckBox();
            this.cbZonas = new System.Windows.Forms.ComboBox();
            this.cbRacks = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEsc = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mi_Cancelar);
            this.mainMenu1.MenuItems.Add(this.mi_Siguiente);
            // 
            // mi_Cancelar
            // 
            this.mi_Cancelar.Text = "SALIR";
            this.mi_Cancelar.Click += new System.EventHandler(this.mi_Cancelar_Click);
            // 
            // mi_Siguiente
            // 
            this.mi_Siguiente.Text = "GUARDAR";
            this.mi_Siguiente.Click += new System.EventHandler(this.mi_Siguiente_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // label
            // 
            this.label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label.Location = new System.Drawing.Point(41, 128);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(171, 20);
            this.label.Text = "CODIGO DEL PRODUCTO";
            this.label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(131, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.Text = "BANDERA";
            this.label5.Visible = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(82, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.Text = "UBICACIÓN";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(7, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 32);
            this.label1.Text = "CANTIDAD DE PIEZAS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbSucursales
            // 
            this.cbSucursales.Location = new System.Drawing.Point(39, 93);
            this.cbSucursales.Name = "cbSucursales";
            this.cbSucursales.Size = new System.Drawing.Size(173, 22);
            this.cbSucursales.TabIndex = 12;
            this.cbSucursales.SelectedIndexChanged += new System.EventHandler(this.cbSucursales_SelectedIndexChanged);
            // 
            // cbAlmacen
            // 
            this.cbAlmacen.Location = new System.Drawing.Point(172, 0);
            this.cbAlmacen.Name = "cbAlmacen";
            this.cbAlmacen.Size = new System.Drawing.Size(21, 22);
            this.cbAlmacen.TabIndex = 41;
            this.cbAlmacen.Visible = false;
            // 
            // ckbZona
            // 
            this.ckbZona.Enabled = false;
            this.ckbZona.Location = new System.Drawing.Point(157, 0);
            this.ckbZona.Name = "ckbZona";
            this.ckbZona.Size = new System.Drawing.Size(21, 20);
            this.ckbZona.TabIndex = 14;
            this.ckbZona.Visible = false;
            this.ckbZona.CheckStateChanged += new System.EventHandler(this.ckbZona_CheckStateChanged);
            // 
            // ckbRack
            // 
            this.ckbRack.Enabled = false;
            this.ckbRack.Location = new System.Drawing.Point(130, 0);
            this.ckbRack.Name = "ckbRack";
            this.ckbRack.Size = new System.Drawing.Size(21, 20);
            this.ckbRack.TabIndex = 21;
            this.ckbRack.Visible = false;
            this.ckbRack.CheckStateChanged += new System.EventHandler(this.ckbRack_CheckStateChanged);
            // 
            // cbZonas
            // 
            this.cbZonas.Enabled = false;
            this.cbZonas.Location = new System.Drawing.Point(39, 0);
            this.cbZonas.Name = "cbZonas";
            this.cbZonas.Size = new System.Drawing.Size(173, 22);
            this.cbZonas.TabIndex = 29;
            this.cbZonas.Visible = false;
            this.cbZonas.SelectedIndexChanged += new System.EventHandler(this.cbZonas_SelectedIndexChanged);
            // 
            // cbRacks
            // 
            this.cbRacks.Enabled = false;
            this.cbRacks.Location = new System.Drawing.Point(100, 0);
            this.cbRacks.Name = "cbRacks";
            this.cbRacks.Size = new System.Drawing.Size(100, 22);
            this.cbRacks.TabIndex = 30;
            this.cbRacks.Visible = false;
            this.cbRacks.SelectedIndexChanged += new System.EventHandler(this.cbRacks_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(121, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "# ESCUADRA";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEsc
            // 
            this.txtEsc.Location = new System.Drawing.Point(126, 210);
            this.txtEsc.Name = "txtEsc";
            this.txtEsc.Size = new System.Drawing.Size(100, 21);
            this.txtEsc.TabIndex = 39;
            this.txtEsc.TextChanged += new System.EventHandler(this.txtEsc_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(39, 151);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 21);
            this.textBox1.TabIndex = 47;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(28, 210);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(60, 21);
            this.textBox2.TabIndex = 48;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Carga_Inv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtEsc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbRacks);
            this.Controls.Add(this.cbZonas);
            this.Controls.Add(this.ckbRack);
            this.Controls.Add(this.ckbZona);
            this.Controls.Add(this.cbAlmacen);
            this.Controls.Add(this.cbSucursales);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Carga_Inv";
            this.Text = "Seleccion de Almacen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mi_Cancelar;
        private System.Windows.Forms.MenuItem mi_Siguiente;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSucursales;
        private System.Windows.Forms.ComboBox cbAlmacen;
        private System.Windows.Forms.CheckBox ckbZona;
        private System.Windows.Forms.CheckBox ckbRack;
        private System.Windows.Forms.ComboBox cbZonas;
        private System.Windows.Forms.ComboBox cbRacks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEsc;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}