using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Web.Services;
using System.Security.Cryptography;

namespace SmartDeviceProject1
{
    public partial class frmLogin : Form
    {
        public string webSer;
        string[] usuario;   
        //SmartDeviceProject1.NapresaLocal.Service1 ws = new SmartDeviceProject1.NapresaLocal.Service1();
        //SmartDeviceProject1.wsGM.Service1 ws = new SmartDeviceProject1.wsGM.Service1();        
        //SmartDeviceProject1.localhost.Service1 ws = new SmartDeviceProject1.localhost.Service1();
        cMetodos c = new cMetodos();
        ValidateOP vop = new ValidateOP();


        public frmLogin()
        {
            InitializeComponent();
            //ws.Timeout=2000;
            
        }

        private void mi_Login_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string[] res = { "", "", "", "" };
            label4.Text = "Cargando ...";
            string user = txtUsuario.Text;
            //string pass = txtContrasena.Text;
            string pass = getMd5Hash(txtContrasena.Text.Trim());
            if (user.Length <= 0 || pass.Length <= 0)
            {
                label4.Refresh();
                txtUsuario.Text = "";
                txtUsuario.Focus();
                txtContrasena.Text = "";
                MessageBox.Show("Favor introducir el Usuario y Contraseña");
                Cursor.Current = Cursors.Default;
            }
            else
            {
                label4.Refresh();
                try
                {
                    res = c.ingresoUsuario(user, pass);
                    usuario = res;
                    if (res[0].Equals("0") && res[1].Equals("0") && res[2].Equals("0") && res[3].Equals("0"))
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Usuario o contraseña incorrecto", "SESION");
                        label4.Text = "";
                        label4.Refresh();
                    }
                    else
                    {
                        label4.Text = "";
                        label4.Refresh();
                        txtUsuario.Text = "";
                        txtContrasena.Text = "";
                        txtUsuario.Focus();
                        frmMenu_Principal men = new frmMenu_Principal(usuario);
                        men.Visible = true;
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        Console.WriteLine("Status Code : {0}", ((HttpWebResponse)ex.Response).StatusCode);
                        Console.ReadLine();
                        Console.WriteLine("Status Description : {0}", ((HttpWebResponse)ex.Response).StatusDescription);
                        Console.ReadLine();
                    }
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Error de comunicación con el web service");
                    label4.Text = "";
                    label4.Refresh();
                    Cursor.Current = Cursors.Default;
                    return;                    
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("Servidor no visible, intente de nuevo", "Informacion");
                    label4.Text = "";
                    label4.Refresh();
                }
            }
        }

        private void mi_Salir_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void frmLogin_Closing(object sender, CancelEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

    }
}