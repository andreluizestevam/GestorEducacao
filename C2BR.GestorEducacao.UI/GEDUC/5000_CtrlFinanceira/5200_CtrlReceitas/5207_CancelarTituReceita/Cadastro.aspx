<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5207_CancelarTituReceita.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS LIs */
        .ulDados {width: 600px;}        
        .liClear{clear: both;}
        
        /*--> CSS DADOS */
        .liBtnAdd
        {
            clear: both;
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-left: 520px;
            margin-top: 8px;
            margin-bottom: 8px;
            margin-right: 8px !important;
            padding: 2px 3px 1px 3px;
        }
        
        /*--> CSS DADOS */
        .divGrid
        {
            width: 600px;
            overflow-y: auto;
            margin-top: 10px;
            height: 290px;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
        #divBarraPadraoContent{display: none;}
        #divBarraComoChegar
        {
            position: absolute;
            margin-left: 750px;
            margin-top: -25px;
            border: 1px solid #CCC;
            overflow: auto;
            width: 230px;
            padding: 3px 0;
        }
        #divBarraComoChegar ul
        {
            display: inline;
            float: left;
            margin-left: 10px;
        }
        #divBarraComoChegar ul li
        {
            display: inline;
            margin-left: -2px;
        }
        #divBarraComoChegar ul li img
        {
            width: 19px;
            height: 19px;
        }
        .liResumo
        {
            clear: both;
            margin-top: 10px;
            margin-left: 200px;
        }
        .check label{display: inline;}
        .check input{margin-left: -5px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div id="div1" class="bar">
        <div id="divBarraComoChegar" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
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
                <li id="btnNovo">
                    <img title="Abre o formulario para Criar um Novo Registro." alt="Icone de Criar Novo Registro."
                        src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
                </li>
            </ul>
            <ul id="ulGravar">
                <li>
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSalvar_Click">
                    <img title="Direciona para tela de movimentação de caixa."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar.png" />
                    </asp:LinkButton>
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 39px;">
                <li id="btnExcluir">
                    <img title="Exclui o Registro atual selecionado." alt="Icone de Excluir o Registro."
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                </li>
                <li id="btnCancelar"><a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                        alt="Icone de Cancelar Operacao Atual." src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a></li>
            </ul>
            <ul id="ulAcoes" style="width: 39px;">
                <li id="btnPesquisar">
                    <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                        alt="Icone de Pesquisa." src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                </li>
                <li id="liImprimir">
                    <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão."
                        alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                </li>
            </ul>
        </div>
    </div>
    <div style="height: 500px;">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtNomeEmp" title="Unidade">
                Unidade</label>
            <asp:TextBox ID="txtNomeEmp" CssClass="campoUnidadeEscolar" Enabled="false" runat="server" ToolTip="Nome da unidade atual"></asp:TextBox>
        </li>
        <li>
            <label for="ddlUnidadeContrato" title="Unidade de Contrato">Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidadeContrato" runat="server" CssClass="campoUnidadeEscolar" ToolTip="Selecione a Unidade de Contrato" />
            <asp:RequiredFieldValidator ID="rfvUni" runat="server" 
                ControlToValidate="ddlUnidadeContrato" ErrorMessage="Informe a unidade">*</asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlAgrupador" title="Agrupador">Agrupador</label>
            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Histórico" Width="120">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAgru" runat="server" 
                ControlToValidate="ddlAgrupador" ErrorMessage="Informe o agrupador">*</asp:RequiredFieldValidator>
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liClear">
                    <label for="ddlResponsavel" title="Responsável">Responsável</label>
                    <asp:DropDownList ID="ddlResponsavel" runat="server" ToolTip="Selecione o Responsável" Width="210" AutoPostBack="true"
                        onselectedindexchanged="ddlResponsavel_SelectedIndexChanged"/>
                    <asp:RequiredFieldValidator ID="rfvRes" runat="server" 
                        ControlToValidate="ddlResponsavel" ErrorMessage="Informe o responsavel">*</asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:Label CssClass="lblObrigatorio" runat="server" ID="lblAluPaci">Aluno</asp:Label><br />
                    <asp:DropDownList ID="ddlAluno" Width="210" runat="server" ToolTip="Selecione o desejado" />
                    <asp:RequiredFieldValidator ID="rfvAluno" runat="server" 
                        ControlToValidate="ddlAluno" ErrorMessage="Informe o desejado">*</asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li style="margin-top:10px;">
            <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                Período (Data de Vencimento)</label>
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" ToolTip="Informe a Data Inicial"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoIni" Text="*" ErrorMessage="Campo Data Período Início é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData"
                runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red" ControlToValidate="txtDataPeriodoFim" ControlToCompare="txtDataPeriodoIni"
                Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
            </asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoFim" Text="*" ErrorMessage="Campo Data Período Fim é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="Li1" runat="server" title="Processar arqui retorno" class="liBtnAdd">
                <asp:LinkButton ID="btnGerar" runat="server" class="btnLabel" OnClick="btnPesquisar_Click" Width="70">PESQUISAR</asp:LinkButton>
            </li>
            <li class="liResumo" id="liResumo" runat="server" visible="false"><span style="font-size: 1.4em;
                font-weight: bold;">*** TÍTULOS EM ABERTO ***</span> </li>
            <li class="liClear">
                <div id="divGrid" runat="server" class="divGrid">
                    <asp:GridView ID="grdResumo" CssClass="grdBusca" Width="580px" runat="server" AutoGenerateColumns="False">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            NENHUM TÍTULO ENCONTRADO.<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle Width="0px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" Checked='<%# bind("Checked") %>' runat="server"/>
                                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NU_DOC" HeaderText="Nº Documento">
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_PAR" HeaderText="PA">
                                <ItemStyle Width="20px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VR_PAR_DOC" HeaderText="R$ Valor">
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DE_HISTORICO" HeaderText="Histórico">
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DT_VEN_DOC" HeaderText="Vencimento" DataFormatString = "{0:dd/MM/yyyy}">
                                <ItemStyle Width="80px"></ItemStyle>
                            </asp:BoundField>   
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
    </ul>
    </div>
</asp:Content>
