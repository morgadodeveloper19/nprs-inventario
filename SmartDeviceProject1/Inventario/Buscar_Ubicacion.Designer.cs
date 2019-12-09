namespace SmartDeviceProject1.Inventario
{
    partial class Buscar_Ubicacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Buscar_Ubicacion));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(7, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 34);
            this.label1.Text = "Seleccione un elemento para obtener su ubicacion";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(240, 55);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(-3, 86);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(243, 182);
            this.dataGrid1.TabIndex = 4;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // Buscar_Ubicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Menu = this.mainMenu1;
            this.Name = "Buscar_Ubicacion";
            this.Text = "Buscar Ubicacion";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGrid dataGrid1;

    }
}