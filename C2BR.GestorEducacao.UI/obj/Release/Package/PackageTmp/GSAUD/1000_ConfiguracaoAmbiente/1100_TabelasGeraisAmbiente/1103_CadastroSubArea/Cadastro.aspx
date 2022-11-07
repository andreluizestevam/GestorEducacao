<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1103_CadastroSubArea.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 10px;
            margin-right: 10px;
        }
        .liClear
        {
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlRegiao" title="Região" class="lblObrigatorio">
                Região</label>
            <asp:DropDownList ID="ddlRegiao" ToolTip="Selecione uma Região" runat="server" CssClass="campoRegiao"
                AutoPostBack="true" OnSelectedIndexChanged="ddlRegiao_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"
                runat="server" ControlToValidate="ddlRegiao" ErrorMessage="Região deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlArea" title="Área" class="lblObrigatorio">
                Área</label>
            <asp:DropDownList ID="ddlArea" ToolTip="Selecione uma Área" runat="server" CssClass="campoArea">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"
                runat="server" ControlToValidate="ddlArea" ErrorMessage="Área deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtSubarea" title="Subárea" class="lblObrigatorio">
                Subárea</label>
            <asp:TextBox ID="txtSubarea" ToolTip="Informe uma Subárea" runat="server" MaxLength="80"
                CssClass="campoSubarea"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
                runat="server" ControlToValidate="txtSubarea" ErrorMessage="Subárea deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtSigla" title="Sigla" class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Informe uma Sigla" runat="server" MaxLength="12"
                CssClass="campoSigla"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"
                runat="server" ControlToValidate="txtSigla" ErrorMessage="Sigla deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
