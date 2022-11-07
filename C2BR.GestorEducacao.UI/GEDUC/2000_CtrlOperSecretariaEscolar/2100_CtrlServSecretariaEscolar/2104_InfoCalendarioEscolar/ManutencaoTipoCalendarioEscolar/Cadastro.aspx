<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2104_InfoCalendarioEscolar.ManutencaoTipoCalendarioEscolar.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 266px;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-right: 10px;}        
        .liClear { clear: both; }   
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição do Calendário">Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" MaxLength="30" CssClass="campoDescricao" ToolTip="Informe a Descrição do Calendário"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtSigla" class="lblObrigatorio" title="Sigla do Calendário">Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" MaxLength="12" CssClass="campoSigla" ToolTip="Informe a Sigla do Calendário"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla" 
            ErrorMessage="Sigla deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação do Calendário">Situação</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="ddlSituacao" ToolTip="Informe a Situação do Calendário">
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtDataSituacao" class="lblObrigatorio" title="Data da Situação do Calendário">Data da Situação</label>
            <asp:TextBox ID="txtDataSituacao" Enabled="False" runat="server" MaxLength="12" CssClass="campoData" ToolTip="Informe a Data da Situação do Calendário"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDataSituacao" 
            ErrorMessage="Data da Situação deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
