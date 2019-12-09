<%@ Page Language="C#" Debug="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignacionProduccion.aspx.cs" Inherits="JQuery.AsignacionProduccion" MaintainScrollPositionOnPostback="true" %>

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
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="body">
        <br />
        <h1 align="center">Asignación de Producción</h1>
        <br />
        <table width="90%" align="center">
            <tr>
                <td class="auto-style1"><b>1. Inserte la Orden de Producción a Asignar:</b></td>
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
                <td align="center">
                    <asp:Button CssClass="btn btn-large btn-primary" ID="btnBuscar" runat="server"
                        Text="Verificar Orden" OnClick="btnBuscar_Click" />
                </td>
            </tr>
            <!--renglon intencionalmente dejado en blanco-->
            <tr>
                <td>
                    <asp:Label ID="tempCod" runat="server" Text="Label" Visible="False"></asp:Label>
                    <asp:Label ID="lblTR" runat="server" Text="Label" Visible="False"></asp:Label>
                    <asp:Label ID="lblRenglon" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
            </tr>
            <!--renglon intencionalmente dejado en blanco-->
            <tr>
                <td align="center">
                    <br />
                    <form>
                        <asp:GridView ID="gridOP" runat="server" AutoGenerateColumns="False"
                            DataSourceID="datasourceRenglones" ForeColor="#333333"
                            GridLines="None" DataKeyNames="Renglon">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:CommandField ButtonType="Button" SelectText="X" ShowSelectButton="True"
                                    ControlStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle Width="30px"></ControlStyle>
                                </asp:CommandField>
                                <asp:BoundField DataField="Pedido" HeaderText="Pedido"
                                    SortExpression="Pedido" />
                                <asp:BoundField DataField="OrdenProduccion" HeaderText="Orden de Producción"
                                    SortExpression="OrdenProduccion" />
                                <asp:BoundField DataField="Cliente" HeaderText="Cliente"
                                    SortExpression="Cliente" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Codigo" HeaderText="Código"
                                    SortExpression="Codigo" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción"
                                    SortExpression="Descripcion" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad"
                                    SortExpression="Cantidad" />
                                <asp:BoundField DataField="Renglon" HeaderText="Renglon"
                                    SortExpression="Renglon" Visible="true" />
                                <asp:BoundField DataField="Medida" HeaderText="Unidad Compra"
                                    SortExpression="Medida" />
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
                    <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar Orden"
                        OnClick="btnSeleccionar_Click" Visible="False" Width="250" CssClass="btn btn-large btn-primary" />
                    <asp:SqlDataSource ID="datasourceRenglones" runat="server"
                        ConnectionString="<%$ ConnectionStrings:napresaSolutia %>"
                        SelectCommand="SELECT [Pedido], [OrdenProduccion], [Cliente], [Codigo], [Descripcion], [Cantidad], [Renglon], [Medida] FROM [catProd] WHERE (([OrdenProduccion] = @OrdenProduccion) AND ([Estatus] = @Estatus) AND ([Asignado] = @Asignado))">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtOP" Name="OrdenProduccion"
                                PropertyName="Text" Type="String" />
                            <asp:Parameter DefaultValue="PENDIENTE" Name="Estatus" Type="String" />
                            <asp:Parameter DefaultValue="0" Name="Asignado" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dataSourceLabels" runat="server"
                        ConnectionString="<%$ ConnectionStrings:labelsSolutia %>"
                        SelectCommand="SELECT Articulo, ArtDescripcion, Cantidad, Ruta, Centro FROM ProdPendienteD WHERE (MovID = @MovID) and (Articulo = @Articulo)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtOP" Name="MovID" PropertyName="Text"   Type="String" />
                             <asp:ControlParameter ControlID="tempCod" Name="Articulo" PropertyName="Text" />
           
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        <br />
        <asp:Table runat="server" HorizontalAlign="Center" ID="tblLabels" Visible="false">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                    Código<br />
                    <asp:Label ID="lblCodigo" runat="server" Font-Bold="True" Font-Size="XX-Large"
                        Text="lblCodigo"></asp:Label><br />
                    <br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                    Descripción<br />
                    <asp:Label ID="lblDescripcion" runat="server" Font-Bold="True"
                        Font-Size="XX-Large" Text="lblDescripcion"></asp:Label>
                    <br />
                    <br />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    Cantidad<br />
                    <asp:Label ID="lblCantidad" runat="server" Font-Bold="True"
                        Font-Size="XX-Large" Text="lblCantidad" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtCantidad" runat="server" Font-Size="XX-Large" Width="75%" Enabled="false"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2">
                    <asp:RadioButton Text="Producción Completa" ID="rbComp" TextAlign="Right" runat="server" OnCheckedChanged="rbComp_CheckedChanged" AutoPostBack="true" GroupName="rbGroup" />
                    <asp:RadioButton Text="Producción Parcial" ID="rbParc" TextAlign="Right" runat="server" OnCheckedChanged="rbParc_CheckedChanged" AutoPostBack="true" GroupName="rbGroup" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                    Ruta<br />
                    <asp:Label ID="lblRuta" runat="server" Font-Bold="True"
                        Font-Size="XX-Large" Text="lblRuta"></asp:Label>
                    <br />
                    <br />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                    Centro
                    <br />
                    <asp:Label ID="lblCentro" runat="server" Font-Bold="True" Font-Size="XX-Large"
                        Text="lblCentro"></asp:Label>
                    <br />
                    <br />
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>

        <br />
        <asp:Table Width="90%" HorizontalAlign="Center" runat="server" Visible="false" ID="tblRacks">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4"><b>2. Seleccione el Tipo de Rack a utilizar</b></asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:CheckBox ID="CheckBox5" runat="server" TextAlign="Right" Text="Besser 1" /></asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:CheckBox ID="CheckBox6" runat="server" TextAlign="Right" Text="Compactas" /></asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:CheckBox ID="CheckBox7" runat="server" TextAlign="Right" Text="Col 22" /></asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:CheckBox ID="CheckBox8" runat="server" TextAlign="Right" Text="Col 10" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                    <asp:Button ID="btnCalcular" runat="server" Text="Calcular Racks" OnClick="btnCalcular_Click" Width="350" CssClass="btn btn-large btn-primary" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><br /><br /></asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                    <asp:Label ID="lblParte1" runat="server" Text="Se han calculado " Visible="false"></asp:Label>
                    <asp:Label ID="lblCalculo" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large" Visible="false"></asp:Label>
                    &nbsp;
        <asp:Label ID="lblParte2" runat="server" Text=" racks para esta Orden de Producción" Visible="false"></asp:Label>
                    <br />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <asp:GridView HorizontalAlign="Center" ID="gridRacks" runat="server" AutoGenerateColumns="False"
            ForeColor="#333333" GridLines="None" DataSourceID="dataSourceRacks"
            Visible="False" AllowPaging="True" PageSize="20" OnPageIndexChanged="gridRacks_PageIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Seleccionar">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EPC" Visible="False">
                    <EditItemTemplate>
                        <asp:Label ID="EPC" runat="server" Text='<%# Eval("EPC") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="EPC" runat="server" Text='<%# Eval("EPC") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="EPC" HeaderText="EPC"
                    SortExpression="EPC" />
                <asp:BoundField DataField="Numero" HeaderText="Número"
                    SortExpression="Numero" />
                <asp:BoundField DataField="Modelo" HeaderText="Modelo"
                    SortExpression="Modelo" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerSettings PageButtonCount="15" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <%--<asp:SqlDataSource ID="dataSourceRacks" runat="server" 
                ConnectionString="<%$ ConnectionStrings:napresaSolutia %>"                 
                SelectCommand="SELECT [EPC], [Numero], [IdRProd] FROM [DetRProd] WHERE (([Estado] = @Estado) AND ([IdRProd] = @IdRProd))">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="Estado" Type="Int32" />
                    <asp:ControlParameter ControlID="lblTR" Name="IdRProd" PropertyName="Text" 
                        Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>--%>
        <asp:SqlDataSource ID="dataSourceRacks" runat="server"
            ConnectionString="<%$ ConnectionStrings:napresaSolutia %>"
            SelectCommand="SELECT [DetRProd].[EPC], [DetRProd].[Numero], [RProduccion].[Modelo] FROM [DetRProd] 
                INNER JOIN [RProduccion] on [RProduccion].[IdRack] = [DetRProd].[IdRProd] WHERE (([Estado] = @Estado) AND ([IdRProd] = @IdRProd))">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="Estado" Type="Int32" />
                <asp:ControlParameter ControlID="lblTR" Name="IdRProd" PropertyName="Text"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:Table HorizontalAlign="Center" runat="server" ID="tblFinal" Width="90%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:Button ID="btnFinalizar" Text="Finalizar" runat="server" CssClass="btn btn-large btn-primary" Width="450px" OnClick="btnFinalizar_Click" Visible="false" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
