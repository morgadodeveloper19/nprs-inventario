using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreacionAlamacen;

namespace JQuery
{
    public partial class Zona : System.Web.UI.Page
    {
        Conex con = new Conex();
        string[] ABC = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["FirstName"] == null || Session["LastName"] == null || Session["Name"] == null)
            //{
            //    Response.Redirect("login.aspx");
            //}
        }

        protected void btnTablas_Click(object sender, EventArgs e)
        {
            try
            {
                if (generar())
                {
                    if (valoresDafault() == true)
                    {
                        System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                        l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                            "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                            "<strong>Correcto!! </strong> Se han creado exitosamente las tablas.</div>";
                        logsuccess.Controls.Add(l);
                        btnTablas.Enabled = false;
                        //btnZonas.Enabled = true;
                    }
                    else 
                    {
                        System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                        l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                            "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                            "<strong>Advertencia!!</strong>" + " Error al insertar valores, favor de probar mas tarde" + ".</div>";
                        logerror.Controls.Add(l);
                    }
                }
                else 
                {
                    System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                    l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                        "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                        "<strong>Advertencia!!</strong>" + " Ha ocurrido un error al generar, favor de probar mas tarde"+".</div>";
                    logerror.Controls.Add(l);
                }
            }
            catch (Exception ex)
            {
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Advertencia!!</strong>" + " Ha ocurrido un error al generar, favor de probar mas tarde" + ".</div>";
                logerror.Controls.Add(l);
            };
        }

        protected void btnZonas_Click(object sender, EventArgs e)
        {
            string repetidos = "";
            int cantZonas = 0;
            int conteo = 0;         
            try
            {
                if (txtZonas.Text.Length <= 0)
                {
                    System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                    l.Text = "<div class='alert' style='margin-bottom: 5px;'>" +
                        "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                        "<strong>Advertencia!!</strong>" + " Favor de colocar el número de zonas que desea crear" + ".</div>";
                    logerror.Controls.Add(l);
                }
                else
                {
                    cantZonas = Convert.ToInt32(txtZonas.Text.ToString());
                    for (int x = 0; x < cantZonas; x++)
                    {
                        if (con.getInt("select count(*) from Zonas where ClaveZona ='" + ABC[x] + "'") == 1) 
                            repetidos += " " + ABC[x].ToString();
                        else
                            conteo += con.insert("INSERT INTO Zonas VALUES('" + ABC[x] + "','')");
                    }
                    if (conteo == 0)
                    {
                        System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                        l.Text = "<div class='alert alert-info' style='margin-bottom: 5px;'>" +
                            "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                            "<strong>Informacion!!</strong>" + " No se creó ninguna zona, dado a que existian previamente. \nLas zonas " + repetidos + " ya existian en el almacén.</div>";
                        logerror.Controls.Add(l);
                        btnSiguiente.Visible = true;
                    }
                    else
                    {
                        if (conteo == cantZonas)
                        {
                            System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                            l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                                "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                                "<strong>Correcto!! </strong>Se han creado las " + cantZonas + " zonas correctamente.</div>";
                            logsuccess.Controls.Add(l);
                            btnZonas.Enabled = false;
                            btnSiguiente.Visible = true;
                        }
                        else
                        {
                            System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                            l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                                "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                                "<strong>Correcto!! </strong> Se han creado " + conteo + " de las " + cantZonas + " zonas correctamente.</div>";
                            logsuccess.Controls.Add(l);
                            btnZonas.Enabled = false;
                            btnSiguiente.Visible = true;
                            if (repetidos.Length > 0)
                            {
                                System.Web.UI.WebControls.Label m = new System.Web.UI.WebControls.Label();
                                m.Text = "<div class='alert alert-info' style='margin-bottom: 5px;'>" +
                                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                                    "<strong>Información!! </strong> Las zonas " + repetidos + " ya existian en el almacén.</div>";
                                logerror.Controls.Add(m);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            };
           
        }

        public bool limpiar()
        {
            bool res = false;
            try
            {
                int cuenta = 0;
                cuenta += con.Update_Int("if object_id('dbo.posiciones') is not null begin drop table dbo.posiciones end");
                cuenta += con.Update_Int("if object_id('dbo.ventanas') is not null begin drop table dbo.ventanas end");
                cuenta += con.Update_Int("if object_id('dbo.niveles') is not null begin drop table dbo.niveles end");
                cuenta += con.Update_Int("if object_id('dbo.racks') is not null begin drop table dbo.racks end");
                cuenta += con.Update_Int("if object_id('dbo.Alto') is not null begin drop table dbo.Alto end");
                cuenta += con.Update_Int("if object_id('dbo.Ancho') is not null begin drop table dbo.Ancho end");
                cuenta += con.Update_Int("if object_id('dbo.TipoPosiciones') is not null begin drop table dbo.TipoPosiciones end");
                cuenta += con.Update_Int("if object_id('dbo.Imo') is not null begin drop table dbo.Imo end");
                cuenta += con.Update_Int("if object_id('dbo.Zonas') is not null begin drop table dbo.Zonas end");
                cuenta += con.Update_Int("if object_id('dbo.Maquinarias') is not null begin drop table dbo.Maquinarias end");
                if (cuenta > 0)
                {
                    System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                    l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                        "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                        "<strong>Correcto!! </strong> Se ha limpiado correctamente la base de datos.</div>";
                    logsuccess.Controls.Add(l);
                    res = true;
                }
                else
                {
                    System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                    l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                        "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                        "<strong>Correcto!! </strong> Listo para generacion de tablas.</div>";
                    logsuccess.Controls.Add(l);
                    res = true;
                }

            }
            catch (Exception ex)
            {
                return false;
            };
            return res;
        }

        public bool generar()
        {
            int cuenta = 0;
            try
            {
                if (limpiar())
                {
                    cuenta += con.Update_Int("CREATE TABLE Imo(Claveimo varchar(5) primary key,Descrip varchar(254)not null)");                    
                    cuenta += con.Update_Int("CREATE TABLE Maquinarias(Cvemaq int  primary key,Nommaq varchar(40) not null,Desmaq varchar(50) not null)");                    
                    cuenta += con.Update_Int("CREATE TABLE Alto(IdAlto int identity(1,1) primary key, ClaveAlto	char not null, Descripcion varchar(30)not null)");                    
                    cuenta += con.Update_Int("CREATE TABLE Ancho(IdAncho int identity(1,1) primary key, ClaveAncho	char not null, Descripcion varchar(60)not null)");                    
                    cuenta += con.Update_Int("CREATE TABLE Zonas( IdZona int identity(1,1) primary key, ClaveZona char not null, Descripcion varchar(30)not null)");
                    cuenta += con.Update_Int("CREATE TABLE racks(IDRack int identity(1,1) primary key, Clave varchar(2) not null, IDZona int references Zonas(IDZona))");                    
                    cuenta += con.Update_Int("CREATE TABLE niveles( IDNivel	int identity(1,1) primary key, Clave char not null, IDRack int references racks(IDRack) )");                    
                    cuenta += con.Update_Int("CREATE TABLE ventanas( IDVentana	int identity(1,1) primary key, Clave varchar(2) not null, IDNivel int references niveles(IDNivel), Tipo int not null )");
                    cuenta += con.Update_Int("CREATE TABLE TipoPosiciones( IdTipoPos int identity(1,1) primary key, ClaveTP char not null, Descripcion varchar(30)not null)");
                    cuenta += con.Update_Int("CREATE TABLE posiciones( IDPosicion int identity(1,1) primary key, Clave char not null, IDVentana int references ventanas(IDVentana), Estatus int not null, Imo int not null, Peso decimal(5,3) not null, Altura decimal(5,3) not null, IdTipoPos int references TipoPosiciones(IdTipoPos))");
                }
            }
            catch (Exception ex)
            {
                System.Web.UI.WebControls.Label l = new System.Web.UI.WebControls.Label();
                l.Text = "<div class='alert alert-success' style='margin-bottom: 5px;'>" +
                    "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<strong>Advertencia!!</strong>" + " Ha ocurrido un error al generar, favor de probar mas tarde" + ".</div>";
                logerror.Controls.Add(l);
                return false;
            };
            if (cuenta == 10)
                return true;
            else
                return false;
        }

        public bool valoresDafault()
        {
            bool res = false;
            int conta = 0;
            try
            {
                conta += con.insert("INSERT INTO Alto VALUES('E', 'Estándar')");
                conta += con.insert("INSERT INTO Alto VALUES('D', 'Doble Altura')");
                conta += con.insert("INSERT INTO Alto VALUES('M', 'Media Altura')");
                conta += con.insert("INSERT INTO Alto VALUES('T', 'Tiny')");
                conta += con.insert("INSERT INTO Ancho VALUES('A', '1/2 de ventana (tarima estándar, 2 x ventana)')");
                conta += con.insert("INSERT INTO Ancho VALUES('B', '1/3 de ventana (3 tarimas x ventana)')");
                conta += con.insert("INSERT INTO Ancho VALUES('C', '1/4 de ventana (4 tarimas x ventana)')");                
                conta += con.insert("INSERT INTO Ancho VALUES('D', 'Especial, mercancía que no cabe en rack')");
                conta += con.insert("INSERT INTO Ancho VALUES('E', 'Mercancia de tamaño minimo, caben varios')");
                conta += con.insert("INSERT INTO TipoPosiciones VALUES('E', 'Estandar')");
                conta += con.insert("INSERT INTO TipoPosiciones VALUES('A', 'Arreglo')");
                conta += con.insert("INSERT INTO TipoPosiciones VALUES('P', 'Puente')");
                if(conta == 12)
                    res = true;
            }
            catch (Exception ex)
            {
                return false;
            };
            return res;
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}