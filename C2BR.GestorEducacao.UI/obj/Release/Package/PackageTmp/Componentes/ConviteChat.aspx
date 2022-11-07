<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConviteChat.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.ConviteChat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmConviteChat" runat="server">
        <div>
            <asp:GridView 
                ID="grdUsuario" 
                CssClass="grdBusca" 
                runat="server" 
                AutoGenerateColumns="False"
                ShowHeader="false">

                <RowStyle CssClass="grdLinha" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum registro encontrado.<br />
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hidIdUsu" Value='<%# Eval("idUsu") %>' />
                            <asp:HiddenField runat="server" ID="hidCoUsu" Value='<%# Eval("coUsu") %>' />
                            <asp:HiddenField runat="server" ID="hidTpUsu" Value='<%# Eval("noUsu") %>' />
                            <asp:CheckBox runat="server" ID="chkSelUsu" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="noUsu" HeaderText="USUÁRIO">
                        <ItemStyle Width="25px" HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>


            <asp:LinkButton ID="lnkConvUsu" OnClick="lnkConvUsu_Click" ValidationGroup="SelSala"
                runat="server" ToolTip="Clique para Convidar os Usuários Selecionados">
                <asp:Label ID="Label1" runat="server">CONVIDAR</asp:Label>
            </asp:LinkButton>

        </div>
    </form>
</body>
</html>
