<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BarraBusca.ascx.cs"
    Inherits="C2BR.GestorEducacao.UI.Library.Componentes.BarraBusca" %>
<style type="text/css">
        /*#divBarraPadraoContent { border: 1px solid #CCC; overflow: auto; width: 223px; padding: 3px 0; }*/
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
                <li id="btnEditar">
                    <a title="Abre o registro atualmente selecionado em modo de edicao" href="javascript:__doPostBack('ctl00$grdBusca','Edit$0')">
                        <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar." src="/BarrasFerramentas/Icones/EditarRegistro.png" />
                    </a>
                </li>
                <li id="btnNovo">
                    <a href='<%= String.Format("{0}?op=insert&moduloNome={1}", Request.Url.AbsolutePath.ToLower().Replace("busca.aspx","cadastro.aspx"), HttpContext.Current.Server.UrlDecode(Request.QueryString["moduloNome"].ToString())) %>'>
                        <img title="Abre o formulario para Criar um Novo Registro."
                             alt="Icone de Criar Novo Registro." 
                             src="/BarrasFerramentas/Icones/NovoRegistro.png" />
                    </a>
                </li>
            </ul>
            <ul id="ulGravar">
                <li>
                    <img title="Grava o registro atual na base de dados."
                         alt="Icone de Gravar o Registro." 
                         src="/BarrasFerramentas/Icones/Gravar_Desabilitado.png" />
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 43px;">
                <li id="btnExcluir">
                        <img title="Exclui o Registro atual selecionado."
                             alt="Icone de Excluir o Registro." 
                             src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                </li>
                <li id="btnCancelar">
                    <a href='<%= Request.Url.AbsoluteUri %>'>
                        <img title="Cancela a Pesquisa Atual, limpa a Grade de Resultados e o Formulário de Parâmetros de Pesquisa."
                                alt="Icone de Cancelar Operacao Atual." 
                                src="/BarrasFerramentas/Icones/Cancelar.png" />
                    </a>
                </li>
            </ul>
            <ul id="ulAcoes" style="width: 43px;">
                <li  id="btnPesquisar">
                    <a href="javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions('ctl00$btnSearch', '', true, '', '', false, true))">
                        <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                                alt="Icone de Pesquisa." 
                                src="/BarrasFerramentas/Icones/Pesquisar.png" />
                    </a>
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
    function BackToHome() 
    {
        $('#divContent', parent.document.body).show();
        $('#divLoadTelaFuncionalidade', parent.document.body).hide();
        $('#divLoginInfo', parent.document.body).show();
    }
</script>