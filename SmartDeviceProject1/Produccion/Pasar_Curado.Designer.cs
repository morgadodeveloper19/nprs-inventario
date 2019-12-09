namespace SmartDeviceProject1.Produccion
{
    partial class Pasar_Curado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pasar_Curado));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMod = new System.Windows.Forms.LinkLabel();
            this.txtCant = new System.Windows.Forms.TextBox();
            this.LblRenglon = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblProd = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.lblFolio = new System.Windows.Forms.Label();
            this.lblOP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.menuItem2.Text = "LIBERAR";
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
            this.lblMod.Location = new System.Drawing.Point(150, 204);
            this.lblMod.Name = "lblMod";
            this.lblMod.Size = new System.Drawing.Size(82, 20);
            this.lblMod.TabIndex = 15;
            this.lblMod.Text = "Modificar";
            this.lblMod.Click += new System.EventHandler(this.lblMod_Click);
            // 
            // txtCant
            // 
            this.txtCant.Enabled = false;
            this.txtCant.Location = new System.Drawing.Point(44, 204);
            this.txtCant.Name = "txtCant";
            this.txtCant.Size = new System.Drawing.Size(100, 21);
            this.txtCant.TabIndex = 14;
            this.txtCant.TextChanged += new System.EventHandler(this.txtCant_TextChanged);
            // 
            // LblRenglon
            // 
            this.LblRenglon.Location = new System.Drawing.Point(132, 240);
            this.LblRenglon.Name = "LblRenglon";
            this.LblRenglon.Size = new System.Drawing.Size(100, 20);
            this.LblRenglon.Visible = false;
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(106, 145);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(100, 20);
            this.lblId.Visible = false;
            // 
            // lblProd
            // 
            this.lblProd.Location = new System.Drawing.Point(0, 141);
            this.lblProd.Name = "lblProd";
            this.lblProd.Size = new System.Drawing.Size(237, 32);
            // 
            // lblCantidad
            // 
            this.lblCantidad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.Location = new System.Drawing.Point(59, 181);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(138, 20);
            this.lblCantidad.Text = "Cantidad Producida";
            // 
            // lblProducto
            // 
            this.lblProducto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblProducto.Location = new System.Drawing.Point(78, 121);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(66, 20);
            this.lblProducto.Text = "Producto";
            // 
            // lblFolio
            // 
            this.lblFolio.Location = new System.Drawing.Point(4, 97);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(111, 20);
            this.lblFolio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblOP
            // 
            this.lblOP.Location = new System.Drawing.Point(132, 97);
            this.lblOP.Name = "lblOP";
            this.lblOP.Size = new System.Drawing.Size(100, 20);
            this.lblOP.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(144, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.Text = "Pedido:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 32);
            this.label1.Text = "Folio Orden de Producción";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Pasar_Curado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblFolio);
            this.Controls.Add(this.lblOP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMod);
            this.Controls.Add(this.txtCant);
            this.Controls.Add(this.LblRenglon);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblProd);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Pasar_Curado";
            this.Text = "Pasar a Curado";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lblMod;
        private System.Windows.Forms.TextBox txtCant;
        private System.Windows.Forms.Label LblRenglon;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblProd;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.Label lblOP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}