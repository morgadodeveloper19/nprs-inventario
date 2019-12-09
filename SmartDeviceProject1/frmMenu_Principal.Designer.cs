namespace SmartDeviceProject1
{
    partial class frmMenu_Principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu_Principal));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mi_Salir = new System.Windows.Forms.MenuItem();
            this.btnInventario = new System.Windows.Forms.Button();
            this.btnProduccion = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnEmbarques = new System.Windows.Forms.Button();
            this.btnAlmacen = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.mi_Salir);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "SALIR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // mi_Salir
            // 
            this.mi_Salir.Enabled = false;
            this.mi_Salir.Text = " ";
            this.mi_Salir.Click += new System.EventHandler(this.mi_Salir_Click);
            // 
            // btnInventario
            // 
            this.btnInventario.Enabled = false;
            this.btnInventario.Location = new System.Drawing.Point(15, 65);
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Size = new System.Drawing.Size(95, 90);
            this.btnInventario.TabIndex = 0;
            this.btnInventario.Text = "INVENTARIO";
            this.btnInventario.Visible = false;
            this.btnInventario.Click += new System.EventHandler(this.btnInventario_Click);
            // 
            // btnProduccion
            // 
            this.btnProduccion.Enabled = false;
            this.btnProduccion.Location = new System.Drawing.Point(132, 65);
            this.btnProduccion.Name = "btnProduccion";
            this.btnProduccion.Size = new System.Drawing.Size(78, 48);
            this.btnProduccion.TabIndex = 1;
            this.btnProduccion.Text = "PRODUCCION";
            this.btnProduccion.Visible = false;
            this.btnProduccion.Click += new System.EventHandler(this.btnProduccion_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // btnEmbarques
            // 
            this.btnEmbarques.Enabled = false;
            this.btnEmbarques.Location = new System.Drawing.Point(28, 176);
            this.btnEmbarques.Name = "btnEmbarques";
            this.btnEmbarques.Size = new System.Drawing.Size(18, 19);
            this.btnEmbarques.TabIndex = 4;
            this.btnEmbarques.Text = "EMBARQUES";
            this.btnEmbarques.Visible = false;
            this.btnEmbarques.Click += new System.EventHandler(this.btnEmbarques_Click);
            // 
            // btnAlmacen
            // 
            this.btnAlmacen.Enabled = false;
            this.btnAlmacen.Location = new System.Drawing.Point(44, 91);
            this.btnAlmacen.Name = "btnAlmacen";
            this.btnAlmacen.Size = new System.Drawing.Size(48, 43);
            this.btnAlmacen.TabIndex = 5;
            this.btnAlmacen.Text = "ALMACEN";
            this.btnAlmacen.Visible = false;
            this.btnAlmacen.Click += new System.EventHandler(this.btnAlmacen_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(212, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "INVENTARIO";
            this.button1.Click += new System.EventHandler(this.btnInventario_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(132, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 48);
            this.button2.TabIndex = 1;
            this.button2.Text = "PRODUCCION";
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.btnProduccion_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 65);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(212, 55);
            this.button4.TabIndex = 5;
            this.button4.Text = "ALMACEN";
            this.button4.Click += new System.EventHandler(this.btnAlmacen_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 140);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(212, 55);
            this.button3.TabIndex = 4;
            this.button3.Text = "EMBARQUES";
            this.button3.Click += new System.EventHandler(this.btnEmbarques_Click);
            // 
            // frmMenu_Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnAlmacen);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnEmbarques);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnProduccion);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnInventario);
            this.Menu = this.mainMenu1;
            this.Name = "frmMenu_Principal";
            this.Text = "Menu Principal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mi_Salir;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnProduccion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnEmbarques;
        private System.Windows.Forms.Button btnAlmacen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
    }
}