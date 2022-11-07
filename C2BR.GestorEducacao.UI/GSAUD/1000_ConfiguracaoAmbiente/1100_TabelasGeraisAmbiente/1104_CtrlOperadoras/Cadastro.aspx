<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1104_CtrlOperadoras.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 270px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
        input
        {
            height: 13px;
        }
        .chk label
        {
            display: inline;
        }
        .ddlSitu
        {
            width: auto;
        }
        .txtCodOper
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidSituacao" />
    <asp:HiddenField runat="server" ID="hidId" />
    <asp:HiddenField runat="server" ID="hidImg" />
    <ul class="ulDados">
        <li style="clear: both; margin-bottom: -8px;">
            <fieldset style="width: 250px; height: 100px;">
                <img id="imgLogo" alt="" style="width: 250px; height: 100px;" runat="server" src="~/Library/IMG/Gestor_SemImagem.png" />
            </fieldset>
        </li>
        <li>
            <asp:LinkButton Text="Nova imagem" Style="clear: both;" ID="lnkNovaImg" runat="server">
                <asp:FileUpload ID="fpNovaImg" runat="server" Width="1px" Style="opacity: 0; margin-left: -60px;"
                    size="1" OnDataBinding="fpNovaImg_OnDataBinding" />
            </asp:LinkButton>
        </li>
        <li style="margin-top: 9px; margin-left: 50px;">
            <asp:LinkButton Text="Preview" Style="clear: both;" ID="lnkPreviewImg" runat="server"
                OnClick="lnkPreviewImg_OnClick">
            </asp:LinkButton>
        </li>
        <li style="margin-top: 9px; margin-right: 14px; float: right;">
            <asp:LinkButton Text="Limpar imagem" ID="lnkLimparImg" runat="server" OnClick="lnkLimparImg_OnClick">
            </asp:LinkButton>
        </li>
        <li>
            <label title="Nome da Operadora" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomeOper" TextMode="MultiLine" Rows="2" Width="250px"
                ToolTip="Nome da Operadora" MaxLength="60" onkeydown="checkTextAreaMaxLength(this, event, 60)"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfv1" ControlToValidate="txtNomeOper"
                Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Código da Operadora" class="lblObrigatorio">
                Código</label>
            <asp:TextBox runat="server" ID="txtCodOper" class="txtCodOper" Width="45px" ToolTip="Código da Operadora"
                MaxLength="6"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtCodOper"
                Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Sigla" class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox runat="server" ID="txtSigla" Width="45px" ToolTip="Sigla" MaxLength="6"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtSigla"
                Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="CNPJ da Operadora" class="lblObrigatorio">
                CNPJ</label>
            <asp:TextBox runat="server" ID="txtCNPJ" CssClass="txtCNPJ" Width="95px" ToolTip="CNPJ da Operadora"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtCNPJ"
                Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Situação da Operadora">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSitu" class="ddlSitu" ToolTip="Situação da Operadora">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <asp:CheckBox runat="server" ID="chkOperaInst" Text="Operadora é a Instituição" ToolTip="Marque caso a Operadora seja a instituição"
                CssClass="chk" Style="margin-left: -5px !important;" />
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtCNPJ").mask("99.999.999/9999-99");
        });

        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
    </script>
</asp:Content>
