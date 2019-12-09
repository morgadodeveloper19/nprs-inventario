<%@ Page Language="C#" Debug="true" AutoEventWireup="true" CodeBehind="loadGrid.aspx.cs" Inherits="JQuery.loadGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editar Grids</title>
    <script src="bootstrap/js/jquery-1.9.1.min.js" type="text/javascript"></script>    
    <script src="bootstrap/jsFunctions/createGrid.js" type="text/javascript"></script>
    <link href="bootstrap/css/bootstrap.css" rel="Stylesheet" media="screen" />
    <link href="bootstrap/css/bootstrap-responsive.css" rel="Stylesheet" media="screen" />

    <script type="text/javascript">
        $(document).ready(function () {
            var btn = $("#t1 input");
            btn.tooltip();
            $("#toolthis").tooltip();

            $("#changeval").click(function () {
                $("#posiciones").attr("value", 500);
            });           
        });
        
            function checarCampos() {
                var pos = $("#posiciones").val();
                var imo = $("#imo").val();
                var peso = $("#peso").val();
                var altura = $("#altura").val();
                if (peso == "" || altura == "" || pos == "") 
                {
                    alert("Asegurese de no dejar los campos vacios e intente de nuevo.");
                    return false;
                } 
                else 
                {
                    if (isNaN(peso) || isNaN(altura) || isNaN(pos)) 
                    {
                        alert("Se han ingresado datos incorrectos en los campos, verifique.");
                        return false;
                    }
                    else 
                    {
                        if (pos > 4 && pos > 0) {
                            alert("Las pociciones no pueden ser mas de 4 ni menores a 0.");
                            return false;
                        }
                        else
                            return true;
                    }                
                }
        }

        function netbtn() {
            var btn = document.getElementById("<%= Button3.ClientID %>");
            //alert("btn->" + btn.name);
            btn.click();
        }
       
        function JSFunction(e) {
            var str = e.id.split(",");            
            var text = $("h3").text();
            var ventana = str[0].substring(4, 6);            
            var pos = str[0].substring(6, 7);
            //alert(ventana);
            var imo = str[1];
            var peso = str[2];
            var altura = str[3];
            var tipoPos = str[4];
            // method 2
            $("#clave").val(e.id);            
            $("#posiciones").val(pos.toString());
            $("#imo").val(imo);
            $("#peso").val(peso);
            $("#altura").val(altura);
            // ----
            $("h3").text("Ventana " + str[0].substring(0, 6));

            if (tipoPos == "1") {
                $("#selectTipo").val('E');
            } 
            if (tipoPos == "2") {
                $("#selectTipo").val('A');
            } 
            if (tipoPos == "3") {
                $("#selectTipo").val('P');
            }
            if (text == str[0]) {
                $(".mini-layout").toggle('slow');

                //$(".span8").toggle('slow');
            } else {
                $(".mini-layout").show('slow');  // ".mini-layout"
                //$(".span8").toggle('slow');
            }
            //e.preventDefault(); // no me funciono :(
            return false;
        }

        function getMessage() {
            alert("message :D");
        }

        function config_click() {
            //$("#t1 input").tooltip();
            //$("#toolthis").tooltip();
            //alert("config ok");
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
        .mini-layout {
            border: 1px solid #ddd;
            -webkit-border-radius: 6px;
            box-shadow: 0 1px 2px rgba(0,0,0,.075);
            margin-bottom: 20px;
            padding: 9px;
            float: left;
        }
 
    </style>
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container-fluid">
                <button type="button" class="btn btn-navbar collapse" data-toggle="collapse" data-target=".nav-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="brand" href="zona.aspx">Almacen NAPRESA</a>
                <div class="nav-collapse in collapse">
                <p class="navbar-text pull-right">
                    Conectado como <a href="#" class="navbar-link">
                    <%--<%= go() %>--%>
                    </a>
                </p>
                    <ul class="nav">
                        <li><a href="zona.aspx">Zonas</a></li>
                        <li><a href="Default.aspx">Creacion Almacen</a></li>                        
                        <li class="active"><a href="loadGrid.aspx">Posiciones</a></li>
                        <li><a href="Arreglos.aspx">Arreglos</a></li>
                    </ul>
                </div> <!--/ .nav collapse -->
            </div>
        </div>
    </div>

    <div class="container">
    
        <form id="form1" runat="server" method="get" class="form-horizontal">
            <asp:Button ID="Button1" runat="server" Text="create GRID" 
                CssClass="btn btn-primary" onclick="Button1_Click" Enabled="false" 
                Visible="False" />
            <asp:Label ID="Label1" runat="server" Font-Bold="False" 
                Text="Seleccione una ZONA: "></asp:Label>
            &nbsp;<asp:DropDownList ID="ddlZona" runat="server" DataSourceID="SqlDSZonas" 
                DataTextField="ClaveZona" DataValueField="IdZona" 
                onselectedindexchanged="ddlZona_SelectedIndexChanged" AutoPostBack="True" >
            </asp:DropDownList>
&nbsp;RACKS: 
            <asp:DropDownList ID="ddlRack" runat="server" 
                DataSourceID="SqlDSRacks" DataTextField="Clave" DataValueField="IDRack">
            </asp:DropDownList>
            &nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                Text="Cargar Rack" CssClass="btn btn-primary" 
                OnClientClick=" return showlist();" Height="38px" />
                

                <p></p>
                <table id="t1" style="width: 100%;" runat="server" class="table table-condensed">
                </table>

    <!-- mensajes -->
    <div class="container" style="padding-bottom:10px;">    
        <div id="logsuccess" runat="server" style=" margin-bottom:5px;">  
        </div>
        <div id="logerror" runat="server" style=" margin-bottom:5px;">
        </div>
    </div>
        
    
        <!-- labels :D -->
        <div class="lista nodisplay" id="lista" runat="server">
            <div class="span2 ">
                <ul>
                    <li><span class="label">1 Posición</span><br /></li>
                    <li><span class="label label-success">2 Posiciones</span><br /></li>
                    <li><span class="label label-warning">3 Posiciones</span><br /></li>
                    <li><span class="label label-info">4 Posiciones</span><br /></li>
                    <br />
                    <span class="label label-important">Puente</span><br />
                </ul>            
            </div>
        </div>
        <!-- end labels -->
   <!-- LAyout-->
   <div class="mini-layout nodisplay">        
        <div class="span8">
        <h3></h3>
        <input type="hidden" name="clave" id="clave" value="" />
            <div class="form-horizontal" >

                <div class="control-group">
                    <label for="posiciones" class="control-label">posiciones:</label>
                    <div class="controls">
                        <div class="input-append">                            
                            <input type="text" name="posiciones" placeholder="posiciones" id="posiciones" class="input-mini"/>
                            <span class="add-on"><i class="icon-th-large"></i></span>
                        </div>
                    </div>
                </div>

                <div class="control-group">
                    <label for="imo" class="control-label">imo:</label>
                    <div class="controls">
                        <div class="input-prepend">
                            <span class="add-on"><i class="icon-fire"></i></span>
                            <input type="text" name="imo" placeholder="imo" id="imo" class="input-mini" />
                        </div>
                    </div>
                </div>

                <div class="control-group">
                    <label for="peso" class="control-label">peso:</label>
                    <div class="controls">
                        <div class="input-prepend">
                            <span class="add-on"><i class="icon-align-left"></i></span>
                            <input  type="text" name="peso" placeholder="peso" id="peso" class="input-mini" />
                        </div>                        
                    </div>                    
                </div>

                <div class="control-group">
                    <label for="altura" class="control-label">altura:</label>
                    <div class="controls">
                        <div class="input-prepend">
                            <span class="add-on"><i class="icon-resize-full"></i></span>
                            <input type="text" name="altura" placeholder="altura" id="altura" class="input-mini"  />
                        </div>                        
                    </div>
                </div>

            <div class="control-group">
                    <label for="tipoPos" class="control-label">Tipo Ventana:</label>
                    <div class="controls">
                        <div class="input-prepend">
                            <span class="add-on"><i class="icon-hand-right"></i></span>
                            <select name="selectTipo" id="selectTipo">
                                <option value="E">Estandar</option>
                                <option value="A">Arreglo</option>
                                <option value="P">Puente</option>
                             </select>
                        </div>                        
                    </div>
                </div>  
                                   
                <div class="control-group">
                    <label for="altura2" class="control-label">Altura:</label>
                    <div class="controls">
                        <div class="input-prepend">
                            <span class="add-on"><i class="icon-resize-vertical"></i></span>
                            <select name="altura2" id="altura2">
                                <option value="1">Estándar</option>
                                <option value="2">Doble Altura</option>
                                <option value="3">Media Altura</option>
                                <option value="4">Tiny</option>
                             </select>
                        </div>                        
                    </div>
                </div>
                
                    <div class="control-group">
                    <label for="ancho" class="control-label">Ancho:</label>
                    <div class="controls">
                        <div class="input-prepend">
                            <span class="add-on"><i class="icon-resize-horizontal"></i></span>
                            <select name="ancho" id="ancho">
                                <option value="1">A - 1/2 Ven</option>
                                <option value="2">B - 1/3 Ven</option>
                                <option value="3">C - 1/4 Ven</option>
                                <option value="4">D - No Cabe</option>
                                <option value="5">E - Mínimo</option>
                             </select>
                        </div>                        
                    </div>

                </div>
                <asp:Button ID="Button3" runat="server" Text="Guardar cambios" 
                    CssClass="btn btn-primary" onclick="Button3_Click"  OnClientClick="return checarCampos();"/>

            </div>
        </div> <!-- span8 -->
        
    </div>
    <!-- layout end-->

    
        </form>

        
    </div>
    
<script src="bootstrap/js/bootstrap-transition.js"></script>
    <script src="bootstrap/js/bootstrap-alert.js"></script>
    <script src="bootstrap/js/bootstrap-modal.js"></script>
    <script src="bootstrap/js/bootstrap-dropdown.js"></script>
    <script src="bootstrap/js/bootstrap-scrollspy.js"></script>
    <script src="bootstrap/js/bootstrap-tab.js"></script>
    <script src="bootstrap/js/bootstrap-tooltip.js"></script>
    <script src="bootstrap/js/bootstrap-popover.js"></script>
    <script src="bootstrap/js/bootstrap-button.js"></script>
    <script src="bootstrap/js/bootstrap-collapse.js"></script>
    <script src="bootstrap/js/bootstrap-carousel.js"></script>
    <script src="bootstrap/js/bootstrap-typeahead.js"></script>
    <script src="bootstrap/js/bootstrap-affix.js"></script>    
    

    <asp:SqlDataSource ID="SqlDSZonas" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Ricsa %>" 
        SelectCommand="SELECT [IdZona], [ClaveZona] FROM [Zonas]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSRacks" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Ricsa %>" 
        SelectCommand="SELECT [IDRack], [Clave], [IDZona] FROM [racks] WHERE ([IDZona] = @IDZona)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlZona" Name="IDZona" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    

</body>
</html>
