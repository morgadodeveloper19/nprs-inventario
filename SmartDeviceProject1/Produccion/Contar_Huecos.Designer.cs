namespace SmartDeviceProject1.Produccion
{
    partial class Contar_Huecos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Contar_Huecos));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnDetener = new System.Windows.Forms.Button();
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
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
            this.menuItem2.Text = "A CURADO";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(3, 65);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(72, 20);
            this.btnConectar.TabIndex = 43;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(162, 65);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(72, 20);
            this.btnDetener.TabIndex = 46;
            this.btnDetener.Text = "Detener";
            this.btnDetener.Visible = false;
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // btnLeer
            // 
            this.btnLeer.Location = new System.Drawing.Point(84, 65);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(72, 20);
            this.btnLeer.TabIndex = 47;
            this.btnLeer.Text = "Leer";
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(162, 91);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(72, 20);
            this.btnFinalizar.TabIndex = 49;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 91);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(234, 174);
            this.dataGrid1.TabIndex = 51;
            // 
            // Contar_Huecos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Contar_Huecos";
            this.Text = "Contar Huecos";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.DataGrid dataGrid1;
    }
}