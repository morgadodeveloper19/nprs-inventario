namespace SmartDeviceProject1.Inventario
{
    partial class Leer_Inventario
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
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.panelStock = new System.Windows.Forms.Panel();
            this.lbDescripcion = new System.Windows.Forms.Label();
            this.lbArticulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConteo = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnToStock = new System.Windows.Forms.Button();
            this.lbInventario = new System.Windows.Forms.Label();
            this.panelStock.SuspendLayout();
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
            this.menuItem2.Enabled = false;
            this.menuItem2.Text = "FINALIZAR";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConectar.BackColor = System.Drawing.Color.White;
            this.btnConectar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnConectar.Location = new System.Drawing.Point(3, 66);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(0, 0);
            this.btnConectar.TabIndex = 76;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 66);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(234, 171);
            this.dataGrid1.TabIndex = 80;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
            // 
            // panelStock
            // 
            this.panelStock.BackColor = System.Drawing.SystemColors.Info;
            this.panelStock.Controls.Add(this.lbDescripcion);
            this.panelStock.Controls.Add(this.lbArticulo);
            this.panelStock.Controls.Add(this.label1);
            this.panelStock.Controls.Add(this.label7);
            this.panelStock.Controls.Add(this.txtConteo);
            this.panelStock.Controls.Add(this.btnCancelar);
            this.panelStock.Controls.Add(this.btnToStock);
            this.panelStock.Enabled = false;
            this.panelStock.Location = new System.Drawing.Point(5, 38);
            this.panelStock.Name = "panelStock";
            this.panelStock.Size = new System.Drawing.Size(231, 227);
            this.panelStock.Visible = false;
            // 
            // lbDescripcion
            // 
            this.lbDescripcion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbDescripcion.Location = new System.Drawing.Point(2, 81);
            this.lbDescripcion.Name = "lbDescripcion";
            this.lbDescripcion.Size = new System.Drawing.Size(225, 69);
            this.lbDescripcion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbArticulo
            // 
            this.lbArticulo.Location = new System.Drawing.Point(85, 48);
            this.lbArticulo.Name = "lbArticulo";
            this.lbArticulo.Size = new System.Drawing.Size(128, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 20);
            this.label1.Text = "ARTICULO : ";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(11, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 18);
            this.label7.Text = "INGRESA EL CONTEO:";
            // 
            // txtConteo
            // 
            this.txtConteo.Location = new System.Drawing.Point(153, 165);
            this.txtConteo.Name = "txtConteo";
            this.txtConteo.Size = new System.Drawing.Size(60, 21);
            this.txtConteo.TabIndex = 2;
            this.txtConteo.TextChanged += new System.EventHandler(this.txtConteo_TextChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(11, 200);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(83, 27);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnToStock
            // 
            this.btnToStock.Location = new System.Drawing.Point(132, 200);
            this.btnToStock.Name = "btnToStock";
            this.btnToStock.Size = new System.Drawing.Size(81, 27);
            this.btnToStock.TabIndex = 0;
            this.btnToStock.Text = "GUARDAR";
            this.btnToStock.Click += new System.EventHandler(this.btnToStock_Click);
            // 
            // lbInventario
            // 
            this.lbInventario.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbInventario.Location = new System.Drawing.Point(5, 0);
            this.lbInventario.Name = "lbInventario";
            this.lbInventario.Size = new System.Drawing.Size(218, 35);
            this.lbInventario.Text = "TOTAL DE ESCUADRAS PARA LA UBICACION ELEGIDA";
            this.lbInventario.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Leer_Inventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lbInventario);
            this.Controls.Add(this.panelStock);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnConectar);
            this.Menu = this.mainMenu1;
            this.Name = "Leer_Inventario";
            this.Text = "Inventario";
            this.panelStock.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Panel panelStock;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtConteo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnToStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbArticulo;
        private System.Windows.Forms.Label lbDescripcion;
        private System.Windows.Forms.Label lbInventario;
    }
}