namespace SmartDeviceProject1.Almacen
{
    partial class Remisiones
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
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnDetener = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.lblremi = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblcodigo = new System.Windows.Forms.Label();
            this.lblPzaRemi = new System.Windows.Forms.Label();
            this.lblPzaInRemi = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblpzaesc = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAddPzaRemi = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "  ";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "GUARDAR";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // btnLeer
            // 
            this.btnLeer.Enabled = false;
            this.btnLeer.Location = new System.Drawing.Point(87, 12);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(72, 20);
            this.btnLeer.TabIndex = 69;
            this.btnLeer.Text = "Leer";
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click_1);
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(165, 12);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(72, 20);
            this.btnDetener.TabIndex = 68;
            this.btnDetener.Text = "Detener";
            this.btnDetener.Visible = false;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(6, 12);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(72, 20);
            this.btnConectar.TabIndex = 67;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click_1);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(6, 38);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(231, 36);
            this.dataGrid1.TabIndex = 70;
            this.dataGrid1.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Remision :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblremi
            // 
            this.lblremi.Location = new System.Drawing.Point(103, 124);
            this.lblremi.Name = "lblremi";
            this.lblremi.Size = new System.Drawing.Size(100, 20);
            this.lblremi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(6, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Codigo :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblcodigo
            // 
            this.lblcodigo.Location = new System.Drawing.Point(103, 152);
            this.lblcodigo.Name = "lblcodigo";
            this.lblcodigo.Size = new System.Drawing.Size(100, 20);
            this.lblcodigo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPzaRemi
            // 
            this.lblPzaRemi.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPzaRemi.Location = new System.Drawing.Point(6, 173);
            this.lblPzaRemi.Name = "lblPzaRemi";
            this.lblPzaRemi.Size = new System.Drawing.Size(100, 30);
            this.lblPzaRemi.Text = "# Piezas en Remision :";
            this.lblPzaRemi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPzaInRemi
            // 
            this.lblPzaInRemi.Location = new System.Drawing.Point(103, 183);
            this.lblPzaInRemi.Name = "lblPzaInRemi";
            this.lblPzaInRemi.Size = new System.Drawing.Size(100, 20);
            this.lblPzaInRemi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(6, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 32);
            this.label7.Text = "# Piezas en       escuadra:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblpzaesc
            // 
            this.lblpzaesc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblpzaesc.Location = new System.Drawing.Point(103, 81);
            this.lblpzaesc.Name = "lblpzaesc";
            this.lblpzaesc.Size = new System.Drawing.Size(100, 20);
            this.lblpzaesc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label9.Location = new System.Drawing.Point(0, 217);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 51);
            this.label9.Text = "Cantidad de Piezas por agregar :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtAddPzaRemi
            // 
            this.txtAddPzaRemi.Location = new System.Drawing.Point(103, 230);
            this.txtAddPzaRemi.Name = "txtAddPzaRemi";
            this.txtAddPzaRemi.Size = new System.Drawing.Size(100, 21);
            this.txtAddPzaRemi.TabIndex = 80;
            // 
            // Remisiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtAddPzaRemi);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblpzaesc);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblPzaInRemi);
            this.Controls.Add(this.lblPzaRemi);
            this.Controls.Add(this.lblcodigo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblremi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnLeer);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.btnConectar);
            this.Menu = this.mainMenu1;
            this.Name = "Remisiones";
            this.Text = "Remisiones";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblremi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblcodigo;
        private System.Windows.Forms.Label lblPzaRemi;
        private System.Windows.Forms.Label lblPzaInRemi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblpzaesc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAddPzaRemi;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;

    }
}