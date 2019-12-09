namespace SmartDeviceProject1.Inventario
{
    partial class Confirma_Inventario
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
            this.mi_Cancelar = new System.Windows.Forms.MenuItem();
            this.mi_Siguiente = new System.Windows.Forms.MenuItem();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbZonas = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mi_Cancelar);
            this.mainMenu1.MenuItems.Add(this.mi_Siguiente);
            // 
            // mi_Cancelar
            // 
            this.mi_Cancelar.Text = "CANCELAR";
            this.mi_Cancelar.Click += new System.EventHandler(this.mi_Cancelar_Click);
            // 
            // mi_Siguiente
            // 
            this.mi_Siguiente.Text = "VALIDAR";
            this.mi_Siguiente.Click += new System.EventHandler(this.mi_Siguiente_Click);
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(45, 122);
            this.txtClave.MaxLength = 25;
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(151, 21);
            this.txtClave.TabIndex = 54;
            // 
            // label
            // 
            this.label.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label.Location = new System.Drawing.Point(18, 99);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(209, 20);
            this.label.Text = "CLAVE DE INVENTARIO";
            this.label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(16, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 20);
            this.label2.Text = "UBICACIÓN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbZonas
            // 
            this.cbZonas.Location = new System.Drawing.Point(18, 41);
            this.cbZonas.Name = "cbZonas";
            this.cbZonas.Size = new System.Drawing.Size(209, 22);
            this.cbZonas.TabIndex = 62;
            // 
            // Confirma_Inventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.cbZonas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.label);
            this.Menu = this.mainMenu1;
            this.Name = "Confirma_Inventario";
            this.Text = "Congelar Inventario";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mi_Cancelar;
        private System.Windows.Forms.MenuItem mi_Siguiente;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbZonas;
    }
}