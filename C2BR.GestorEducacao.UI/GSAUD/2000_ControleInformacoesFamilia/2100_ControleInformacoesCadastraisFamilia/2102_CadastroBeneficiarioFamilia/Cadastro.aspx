<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
Inherits="C2BR.GestorEducacao.UI.GSAUD._2000_ControleInformacoesFamilia._2100_ControleInformacoesCadastraisFamilia._2102_CadastroBeneficiarioFamilia.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 800px;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        /*--> CSS DADOS */
        .divGrid
        {
            height: 267px;
            width: 800px;
            overflow-y: scroll;
            margin-top: 10px;
        }
         .txtObs
        {
            width: 285px;
            height: 48px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtCodigo" title="Código" class="lblObrigatorio">
                Código</label>
             <asp:TextBox ID="txtCodigo" runat="server" ToolTip="Informe o Rendimento Extra" MaxLength="15" Enabled="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCodigo" runat="server" ControlToValidate="txtCodigo"
                CssClass="validatorField" ErrorMessage="O Codigo deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlFamilia" title="Nome da Família" class="lblObrigatorio">
                Nome da Família</label>
            <asp:DropDownList ID="ddlFamilia" ToolTip="Selecione o Nome da Família" CssClass="campoNomePessoa" OnSelectedIndexChanged="ddlFamilia_SelectedIndexChanged"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvFamilia" runat="server" ControlToValidate="ddlFamilia"
                CssClass="validatorField" ErrorMessage="O Nome da Família deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlBeneficiario" title="Beneficiário" class="lblObrigatorio">
                Beneficiário</label>
            <asp:DropDownList ID="ddlBeneficiario" ToolTip="Selecione o Beneficiário" CssClass="campoNomePessoa"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvBeneficiario" runat="server" ControlToValidate="ddlBeneficiario"
                CssClass="validatorField" ErrorMessage="O Beneficiário deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlGrau" title="Grau de Parentesco" class="lblObrigatorio">
                Grau de Parentesco</label>
            <asp:DropDownList ID="ddlGrau" ToolTip="Selecione o Grau de Parentesco" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvGrau" runat="server" ControlToValidate="ddlGrau"
                CssClass="validatorField" ErrorMessage="O Grau de Parentesco deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>     
        <li style="margin-left: -130px;margin-top:10px;">
            <label for="txtObs" title="Observação">
                Observação</label>
            <asp:TextBox ID="txtObs" runat="server" CssClass="txtObs"
               TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 200);"
                ToolTip="Informe alguma observação"></asp:TextBox>
        </li>
        <li id="liSituacao" runat="server" visible="false" style="margin-top:10px;margin-left:100px;">
            <label for="ddlSituacao" title="Situação" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação" runat="server">
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
                <asp:ListItem Value="V">Validação</asp:ListItem>
                <asp:ListItem Value="S">Suspenso</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <asp:HiddenField ID="hdnIdHistoSalar" runat="server" />
    <script type="text/javascript">

    </script>
</asp:Content>
