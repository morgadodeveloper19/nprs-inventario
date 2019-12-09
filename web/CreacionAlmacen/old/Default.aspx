<%@ Page Title="Creacion Almacen" Language="C#" Debug="true" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CreacionAlamacen._Default" %>

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
            width: 275px;
        }
        .style2
        {
            width: 78px;
            height: 88px;
        }
        #t1
        {
            height: 3px;
        }
        .style4
        {
            width: 275px;
            height: 36px;
        }
        .style5
        {
            width: 78px;
            height: 36px;
        }
        .style6
        {
            width: 78px;
            height: 89px;
        }
        </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
            <div class="body">        
              <h1 align="center">CREACION DE RACKS POR ZONA</h1>
              <br />
                <table width ="90%" align="center">
                    <tr>
                        <td>Seleccione la zona donde estarán los Racks:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlZonas" runat="server" DataSourceID="SQLDS_Ricsa" 
                                DataTextField="ClaveZona" DataValueField="IdZona" Height="36px" 
                                Width="70px"> </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>

              <form id="Form1">
                <table width ="90%" align="center">
                <tr>
                     <td class="style1"><strong>Cuantos racks contendrá el almacén?</strong>  </td>                     
                     <td rowspan ="6" align="right">
                        
                         <IMG SRC="Racks2.png" ALT="Diseño del rack" >                     
                         <!--<IMG SRC="Racks.png" ALT="Diseño del rack" style="height: 278px; width: 234px">   -->
                     </td>
                </tr>
                <tr>
                    <td class="style2" align ="right">
                        RACKS:  
                        <asp:TextBox ID="txtRacks" runat="server" Width="43px"></asp:TextBox>  
                    </td>
                </tr>
                <tr>
                     <td class="style1"><strong> Cuantos niveles contendrá cada rack?</strong>  </td>                      
                </tr>
                 <tr>                    
                    <td class="style6" align ="right">
                        NIVELES: 
                        <asp:TextBox ID="txtNiveles" runat="server" Width="39px"></asp:TextBox>
                    </td>                    
                </tr>
                <tr>
                     <td class="style1"><strong>Cuantas ventanas tendra cada rack?</strong></td>
                </tr>
                 <tr>                    
                    <td class="style5" align ="right">
                        VENTANAS:  
                        <asp:TextBox ID="txtVentanas" runat="server" Width="40px"></asp:TextBox>  
                    </td>
                </tr>
                <tr><td colspan="4"><b>*Las ventanas se crean con una posición</b></td></tr>
                </table>
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
                </table>  
                <br />
                     
                <table width ="70%" align="center">
                <tr>
                    <td align="center">  
                         <asp:Button CssClass="btn btn-large btn-primary" ID="btnGenerar0" 
                             runat="server" Text="Vista Previa" Height="40px" 
                            Width="145px" onclick="btnGenerar0_Click" />
                        
                    </td>
                    <td align="center">
                            <asp:Button CssClass="btn btn-large btn-primary" ID="btnGenerar" runat="server" 
                                Text="Generar" Height="41px"  
                            Width="107px" onclick="btnGenerar_Click" />
                    </td>
                </tr>
                </table>    
              </div>        
              <br />
              <table align ="center"> 
                <tr>
                    <td colspan="3" align="center">
                         <asp:Label ID="lblMessage" runat="server" visible="False" Font-Bold="True" 
                             Font-Size="Large">Asi seria la vista frontal de cada rack</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblZona" runat="server" visible="False" Font-Bold="True" 
                             Font-Size="XX-Large"></asp:Label> &nbsp;
                    </td>
                     <td>
                        <asp:Label ID="lblCosas" runat="server" visible="False" Font-Bold="True" 
                             Font-Size="XX-Large"></asp:Label> &nbsp;
                    </td>
                    <td><table id="t1" border="2" runat="server" visible="false" class="table table-striped"/> </td>
                </tr> 
             </table>
            
            </form>
             <br />
             <br />
                <asp:SqlDataSource ID="SQLDS_Ricsa" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:Ricsa %>" 
                    SelectCommand="SELECT [IdZona], [ClaveZona] FROM [Zonas]">
                </asp:SqlDataSource>
                
</asp:Content>
