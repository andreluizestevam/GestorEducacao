<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0506_CadastroExportacaoDados.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 300px; }
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px; margin-right: 10px;}              
        .liClear{ clear: both;}
        
        /*--> CSS Dados */
        .txtFunciExpor { width: 300px; }
        .ddlStatus { width: 60px; }
        .ddlClassiGrafi { width: 85px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtFunciExpor" title="Funcionalidade da Exportação" class="lblObrigatorio">Funcionalidade</label>
            <asp:TextBox ID="txtFunciExpor" ToolTip="Informe a Funcionalidade da Exportação" runat="server" MaxLength="300" CssClass="txtFunciExpor"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"  runat="server" ControlToValidate="txtFunciExpor"
                ErrorMessage="Funcionalidade da Exportação deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtModulExpor" title="Módulo da Exportação" class="lblObrigatorio">Módulo</label>
            <asp:TextBox ID="txtModulExpor" ToolTip="Informe a Módulo da Exportação" runat="server" MaxLength="300" CssClass="txtFunciExpor"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"  runat="server" ControlToValidate="txtModulExpor"
                ErrorMessage="Módulo da Exportação deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtTabelOrigem" title="Tabela(s) de Origem" class="lblObrigatorio">Tabela(s) de Origem</label>
            <asp:TextBox ID="txtTabelOrigem" ToolTip="Informe a(s) Tabela(s) de Origem da Exportação" runat="server" MaxLength="500" CssClass="txtFunciExpor"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"  runat="server" ControlToValidate="txtTabelOrigem"
                ErrorMessage="Tabela(s) de Origem da Exportação deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtTabelDestino" title="Tabela(s) de Destino" class="lblObrigatorio">Tabela(s) de Destino</label>
            <asp:TextBox ID="txtTabelDestino" ToolTip="Informe a(s) Tabela(s) de Destino da Exportação" runat="server" MaxLength="500" CssClass="txtFunciExpor"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validatorField"  runat="server" ControlToValidate="txtTabelDestino"
                ErrorMessage="Tabela(s) de Destino da Exportação deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlAtualCompl" title="Atualização Completa" class="lblObrigatorio">Atualização Completa</label>
            <asp:DropDownList ID="ddlAtualCompl" ToolTip="Selecione se Atualização Completa" runat="server" CssClass="ddlStatus">
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                <asp:ListItem Text="Não" Value="N" Selected="true"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlStatus" title="Status" class="lblObrigatorio">Status</label>
            <asp:DropDownList ID="ddlStatus" ToolTip="Selecione o status" runat="server" CssClass="ddlStatus">
                <asp:ListItem Text="Ativa" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativa" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
