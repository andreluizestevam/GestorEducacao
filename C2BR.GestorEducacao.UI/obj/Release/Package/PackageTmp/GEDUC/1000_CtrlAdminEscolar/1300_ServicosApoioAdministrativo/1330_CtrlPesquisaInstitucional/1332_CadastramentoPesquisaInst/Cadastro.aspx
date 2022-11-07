<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1332_CadastramentoPesquisaInst.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liMov
        {
            clear: both;
            margin-top: 10px;
        }
        .liClear { clear: both; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="ddlTipAva" class="lblObrigatorio" title="Grupo de Quest�es">
                Grupo de Quest�es
            </label>
            <asp:DropDownList ID="ddlTipAva" ToolTip="Selecione o Tipo da Avalia��o" runat="server" CssClass="campoNomePessoa">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"
                runat="server" ControlToValidate="ddlTipAva" ErrorMessage="Tipo Avalia��o deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liMov">
            <label for="txtItemAva" class="lblObrigatorio" title=" Nome da Quest�o">
                Nome da Quest�o
            </label>
            <asp:TextBox ID="txtItemAva" ToolTip="Informe o T�tulo da Avalia��o" MaxLength="60"  runat="server" CssClass="campoNomePessoa"
                onkeyup="javascript:MaxLength(this, 60);">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"
                runat="server" ControlToValidate="txtItemAva" ErrorMessage="T�tulo da Avalia��o deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
