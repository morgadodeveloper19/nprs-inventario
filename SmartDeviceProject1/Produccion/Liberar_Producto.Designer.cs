namespace SmartDeviceProject1.Produccion
{
    partial class Liberar_Producto
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
            this.label12 = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblResistencia = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblMedida = new System.Windows.Forms.Label();
            this.lbTipo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTarima = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOrdProd = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPedido = new System.Windows.Forms.Label();
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
            this.menuItem1.Text = "Regresar";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Asignar Escuadra";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(3, 292);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 28);
            this.label12.Text = "Cantidad Liberada:";
            // 
            // lblQty
            // 
            this.lblQty.Location = new System.Drawing.Point(70, 296);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(55, 20);
            this.lblQty.Text = "Cantidad";
            // 
            // lblCodigo
            // 
            this.lblCodigo.Location = new System.Drawing.Point(6, 217);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(100, 32);
            this.lblCodigo.Text = "Codigo";
            // 
            // lblResistencia
            // 
            this.lblResistencia.Location = new System.Drawing.Point(131, 217);
            this.lblResistencia.Name = "lblResistencia";
            this.lblResistencia.Size = new System.Drawing.Size(91, 20);
            this.lblResistencia.Text = "Resistencia";
            // 
            // lblColor
            // 
            this.lblColor.Location = new System.Drawing.Point(6, 185);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(100, 20);
            this.lblColor.Text = "Color";
            // 
            // lblMedida
            // 
            this.lblMedida.Location = new System.Drawing.Point(121, 185);
            this.lblMedida.Name = "lblMedida";
            this.lblMedida.Size = new System.Drawing.Size(91, 32);
            this.lblMedida.Text = "Medida";
            // 
            // lbTipo
            // 
            this.lbTipo.Location = new System.Drawing.Point(3, 260);
            this.lbTipo.Name = "lbTipo";
            this.lbTipo.Size = new System.Drawing.Size(221, 32);
            this.lbTipo.Text = "Tipo";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(3, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(219, 46);
            this.label5.Text = "Producto:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTarima
            // 
            this.lblTarima.Location = new System.Drawing.Point(112, 119);
            this.lblTarima.Name = "lblTarima";
            this.lblTarima.Size = new System.Drawing.Size(100, 20);
            this.lblTarima.Text = "# Tarima";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(5, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "# de Tarima: ";
            // 
            // lblCliente
            // 
            this.lblCliente.Location = new System.Drawing.Point(59, 73);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(163, 35);
            this.lblCliente.Text = "Cliente";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(5, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Cliente: ";
            // 
            // lblOrdProd
            // 
            this.lblOrdProd.Location = new System.Drawing.Point(112, 41);
            this.lblOrdProd.Name = "lblOrdProd";
            this.lblOrdProd.Size = new System.Drawing.Size(100, 20);
            this.lblOrdProd.Text = "# OP";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(6, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 35);
            this.label2.Text = "# Orden de Producción: ";
            // 
            // lblPedido
            // 
            this.lblPedido.Location = new System.Drawing.Point(112, 11);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(100, 20);
            this.lblPedido.Text = "Pedido";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Pedido:";
            // 
            // Liberar_Producto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblResistencia);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.lblMedida);
            this.Controls.Add(this.lbTipo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTarima);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblOrdProd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPedido);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Liberar_Producto";
            this.Text = "Liberar Producto";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblResistencia;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblMedida;
        private System.Windows.Forms.Label lbTipo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTarima;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOrdProd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}