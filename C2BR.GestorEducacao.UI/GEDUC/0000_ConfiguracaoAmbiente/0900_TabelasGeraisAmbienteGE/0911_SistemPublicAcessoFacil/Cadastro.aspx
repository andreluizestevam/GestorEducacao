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
            <label for="ddlTipoConexao" title="Tipo de Conexão" class="lblObrigatorio">Tipo</label>
            <asp:DropDownList ID="ddlTipoConexao" ToolTip="Selecione o tipo de conexão" runat="server" CssClass="ddlTipoConexao">
                <asp:ListItem Text="Acesso Fácil" Value="A"></asp:ListItem>
                <asp:ListItem Text="Sistema Público" Value="S"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"  runat="server" ControlToValidate="ddlTipoConexao"
                ErrorMessage="Tipo de Conexão deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 6px;">
            <label for="txtOrgao" title="Orgão" class="lblObrigatorio">Orgão</label>
            <asp:TextBox ID="txtOrgao" ToolTip="Informe o Orgão" runat="server" MaxLength="20" CssClass="campoBairro"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField"  runat="server" ControlToValidate="txtOrgao"
                ErrorMessage="Orgão deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtSisteServi" title="Sistema / Serviço" class="lblObrigatorio">Sistema / Serviço</label>
            <asp:TextBox ID="txtSisteServi" ToolTip="Informe o Sistema / Serviço" runat="server" MaxLength="50" CssClass="txtSisteServi"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"  runat="server" ControlToValidate="txtSisteServi"
                ErrorMessage="Sistema / Serviço deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDescricao" title="Descrição" class="lblObrigatorio">Descrição</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descrição" runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);" CssClass="txtDescricao"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"  runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
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
