<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3800_CtrlEncerramentoLetivo.F3810_CtrlRegioesEnsino.F3813_CadastramentoTpAvalDesempRegEnsino.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 600px; }        
        
        /*--> CSS LIs */
        .liClear
        {
            margin-top: 5px !important;
            clear: both;
        }
        .liClear2 { clear: both; }
        .liDescricao { margin-top: 5px; }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liDescricao">
            <label for="txtNAvaliacao" class="lblObrigatorio" title="Nome da Avaliação">
                Nome Avaliação</label>
            <asp:TextBox ID="txtNAvaliacao" ToolTip="Informe o Nome da Avaliação" runat="server" CssClass="txtNomeAvaliacao" MaxLength="70"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtNAvaliacao"
                ErrorMessage="Nome Avaliação deve ter no máximo 70 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"
                Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtNAvaliacao"
                CssClass="validatorField" ErrorMessage="Nome Avaliação deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDescricao">
            <label for="txtSigla" class="lblObrigatorio labelPixel" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla" runat="server" CssClass="txtSigla" MaxLength="10"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ter no máximo 10 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla"
                CssClass="validatorField" ErrorMessage="Sigla deve ser informada" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear2">
            <label for="txtAsp1" class="labelPixel" title="Aspecto 1">
                Aspecto 1</label>
            <asp:TextBox ID="txtAsp1" ToolTip="Informe o Aspecto 1" runat="server" CssClass="txtAvaliacao" onkeyup="javascript:MaxLength(this, 100);"
                TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtAsp2" class="labelPixel" title="Aspecto 2">
                Aspecto 2</label>
            <asp:TextBox ID="txtAsp2" ToolTip="Informe o Aspecto 2" runat="server" CssClass="txtAvaliacao" onkeyup="javascript:MaxLength(this, 100);"
                TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtAsp3" class="labelPixel" title="Aspecto 3">
                Aspecto 3</label>
            <asp:TextBox ID="txtAsp3" ToolTip="Informe o Aspecto 3" runat="server" CssClass="txtAvaliacao" onkeyup="javascript:MaxLength(this, 100);"
                TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtAsp4" class="labelPixel" title="Aspecto 4">
                Aspecto 4</label>
            <asp:TextBox ID="txtAsp4" runat="server" ToolTip="Informe o Aspecto 4" CssClass="txtAvaliacao" onkeyup="javascript:MaxLength(this, 100);"
                TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtAsp5" class="labelPixel"  title="Aspecto 5">
                Aspecto 5</label>
            <asp:TextBox ID="txtAsp5" runat="server" ToolTip="Informe o Aspecto 5" CssClass="txtAvaliacao" onkeyup="javascript:MaxLength(this, 100);"
                TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtAsp6" class="labelPixel" title="Aspecto 6">
                Aspecto 6</label>
            <asp:TextBox ID="txtAsp6" runat="server" ToolTip="Informe o Aspecto 6" CssClass="txtAvaliacao" onkeyup="javascript:MaxLength(this, 100);"
                TextMode="MultiLine"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
