<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9308_ControleSubGruposExameFisico.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label class="lblObrigatorio">
                Grupo</label>
            <asp:DropDownList ID="ddlGrupo" runat="server" Style="width: 125px;" ToolTip="Selecione o Grupo" />
            <asp:RequiredFieldValidator ID="rfvGrupo" CssClass="validatorField" runat="server"
                ControlToValidate="ddlGrupo" ErrorMessage="Grupo deve ser informado" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label class="lblObrigatorio">
                Nome do Subgrupo</label>
            <asp:TextBox ID="txtNomeSubgrupo" ToolTip="Informe o Título da Avaliação" MaxLength="60"
                runat="server" Width="311px" Style=" margin-bottom: 0px;" />
            <asp:RequiredFieldValidator ID="rfvNomeSubgrupo" CssClass="validatorField" runat="server"
                ControlToValidate="txtNomeSubgrupo" ErrorMessage="Subgrupo deve ser informado"
                Display="Dynamic"></asp:RequiredFieldValidator>
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
            <label>
                Situação</label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione o Status">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Numero").mask("?999");
        });
    </script>
</asp:Content>
