namespace SmartDeviceProject1.Produccion
{
    partial class Revisar_Avance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Revisar_Avance));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.mi_NoFiltro = new System.Windows.Forms.MenuItem();
            this.mi_FiltroPendiente = new System.Windows.Forms.MenuItem();
            this.mi_FiltroCurado = new System.Windows.Forms.MenuItem();
            this.mi_FiltroLiberado = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgOrdenesProd = new System.Windows.Forms.DataGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.rbLiberar = new System.Windows.Forms.RadioButton();
            this.rbHuecos = new System.Windows.Forms.RadioButton();
            this.rbCurado = new System.Windows.Forms.RadioButton();
            this.dgInfoProd = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem3);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "REGRESAR";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.MenuItems.Add(this.mi_NoFiltro);
            this.menuItem3.MenuItems.Add(this.mi_FiltroPendiente);
            this.menuItem3.MenuItems.Add(this.mi_FiltroCurado);
            this.menuItem3.MenuItems.Add(this.mi_FiltroLiberado);
            this.menuItem3.Text = "FILTROS";
            // 
            // mi_NoFiltro
            // 
            this.mi_NoFiltro.Checked = true;
            this.mi_NoFiltro.Text = "Todos";
            this.mi_NoFiltro.Click += new System.EventHandler(this.mi_NoFiltro_Click);
            // 
            // mi_FiltroPendiente
            // 
            this.mi_FiltroPendiente.Text = "Pendiente";
            this.mi_FiltroPendiente.Click += new System.EventHandler(this.mi_FiltroPendiente_Click);
            // 
            // mi_FiltroCurado
            // 
            this.mi_FiltroCurado.Text = "Curado";
            this.mi_FiltroCurado.Click += new System.EventHandler(this.mi_FiltroCurado_Click);
            // 
            // mi_FiltroLiberado
            // 
            this.mi_FiltroLiberado.Text = "Liberado";
            this.mi_FiltroLiberado.Click += new System.EventHandler(this.mi_FiltroLiberado_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // dgOrdenesProd
            // 
            this.dgOrdenesProd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgOrdenesProd.Location = new System.Drawing.Point(0, 91);
            this.dgOrdenesProd.Name = "dgOrdenesProd";
            this.dgOrdenesProd.Size = new System.Drawing.Size(240, 174);
            this.dgOrdenesProd.TabIndex = 2;
            this.dgOrdenesProd.CurrentCellChanged += new System.EventHandler(this.dgOrdenesProd_CurrentCellChanged);
            this.dgOrdenesProd.Click += new System.EventHandler(this.dgOrdenesProd_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(22, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(196, 156);
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.rbLiberar);
            this.panel2.Controls.Add(this.rbHuecos);
            this.panel2.Controls.Add(this.rbCurado);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(190, 150);
            this.panel2.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(113, 125);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 20);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 47);
            this.label2.Text = "¿Que operación desea realizar??";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.ParentChanged += new System.EventHandler(this.label2_ParentChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(7, 125);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 20);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Aceptar";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rbLiberar
            // 
            this.rbLiberar.Location = new System.Drawing.Point(36, 99);
            this.rbLiberar.Name = "rbLiberar";
            this.rbLiberar.Size = new System.Drawing.Size(120, 20);
            this.rbLiberar.TabIndex = 2;
            this.rbLiberar.Text = "Liberar Producto";
            this.rbLiberar.CheckedChanged += new System.EventHandler(this.rbLiberar_CheckedChanged);
            // 
            // rbHuecos
            // 
            this.rbHuecos.Location = new System.Drawing.Point(36, 73);
            this.rbHuecos.Name = "rbHuecos";
            this.rbHuecos.Size = new System.Drawing.Size(120, 20);
            this.rbHuecos.TabIndex = 1;
            this.rbHuecos.Text = "Contar Mermas";
            this.rbHuecos.CheckedChanged += new System.EventHandler(this.rbHuecos_CheckedChanged);
            // 
            // rbCurado
            // 
            this.rbCurado.Location = new System.Drawing.Point(36, 47);
            this.rbCurado.Name = "rbCurado";
            this.rbCurado.Size = new System.Drawing.Size(120, 20);
            this.rbCurado.TabIndex = 0;
            this.rbCurado.Text = "Contar Huecos";
            this.rbCurado.CheckedChanged += new System.EventHandler(this.rbCurado_CheckedChanged);
            // 
            // dgInfoProd
            // 
            this.dgInfoProd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgInfoProd.Location = new System.Drawing.Point(19, 242);
            this.dgInfoProd.Name = "dgInfoProd";
            this.dgInfoProd.Size = new System.Drawing.Size(193, 23);
            this.dgInfoProd.TabIndex = 4;
            this.dgInfoProd.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 29);
            this.label1.Text = "Seleccione la Orden de Produccion a trabajar: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Revisar_Avance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgInfoProd);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgOrdenesProd);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Revisar_Avance";
            this.Text = "Revisar Avance";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGrid dgOrdenesProd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rbLiberar;
        private System.Windows.Forms.RadioButton rbHuecos;
        private System.Windows.Forms.RadioButton rbCurado;
        private System.Windows.Forms.DataGrid dgInfoProd;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem mi_NoFiltro;
        private System.Windows.Forms.MenuItem mi_FiltroPendiente;
        private System.Windows.Forms.MenuItem mi_FiltroCurado;
        private System.Windows.Forms.MenuItem mi_FiltroLiberado;
    }
}