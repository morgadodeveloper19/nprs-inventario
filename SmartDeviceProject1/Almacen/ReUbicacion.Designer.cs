namespace SmartDeviceProject1.Almacen
{
    partial class Re_Ubicacion
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
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbCodigo = new System.Windows.Forms.Label();
            this.lbDescripcion = new System.Windows.Forms.Label();
            this.lbPzas = new System.Windows.Forms.Label();
            this.cbZonas = new System.Windows.Forms.ComboBox();
            this.lbSelecciona = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "CANCELAR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Enabled = false;
            this.menuItem2.Text = "REUBICAR";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 20);
            this.label1.Text = "INGRESA EL TAG:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(165, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(65, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lbCodigo
            // 
            this.lbCodigo.Enabled = false;
            this.lbCodigo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbCodigo.ForeColor = System.Drawing.Color.Blue;
            this.lbCodigo.Location = new System.Drawing.Point(4, 39);
            this.lbCodigo.Name = "lbCodigo";
            this.lbCodigo.Size = new System.Drawing.Size(233, 20);
            this.lbCodigo.Text = "codigo";
            this.lbCodigo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbCodigo.Visible = false;
            // 
            // lbDescripcion
            // 
            this.lbDescripcion.Enabled = false;
            this.lbDescripcion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbDescripcion.ForeColor = System.Drawing.Color.Red;
            this.lbDescripcion.Location = new System.Drawing.Point(4, 63);
            this.lbDescripcion.Name = "lbDescripcion";
            this.lbDescripcion.Size = new System.Drawing.Size(233, 52);
            this.lbDescripcion.Text = "descripcion";
            this.lbDescripcion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbDescripcion.Visible = false;
            // 
            // lbPzas
            // 
            this.lbPzas.Enabled = false;
            this.lbPzas.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbPzas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbPzas.Location = new System.Drawing.Point(3, 115);
            this.lbPzas.Name = "lbPzas";
            this.lbPzas.Size = new System.Drawing.Size(233, 20);
            this.lbPzas.Text = "pzas";
            this.lbPzas.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbPzas.Visible = false;
            // 
            // cbZonas
            // 
            this.cbZonas.Enabled = false;
            this.cbZonas.Location = new System.Drawing.Point(4, 208);
            this.cbZonas.Name = "cbZonas";
            this.cbZonas.Size = new System.Drawing.Size(226, 22);
            this.cbZonas.TabIndex = 5;
            this.cbZonas.Visible = false;
            // 
            // lbSelecciona
            // 
            this.lbSelecciona.Enabled = false;
            this.lbSelecciona.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbSelecciona.Location = new System.Drawing.Point(4, 148);
            this.lbSelecciona.Name = "lbSelecciona";
            this.lbSelecciona.Size = new System.Drawing.Size(232, 48);
            this.lbSelecciona.Text = "SELECCIONA LA UBICACION A DONDE QUIERES REUBICAR EL MATERIAL";
            this.lbSelecciona.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbSelecciona.Visible = false;
            // 
            // Re_Ubicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lbSelecciona);
            this.Controls.Add(this.cbZonas);
            this.Controls.Add(this.lbPzas);
            this.Controls.Add(this.lbDescripcion);
            this.Controls.Add(this.lbCodigo);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Re_Ubicacion";
            this.Text = "REUBICACION";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbCodigo;
        private System.Windows.Forms.Label lbDescripcion;
        private System.Windows.Forms.Label lbPzas;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ComboBox cbZonas;
        private System.Windows.Forms.Label lbSelecciona;
    }
}