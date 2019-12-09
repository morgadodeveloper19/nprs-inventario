namespace SmartDeviceProject1
{
    partial class frmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu menuLogin;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.menuLogin = new System.Windows.Forms.MainMenu();
            this.mi_Salir = new System.Windows.Forms.MenuItem();
            this.mi_Login = new System.Windows.Forms.MenuItem();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // menuLogin
            // 
            this.menuLogin.MenuItems.Add(this.mi_Salir);
            this.menuLogin.MenuItems.Add(this.mi_Login);
            // 
            // mi_Salir
            // 
            this.mi_Salir.Text = "Salir";
            this.mi_Salir.Click += new System.EventHandler(this.mi_Salir_Click);
            // 
            // mi_Login
            // 
            this.mi_Login.Text = "Iniciar Sesión";
            this.mi_Login.Click += new System.EventHandler(this.mi_Login_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblUsuario.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.Location = new System.Drawing.Point(66, 90);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(100, 20);
            this.lblUsuario.Text = "USUARIO";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(44, 113);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(150, 21);
            this.txtUsuario.TabIndex = 2;
            // 
            // lblContrasena
            // 
            this.lblContrasena.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblContrasena.Location = new System.Drawing.Point(55, 168);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(123, 20);
            this.lblContrasena.Text = "CONTRASEÑA";
            this.lblContrasena.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtContrasena
            // 
            this.txtContrasena.Location = new System.Drawing.Point(45, 191);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.PasswordChar = '*';
            this.txtContrasena.Size = new System.Drawing.Size(150, 21);
            this.txtContrasena.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(137, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtContrasena);
            this.Controls.Add(this.lblContrasena);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblUsuario);
            this.Menu = this.menuLogin;
            this.Name = "frmLogin";
            this.Text = "Inicio de Sesión";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mi_Salir;
        private System.Windows.Forms.MenuItem mi_Login;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}