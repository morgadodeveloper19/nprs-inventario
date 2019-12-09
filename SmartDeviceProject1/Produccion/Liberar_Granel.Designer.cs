namespace SmartDeviceProject1.Produccion
{
    partial class Liberar_Granel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Liberar_Granel));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblOrdProd = new System.Windows.Forms.Label();
            this.lblPedido = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMerma = new System.Windows.Forms.TextBox();
            this.btnMerma = new System.Windows.Forms.Button();
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
            this.menuItem2.Text = "AVANZAR";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblOrdProd
            // 
            this.lblOrdProd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblOrdProd.Location = new System.Drawing.Point(109, 65);
            this.lblOrdProd.Name = "lblOrdProd";
            this.lblOrdProd.Size = new System.Drawing.Size(100, 20);
            this.lblOrdProd.Text = "# OP";
            // 
            // lblPedido
            // 
            this.lblPedido.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPedido.Location = new System.Drawing.Point(3, 65);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(100, 20);
            this.lblPedido.Text = "Pedido";
            // 
            // lblCliente
            // 
            this.lblCliente.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCliente.Location = new System.Drawing.Point(1, 108);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(237, 57);
            this.lblCliente.Text = "Cliente";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(3, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(231, 32);
            this.label5.Text = "Producto:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(3, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "# de Pieza: ";
            // 
            // txtMerma
            // 
            this.txtMerma.Enabled = false;
            this.txtMerma.Location = new System.Drawing.Point(122, 220);
            this.txtMerma.Name = "txtMerma";
            this.txtMerma.Size = new System.Drawing.Size(112, 21);
            this.txtMerma.TabIndex = 12;
            // 
            // btnMerma
            // 
            this.btnMerma.Location = new System.Drawing.Point(122, 247);
            this.btnMerma.Name = "btnMerma";
            this.btnMerma.Size = new System.Drawing.Size(112, 20);
            this.btnMerma.TabIndex = 13;
            this.btnMerma.Text = "Reportar Merma";
            this.btnMerma.Click += new System.EventHandler(this.btnMerma_Click);
            // 
            // Liberar_Granel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnMerma);
            this.Controls.Add(this.txtMerma);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.lblOrdProd);
            this.Controls.Add(this.lblPedido);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Liberar_Granel";
            this.Text = "Liberar Granel";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblOrdProd;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMerma;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Button btnMerma;
    }
}