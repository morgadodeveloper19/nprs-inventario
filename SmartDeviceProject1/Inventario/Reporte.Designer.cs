namespace SmartDeviceProject1.Inventario
{
    partial class Reporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reporte));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgLeidos = new System.Windows.Forms.DataGrid();
            this.label2 = new System.Windows.Forms.Label();
            this.dgNoLeidos = new System.Windows.Forms.DataGrid();
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
            this.menuItem2.Text = "FINALIZAR";
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
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 18);
            this.label1.Text = "TAGS Leidos";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgLeidos
            // 
            this.dgLeidos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgLeidos.Location = new System.Drawing.Point(4, 90);
            this.dgLeidos.Name = "dgLeidos";
            this.dgLeidos.Size = new System.Drawing.Size(233, 75);
            this.dgLeidos.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(4, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 20);
            this.label2.Text = "TAGS No Leidos";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgNoLeidos
            // 
            this.dgNoLeidos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgNoLeidos.Location = new System.Drawing.Point(4, 191);
            this.dgNoLeidos.Name = "dgNoLeidos";
            this.dgNoLeidos.Size = new System.Drawing.Size(233, 73);
            this.dgNoLeidos.TabIndex = 7;
            this.dgNoLeidos.CurrentCellChanged += new System.EventHandler(this.dgNoLeidos_CurrentCellChanged);//JLMQ 
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.dgNoLeidos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgLeidos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Reporte";
            this.Text = "Reporte";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid dgLeidos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGrid dgNoLeidos;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}