<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cierre.aspx.cs" Inherits="JQuery.Cierre" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="bootstrap/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap-tooltip.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap-popover.js" type="text/javascript"></script>
    <script src="bootstrap/jsFunctions/createGrid.js" type="text/javascript"></script>
    <link href="bootstrap/css/bootstrap.css" rel="Stylesheet" media="screen" />
    <link href="bootstrap/css/bootstrap-responsive.css" rel="Stylesheet" media="screen" />
    <script type="text/javascript">
    
    </script>

    <style type="text/css">
     
        .style1
        {
            width: 342px;
        }
     
        .style2
        {
            width: 342px;
            height: 22px;
            margin-left: 40px;
        }
     
        .style3
        {
            width: 342px;
            height: 22px;
        }
        .style4
        {
            height: 55px;
        }
     
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="body">        
              <h1 align="center">Cierre de Inventarios</h1>              
              <form id="Form1">
                <table width ="90%" align="center">
                <tr>
                     <td class="style3"><b>1. Seleccione una Sucursal y Almacén:</b></td>
                     <td class="style2">&nbsp;</td>
                </tr>

                <tr>
                    <td class="style1" align="center">
                        <asp:Label ID="Label1" runat="server" Text="SUCURSAL"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlSucursal" runat="server" Width="192px" AppendDataBoundItems="True" 
                            onselectedindexchanged="ddlSucursal_SelectedIndexChanged" 
                            AutoPostBack="True" >
                        </asp:DropDownList>
                        <br />
                        <asp:Label ID="lblddl1" runat="server"></asp:Label>
                    </td> <td class="style2" align="center">
                        <asp:Label ID="Label2" runat="server" Text="ALMACEN"></asp:Label>                      
                        <asp:DropDownList ID="ddlAlmacen" runat="server" Width="402px" 
                            onselectedindexchanged="ddlAlmacen_SelectedIndexChanged" 
                            AppendDataBoundItems="True" AutoPostBack="True"> </asp:DropDownList>
                        <br />
                        <asp:Label ID="lblddl2" runat="server"></asp:Label>
                    </td>                                       
                </tr>                              
                 <trñ>
                    <td align="center" colspan ="3" class="style4">
                     
                        <asp:Label ID="lblinvPorCer" runat="server" Text="Inventarios por Cerrar:" 
                            Visible="False"></asp:Label>
                        <br />
&nbsp;
                        <asp:DropDownList ID="ddlPorCerrar" runat="server" AutoPostBack="True" 
                            Width="190px" Visible="False" AppendDataBoundItems="True">
                        </asp:DropDownList>
                     </td>                
                </tr>
                <tr>
                     <td class="style1">                      
                     <!--<asp:Button CssClass="btn btn-large btn-primary" ID="btnTablas" runat="server" Text="Generar Tablas" Visible="False"/>-->                      
                         <asp:Label ID="lblPrueba2" runat="server"></asp:Label>
                     </td>
                     <td class="style2">
                         <asp:Label ID="lblPrueba" runat="server"></asp:Label>
                     </td>
                </tr>
                
                </table>
                <br />

         </form>
         

     <table width ="85%" align="center">
        <tr>                   
            <td >                        
                <div class="container" style="padding-bottom:10pxg;">    
                    <div id="logsuccess" runat="server" style=" margin-bottom:5px;">  
                    </div>
                    <div id="logerror" runat="server" style=" margin-bottom:5px;">
                    </div>                    
                </div>
            </td>
        </tr>                 
    </table> 
   <br>
    <asp:SqlDataSource ID="dsDllAlmacen" runat="server" SelectCommand="SELECT [Almacen], [Nombre] FROM [Alm]" >
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsDllSucursales" runat="server" SelectCommand="SELECT [Sucursal], [Nombre] FROM [Sucursal] WHERE [Nombre] LIKE '%fab%'">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsDllInventarios" runat="server" ></asp:SqlDataSource>
   
   </div>
</asp:Content>
