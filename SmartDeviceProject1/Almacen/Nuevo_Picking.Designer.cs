namespace SmartDeviceProject1.Almacen
{
    partial class Nuevo_Picking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Nuevo_Picking));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtRemision = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.dgPaquetes = new System.Windows.Forms.DataGrid();
            this.cbOrdenProd = new System.Windows.Forms.ComboBox();
            this.panelStock = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCantToStock = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnToStock = new System.Windows.Forms.Button();
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
            this.menuItem2.Text = "VALIDAR";
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
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(4, 69);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(233, 36);
            this.lblHeader.Text = "SELECCIONA LA REMISIÓN A VALIDAR";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtRemision
            // 
            this.txtRemision.Enabled = false;
            this.txtRemision.Location = new System.Drawing.Point(4, 109);
            this.txtRemision.Name = "txtRemision";
            this.txtRemision.Size = new System.Drawing.Size(165, 21);
            this.txtRemision.TabIndex = 3;
            this.txtRemision.Visible = false;
            this.txtRemision.TextChanged += new System.EventHandler(this.txtRemision_TextChanged);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Enabled = false;
            this.btnAceptar.Location = new System.Drawing.Point(175, 110);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(61, 20);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Buscar";
            this.btnAceptar.Visible = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // dgPaquetes
            // 
            this.dgPaquetes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgPaquetes.Location = new System.Drawing.Point(4, 136);
            this.dgPaquetes.Name = "dgPaquetes";
            this.dgPaquetes.Size = new System.Drawing.Size(232, 123);
            this.dgPaquetes.TabIndex = 5;
            this.dgPaquetes.Visible = false;
            this.dgPaquetes.CurrentCellChanged += new System.EventHandler(this.dgPaquetes_CurrentCellChanged);
            this.dgPaquetes.Click += new System.EventHandler(this.dgPaquetes_Click);
            // 
            // cbOrdenProd
            // 
            this.cbOrdenProd.Location = new System.Drawing.Point(21, 108);
            this.cbOrdenProd.Name = "cbOrdenProd";
            this.cbOrdenProd.Size = new System.Drawing.Size(202, 22);
            this.cbOrdenProd.TabIndex = 19;
            this.cbOrdenProd.SelectedIndexChanged += new System.EventHandler(this.cbOrdenProd_SelectedIndexChanged);
            // 
            // panelStock
            // 
            this.panelStock.BackColor = System.Drawing.SystemColors.Info;
            this.panelStock.Controls.Add(this.label7);
            this.panelStock.Controls.Add(this.label5);
            this.panelStock.Controls.Add(this.txtCantToStock);
            this.panelStock.Controls.Add(this.btnCancelar);
            this.panelStock.Controls.Add(this.btnToStock);
            this.panelStock.Enabled = false;
            this.panelStock.Location = new System.Drawing.Point(18, 68);
            this.panelStock.Name = "panelStock";
            this.panelStock.Size = new System.Drawing.Size(205, 131);
            this.panelStock.Visible = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(38, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 20);
            this.label7.Text = "TAG:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(7, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 35);
            this.label5.Text = "INGRESA EL NUMERO DE TAG PARA ESTA REMISION";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtCantToStock
            // 
            this.txtCantToStock.Location = new System.Drawing.Point(103, 51);
            this.txtCantToStock.Name = "txtCantToStock";
            this.txtCantToStock.Size = new System.Drawing.Size(60, 21);
            this.txtCantToStock.TabIndex = 2;
            this.txtCantToStock.TextChanged += new System.EventHandler(this.txtCantToStock_TextChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(15, 84);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 20);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // btnToStock
            // 
            this.btnToStock.Location = new System.Drawing.Point(122, 84);
            this.btnToStock.Name = "btnToStock";
            this.btnToStock.Size = new System.Drawing.Size(72, 20);
            this.btnToStock.TabIndex = 0;
            this.btnToStock.Text = "Agregar";
            this.btnToStock.Click += new System.EventHandler(this.btnToStock_Click_1);
            // 
            // Nuevo_Picking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panelStock);
            this.Controls.Add(this.cbOrdenProd);
            this.Controls.Add(this.dgPaquetes);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtRemision);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Nuevo_Picking";
            this.Text = "Validar Remisión";
            this.panelStock.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TextBox txtRemision;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.DataGrid dgPaquetes;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ComboBox cbOrdenProd;
        private System.Windows.Forms.Panel panelStock;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCantToStock;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnToStock;
    }
}