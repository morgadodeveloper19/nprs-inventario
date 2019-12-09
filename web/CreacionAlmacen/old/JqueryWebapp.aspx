<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JqueryWebapp.aspx.cs" Inherits="JQuery.JqueryWebapp" Debug="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Almacenes Raks</title>
    <script src="bootstrap/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap-tooltip.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap-popover.js" type="text/javascript"></script>
    <script src="bootstrap/jsFunctions/createGrid.js" type="text/javascript"></script>

    <link href="bootstrap/css/bootstrap.css" rel="Stylesheet" media="screen" />
    <link href="bootstrap/css/bootstrap-responsive.css" rel="Stylesheet" media="screen" />
    <%--<link href="bootstrap/css/docs.css" rel="Stylesheet" media="screen" />--%>
    
    <script type="text/javascript" language="javascript">
        function on_client() {
            javascript:alert('antes de enviar');
        }

        $(document).ready(function () {
            $(".nodisplay").fadeIn('slow');
            var $log = $("#log");
            var $log2 = $("#log2");


            $("#aceptar").click(function () {
                var niveles = $("#niveles").val();
                var ventanas = $("#ventanas").val();
                var my_grid;
                if (niveles.replace(/^\s+/, "") == "" || ventanas.replace(/^\s+/, "") == "")
                    alert("campos vacios");
                else {
                    if (isNaN(niveles) || isNaN(ventanas))
                        alert("Nan ok");
                    else {
                        if (niveles > 8 || ventanas > 20)
                            alert("No es posible de establecer mas de 8 NIVELES u/o mas de 20 ventanas");
                        else {
                            //alert("niv->" + niveles + "  vent->" + ventanas);
                            my_grid = createGridWhitParams(niveles, ventanas);
                            $log.text("");
                            $log.append(my_grid);
                            $("div#toolhere").tooltip();
                            // -- add
                            var $magic = $("div#toolhere");
                            $magic.click(function () {
                                var details = $magic.attr("data-toggle");
                                $log2.append(details + ". ");
                            });



                        }
                    }
                }
                //$log.append(my_grid);
            });
            //------ log 2
            $("#setlog2").click(function () {
                my_grid = createGrid();
                $log2.append(my_grid);
            });

            //------- tool tip
            $("a.mypopup").tooltip();


            var $added = $("#addElement");
            $added.click(function () {
                var name = $added.attr("name");
                $log2.append("<input type='text' id='num' class='input-mini'  value='" + name + "' />");
            });    


        });             // end doc.ready


        
        

    </script>
    <script runat="server">
        public string go()
        {
            return (Request.QueryString["Username"] != null ? Request.QueryString["Username"].ToString() : "");
        }
    </script>
    
    <style type="text/css">
        body
        {
            padding-top: 60px;
            padding-bottom: 40px;
        }
        .sidebar-nav 
        {
            padding: 9px 0;
        }
        @media (max-width: 980px) {
            /* Enable use of floated navbar text :D cool*/
            .navbar-text.pull-right 
            {
                float: none;
                padding-left : 5px;
                padding-right: 5px;
            }
        }        
        .nodisplay
        {
            display: none;
        }
        .show-grid [class*="span"] {
          background-color: #eee;
          text-align: center;
          -webkit-border-radius: 3px;
             -moz-border-radius: 3px;
                  border-radius: 3px;
          min-height: 40px;
          line-height: 40px;
        }
        .show-grid {
        margin-top: 10px;
        margin-bottom: 20px;
        }
        .show-grid [class*="span"]:hover {
          background-color: #ddd;
        }
        .bs-docs-example {
position: relative;
margin: 15px 0;
padding: 39px 19px 14px;
background-color: #fff;
border: 1px solid #ddd;
-webkit-border-radius: 4px;
-moz-border-radius: 4px;
border-radius: 4px;
}
        .bs-docs-example:after {
content: "Mensajes";
position: absolute;
top: -1px;
left: -1px;
padding: 3px 7px;
font-size: 12px;
font-weight: bold;
background-color: #f5f5f5;
border: 1px solid #ddd;
color: #9da0a4;
-webkit-border-radius: 4px 0 4px 0;
-moz-border-radius: 4px 0 4px 0;
border-radius: 4px 0 4px 0;
}
    </style>
</head>
<body>
    <form id="form2" runat="server"  method="get" >
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container-fluid">
                <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="brand" href="#">Rack y Almacenes</a>
                <div class="nav-collapse collapse">
                <p class="navbar-text pull-right">
                    Conectado como <a href="#" class="navbar-link">
                    <%--<%= go() %>--%>
                    </a>
                </p>
                    <ul class="nav">
        <%--                <li class="active">Home</li>--%>
                        <li><a href="loadGrid.aspx">About</a></li>
                        <li><a href="Login.aspx">Login</a></li>
                    </ul>
                </div> <!--/ .nav collapse -->
            </div>
        </div>
    </div>

    <div class="container">
        <h1 class="nodisplay">Administracion de Racks</h1>

    
        <table class="tabs-below">
            <thead></thead>
            <tbody>
                <tr> 
                    <td valign="top"> Cantidad de Niveles:</td>
                    <td><input type="text" placeholder="niveles" id="niveles" name="niveles" class="input-small" /></td>
                </tr>
                <tr> 
                    <td valign="top"> Cantidad de Ventanas:</td>
                    <td><input type="text" placeholder="ventanas" id="ventanas" name="ventanas" class="input-small"/></td>
                </tr>
                <tr>
                    <td class="left">
                        <input type="button" value="Aceptar" id="aceptar" class="btn-mini btn-primary" />
                    </td>                
                </tr>
            </tbody>
        </table>
        <input type="button" value="log2" id="setlog2" class="btn-primary btn-small" />
        <input type="button" id="addElement" value="addElement" class="btn-primary btn-small" name="israel" /><asp:CheckBox 
            ID="CheckBox1" runat="server" Text="checkbox :D" />
&nbsp;<div id="log">
        </div>
        
        

        <div class="bs-docs-example">
            <div id="log2" style="border-top: 1px solid #cccccc;"></div>
        </div>

        <%--<asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />--%>

    </div>

    <input type="submit" value="submit" />
    <asp:Button ID="Button1" runat="server" Text="Buttoncito" 
        onclick="Button1_Click1" />
    <asp:Button ID="Button3" runat="server" Text="Client" OnClientClick="on_client()" />
    <br />
    <asp:TextBox ID="txt1" runat="server" Width="29px">2</asp:TextBox>
    <asp:TextBox ID="txt2" runat="server" Width="33px">2</asp:TextBox>
    <p>
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="create table" />
    </p>
    <p>
        <table id="t1" runat="server" border="1" visible="false">
        </table>
    </p>
    </form>


    
</body>
</html>
