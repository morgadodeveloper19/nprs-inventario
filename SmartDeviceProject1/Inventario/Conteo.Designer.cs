namespace SmartDeviceProject1.Inventario
{
    partial class Conteo
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.guardar = new System.Windows.Forms.MenuItem();
            this.cbUbicacion = new System.Windows.Forms.ComboBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnLeer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbCodigo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbUbicacion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbPiezas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.guardar);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "";
            // 
            // guardar
            // 
            this.guardar.Text = "GUARDAR";
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // cbUbicacion
            // 
            this.cbUbicacion.Location = new System.Drawing.Point(31, 14);
            this.cbUbicacion.Name = "cbUbicacion";
            this.cbUbicacion.Size = new System.Drawing.Size(176, 22);
            this.cbUbicacion.TabIndex = 0;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(16, 51);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(90, 34);
            this.btnConectar.TabIndex = 1;
            this.btnConectar.Text = "CONECTAR";
            // 
            // btnLeer
            // 
            this.btnLeer.Location = new System.Drawing.Point(138, 51);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(90, 34);
            this.btnLeer.TabIndex = 2;
            this.btnLeer.Text = "LEER";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.Text = "CODIGO:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(6, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "UBICACIÓN:";
            // 
            // lbCodigo
            // 
            this.lbCodigo.Location = new System.Drawing.Point(66, 98);
            this.lbCodigo.Name = "lbCodigo";
            this.lbCodigo.Size = new System.Drawing.Size(126, 20);
            this.lbCodigo.Text = "codigo";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "DESCRIPCIÓN:";
            // 
            // lbUbicacion
            // 
            this.lbUbicacion.Location = new System.Drawing.Point(92, 127);
            this.lbUbicacion.Name = "lbUbicacion";
            this.lbUbicacion.Size = new System.Drawing.Size(145, 20);
            this.lbUbicacion.Text = "UBICACION";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 63);
            this.label4.Text = "DESCRIPCION";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(92, 243);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 9;
            // 
            // lbPiezas
            // 
            this.lbPiezas.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbPiezas.Location = new System.Drawing.Point(6, 244);
            this.lbPiezas.Name = "lbPiezas";
            this.lbPiezas.Size = new System.Drawing.Size(80, 20);
            this.lbPiezas.Text = "CANTIDAD:";
            // 
            // Conteo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lbPiezas);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbUbicacion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbCodigo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.cbUbicacion);
            this.Menu = this.mainMenu1;
            this.Name = "Conteo";
            this.Text = "Conteo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbUbicacion;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbCodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbUbicacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbPiezas;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem guardar;
    }
}