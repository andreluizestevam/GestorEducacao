<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6250_LanctoEntradaSaidaItensEstoque.F6251_RegistroMovimentacaoEstoque.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlTipoMovimentacao" class="labelPixel" title="Tipo de Movimentação">
                Tipo Movimentação</label>
            <asp:DropDownList ID="ddlTipoMovimentacao" Width="90px" ToolTip="Selecione o Tipo de Movimentação"
                runat="server">
                <asp:ListItem Text ="Entrada" Value="E" Selected="True"></asp:ListItem>
                <asp:ListItem Text ="Saída" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-top: 5px !important;">
            <label for="ddlProduto" style="margin-bottom: -6px" title="Nome do produto a ser movimentado">
                Produto</label>
            <asp:TextBox runat="server" ID="txtProduto" Width="250px" Style="margin: 0;" MaxLength="30" ToolTip="Informe o nome do produto"></asp:TextBox>
            <asp:ImageButton ID="imgbPesqPacNome" CssClass="btnProcurar"
                runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgbPesqProduto_OnClick" />
            <asp:DropDownList runat="server" ID="ddlProduto" Visible="false" Width="250px" ToolTip="Selecione o Produto">
            </asp:DropDownList>
            <asp:ImageButton ID="imgbVoltarPesq" CssClass="btnProcurar"
                Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgbVoltarPesq_OnClick"
                Visible="false" runat="server" />
        </li> 
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
