namespace SmartDeviceProject1.Produccion
{
    partial class Detalle_Orden
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Detalle_Orden));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblOP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEstatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCant = new System.Windows.Forms.Label();
            this.lblProd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = " REGRESAR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "AVANZAR";
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
            // lblOP
            // 
            this.lblOP.Location = new System.Drawing.Point(136, 182);
            this.lblOP.Name = "lblOP";
            this.lblOP.Size = new System.Drawing.Size(100, 20);
            this.lblOP.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(151, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.Text = "Pedido:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblEstatus
            // 
            this.lblEstatus.Location = new System.Drawing.Point(70, 230);
            this.lblEstatus.Name = "lblEstatus";
            this.lblEstatus.Size = new System.Drawing.Size(100, 20);
            this.lblEstatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(70, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Estatus:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCant
            // 
            this.lblCant.Location = new System.Drawing.Point(10, 182);
            this.lblCant.Name = "lblCant";
            this.lblCant.Size = new System.Drawing.Size(127, 20);
            this.lblCant.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblProd
            // 
            this.lblProd.Location = new System.Drawing.Point(2, 130);
            this.lblProd.Name = "lblProd";
            this.lblProd.Size = new System.Drawing.Size(237, 32);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(38, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 20);
            this.label1.Text = "Folio Orden de Producción";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCantidad
            // 
            this.lblCantidad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.Location = new System.Drawing.Point(2, 162);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(143, 20);
            this.lblCantidad.Text = "Piezas a surtir:";
            this.lblCantidad.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblProducto
            // 
            this.lblProducto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblProducto.Location = new System.Drawing.Point(80, 110);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(100, 20);
            this.lblProducto.Text = "Producto:";
            this.lblProducto.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(38, 83);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(170, 20);
            this.textBox1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Detalle_Orden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblOP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblEstatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblCant);
            this.Controls.Add(this.lblProd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Detalle_Orden";
            this.Text = "Detalle";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblOP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblEstatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCant;
        private System.Windows.Forms.Label lblProd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Label textBox1;
    }
}