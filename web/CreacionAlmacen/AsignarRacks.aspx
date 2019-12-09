<%@ Page Language="C#" Debug="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignarRacks.aspx.cs" Inherits="JQuery.AsignarRacks" MaintainScrollPositionOnPostback="true" %>

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
        .auto-style1 {
            height: 22px;
        }

        .auto-style2 {
            height: 41px;
        }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="body">
        <br />
        <h1 align="center">Asignación de Racks Adicionales</h1>
        <br />
        <table width="90%" align="center">
            <tr>
                <td class="auto-style1">
                    <b>1. Seleccione la Orden de Produccion a revisar
                    </b>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="txtOP" runat="server"></asp:TextBox>
                </td>
            </tr>
            <!--renglon intencionalmente dejado en blanco-->
            <tr>
                <td></td>
            </tr>
            <!--renglon intencionalmente dejado en blanco-->
            <tr>
                <td align="center" class="auto-style2">
                    <asp:Button CssClass="btn btn-large btn-primary" ID="btnBuscar" runat="server" Text="Verificar Orden" OnClick="btnBuscar_Click" />
                </td>
            </tr>
            <!--renglon intencionalmente dejado en blanco-->
            <tr>
                <td>
                    <asp:Label ID="lblOP" runat="server" Visible="false" Text="Orden de Producción"></asp:Label>
                    <asp:Label ID="lblTR" runat="server" Visible="false" Text="0"></asp:Label>
                    <asp:Label ID="lblRenglon" runat="server" Visible="false" Text="Renglon"></asp:Label>
                    <asp:Label ID="lblHuecos" runat="server" Visible="false" Text="Huecos"></asp:Label>
                    <asp:Label ID="lblCodigo" runat="server" Visible="false" Text="Codigo"></asp:Label>
                    <asp:Label ID="lblNumero" runat="server" Visible="false" Text="Numero de Rack"></asp:Label>
                    <asp:Label ID="lblCalculo" runat="server" Visible="false" Text="Calculo"></asp:Label>
                </td>
            </tr>
            <!--renglon intencionalmente dejado en blanco-->
            <tr>
                <td align="center">
                    <br />
                    <form>
                        <asp:GridView ID="gvOP" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" DataSourceID="dataSourceOrdenes" DataKeyNames="Renglon">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:CommandField ButtonType="Button" SelectText="X" ShowSelectButton="true" ControlStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle Width="30px"></ControlStyle>
                                </asp:CommandField>
                                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                                <asp:BoundField DataField="OrdenProduccion" HeaderText="Orden de Produccion" SortExpression="OrdenProduccion" />
                                <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" />
                                <asp:BoundField DataField="Codigo" HeaderText="Codigo de Producto" SortExpression="Codigo" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
                                <asp:BoundField DataField="Estatus" HeaderText="Estatus" SortExpression="Estatus" />
                                <asp:BoundField DataField="Renglon" HeaderText="Renglon" SortExpression="Renglon" />
                                <asp:BoundField DataField="mermas" HeaderText="Huecos Actuales" SortExpression="mermas" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </form>
                    <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar Orden" Visible="false" Width="250" CssClass="btn btn-large btn-primary" OnClick="btnSeleccionar_Click" />
                    <asp:SqlDataSource ID="dataSourceOrdenes" runat="server" ConnectionString="<%$ ConnectionStrings:napresaSolutia %>"
                        SelectCommand="SELECT [Id], [OrdenProduccion], [Cliente], [Codigo], [Descripcion], [Cantidad], [Estatus], [Renglon], [mermas] FROM [catProd] WHERE ([OrdenProduccion] = @OrdenProduccion) AND ([Asignado] = @Asignado)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtOP" Name="OrdenProduccion" PropertyName="Text" Type="String" />
                            <asp:Parameter DefaultValue="1" Name="Asignado" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Table runat="server" HorizontalAlign="Center" Width="90%" ID="tblRacks" Visible="false">
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Center" Text="Racks Asignados" Width="50%"></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Text="Racks Disponibles" Width="50%"></asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                <br />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    Introduzca el número de Rack a asignar<br />
                    <asp:TextBox ID="txtNumero" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnNumero" runat="server" Text="Buscar Rack" CssClass="btn btn-large btn-primary" OnClick="btnNumero_Click" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                    <asp:GridView ID="gvRacksAsignados" runat="server" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" DataSourceID="datasourceRacksAsignados" AllowPaging="true" PageSize="15">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="Numero" HeaderText="Número" SortExpression="Numero" />
                            <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="datasourceRacksAsignados" runat="server" ConnectionString="<%$ ConnectionStrings:napresaSolutia %>"
                        SelectCommand="SELECT [DetRProd].[Numero], [RProduccion].[Modelo] FROM [DetRProd] INNER JOIN [RProduccion] on [RProduccion].[IdRack] = [DetRProd].[IdRProd] WHERE (([Estado] = @Estado) AND ([OrdenProduccion] = @OrdenProduccion))">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="1" Name="Estado" Type="Int32" />
                            <asp:ControlParameter ControlID="lblOP" Name="OrdenProduccion" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top">
                    <asp:GridView ID="gvRacksDisponibles" runat="server" AutoGenerateColumns="False" GridLines="None"
                        DataSourceID="datasourceRacksDisponibles" AllowPaging="true" PageSize="15" DataKeyNames="Numero">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSeleccion" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Numero" Visible="False">
                                <EditItemTemplate>
                                    <asp:Label ID="Numero" runat="server" Text='<%# Eval("Numero") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Numero" runat="server" Text='<%# Eval("Numero") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Numero" HeaderText="Numero" SortExpression="Numero" />
                            <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="datasourceRacksDisponibles" runat="server" ConnectionString="<%$ ConnectionStrings:napresaSolutia %>"
                        SelectCommand="SELECT [DetRProd].[Numero], [RProduccion].[Modelo] FROM [DetRProd] INNER JOIN [RProduccion] on [RProduccion].[IdRack] = [DetRProd].[IdRProd] WHERE ([Numero] = @Numero) AND ([Estado] = @Estado) AND ([IdRProd] = @tipoRack)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="Estado" Type="Int32" />
                            <asp:ControlParameter ControlID="lblTR" Name="tipoRack" Type="Int32" />
                            <asp:ControlParameter ControlID="txtNumero" Name="Numero" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                <br />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblTexto" runat="server" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtCantidad" runat="server" Visible="false"></asp:TextBox>
                    <br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                <br />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:Button ID="btnCantidad" Text="Asignar Rack" runat="server" CssClass="btn btn-large btn-primary" OnClick="btnCantidad_Click" Visible="true" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
