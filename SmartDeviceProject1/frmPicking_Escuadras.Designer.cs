namespace SmartDeviceProject1
{
    partial class frmPicking_Escuadras
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPicking_Escuadras));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnDetener = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRemision = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
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
            this.btnLeer.Location = new System.Drawing.Point(86, 255);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(45, 20);
            this.btnLeer.TabIndex = 62;
            this.btnLeer.Text = "LEER";
            this.btnLeer.Visible = false;
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // btnDetener
            // 
            this.btnDetener.Enabled = false;
            this.btnDetener.Location = new System.Drawing.Point(86, 255);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(64, 20);
            this.btnDetener.TabIndex = 61;
            this.btnDetener.Text = "DETENER";
            this.btnDetener.Visible = false;
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(143, 255);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(80, 20);
            this.btnFinalizar.TabIndex = 60;
            this.btnFinalizar.Text = "FINALIZAR";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Enabled = false;
            this.btnConectar.Location = new System.Drawing.Point(3, 255);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(77, 20);
            this.btnConectar.TabIndex = 59;
            this.btnConectar.Text = "CONECTAR";
            this.btnConectar.Visible = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Enabled = false;
            this.dataGrid1.Location = new System.Drawing.Point(3, 124);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(233, 95);
            this.dataGrid1.TabIndex = 58;
            this.dataGrid1.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Enabled = false;
            this.btnBuscar.Location = new System.Drawing.Point(17, 184);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(206, 28);
            this.btnBuscar.TabIndex = 57;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Visible = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(41, 157);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(139, 21);
            this.textBox1.TabIndex = 56;
            this.textBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(62, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.Text = "Folio de Remisión";
            // 
            // cbRemision
            // 
            this.cbRemision.Location = new System.Drawing.Point(51, 87);
            this.cbRemision.Name = "cbRemision";
            this.cbRemision.Size = new System.Drawing.Size(139, 22);
            this.cbRemision.TabIndex = 65;
            this.cbRemision.SelectedIndexChanged += new System.EventHandler(this.cbRemision_SelectedIndexChanged);
            // 
            // frmPicking_Escuadras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.cbRemision);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.Name = "frmPicking_Escuadras";
            this.Text = "Picking";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPicking_Escuadras_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbRemision;
    }
}