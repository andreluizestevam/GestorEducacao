<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoGenericas.Master"
    AutoEventWireup="true" CodeBehind="GerarCopiaDeSeguranca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0501_CopiaSeguranca.GerarCopiaDeSeguranca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }
        
        /*--> CSS DADOS */
        .txtNomeCS {  width: 220px; }
        .campoDescricaoCS 
        {
            width: 250px;
            height: 100px;
        }
        #divBarraGerarCopiaSegurCS { position:absolute; margin-left: 750px; margin-top:-35px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraGerarCopiaSegurCS ul { display: inline; float: left; margin-left: 10px; }
        #divBarraGerarCopiaSegurCS ul li { display: inline; margin-left: -2px; }
        #divBarraGerarCopiaSegurCS ul li img { width: 19px; height: 19px; }
        #div1 { width: 100px; }
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraGerarCopiaSegurCS { width: 238px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar"> 
            <div id="divBarraGerarCopiaSegurCS" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <ul id="ulNavegacao" style="width: 39px;">
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
            <ul id="ulEditarNovo" style="width: 39px;">
                <li id="btnEditar" style="float:left;">
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
            <ul id="ulExcluirCancelar" style="width: 39px;">
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
            <ul id="ulAcoes" style="width: 39px;">
                <li  id="btnPesquisar">
                    <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                            alt="Icone de Pesquisa." 
                            src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                </li>
                <li id="liImprimir">
                    <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão." 
                            alt="Icone de Impressao." 
                            src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />                    
                </li>
            </ul>
        </div>
    </div>
    <ul class="ulDados">
        <li>
            <label for="txtNomeCS" title="Nome" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox ID="txtNomeCS"  runat="server" Enabled="false" CssClass="txtNomeCS" MaxLength="20"
                ToolTip="O nome da Cópia de Segurança é sistemáticamente gerenciado."></asp:TextBox>
        </li>
        <li>
            <label for="txtDescricaoCS" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtDescricaoCS" ToolTip="Informe a Descrição" runat="server" CssClass="campoDescricaoCS" TextMode="MultiLine"></asp:TextBox>
        </li>
    </ul>
    <div id="divButtonsBarCS" class="corner" style="text-align:center;">
        <ul id="ulButtons">
            <li id="liBtnActionCS">
                <asp:LinkButton ID="btnActionCS" runat="server" class="btnActionCS" ToolTip="Gerar Cópia de Segurança"
                    OnClick="btnActionCS_Click">
                    <img id="imgIconSaveCS" src='/Library/IMG/Gestor_DBBackup.png' alt="Gerar Cópia de Segurança" />
                    <asp:Label runat="server" ID="lblBtnActionCS" CssClass="lblBtnActionCS" Text="Gerar Cópia de Segurança"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>

    <script type="text/javascript">
        $('.btnActionCS').click(function () {
            $('#imgIconSaveCS').hide();
            $('#liBtnActionCS').text("Aguarde... A Cópia de segurança está sendo gerada.");
            $('#divButtonsBarCS').css('text-align', 'center');
            $('#divButtonsBarCS').css('padding', '5px');
        });
    </script>

</asp:Content>
