namespace SmartDeviceProject1.Produccion
{
    partial class ValidarOP
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
            this.BTNValidaOP = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dgOrdenP = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.menuItem2.Text = "ACEPTAR";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // BTNValidaOP
            // 
            this.BTNValidaOP.Location = new System.Drawing.Point(153, 25);
            this.BTNValidaOP.Name = "BTNValidaOP";
            this.BTNValidaOP.Size = new System.Drawing.Size(72, 20);
            this.BTNValidaOP.TabIndex = 0;
            this.BTNValidaOP.Text = "VALIDAR";
            this.BTNValidaOP.Click += new System.EventHandler(this.BTNValidaOP_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(135, 21);
            this.textBox1.TabIndex = 1;
            // 
            // dgOrdenP
            // 
            this.dgOrdenP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgOrdenP.Location = new System.Drawing.Point(12, 75);
            this.dgOrdenP.Name = "dgOrdenP";
            this.dgOrdenP.Size = new System.Drawing.Size(213, 82);
            this.dgOrdenP.TabIndex = 2;
            this.dgOrdenP.Visible = false;
            // 
            // label1
            // 
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 20);
            this.label1.Text = "VALIDAR UN PRODUCTO A LA VEZ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.Enabled = false;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.checkBox1.Location = new System.Drawing.Point(28, 177);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(100, 20);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "TOTAL";
            this.checkBox1.Visible = false;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Enabled = false;
            this.checkBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.checkBox2.Location = new System.Drawing.Point(125, 177);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(100, 20);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "PARCIAL";
            this.checkBox2.Visible = false;
            this.checkBox2.CheckStateChanged += new System.EventHandler(this.checkBox2_CheckStateChanged);
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(125, 215);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 6;
            this.textBox2.Visible = false;
            // 
            // label2
            // 
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(12, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.Text = "CANTIDAD";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label2.Visible = false;
            // 
            // ValidarOP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgOrdenP);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BTNValidaOP);
            this.Menu = this.mainMenu1;
            this.Name = "ValidarOP";
            this.Text = "Validar Orden de Produccion";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Button BTNValidaOP;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGrid dgOrdenP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
    }
}