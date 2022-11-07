<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1340_CtrlUploadDownloadArquivos.F1341_RegistroArquivoDownload.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 600px; }
        
        /*--> CSS LIs */
        #liUnidades { float: right; border-left: solid 2px #CCCCCC; padding-left:13px; }
        
        /*--> CSS DADOS */
        #liUnidades #lblUnidades { background-color: #DFDFDF; padding: 3px; }
        #liUnidades #lblUnidadesDescricao { background-color: #F5F5F5; padding: 3px; }
        #liUnidades #divUnidades { overflow-y: auto; height: 145px; border: solid 1px #CCC; width: 300px; margin-top: 5px; }
        #liUnidades #divUnidades table { border: 0 }
        .campoArquivoNome { width: 135px; }
        .campoDescricao{ width: 262px; height: 75px; margin-bottom: 5px; }
        .campoUploadArquivo { width: 220px; margin-bottom: 10px; }
        td label { display: inline; }
        #divBarraPadraoContent{display:none;}
        #divBarraDownlArqui { position:absolute; margin-left: 770px; margin-top:-50px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraDownlArqui ul { display: inline; float: left; margin-left: 10px; }
        #divBarraDownlArqui ul li { display: inline; margin-left: -2px; }
        #divBarraDownlArqui ul li img { width: 19px; height: 19px; }
        
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraDownlArqui { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<div id="div1" class="bar" > 
        <div id="divBarraDownlArqui" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
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
            <li id="btnEditar">
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
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnGravar_Click">
                    <img title="Grava o registro atual na base de dados."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar.png" />
                </asp:LinkButton>
            </li>
        </ul>
        <ul id="ulExcluirCancelar" style="width: 39px;">
            <li id="btnExcluir">
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:return confirm('Deseja realmente excluir este item?');">
                    <img title="Exclui o Registro atual selecionado."
                            alt="Icone de Excluir o Registro." 
                            src="/BarrasFerramentas/Icones/Excluir.png" />
                </asp:LinkButton>
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
            <li id="btnPesquisar">
                <asp:LinkButton ID="btnNewSearch" runat="server" CausesValidation="false" OnClick="btnNewSearch_Click">
                    <img title="Volta ao formulário de pesquisa."
                            alt="Icone de Pesquisa." 
                            src="/BarrasFerramentas/Icones/Pesquisar.png" />
                </asp:LinkButton>
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
        <li id="liUnidades">
            <label id="lblUnidades">Unidades:</label>
            <label id="lblUnidadesDescricao">Marque abaixo as Unidades que poderão fazer este download</label>
            <input type="checkbox" id="chkAtivaDesativaTodas" />Ativar/Desativar Todas
            <div id="divUnidades">
                <asp:CheckBoxList runat="server" ID="chkUnidades" ToolTip="Selecione as unidades em que o arquivo será compartilhado.">
                </asp:CheckBoxList>
            </div>
        </li>
        <li>
            <label for="fuArquivoPublicar" class="lblObrigatorio">Arquivo para Publicar</label>
            <asp:FileUpload runat="server" ID="fuArquivoPublicar" CssClass="campoUploadArquivo"></asp:FileUpload>
        </li>
        <li>
            <label for="txtArquivoNome" class="lblObrigatorio">Nome do Arquivo</label>
            <asp:TextBox runat="server" ID="txtArquivoNome" CssClass="campoArquivoNome" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtArquivoNome" runat="server" Display="None" ControlToValidate="txtArquivoNome" ErrorMessage="Nome do Arquivo é Requerido."></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="chkAtivo">Ativo</label>
            <asp:CheckBox runat="server" ID="chkAtivo" Checked="true"></asp:CheckBox>
        </li>
        <li>
            <label for="txtDescricao">Descrição</label>
            <asp:TextBox runat="server" ID="txtDescricao" CssClass="campoDescricao" MaxLength="1024" TextMode="MultiLine"></asp:TextBox>
        </li>
        <li>
            <label for="txtDataValidade" class="lblObrigatorio">Data de Cadastro</label>
            <asp:TextBox runat="server" ID="txtDataCadastro" CssClass="campoData" Enabled="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtDataCadastro" runat="server" Display="None" ControlToValidate="txtDataCadastro" ErrorMessage="Data de Cadastro é Requerido."></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDataValidade" class="lblObrigatorio">Data de Publicação</label>
            <asp:TextBox runat="server" ID="txtDataPublicacao" CssClass="campoData"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtDataPublicacao" runat="server" Display="None" ControlToValidate="txtDataPublicacao" ErrorMessage="Data de Publicação é Requerido."></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDataValidade" class="lblObrigatorio">Data de Validade</label>
            <asp:TextBox runat="server" ID="txtDataValidade" CssClass="campoData"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtDataValidade" runat="server" Display="None" ControlToValidate="txtDataValidade" ErrorMessage="Data de Validade é Requerido."></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        $("#chkAtivaDesativaTodas").click(function() {
            $("#divUnidades input[type=checkbox]").attr('checked', $(this).attr('checked'));
        });
    </script>
</asp:Content>
