<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0905_CadastroInternacionalDoenca.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 160px;
            margin-top: 40px !important;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        
        /*--> CSS DADOS */
        .labelPixel
        {
            margin-bottom: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtCID" class="lblObrigatorio labelPixel" title="Código Internacional de Doença">
                CID</label>
            <asp:TextBox ID="txtCID" ToolTip="Informe o Código Internacional de Doença" runat="server"
                CssClass="txtSigla" MaxLength="12"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCID"
                ErrorMessage="CID deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDescricaoCID" class="lblObrigatorio labelPixel" title="Descrição da Doença relacionada ao CID">
                Descrição</label>
            <asp:TextBox ID="txtDescricaoCID" ToolTip="Informe a Descrição da Doença relacionada ao CID"
                CssClass="txtDescricaoCID" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricaoCID"
                ErrorMessage="Descrição deve ter no máximo 30 caracteres" ValidationExpression="^(.|\s){1,60}$"
                CssClass="validatorField">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricaoCID"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <%--<li class="liClear">
            <label>
                Sigla</label>
            <asp:TextBox runat="server" ID="txtSigla"></asp:TextBox>
        </li>--%>
        <li class="liClear">
            <label>
                CID Geral</label>
            <asp:DropDownList runat="server" ID="ddlCIDGeral" Width="170px">
            </asp:DropDownList>
        </li>
        <li class="liClear" style="margin-top: 10px;">
            <label>
                Situação</label>
            <asp:DropDownList runat="Server" ID="ddlSituacao">
                <asp:ListItem Value="A" Text="Ativo" Selected="true"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
