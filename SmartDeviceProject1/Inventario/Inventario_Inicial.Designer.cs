namespace SmartDeviceProject1.Inventario
{
    partial class Inventario_Inicial
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
            this.lbProd = new System.Windows.Forms.Label();
            this.lbPza = new System.Windows.Forms.Label();
            this.ComboZona = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ckbZona = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbRack = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
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
            this.menuItem2.Text = "GUARDAR";
            // 
            // lbProd
            // 
            this.lbProd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbProd.Location = new System.Drawing.Point(13, 23);
            this.lbProd.Name = "lbProd";
            this.lbProd.Size = new System.Drawing.Size(100, 33);
            this.lbProd.Text = "CODIGO DEL PRODUCTO:";
            // 
            // lbPza
            // 
            this.lbPza.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbPza.Location = new System.Drawing.Point(13, 74);
            this.lbPza.Name = "lbPza";
            this.lbPza.Size = new System.Drawing.Size(100, 20);
            this.lbPza.Text = "PIEZAS";
            // 
            // ComboZona
            // 
            this.ComboZona.Location = new System.Drawing.Point(119, 117);
            this.ComboZona.Name = "ComboZona";
            this.ComboZona.Size = new System.Drawing.Size(100, 22);
            this.ComboZona.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(119, 72);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(119, 35);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 8;
            // 
            // ckbZona
            // 
            this.ckbZona.Location = new System.Drawing.Point(13, 117);
            this.ckbZona.Name = "ckbZona";
            this.ckbZona.Size = new System.Drawing.Size(21, 20);
            this.ckbZona.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(33, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.Text = "ZONA";
            // 
            // ckbRack
            // 
            this.ckbRack.Enabled = false;
            this.ckbRack.Location = new System.Drawing.Point(13, 153);
            this.ckbRack.Name = "ckbRack";
            this.ckbRack.Size = new System.Drawing.Size(21, 20);
            this.ckbRack.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(35, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.Text = "RACK";
            // 
            // comboBox2
            // 
            this.comboBox2.Location = new System.Drawing.Point(119, 150);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(100, 22);
            this.comboBox2.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "ESCUADRA:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(118, 197);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 37;
            // 
            // Inventario_Inicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.ckbRack);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ckbZona);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ComboZona);
            this.Controls.Add(this.lbPza);
            this.Controls.Add(this.lbProd);
            this.Menu = this.mainMenu1;
            this.Name = "Inventario_Inicial";
            this.Text = "Inventario Inicial";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbProd;
        private System.Windows.Forms.Label lbPza;
        private System.Windows.Forms.ComboBox ComboZona;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox ckbZona;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbRack;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
    }
}