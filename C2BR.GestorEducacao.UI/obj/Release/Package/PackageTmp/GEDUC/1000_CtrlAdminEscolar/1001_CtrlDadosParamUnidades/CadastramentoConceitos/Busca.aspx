<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoConceitos.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlUnidade
        {
            width: 270px;
        }
        .txtInstituicao
        {
            width: 180px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label>
                Nome</label>
            <asp:TextBox ID="txtNome" CssClass="txtInstituicao" runat="server"></asp:TextBox>
        </li>
        <li>
            <label>
                Sigla</label>
            <asp:TextBox runat="server" ID="txtSgl" Width="100px"></asp:TextBox>
        </li>
        <li>
            <label for="ddlUnidade" title="Selecione a Unidade">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidade" runat="server">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Situação do Conceito">
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
