namespace SmartDeviceProject1.Produccion
{
    partial class Buscar_OrdenProduccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Buscar_OrdenProduccion));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnVerificar = new System.Windows.Forms.Button();
            this.dgOrden = new System.Windows.Forms.DataGrid();
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
            this.menuItem2.Text = "";
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
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 33);
            this.label1.Text = "Inserte el folio de la Orden de Producción que desea trabajar";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 21);
            this.textBox1.TabIndex = 2;
            // 
            // btnVerificar
            // 
            this.btnVerificar.Location = new System.Drawing.Point(166, 102);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.Size = new System.Drawing.Size(72, 20);
            this.btnVerificar.TabIndex = 3;
            this.btnVerificar.Text = "Validar";
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            // 
            // dgOrden
            // 
            this.dgOrden.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgOrden.Location = new System.Drawing.Point(0, 128);
            this.dgOrden.Name = "dgOrden";
            this.dgOrden.Size = new System.Drawing.Size(240, 130);
            this.dgOrden.TabIndex = 4;
            this.dgOrden.Visible = false;
            this.dgOrden.Click += new System.EventHandler(this.dgOrden_Click);
            // 
            // Buscar_OrdenProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.dgOrden);
            this.Controls.Add(this.btnVerificar);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Buscar_OrdenProduccion";
            this.Text = "Buscar Orden de Producción";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnVerificar;
        private System.Windows.Forms.DataGrid dgOrden;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}