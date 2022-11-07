<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8230_AvalTecnicaPlanejamento._8231_AvalTecnicaPlanejCompl.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1120px;
        }
        .ulDados li
        {
            margin-left: 5px;
            margin-bottom: 5px;
        }
        .ulPer label
        {
            text-align: left;
        }
        label
        {
            margin-bottom: 1px;
        }
        input
        {
            height: 13px !important;
        }
        .ulDadosGerais li
        {
            margin-left: 5px;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            padding: 2px 3px 1px 3px;
        }
        .chk label
        {
            display: inline;
            margin-left: -6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <ul class="divEncamMedicoGeral">
                <li style="clear: both; margin-left: 3px;">
                    <ul>
                        <li class="liTituloGrid" style="width: 975px; height: 20px !important; margin-right: 0px;
                            background-color: #FFEC8B; text-align: center; font-weight: bold; margin-bottom: 5px;
                            padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: none">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        GRID DE PACIENTES COM AVALIAÇÃO PRÉVIA PARA PLANEJAMENTO DE AÇÕES</label>
                                </li>
                                <li style="margin-left: 23px; float: right; margin-top: 2px;"></li>
                            </ul>
                        </li>
                        <li style="clear: both">
                            <div style="width: 380px; height: 95px; border: 1px solid #CCC; overflow-y: scroll" id="divPacien">
                              <input type="hidden" id="divPacien_posicao" name="divPacien_posicao" />
                                <asp:HiddenField runat="server" ID="hidIdItem" />
                                <asp:GridView ID="grdPacientes" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma solicitação em Aberto<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidIdAgendaAval" Value='<%# Eval("ID_AGEND_AVALI") %>' runat="server" />
                                                <asp:CheckBox ID="chkSelectPaciente" runat="server" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PACIENTE">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblNomPaci" Text='<%# Eval("PACIENTE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NU_REGIS" HeaderText="Nº REGISTRO">
                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SX" HeaderText="SX">
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IDADE" HeaderText="IDADE">
                                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="INF">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgInfAgenda" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                                    ToolTip="Apresenta informações do agendamento/recepção de avaliação" Style="width: 13px !important;
                                                    height: 13px !important; margin: 0 0 0 0 !important" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ST">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgSituacao" ImageUrl="/Library/IMG/PGS_PacienteNaoChegou.ico"
                                                    ToolTip="Status do agendamento de avaliação" Style="width: 18px !important; height: 18px !important;
                                                    margin: 0 0 0 0 !important" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                        <li style="margin-left: 3px;">
                            <div style="width: 583px; height: 95px; border: 1px solid #CCC; overflow-y: scroll" id="divProced" >
                             <input type="hidden" id="divProced_posicao" name="divProced_posicao" />
                                <asp:HiddenField runat="server" ID="HiddenField1" />
                                <asp:GridView ID="grdItensRecep" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma solicitação para esta recepção<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="PROCEDIMENTO" HeaderText="PROCEDIMENTO">
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QTDE_V" HeaderText="QAP">
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NU_REGIS" HeaderText="Nº CONTROLE">
                                            <ItemStyle HorizontalAlign="Center" Width="64px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="PLANO">
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidIdOper" Value='<%# Eval("ID_OPER") %>' />
                                                <asp:HiddenField runat="server" ID="hidIdPlano" Value='<%# Eval("ID_PLAN") %>' />
                                                <asp:HiddenField runat="server" ID="hidIdCateg" Value='<%# Eval("ID_CATEG_PLANO_SAUDE") %>' />
                                                <asp:HiddenField runat="server" ID="hidNuPlano" Value='<%# Eval("NU_PLAN_SAUDE") %>' />
                                                <asp:HiddenField runat="server" ID="hidNuGuia" Value='<%# Eval("NU_GUIA") %>' />
                                                <asp:HiddenField runat="server" ID="hidNuAutor" Value='<%# Eval("NU_AUTOR") %>' />
                                                <asp:ImageButton runat="server" ID="imgInfosPlano" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_Ocorrencias.png"
                                                    ToolTip="Informações do Plano de Saúde" Style="margin: 0 0 0 0 !important" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OBSER">
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgInfoObser" ImageUrl="/Library/IMG/Gestor_Serviços.png"
                                                    ToolTip="Informações do Plano de Saúde" Style="margin: 0 0 0 0 !important; width: 16px !important;
                                                    height: 16px !important;" />
                                                <asp:TextBox runat="server" ID="txtObservacao" Style="margin: 2px 0px 2px 0px !important;
                                                    width: 70px; height: 26px;" TextMode="MultiLine" Visible="false" Text='<%# Eval("OBSERVACAO") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="STATUS" HeaderText="STATUS">
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
                <li style="text-align: center; margin: 2px 0 0 3px">
                    <ul style="width: 990px">
                        <li style="width: 435px">
                            <ul style="float: left;">
                                <li>
                                    <ul style="width: 435px">
                                        <li class="liTituloGrid" style="width: 370px; height: 20px !important; margin-left: -5px;
                                            background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                                            <ul>
                                                <li style="margin: 0 0 0 10px; float: left">
                                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                        QUESTIONÁRIOS AVALIATIVOS</label>
                                                </li>
                                            </ul>
                                        </li>
                                        <li id="li3" runat="server" title="Clique para Adicionar as Questões da Avaliação"
                                            class="liBtnAddA" style="float: right; margin: -25px -2px 3px 5px; width: 60px">
                                            <img alt="" class="imgAdd" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                                                height="15px" width="15px" />
                                            <asp:LinkButton ID="btnAddForm" runat="server" class="btnLabel">Adicionar</asp:LinkButton>
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: both; margin: -7px 0 0 0;">
                                    <div style="width: 440px; height: 100px; border: 1px solid #CCC; overflow-y: scroll">
                                        <asp:GridView ID="grdQuestionario" CssClass="grdBusca" runat="server" Style="width: 100%;
                                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhum Questionário associado<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="QUESTIONÁRIO">
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlQuest">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EX">
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hidIdAvaliacao" Value='<%# Eval("ID_AVALIACAO") %>' runat="server" />
                                                        <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                            ToolTip="Excluir Questionário" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </li>
                            </ul>
                        </li>
                        <li style="margin-left: 26px;">
                            <ul>
                                <li class="liTituloGrid" style="width: 242px; height: 20px !important; background-color: #EEEEE0;
                                    text-align: center; font-weight: bold; float: left">
                                    <ul>
                                        <li style="margin: 0 0 0 10px; float: none">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                CONSIDERAÇÕES AVALIADOR</label>
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: both; margin-top: -2px;">
                                    <asp:TextBox runat="server" ID="txtConsidAvaliador" Style="width: 240px; height: 100px !important;"
                                        TextMode="MultiLine"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <ul style="margin-right: -5px; clear: none">
                                <li class="liTituloGrid" style="width: 242px; height: 20px !important; background-color: #EEEEE0;
                                    text-align: center; font-weight: bold; float: left">
                                    <ul>
                                        <li style="margin: 0 0 0 10px; float: none">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                CONSIDERAÇÕES PLANEJADOR</label>
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: both; margin-top: -2px;">
                                    <asp:TextBox runat="server" ID="TextBox1" Style="width: 240px; height: 100px !important;"
                                        TextMode="MultiLine"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li style="width: 975px; clear: both; margin-left: 13px; margin-top: -5px;">
            <ul style="float: left; width: 600px">
                <li>
                    <ul style="width: 670px">
                        <li class="liTituloGrid" style="width: 580px; height: 20px !important; margin-left: -5px;
                            background-color: #fa8072; text-align: center; font-weight: bold; float: left">
                            <ul>
                                <li style="margin: 0 0 0 10px; float: none">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 4px;">
                                        PLANEJAMENTO DAS AÇÕES POR PROCEDIMENTO</label>
                                </li>
                            </ul>
                        </li>
                        <li style="float: right; margin-top: -22px;">
                            <img alt="" class="imgAdd" title="Adicionar Questão" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                                height="15px" width="15px" />
                            <asp:LinkButton runat="server" ID="btnMaisSolicitacoes" Style="margin-left: 1px;"><span style="color:#FF7050">INSERIR MAIS</span></asp:LinkButton>
                        </li>
                    </ul>
                </li>
                <li style="clear: both; margin: -7px 0 0 0;">
                    <div style="width: 670px; height: 130px; border: 1px solid #CCC; overflow-y: scroll">
                        <asp:GridView ID="grdProfissionais" CssClass="grdBusca" runat="server" Style="width: 100%;
                            cursor: default; height: 30px !important;" AutoGenerateColumns="false" AllowPaging="false"
                            GridLines="Vertical">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhumm Profissional nesses parâmetros<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="CK">
                                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidCoCol" Value='<%# Eval("CO_COL") %>' runat="server" />
                                        <asp:HiddenField ID="hidCoEmp" Value='<%# Eval("CO_EMP") %>' runat="server" />
                                        <asp:CheckBox ID="chkselect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NO_COL_V" HeaderText="PROFISSIONAL">
                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SX" HeaderText="SX">
                                    <ItemStyle HorizontalAlign="Left" Width="15px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ID" HeaderText="ID">
                                    <ItemStyle HorizontalAlign="Left" Width="15px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NU_TEL_V" HeaderText="TELEFONE">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPEC">
                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLASS_PROFI" HeaderText="CLASS PROFI">
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QPE" HeaderText="QPE">
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TP_CONTR" HeaderText="TP CONTR">
                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_TPCAL" HeaderText="TP PGTO">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VL_SALAR_COL" HeaderText="R$ BASE">
                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
            <ul style="float: right; margin-right: -10px;">
                <li>
                    <ul style="width: 290px">
                        <li class="liTituloGrid" style="width: 290px; height: 20px !important; margin-left: -5px;
                            background-color: #c1ffc1; text-align: center; font-weight: bold; float: left">
                            <ul style="float: left">
                                <li style="margin: 0 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 4px;">
                                        AGENDA</label>
                                </li>
                            </ul>
                            <ul style="float: right; margin-top: 3px;">
                                <li>
                                    <asp:CheckBox runat="server" ID="chkDom" Text="D" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo os Domingos" />
                                </li>
                                <li style="margin-left: -5px;">
                                    <asp:CheckBox runat="server" ID="chkSeg" Text="S" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Segundas"
                                        Checked="true" />
                                </li>
                                <li style="margin-left: -5px;">
                                    <asp:CheckBox runat="server" ID="chkTer" Text="T" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Terças"
                                        Checked="true" />
                                </li>
                                <li style="margin-left: -5px;">
                                    <asp:CheckBox runat="server" ID="chkQua" Text="Q" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Quartas"
                                        Checked="true" />
                                </li>
                                <li style="margin-left: -5px;">
                                    <asp:CheckBox runat="server" ID="chkQui" Text="Q" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Quintas"
                                        Checked="true" />
                                </li>
                                <li style="margin-left: -5px;">
                                    <asp:CheckBox runat="server" ID="chkSex" Text="S" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Sextas"
                                        Checked="true" />
                                </li>
                                <li style="margin-left: -5px;">
                                    <asp:CheckBox runat="server" ID="chkSab" Text="S" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo os Sábados" />
                                </li>
                                <li style="margin-top: 0px; margin-left: 2px;">
                                    <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" />
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                          <input type="hidden" id="Hidden1" name="div_posicao" />
                            <div style="width: 288px; height: 130px; border: 1px solid #CCC; overflow-y: scroll">
                                <asp:GridView ID="grdAgenda" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum Questionário associado<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="QUESTIONÁRIO">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlQuest">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EX">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidIdAvaliacao" Value='<%# Eval("ID_AVALIACAO") %>' runat="server" />
                                                <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                    ToolTip="Excluir Questionário" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadShowInfosPaciente" style="display: none; height: 305px !important;" />
        </li>
        <li>
            <div id="divLoadShowInfosPlano" style="display: none; height: 305px !important;" />
        </li>
        <li>
            <div id="divLoadShowObservacao" style="display: none; height: 120px !important;">
                <asp:HiddenField runat="server" ID="HiddenField2" />
                <ul class="ulDados" style="width: 400px; margin-top: 0px !important">
                    <li>
                        <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Style="width: 400px;
                            height: 130px"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <script type="text/javascript">

        window.onload = function () {
            mantemScrollPac();
            mantemScrollProced();
            mantemScrollAgenda();
        }

        function mantemScrollPac() {
            var div = document.getElementById("divPacien");
            var div_position = document.getElementById("divPacien_posicao");
            var position = parseInt('<%= Request.Form["divPacien_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function mantemScrollProced() {
            var div = document.getElementById("divProced");
            var div_position = document.getElementById("divProced_posicao");
            var position = parseInt('<%= Request.Form["divProced_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        
    </script>
</asp:Content>
