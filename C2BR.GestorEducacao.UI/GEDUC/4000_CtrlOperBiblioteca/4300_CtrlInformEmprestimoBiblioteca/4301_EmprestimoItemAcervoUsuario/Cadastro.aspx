<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4300_CtrlInformEmprestimoBiblioteca.F4301_EmprestimoItemAcervoUsuario.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 920px; margin-top: 20px; }                
        .ulDados input { margin-bottom: 0 !important; }
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px; margin-right: 10px; }
        .liDados1 { width: 398px; border-right: solid 1px #E5E5E5; }
        .liDados2{ width: 500px; }
        .liTitulo1 { text-transform: uppercase; background-color: #E5E5E5; text-align: center; height: 20px; width: 390px; }
        .liTitulo2 { text-transform: uppercase; background-color: #99BB99; text-align: center; height: 20px; width: 500px; }
        .liTitulo1 label, .liTitulo2 label { vertical-align: middle; line-height: 20px; }
        .liClear { clear: both; }
        .liAutor, .liNomeObra { margin-right: 0 !important; }
        .liPesquisar { margin-bottom: 0 !important; margin-right: 0 !important; margin-top: 11px !important;}
        .liAdicionar { margin: 0 !important; position: relative; left: 276px; top: 126px; }
        .liData { margin-left: 44px; margin-right: 0 !important;}      
        #ulPopup1 li { margin-bottom: 13px !important; }            
        #divPopup ul li { margin-bottom: 10px; }    
        .liNumeroPaginas { float: left; }
        .liPopupIsbn { float: left; margin-right: 20px; }       
        .liLocal{ margin-bottom: 2px !important;}     
        
        /*--> CSS DADOS */
        .liInformacoes div { height: 9px; margin-top: -10px; }
        .ddlAreaConhecimento  
        {
        	width: 150px;
            height: 14px;
        }
        .ddlAutor { width: 228px; }
        .ddlEditora { width: 150px; }
        .txtNomeObra { width: 225px; }
        .txtIsbn { width: 80px; text-align: right; }        
        .ddlTipo { width: 100px; }
        .txtObservacao { width: 500px; height: 39px; }
        .divGridPesquisa { border: solid 1px #E5E5E5; width: 388px; height: 200px; overflow-y: scroll; overflow-x: hidden; }
        .divGridEmprestimo { border: solid 1px #E5E5E5; width: 500px; height: 200px; overflow-y: scroll; overflow-x: hidden; }        
        .divGridEmprestimo th { background-color: #99BB99 !important; }
        #divPopup { display: none; padding: 15px; }
        #divPopup ul { float: left; }
        #divPopup .ui-dialog-title { margin-left: 80px; }        
        #ulPopup1 { border-right: solid 1px #CCCCCC; margin-right: 20px; padding-right: 15px; }
        .labelNomeCampo, .labelTitulo { font-weight: bold; }        
        .liLocal label{ display: inline; height: 30px; }
        .liLocal .labelNomeCampo { font-weight: normal; margin-left: 10px; }        
        .liSinopse div { height: 50px; width: 280px; border: solid 1px #888888; overflow-y: scroll; padding: 2px; }   
        
        #divBarraPadraoContent{display:none;}
        #divBarraEmpreBibli { position:absolute; margin-left: 770px; margin-top:-50px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraEmpreBibli ul { display: inline; float: left; margin-left: 10px; }
        #divBarraEmpreBibli ul li { display: inline; margin-left: -2px; }
        #divBarraEmpreBibli ul li img { width: 19px; height: 19px; }
             
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraEmpreBibli { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<div id="div1" class="bar" > 
        <div id="divBarraEmpreBibli" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
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
                <img title="Abre o formulario para Criar um Novo Registro."
                        alt="Icone de Criar Novo Registro." 
                        src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
            </li>
        </ul>
        <ul id="ulGravar">
            <li>
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnEmpreBibli_Click">
                    <img title="Grava o registro atual na base de dados."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar.png" />
                </asp:LinkButton>
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
            <li  id="Li1">
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
        <li class="liDados1">
            <ul>
                <li class="liTitulo1">
                    <label>Pesquisa Itens Acervo</label>
                </li>
                <li class="liClear">
                    <label for="ddlAreaConhecimento">Área Conhecimento</label>
                    <asp:DropDownList ID="ddlAreaConhecimento" CssClass="ddlAreaConhecimento" runat="server"></asp:DropDownList>
                </li>
                <li class="liAutor">
                    <label for="ddlAutor">Autor</label>
                    <asp:DropDownList ID="ddlAutor" CssClass="ddlAutor" runat="server"></asp:DropDownList>
                </li>
                <li class="liClear">
                    <label for="ddlEditora">Editora</label>
                    <asp:DropDownList ID="ddlEditora" CssClass="ddlEditora" runat="server"></asp:DropDownList>
                </li>
                <li class="liNomeObra">
                    <label for="ddlNomeObra">Nome da Obra</label>
                    <asp:TextBox ID="txtNomeObra" CssClass="txtNomeObra" runat="server"></asp:TextBox>
                </li>
                <li class="liClear">
                    <label for="txtIsbn">ISBN</label>
                    <asp:TextBox ID="txtIsbn" CssClass="txtIsbn" runat="server"></asp:TextBox>
                </li>
                <li class="liPesquisar">
                    <asp:ImageButton ID="btnPesquisar" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" runat="server" CausesValidation="false" OnClick="btnPesquisar_Click" />
                </li>
                <li class="liAdicionar">
                     <asp:ImageButton ID="btnAdicionar" ToolTip="Clique para adicionar os itens marcados" ImageUrl="~/Library/IMG/Gestor_SetaDireita.png" runat="server" CausesValidation="false" OnClick="btnAdicionar_Click" />
                </li>
                <li class="liClear">
                    <div class="divGridPesquisa">
                        <asp:GridView ID="grvPesquisa" CssClass="grdBusca" Width="372px" runat="server" AutoGenerateColumns="False" onrowdatabound="grvPesquisa_RowDataBound" onrowdeleting="grvPesquisa_RowDeleting">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataTemplate>
                                <table rules="all" cellspacing="0" border="1" style="width: 374px; border-collapse: collapse; margin-left: -5px; margin-top: -2px;" class="grdBusca">
		                            <tbody>
		                                <tr>
			                                <th style="width: 0px;" scope="col">&nbsp;</th>
			                                <th style="width: 70px;" scope="col">ISBN</th>
			                                <th scope="col">Obra</th>
			                                <th style="width: 0px;" scope="col">&nbsp;</th>
			                                <th style="width: 90px;" scope="col">Cód. Interno</th>
		                                </tr>
	                                </tbody>
	                            </table>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemStyle Width="0px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CO_ISBN_ACER" HeaderText="ISBN">
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_ACERVO" HeaderText="Obra">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CommandField HeaderStyle-Width="0px" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Library/IMG/Gestor_BtnEdit.png" />
                                <asp:TemplateField HeaderText="Cód. Interno">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCodigo" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
        <li class="liDados2">
            <ul>
                <li class="liTitulo2">
                    <label>Registro de Empréstimo</label>
                </li>
                <li class="liClear">
                    <label for="ddlTipo">Tipo</label>
                    <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                        <asp:ListItem Value="">Todos</asp:ListItem>
                        <asp:ListItem Value="A">Aluno</asp:ListItem>
                        <asp:ListItem Value="P">Professor</asp:ListItem>
                        <asp:ListItem Value="F">Funcionário</asp:ListItem>
                        <asp:ListItem Value="O">Outro</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="ddlNome" class="lblObrigatorio">Nome</label>
                    <asp:DropDownList ID="ddlNome" CssClass="campoNomePessoa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNome_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlNome" CssClass="validatorField" ErrorMessage="Nome deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li class="liData">
                    <label for="txtData">Data</label>
                    <asp:TextBox ID="txtData" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtData" CssClass="validatorField" ErrorMessage="Data deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li class="liInformacoes">
                    <div>
                        <asp:Label ID="lblInformacaoUsuario" runat="server" Text=""></asp:Label>
                    </div>
                </li>
                <li class="liClear">
                    <label for="txtObservacao">Observação</label>
                    <asp:TextBox ID="txtObservacao" CssClass="txtObservacao" TextMode="MultiLine" runat="server"></asp:TextBox>
                </li>
                <li class="liClear">
                    <div class="divGridEmprestimo">
                        <asp:GridView ID="grvEmprestimo" CssClass="grdBusca" runat="server" Width="484px" AutoGenerateColumns="False" onrowdeleting="grvEmprestimo_RowDeleting">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataTemplate>
                                <table rules="all" cellspacing="0" border="1" style="width: 484px; border-collapse: collapse; margin-left: -5px; margin-top: -2px;" class="grdBusca">
		                            <tbody>
		                                <tr>
			                                <th style="width: 70px;" scope="col">Cód. Interno</th>
			                                <th scope="col">Obra</th>
			                                <th style="width: 76px;" scope="col">Entrega</th>
			                                <th style="width: 105px;" scope="col">Observação</th>
			                                <th style="width: 0px;" scope="col">&nbsp;</th>
		                                </tr>
	                                </tbody>
	                            </table>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="CodigoInterno" HeaderText="Cód. Interno">
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NomeObra" HeaderText="Obra">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Entrega">
                                    <ItemStyle Width="76px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDataEntrega" Text="<%# bind('DataEntregaStr') %>" CssClass="campoData" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observação">
                                    <ItemStyle Width="105px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtObservacao" Text="<%# bind('Observacao') %>" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField HeaderStyle-Width="0px" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Library/IMG/Gestor_BtnDel.png" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
    </ul>
    <div id="divPopup">
        <ul id="ulPopup1">
            <li>
                <label class="labelNomeCampo">Área de Conhecimento</label>
                <asp:Label ID="lblAreaConhecimento" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="labelNomeCampo">Classificação</label>
                <asp:Label ID="lblClassificacao" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="labelNomeCampo">Editora</label>
                <asp:Label ID="lblEditora" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="labelNomeCampo">Autor</label>
                <asp:Label ID="lblAutor" runat="server" Text=""></asp:Label>
            </li>
            <li class="liPopupIsbn">
                <label class="labelNomeCampo">ISBN</label>
                <asp:Label ID="lblIsbn" runat="server" Text=""></asp:Label>
            </li>
            <li class="liNumeroPaginas">
                <label class="labelNomeCampo">N° Pág.</label>
                <asp:Label ID="lblPaginas" runat="server" Text=""></asp:Label>                
            </li>
        </ul>
        <ul>
            <li>
                <label class="labelNomeCampo">Nome da Obra</label>
                <asp:Label ID="lblNomeObra" runat="server" Text=""></asp:Label>
            </li>
            <li class="liSinopse">
                <label class="labelNomeCampo">Sinopse</label>
                <div>
                    <asp:Label ID="lblSinopse" runat="server" Text=""></asp:Label>
                </div>
            </li>
            <li class="liLocal">
                <label class="labelTitulo">Localização</label>
            </li>
            <li class="liLocal">
                <label class="labelNomeCampo">Local:</label>
                <asp:Label ID="lblLocal" runat="server" Text=""></asp:Label>
            </li>
            <li class="liLocal">
                <label class="labelNomeCampo">Coordenada 1:</label>
                <asp:Label ID="lblCoordenada1" runat="server" Text=""></asp:Label>
            </li>
            <li class="liLocal">
                <label class="labelNomeCampo">Coordenada 2:</label>
                <asp:Label ID="lblCoordenada2" runat="server" Text=""></asp:Label>
            </li>
            <li class="liLocal">
                <label class="labelNomeCampo">Coordenada 3:</label>
                <asp:Label ID="lblCoordenada3" runat="server" Text=""></asp:Label>
            </li>
        </ul>
        <input id="hdfVisible" class="hdfVisible" type="hidden" value="N" runat="server"/>
    </div>
    <script type="text/javascript">
        $(document).ready(function() {
            $(".txtIsbn").mask("999-99-9999-999-9");          
            if ($(".hdfVisible").val() == 'S') 
            {
                $('#divPopup').dialog("open").dialog(
                    { 
                        modal: true,
                        width: 500, 
                        height: 250,
                        resizable: false, 
                        bgiframe: true,
                        title: 'Informações do Livro'                        
                    }
                ).parent().appendTo("/html/body/form[0]");
            }            
            $(".hdfVisible").attr("value",'N');
        });
    </script>
</asp:Content>