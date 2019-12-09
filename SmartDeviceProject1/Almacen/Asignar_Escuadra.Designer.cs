namespace SmartDeviceProject1.Almacen
{
    partial class Asignar_Escuadra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Asignar_Escuadra));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mi_Regresar = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEsc = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblCta = new System.Windows.Forms.Label();
            this.lblCodProd = new System.Windows.Forms.Label();
            this.lblDescProd = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mi_Regresar);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // mi_Regresar
            // 
            this.mi_Regresar.Text = "REGRESAR";
            this.mi_Regresar.Click += new System.EventHandler(this.mi_Regresar_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Enabled = false;
            this.menuItem1.Text = "  ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(226, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(7, 69);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(225, 30);
            this.lblHeader.Text = "Seleccione el Producto que desea asignar";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(7, 102);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(225, 156);
            this.dataGrid1.TabIndex = 3;
            this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(17, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 152);
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnEsc);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.lblCta);
            this.panel2.Controls.Add(this.lblCodProd);
            this.panel2.Controls.Add(this.lblDescProd);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 145);
            // 
            // btnEsc
            // 
            this.btnEsc.Location = new System.Drawing.Point(3, 107);
            this.btnEsc.Name = "btnEsc";
            this.btnEsc.Size = new System.Drawing.Size(90, 30);
            this.btnEsc.TabIndex = 4;
            this.btnEsc.Text = "Cancelar";
            this.btnEsc.Click += new System.EventHandler(this.btnEsc_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(107, 107);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 30);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Aceptar";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblCta
            // 
            this.lblCta.Location = new System.Drawing.Point(125, 65);
            this.lblCta.Name = "lblCta";
            this.lblCta.Size = new System.Drawing.Size(72, 39);
            this.lblCta.Text = "Ctad";
            // 
            // lblCodProd
            // 
            this.lblCodProd.Location = new System.Drawing.Point(4, 65);
            this.lblCodProd.Name = "lblCodProd";
            this.lblCodProd.Size = new System.Drawing.Size(115, 39);
            this.lblCodProd.Text = "codProd";
            // 
            // lblDescProd
            // 
            this.lblDescProd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescProd.Location = new System.Drawing.Point(4, 13);
            this.lblDescProd.Name = "lblDescProd";
            this.lblDescProd.Size = new System.Drawing.Size(193, 42);
            this.lblDescProd.Text = "descProd";
            this.lblDescProd.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Asignar_Escuadra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Asignar_Escuadra";
            this.Text = "Asignar Escuadra";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.MenuItem mi_Regresar;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDescProd;
        private System.Windows.Forms.Label lblCodProd;
        private System.Windows.Forms.Label lblCta;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnEsc;
    }
}