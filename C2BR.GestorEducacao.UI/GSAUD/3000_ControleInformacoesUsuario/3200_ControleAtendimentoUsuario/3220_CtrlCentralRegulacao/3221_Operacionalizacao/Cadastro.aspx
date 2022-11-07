<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3220_CtrlCentralRegulacao._3221_Operacionalizacao.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 973px;
        }
        label
        {
            margin-bottom:1px;
        }
        .th
        {
            position:relative;
        }
        #divLoadInfosCadas .ulDados
        {
            width:auto;
        }
        
         #divLoadInfosCadas .ulDados input
        {
            height: 13px !important;
        }
        
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
            font-size:9px !important;
        }
  
           .divAtendPendentes
        {
            border: 1px solid #CCCCCC;
            width: 100%;
            height: 180px;
            overflow-y: scroll;
            margin-left: 5px;
            margin-bottom: 13px;
            <%--display: none;--%>
        }
        .divDetalhePendencia
        {
            border: 1px solid #CCCCCC;
            width: 976px;
            height: 180px;
            overflow-y: scroll;
            margin-left: 5px;
            margin-bottom: 13px;
            <%--display: none;--%>
        }
          .chkLocais label
        {
            display: inline !important;
        }
        .lblsub
        {
            color: #436EEE;
            clear: both;
            margin-bottom: -3px;
        }
        .lblTitInf
        {
            text-transform: uppercase;
            font-weight: bold;
            font-size: 1.0em;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
            margin-top: 10px;
        }
        .liLeft
        {
            margin-top: 10px;
        }
        
        
        /*--> CSS DADOS */
        #divBarraPadraoContent
        {
            display: none;
        }
        #divBarraComoChegar
        {
            position: absolute;
            margin-left: 750px;
            margin-top: -45px;
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
        
        /* ------------------------ FORMATAÇÃO DA GRADE DE HISTÓRICO DE OCORRÊNCIAS -------------------------*/
        
            #divListarResponsaveisContent .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarResponsaveisContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarResponsaveisContent .alternateRowStyleLR td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarResponsaveisContent .rowStyleLR td
        {
        	padding-left: 5px;
        	padding-right: 5px;
        }
        #divListarResponsaveisContainer #divRodapeLR
        {
            margin-top: 10px;
            float: right;
        }
        #divListarResponsaveisContainer #imgLogoGestorLR
        {
            width: 127px;
            height: 30px;
        }
        #divHelpTxtLR
        {
            float: left;
            margin-top: 10px;
            width: 400px;
            color: #DF6B0D;
            font-weight: bold;
        }
        .pAcesso
        {
        	font-size: 1.1em;
        	color: #4169E1;
        }
        .pFechar
        {
        	font-size: 0.9em;
        	color: #FF6347;
        	margin-top: 2px;
        }
        #divListarResponsaveisContent
        {
        	height: 261px;
        	overflow-y: auto;
        	border: 1px solid #EBF0FB;
        }
        .liFoto
        {
            float: left !important;
            margin-right: 0 !important;
        }
        .fldFotoAluno
        {
            border: none;
            width: 80px;
            height: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="div1" class="bar" style="margin-top: 43px !important; margin-bottom: -30px !important;">
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
                    <img title="Grava o registro atual na base de dados." alt="Icone de Gravar o Registro."
                        src="/BarrasFerramentas/Icones/Gravar_Desabilitado.png" />
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 39px;">
                <li id="btnExcluir">
                    <img title="Exclui o Registro atual selecionado." alt="Icone de Excluir o Registro."
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                </li>
                <li id="btnCancelar">
                    <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                        alt="Icone de Cancelar Operacao Atual." src="/BarrasFerramentas/Icones/Cancelar_Desabilitado.png" />
                </li>
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
    <ul id="ulDados" class="ulDados" style="margin-top: -23px !important;">
        <li class="lblsub" style="margin-bottom: 1px">
            <label>
                Parâmetros</label>
        </li>
        <li style="clear: both">
            <label>
                Período</label>
            <asp:TextBox ID="txtIniPeri" Style="margin: 0px !important;" runat="server" CssClass="campoData"
                ToolTip="Data de início para pesquisa de Solicitacoes Médicas">
            </asp:TextBox>
            <asp:Label runat="server" ID="lbl"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" ID="txtFimPeri" CssClass="campoData" ToolTip="Data de término para pesquisa de Solicitacoes Médicas"></asp:TextBox>
        </li>
        <li>
            <label>
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnidade" Width="210px" ToolTip="Unidade para pesquisa de Solicitacoes Medicas"
                OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Tipo</label>
            <asp:DropDownList runat="server" ID="ddlTipo" Width="80px">
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="A" Text="Em Análise"></asp:ListItem>
                <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                <asp:ListItem Value="N" Text="Não"></asp:ListItem>
                <asp:ListItem Value="P" Text="Pendente"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Paciente</label>
            <asp:DropDownList runat="server" ID="ddlPaciente" Width="190px">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Profissional</label>
            <asp:DropDownList runat="server" ID="ddlProfissional" Width="190px">
            </asp:DropDownList>
        </li>
        <li style="margin-top: 13px">
            <asp:ImageButton ID="imgPesqGrid" Width="12px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                CssClass="btnPesqDescMed" ToolTip="Pesquisa as solicitacoes medicas de acordo com os parametros informados"
                OnClick="imgPesqGrid_OnClick" />
        </li>
        <li style="clear: both; margin: 0px 0 0 -5px !important;">
            <%--<div class="divPaciEnca" style="display:none; margin-left: 5px" ClientIDMode="static">--%>
            <div id="divPaciEnca" class="divAtendPendentes" runat="server" clientidmode="static"
                style="height: 190px">
                <div style="width: 100%; text-align: center; height: 17px; background-color: #add8e6;">
                    <div style="float: left;">
                        <asp:Label ID="Label1" runat="server" Style="font-size: 1.1em; font-family: Tahoma;
                            vertical-align: middle; margin-left: 4px !important;">
                                    GRADE DE SOLICITAÇÕES MÉDICAS</asp:Label>
                    </div>
                    <div style="float: right; margin: 1px 4px 0 3px;">
                        <asp:Label runat="server" ID="lblo">Ordenado por </asp:Label>
                        <asp:DropDownList runat="server" ID="ddlOrdeAtendPend" OnSelectedIndexChanged="ddlOrdeAtendPend_OnSelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="Class. Risco" Value="C" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Paciente" Value="P"></asp:ListItem>
                            <asp:ListItem Text="Unidade" Value="U"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
                </asp:Timer>
                <asp:UpdatePanel ID="updAtendPenden" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger EventName="Tick" ControlID="Timer1" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:HiddenField runat="server" ID="hidIdCentrRegul" />
                        <asp:HiddenField runat="server" ID="hidItemSelec" />
                        <asp:GridView ID="grdAtendimPendentes" CssClass="grdBusca" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="false" OnRowDataBound="grdAtendimPendentes_OnRowDataBound"
                            ToolTip="Grid de Atendimentos com encaminhamentos pendentes de aprovação">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum Atendimento Médico com Encaminhamento em aberto<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CK">
                                    <ItemStyle Width="15px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelecGridDetalhada" Style="margin: 0px !important;"
                                            OnCheckedChanged="chkSelecGridDetalhada_OnCheckedChanged" AutoPostBack="true" />
                                        <asp:HiddenField runat="server" ID="hidCoAtend" Value='<%# Eval("ID_ATEND_MEDIC") %>' />
                                        <asp:HiddenField runat="server" ID="hidCoCentrRegul" Value='<%# Eval("ID_CENTR_REGUL") %>' />
                                        <asp:HiddenField runat="server" ID="hidFlUso" Value='<%# Eval("FL_USO") %>' />
                                        <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UNID" HeaderText="UNID">
                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                    <HeaderStyle HorizontalAlign="Left" CssClass="aument" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="PACIENTE">
                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <ul>
                                            <li style="margin-top: 1px; clear: none; float: left;">
                                                <asp:Label runat="server" ID="lblg1noPac" Text='<%# Eval("NO_PAC_V") %>'></asp:Label>
                                            </li>
                                            <li style="float: right; margin: 0px;">
                                                <asp:ImageButton runat="server" ID="imgInfosCadastrais" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_InformacoesUsuario.png"
                                                    ToolTip="Exibe informações cadastrais do paciente" Width="13px" Height="14px"
                                                    OnClick="imgInfosCadastrais_OnClick" />
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NO_OPERA" HeaderText="OPERAD">
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CO_SEXO" HeaderText="SX">
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NU_IDADE" HeaderText="ID">
                                    <ItemStyle HorizontalAlign="Center" Width="17px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="CLASSIF. DE RISCO">
                                    <ItemStyle Width="103px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <ul>
                                            <li>
                                                <div id="Div1" runat="server" visible='<%# Eval("DIV_1") %>' style="background-color: Red;
                                                    width: 10px; height: 10px; margin: 0px;" title="Atendimento classificado como Emergência">
                                                </div>
                                                <div id="Div2" runat="server" visible='<%# Eval("DIV_2") %>' style="background-color: Orange;
                                                    width: 10px; height: 10px; margin: 0px" title="Atendimento classificado como Muito Urgente">
                                                </div>
                                                <div id="Div3" runat="server" visible='<%# Eval("DIV_3") %>' style="background-color: Yellow;
                                                    width: 10px; height: 10px; margin: 0px" title="Atendimento classificado como Urgente">
                                                </div>
                                                <div id="Div4" runat="server" visible='<%# Eval("DIV_4") %>' style="background-color: Green;
                                                    width: 10px; height: 10px; margin: 0px" title="Atendimento classificado como Pouco Urgente">
                                                </div>
                                                <div id="Div5" runat="server" visible='<%# Eval("DIV_5") %>' style="background-color: Blue;
                                                    width: 10px; height: 10px; margin: 0px" title="Atendimento classificado como Não Urgente">
                                                </div>
                                            </li>
                                            <li style="margin-top: -1px; clear: none;">
                                                <asp:Label runat="server" ID="lblg1" Text='<%# Eval("CLASS_RISCO") %>'></asp:Label>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CO_ATEND_V" HeaderText="Nº ATENDIMENTO">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DT_ATEND" HeaderText="DT/HR ATEND">
                                    <ItemStyle HorizontalAlign="Center" Width="72px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_COL" HeaderText="MÉDICO SOLICITANTE">
                                    <ItemStyle HorizontalAlign="Left" Width="193px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="EXA">
                                    <ItemStyle Width="20px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label Text="NÃO" Visible='<%# bind("SW_NAO_PRETO_EX") %>' ID="lblg1e1" runat="server"></asp:Label>
                                        <asp:Label ID="lblg1e2" Text="SIM" Visible='<%# bind("SW_SIM_PRETO_EX") %>' runat="server"></asp:Label>
                                        <asp:Label ID="lblg1e3" Style="color: Blue" Text="SIM" Visible='<%# bind("SW_SIM_AZUL_EX") %>'
                                            runat="server"></asp:Label>
                                        <asp:Label ID="lblg1e4" Style="color: Red" Text="SIM" Visible='<%# bind("SW_SIM_VERMELHO_EX") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="SVA">
                                    <ItemStyle Width="20px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblg1s1" Text="NÃO" Visible='<%# bind("SW_NAO_PRETO_SA") %>' runat="server"></asp:Label>
                                        <asp:Label ID="lblg1s2" Text="SIM" Visible='<%# bind("SW_SIM_PRETO_SA") %>' runat="server"></asp:Label>
                                        <asp:Label ID="lblg1s3" Style="color: Blue" Text="SIM" Visible='<%# bind("SW_SIM_AZUL_SA") %>'
                                            runat="server"></asp:Label>
                                        <asp:Label ID="lblg1s4" Style="color: Red" Text="SIM" Visible='<%# bind("SW_SIM_VERMELHO_SA") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RME">
                                    <ItemStyle Width="20px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblg1s12" Text="NÃO" Visible="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="EME">
                                    <ItemStyle Width="20px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblg1s13" Text="NÃO" Visible="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="EIN">
                                    <ItemStyle Width="20px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblg1s14" Text="NÃO" Visible="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="AÇÃO">
                                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlAcao" Width="70px" OnSelectedIndexChanged="ddlAcao_OnSelectedIndexChange"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                <asp:ListItem Value="EX" Text="Exame"></asp:ListItem>
                                                <asp:ListItem Value="SA" Text="Serviços Ambulatoriais"></asp:ListItem>
                                                <asp:ListItem Value="RM" Text="Reserva de Medicamentos"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </li>
        <asp:UpdatePanel runat="server" ID="updItensPendentes" UpdateMode="Always">
            <ContentTemplate>
                <li style="clear: both; margin: -7px 0 0 -5px;" runat="server" id="liItensPendentes"
                    visible="false">
                    <div class="divDetalhePendencia" id="divConsul" clientidmode="Static">
                        <div style="margin-bottom: 1px; text-align: center; background-color: #8FBC8F; height: 17px;">
                            <div style="float: left; margin-left: 4px;">
                                <asp:Label runat="server" ID="lblTituSegunGrid" Style="font-size: 1.1em; font-family: Tahoma;
                                    vertical-align: middle;">RELAÇÃO DE ITENS SOLICITADOS AO USUÁRIO DE SAÚDE</asp:Label>
                            </div>
                            <div style="float: right; margin: 1px 4px 0 3px;">
                                <asp:Label runat="server" ID="Label9" Text="Tipo"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlTipoItem" OnSelectedIndexChanged="ddlTipoItem_OnSelectedIndexChanged"
                                    AutoPostBack="true" Style="margin-right: 8px;">
                                    <asp:ListItem Text="Todos" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Exames" Value="EX"></asp:ListItem>
                                    <asp:ListItem Text="Serviços Ambulatoriais" Value="SA"></asp:ListItem>
                                    <asp:ListItem Text="Reserva de Medicamentos" Value="RM"></asp:ListItem>
                                    <asp:ListItem Text="Encaminhamentos Médicos" Value="EM"></asp:ListItem>
                                    <asp:ListItem Text="Encaminhamentos Internação" Value="EI"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label runat="server" ID="Label8" Text="Ordenado por"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlOrdDetalhePendencia" OnSelectedIndexChanged="ddlOrdDetalhePendencia_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="Registro" Value="R" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Código" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="Nome" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Tipo" Value="T"></asp:ListItem>
                                    <asp:ListItem Text="Situação" Value="S"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:GridView ID="grdDetalhePendencia" CssClass="grdBusca" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="false" ToolTip="Grade de Itens solicitados ao Usuário de Saúde"
                            OnRowDataBound="grdDetalhePendencia_OnRowDataBound">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum Item pendente de Aprovação<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="NO_TIPO_ITEM" HeaderText="TIPO">
                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CO_CAD_ITEM" HeaderText="CÓDIGO ">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_ITEM" HeaderText="NOME DO PROCEDIMENTO">
                                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CO_PROTOCOLO" HeaderText="Nº REGISTRO">
                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DATA E HORA       SITUAÇÃO">
                                    <ItemStyle Width="150px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataApr" runat="server" Text='<%# bind("DT_ALTER_V") %>' ToolTip="Data de registro da situação"></asp:Label>
                                        <asp:Label ID="Label1" Style="color: Red" Text="NÃO APROVADO" Visible='<%# bind("SW_NAO_VERMELHO") %>'
                                            runat="server" ToolTip="Não Autorizado"></asp:Label>
                                        <asp:Label ID="Label6" Style="color: Blue" Text="APROVADO" Visible='<%# bind("SW_SIM_VERDE") %>'
                                            runat="server" ToolTip="Autorizado"></asp:Label>
                                        <asp:Label ID="Label7" Style="color: Green" Text="EM ANÁLISE" Visible='<%# bind("SW_ANALISE_AZUL") %>'
                                            runat="server" ToolTip="Em Análise"></asp:Label>
                                        <asp:Label ID="Label10" Style="color: #FF7619" Text="PENDENTE" Visible='<%# bind("SW_PENDENTE_ABOBORA") %>'
                                            runat="server" ToolTip="Pendente"></asp:Label>
                                        <asp:Label ID="Label2" Text="CANCELADO" Visible='<%# bind("SW_CANCELADO_PRETO") %>'
                                            runat="server" ToolTip="Cancelado"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OBSERVAÇÃO / OCORRÊNCIAS ANÁLISE">
                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidCoStatus" Value='<%# bind("CO_STATUS") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdCentrRegu" Value='<%# bind("ID_CENTR_REGUL") %>' />
                                        <asp:HiddenField runat="server" ID="hidNoItem" Value='<%# bind("NO_ITEM") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdItemCentrRegul" Value='<%# bind("ID_ITEM_CENTR_REGUL") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdItem" Value='<%# bind("ID_ITEM") %>' />
                                        <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Width="150px" Height="100%"
                                            MaxLength="200" ToolTip="Observação sobre a alteração do status" Text='<%# bind("DE_OBS") %>'
                                            ReadOnly="true"></asp:TextBox>
                                        <asp:ImageButton runat="server" ID="imgHistOcorrencias" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_Ocorrencias.png"
                                            ToolTip="Histórico de Ocorrência do item selecionado" Width="25px" Height="20px"
                                            OnClick="imgHistOcorrencias_OnClick" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nº DA APROV">
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtNuAprovacao" Width="60px" Style="margin-top: 8px;"
                                            Enabled="false" ToolTip="Número de aprovação do Item" Text='<%# bind("NU_APROV") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nº DA GUIA">
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtNuGuia" Width="60px" Style="margin-top: 8px;"
                                            Enabled="false" ToolTip="Número da Guia do Item" Text='<%# bind("NU_GUIA") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IMP">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgImpGuia" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_EmissaoGuia.png"
                                            ToolTip="Impressão da Guia autorizada" Width="20px" Height="20px" OnClick="imgImpGuia_OnClick" />
                                        <asp:ImageButton runat="server" ID="imgImpGuiaDesabilitada" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_EmissaoGuia.png"
                                            ToolTip="Impressão da Guia autorizada(AINDA NÃO AUTORIZADA)" Width="20px" Height="20px"
                                            Style="cursor: default;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="float: left; margin: -7px 0 0 5px;">
                        <label style="font-size: xx-small">
                            Legenda: EXA (Exame) - SRV (Serv. Ambulatoriais) - RME (Reser. Medicamentos) - EME
                            (Encam. Médico) - EIN (Encam. Internação)</label>
                    </div>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
    <asp:UpdatePanel runat="server" ID="updModalHistoOcorr" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divLoadHistoOcorrencias" style="display: none; height: 310px !important;">
                <div id="divListarResponsaveisContainer">
                    <div id="frmListarResponsaveis" runat="server">
                        <div id="divListarResponsaveisContent">
                            <asp:GridView ID="grdHistOcorrencias" runat="server" Style="width: 100%;" AutoGenerateColumns="false"
                                ToolTip="Grade de registro de Ocorrências de itens de saúde">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhuma ocorrência registrada para o item selecionado<br />
                                </EmptyDataTemplate>
                                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />
                                <AlternatingRowStyle CssClass="alternateRowStyleLR" Height="15" />
                                <RowStyle CssClass="rowStyleLR" Height="15" />
                                <Columns>
                                    <asp:BoundField DataField="DT_REGIS" HeaderText="DT/HR REGISTRO">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_COL" HeaderText="REGISTRANTE">
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DE_OCORR" HeaderText="OCORRÊNCIA">
                                        <ItemStyle HorizontalAlign="Left" Width="380px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div id="divHelpTxtLR">
                            <p id="pAcesso" class="pAcesso">
                                Verifique as ocorrências existentes no quadro acima para iniciar os trabalhos.</p>
                            <p id="pFechar" class="pFechar">
                                Clique no X para fechar a janela.</p>
                        </div>
                        <div id="divRodapeLR">
                            <img id="imgLogoGestorLR" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="updModalContatos" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divLoadContatos" style="display: none; height: 385px !important;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="updModalInfosCadas" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divLoadInfosCadas" style="display: none; height: 385px !important;">
                <ul class="ulDados" style="margin-top: -10px !important;">
                    <li style="clear: both">
                        <label class="lblTitInf">
                            DADOS PACIENTE</label></li>
                    <li class="liFoto" style="clear: both">
                        <ul>
                            <li class="liClear" style="margin-top: 5px; margin-bottom: 5px;">
                                <fieldset class="fldFotoAluno">
                                    <uc1:ControleImagem ID="upImagemAluno" runat="server" />
                                </fieldset>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <label title="Número do NIS">
                            NIS</label>
                        <asp:TextBox ID="txtNisAluMod" Width="60px" runat="server" ToolTip="Número do NIS"
                            Enabled="false"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Nome do(a) Paciente">
                            Nome</label>
                        <asp:TextBox ID="txtNomeAluMod" runat="server" Width="253px" ToolTip="Nome do(a) Paciente"
                            Enabled="false"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Sexo do Aluno">
                            Sexo</label>
                        <asp:DropDownList ID="ddlSexoAluMod" Width="60px" runat="server" ToolTip="Sexo do Usuario"
                            Enabled="false">
                            <asp:ListItem Value="M">Mas</asp:ListItem>
                            <asp:ListItem Value="F">Fem</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="Data de Nascimento">
                            Nascimento</label>
                        <asp:TextBox ID="txtDataNascimentoAluMod" Width="53px" runat="server" ToolTip="Data de Nascimento"
                            Enabled="false"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Número da Identidade do(a) Paciente">
                            Número RG</label>
                        <asp:TextBox ID="txtRgAluMod" ToolTip="Número da Identidade do(a) Paciente" runat="server"
                            Enabled="false" Width="75px"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtOrgaoEmissorAlu" title="Órgão Emissor da Identidade">
                            Emissor</label>
                        <asp:TextBox ID="txtOrgaoEmissorAluMod" CssClass="txtOrgaoEmissorAlu" ToolTip="Órgão Emissor da Identidade"
                            runat="server" Enabled="false" Width="63px"></asp:TextBox>
                    </li>
                    <li>
                        <label for="ddlUfRgAlu" title="UF do Órgão Emissor da Identidade">
                            UF</label>
                        <asp:DropDownList ID="ddlUfRgAluMod" ToolTip="UF do Órgão Emissor da Identidade"
                            runat="server" Enabled="false" Width="45px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="CPF do(a) Paciente">
                            CPF</label>
                        <asp:TextBox ID="txtCpfAluMod" ToolTip="CPF do(a) Paciente" runat="server" CssClass="campoCpf"
                            Enabled="false"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Etnia do(a) Paciente">
                            Etnia</label>
                        <asp:DropDownList ID="ddlEtniaAluMod" Width="70px" runat="server" Enabled="false"
                            ToolTip="Etnia do(a) Paciente">
                            <asp:ListItem Value="B">Branca</asp:ListItem>
                            <asp:ListItem Value="N">Negra</asp:ListItem>
                            <asp:ListItem Value="A">Amarela</asp:ListItem>
                            <asp:ListItem Value="P">Parda</asp:ListItem>
                            <asp:ListItem Value="I">Indígena</asp:ListItem>
                            <asp:ListItem Value="X" Selected="true">Não Informada</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="E-mail do(a) Paciente">
                            Email</label>
                        <asp:TextBox ID="txtEmailAluMod" Width="160px" ToolTip="E-mail do(a) Paciente" runat="server"
                            Enabled="false"></asp:TextBox>
                    </li>
                    <li style="clear: both">
                        <label class="lblTitInf">
                            Endereço Residencial</label></li>
                    <li style="clear: both;">
                        <label title="CEP do(a) Paciente">
                            CEP</label>
                        <asp:TextBox ID="txtCepAluMod" CssClass="campoCEP" ToolTip="CEP do(a) Paciente" runat="server"
                            Enabled="false" Width="54px"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Logradouro da Residência do(a) Paciente">
                            Logradouro</label>
                        <asp:TextBox ID="txtLogradouroAluMod" ToolTip="Logradouro da Residência do(a) Paciente"
                            runat="server" Enabled="false" Width="237px"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Número da Residência do(a) Paciente">
                            Número</label>
                        <asp:TextBox ID="txtNumeroAluMod" ToolTip="Informe o Número da Residência do(a) Paciente"
                            runat="server" Enabled="false" Width="50px"></asp:TextBox>
                    </li>
                    <li>
                        <label title="UF do endereço do(a) Paciente">
                            UF</label>
                        <asp:DropDownList ID="ddlUfAluMod" runat="server" Enabled="false" ToolTip="UF do endereço do(a) Paciente"
                            Width="45px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="Complemento do endereço do(a) Paciente">
                            Complemento</label>
                        <asp:TextBox ID="txtComplementoAluMod" Width="200px" ToolTip="Complemento do endereço do(a) Paciente"
                            runat="server" Enabled="false"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Bairro do endereço do(a) Paciente">
                            Bairro</label>
                        <asp:DropDownList ID="ddlBairroAluMod" ToolTip="Bairro do endereço do(a) Paciente"
                            runat="server" Enabled="false" Width="105px">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="Cidade do endereço do(a) Paciente">
                            Cidade</label>
                        <asp:DropDownList ID="ddlCidadeAluMod" ToolTip="Cidade do endereço do(a) Paciente"
                            runat="server" Enabled="false" Width="90px">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both">
                        <label class="lblTitInf">
                            Plano de Saúde/Convênio</label></li>
                    <li style="clear: both">
                        <label title="Tipo plano de saúde do(a) Paciente">
                            Tipo</label>
                        <asp:DropDownList ID="ddlTipoBolsaMod" Width="60px" ToolTip="Tipo plano de saúde do(a) Paciente"
                            runat="server" Enabled="false">
                            <asp:ListItem Value="N" Selected="True">Nenhuma</asp:ListItem>
                            <asp:ListItem Value="B"> Plano de Saúde</asp:ListItem>
                            <asp:ListItem Value="C">Convênio</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="Nome da Bolsa do(a) Paciente">
                            Nome do Plano de Saúde</label>
                        <asp:DropDownList ID="ddlBolsaAluMod" ToolTip="Nome da Bolsa do(a) Paciente" runat="server"
                            Width="115" Enabled="false">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label title="Valor Desconto do(a) Paciente">
                            Valor</label>
                        <asp:TextBox ID="txtValorDesctoMod" ToolTip="Valor Desconto do(a) Paciente" runat="server"
                            Enabled="False" Width="36px"></asp:TextBox>
                    </li>
                    <li style="margin-top: 13px; margin-left: -3px;">
                        <asp:CheckBox CssClass="chkLocais" ID="chkDesctoPercBolsaMod" TextAlign="Right" Enabled="false"
                            runat="server" ToolTip="% de Desconto da Bolsa?" Text="%" />
                    </li>
                    <li>
                        <label title="Período de Duração da Bolsa do(a) Paciente">
                            Período</label>
                        <asp:TextBox ID="txtPeriodoDeAluMod" ToolTip="Período de Duração da Bolsa do(a) Paciente"
                            runat="server" Width="53px" Enabled="False"></asp:TextBox>
                        <asp:Label runat="server" ID="Label3"> &nbsp à &nbsp </asp:Label>
                        <asp:TextBox ID="txtPeriodoAteAluMod" ToolTip="Informe a Data de Término da Bolsa"
                            runat="server" Width="53px" Enabled="False"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        $(document).ready(function () {
            JavscriptAtend();
        });

        //Inserida função apra abertura de nova janela popup com a url do relatório que apresenta as guias
        function customOpen(url) {
            var w = window.open(url);
            w.focus();
        }

        function JavscriptAtend() {
            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");
            $(".campoCpf").mask("999.999.999-99");
            $(".campoCEP").mask("99999-999");
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            JavscriptAtend();
        });

        function AbreModalHistOcorr() {
            $('#divLoadHistoOcorrencias').dialog({ autoopen: false, modal: true, width: 700, height: 390, resizable: false, title: "CENTRAL DE REGULAÇÃO - HISTÓRICO DE OCORRÊNCIAS",
                open: function () { $('#divLoadHistoOcorrencias').show(); }
            });
        }

        function AbreModalInfosCadas() {
            $('#divLoadInfosCadas').dialog({ autoopen: false, modal: true, width: 432, height: 390, resizable: false, title: "USUÁRIO DE SAÚDE - INFORMAÇÕES CADASTRAIS",
                open: function () { $('#divLoadInfosCadas').show(); }
            });
        }
    </script>
</asp:Content>
