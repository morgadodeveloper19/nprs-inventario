namespace SmartDeviceProject1.Produccion
{
    partial class Huecos_NoRack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Huecos_NoRack));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.lblFolio = new System.Windows.Forms.Label();
            this.lblOP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblProd = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtActual = new System.Windows.Forms.TextBox();
            this.txtMerma = new System.Windows.Forms.TextBox();
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
            this.menuItem2.Text = "CONTAR";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // lblFolio
            // 
            this.lblFolio.Location = new System.Drawing.Point(4, 96);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(111, 20);
            this.lblFolio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblOP
            // 
            this.lblOP.Location = new System.Drawing.Point(132, 96);
            this.lblOP.Name = "lblOP";
            this.lblOP.Size = new System.Drawing.Size(100, 20);
            this.lblOP.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(144, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.Text = "Pedido:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 32);
            this.label1.Text = "Folio Orden de Producción";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(106, 144);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(100, 20);
            this.lblId.Visible = false;
            // 
            // lblProd
            // 
            this.lblProd.Location = new System.Drawing.Point(0, 140);
            this.lblProd.Name = "lblProd";
            this.lblProd.Size = new System.Drawing.Size(237, 32);
            // 
            // lblProducto
            // 
            this.lblProducto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblProducto.Location = new System.Drawing.Point(78, 120);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(66, 20);
            this.lblProducto.Text = "Producto";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(4, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "En Produccion:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(163, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.Text = "Huecos:";
            // 
            // txtActual
            // 
            this.txtActual.Enabled = false;
            this.txtActual.Location = new System.Drawing.Point(4, 195);
            this.txtActual.Name = "txtActual";
            this.txtActual.Size = new System.Drawing.Size(100, 21);
            this.txtActual.TabIndex = 18;
            // 
            // txtMerma
            // 
            this.txtMerma.Location = new System.Drawing.Point(139, 195);
            this.txtMerma.Name = "txtMerma";
            this.txtMerma.Size = new System.Drawing.Size(100, 21);
            this.txtMerma.TabIndex = 19;
            // 
            // Huecos_NoRack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtMerma);
            this.Controls.Add(this.txtActual);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblFolio);
            this.Controls.Add(this.lblOP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblProd);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Huecos_NoRack";
            this.Text = "Contar Huecos";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.Label lblOP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblProd;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtActual;
        private System.Windows.Forms.TextBox txtMerma;
    }
}