namespace SmartDeviceProject1.Inventario
{
    partial class Seleccion_Almacen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Seleccion_Almacen));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mi_Cancelar = new System.Windows.Forms.MenuItem();
            this.mi_Siguiente = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSucursales = new System.Windows.Forms.ComboBox();
            this.cbAlmacen = new System.Windows.Forms.ComboBox();
            this.ckbZona = new System.Windows.Forms.CheckBox();
            this.ckbRack = new System.Windows.Forms.CheckBox();
            this.ckbPosicion = new System.Windows.Forms.CheckBox();
            this.cbZonas = new System.Windows.Forms.ComboBox();
            this.cbRacks = new System.Windows.Forms.ComboBox();
            this.cbPosiciones = new System.Windows.Forms.ComboBox();
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
            this.mi_Siguiente.Text = "SIGUIENTE";
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
            this.label.Location = new System.Drawing.Point(85, 70);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(81, 20);
            this.label.Text = "UBICACIÓN";
            // 
            // label5
            // 
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(42, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.Text = "RACK";
            this.label5.Visible = false;
            // 
            // label3
            // 
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(37, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.Text = "POSICION";
            this.label3.Visible = false;
            this.label3.ParentChanged += new System.EventHandler(this.label3_ParentChanged);
            // 
            // label2
            // 
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(42, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.Text = "ZONA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label2.Visible = false;
            this.label2.ParentChanged += new System.EventHandler(this.label2_ParentChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(85, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.Text = "ALMACEN";
            // 
            // cbSucursales
            // 
            this.cbSucursales.Location = new System.Drawing.Point(15, 88);
            this.cbSucursales.Name = "cbSucursales";
            this.cbSucursales.Size = new System.Drawing.Size(211, 22);
            this.cbSucursales.TabIndex = 12;
            this.cbSucursales.SelectedIndexChanged += new System.EventHandler(this.cbSucursales_SelectedIndexChanged);
            // 
            // cbAlmacen
            // 
            this.cbAlmacen.Enabled = false;
            this.cbAlmacen.Location = new System.Drawing.Point(15, 153);
            this.cbAlmacen.Name = "cbAlmacen";
            this.cbAlmacen.Size = new System.Drawing.Size(211, 22);
            this.cbAlmacen.TabIndex = 13;
            this.cbAlmacen.Visible = false;
            this.cbAlmacen.SelectedIndexChanged += new System.EventHandler(this.cbAlmacen_SelectedIndexChanged);
            // 
            // ckbZona
            // 
            this.ckbZona.Enabled = false;
            this.ckbZona.Location = new System.Drawing.Point(15, 193);
            this.ckbZona.Name = "ckbZona";
            this.ckbZona.Size = new System.Drawing.Size(32, 28);
            this.ckbZona.TabIndex = 14;
            this.ckbZona.Visible = false;
            this.ckbZona.CheckStateChanged += new System.EventHandler(this.ckbZona_CheckStateChanged);
            // 
            // ckbRack
            // 
            this.ckbRack.Enabled = false;
            this.ckbRack.Location = new System.Drawing.Point(15, 248);
            this.ckbRack.Name = "ckbRack";
            this.ckbRack.Size = new System.Drawing.Size(21, 20);
            this.ckbRack.TabIndex = 21;
            this.ckbRack.Visible = false;
            this.ckbRack.CheckStateChanged += new System.EventHandler(this.ckbRack_CheckStateChanged);
            // 
            // ckbPosicion
            // 
            this.ckbPosicion.Enabled = false;
            this.ckbPosicion.Location = new System.Drawing.Point(15, 230);
            this.ckbPosicion.Name = "ckbPosicion";
            this.ckbPosicion.Size = new System.Drawing.Size(21, 20);
            this.ckbPosicion.TabIndex = 22;
            this.ckbPosicion.Visible = false;
            this.ckbPosicion.CheckStateChanged += new System.EventHandler(this.ckbPosicion_CheckStateChanged);
            // 
            // cbZonas
            // 
            this.cbZonas.Location = new System.Drawing.Point(85, 201);
            this.cbZonas.Name = "cbZonas";
            this.cbZonas.Size = new System.Drawing.Size(141, 22);
            this.cbZonas.TabIndex = 29;
            this.cbZonas.SelectedIndexChanged += new System.EventHandler(this.cbZonas_SelectedIndexChanged);
            // 
            // cbRacks
            // 
            this.cbRacks.Enabled = false;
            this.cbRacks.Location = new System.Drawing.Point(126, 204);
            this.cbRacks.Name = "cbRacks";
            this.cbRacks.Size = new System.Drawing.Size(100, 22);
            this.cbRacks.TabIndex = 30;
            this.cbRacks.Visible = false;
            this.cbRacks.SelectedIndexChanged += new System.EventHandler(this.cbRacks_SelectedIndexChanged);
            // 
            // cbPosiciones
            // 
            this.cbPosiciones.Enabled = false;
            this.cbPosiciones.Location = new System.Drawing.Point(126, 230);
            this.cbPosiciones.Name = "cbPosiciones";
            this.cbPosiciones.Size = new System.Drawing.Size(100, 22);
            this.cbPosiciones.TabIndex = 31;
            this.cbPosiciones.Visible = false;
            this.cbPosiciones.SelectedIndexChanged += new System.EventHandler(this.cbPosiciones_SelectedIndexChanged);
            // 
            // Seleccion_Almacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.cbPosiciones);
            this.Controls.Add(this.cbRacks);
            this.Controls.Add(this.cbZonas);
            this.Controls.Add(this.ckbPosicion);
            this.Controls.Add(this.ckbRack);
            this.Controls.Add(this.ckbZona);
            this.Controls.Add(this.cbAlmacen);
            this.Controls.Add(this.cbSucursales);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Seleccion_Almacen";
            this.Text = "Seleccion de Almacen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mi_Cancelar;
        private System.Windows.Forms.MenuItem mi_Siguiente;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSucursales;
        private System.Windows.Forms.ComboBox cbAlmacen;
        private System.Windows.Forms.CheckBox ckbZona;
        private System.Windows.Forms.CheckBox ckbRack;
        private System.Windows.Forms.CheckBox ckbPosicion;
        private System.Windows.Forms.ComboBox cbZonas;
        private System.Windows.Forms.ComboBox cbRacks;
        private System.Windows.Forms.ComboBox cbPosiciones;
    }
}