namespace SmartDeviceProject1
{
    partial class frmMenu_Embarques
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu_Embarques));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSalida = new System.Windows.Forms.Button();
            this.btnAlmacen = new System.Windows.Forms.Button();
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
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 58);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // btnSalida
            // 
            this.btnSalida.Location = new System.Drawing.Point(44, 175);
            this.btnSalida.Name = "btnSalida";
            this.btnSalida.Size = new System.Drawing.Size(163, 70);
            this.btnSalida.TabIndex = 6;
            this.btnSalida.Text = "Salida de Embarques";
            this.btnSalida.Click += new System.EventHandler(this.btnSalida_Click);
            // 
            // btnAlmacen
            // 
            this.btnAlmacen.Location = new System.Drawing.Point(44, 74);
            this.btnAlmacen.Name = "btnAlmacen";
            this.btnAlmacen.Size = new System.Drawing.Size(163, 70);
            this.btnAlmacen.TabIndex = 5;
            this.btnAlmacen.Text = "Recepción de Producto";
            this.btnAlmacen.Click += new System.EventHandler(this.btnAlmacen_Click);
            // 
            // frmMenu_Embarques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSalida);
            this.Controls.Add(this.btnAlmacen);
            this.Menu = this.mainMenu1;
            this.Name = "frmMenu_Embarques";
            this.Text = "Embarques";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSalida;
        private System.Windows.Forms.Button btnAlmacen;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}