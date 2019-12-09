<%@ Page Language="C#" Debug="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IniciarProduccion.aspx.cs" Inherits="JQuery.IniciarProduccion" %>

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
    <div class="body">        
              <h1 align="center">&nbsp;</h1>
              <h1 align="center">Alta Ordenes de Producción</h1>
              <br />
              <form id="Form1">
                <table width ="90%" align="center">
                <tr>
                     <td class="style1" colspan=8><b>1. Introduzca la Orden de Producción</b></td>
                </tr>

                <tr>
                    <td class="style1" align="center" colspan="8">
                        <asp:TextBox ID="txtOP" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1"></td> <td class="style2">
                    &nbsp;</td>
                    <td class="style3">  &nbsp;</td>
                    
                </tr>
                <tr >
                <td colspan="8" align="center">
                    <br />
                    Codigo<br />
                    <asp:Label ID="lblCodigo" runat="server" Text="Codigo" Font-Bold="True" 
                        Font-Size="XX-Large" Visible="False"></asp:Label>
                    <br />
                </td>
                <tr>
                <td align="center" colspan="4">
                    <br />
                    Descripción<br />
                <asp:Label ID="lblDescricpcion" runat="server" Text="Descripción" Font-Bold="True" 
                        Font-Size="XX-Large" Visible="False"></asp:Label>
                    <br />
                </td>
                <td align="center" colspan="4">
                    Cantidad<br />
                <asp:Label ID="lblCantidad" runat="server" Text="Cantidad" Font-Bold="True" 
                        Font-Size="XX-Large" Visible="False"></asp:Label>
                </td>
                </tr>
                <tr>
                    <td align="center" colspan="8">
                        <br />
                        Ruta&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        Centro<br />
                    <asp:Label ID="lblRuta" runat="server" Text="Ruta" Font-Bold="True" 
                            Font-Size="XX-Large" Visible="False"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblCentro" runat="server" Text="Centro" Font-Bold="True" 
                            Font-Size="XX-Large" Visible="False"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>
                     <td class="style1" colspan=8><b>2. Selecciona el tipo de rack a utilizar para esta 
                         orden</b></td>
                     
                </tr>
                 <tr>
                        <td colspan="2"><asp:CheckBox ID="CheckBox1" runat="server" TextAlign="Right" Text="Besser 1"/></td>
                        
                        <td colspan="2"><asp:CheckBox ID="CheckBox2" runat="server" TextAlign="Right" Text="Compactas"/></td>
                        
                        <td colspan="2"><asp:CheckBox ID="CheckBox3" runat="server" TextAlign="Right" Text="Col 22"/></td>
                        
                        <td colspan="2"><asp:CheckBox ID="CheckBox4" runat="server" TextAlign="Right" Text="Col 10"/></td>
                        
                    
                </tr>

                <td colspan=2 class=style1 align=center>
                </td>

                <tr>
                <td class="style1" colspan=8><b>3. Presiona &quot;Calcular&quot; para saber cuantos racks ocupara la 
                         orden</b></td>
                </tr>
                <tr>
                <td colspan=8 class=style1 align=center>
                </td>
                </tr>
                <tr>
                <td colspan=8 class=style1 align=center>
                        <asp:Button CssClass="btn btn-large btn-primary" ID="btnCalcular" 
                            runat="server" Text="Calcular Racks..." 
                            onclick="btnZonas_Click" />
                </td>
                </tr>
                <tr>
                     <td class="style1" colspan="8">
                         <asp:Label ID="lblErrores" runat="server" Visible="False"></asp:Label>
                      </td>
                </tr>
                </tr>
                  <tr>
                     <td class="style1" colspan=8> 
                         <asp:Label ID="lblYolo" runat="server" Text="Se han calculado " Visible="False"></asp:Label>
                         <asp:Label ID="lblCalculo" runat="server" Text="Label" Font-Bold="True" 
                             Font-Size=X-Large Visible="False"></asp:Label>
                         &nbsp;<asp:Label ID="lblYolo2" runat="server" 
                             Text="racks para esta Orden de Producción" Visible="False"></asp:Label>
                         </td>
                </tr>
                <tr><td colspan="8" align=center>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource2" Visible="False">
                        <Columns>
                                                <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EPC" Visible="False">
                            <EditItemTemplate>
                                <asp:Label ID ="EPC" runat="server" Text='<%# Eval("EPC") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="EPC" runat="server" Text='<%# Eval("EPC") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EPC" HeaderText="EPC" 
                                SortExpression="EPC" />
                            <asp:BoundField DataField="Numero" HeaderText="Numero" 
                                SortExpression="Numero" />
                            <asp:BoundField DataField="IdRProd" HeaderText="IdRProd" 
                                SortExpression="IdRProd" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:napresaReal %>" 
                        SelectCommand="SELECT [EPC], [Numero], [IdRProd] FROM [DetRProd] WHERE ([Estado] = @Estado)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="Estado" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:DesarrolloNapresaConnectionString %>" 
                        
                        SelectCommand="SELECT [IdDRP], [EPC], [Numero] FROM [DetRProduccion] WHERE ([Estado] = @Estado)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="Estado" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:labelsSolutia %>" 
                        
                        
                        
                        SelectCommand="SELECT Articulo, ArtDescripcion, Cantidad, Ruta, Centro FROM ProdPendienteD WHERE (MovID = @MovID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtOP" Name="MovID" PropertyName="Text" 
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    </td></tr>
                </table>
                <br /><br />
               <table width ="90%" align="center">
                <tr>
                    <td>
                    </td>
                    <td class="style4">
                         <!-- mensajes -->
                            <div class="container" style="padding-bottom:10px;">    
                                <div id="logsuccess1" runat="server" style=" margin-bottom:5px;">  
                                </div>
                                <div id="logerror1" runat="server" style=" margin-bottom:5px;">
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
                             <asp:Button CssClass="btn btn-large btn-primary" ID="btnSiguiente2" 
                                 runat="server" Text="Siguiente -->" Width="159px" 
                                 onclick="btnSiguiente_Click" />
                         </td>
                    </tr> 
                </table>
                </div>        
              </asp:Content>
