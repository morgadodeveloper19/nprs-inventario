namespace SmartDeviceProject1.Almacen
{
    partial class CicloRemisiones
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
            this.cbProdBusq = new System.Windows.Forms.ComboBox();
            this.cbOrdProdTerm = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgTarimas = new System.Windows.Forms.DataGrid();
            this.dgStock = new System.Windows.Forms.DataGrid();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtEPC = new System.Windows.Forms.TextBox();
            this.btnMover = new System.Windows.Forms.Button();
            this.btnLeer = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.panelStock = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCantToStock = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnToStock = new System.Windows.Forms.Button();
            this.btnDetener = new System.Windows.Forms.Button();
            this.panelStock.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbProdBusq
            // 
            this.cbProdBusq.Location = new System.Drawing.Point(7, 32);
            this.cbProdBusq.Name = "cbProdBusq";
            this.cbProdBusq.Size = new System.Drawing.Size(221, 22);
            this.cbProdBusq.TabIndex = 1;
            this.cbProdBusq.SelectedIndexChanged += new System.EventHandler(this.cbProdBusq_SelectedIndexChanged);
            // 
            // cbOrdProdTerm
            // 
            this.cbOrdProdTerm.Location = new System.Drawing.Point(6, 81);
            this.cbOrdProdTerm.Name = "cbOrdProdTerm";
            this.cbOrdProdTerm.Size = new System.Drawing.Size(222, 22);
            this.cbOrdProdTerm.TabIndex = 2;
            this.cbOrdProdTerm.SelectedIndexChanged += new System.EventHandler(this.cbOrdProdTerm_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 20);
            this.label1.Text = "Remisiones Disponibles";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(7, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 19);
            this.label2.Text = "Productos de la Remision";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgTarimas
            // 
            this.dgTarimas.BackgroundColor = System.Drawing.Color.Silver;
            this.dgTarimas.Enabled = false;
            this.dgTarimas.Location = new System.Drawing.Point(6, 223);
            this.dgTarimas.Name = "dgTarimas";
            this.dgTarimas.Size = new System.Drawing.Size(218, 56);
            this.dgTarimas.TabIndex = 3;
            this.dgTarimas.Visible = false;
            this.dgTarimas.DoubleClick += new System.EventHandler(this.dgTarimas_DoubleClick);
            this.dgTarimas.Click += new System.EventHandler(this.dgTarimas_Click);
            // 
            // dgStock
            // 
            this.dgStock.BackgroundColor = System.Drawing.Color.Silver;
            this.dgStock.Location = new System.Drawing.Point(3, 326);
            this.dgStock.Name = "dgStock";
            this.dgStock.Size = new System.Drawing.Size(218, 24);
            this.dgStock.TabIndex = 6;
            this.dgStock.Visible = false;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(22, 118);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(87, 30);
            this.btnConectar.TabIndex = 9;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // txtEPC
            // 
            this.txtEPC.Location = new System.Drawing.Point(44, 165);
            this.txtEPC.Name = "txtEPC";
            this.txtEPC.Size = new System.Drawing.Size(177, 21);
            this.txtEPC.TabIndex = 10;
            // 
            // btnMover
            // 
            this.btnMover.Location = new System.Drawing.Point(159, 191);
            this.btnMover.Name = "btnMover";
            this.btnMover.Size = new System.Drawing.Size(52, 30);
            this.btnMover.TabIndex = 11;
            this.btnMover.Text = "Mover";
            this.btnMover.Visible = false;
            this.btnMover.Click += new System.EventHandler(this.btnMover_Click);
            // 
            // btnLeer
            // 
            this.btnLeer.Location = new System.Drawing.Point(125, 118);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(83, 30);
            this.btnLeer.TabIndex = 15;
            this.btnLeer.Text = "Leer tag";
            this.btnLeer.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblStatus.Location = new System.Drawing.Point(25, 197);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(180, 20);
            this.lblStatus.Text = "STATUS";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(2, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 20);
            this.label4.Text = "Tag:";
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
            this.menuItem2.Text = " ";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // panelStock
            // 
            this.panelStock.BackColor = System.Drawing.SystemColors.Info;
            this.panelStock.Controls.Add(this.label7);
            this.panelStock.Controls.Add(this.lblCantidad);
            this.panelStock.Controls.Add(this.label5);
            this.panelStock.Controls.Add(this.txtCantToStock);
            this.panelStock.Controls.Add(this.btnCancelar);
            this.panelStock.Controls.Add(this.btnToStock);
            this.panelStock.Enabled = false;
            this.panelStock.Location = new System.Drawing.Point(10, 37);
            this.panelStock.Name = "panelStock";
            this.panelStock.Size = new System.Drawing.Size(215, 180);
            this.panelStock.Visible = false;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(48, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 20);
            this.label7.Text = "Cantidad:";
            // 
            // lblCantidad
            // 
            this.lblCantidad.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblCantidad.Location = new System.Drawing.Point(16, 11);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(186, 50);
            this.lblCantidad.Text = "de 8000";
            this.lblCantidad.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(48, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 39);
            this.label5.Text = "¿CUÁNTAS PIEZAS QUIERES AGREGAR?";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtCantToStock
            // 
            this.txtCantToStock.Location = new System.Drawing.Point(113, 100);
            this.txtCantToStock.Name = "txtCantToStock";
            this.txtCantToStock.Size = new System.Drawing.Size(60, 21);
            this.txtCantToStock.TabIndex = 2;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(23, 146);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 20);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnToStock
            // 
            this.btnToStock.Location = new System.Drawing.Point(130, 146);
            this.btnToStock.Name = "btnToStock";
            this.btnToStock.Size = new System.Drawing.Size(72, 20);
            this.btnToStock.TabIndex = 0;
            this.btnToStock.Text = "Agregar";
            this.btnToStock.Click += new System.EventHandler(this.btnToStock_Click);
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(125, 118);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(83, 30);
            this.btnDetener.TabIndex = 19;
            this.btnDetener.Text = "Detener";
            this.btnDetener.Visible = false;
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // CicloRemisiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.panelStock);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnMover);
            this.Controls.Add(this.txtEPC);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.dgStock);
            this.Controls.Add(this.dgTarimas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbOrdProdTerm);
            this.Controls.Add(this.cbProdBusq);
            this.Menu = this.mainMenu1;
            this.Name = "CicloRemisiones";
            this.Text = "Surtir Remisión";
            this.panelStock.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ComboBox cbProdBusq;
        private System.Windows.Forms.ComboBox cbOrdProdTerm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGrid dgTarimas;
		private System.Windows.Forms.DataGrid dgStock;
		private System.Windows.Forms.Button btnConectar;
		private System.Windows.Forms.TextBox txtEPC;
        private System.Windows.Forms.Button btnMover;
		private System.Windows.Forms.Button btnLeer;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Panel panelStock;
		private System.Windows.Forms.Button btnToStock;
		private System.Windows.Forms.Button btnCancelar;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtCantToStock;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnDetener;
    }
}