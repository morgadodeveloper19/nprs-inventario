namespace SmartDeviceProject1.Inventario
{
    partial class Buscar_Tag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Buscar_Tag));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtTagEpc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFijar = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lstEpc = new System.Windows.Forms.ListBox();
            this.lblEstatus = new System.Windows.Forms.Label();
            this.btnDetener = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "LIMPIAR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "FINALIZAR";
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
            // txtTagEpc
            // 
            this.txtTagEpc.Location = new System.Drawing.Point(38, 65);
            this.txtTagEpc.Name = "txtTagEpc";
            this.txtTagEpc.Size = new System.Drawing.Size(195, 21);
            this.txtTagEpc.TabIndex = 2;
            this.txtTagEpc.TextChanged += new System.EventHandler(this.txtTagEpc_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 20);
            this.label1.Text = "TAG";
            // 
            // btnFijar
            // 
            this.btnFijar.Location = new System.Drawing.Point(7, 89);
            this.btnFijar.Name = "btnFijar";
            this.btnFijar.Size = new System.Drawing.Size(71, 22);
            this.btnFijar.TabIndex = 5;
            this.btnFijar.Text = "Fijar Tag";
            this.btnFijar.Click += new System.EventHandler(this.btnFijar_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(85, 89);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(71, 22);
            this.btnConectar.TabIndex = 6;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(71, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.Text = "TAG\'s Leidos";
            // 
            // lstEpc
            // 
            this.lstEpc.Location = new System.Drawing.Point(5, 131);
            this.lstEpc.Name = "lstEpc";
            this.lstEpc.Size = new System.Drawing.Size(228, 100);
            this.lstEpc.TabIndex = 89;
            // 
            // lblEstatus
            // 
            this.lblEstatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblEstatus.Location = new System.Drawing.Point(53, 241);
            this.lblEstatus.Name = "lblEstatus";
            this.lblEstatus.Size = new System.Drawing.Size(149, 20);
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(161, 89);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(71, 22);
            this.btnDetener.TabIndex = 92;
            this.btnDetener.Text = "Detener";
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(161, 89);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(71, 22);
            this.btnBuscar.TabIndex = 93;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // Buscar_Tag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.lblEstatus);
            this.Controls.Add(this.lstEpc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.btnFijar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTagEpc);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Buscar_Tag";
            this.Text = "Buscar Tag";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.TextBox txtTagEpc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFijar;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstEpc;
        private System.Windows.Forms.Label lblEstatus;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.Button btnBuscar;
    }
}