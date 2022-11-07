<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento.Cadastro" %>

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
        .ulDadosLog li
        {
            float: left;
            margin-left: 10px;
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
        .liBtnConfirm
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        #divCronometro
        {
            text-align:center;
            background-color:#FFE1E1;
            float:left;
            margin-left:13px;
            margin-top:-48px;
            width:115px;
            margin-right:-130px;
            display:none;
        }
        .LabelHora
        {
            margin-top:4px;
            font-size:10px;
        }
        .Hora
        {
            font-family:Trebuchet MS;
            font-size:23px;
            color:#9C3535;
            margin-top:-3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidIdAtendimento" />
    <div id="divCronometro">
        <asp:HiddenField ID="hidTimerId" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="hidHoras" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="hidMinutos" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="hidSegundos" runat="server" Value="" ClientIDMode="Static" />
        <label class="LabelHora">Tempo de Atendimento</label>
        <label id="lblHora" class="Hora">00:00:00</label>
    </div>
    <ul class="ulDados">
        <li style="clear: both; margin-left: 3px;">
            <ul>
                <li class="liTituloGrid" style="width: 975px; height: 20px !important; margin-right: 0px;
                    background-color: #ADD8E6; text-align: center; font-weight: bold; margin-bottom: 2px;
                    padding-top: 2px;">
                    <ul>
                        <li style="margin: 0px 0 0 10px; float: left">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FFF">
                                GRID DE PACIENTES COM AGENDAMENTO</label>
                        </li>
                        <li style="margin-left: 18px; float: right; margin-top: 2px;">
                            <ul class="ulPer">
                                <li>
                                    <asp:TextBox runat="server" ID="txtNomePacPesqAgendAtend" Width="240px" placeholder="Pesquise pelo Nome do Paciente"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:TextBox runat="server" class="campoData" ID="IniPeriAG" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                                        ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeriAG"></asp:RequiredFieldValidator>
                                    <asp:Label runat="server" ID="Label4"> &nbsp à &nbsp </asp:Label>
                                    <asp:TextBox runat="server" class="campoData" ID="FimPeriAG" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                                        ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeriAG"></asp:RequiredFieldValidator><br />
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="Label3">
                                                Situação
                                    </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlSituacao" Width="110px">
                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Em Aberto" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Encaminhado" Value="E"></asp:ListItem>
                                        <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li style="margin: 0px 2px 0 -2px;">
                                    <asp:ImageButton ID="imgPesqAgendamentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        Width="13px" Height="13px" OnClick="imgPesqAgendamentos_OnClick" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li style="clear: both">
                    <div style="width: 583px; height: 95px; border: 1px solid #CCC; overflow-y: scroll" id="divAgendasRecp">
                    <input type="hidden" id="divAgendasRecp_posicao" name="divAgendasRecp_posicao" />
                        <asp:HiddenField runat="server" ID="hidIdAgenda" />
                        <asp:GridView ID="grdPacientes" CssClass="grdBusca" runat="server" Style="width: 100%;
                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                            ShowHeaderWhenEmpty="true">
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
                                        <asp:HiddenField ID="hidIdAgenda" Value='<%# Eval("ID_AGEND_HORAR") %>' runat="server" />
                                        <asp:HiddenField ID="hidSituacao" Value='<%# Eval("Situacao") %>' runat="server" />
                                        <asp:HiddenField ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' runat="server" />
                                        <asp:CheckBox ID="chkSelectPaciente" runat="server" OnCheckedChanged="chkSelectPaciente_OnCheckedChanged"
                                            AutoPostBack="true" Enabled='<%# Eval("podeClicar") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DTHR" HeaderText="HORÁRIO">
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="PACIENTE">
                                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNomPaci" Text='<%# Eval("PACIENTE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SX" HeaderText="SX">
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IDADE" HeaderText="IDADE">
                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="CNT">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Image ID="imgContratacao" ImageUrl='<%# Eval("tpContr_URL") %>' ToolTip='<%# Eval("tpContr_TIP") %>' Width="17px" Height="17px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ST">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgSituacao" ImageUrl='<%# Eval("imagem_URL") %>'
                                            ToolTip='<%# Eval("imagem_TIP") %>' Style="width: 18px !important; height: 18px !important;
                                            margin: 0 0 0 0 !important" OnClick="imgSituacao_OnClick" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <li style="margin-left: 3px;">
                    <div style="width: 380px; height: 95px; border: 1px solid #CCC; overflow-y: scroll">
                        <asp:HiddenField runat="server" ID="HiddenField1" />
                        <asp:GridView ID="grdItensAgend" CssClass="grdBusca" runat="server" Style="width: 100%;
                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                            ShowHeaderWhenEmpty="true">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma solicitação para esta recepção<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="NO_PROCED" HeaderText="PROCEDIMENTO">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
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
                                <asp:BoundField DataField="NO_PRIORI" HeaderText="PRIORI">
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
        <li style="clear: both">
            <ul style="float: left;">
                <li class="liTituloGrid" style="width: 662px; height: 20px !important; margin-right: 0px;
                    background-color: #c1ffc1; text-align: center; font-weight: bold; margin-bottom: 2px;
                    padding-top: 2px; margin-left: 3px">
                    <ul>
                        <li style="margin: 0px 0 0 10px; float: left">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                DEMONSTRATIVO DE AGENDA</label>
                        </li>
                        <li style="margin-left: 18px; float: right; margin-top: 2px;">
                            <ul class="ulPer">
                                <li>
                                    <asp:DropDownList runat="server" ID="ddlClassFuncio" Width="110px">
                                        <asp:ListItem Text="Class. Funcional" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <asp:DropDownList runat="server" ID="ddlSituaProced" Width="110px">
                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Agendado" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Encaminhado" Value="E"></asp:ListItem>
                                        <asp:ListItem Text="Falta" Value="F"></asp:ListItem>
                                        <asp:ListItem Text="Falta Justificada" Value="J"></asp:ListItem>
                                        <asp:ListItem Text="Presente" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <asp:TextBox runat="server" class="campoData" ID="txtIniAgenda" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                    <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
                                    <asp:TextBox runat="server" class="campoData" ID="txtFimAgenda" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                </li>
                                <li style="margin: 0px 2px 0 -2px;">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        Width="13px" Height="13px" />
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both; margin: -16px 0 0 0px;">
                            <div id="divDemonAge" style="width: 660px; height: 160px; border: 1px solid #CCC; overflow-y: scroll">
                            <input type="hidden" id="divDemonAge_posicao" name="divDemonAge_posicao" />
                                <asp:HiddenField runat="server" ID="HiddenField2" />
                                <asp:GridView ID="grdHistoricoAgenda" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                    ShowHeaderWhenEmpty="true">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum agendamento para este paciente nos parâmetros acima<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSelectHistAge" Enabled='<%# Eval("podeClicar") %>'
                                                    OnCheckedChanged="chkSelectHistAge_OnCheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ST">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgSituacaoHistorico" Style="width: 18px !important;
                                                    height: 18px !important; margin: 0 0 0 0 !important" OnClick="imgSituacaoHistorico_OnClick"
                                                    ImageUrl='<%# Eval("imagem_URL") %>' ToolTip='<%# Eval("imagem_TIP") %>' />
                                                <asp:HiddenField runat="server" ID="hidIdAgenda" Value='<%# Eval("ID_AGENDA") %>' />
                                                <asp:HiddenField runat="server" ID="hidSitAgenda" Value='<%# Eval("Situacao") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SA">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <img class="imgsa" style="width: 16px; height: 16px !important" src='<%# Eval("imagem_URL_ACAO") %>'
                                                    alt="Icone" title="Ação já realizada ou não" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtAgenda_V" HeaderText="AGENDA">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dtAgenda_Atend_V" HeaderText="REALIZADO">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CLASS_FUNCI" HeaderText="CLAS FUNC">
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DE_ACAO" HeaderText="AÇÃO DE ATENDIMENTO">
                                            <ItemStyle HorizontalAlign="Left" Width="280px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
            </ul>
            <ul style="float: right; margin-left: 8px;">
                <li class="liTituloGrid" style="width: 302px; height: 20px !important; margin-right: 0px;
                    background-color: #FFEC8B; text-align: center; font-weight: bold; margin-bottom: 2px;
                    padding-top: 2px; margin-left: 3px">
                    <ul>
                        <li style="margin: 0px 0 0 10px; float: left">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                REGISTRO DE ATENDIMENTO DO PACIENTE</label>
                        </li>
                    </ul>
                </li>
                <li style="clear: both">
                    <label>
                        Profissional Responsável</label>
                    <asp:DropDownList runat="server" ID="ddlProfiResp" Width="132px">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Previsão</label>
                    <asp:TextBox runat="server" ID="txtDtPrevisao" Enabled="false" CssClass="campoData"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Realizado</label>
                    <asp:TextBox runat="server" ID="txtDtRealizado" CssClass="campoData"></asp:TextBox>
                </li>
                <li style="clear: both; margin-top: -13px">
                    <label>
                        Ação Planejada</label>
                    <asp:TextBox runat="server" ID="txtAcaoPlan" Width="298px" Font-Size="11px"></asp:TextBox>
                </li>
                <li style="clear: both; margin-top: -13px">
                    <label>
                        Ação Realizada</label>
                    <asp:TextBox runat="server" ID="txtAcaoReali" Width="298px" Font-Size="11px"></asp:TextBox>
                </li>
                <li style="clear: both; margin-top: -13px">
                    <label>
                        Observação</label>
                    <asp:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Style="width: 298px;
                        height: 35px;" Font-Size="11px"></asp:TextBox>
                </li>
                <li style="clear: both; margin-left: 20px;">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Library/IMG/PGS_IC_Imagens.jpg"
                        Style="width: 18px; height: 18px !important" />
                    <asp:Label runat="server" ID="ll" Style="margin: 8px 0 0 5px; float: right">FOTOS</asp:Label>
                </li>
                <li class="lnkImagens" style="margin-top: 0px; cursor: pointer"><a class="lnkImagens"
                    href="#">
                    <img class="imgImagens" style="width: 18px; height: 18px !important" src="/Library/IMG/PGS_IC_Imagens2.png"
                        alt="Icone" />
                    <label style="margin: 8px 0 0 5px; float: right">
                        IMAGENS</label>
                </a></li>
                <li class="lnkAudios" style="margin-top: 0px; cursor: pointer"><a class="lnkAudios"
                    href="#">
                    <img class="imgAudios" style="width: 18px; height: 18px !important" src="/Library/IMG/PGS_IC_ArquivoAudio.png"
                        alt="Icone" />
                    <label style="margin: 8px 0 0 5px; float: right">
                        ÁUDIO</label>
                </a></li>
                <li style="">
                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Library/IMG/PGS_IC_Anexo.png"
                        Style="width: 18px; height: 18px !important" />
                    <asp:Label runat="server" ID="Label6" Style="margin: 8px 0 0 5px; float: right">ANEXOS</asp:Label>
                </li>
            </ul>
        </li>
        <li style="margin-top:-4px; margin-left: 6px;">
            <ul style="float: left; width: 1000px;">
                <li>
                    <ul>
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
                                    <asp:LinkButton ID="btnAddForm" runat="server" class="btnLabel" OnClick="btnAddForm_OnClick">Adicionar</asp:LinkButton>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both; margin: -7px 0 0 0;">
                            <div style="width: 440px; height: 80px; border: 1px solid #CCC; overflow-y: scroll">
                                <asp:GridView ID="grdQuestionario" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                    ShowHeaderWhenEmpty="true">
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
                                                <asp:HiddenField runat="server" ID="hidNuAval" Value='<%# Eval("QUESTIONARIO") %>' />
                                                <asp:DropDownList runat="server" ID="ddlQuest" Width="385px">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EX">
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                    ToolTip="Excluir Questionário" OnClick="imgExc_OnClick" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
                <li>
                    <ul>
                        <li>
                            <ul style="width: 422px">
                                <li class="liTituloGrid" style="width: 422px; height: 20px !important; margin-left: -5px;
                                    background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                                    <ul>
                                        <li style="margin: 0 0 0 10px; float: left">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                CONSIDERAÇÕES DO ATENDIMENTO AO PACIENTE</label>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both; margin: -7px 0 0 0;">
                            <asp:TextBox runat="server" ID="txtConsidAtendim" TextMode="MultiLine" Style="width: 420px;
                                height: 80px" Font-Size="13px"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="margin: 10px 0 0 0px;">
                    <ul>
                        <li id="li1" runat="server" title="Clique para finalizar o atendimento"
                            class="liBtnAddA" style="width: 80px; margin-left:-10px !important; text-align:center; background-color:#880000;">
                            <asp:LinkButton ID="lnkFinalizar" runat="server" OnClick="lnkFinalizar_OnClick" ForeColor="White">FINALIZAR</asp:LinkButton>
                        </li>
                        <li id="li2" runat="server" title="Clique para emitir a ficha de atendimento"
                            class="liBtnAddA" style="clear: both; width: 80px; margin-left: -10px !important; margin-top:2px !important; background-color: #FF9933;
                            text-align: center">
                            <asp:LinkButton ID="lnkFicha" runat="server" OnClick="lnkFicha_OnClick" ForeColor="White">FICHA ATEND.</asp:LinkButton>
                        </li>
                        <li id="li5" runat="server" title="Clique para emitir a guia do plano"
                            class="liBtnAddA" style="clear: both; width: 80px; margin-left: -10px !important; margin-top:-1px !important; background-color: #FF9933;
                            text-align: center">
                            <asp:LinkButton ID="lnkGuia" runat="server" OnClick="lnkGuia_OnClick" ForeColor="White">GUIA PLANO</asp:LinkButton>
                        </li>
                        <li id="li4" runat="server" title="Clique para emitir um atestado ou uma declaração de atendimento"
                            class="liBtnAddA" style="clear: both; width: 80px; margin-left: -10px !important; margin-top:0px !important; background-color:#5858FA;
                            text-align: center">
                            <asp:LinkButton ID="lnkAtestado" runat="server" OnClick="lnkAtestado_OnClick" ForeColor="White">DECL. COMP.</asp:LinkButton>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadShowLogAgenda" style="display: none; height: 340px !important;">
                <ul class="ulDadosLog">
                    <li>
                        <label>
                            Paciente</label>
                        <asp:TextBox runat="server" ID="txtNomePaciMODLOG" Enabled="false" Width="220px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Sexo</label>
                        <asp:TextBox runat="server" ID="txtSexoMODLOG" Enabled="false" Width="10px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Idade</label>
                        <asp:TextBox runat="server" ID="txtIdadeMODLOG" Enabled="false" Width="70px"></asp:TextBox>
                    </li>
                    <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                        <div style="width: 890px; height: 305px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdLogAgendamento" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Questionário associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Data_V" HeaderText="DATA">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_PROFI" HeaderText="RESPONSÁVEL">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_TIPO" HeaderText="TIPO">
                                        <ItemStyle HorizontalAlign="Left" Width="65px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemStyle Width="18px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <img src='<%# Eval("CAMINHO_IMAGEM") %>' alt="" style="width: 16px !important; height: 16px !important;
                                                margin: 0 0 0 0 !important" title="Representação gráfica da Ação" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DE_TIPO" HeaderText="AÇÃO">
                                        <ItemStyle HorizontalAlign="Left" Width="240px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                height: 23px !important; width: 180px" ReadOnly="true" Text='<%# Eval("OBS") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divFichaAtendimento" style="display: none; height: 220px !important;">
                <ul class="ulDados" style="width: 420px; margin-top: 0px !important">
                    <li>
                        <label title="Paciente">Paciente</label>
                        <asp:DropDownList ID="drpPacienteFicha" runat="server" Width="240px" />
                    </li>
                    <li>
                        <label>Queixas</label>
                        <asp:TextBox runat="server" ID="txtQxsFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                    </li>
                    <li>
                        <label>Observação</label>
                        <asp:TextBox runat="server" ID="txtObsFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                    </li>
                    <li id="li6" runat="server" class="liBtnAddA" style="float: right; margin-top: 10px !important;
                        clear: none !important; height: 15px;">
                        <asp:LinkButton ID="lnkbImprimirFicha" runat="server" OnClick="lnkbImprimirFicha_Click" ToolTip="Imprimir ficha de atendimento">
                            <asp:Label runat="server" ID="lblEmitirFicha" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divGuiaPlano" style="display: none; height: 150px !important;">
                <ul class="ulDados" style="width: 430px; margin-top: 0px !important">
                    <li>
                        <label title="Data Comparecimento" class="lblObrigatorio">Data</label>
                        <asp:TextBox ID="txtDtGuia" runat="server" ValidationGroup="guia" class="campoData" ToolTip="Informe a data de comparecimento"
                            AutoPostBack="true" OnTextChanged="txtDtGuia_OnTextChanged"/>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="guia" ID="rfvDtGuia" CssClass="validatorField"
                            ErrorMessage="O campo data é requerido" ControlToValidate="txtDtGuia"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label title="Paciente">Paciente</label>
                        <asp:DropDownList ID="drpPacienteGuia" runat="server" Width="240px" />
                    </li>
                    <li>
                        <label title="Operadora">Operadora</label>
                        <asp:DropDownList ID="drpOperGuia" runat="server" Width="80px" />
                    </li>
                    <li style="clear: both;">
                        <label title="Observações">Observações / Justificativa</label>
                        <asp:TextBox ID="txtObsGuia" Width="410px" Height="40px" TextMode="MultiLine" MaxLength="180" runat="server" />
                    </li>
                    <li class="liBtnAddA" style="clear: none !important; margin-left: 180px !important; margin-top: 8px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbImprimirGuia" runat="server" ValidationGroup="guia" OnClick="lnkbImprimirGuia_OnClick" ToolTip="Imprimir guia do plano de saúde">
                            <asp:Label runat="server" ID="lblEmitirGuia" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divAtestado" style="display: none; width:500px; height: 340px !important;">
                <ul class="ulDados" style="float:left; width:500px; margin-top: 0px !important;">
                    <li>
                        <label title="Data Comparecimento" class="lblObrigatorio">Data</label>
                        <asp:TextBox ID="txtDtAtestado" runat="server" ValidationGroup="atestado" class="campoData" ToolTip="Informe a data de comparecimento"
                            AutoPostBack="true" OnTextChanged="txtDtAtestado_OnTextChanged"/>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="atestado" ID="rfvDtAtestado" CssClass="validatorField"
                            ErrorMessage="O campo data é requerido" ControlToValidate="txtDtAtestado"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label title="Período Comparecimento">Período</label>
                        <asp:DropDownList ID="drpPrdComparecimento" runat="server" style="vertical-align:top;" ToolTip="Período de comparecimento">
                            <asp:ListItem Value="M" Text="Manhã" />
                            <asp:ListItem Value="T" Text="Tarde" />
                            <asp:ListItem Value="N" Text="Noite" />
                            <asp:ListItem Value="D" Text="Dia" />
                        </asp:DropDownList>
                    </li>
                </ul>
                <ul class="ulDados" style="width:500px; margin-top: 0px !important; margin-left:0px !important;">
                    <li>
                        <div style="border: 1px solid #CCCCCC; width: 480px; height: 240px; overflow-y: scroll; margin-top: 10px;">
                            <asp:GridView ID="grdPacAtestado" CssClass="grdBusca" runat="server" Style="width: 100% !important;
                                cursor: default" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum paciente disponivel<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CK">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hidNmPac" Value='<%# Eval("NO_PAC_") %>' />
                                            <asp:HiddenField runat="server" ID="hidNmResp" Value='<%# Eval("NO_RESP_IMP") %>' />
                                            <asp:HiddenField runat="server" ID="hidRgPac" Value='<%# Eval("RG_PAC") %>' />
                                            <asp:HiddenField runat="server" ID="hidHora" Value='<%# Eval("hr_Consul") %>' />
                                            <asp:RadioButton ID="rbtPaciente" GroupName="rbtPac" runat="server"
                                                OnCheckedChanged="rbtPaciente_OnCheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="hr_Consul" HeaderText="HORA">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_PAC" HeaderText="PACIENTE">
                                        <ItemStyle Width="210px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_RESP" HeaderText="RESPONSÁVEL">
                                        <ItemStyle Width="190px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li id="li9" runat="server" class="liBtnAddA" style="margin-top: 10px !important; margin-left:230px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbGerarAtestado" OnClick="lnkbGerarAtestado_Click" runat="server" ValidationGroup="atestado" ToolTip="Emitir documento">
                            <asp:Label runat="server" ID="Label2" Text="EMITIR" Style="margin-left: 5px; margin-right: 5px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowImagens" style="display: none; height: 340px !important;">
                <ul class="ulDadosLog">
                    <li>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr id="Tr1" runat="server">
                                <td>
                                    <label>
                                        Imagem:</label>
                                    <asp:FileUpload ID="uploadPhoto1" runat="server" CssClass="" /><br />
                                    <div id="Parent">
                                    </div>
                                    <label>
                                        &nbsp;
                                    </label>
                                    <input type="button" onclick="addLinhaImage(); return false;" value="More" />
                                    &nbsp;
                                    <asp:Button ID="btnAddImagem" Text="Adicionar Imagem" runat="server" />
                                    <asp:Button ID="btnCancel" Text="Cancelar" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </li>
                    <%-- <li id="li5" runat="server" title="Clique para adicionar mais imagens" class="liBtnAddA"
                        style="float: right; margin: -25px -2px 3px 5px; width: 60px">
                        <img alt="" class="imgAdd" title="Adicionar imagem" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="lnkAddImg" runat="server" class="btnLabel" OnClick="lnkAddImg_OnClick">Adicionar</asp:LinkButton>
                    </li>
                    <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                        <div style="width: 890px; height: 305px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdAnexoImagens" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhuma imagem incluída<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="IMAGEM">
                                        <ItemStyle Width="18px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:FileUpload runat="server" ID="fupImagens" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DE_TITULO" HeaderText="TÍTULO">
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                height: 23px !important; width: 180px" ReadOnly="true" Text='<%# Eval("OBS") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Questionário" OnClick="imgExc_OnClick" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>--%>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowAudios" style="display: none; height: 340px !important;">
                <ul class="ulDadosLog">
                    <li id="liAddAudio" runat="server" title="Clique para adicionar mais áudios" class="liBtnAddA"
                        style="float: right; width: 70px; cursor: pointer" clientidmode="Static">
                        <img alt="" class="imgAdd" title="Adicionar áudio" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                            height="15px" width="15px" />
                        <label>
                            Adicionar</label>
                    </li>
                    <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                        <div style="width: 890px; height: 305px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdAnexoAudio" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true" ClientIDMode="Static">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhuma imagem incluída<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="ARQUIVO">
                                        <ItemStyle Width="18px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:FileUpload runat="server" ID="fupAudios" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Título">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtTituloAudio" Style="margin: 0 0 0 0 !important;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtObserAudio" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                height: 23px !important; width: 180px" ReadOnly="true"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Questionário" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divEncaminAtend" style="display: none; height: 100px !important; width: 270px">
                <asp:HiddenField ID="hidAgendSelec" runat="server" />
                <ul>
                    <li style="margin-bottom: 10px;">
                        <asp:Label ID="lblConfEncam" Text=" - " runat="server" />
                    </li>
                    <li class="liBtnConfirm" style="margin-left:85px; width: 30px">
                        <asp:LinkButton ID="lnkbAtendSim" OnClick="lnkbAtendSim_OnClick"
                            runat="server" ToolTip="Confirma o encaminhamento do paciente para atendimento">
                            <label style="margin-left:5px; color:White;">SIM</label>
                        </asp:LinkButton>
                    </li>
                    <li class="liBtnConfirm" style="margin:-22px 0 0 135px; width: 30px;">
                        <asp:LinkButton ID="lnkbAtendNao"
                            runat="server" ToolTip="Seleciona o paciente e mostra a situação atual do atendimento">
                            <label style="margin-left:5px; color:White;">NÃO</label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <script src="/Library/JS/Cronometro.js" type="text/javascript"></script>
    <script type="text/javascript">

        window.onload = function () {
            MaintainScroll1();
        }
        
        function MaintainScroll1() {
            var div = document.getElementById("divAgendasRecp");
            var div_position = document.getElementById("divAgendasRecp_posicao");
            var position = parseInt('<%= Request.Form["divAgendasRecp_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function MaintainScroll2() {
            var div = document.getElementById("divDemonAge");
            var div_position = document.getElementById("divDemonAge_posicao");
            var position = parseInt('<%= Request.Form["divDemonAge_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        $(document).ready(function () {

            $('#liAddAudio').click(function () {
                addLinhaAudio();
            });

            $('.lnkImagens').click(function () {
                $('#divLoadShowImagens').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false, title: "IMAGENS ANEXAS",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            });

            $('.lnkAudios').click(function () {
                $('#divLoadShowAudios').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false, title: "ÁUDIOS ANEXOS",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            });
        });

        function AbreModalLog() {
            $('#divLoadShowLogAgenda').dialog({ autoopen: false, modal: true, width: 902, height: 340, resizable: false, title: "HISTÓRICO DO AGENDAMENTO DE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalImagens() {
            $('#divLoadShowImagens').dialog({ autoopen: false, modal: true, width: 802, height: 340, resizable: false, title: "IMAGENS ANEXAS",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalFichaAtendimento() {
            $('#divFichaAtendimento').dialog({ autoopen: false, modal: true, width: 500, height: 320, resizable: false, title: "IMPRESSÃO DA FICHA DE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalGuiaPlano() {
            $('#divGuiaPlano').dialog({ autoopen: false, modal: true, width: 450, height: 180, resizable: false, title: "IMPRESSÃO DA GUIA DE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalAtestado() {
            $('#divAtestado').dialog({ autoopen: false, modal: true, width: 530, height: 350, resizable: false, title: "EMISSÃO DE DOCUMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalEncamAtend() {
            $('#divEncaminAtend').dialog({ autoopen: false, modal: true, width: 280, height: 80, resizable: false, title: "ENCAMINHAMENTO PARA ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function addLinhaAudio() {
            var table = document.getElementById('grdAnexoAudio');
            var elemTr = document.createElement("tr");
            //            var rowCount = table.rows.length;
            //  var row = table.insertRow(0);

            var elemTD = document.createElement("td");
            var element1 = document.createElement("input");
            element1.setAttribute("type", "file");
            element1.setAttribute("ID", "fupAudios");
            element1.setAttribute("runat", "server");
            element1.setAttribute("name", "fupAudios");
            elemTD.appendChild(element1);
            elemTr.appendChild(elemTD);

            var elemTD2 = document.createElement("td");
            var element2 = document.createElement("input");
            element2.setAttribute("type", "text");
            element2.setAttribute("ID", "txtTituloAudio");
            element2.setAttribute("runat", "server");
            element2.setAttribute("name", "txtTituloAudio");
            elemTD2.appendChild(element2);
            elemTr.appendChild(elemTD2);

            var elemTD3 = document.createElement("td");
            var element3 = document.createElement("input");
            element3.setAttribute("type", "text");
            element3.setAttribute("ID", "txtObserAudio");
            element3.setAttribute("runat", "server");
            element3.setAttribute("name", "txtObserAudio");
            elemTD3.appendChild(element3);
            elemTr.appendChild(elemTD3);

            table.appendChild(elemTr);
        }

        function addLinhaImage() {
            var rownum = 1;
            var div = document.createElement("div");
            var divid = "dv" + rownum;
            div.setAttribute("ID", divid);
            rownum++;

            //label dinâmico
            var lbl = document.createElement("label");
            lbl.setAttribute("ID", "lbl" + rownum);
            lbl.setAttribute("class", "label1");
            lbl.innerHTML = "Imagem";
            rownum++;

            //FileUpload dinâmico
            var _upload = document.createElement("input");
            _upload.setAttribute("type", "file");
            _upload.setAttribute("ID", "upload" + rownum);
            _upload.setAttribute("runat", "server");
            _upload.setAttribute("name", "uploads" + rownum);
            rownum++;

            var hyp = document.createElement("a");
            hyp.setAttribute("style", "cursor:Pointer");
            hyp.setAttribute("onclick", "return RemoveDv('" + divid + "');");
            hyp.innerHTML = "Remover";
            rownum++;

            var br = document.createElement("br");
            var _pdiv = document.getElementById("Parent");
            div.appendChild(br);
            div.appendChild(lbl);
            div.appendChild(_upload);
            div.appendChild(hyp);
            _pdiv.appendChild(div);
        }

        function RemoveDv(obj) {
            var p = document.getElementById("Parent");
            var chld = document.getElementById(obj);
            p.removeChild(chld);
        }

    </script>
</asp:Content>
