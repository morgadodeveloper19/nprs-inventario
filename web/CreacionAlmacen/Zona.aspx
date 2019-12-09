<%@ Page Title="Zonas" Language="C#" Debug="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Zona.aspx.cs" Inherits="JQuery.Zona" %>
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
     
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="body">        
              <h1 align="center">CREACION DE ZONAS</h1>
              <br />
              <form id="Form1">
                <table width ="90%" align="center">
                <tr>
                     <td class="style1"><b>1. Seleccione una Sucursal y Almacén:</b></td>
                     <td class="style2"></td> <td class="style3">&nbsp;</td>
                     <td rowspan ="6">
                         &nbsp;</td>
                </tr>

                <tr>
                    <td class="style1" align="center">
                        <asp:Label runat="server" Text="SUCURSAL"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlSucursal" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlSucursal_SelectedIndexChanged" Width="205px">
                        </asp:DropDownList>
                        <br />
                        <asp:Label ID="lblddl1" runat="server"></asp:Label>
                    </td> <td class="style2" align="center">
                        <asp:Label ID="lblAlmacenes" runat="server" Text="ALMACEN" Visible="False"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlAlmacen" runat="server" Visible="False" Width="402px" 
                            AutoPostBack="True" onselectedindexchanged="ddlAlmacen_SelectedIndexChanged"> </asp:DropDownList>
                        <br />
                        <asp:Label ID="lblddl2" runat="server"></asp:Label>
                    </td>
                    <td class="style3">  &nbsp;</td>
                    
                </tr>
                   
                <tr>
                    <td class="style1"></td> <td class="style2">
                    <asp:Button CssClass="btn btn-large btn-primary" ID="btnTablas" runat="server" Text="Generar Tablas" 
                        onclick="btnTablas_Click" Visible="true" />
                    </td>
                    <td class="style3">  &nbsp;</td>
                    
                </tr>
                <tr>
                     <td class="style1"><b>2. Cuantas zonas deseas crear?</b></td>
                     <td class="style2"></td> <td class="style3"></td>
                </tr>
                 <tr>
                    <td class="style1"></td>
                    <td class="style2">
                        <asp:TextBox ID="txtZonas" runat="server" Width="35px"></asp:TextBox>
                        &nbsp;*Se generarán alfabeticamente</td>
                    <td class="style3">
                        &nbsp;</td>
                </tr>
                <tr>
                     <td class="style1"><b>3. Presiona crear para generar las ZONAS</b>:</td>
                     <td class="style2"> </td> <td class="style3"></td>
                </tr>
                 <tr>
                    <td class="style1"></td>
                    <td class="style2">
                        <asp:Button CssClass="btn btn-large btn-primary" ID="btnZonas" runat="server" Text="Crear Zonas" 
                            onclick="btnZonas_Click" />
                     </td>
                    <td class="style3">  &nbsp;</td>
                </tr>
                  <tr>
                     <td class="style1">
                         <asp:Label ID="Label3" runat="server" 
                             Text="1. Presione para generar las tablas del almacén:" Visible="False"></asp:Label>
                         </td>
                     <td class="style2"></td> <td class="style3">&nbsp;</td>
                     <td rowspan ="6">
                         &nbsp;</td>
                </tr>

                <tr><td colspan="4">&nbsp;</td></tr>
                </table>
                <br /><br />
               <table width ="90%" align="center">
                <tr>
                    <td>
                    </td>
                    <td class="style4">
                         <!-- mensajes -->
                            <div class="container" style="padding-bottom:10px;">    
                                <div id="logsuccess" runat="server" style=" margin-bottom:5px;">  
                                </div>
                                <div id="logerror" runat="server" style=" margin-bottom:5px;">
                                </div>
                            </div>
                    </td>
                    <td>
                    </td>
                </tr>
                 <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                </table> 
                
                <table width ="90%" align="center">
                    <tr>
                         <td></td> 
                         <td align ="right">
                             <asp:Button CssClass="btn btn-large btn-primary" ID="btnSiguiente" 
                                 runat="server" Text="Siguiente -->" Width="159px" 
                                 onclick="btnSiguiente_Click" Visible="False" />
                         </td>
                    </tr> 
                </table>
                </div>        
              </asp:Content>