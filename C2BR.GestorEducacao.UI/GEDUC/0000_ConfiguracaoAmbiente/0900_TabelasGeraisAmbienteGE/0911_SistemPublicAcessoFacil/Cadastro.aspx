<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0911_SistemPublicAcessoFacil.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 300px; }
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px; margin-right: 10px;}              
        .liClear{ clear: both;}
        
        /*--> CSS Dados */
        .ddlTipoConexao { width: 95px; }
        .txtSisteServi, .txtURLConexao { width: 275px; }
        .txtDescricao { width: 275px; height: 28px; }
        .ddlStatus { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlTipoConexao" title="Tipo de Conex�o" class="lblObrigatorio">Tipo</label>
            <asp:DropDownList ID="ddlTipoConexao" ToolTip="Selecione o tipo de conex�o" runat="server" CssClass="ddlTipoConexao">
                <asp:ListItem Text="Acesso F�cil" Value="A"></asp:ListItem>
                <asp:ListItem Text="Sistema P�blico" Value="S"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"  runat="server" ControlToValidate="ddlTipoConexao"
                ErrorMessage="Tipo de Conex�o deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 6px;">
            <label for="txtOrgao" title="Org�o" class="lblObrigatorio">Org�o</label>
            <asp:TextBox ID="txtOrgao" ToolTip="Informe o Org�o" runat="server" MaxLength="20" CssClass="campoBairro"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField"  runat="server" ControlToValidate="txtOrgao"
                ErrorMessage="Org�o deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtSisteServi" title="Sistema / Servi�o" class="lblObrigatorio">Sistema / Servi�o</label>
            <asp:TextBox ID="txtSisteServi" ToolTip="Informe o Sistema / Servi�o" runat="server" MaxLength="50" CssClass="txtSisteServi"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"  runat="server" ControlToValidate="txtSisteServi"
                ErrorMessage="Sistema / Servi�o deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDescricao" title="Descri��o" class="lblObrigatorio">Descri��o</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descri��o" runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);" CssClass="txtDescricao"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"  runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descri��o deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtURLConexao" title="URL" class="lblObrigatorio">URL</label>
            <asp:TextBox ID="txtURLConexao" ToolTip="Informe a URL" runat="server" MaxLength="200" CssClass="txtURLConexao"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"  runat="server" ControlToValidate="txtURLConexao"
                ErrorMessage="URL deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlStatus" title="Status" class="lblObrigatorio">Status</label>
            <asp:DropDownList ID="ddlStatus" ToolTip="Selecione o status" runat="server" CssClass="ddlStatus">
                <asp:ListItem Text="Ativa" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativa" Value="I"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validatorField"  runat="server" ControlToValidate="ddlStatus"
                ErrorMessage="Status deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
