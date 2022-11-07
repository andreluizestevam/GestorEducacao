<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1909_TipoBeneficio.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 200px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liEspaco { margin-left: 5px; }
        .liTop { margin-top: 10px; }
        
        /*--> CSS DADOS */
        .txtSigla { width: 100px; }
        .txtDescricao { width: 200px; height: 28px; }
        .txtNome { width: 200px; }
        .ddlSituacao { width: 55px; }
        .txtDataCadas { width: 65px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtSigla" class="lblObrigatorio" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" MaxLength="12" CssClass="txtSigla"
                ToolTip="Informe a Sigla"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtNome" class="lblObrigatorio" title="Nome do Tipo de Benefício">
                Nome</label>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="40" CssClass="txtNome"
                ToolTip="Informe o Nome do Tipo de Benefício"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNome"
                ErrorMessage="Nome do Tipo de Benefício deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 100);" ToolTip="Digite a Descrição do Tipo de Benefício" CssClass="txtDescricao">
            </asp:TextBox>
        </li>
        <li class="liClear liTop">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação">Situação</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="ddlSituacao"
                ToolTip="Informe a Situação">
                <asp:ListItem Value="A">Ativa</asp:ListItem>
                <asp:ListItem Value="I">Inativa</asp:ListItem>                   
            </asp:DropDownList>
        </li>
        <li class="liEspaco liTop">
            <label for="txtDataCadas" class="lblObrigatorio" title="Data de Cadastro">Cadastro</label>
            <asp:TextBox ID="txtDataCadas" Enabled="false" ToolTip="Informe a Data de Cadastro" CssClass="txtDataCadas"
                runat="server"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
