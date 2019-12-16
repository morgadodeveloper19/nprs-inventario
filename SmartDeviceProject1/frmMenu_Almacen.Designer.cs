namespace SmartDeviceProject1
{
    partial class frmMenu_Almacen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu_Almacen));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bntNvoPicking = new System.Windows.Forms.Button();
            this.btnReporteMermas = new System.Windows.Forms.Button();
            this.btnAlmacen = new System.Windows.Forms.Button();
            this.btnPicking = new System.Windows.Forms.Button();
            this.btnEscuadra = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            this.menuItem2.Text = " ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // bntNvoPicking
            // 
            this.bntNvoPicking.Location = new System.Drawing.Point(33, 233);
            this.bntNvoPicking.Name = "bntNvoPicking";
            this.bntNvoPicking.Size = new System.Drawing.Size(170, 50);
            this.bntNvoPicking.TabIndex = 7;
            this.bntNvoPicking.Text = "MERMAS";
            this.bntNvoPicking.Click += new System.EventHandler(this.bntNvoPicking_Click);
            // 
            // btnReporteMermas
            // 
            this.btnReporteMermas.Location = new System.Drawing.Point(33, 292);
            this.btnReporteMermas.Name = "btnReporteMermas";
            this.btnReporteMermas.Size = new System.Drawing.Size(170, 50);
            this.btnReporteMermas.TabIndex = 6;
            this.btnReporteMermas.Text = "REUBICAR}";
            this.btnReporteMermas.Click += new System.EventHandler(this.btnReporteMermas_Click);
            // 
            // btnAlmacen
            // 
            this.btnAlmacen.Location = new System.Drawing.Point(33, 65);
            this.btnAlmacen.Name = "btnAlmacen";
            this.btnAlmacen.Size = new System.Drawing.Size(170, 50);
            this.btnAlmacen.TabIndex = 4;
            this.btnAlmacen.Text = "UBICACIÓN";
            this.btnAlmacen.Click += new System.EventHandler(this.btnAlmacen_Click);
            // 
            // btnPicking
            // 
            this.btnPicking.Location = new System.Drawing.Point(33, 315);
            this.btnPicking.Name = "btnPicking";
            this.btnPicking.Size = new System.Drawing.Size(163, 24);
            this.btnPicking.TabIndex = 9;
            this.btnPicking.Text = "Picking Almacen";
            this.btnPicking.Visible = false;
            this.btnPicking.Click += new System.EventHandler(this.btnPicking_Click);
            // 
            // btnEscuadra
            // 
            this.btnEscuadra.Location = new System.Drawing.Point(33, 121);
            this.btnEscuadra.Name = "btnEscuadra";
            this.btnEscuadra.Size = new System.Drawing.Size(170, 50);
            this.btnEscuadra.TabIndex = 11;
            this.btnEscuadra.Text = "VALIDAR REMISIÓN";
            this.btnEscuadra.Click += new System.EventHandler(this.btnEscuadra_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 50);
            this.button1.TabIndex = 13;
            this.button1.Text = "SURTIR REMISIÓN";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMenu_Almacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnEscuadra);
            this.Controls.Add(this.btnPicking);
            this.Controls.Add(this.bntNvoPicking);
            this.Controls.Add(this.btnReporteMermas);
            this.Controls.Add(this.btnAlmacen);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "frmMenu_Almacen";
            this.Text = "Almacen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Button bntNvoPicking;
        private System.Windows.Forms.Button btnReporteMermas;
        private System.Windows.Forms.Button btnAlmacen;
        private System.Windows.Forms.Button btnPicking;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnEscuadra;
    }
}