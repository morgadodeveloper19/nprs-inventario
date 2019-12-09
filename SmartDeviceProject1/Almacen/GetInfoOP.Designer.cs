namespace SmartDeviceProject1.Almacen
{
    partial class GetInfoOP
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
            this.btnValidar = new System.Windows.Forms.Button();
            this.txtOP = new System.Windows.Forms.TextBox();
            this.dgOrden = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnValidar
            // 
            this.btnValidar.Location = new System.Drawing.Point(154, 29);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(77, 20);
            this.btnValidar.TabIndex = 1;
            this.btnValidar.Text = "INGRESAR";
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // txtOP
            // 
            this.txtOP.Location = new System.Drawing.Point(15, 29);
            this.txtOP.Name = "txtOP";
            this.txtOP.Size = new System.Drawing.Size(133, 21);
            this.txtOP.TabIndex = 2;
            // 
            // dgOrden
            // 
            this.dgOrden.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgOrden.Enabled = false;
            this.dgOrden.Location = new System.Drawing.Point(15, 80);
            this.dgOrden.Name = "dgOrden";
            this.dgOrden.Size = new System.Drawing.Size(216, 171);
            this.dgOrden.TabIndex = 3;
            this.dgOrden.Visible = false;
            this.dgOrden.CurrentCellChanged += new System.EventHandler(this.dgOrden_CurrentCellChanged);
            // 
            // label1
            // 
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(15, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 20);
            this.label1.Text = "VALIDA ALGUN PRODUCTO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Visible = false;
            // 
            // GetInfoOP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgOrden);
            this.Controls.Add(this.txtOP);
            this.Controls.Add(this.btnValidar);
            this.Menu = this.mainMenu1;
            this.Name = "GetInfoOP";
            this.Text = "Validar Orden de Produccion";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.TextBox txtOP;
        private System.Windows.Forms.DataGrid dgOrden;
        private System.Windows.Forms.Label label1;
    }
}