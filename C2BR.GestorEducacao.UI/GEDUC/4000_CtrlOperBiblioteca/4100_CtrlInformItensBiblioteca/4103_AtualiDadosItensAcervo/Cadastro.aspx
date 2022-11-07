<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4103_AtualiDadosItensAcervo.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 490px; }        
        .ulDados input{ margin-bottom: 0; }
        
        /*--> CSS LIs */
        .ulDados li { margin-bottom: 10px; margin-right: 10px; }
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .txtIsbn { width: 95px; }
        .txtNomeObra { width: 300px; }
        .ddlAreaConhecimento, .ddlClassificacao, .ddlEditora { width: 200px;}
        .txtSinopse { width: 468px; height: 44px; }
        .txtNumeroPaginas { width: 30px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlAreaConhecimento" class="lblObrigatorio" title="Área do Conhecimento">Área do Conhecimento</label>
            <asp:DropDownList ID="ddlAreaConhecimento" AutoPostBack="true" runat="server" 
                CssClass="ddlAreaConhecimento" ToolTip="Informe a Área do Conhecimento" 
                onselectedindexchanged="ddlAreaConhecimento_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAreaConhecimento" runat="server" ControlToValidate="ddlAreaConhecimento" 
                ErrorMessage="Área do Conhecimento é requerida" Display="Static" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlClassificacao" class="lblObrigatorio" title="Classificação da Obra">Classificação</label>
            <asp:DropDownList ID="ddlClassificacao" runat="server" CssClass="ddlClassificacao" ToolTip="Informe a Classificação"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClassificacao" 
                ErrorMessage="Classificação é requerida" Display="Static" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlEditora" class="lblObrigatorio" title="Editora">Editora</label>
            <asp:DropDownList ID="ddlEditora" runat="server" CssClass="ddlEditora" ToolTip="Informe a Editora"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEditora" 
                ErrorMessage="Editora é requerida" Display="Static" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlAutor" class="lblObrigatorio" title="Autor da Obra">Autor</label>
            <asp:DropDownList ID="ddlAutor" runat="server" CssClass="campoNomePessoa" ToolTip="Informe o Autor da Obra"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAutor" 
                ErrorMessage="Autor é requerido" Display="Static" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtNomeObra" class="lblObrigatorio" title="Nome da Obra">Nome da Obra</label>
            <asp:TextBox ID="txtNomeObra" MaxLength="50" runat="server" CssClass="txtNomeObra" ToolTip="Informe o Nome da Obra"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNomeObra" 
                ErrorMessage="Nome da Obra é requerido" Display="Static" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtIsbn" class="lblObrigatorio" title="ISBN - Número Universal Padrão do Livro">ISBN</label>
            <asp:TextBox ID="txtIsbn" runat="server" CssClass="txtIsbn campoNumerico" MaxLength="13" ToolTip="Informe o ISBN da Obra"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIsbn" 
                ErrorMessage="ISBN é requerido" Display="Static" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtNumeroPaginas" title="Número de Páginas da Obra">N° Pag</label>
            <asp:TextBox ID="txtNumeroPaginas" runat="server" CssClass="txtNumeroPaginas campoNumerico" ToolTip="Informe o Número de Páginas da Obra"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtSinopse" title="Sinopse da Obra">Sinopse</label>
            <asp:TextBox ID="txtSinopse" runat="server" CssClass="txtSinopse" TextMode="MultiLine" ToolTip="Informe a Sinopse da Obra" onkeyup="javascript:MaxLength(this, 250);"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação">Situação</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="ddlSituacao" ToolTip="Informe a Situação">
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtDataSituacao" title="Data da Situação">Data Situação</label>
            <asp:TextBox ID="txtDataSituacao" runat="server" CssClass="campoData" Enabled="false" ToolTip="Informe a Data da Situação"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDataSituacao" 
                ErrorMessage="Data da Situação é requerida" Display="Static" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function() {
            $(".txtNumeroPaginas").mask("?99999");
            $(".txtIsbn").mask("999-99-9999-999-9");
        });
</script>
</asp:Content>