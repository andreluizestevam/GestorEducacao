<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BarraCadastro.ascx.cs"
    Inherits="C2BR.GestorEducacao.UI.Library.Componentes.BarraCadastro" %>
    <style type="text/css">
        #divBarraPadraoContent { border: 1px solid #CCC; overflow: auto; width: 250px !important; padding: 3px 0; }
        #divBarraPadraoContent ul { display: inline; float: left; margin-left: 10px; }
        #divBarraPadraoContent ul li { display: inline; margin-left: -2px; }
        #divBarraPadraoContent ul li img { width: 19px; height: 19px; }
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraPadraoContent { width: 238px; }
</style>
<![endif]-->
<div id="divActionBar" class="bar" >
        <div id="divBarraPadraoContent" class="boxRoundCorner">
            <ul id="ulNavegacao" style="width: 43px;">
                <li id="btnVoltarPainel">
                    <a href="javascript:parent.showHomePage()">
                        <img title="Clique para voltar ao Painel Inicial." 
                             alt="Icone Voltar ao Painel Inicial." 
                             src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                    </a>
                </li>
                <li id="btnVoltar">
                    <a href="javascript:BackToHome();">
                        <img title="Clique para voltar a Pagina Anterior."
                                alt="Icone Voltar a Pagina Anterior." 
                                src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                    </a>
                </li>
                <%-- <li id="btnProximo">
                    <img title="Avanca a proxima etapa." alt="Icone Proximo" src="/BarrasFerramentas/Icones/AvancarProximo_Desabilitado.png" />
                </li> --%> 
            </ul>
            <ul id="ulEditarNovo" style="width: 43px;">
                <li id="btnEditar" style="float:left;">
                    <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar." src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
                </li>
                <li id="btnNovo">
                    <a href='<%= String.Format("{0}?op=insert&moduloNome={1}", Request.Url.AbsolutePath, HttpContext.Current.Server.UrlDecode(Request.QueryString["moduloNome"].ToString())) %>'>
                        <img title="Abre o formulario para Criar um Novo Registro."
                             alt="Icone de Criar Novo Registro." 
                             src="/BarrasFerramentas/Icones/NovoRegistro.png" />
                    </a>
                </li>
            </ul>
            <ul id="ulGravar">
                <li>
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">
                        <img title="Grava o registro atual na base de dados."
                             alt="Icone de Gravar o Registro." 
                             src="/BarrasFerramentas/Icones/Gravar.png" />
                    </asp:LinkButton>
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 43px;">
                <li id="btnExcluir">
                    <asp:LinkButton ID="btnDelete" CssClass="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:return confirm('Deseja realmente excluir este item?');">
                        <img title="Exclui o Registro atual selecionado."
                                alt="Icone de Excluir o Registro." 
                                src="/BarrasFerramentas/Icones/Excluir.png" />
                    </asp:LinkButton>
                </li>
                <li id="btnCancelar">
                    <a href='<%= Request.Url.AbsoluteUri %>'>
                        <img title="Cancela TODAS as alterações feitas no registro."
                                alt="Icone de Cancelar Operacao Atual." 
                                src="/BarrasFerramentas/Icones/Cancelar.png" />
                    </a>
                </li>
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
                    <img title="Exibe o registro atual em modo de impressao." alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                </li>
            </ul>
            <%-- <ul id="ulAjuda">
                <li>
                    <img title="Exibe uma auto-ajuda referente a etapa atual." alt="Icone Posso Ajudar." src="/BarrasFerramentas/Icones/PossoAjudar_Desabilitado.png" />
                </li>
            </ul>--%>
        </div>

    <div id="divActionBarValidationSummary">
        <asp:Label runat="server" ID="lblMensagemErro" CssClass="errorMessage" Text=""></asp:Label>
        <asp:Label runat="server" ID="lblMensagemSucesso" CssClass="successMessage" Text=""></asp:Label>
    </div>
</div>
<script type="text/javascript">
    function BackToHome() {
        $('#divContent', parent.document.body).show();
        $('#divLoadTelaFuncionalidade', parent.document.body).hide();
        $('#divLoginInfo', parent.document.body).show();
    }

    $(".btnDelete").click(function () {
        var temp = window.location + "";
        temp = temp.replace("op=edit", "op=delete");     
        window.location.search = temp;
    });
</script>