namespace SmartDeviceProject1.Produccion
{
    partial class Racks_RFID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Racks_RFID));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnLeer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCalculoRacks = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEstatus = new System.Windows.Forms.Label();
            this.btnCalcula = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblRacksAsignados = new System.Windows.Forms.Label();
            this.btnValidaciones = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Enabled = false;
            this.menuItem1.Text = "FINALIZAR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(3, 147);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(104, 38);
            this.btnConectar.TabIndex = 7;
            this.btnConectar.Text = "CONECTAR";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnLeer
            // 
            this.btnLeer.Location = new System.Drawing.Point(133, 147);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(104, 38);
            this.btnLeer.TabIndex = 8;
            this.btnLeer.Text = "LEER";
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(155, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.Text = "Racks";
            // 
            // lblCalculoRacks
            // 
            this.lblCalculoRacks.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblCalculoRacks.Location = new System.Drawing.Point(122, 65);
            this.lblCalculoRacks.Name = "lblCalculoRacks";
            this.lblCalculoRacks.Size = new System.Drawing.Size(27, 20);
            this.lblCalculoRacks.Text = "#Racks";
            this.lblCalculoRacks.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Se Calcularon ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblEstatus
            // 
            this.lblEstatus.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblEstatus.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblEstatus.Location = new System.Drawing.Point(3, 203);
            this.lblEstatus.Name = "lblEstatus";
            this.lblEstatus.Size = new System.Drawing.Size(234, 65);
            this.lblEstatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCalcula
            // 
            this.btnCalcula.Location = new System.Drawing.Point(34, 194);
            this.btnCalcula.Name = "btnCalcula";
            this.btnCalcula.Size = new System.Drawing.Size(72, 20);
            this.btnCalcula.TabIndex = 9;
            this.btnCalcula.Text = "Bautiza";
            this.btnCalcula.Visible = false;
            this.btnCalcula.Click += new System.EventHandler(this.btnCalcula_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblRacksAsignados
            // 
            this.lblRacksAsignados.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblRacksAsignados.Location = new System.Drawing.Point(29, 102);
            this.lblRacksAsignados.Name = "lblRacksAsignados";
            this.lblRacksAsignados.Size = new System.Drawing.Size(179, 20);
            this.lblRacksAsignados.Text = "Racks Asignados: ";
            this.lblRacksAsignados.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnValidaciones
            // 
            this.btnValidaciones.Location = new System.Drawing.Point(137, 203);
            this.btnValidaciones.Name = "btnValidaciones";
            this.btnValidaciones.Size = new System.Drawing.Size(72, 20);
            this.btnValidaciones.TabIndex = 14;
            this.btnValidaciones.Text = "Validaciones";
            this.btnValidaciones.Visible = false;
            this.btnValidaciones.Click += new System.EventHandler(this.btnValidaciones_Click);
            // 
            // Racks_RFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnValidaciones);
            this.Controls.Add(this.lblRacksAsignados);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCalcula);
            this.Controls.Add(this.lblEstatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCalculoRacks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnConectar);
            this.Menu = this.mainMenu1;
            this.Name = "Racks_RFID";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCalculoRacks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblEstatus;
        private System.Windows.Forms.Button btnCalcula;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblRacksAsignados;
        private System.Windows.Forms.Button btnValidaciones;
    }
}