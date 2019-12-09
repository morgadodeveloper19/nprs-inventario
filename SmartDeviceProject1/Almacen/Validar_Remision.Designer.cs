namespace SmartDeviceProject1.Almacen
{
    partial class Validar_Remision
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
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnDetener = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRemi = new System.Windows.Forms.Label();
            this.lblCodProd = new System.Windows.Forms.Label();
            this.lblPza = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEsc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "SALIR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Enabled = false;
            this.menuItem2.Text = "ACEPTAR";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(4, 162);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(77, 20);
            this.btnConectar.TabIndex = 0;
            this.btnConectar.Text = "CONECTAR";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnLeer
            // 
            this.btnLeer.Location = new System.Drawing.Point(87, 162);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(72, 20);
            this.btnLeer.TabIndex = 1;
            this.btnLeer.Text = "LEER";
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(165, 162);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(72, 20);
            this.btnDetener.TabIndex = 2;
            this.btnDetener.Text = "DETENER";
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 188);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(233, 77);
            this.dataGrid1.TabIndex = 3;
            this.dataGrid1.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.Text = "Remision:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(5, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 35);
            this.label2.Text = "Codigo de Producto:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(5, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 33);
            this.label3.Text = "Cantidad de Piezas:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblRemi
            // 
            this.lblRemi.Location = new System.Drawing.Point(111, 13);
            this.lblRemi.Name = "lblRemi";
            this.lblRemi.Size = new System.Drawing.Size(100, 20);
            this.lblRemi.Text = "Remision";
            this.lblRemi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCodProd
            // 
            this.lblCodProd.Location = new System.Drawing.Point(111, 56);
            this.lblCodProd.Name = "lblCodProd";
            this.lblCodProd.Size = new System.Drawing.Size(100, 20);
            this.lblCodProd.Text = "Codigo";
            this.lblCodProd.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPza
            // 
            this.lblPza.Location = new System.Drawing.Point(111, 103);
            this.lblPza.Name = "lblPza";
            this.lblPza.Size = new System.Drawing.Size(100, 20);
            this.lblPza.Text = "Piezas";
            this.lblPza.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(5, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "# Escuadra ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEsc
            // 
            this.txtEsc.Location = new System.Drawing.Point(111, 126);
            this.txtEsc.Name = "txtEsc";
            this.txtEsc.Size = new System.Drawing.Size(100, 21);
            this.txtEsc.TabIndex = 8;
            this.txtEsc.TextChanged += new System.EventHandler(this.txtEsc_TextChanged);
            // 
            // Validar_Remision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtEsc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblPza);
            this.Controls.Add(this.lblCodProd);
            this.Controls.Add(this.lblRemi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnConectar);
            this.Menu = this.mainMenu1;
            this.Name = "Validar_Remision";
            this.Text = "Validar Remision";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRemi;
        private System.Windows.Forms.Label lblCodProd;
        private System.Windows.Forms.Label lblPza;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEsc;
    }
}