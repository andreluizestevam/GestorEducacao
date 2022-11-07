<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0920_CadastroProtocoloAcaoItens.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 600px;
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
            margin-left: 0px;
            margin-top: 0px;
        }
        
        .liCID
        {
            clear: both;
            margin-left: -201px;
        }
        
        .liDescricao
        {
            margin-left: 0px;
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
            margin-left: -220px;
            margin-top: 0px;
            margin-bottom: 5px;
        }
        .txtDescItem
        {
            float: left;
            margin-left: -56px;
            margin-top: 0px;
            margin-bottom: 5px;
        }
        .txtReferItem
        {
            float: left;
            margin-left: 17px;
            margin-top: 0px;
            margin-bottom: 5px;
        }
        .txtQtdItem
        {
            float: left;
            margin-left: 17px;
            margin-top: 0px;
            margin-bottom: 5px;
        }
        .txtSeqItem
        {
            float: left;
            margin-left: -262px;
            margin-top: 0px;
            margin-bottom: 5px;
        }
        .chkSituItem
        {
            float: left;
            margin-top: 0px;
            margin-left: 463px;
        }
        .ddlPriorItem
        {
            float: left;
            margin-left: 10px;
            margin-top: 0px;
        }
        
        .txtObsItem
        {
            float: left;
            margin-left: 8px;
            margin-top: 0px;
            margin-bottom: 5px;
        }
        
        .grdBusca
        {
            height: auto;
            overflow: scroll;
            max-height: 200px;
        }
        
        #divBarraPadraoContent
        {
            display: none;
        }
        #divBarraDownlArqui
        {
            position: absolute;
            margin-left: 770px;
            margin-top: -50px;
            border: 1px solid #CCC;
            overflow: auto;
            width: 230px;
            padding: 3px 0;
        }
        #divBarraDownlArqui ul
        {
            display: inline;
            float: left;
            margin-left: 10px;
        }
        #divBarraDownlArqui ul li
        {
            display: inline;
            margin-left: -2px;
        }
        #divBarraDownlArqui ul li img
        {
            width: 19px;
            height: 19px;
        }
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
    <div id="div1" class="bar">
        <div id="divBarraDownlArqui" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <ul id="ulNavegacao" style="width: 39px;">
                <li id="btnVoltarPainel"><a href="javascript:parent.showHomePage()">
                    <img title="Clique para voltar ao Painel Inicial." alt="Icone Voltar ao Painel Inicial."
                        src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                </a></li>
                <li id="btnVoltar"><a href="javascript:BackToHome();">
                    <img title="Clique para voltar a Pagina Anterior." alt="Icone Voltar a Pagina Anterior."
                        src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                </a></li>
            </ul>
            <ul id="ulEditarNovo" style="width: 39px;">
                <li id="btnEditar">
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
                <li id="btnCancelar"><a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                        alt="Icone de Cancelar Operacao Atual." src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a></li>
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
                        alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                </li>
            </ul>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidProto" />
    <ul id="ulDados" class="ulDados">
        <li style="width: 95px; margin-left: -77px;">
            <label for="lblTipo" title="Tipo do protocolo" class="lblObrigatorio">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" Width="120px" ToolTip="Escolha o tipo do protocolo"
                runat="server" class="ddltipo">
                <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Acomodação" Value="ACO"></asp:ListItem>
                <asp:ListItem Text="Controles Internos" Value="CTI"></asp:ListItem>
                <asp:ListItem Text="Esterilização" Value="EST"></asp:ListItem>
                <asp:ListItem Text="Higienização" Value="HIG"></asp:ListItem>
                <asp:ListItem Text="Lavanderia" Value="LAV"></asp:ListItem>
                <asp:ListItem Text="Manutenção" Value="MAN"></asp:ListItem>
                <asp:ListItem Text="Procedimento" Value="PRO"></asp:ListItem>
                <asp:ListItem Text="Segurança" Value="SEG"></asp:ListItem>
                <asp:ListItem Text="Serviços Camareira" Value="SCA"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left: 38px;">
            <label for="lblNome" title="Nome reduzido do protocolo de ações" class="lblObrigatorio">
                Potocolo</label>
            <asp:TextBox ID="txtNome" ToolTip="Digite o nome do protocolo de ações" runat="server"
                MaxLength="30" Width="215px"></asp:TextBox>
        </li>
        <li style="margin-left: 292px; margin-top: -36px;">
            <label for="lblSigla" title="Sigla do protocolo de ações" class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Digite asigla do protocolo de ações" runat="server"
                MaxLength="6" Width="45px"></asp:TextBox>
        </li>
        <li style="margin-top: 0px; margin-left: -77px;">
            <label for="lblDescricao" title="Descrição do protocolo de ações">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Digite a descrição do protocolo de ações"
                runat="server" MaxLength="100" TextMode="MultiLine" Rows="2" Width="414px" onkeydown="checkTextAreaMaxLength(this, event, '100');"></asp:TextBox>
        </li>
        <li style="margin-top: 4px; margin-left: 180px;">
            <label title="Situação do protocolo de ações">
                Situação
            </label>
            <asp:DropDownList ID="ddlSituacao" runat="server" ToolTip="Selecione a situação do protocolo de ações">
                <asp:ListItem Text="Ativo" Value="A" />
                <asp:ListItem Text="Inativo" Value="I" />
            </asp:DropDownList>
        </li>
        <li class="liData" style="margin-top: 4px;">
            <label title="Data da situação do protocolo de ações">
                Data</label>
            <asp:TextBox ID="txtData" runat="server" CssClass="campoData" ToolTip="Digite uma data para a situação do protocolo de ações"></asp:TextBox>
        </li>
        <li class="liClear" style="margin-top: 4px; color: green; margin-left: -330px;">
            <div style="background-color: Gray; width: 925px;">
                <label style="color: White; text-align: center; font-size: 12px;" title="Grupo de informação e cadastro referente aos itens de protocolo de ações">
                    Itens Protocolo</label>
            </div>
        </li>
        <li class="liClear" style="margin-left: -18px;">
            <label style="float: left; margin-left: -312px;" title="Sequencia de execução da ação do protocolo de ação">
                SEQ</label>
            <label style="float: left; margin-left: -270px;" title="Ação do protocolo">
                Ação</label>
            <label style="float: left; margin-left: -106px;" title="Descrição da ação do protocolo">
                Descrição</label>
            <label style="float: left; margin-left: 163px;" title="Código de referência para a ação do protocolo">
                Referência</label>
            <label style="float: left; margin-left: 59px;" title="Quantidade de vezes que essa ação deve ser executada">
                QTD</label>
            <label style="float: left; margin-left: 11px;" title="Prioridade de execução da ação do protocolo">
                Prioridade</label>
            <label style="float: left; margin-left: 42px;" title="Observação referente a ação do protocolo">
                Observação</label>
        </li>
        <li class="liClear" style="margin-left: -67px;">
            <asp:TextBox CssClass="txtSeqItem" ID="txtSeqItem" ToolTip="Digite a sequencia que essa ação deve ser executada"
                runat="server" MaxLength="3" Width="20px"></asp:TextBox>
            <asp:TextBox CssClass="txtNomeItem" ID="txtNomeItem" ToolTip="Digite o nome reduzido da ação a ser inserida no protocolo"
                runat="server" MaxLength="30" TextMode="MultiLine" onkeydown="checkTextAreaMaxLength(this, event, '30');"
                Width="142px"></asp:TextBox>
            <asp:TextBox CssClass="txtDescItem" ID="txtDescItem" ToolTip="Digite a descrição da ação a ser inserida no protocolo"
                runat="server" TextMode="MultiLine" Rows="3" MaxLength="150" Width="250px" onkeydown="checkTextAreaMaxLength(this, event, '150');"></asp:TextBox>
            <asp:TextBox CssClass="txtReferItem" ID="txtReferItem" ToolTip="Digite o código de referância para a ação do protocolo"
                runat="server" MaxLength="12" Width="85px"></asp:TextBox>
            <asp:TextBox CssClass="txtQtdItem" ID="txtQtdItem" ToolTip="Digite a quantidade que essa ação deve ser executada"
                runat="server" MaxLength="3" Width="20px"></asp:TextBox>
            <asp:DropDownList CssClass="ddlPriorItem" ID="ddlPriorItem" runat="server" ToolTip="Escolha a prioridade de execução da ação do protocolo">
            </asp:DropDownList>
            <asp:TextBox CssClass="txtObsItem" ID="txtObsItem" ToolTip="Digite uma observação para a ação do protocolo"
                runat="server" TextMode="MultiLine" Rows="3" MaxLength="150" Width="200px" onkeydown="checkTextAreaMaxLength(this, event, '150');"></asp:TextBox>
            <asp:ImageButton AutoPostBack="true" ToolTip="Clique para adicionar uma nova ação ao protocolo"
                ID="imgAdd" Height="15px" Width="15px" Style="margin-top: -30px; margin-left: 647px;
                float: left;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" runat="server"
                OnClick="onClick_AddItem" />
        </li>
        <li>
            <div style="width: auto; margin-left: -330px; height: auto; overflow: scroll; max-height: 246px;">
                <input type="hidden" id="divItensPos" name="divItensPos" />
                <asp:GridView ID="grdItensProto" CssClass="grdBusca" runat="server" Style="width: 880px; cursor: default;" AutoGenerateColumns="false" AllowPaging="false"
                    GridLines="Vertical" ShowHeaderWhenEmpty="true">
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
                                    Width="100%" Style="margin: 0 0 0 -15px !important;" AutoPostBack="true" OnCheckedChanged="alterItemChk" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SEQ">
                            <ItemStyle Width="144px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtSeqItem" Text='<%# Eval("SEQ_ITEM") %>' ToolTip="Digite a sequencia que essa ação deve ser executada"
                                    runat="server" MaxLength="3" Width="20px" AutoPostBack="true" OnTextChanged="alterItemTxtSeq"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ação">
                            <ItemStyle Width="144px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox BackColor="" ID="txtNomeItemGrd" Text='<%# Eval("NO_ITEM_PROTO") %>'
                                    ToolTip="Digite o nome reduzido da ação a ser inserida no protocolo" runat="server"
                                    MaxLength="30" TextMode="MultiLine" onkeydown="checkTextAreaMaxLength(this, event, '30');"
                                    Width="142px" AutoPostBack="true" OnTextChanged="alterItemTxtNome"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descrição">
                            <ItemStyle Width="252px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtDescItem" Text='<%# Eval("DE_ITEM_PROTO") %>' ToolTip="Digite a descrição da ação a ser inserida no protocolo"
                                    runat="server" TextMode="MultiLine" Rows="3" MaxLength="100" Width="250px" onkeydown="checkTextAreaMaxLength(this, event, '100');"
                                    AutoPostBack="true" OnTextChanged="alterItemTxtDesc"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refer">
                            <ItemStyle Width="144px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtReferItemGrd" Text='<%# Eval("REFER_ITEM_PROTO") %>' ToolTip="Digite o código de referância para a ação do protocolo"
                                    runat="server" MaxLength="12" Width="85px" AutoPostBack="true" OnTextChanged="alterItemTxtRefer"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qtd">
                            <ItemStyle Width="144px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtQtdItemGrd" Text='<%# Eval("QTD_ITEM_PROTO") %>' ToolTip="Digite a quantidade que essa ação deve ser executada"
                                    runat="server" MaxLength="3" Width="20px" AutoPostBack="true" OnTextChanged="alterItemTxtQtd"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prioridade">
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlPriorItem" runat="server" ToolTip="Escolha a prioridade de execução da ação do protocolo"
                                    AutoPostBack="true" OnSelectedIndexChanged="alterItemDdl">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Observação">
                            <ItemStyle Width="202px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtObsItem" Text='<%# Eval("OBS_ITEM_PROTO") %>' ToolTip="Digite uma observação para a ação do protocolo"
                                    runat="server" TextMode="MultiLine" Rows="3" MaxLength="60" Width="200px" onkeydown="checkTextAreaMaxLength(this, event, '100');"
                                    AutoPostBack="true" OnTextChanged="alterItemTxtObs"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EX">
                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" AutoPostBack="true" ID="imgExcItem" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                    ToolTip="Excluir ação do protocolo" OnClick="imgExcItem_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
</asp:Content>
