namespace SmartDeviceProject1.Produccion
{
    partial class Huecos_Racks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Huecos_Racks));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pbV1 = new System.Windows.Forms.PictureBox();
            this.pbN1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNiveles = new System.Windows.Forms.Label();
            this.lblVentanas = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEstimado = new System.Windows.Forms.TextBox();
            this.txtHuecos = new System.Windows.Forms.TextBox();
            this.lblOP = new System.Windows.Forms.Label();
            this.lblArticulo = new System.Windows.Forms.Label();
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
            this.menuItem2.Text = "GUARDAR";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // pbV1
            // 
            this.pbV1.Image = ((System.Drawing.Image)(resources.GetObject("pbV1.Image")));
            this.pbV1.Location = new System.Drawing.Point(3, 92);
            this.pbV1.Name = "pbV1";
            this.pbV1.Size = new System.Drawing.Size(76, 43);
            // 
            // pbN1
            // 
            this.pbN1.Image = ((System.Drawing.Image)(resources.GetObject("pbN1.Image")));
            this.pbN1.Location = new System.Drawing.Point(3, 24);
            this.pbN1.Name = "pbN1";
            this.pbN1.Size = new System.Drawing.Size(100, 42);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Niveles";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Ventanas x Nivel";
            // 
            // lblNiveles
            // 
            this.lblNiveles.Location = new System.Drawing.Point(109, 46);
            this.lblNiveles.Name = "lblNiveles";
            this.lblNiveles.Size = new System.Drawing.Size(35, 20);
            this.lblNiveles.Text = "x";
            // 
            // lblVentanas
            // 
            this.lblVentanas.Location = new System.Drawing.Point(85, 115);
            this.lblVentanas.Name = "lblVentanas";
            this.lblVentanas.Size = new System.Drawing.Size(35, 20);
            this.lblVentanas.Text = "x";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 20);
            this.label3.Text = "Cantidad Estimada";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(136, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Huecos";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtEstimado
            // 
            this.txtEstimado.Enabled = false;
            this.txtEstimado.Location = new System.Drawing.Point(3, 237);
            this.txtEstimado.Name = "txtEstimado";
            this.txtEstimado.Size = new System.Drawing.Size(100, 21);
            this.txtEstimado.TabIndex = 8;
            // 
            // txtHuecos
            // 
            this.txtHuecos.Location = new System.Drawing.Point(136, 237);
            this.txtHuecos.Name = "txtHuecos";
            this.txtHuecos.Size = new System.Drawing.Size(100, 21);
            this.txtHuecos.TabIndex = 9;
            this.txtHuecos.Text = "0";
            // 
            // lblOP
            // 
            this.lblOP.Location = new System.Drawing.Point(3, 149);
            this.lblOP.Name = "lblOP";
            this.lblOP.Size = new System.Drawing.Size(100, 20);
            this.lblOP.Text = "Orden Produccion";
            // 
            // lblArticulo
            // 
            this.lblArticulo.Location = new System.Drawing.Point(109, 149);
            this.lblArticulo.Name = "lblArticulo";
            this.lblArticulo.Size = new System.Drawing.Size(127, 65);
            this.lblArticulo.Text = "Articulo";
            // 
            // Huecos_Racks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblArticulo);
            this.Controls.Add(this.lblOP);
            this.Controls.Add(this.txtHuecos);
            this.Controls.Add(this.txtEstimado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblVentanas);
            this.Controls.Add(this.lblNiveles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbN1);
            this.Controls.Add(this.pbV1);
            this.Menu = this.mainMenu1;
            this.Name = "Huecos_Racks";
            this.Text = "Conteo de Huecos";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbV1;
        private System.Windows.Forms.PictureBox pbN1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNiveles;
        private System.Windows.Forms.Label lblVentanas;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEstimado;
        private System.Windows.Forms.TextBox txtHuecos;
        private System.Windows.Forms.Label lblOP;
        private System.Windows.Forms.Label lblArticulo;

    }
}