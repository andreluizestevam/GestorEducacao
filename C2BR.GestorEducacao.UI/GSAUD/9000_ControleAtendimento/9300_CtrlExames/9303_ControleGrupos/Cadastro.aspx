<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9303_ControleGrupos.Cadastro" %>

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
        <li style="float: right;">
            <label for="txtProced" class="lblObrigatorio" title="Procedimento">
                 Procedimento
            </label>
            <asp:DropDownList ID="ddlProced" runat="server" ToolTip="Selecione o Procedimento do grupo" style="width: 225px;">
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtOper" class="lblObrigatorio" title="Procedimento">
                 Contratação
            </label>
            <asp:DropDownList ID="ddlOper" runat="server" ToolTip="Selecione a Operadora" OnSelectedIndexChanged="ddlOper_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label title="Código do Grupo">
               Código
            </label>
            <asp:TextBox ID="txtCodGrupo" runat="server" MaxLength="10" Width="70px" ToolTip="Informe o Código do Grupo" />
        </li>
        <li style="margin-top:5px;">
            <label for="txtTipAva" class="lblObrigatorio" title="Grupo">
                 Grupo
            </label>
            <asp:TextBox ID="txtNomeGrupo" runat="server" CssClass="campoNomePessoa"
                MaxLength="30" ToolTip="Informe o Tipo de Avaliação" Width="234px">
            </asp:TextBox>
            <asp:RequiredFieldValidator CssClass="validatorField" runat="server" Display="Dynamic"
                ControlToValidate="txtNomeGrupo" ErrorMessage="Nome do grupo deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li style="clear:both;">
            <label title="Ordem de Apresentação">
               Ordem
            </label>
            <asp:TextBox ID="txtNumOrdem" runat="server" MaxLength="3" Width="30px" Height="13px" CssClass="Numero" ToolTip="Informe a Ordem de Apresentação" />
        </li>
        <li style="margin-left:5px;">
            <label for="ddlStatus" title="Status" class="lblObrigatorio">
                Status
            </label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione o Status">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I" ></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear:both;">
            <label for="txtTipAva" title="Grupo">
                 Método
            </label>
            <asp:TextBox ID="txtMetodoProced" runat="server" CssClass="campoNomePessoa"
                MaxLength="200" ToolTip="Informe o método utilizado para a realização do procedimento" TextMode="MultiLine" 
                width="311px" Height="35px" onkeyDown="checkTextAreaMaxLength(this,event,'200');">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtTipAva" title="Grupo">
                 Material
            </label>
            <asp:TextBox ID="txtMaterialProced" runat="server" CssClass="campoNomePessoa"
                ToolTip="Informe o material utilizado no procedimento" TextMode="MultiLine" 
                width="311px" Height="35px" onkeyDown="checkTextAreaMaxLength(this,event,'150');">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtObjetivo" title="Objetivo">Objetivo</label>
            <asp:TextBox ID="txtObjetivo" runat="server" TextMode="MultiLine" 
                Width="311px" Height="80px" onkeyDown="checkTextAreaMaxLength(this,event,'500');"
                ToolTip="Informe o Objetivo">
            </asp:TextBox>
        </li>
        <li style="clear:both;">
            <label for="txtObs" title="Observação">Observações</label>
            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Width="311px" Height="53px" ToolTip="Informe a Observação">
            </asp:TextBox>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Numero").mask("?999");
        });
    </script>
</asp:Content>