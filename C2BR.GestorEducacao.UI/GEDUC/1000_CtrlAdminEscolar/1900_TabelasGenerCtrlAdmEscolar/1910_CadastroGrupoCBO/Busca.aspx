<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1910_CadastroGrupoCBO.Busca"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS LIs */
        .liPeriodoAte
        {
            clear: none !important;
            display: inline;
            margin-left: 0px;
            margin-top: 13px;
        }
        .liAux
        {
            margin-left: 5px;
            margin-right: 5px;
            clear: none !important;
            display: inline;
        }
        
        /*--> CSS DADOS */
        .labelAux
        {
            margin-top: 16px;
        }
        .centro
        {
            text-align: center;
        }
        .direita
        {
            text-align: right;
        }
        .ddlNomeReduzido, .ddlUnidade, .ddlCodRefer, .ddlSituacao
        {
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li id="liDescricao" runat="server" class="liDescricao">
            <label title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" Width="200" MaxLength="50"></asp:TextBox>
        </li>
        <li id="liSituacao" runat="server" class="liSituacao">
            <label title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Escolha uma situação" CssClass="ddlSituacao"
                runat="server">
                <asp:ListItem Value="">Selecione</asp:ListItem>
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
