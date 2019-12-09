namespace SmartDeviceProject1.Produccion
{
    partial class Asignar_Escuadra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Asignar_Escuadra));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rbProduccion = new System.Windows.Forms.RadioButton();
            this.rbMerma = new System.Windows.Forms.RadioButton();
            this.txtPXT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnDetener = new System.Windows.Forms.Button();
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
            // rbProduccion
            // 
            this.rbProduccion.Location = new System.Drawing.Point(127, 107);
            this.rbProduccion.Name = "rbProduccion";
            this.rbProduccion.Size = new System.Drawing.Size(113, 20);
            this.rbProduccion.TabIndex = 95;
            this.rbProduccion.Text = "por Producción";
            this.rbProduccion.CheckedChanged += new System.EventHandler(this.rbProduccion_CheckedChanged);
            // 
            // rbMerma
            // 
            this.rbMerma.Location = new System.Drawing.Point(3, 107);
            this.rbMerma.Name = "rbMerma";
            this.rbMerma.Size = new System.Drawing.Size(100, 20);
            this.rbMerma.TabIndex = 94;
            this.rbMerma.Text = "Merma";
            this.rbMerma.CheckedChanged += new System.EventHandler(this.rbMerma_CheckedChanged);
            // 
            // txtPXT
            // 
            this.txtPXT.Location = new System.Drawing.Point(65, 79);
            this.txtPXT.Name = "txtPXT";
            this.txtPXT.Size = new System.Drawing.Size(100, 21);
            this.txtPXT.TabIndex = 93;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(36, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.Text = "Piezas por Tarima";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(7, 172);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(224, 93);
            this.dataGrid1.TabIndex = 92;
            this.dataGrid1.Visible = false;
            // 
            // btnLeer
            // 
            this.btnLeer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeer.BackColor = System.Drawing.Color.White;
            this.btnLeer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnLeer.Location = new System.Drawing.Point(85, 144);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(65, 22);
            this.btnLeer.TabIndex = 91;
            this.btnLeer.Text = "Leer";
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConectar.BackColor = System.Drawing.Color.White;
            this.btnConectar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnConectar.Location = new System.Drawing.Point(3, 144);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(66, 22);
            this.btnConectar.TabIndex = 89;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnDetener
            // 
            this.btnDetener.BackColor = System.Drawing.Color.White;
            this.btnDetener.Enabled = false;
            this.btnDetener.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnDetener.Location = new System.Drawing.Point(169, 144);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(0, 0);
            this.btnDetener.TabIndex = 90;
            this.btnDetener.Text = "Detener";
            this.btnDetener.Visible = false;
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // Asignar_Escuadra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.rbProduccion);
            this.Controls.Add(this.rbMerma);
            this.Controls.Add(this.txtPXT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Asignar_Escuadra";
            this.Text = "Asignar Escuadra";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton rbProduccion;
        private System.Windows.Forms.RadioButton rbMerma;
        private System.Windows.Forms.TextBox txtPXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}