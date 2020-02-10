namespace SmartDeviceProject1.Inventario
{
    partial class Reclasificacion
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSalida = new System.Windows.Forms.TextBox();
            this.txtIngreso = new System.Windows.Forms.TextBox();
            this.lbCodigo = new System.Windows.Forms.Label();
            this.lbDescripcion = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtReclasificacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPedido = new System.Windows.Forms.TextBox();
            this.lbPedido = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "CANCELAR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Enabled = false;
            this.menuItem2.Text = "RECLASIFICAR";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 20);
            this.label1.Text = "ESCUADRA DE SALIDA";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(5, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.Text = "ESCUADRA DE INGRESO";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Visible = false;
            // 
            // txtSalida
            // 
            this.txtSalida.Location = new System.Drawing.Point(153, -1);
            this.txtSalida.Name = "txtSalida";
            this.txtSalida.Size = new System.Drawing.Size(80, 21);
            this.txtSalida.TabIndex = 4;
            this.txtSalida.TextChanged += new System.EventHandler(this.txtSalida_TextChanged);
            // 
            // txtIngreso
            // 
            this.txtIngreso.Enabled = false;
            this.txtIngreso.Location = new System.Drawing.Point(153, 112);
            this.txtIngreso.Name = "txtIngreso";
            this.txtIngreso.Size = new System.Drawing.Size(82, 21);
            this.txtIngreso.TabIndex = 5;
            this.txtIngreso.Visible = false;
            this.txtIngreso.TextChanged += new System.EventHandler(this.txtIngreso_TextChanged);
            // 
            // lbCodigo
            // 
            this.lbCodigo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbCodigo.ForeColor = System.Drawing.Color.Red;
            this.lbCodigo.Location = new System.Drawing.Point(6, 23);
            this.lbCodigo.Name = "lbCodigo";
            this.lbCodigo.Size = new System.Drawing.Size(227, 20);
            this.lbCodigo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbDescripcion
            // 
            this.lbDescripcion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbDescripcion.ForeColor = System.Drawing.Color.Blue;
            this.lbDescripcion.Location = new System.Drawing.Point(5, 60);
            this.lbDescripcion.Name = "lbDescripcion";
            this.lbDescripcion.Size = new System.Drawing.Size(234, 36);
            this.lbDescripcion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCodigo
            // 
            this.lblCodigo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.ForeColor = System.Drawing.Color.Red;
            this.lblCodigo.Location = new System.Drawing.Point(6, 136);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(231, 20);
            this.lblCodigo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblDescripcion.ForeColor = System.Drawing.Color.Blue;
            this.lblDescripcion.Location = new System.Drawing.Point(6, 159);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(227, 52);
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtReclasificacion
            // 
            this.txtReclasificacion.Enabled = false;
            this.txtReclasificacion.Location = new System.Drawing.Point(185, 225);
            this.txtReclasificacion.Name = "txtReclasificacion";
            this.txtReclasificacion.Size = new System.Drawing.Size(48, 21);
            this.txtReclasificacion.TabIndex = 12;
            this.txtReclasificacion.Visible = false;
            this.txtReclasificacion.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(5, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 30);
            this.label3.Text = "CANTIDAD A RECLASIFICAR:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label3.Visible = false;
            // 
            // txtPedido
            // 
            this.txtPedido.Enabled = false;
            this.txtPedido.Location = new System.Drawing.Point(146, 258);
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.Size = new System.Drawing.Size(89, 21);
            this.txtPedido.TabIndex = 19;
            this.txtPedido.Visible = false;
            // 
            // lbPedido
            // 
            this.lbPedido.Enabled = false;
            this.lbPedido.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbPedido.Location = new System.Drawing.Point(11, 258);
            this.lbPedido.Name = "lbPedido";
            this.lbPedido.Size = new System.Drawing.Size(129, 20);
            this.lbPedido.Text = "INGRESA EL PEDIDO";
            this.lbPedido.Visible = false;
            // 
            // Reclasificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lbPedido);
            this.Controls.Add(this.txtPedido);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtReclasificacion);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lbDescripcion);
            this.Controls.Add(this.lbCodigo);
            this.Controls.Add(this.txtIngreso);
            this.Controls.Add(this.txtSalida);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Reclasificacion";
            this.Text = "Reclasificacion";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSalida;
        private System.Windows.Forms.TextBox txtIngreso;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Label lbCodigo;
        private System.Windows.Forms.Label lbDescripcion;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtReclasificacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPedido;
        private System.Windows.Forms.Label lbPedido;
    }
}