namespace SmartDeviceProject1.Produccion
{
    partial class Asignar_Rack
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtOP = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnLeer = new System.Windows.Forms.Button();
            this.dgPaquetes = new System.Windows.Forms.DataGrid();
            this.lblEstatus = new System.Windows.Forms.Label();
            this.dgInfoProd = new System.Windows.Forms.DataGrid();
            this.panelStock = new System.Windows.Forms.Panel();
            this.cbZonas = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumTag = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnToStock = new System.Windows.Forms.Button();
            this.panelStock.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "REGRESAR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 35);
            this.label1.Text = "INGRESA LA ORDEN DE PRODUCCION";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtOP
            // 
            this.txtOP.Location = new System.Drawing.Point(3, 63);
            this.txtOP.Name = "txtOP";
            this.txtOP.Size = new System.Drawing.Size(134, 21);
            this.txtOP.TabIndex = 1;
            // 
            // btnIngresar
            // 
            this.btnIngresar.Location = new System.Drawing.Point(143, 63);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(94, 21);
            this.btnIngresar.TabIndex = 2;
            this.btnIngresar.Text = "INGRESAR";
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Enabled = false;
            this.btnConectar.Location = new System.Drawing.Point(86, 134);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(26, 38);
            this.btnConectar.TabIndex = 6;
            this.btnConectar.Text = "CONECTAR";
            this.btnConectar.Visible = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnLeer
            // 
            this.btnLeer.Enabled = false;
            this.btnLeer.Location = new System.Drawing.Point(127, 134);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(10, 38);
            this.btnLeer.TabIndex = 7;
            this.btnLeer.Text = "LEER";
            this.btnLeer.Visible = false;
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // dgPaquetes
            // 
            this.dgPaquetes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgPaquetes.Enabled = false;
            this.dgPaquetes.Location = new System.Drawing.Point(4, 97);
            this.dgPaquetes.Name = "dgPaquetes";
            this.dgPaquetes.Size = new System.Drawing.Size(233, 139);
            this.dgPaquetes.TabIndex = 12;
            this.dgPaquetes.Visible = false;
            this.dgPaquetes.Click += new System.EventHandler(this.dgPaquetes_Click);
            // 
            // lblEstatus
            // 
            this.lblEstatus.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblEstatus.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblEstatus.Location = new System.Drawing.Point(37, 97);
            this.lblEstatus.Name = "lblEstatus";
            this.lblEstatus.Size = new System.Drawing.Size(100, 20);
            // 
            // dgInfoProd
            // 
            this.dgInfoProd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgInfoProd.Location = new System.Drawing.Point(27, 198);
            this.dgInfoProd.Name = "dgInfoProd";
            this.dgInfoProd.Size = new System.Drawing.Size(193, 23);
            this.dgInfoProd.TabIndex = 15;
            this.dgInfoProd.Visible = false;
            // 
            // panelStock
            // 
            this.panelStock.BackColor = System.Drawing.SystemColors.Info;
            this.panelStock.Controls.Add(this.cbZonas);
            this.panelStock.Controls.Add(this.label7);
            this.panelStock.Controls.Add(this.txtNumTag);
            this.panelStock.Controls.Add(this.btnCancelar);
            this.panelStock.Controls.Add(this.btnToStock);
            this.panelStock.Enabled = false;
            this.panelStock.Location = new System.Drawing.Point(6, 12);
            this.panelStock.Name = "panelStock";
            this.panelStock.Size = new System.Drawing.Size(231, 191);
            this.panelStock.Visible = false;
            // 
            // cbZonas
            // 
            this.cbZonas.Location = new System.Drawing.Point(12, 80);
            this.cbZonas.Name = "cbZonas";
            this.cbZonas.Size = new System.Drawing.Size(202, 22);
            this.cbZonas.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(21, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 37);
            this.label7.Text = "CAPTURA EL NUMERO DE TAG:";
            // 
            // txtNumTag
            // 
            this.txtNumTag.Location = new System.Drawing.Point(154, 24);
            this.txtNumTag.Name = "txtNumTag";
            this.txtNumTag.Size = new System.Drawing.Size(60, 21);
            this.txtNumTag.TabIndex = 2;
            this.txtNumTag.TextChanged += new System.EventHandler(this.txtNumTag_TextChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(12, 126);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(83, 40);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // btnToStock
            // 
            this.btnToStock.Location = new System.Drawing.Point(133, 126);
            this.btnToStock.Name = "btnToStock";
            this.btnToStock.Size = new System.Drawing.Size(81, 40);
            this.btnToStock.TabIndex = 0;
            this.btnToStock.Text = "GUARDAR";
            this.btnToStock.Click += new System.EventHandler(this.btnToStock_Click_1);
            // 
            // Asignar_Rack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panelStock);
            this.Controls.Add(this.dgInfoProd);
            this.Controls.Add(this.dgPaquetes);
            this.Controls.Add(this.lblEstatus);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.txtOP);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Asignar_Rack";
            this.Text = "Validar Orden de Producción";
            this.panelStock.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOP;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.DataGrid dgPaquetes;
        private System.Windows.Forms.Label lblEstatus;
        private System.Windows.Forms.DataGrid dgInfoProd;
        private System.Windows.Forms.Panel panelStock;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumTag;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnToStock;
        private System.Windows.Forms.ComboBox cbZonas;
    }
}