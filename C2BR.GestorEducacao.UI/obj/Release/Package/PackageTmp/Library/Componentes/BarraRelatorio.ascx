<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BarraRelatorio.ascx.cs" Inherits="C2BR.GestorEducacao.UI.Library.Componentes.BarraRelatorio" %>
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
<div id="div1" class="bar" >
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
            </ul>
            <ul id="ulEditarNovo" style="width: 43px;">
                <li id="btnEditar">
                    <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar." src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
                </li>
                <li id="btnNovo">
                    <img title="Abre o formulario para Criar um Novo Registro."
                            alt="Icone de Criar Novo Registro." 
                            src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
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
                        <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                                alt="Icone de Cancelar Operacao Atual." 
                                src="/BarrasFerramentas/Icones/Cancelar.png" />
                    </a>
                </li>
            </ul>
            <ul id="ulAcoes" style="width: 43px;">
                <li  id="btnPesquisar">
                    <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                            alt="Icone de Pesquisa." 
                            src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                </li>
                <li id="liImprimir">
                    <asp:LinkButton ID="btnImprimir" runat="server" OnClick="btnImprimir_Click" ToolTip="Imprimir">
                        <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão." alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir.png" />
                    </asp:LinkButton>
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
</script>
