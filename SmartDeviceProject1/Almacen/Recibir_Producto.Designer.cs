namespace SmartDeviceProject1.Almacen
{
    partial class Recibir_Producto
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
            System.Windows.Forms.MenuItem menuItem2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recibir_Producto));
            this.mi_NoFiltro = new System.Windows.Forms.MenuItem();
            this.mi_Tarima = new System.Windows.Forms.MenuItem();
            this.mi_Granel = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnDetener = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            menuItem2 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // menuItem2
            // 
            menuItem2.Enabled = false;
            menuItem2.MenuItems.Add(this.mi_NoFiltro);
            menuItem2.MenuItems.Add(this.mi_Tarima);
            menuItem2.MenuItems.Add(this.mi_Granel);
            menuItem2.Text = "FILTRO";
            menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // mi_NoFiltro
            // 
            this.mi_NoFiltro.Checked = true;
            this.mi_NoFiltro.Text = "[Sin Filtro]";
            this.mi_NoFiltro.Click += new System.EventHandler(this.mi_NoFiltro_Click);
            // 
            // mi_Tarima
            // 
            this.mi_Tarima.Text = "Entarimado";
            this.mi_Tarima.Click += new System.EventHandler(this.mi_Tarima_Click);
            // 
            // mi_Granel
            // 
            this.mi_Granel.Text = "A Granel";
            this.mi_Granel.Click += new System.EventHandler(this.mi_Granel_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "REGRESAR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // btnLeer
            // 
            this.btnLeer.Enabled = false;
            this.btnLeer.Location = new System.Drawing.Point(87, 63);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(72, 20);
            this.btnLeer.TabIndex = 66;
            this.btnLeer.Text = "Leer";
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(165, 63);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(72, 20);
            this.btnDetener.TabIndex = 65;
            this.btnDetener.Text = "Detener";
            this.btnDetener.Visible = false;
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(6, 63);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(72, 20);
            this.btnConectar.TabIndex = 63;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(0, 89);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(240, 165);
            this.dataGrid1.TabIndex = 62;
            this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
            // 
            // Recibir_Producto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Recibir_Producto";
            this.Text = "Recepción de Producto";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mi_Tarima;
        private System.Windows.Forms.MenuItem mi_Granel;
        private System.Windows.Forms.MenuItem mi_NoFiltro;

    }
}