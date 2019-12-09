<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="JQuery.login"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>    
    <script src="bootstrap/js/bootstrap.js" type="text/javascript"></script>
    <script src="bootstrap/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    
    <script runat="server">
        public void iniciar_sesion(object sender, EventArgs e)
        {
            TextBox t = (TextBox)this.FindControl("Username"); 
            string req = Request.QueryString["hola"];
            
            string name = t.Text;
            t = (TextBox)this.FindControl("Password");
            string password = t.Text;
            Response.Redirect("~/JqueryWebapp.aspx?id=1&Username=" + name + "&Pasword=" + password);
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#test").click(function () {
                var user = document.getElementById("<%= Username.ClientID %>");
                //alert("holaa " + user.value);
                var clickButton = document.getElementById("<%= Button2.ClientID %>");
                clickButton.click();

            });
        });
    </script>
    <link href="bootstrap/css/bootstrap.css" rel="Stylesheet" media="screen" />
    <link href="bootstrap/css/bootstrap-responsive.css" rel="Stylesheet" media="screen" />
    <style type="text/css">
    body {
        padding-top: 40px;
        padding-bottom: 40px;
        background-color: #f5f5f5;
    }
    .form-signin
    {
        max-width: 300px;
        padding: 19px 29px 29px;
        margin: 0 auto 20px;
        background-color: #fff;
        border: 1px solid #e5e5e5;
        -webkit-border-radius: 5px;
        -moz-webkit-radius: 5px;
        border-radius: 5px;
        -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
        -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
        box-shadow: 0 1px 2px rgba(0,0,0,.05);
    }
    </style>
</head>
        <br />
<body>
    <div class="container">
        <form class="form-signin" id="form1" runat="server" method="get">
        <h2 class="form-signin-heading">Inicio de Sesion
        </h2>

        <div class="control-group">
            <div class="controls">
                <label for="username" class="control-label">Usuario:</label>
                <div class="input-prepend">
                    <span class="add-on"><i class="icon-user"></i></span>                    
                    <asp:TextBox ID="Username" runat="server" class="span3" placeholder="usuario" value="" ></asp:TextBox>                 
                </div>
            </div>
        </div>
        
        <div class="control-group">
            <div class="controls">
                <label for="password" class="control-label">Contraseña:</label>
                <div class="input-prepend">
                    <span class="add-on"><i class="icon-asterisk"></i></span>
                    <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="span3" placeholder="contraseña"></asp:TextBox>
                </div>
            </div>
        </div>


        
        
        <asp:Button CssClass="btn btn-large btn-primary" ID="Button2" runat="server" Text="Entrar" OnClick="iniciar_sesion"/>
        
        <!--<input type="button" id="test" value="test" name="mybutton" />-->
        <br />
        <asp:Label ID="lblErrors" runat="server" Font-Bold="True" 
            Font-Underline="False" ForeColor="Red"></asp:Label>
        </form>
    </div>   
    
 
</body>
</html>
