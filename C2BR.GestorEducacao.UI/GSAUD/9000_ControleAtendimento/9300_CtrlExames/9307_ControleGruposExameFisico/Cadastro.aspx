<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9307_ControleGruposExameFisico.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 318px; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liClear {  clear: both; margin-top:5px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlEspec" class="lblObrigatorio" title="Especialidade">
                 Especialidade
            </label>
            <asp:DropDownList ID="ddlEspec" runat="server" ToolTip="Selecione a especialidade do grupo">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtNomeGrupo" class="lblObrigatorio" title="Nome do grupo">
                 Nome
            </label>
            <asp:TextBox ID="txtNomeGrupo" runat="server" CssClass="campoNomePessoa"
                MaxLength="50" ToolTip="Informe o nome do grupo" Width="311px">
            </asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validatorField" runat="server" Display="Dynamic"
                ControlToValidate="txtNomeGrupo" ErrorMessage="Nome do grupo deve ser informado">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liClear">
            <label for="txtDesc" title="Descrição do grupo">
                 Descrição
            </label>
            <asp:TextBox ID="txtDesc" runat="server" CssClass="campoNomePessoa"
                MaxLength="200" ToolTip="Informe uma descrição para o grupo" TextMode="MultiLine" 
                width="311px" Height="35px" onkeyDown="checkTextAreaMaxLength(this,event,'200');">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label for="ddlSitu" title="Situação do grupo" class="lblObrigatorio">
                Situação
            </label>
            <asp:DropDownList ID="ddlSitu" runat="server" ToolTip="Selecione a situação inicial do grupo">
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