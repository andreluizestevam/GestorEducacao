<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="RelacMensagensSMS.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0399_Relatorios.RelacMensagensSMS"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
            margin: 50px 0 0 347px !important;
        }
        .ulDados li
        {
            margin-left: 5px;
            margin-bottom: 5px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlUnidadee" class="lblObrigatorio labelPixel" title="Unidade">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged"
                AutoPostBack="true" runat="server" Style="width: 250px;">
            </asp:DropDownList>
        </li>
        <li>
            <label id="Label1" class="lblObrigatorio" title="Usuario">
                Nome Colaborador
            </label>
            <asp:DropDownList ID="ddlColaborador" CssClass="ddlColaborador" ToolTip="Selecione o Usuário"
                runat="server" Style="width: 260px;">               
            </asp:DropDownList>
        </li>
        <li>
            <label class="lblObrigatorio" title="Tipo Contato">
                Tipo Contato</label>
            <asp:DropDownList ID="ddlTpContato" OnSelectedIndexChanged="ddlTpContato_OnSelectedIndexChanged"
                AutoPostBack="true" runat="server" Style="width: 100px;">
                <asp:ListItem Value="0" Selected="true">Todos</asp:ListItem>
                <asp:ListItem Value="F">Funcionário</asp:ListItem>
                <asp:ListItem Value="P">Professor</asp:ListItem>
                <asp:ListItem Value="A">Aluno</asp:ListItem>
                <asp:ListItem Value="R">Responsável</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label id="Label2" class="lblObrigatorio" title="Usuario">
                Nome Destinatario
            </label>
            <asp:DropDownList ID="ddlNomeDestinatario" CssClass="ddlNomeDestinatario" ToolTip="Selecione o Usuário"
                runat="server" Style="width: 260px;">
            </asp:DropDownList>
            <asp:TextBox ID="txtOutroDestinatario" runat="server" Style="width: 240px;" Visible="False"></asp:TextBox>
        </li>
        <li>
            <label class="lblObrigatorio" title="Status">
                Status</label>
            <asp:DropDownList ID="ddlStatus" ToolTip="Selecione o Status" CssClass="ddlStatus"
                runat="server">
                <asp:ListItem Selected="True" Value="0">Todas</asp:ListItem>
                <asp:ListItem Value="S">Enviadas</asp:ListItem>
                <asp:ListItem Value="N">Não Enviadas</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Período</label>
            <asp:TextBox ID="txtDataInicial" CssClass="campoData" runat="server"></asp:TextBox>
            à
            <asp:TextBox ID="txtDataFinal" CssClass="campoData" runat="server"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
