<%@ Page Title="Arreglos" Language="C#" Debug="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Arreglos.aspx.cs" Inherits="JQuery.Arreglos" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="bootstrap/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap-tooltip.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap-popover.js" type="text/javascript"></script>
    <script src="bootstrap/jsFunctions/createGrid.js" type="text/javascript"></script>
    <link href="bootstrap/css/bootstrap.css" rel="Stylesheet" media="screen" />
    <link href="bootstrap/css/bootstrap-responsive.css" rel="Stylesheet" media="screen" />
    <script type="text/javascript">
    </script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1 align="center">Arreglos</h1>

 <table width ="90%" align="center">
 <tr>
    <td>    
        Seleccione la ZONA        
    </td>
    <td>
        <asp:DropDownList ID="ddlZonas2" runat="server" AutoPostBack="True" 
            DataSourceID="DSZonas2" DataTextField="ClaveZona" DataValueField="IdZona" 
            Width="70px" onselectedindexchanged="ddlZonas2_SelectedIndexChanged">
        </asp:DropDownList>
    </td>
 </tr>
 <tr>
    <td>   
        Seleccione el RACK        
    </td>
     <td>
         <asp:DropDownList ID="ddlRacks2" runat="server" AutoPostBack="True" 
             DataSourceID="DSRacks2" DataTextField="Clave" DataValueField="IDRack" 
             Width="70px" onselectedindexchanged="ddlRacks2_SelectedIndexChanged">
         </asp:DropDownList>
    </td>
 </tr>
 <tr>
    <td>    
        Seleccione el NIVEL
    </td>
     <td>
         <asp:DropDownList ID="ddlNiveles2" runat="server" AutoPostBack="True" 
             DataSourceID="DSNiveles2" DataTextField="Clave" DataValueField="IDNivel" 
             Width="70px" onselectedindexchanged="ddlNiveles2_SelectedIndexChanged">
         </asp:DropDownList>
    </td>
 </tr>
 <tr>
    <td>    
        Seleccione el VENTANA
    </td>
     <td>
         <asp:DropDownList ID="ddlVentanas2" runat="server" AutoPostBack="True" 
             DataSourceID="DSVentanas2" DataTextField="Clave" DataValueField="IDVentana" 
             Width="70px" onselectedindexchanged="ddlVentanas2_SelectedIndexChanged">
         </asp:DropDownList>
    </td>
 </tr>
 <tr>
    <td>
 
        Seleccione la POSICION</td>
    <td>    
        
        <asp:DropDownList ID="ddlPosiciones" runat="server" Width="70px" 
            AutoPostBack="True" DataSourceID="DSPosiciones" DataTextField="Clave" 
            DataValueField="IDPosicion" 
            onselectedindexchanged="ddlPosiciones_SelectedIndexChanged">
        </asp:DropDownList>
        
    </td>
 </tr>
 <tr>
    <td>
    
        Cambiar a posicion:</td>
    <td>    
        
        <asp:DropDownList ID="ddlOpciones" runat="server">                   
        </asp:DropDownList>

         <!--<asp:ListItem Value="3">Puente</asp:ListItem>-->
    </td>
 </tr>
 <tr>
    <td>
 
    </td>
    <td>    
        <asp:Button ID="Button1" CssClass="btn btn-large btn-primary" runat="server" Text="Arreglos" />    
    </td>
 </tr>
 </table>

    <br />
    
     <!-- mensajes -->
    <div class="container" style="padding-bottom:10px;">    
        <div id="logsuccess" runat="server" style=" margin-bottom:5px;">  
        </div>
        <div id="logerror" runat="server" style=" margin-bottom:5px;">
        </div>
    </div>
    <asp:SqlDataSource ID="DSZonas2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Ricsa %>" 
        SelectCommand="SELECT [IdZona], [ClaveZona] FROM [Zonas]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DSRacks2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Ricsa %>" 
        SelectCommand="SELECT [Clave], [IDRack], [IDZona] FROM [racks] WHERE ([IDZona] = @IDZona)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlZonas2" Name="IDZona" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DSNiveles2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Ricsa %>" 
        SelectCommand="SELECT [IDNivel], [Clave], [IDRack] FROM [niveles] WHERE ([IDRack] = @IDRack)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlRacks2" Name="IDRack" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DSVentanas2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Ricsa %>" 
        SelectCommand="SELECT [IDVentana], [Clave], [IDNivel], [Tipo] FROM [ventanas] WHERE ([IDNivel] = @IDNivel)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlNiveles2" Name="IDNivel" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="DSPosiciones" 
    ConnectionString="<%$ ConnectionStrings:Ricsa %>" 
    SelectCommand="SELECT [IDPosicion], [Clave] FROM [posiciones] WHERE ([IDVentana] = @IDVentana)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlVentanas2" Name="IDVentana" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

</asp:Content>
