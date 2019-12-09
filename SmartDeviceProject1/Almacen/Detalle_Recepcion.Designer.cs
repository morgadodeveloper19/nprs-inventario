namespace SmartDeviceProject1.Almacen
{
    partial class Detalle_Recepcion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Detalle_Recepcion));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMod = new System.Windows.Forms.LinkLabel();
            this.labelOrdenProd = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.lblOP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblProd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.btnMerma = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
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
            this.menuItem2.Text = "SIGUIENTE";
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
            // lblMod
            // 
            this.lblMod.Location = new System.Drawing.Point(180, 215);
            this.lblMod.Name = "lblMod";
            this.lblMod.Size = new System.Drawing.Size(56, 20);
            this.lblMod.TabIndex = 70;
            this.lblMod.Text = "Modificar";
            this.lblMod.Click += new System.EventHandler(this.lblMod_Click);
            // 
            // labelOrdenProd
            // 
            this.labelOrdenProd.Location = new System.Drawing.Point(20, 84);
            this.labelOrdenProd.Name = "labelOrdenProd";
            this.labelOrdenProd.Size = new System.Drawing.Size(100, 20);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Enabled = false;
            this.txtCantidad.Location = new System.Drawing.Point(122, 214);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(50, 21);
            this.txtCantidad.TabIndex = 69;
            this.txtCantidad.TextChanged += new System.EventHandler(this.txtCantidad_TextChanged);
            // 
            // lblOP
            // 
            this.lblOP.Location = new System.Drawing.Point(136, 82);
            this.lblOP.Name = "lblOP";
            this.lblOP.Size = new System.Drawing.Size(100, 20);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(167, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 18);
            this.label2.Text = "Pedido:";
            // 
            // lblProd
            // 
            this.lblProd.Location = new System.Drawing.Point(2, 152);
            this.lblProd.Name = "lblProd";
            this.lblProd.Size = new System.Drawing.Size(237, 32);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(2, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 20);
            this.label1.Text = "Orden de Producción";
            // 
            // lblCantidad
            // 
            this.lblCantidad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.Location = new System.Drawing.Point(80, 192);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(119, 20);
            this.lblCantidad.Text = "Cantidad:";
            this.lblCantidad.ParentChanged += new System.EventHandler(this.lblCantidad_ParentChanged);
            // 
            // lblProducto
            // 
            this.lblProducto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblProducto.Location = new System.Drawing.Point(80, 134);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(100, 20);
            this.lblProducto.Text = "Producto:";
            // 
            // btnMerma
            // 
            this.btnMerma.BackColor = System.Drawing.Color.White;
            this.btnMerma.Enabled = false;
            this.btnMerma.Location = new System.Drawing.Point(122, 238);
            this.btnMerma.Name = "btnMerma";
            this.btnMerma.Size = new System.Drawing.Size(118, 28);
            this.btnMerma.TabIndex = 79;
            this.btnMerma.Text = "Reportar Merma";
            this.btnMerma.Click += new System.EventHandler(this.btnMerma_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(3, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 51);
            this.label3.Text = "Ingresa Cantidad real menos la merma";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label3.ParentChanged += new System.EventHandler(this.label3_ParentChanged);
            // 
            // Detalle_Recepcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnMerma);
            this.Controls.Add(this.lblMod);
            this.Controls.Add(this.labelOrdenProd);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.lblOP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblProd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Detalle_Recepcion";
            this.Text = "Detalle de Producto";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lblMod;
        private System.Windows.Forms.Label labelOrdenProd;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label lblOP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Button btnMerma;
        private System.Windows.Forms.Label label3;
    }
}