<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9309_ControleItensExameFisico.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 260px; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liClear { clear: both; margin-top:10px; }
        .liTitulo { margin-left: 20px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label class="lblObrigatorio">Grupo</label>
            <asp:DropDownList ID="ddlGrupo" runat="server" style="width: 180px;" ToolTip="Selecione o Grupo" OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator ID="rfvGrupo" CssClass="validatorField"
                runat="server" ControlToValidate="ddlGrupo" ErrorMessage="Grupo deve ser informado.">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">Subgrupo</label>
            <asp:DropDownList ID="ddlSubgrupo" runat="server" style="width: 180px;" ToolTip="Selecione o Subgrupo" />
            <asp:RequiredFieldValidator ID="rfvSubgrupo" CssClass="validatorField" runat="server" 
                ControlToValidate="ddlSubgrupo" ErrorMessage="Subgrupo deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">Item de Exame Fisico</label>
            <asp:TextBox ID="txtItem" runat="server" style=" margin-bottom:0px;" Width="311px" ToolTip="Informe o nome do Item de Avaliação" />
            <asp:RequiredFieldValidator ID="rfvTxtItem" CssClass="validatorField"
                runat="server" ControlToValidate="txtItem" ErrorMessage="O item de avaliação deve ser informado."></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDesc" title="Descrição do grupo">
                Descrição
            </label>
            <asp:TextBox ID="txtDesc" runat="server" CssClass="campoNomePessoa" MaxLength="200"
                ToolTip="Informe uma descrição para o grupo" TextMode="MultiLine" Width="311px"
                Height="35px" onkeyDown="checkTextAreaMaxLength(this,event,'200');">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label>Situação</label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione a Situação">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I" ></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Numero").mask("?999");
        });
    </script>
</asp:Content>
