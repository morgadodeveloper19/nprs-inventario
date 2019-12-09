namespace SmartDeviceProject1
{
    partial class frmMenu_Inventario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu_Inventario));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mi_Regresar = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnNuevo_Inventario = new System.Windows.Forms.Button();
            this.btnContinuar_Inventario = new System.Windows.Forms.Button();
            this.btnBuscar_Tag = new System.Windows.Forms.Button();
            this.btnReporte = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.BtnInvIni = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.mi_Regresar);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "REGRESAR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // mi_Regresar
            // 
            this.mi_Regresar.Enabled = false;
            this.mi_Regresar.Text = " ";
            this.mi_Regresar.Click += new System.EventHandler(this.mi_Regresar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // btnNuevo_Inventario
            // 
            this.btnNuevo_Inventario.Location = new System.Drawing.Point(23, 65);
            this.btnNuevo_Inventario.Name = "btnNuevo_Inventario";
            this.btnNuevo_Inventario.Size = new System.Drawing.Size(200, 45);
            this.btnNuevo_Inventario.TabIndex = 2;
            this.btnNuevo_Inventario.Text = "CONTEOS";
            this.btnNuevo_Inventario.Click += new System.EventHandler(this.btnNuevo_Inventario_Click);
            // 
            // btnContinuar_Inventario
            // 
            this.btnContinuar_Inventario.Location = new System.Drawing.Point(23, 116);
            this.btnContinuar_Inventario.Name = "btnContinuar_Inventario";
            this.btnContinuar_Inventario.Size = new System.Drawing.Size(200, 45);
            this.btnContinuar_Inventario.TabIndex = 3;
            this.btnContinuar_Inventario.Text = "CONTINUAR CONTEOS";
            this.btnContinuar_Inventario.Click += new System.EventHandler(this.btnContinuar_Inventario_Click);
            // 
            // btnBuscar_Tag
            // 
            this.btnBuscar_Tag.Location = new System.Drawing.Point(23, 324);
            this.btnBuscar_Tag.Name = "btnBuscar_Tag";
            this.btnBuscar_Tag.Size = new System.Drawing.Size(200, 45);
            this.btnBuscar_Tag.TabIndex = 4;
            this.btnBuscar_Tag.Text = "BUSQUEDA DE TAG";
            this.btnBuscar_Tag.Visible = false;
            this.btnBuscar_Tag.Click += new System.EventHandler(this.btnBuscar_Tag_Click);
            // 
            // btnReporte
            // 
            this.btnReporte.Enabled = false;
            this.btnReporte.Location = new System.Drawing.Point(23, 211);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(200, 45);
            this.btnReporte.TabIndex = 6;
            this.btnReporte.Text = "REPORTE DE INVENTARIOS";
            this.btnReporte.Visible = false;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // btnStock
            // 
            this.btnStock.Location = new System.Drawing.Point(23, 273);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(200, 45);
            this.btnStock.TabIndex = 8;
            this.btnStock.Text = "DESASIGNACIÓN";
            this.btnStock.Visible = false;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // BtnInvIni
            // 
            this.BtnInvIni.Location = new System.Drawing.Point(23, 167);
            this.BtnInvIni.Name = "BtnInvIni";
            this.BtnInvIni.Size = new System.Drawing.Size(200, 45);
            this.BtnInvIni.TabIndex = 10;
            this.BtnInvIni.Text = "INVENTARIO INICIAL";
            this.BtnInvIni.Click += new System.EventHandler(this.BtnInvIni_Click);
            // 
            // frmMenu_Inventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.BtnInvIni);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.btnBuscar_Tag);
            this.Controls.Add(this.btnContinuar_Inventario);
            this.Controls.Add(this.btnNuevo_Inventario);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "frmMenu_Inventario";
            this.Text = "Inventario";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mi_Regresar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnNuevo_Inventario;
        private System.Windows.Forms.Button btnContinuar_Inventario;
        private System.Windows.Forms.Button btnBuscar_Tag;
        private System.Windows.Forms.Button btnReporte;
		private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button BtnInvIni;
    }
}