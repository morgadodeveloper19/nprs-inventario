namespace SmartDeviceProject1
{
    partial class frmMenu_Produccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu_Produccion));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mi_Regresar = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnValidar_Orden = new System.Windows.Forms.Button();
            this.btnRevisar_Avance = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            // btnValidar_Orden
            // 
            this.btnValidar_Orden.Location = new System.Drawing.Point(26, 82);
            this.btnValidar_Orden.Name = "btnValidar_Orden";
            this.btnValidar_Orden.Size = new System.Drawing.Size(200, 46);
            this.btnValidar_Orden.TabIndex = 2;
            this.btnValidar_Orden.Text = "VALIDAR ORDEN";
            this.btnValidar_Orden.Click += new System.EventHandler(this.btnValidar_Orden_Click);
            // 
            // btnRevisar_Avance
            // 
            this.btnRevisar_Avance.Location = new System.Drawing.Point(26, 143);
            this.btnRevisar_Avance.Name = "btnRevisar_Avance";
            this.btnRevisar_Avance.Size = new System.Drawing.Size(200, 46);
            this.btnRevisar_Avance.TabIndex = 3;
            this.btnRevisar_Avance.Text = "REVISAR AVANCE";
            this.btnRevisar_Avance.Click += new System.EventHandler(this.btnRevisar_Avance_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 205);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 46);
            this.button1.TabIndex = 5;
            this.button1.Text = "ASIGNAR RACKS";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMenu_Produccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRevisar_Avance);
            this.Controls.Add(this.btnValidar_Orden);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "frmMenu_Produccion";
            this.Text = "Produccion";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mi_Regresar;
        private System.Windows.Forms.Button btnValidar_Orden;
        private System.Windows.Forms.Button btnRevisar_Avance;
        private System.Windows.Forms.Button button1;
    }
}