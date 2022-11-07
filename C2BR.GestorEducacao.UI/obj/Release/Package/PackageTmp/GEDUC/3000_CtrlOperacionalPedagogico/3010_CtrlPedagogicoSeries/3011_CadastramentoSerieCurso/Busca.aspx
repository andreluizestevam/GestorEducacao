<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3011_CadastramentoSerieCurso.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */ 
        .ddlClassificacao { width: 125px; }
        .ddlSituacao { width: 80px; }
        .direita { text-align: right;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlClassificacao" title="Classificação">
            Classificação</label>
        <asp:DropDownList ID="ddlClassificacao" runat="server" CssClass="ddlClassificacao"
            ToolTip="Selecione a Classificação">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlModalidade" title="Modalidade">Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server"
            ToolTip="Selecione a Modalidade">
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtNome" title="Curso">Curso</label>
        <asp:TextBox ID="txtNome" CssClass="campoSerieCurso" runat="server" MaxLength="50"
            ToolTip="Informe a Série/Curso">
        </asp:TextBox>
    </li>
    <li>
        <label for="ddlSituacao" style="display:block !important;" title="Situação da Curso">
            Situação Curso</label>
        <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" ToolTip="Selecione a Situação da Série">
            <asp:ListItem Value="T">Todas</asp:ListItem>
            <asp:ListItem Value="A">Ativo</asp:ListItem>
            <asp:ListItem Value="S">Suspenso</asp:ListItem>
            <asp:ListItem Value="C">Cancelado</asp:ListItem>
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
