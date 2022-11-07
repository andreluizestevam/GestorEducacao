<%@ Page Title="Cadastro" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1910_CadastroGrupoCBO.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 818px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        .span1
        {
            margin-left: 380px;
        }
        
        /*--> CSS LIs */
        .liDescricao, .liSituacao
        {
            clear: both;
        }
        .ulDados li
        {
            margin-right: 5px;
            margin-bottom: 5px;
        }
        .liClear
        {
            clear: both;
        }
        .liEspaco
        {
            margin-left: 15px;
        }
        
        /*-- CSS DADOS */
        
        .ddlSituacao
        {
            width: auto;
        }
        
        .liDescricao
        {
            margin-top: 30px;
        }
        
        #divBarraPadraoContent
        {
            display: none;
        }
        
        #divBarraLanctoExameFinal
        {
            position: absolute;
            margin-left: 773px;
            margin-top: -50px;
            border: 1px solid #CCC;
            overflow: auto;
            width: 250px !important;
            padding: 3px 0;
        }
        #divBarraLanctoExameFinal ul
        {
            display: inline;
            float: left;
            margin-left: 10px;
        }
        #divBarraLanctoExameFinal ul li
        {
            display: inline;
            margin-left: -2px;
        }
        #divBarraLanctoExameFinal ul li img
        {
            width: 19px;
            height: 19px;
        }
    </style>
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar" style="margin-left: -30px;">
        <div id="divBarraLanctoExameFinal" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <ul id="ulNavegacao" style="width: 43px;">
                <li id="btnVoltarPainel"><a href="javascript:parent.showHomePage()">
                    <img title="Clique para voltar ao Painel Inicial." alt="Icone Voltar ao Painel Inicial."
                        src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                </a></li>
                <li id="btnVoltar"><a href="javascript:BackToHome();">
                    <img title="Clique para voltar a Pagina Anterior." alt="Icone Voltar a Pagina Anterior."
                        src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                </a></li>
            </ul>
            <ul id="ulEditarNovo" style="width: 43px;">
                <li id="btnEditar" style="float: left;">
                    <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar."
                        src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
                </li>
                <li id="btnNovo"><a href='<%= String.Format("{0}?op=insert&moduloNome={1}", Request.Url.AbsolutePath, HttpContext.Current.Server.UrlDecode(Request.QueryString["moduloNome"].ToString())) %>'>
                    <img title="Abre o formulario para Criar um Novo Registro." alt="Icone de Criar Novo Registro."
                        src="/BarrasFerramentas/Icones/NovoRegistro.png" />
                </a></li>
            </ul>
            <ul id="ulGravar">
                <li>
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_OnClick">
                        <img title="Grava o registro atual na base de dados."
                             alt="Icone de Gravar o Registro." 
                             src="/BarrasFerramentas/Icones/Gravar.png" />
                    </asp:LinkButton>
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 43px;">
                <li id="btnExcluir">
                    <asp:LinkButton ID="btnDelete" CssClass="btnDelete" runat="server" OnClick="btnDelete_OnClick"
                        OnClientClick="javascript:return confirm('Deseja realmente excluir este item?');">
                        <img title="Exclui o Registro atual selecionado."
                                alt="Icone de Excluir o Registro." 
                                src="/BarrasFerramentas/Icones/Excluir.png" />
                    </asp:LinkButton>
                </li>
                <li id="btnCancelar"><a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela TODAS as alterações feitas no registro." alt="Icone de Cancelar Operacao Atual."
                        src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a></li>
            </ul>
            <ul id="ulAcoes" style="width: 43px;">
                <li id="btnPesquisar">
                    <asp:LinkButton ID="btnNewSearch" runat="server" CausesValidation="false" OnClick="btnNewSearch_Click">
                        <img title="Volta ao formulário de pesquisa."
                                alt="Icone de Pesquisa." 
                                src="/BarrasFerramentas/Icones/Pesquisar.png" />
                    </asp:LinkButton>
                </li>
                <li id="btnImprimir">
                    <img title="Exibe o registro atual em modo de impressao." alt="Icone de Impressao."
                        src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                </li>
            </ul>
        </div>
    </div>
    <ul id="ulDados" class="ulDados">
        <li id="liDescricao" runat="server" class="liDescricao" style="margin-left: 250px;">
            <label class="lblObrigatorio" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Digite a descrição do grupo CBO"
                CssClass="txtDescricao" Width="335" TextMode="MultiLine" Rows="3" onkeyDown="checkTextAreaMaxLength(this,event,'150');"
                runat="server">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <%--<li id="liCodigo" runat="server" class="liCodigo" style="margin-left: 250px;">
            <label class="lblObrigatorio" title="Código">
                Código</label>
            <asp:TextBox ID="txtCodigo" ToolTip="Digite o código do grupo CBO"
                CssClass="txtCodigo" MaxLength="3" Width="75" runat="server">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCodigo" runat="server" ControlToValidate="txtCodigo"
                ErrorMessage="Descrição deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>--%>
        <li id="liSituacao" runat="server" class="liSituacao" style="margin-left: 250px;">
            <label class="lblObrigatorio" title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Escolha uma situação" CssClass="ddlSituacao"
                runat="server">
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacao"
                ErrorMessage="Situação deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
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
