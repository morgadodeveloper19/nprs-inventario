namespace SmartDeviceProject1
{
    partial class frmLectura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLectura));
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstNum = new System.Windows.Forms.ListBox();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTagsNo = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.lstCaf = new System.Windows.Forms.ListBox();
            this.lstDescripcion = new System.Windows.Forms.ListBox();
            this.lstSerie = new System.Windows.Forms.ListBox();
            this.pnlDetalles = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnPiezas = new System.Windows.Forms.Button();
            this.txtCantCont = new System.Windows.Forms.TextBox();
            this.lblCantCong = new System.Windows.Forms.Label();
            this.lblUnTar = new System.Windows.Forms.Label();
            this.lblCantTar = new System.Windows.Forms.Label();
            this.lblLinea = new System.Windows.Forms.Label();
            this.lblFamilia = new System.Windows.Forms.Label();
            this.lblCat = new System.Windows.Forms.Label();
            this.lblDescrip = new System.Windows.Forms.Label();
            this.lblArticulo = new System.Windows.Forms.Label();
            this.btnUnidad = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlDetalles.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(10, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 22);
            this.button2.TabIndex = 64;
            this.button2.Text = "Conectar";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 17);
            this.label1.Text = "  ID              EPC              Descripción";
            // 
            // lstNum
            // 
            this.lstNum.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lstNum.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lstNum.Location = new System.Drawing.Point(4, 111);
            this.lstNum.Name = "lstNum";
            this.lstNum.Size = new System.Drawing.Size(21, 178);
            this.lstNum.TabIndex = 63;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.BackColor = System.Drawing.Color.White;
            this.btnFinalizar.Enabled = false;
            this.btnFinalizar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnFinalizar.Location = new System.Drawing.Point(160, 7);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(73, 22);
            this.btnFinalizar.TabIndex = 62;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 288);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTagsNo);
            this.panel1.Controls.Add(this.dataGrid1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lstCaf);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.btnFinalizar);
            this.panel1.Controls.Add(this.lstDescripcion);
            this.panel1.Controls.Add(this.lstSerie);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 239);
            // 
            // btnTagsNo
            // 
            this.btnTagsNo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnTagsNo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnTagsNo.Location = new System.Drawing.Point(0, 215);
            this.btnTagsNo.Name = "btnTagsNo";
            this.btnTagsNo.Size = new System.Drawing.Size(240, 24);
            this.btnTagsNo.TabIndex = 78;
            this.btnTagsNo.Text = "Tags No Procedentes";
            this.btnTagsNo.Click += new System.EventHandler(this.btnTagsNo_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(2, 35);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(237, 179);
            this.dataGrid1.TabIndex = 76;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(85, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 22);
            this.button1.TabIndex = 74;
            this.button1.Text = "Leer";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lstCaf
            // 
            this.lstCaf.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lstCaf.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lstCaf.Location = new System.Drawing.Point(189, 56);
            this.lstCaf.Name = "lstCaf";
            this.lstCaf.Size = new System.Drawing.Size(48, 178);
            this.lstCaf.TabIndex = 72;
            this.lstCaf.Visible = false;
            // 
            // lstDescripcion
            // 
            this.lstDescripcion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lstDescripcion.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lstDescripcion.Location = new System.Drawing.Point(160, 56);
            this.lstDescripcion.Name = "lstDescripcion";
            this.lstDescripcion.Size = new System.Drawing.Size(77, 178);
            this.lstDescripcion.TabIndex = 70;
            // 
            // lstSerie
            // 
            this.lstSerie.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lstSerie.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lstSerie.Location = new System.Drawing.Point(31, 56);
            this.lstSerie.Name = "lstSerie";
            this.lstSerie.Size = new System.Drawing.Size(123, 178);
            this.lstSerie.TabIndex = 69;
            // 
            // pnlDetalles
            // 
            this.pnlDetalles.Controls.Add(this.btnGuardar);
            this.pnlDetalles.Controls.Add(this.btnPiezas);
            this.pnlDetalles.Controls.Add(this.txtCantCont);
            this.pnlDetalles.Controls.Add(this.lblCantCong);
            this.pnlDetalles.Controls.Add(this.lblUnTar);
            this.pnlDetalles.Controls.Add(this.lblCantTar);
            this.pnlDetalles.Controls.Add(this.lblLinea);
            this.pnlDetalles.Controls.Add(this.lblFamilia);
            this.pnlDetalles.Controls.Add(this.lblCat);
            this.pnlDetalles.Controls.Add(this.lblDescrip);
            this.pnlDetalles.Controls.Add(this.lblArticulo);
            this.pnlDetalles.Controls.Add(this.btnUnidad);
            this.pnlDetalles.Controls.Add(this.label15);
            this.pnlDetalles.Controls.Add(this.label32);
            this.pnlDetalles.Controls.Add(this.label17);
            this.pnlDetalles.Controls.Add(this.label22);
            this.pnlDetalles.Controls.Add(this.label21);
            this.pnlDetalles.Controls.Add(this.label20);
            this.pnlDetalles.Controls.Add(this.label14);
            this.pnlDetalles.Controls.Add(this.label18);
            this.pnlDetalles.Controls.Add(this.label19);
            this.pnlDetalles.Controls.Add(this.btnAgregar);
            this.pnlDetalles.Location = new System.Drawing.Point(0, 170);
            this.pnlDetalles.Name = "pnlDetalles";
            this.pnlDetalles.Size = new System.Drawing.Size(240, 124);
            this.pnlDetalles.Visible = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(147, 261);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 30);
            this.btnGuardar.TabIndex = 83;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnPiezas
            // 
            this.btnPiezas.Enabled = false;
            this.btnPiezas.Location = new System.Drawing.Point(6, 229);
            this.btnPiezas.Name = "btnPiezas";
            this.btnPiezas.Size = new System.Drawing.Size(75, 26);
            this.btnPiezas.TabIndex = 65;
            this.btnPiezas.Text = "Piezas";
            this.btnPiezas.Click += new System.EventHandler(this.btnPiezas_Click);
            // 
            // txtCantCont
            // 
            this.txtCantCont.Location = new System.Drawing.Point(163, 234);
            this.txtCantCont.Name = "txtCantCont";
            this.txtCantCont.Size = new System.Drawing.Size(70, 21);
            this.txtCantCont.TabIndex = 64;
            // 
            // lblCantCong
            // 
            this.lblCantCong.Location = new System.Drawing.Point(165, 203);
            this.lblCantCong.Name = "lblCantCong";
            this.lblCantCong.Size = new System.Drawing.Size(68, 20);
            // 
            // lblUnTar
            // 
            this.lblUnTar.Location = new System.Drawing.Point(87, 172);
            this.lblUnTar.Name = "lblUnTar";
            this.lblUnTar.Size = new System.Drawing.Size(148, 20);
            // 
            // lblCantTar
            // 
            this.lblCantTar.Location = new System.Drawing.Point(87, 151);
            this.lblCantTar.Name = "lblCantTar";
            this.lblCantTar.Size = new System.Drawing.Size(148, 20);
            // 
            // lblLinea
            // 
            this.lblLinea.Location = new System.Drawing.Point(87, 130);
            this.lblLinea.Name = "lblLinea";
            this.lblLinea.Size = new System.Drawing.Size(148, 20);
            // 
            // lblFamilia
            // 
            this.lblFamilia.Location = new System.Drawing.Point(87, 109);
            this.lblFamilia.Name = "lblFamilia";
            this.lblFamilia.Size = new System.Drawing.Size(148, 20);
            // 
            // lblCat
            // 
            this.lblCat.Location = new System.Drawing.Point(87, 88);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(148, 20);
            // 
            // lblDescrip
            // 
            this.lblDescrip.Location = new System.Drawing.Point(64, 28);
            this.lblDescrip.Name = "lblDescrip";
            this.lblDescrip.Size = new System.Drawing.Size(171, 55);
            // 
            // lblArticulo
            // 
            this.lblArticulo.Location = new System.Drawing.Point(87, 4);
            this.lblArticulo.Name = "lblArticulo";
            this.lblArticulo.Size = new System.Drawing.Size(148, 20);
            // 
            // btnUnidad
            // 
            this.btnUnidad.Location = new System.Drawing.Point(6, 195);
            this.btnUnidad.Name = "btnUnidad";
            this.btnUnidad.Size = new System.Drawing.Size(75, 28);
            this.btnUnidad.TabIndex = 53;
            this.btnUnidad.Text = "Unidad";
            this.btnUnidad.Click += new System.EventHandler(this.btnUnidad_Click);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(9, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 20);
            this.label15.Text = "Articulo:";
            // 
            // label32
            // 
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label32.Location = new System.Drawing.Point(9, 172);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(58, 20);
            this.label32.Text = "Un  /Tar";
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(9, 151);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(72, 20);
            this.label17.Text = "Cant/Tar";
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(9, 130);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(61, 20);
            this.label22.Text = "Linea";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(9, 109);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 20);
            this.label21.Text = "Familia";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(9, 88);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(78, 20);
            this.label20.Text = "Categoria";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(87, 233);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 20);
            this.label14.Text = "Cant Cont:";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(87, 203);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 20);
            this.label18.Text = "Cant Cong:";
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(9, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 20);
            this.label19.Text = "Descrip:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Aquamarine;
            this.btnAgregar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.Location = new System.Drawing.Point(5, 261);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(94, 30);
            this.btnAgregar.TabIndex = 8;
            this.btnAgregar.Text = "CANCELAR";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(240, 55);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(85, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 22);
            this.button3.TabIndex = 65;
            this.button3.Text = "Detener";
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmLectura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.pnlDetalles);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lstNum);
            this.KeyPreview = true;
            this.Name = "frmLectura";
            this.Text = "Lectura";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLectura_KeyDown);
            this.panel1.ResumeLayout(false);
            this.pnlDetalles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstNum;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lstDescripcion;
        private System.Windows.Forms.ListBox lstSerie;
        private System.Windows.Forms.ListBox lstCaf;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnlDetalles;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label lblCantCong;
        private System.Windows.Forms.Label lblUnTar;
        private System.Windows.Forms.Label lblCantTar;
        private System.Windows.Forms.Label lblLinea;
        private System.Windows.Forms.Label lblFamilia;
        private System.Windows.Forms.Label lblCat;
        private System.Windows.Forms.Label lblDescrip;
        private System.Windows.Forms.Label lblArticulo;
        private System.Windows.Forms.Button btnUnidad;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtCantCont;
        private System.Windows.Forms.Button btnPiezas;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnTagsNo;
        private System.Windows.Forms.Button button3;

    }
}