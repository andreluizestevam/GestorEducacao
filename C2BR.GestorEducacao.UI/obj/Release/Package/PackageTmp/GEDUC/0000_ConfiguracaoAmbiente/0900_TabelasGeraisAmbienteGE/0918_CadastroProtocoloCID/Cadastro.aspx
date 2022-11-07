<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0918_CadastroProtocoloCID.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 200px;
            margin-left: 370px !important;
        }
        
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        
        .liSituacao
        {
            clear: both;
            margin-left: 381px;
            margin-top: -70px;
        }
        
        .liTipo
        {
            clear: both;
            margin-left: -187px;
        }
        
        .liNome
        {
            clear: both;
            margin-left: -201px;
            margin-top: 5px;
        }
        
        .liCID
        {
            clear: both;
            margin-left: -201px;
        }
        
        .liDescricao
        {
            margin-left: 60px;
        }
        
        /*--> CSS DADOS */
        .ulDados label
        {
            margin-bottom: 1px;
        }
        
        .ddlTipo, .ddlSituacao, .ddlCID
        {
            width: auto;
        }
        .txtNomeItem
        {
            float: left;
            margin-left: -183px;
            margin-top: 0px;
            margin-bottom: 5px;
        }
        .txtDescItem
        {
            float: left;
            margin-left: 18px;
            margin-top: 0px;
            margin-bottom: 5px;
        }
        .chkSituItem
        {
            float: left;
            margin-top: 0px;
            margin-left: 463px;
        }
        .ddlTipoItem
        {
            float: left;
            margin-left: 370px;
            margin-top: -40px;
        }
        
        #divBarraPadraoContent{display:none;}
        #divBarraDownlArqui { position:absolute; margin-left: 770px; margin-top:-50px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraDownlArqui ul { display: inline; float: left; margin-left: 10px; }
        #divBarraDownlArqui ul li { display: inline; margin-left: -2px; }
        #divBarraDownlArqui ul li img { width: 19px; height: 19px; }
    </style>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidCID" />
    <ul id="ulDados" class="ulDados">
        <li class="liCID">
            <label for="lblCID" title="Código do CID">
                CID</label>
            <asp:DropDownList ID="ddlCID" ToolTip="Escolha um Código do CID" runat="server" class="ddlCID">
            </asp:DropDownList>
        </li>
        <li class="liNome">
            <label for="lblNome" title="Nome reduzido do protocolo de CID">
                Nome Reduzido Protocolo</label>
            <asp:TextBox ID="txtNome" ToolTip="Digite o nome do protocolo CID" runat="server"
                MaxLength="30" Width="215px"></asp:TextBox>
        </li>
        <li class="liDescricao" style="margin-top: -70px;">
            <label for="lblDescricao" title="Descrição do protocolo de CID">
                Descrição Protocolo</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Digite a descrição do protocolo CID" runat="server"
                MaxLength="200" TextMode="MultiLine" Rows="4" Width="310px" onkeydown="checkTextAreaMaxLength(this, event, '200');"></asp:TextBox>
        </li>
        <li class="liSituacao">
            <label title="Situação do protocolo CID">
                Situação Protocolo</label>
            <asp:DropDownList runat="Server" ID="ddlSituacao" ToolTip="Escolha a situação do protocolo CID">
                <asp:ListItem Value="A" Text="Ativo" Selected="true"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear" style="margin-top: 0px; color: green; margin-left:-201px;">
            <label style="font-size: 12px;" title="Grupo de informação e cadastro referente aos itens de protocolo">
                Itens Protocolo</label>
        </li>
        <li class="liClear" style="margin-left: -18px;">
            <%--<label style="float: left; margin-left: -204px;">
                ST</label>--%>
            <label style="float: left; margin-left: -183px;" title="Nome reduzido do item de protocolo de CID">
                Nome Reduzido</label>
            <label style="float: left; margin-left:52px;" title="Descrição do item de protocolo de CID">
                Descrição</label>
            <label style="float: left; margin-left:41px;" title="Tipo do item de protocolo de CID">
            <label style="float: left; margin-left:330px; margin-top:-14px;" >
                Tipo</label>
        </li>
        <li class="liClear" style="margin-left: -18px;">
            <%--<asp:CheckBox CssClass="chkSituItem" Checked="true" runat="server" ID="chkSituItem"
                ToolTip="Marque para definir a situação do item como ativa, desmarque para defini-la como inativa" />--%>
            <asp:TextBox CssClass="txtNomeItem" ID="txtNomeItem" ToolTip="Digite o nome reduzido do item a ser inserido no protocolo"
                runat="server" MaxLength="30" Width="215px"></asp:TextBox>
            <asp:TextBox CssClass="txtDescItem" ID="txtDescItem" ToolTip="Digite a descrição do item a ser inserido no protocolo"
                runat="server" TextMode="MultiLine" Rows="3" MaxLength="100" Width="300px"></asp:TextBox>
            <asp:DropDownList CssClass="ddlTipoItem" ID="ddlTipoItem" runat="server" ToolTip="Escolha o tipo do item protocolo CID">
                <asp:ListItem Text="Consulta" Value="1" Selected="true" />
                <asp:ListItem Text="Exame" Value="2" />
                <asp:ListItem Text="Procedimento" Value="3" />
                <asp:ListItem Text="Vacina" Value="4" />
            </asp:DropDownList>
            <asp:ImageButton AutoPostBack="true" ToolTip="Clique para adicionar um novo item de protocolo"
                ID="imgAdd" Height="15px" Width="15px" Style="margin-top: -41px; margin-left: 463px;
                float: left;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" runat="server"
                OnClick="onClick_AddItem" />
        </li>
        <li>
            <div style="width: 664px; margin-left:-202px;">
                <input type="hidden" id="divItensPos" name="divItensPos" />
                <asp:GridView ID="grdItensProto" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                    ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum item cadastrado neste protocolo.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="ST">
                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField ID="idItemProto" runat="server" Value='<%# Eval("ID_ITEM_PROTO") %>' />
                                <asp:CheckBox runat="server" ID="chkSituaItemGrd" Checked='<%# Eval("CO_SITU") %>'
                                    Width="100%" Style="margin: 0 0 0 -15px !important;" AutoPostBack="true" OnCheckedChanged="alterItemChk"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nome Reduzido">
                            <ItemStyle Width="215px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox BackColor="" ID="txtNomeItemGrd" Text='<%# Eval("NO_ITEM_PROTO") %>'
                                    ToolTip="Digite o nome reduzido do item a ser inserido no protocolo" runat="server" MaxLength="30"
                                    Width="215px" AutoPostBack="true" OnTextChanged="alterItemTxtNome"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descrição">
                            <ItemStyle Width="215px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtDescItem" ToolTip="Digite a descrição do item a ser inserido no protocolo"
                runat="server" TextMode="MultiLine" Rows="3" MaxLength="100" Width="300px" AutoPostBack="true" OnTextChanged="alterItemTxtDesc"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo">
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlTipoItemGrd" runat="server" AutoPostBack="true" ToolTip="Escolha o tipo do item protocolo CID" OnSelectedIndexChanged="alterItemDdl">
                                    <asp:ListItem Text="Consulta" Value="1" />
                                    <asp:ListItem Text="Exame" Value="2" />
                                    <asp:ListItem Text="Procedimento" Value="3" />
                                    <asp:ListItem Text="Vacina" Value="4" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EX">
                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" AutoPostBack="true" ID="imgExcItem" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                    ToolTip="Excluir Item do Protocolo" OnClick="imgExcItem_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
</asp:Content>
