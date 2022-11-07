<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8264_AtendimentoUnificado.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1050px;
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
            margin-left: -4px;
        }
        .chk label
        {
            display: inline;
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
            text-align: center;
            background-color: #FFE1E1;
            float: left;
            margin-left: 13px;
            margin-top: -48px;
            width: 115px;
            margin-right: -130px;
            display: none;
        }
        .LabelHora
        {
            margin-top: 4px;
            font-size: 10px;
        }
        .Hora
        {
            font-family: Trebuchet MS;
            font-size: 23px;
            color: #9C3535;
            margin-top: -3px;
        }
        .invisible
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidIdAtendimento" />
    <div id="dvLocal" style="margin-bottom: -17px; margin-left: 14px">
        <label style="margin-bottom: -15px;" title="Escolha o local de atendimento de Pacientes">
            LOCAL</label>
        <asp:DropDownList runat="server" ToolTip="Escolha o local de atendimento de Pacientes"
            ID="ddlLocal" Style="width: auto; margin-left: 35px;">
        </asp:DropDownList>
    </div>
    <div id="divCronometro">
        <asp:HiddenField ID="hidTimerId" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="hidHoras" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="hidMinutos" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="hidSegundos" runat="server" Value="" ClientIDMode="Static" />
        <label class="LabelHora">
            Tempo de Atendimento</label>
        <label id="lblHora" class="Hora">
            00:00:00</label>
    </div>
    <ul class="ulDados">
        <li style="float: left; margin-left: 3px; width: 461px !important; border-right: 2px solid #EE9A00;
            padding-right: 10px;">
            <ul>
                <li>
                    <ul>
                        <li class="liTituloGrid" style="width: 450px !important; height: 20px !important;
                            margin-right: 0px; background-color: #ADD8E6; text-align: center; font-weight: bold;
                            margin-bottom: 2px; padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FFF">
                                        GRID DE PACIENTES</label>
                                </li>
                                <li style="margin-left: 10px; float: right; margin-top: 2px;">
                                    <ul class="ulPer">
                                        <li>
                                            <asp:TextBox runat="server" ID="txtNomePacPesqAgendAtend" Width="110px" placeholder="Pesquise pelo Nome"></asp:TextBox>
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
                                        <li style="margin: 0px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqAgendamentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqAgendamentos_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both">
                            <div style="width: 448px; height: 120px; border: 1px solid #CCC; overflow-y: scroll"
                                id="divAgendasRecp">
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
                                                <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# ("CO_ALU") %>' />
                                                <asp:CheckBox ID="chkSelectPaciente" runat="server" Enabled='<%# Eval("podeClicar") %>'
                                                    Width="100%" Style="margin: 0 0 0 -15px !important;" OnCheckedChanged="chkSelectPaciente_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DTHR" HeaderText="HORÁRIO">
                                            <ItemStyle HorizontalAlign="Center" Width="110px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="PACIENTE">
                                            <ItemStyle Width="320px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblNomPaci" Text='<%# Eval("PACIENTE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PASTA_CONTROL" HeaderText="Nº PASTA">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SX" HeaderText="SX">
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IDADE" HeaderText="IDADE">
                                            <ItemStyle HorizontalAlign="Left" Width="85px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="CNT">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgContratacao" ImageUrl='<%# Eval("tpContr_URL") %>' ToolTip='<%# Eval("tpContr_TIP") %>'
                                                    Width="17px" Height="17px" runat="server" />
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
                    </ul>
                </li>
                <li style="clear: both">
                    <ul style="float: left;">
                        <li class="liTituloGrid" style="width: 450px; height: 20px !important; margin-right: 0px;
                            background-color: #c1ffc1; text-align: center; font-weight: bold; margin-bottom: 2px;
                            padding-top: 2px; margin-left: 5px">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        DEMONSTRATIVO - AGENDA DO PACIENTE</label>
                                </li>
                                <li style="margin-left: 18px; float: right; margin-top: 2px;">
                                    <ul class="ulPer">
                                        <li>
                                            <asp:TextBox runat="server" class="campoData" ID="txtIniAgenda" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
                                            <asp:TextBox runat="server" class="campoData" ID="txtFimAgenda" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                        </li>
                                        <li style="margin: 0px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqHistAgend" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqHistAgend_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: both; margin: -14px 0 0 0px;">
                                    <div id="divDemonAge" style="width: 448px; height: 105px; border: 1px solid #CCC;
                                        overflow-y: scroll">
                                        <input type="hidden" id="divDemonAge_posicao" name="divDemonAge_posicao" />
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
                                                            Width="100%" Style="margin: 0 0 0 -15px !important;" OnCheckedChanged="chkSelectHistAge_OnCheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ST">
                                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgSituacaoHistorico" Style="width: 18px !important;
                                                            height: 18px !important; margin: 0 0 0 0 !important" ImageUrl='<%# Eval("imagem_URL") %>'
                                                            OnClick="imgSituacaoHistorico_OnClick" ToolTip='<%# Eval("imagem_TIP") %>' />
                                                        <asp:HiddenField runat="server" ID="hidIdAgenda" Value='<%# Eval("ID_AGENDA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="dtAgenda_V" HeaderText="AGENDA">
                                                    <ItemStyle HorizontalAlign="Left" Width="115px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PROFISSIONAL" HeaderText="PROFISSIONAL">
                                                    <ItemStyle HorizontalAlign="Left" Width="95px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DE_ACAO" HeaderText="AÇÃO ATENDIMENTO">
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
                </li>
                <li style="clear: both; margin: 108px 0 0 10px;">
                    <ul>
                        <li>
                            <ul style="width: 443px">
                                <li class="liTituloGrid" style="width: 273px; height: 20px !important; margin-left: -5px;
                                    background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                                    <ul>
                                        <li style="margin-left: 5px; float: left; width: 150px;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FF6347">
                                                AÇÕES E PROCEDIMENTOS</label>
                                        </li>
                                    </ul>
                                </li>
                                <li style="float: right; margin: -25px 80px 3px 5px; width: 82px" class="liBtnAddA">
                                    <img title="Emitir Prontuário do Paciente" style="margin-top: -1px" src="/Library/IMG/PGS_IC_Odontograma.png"
                                        height="16px" width="16px" />
                                    <asp:LinkButton ID="lnkbProntuarioOdonto" runat="server" OnClick="lnkpProntuarioOdonto_OnClick"
                                        ForeColor="#0099ff">PRONTUÁRIO</asp:LinkButton>
                                </li>
                                <li title="Clique para emitir o orçamento do paciente" class="liBtnAddA" style="float: right;
                                    margin: -25px -2px 3px 5px; width: 70px">
                                    <img title="Emitir Orçamento do Paciente" style="margin-top: -1px" src="/BarrasFerramentas/Icones/Imprimir.png"
                                        height="16px" width="16px" />
                                    <asp:LinkButton ID="lnkbOrcComplt" runat="server" OnClick="lnkbOrcamento_Click">Orçamento</asp:LinkButton>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both; margin: -7px 0 0 0;">
                            <div style="width: 448px; height: 120px; border: 1px solid #CCC; overflow-y: scroll">
                                <asp:GridView ID="grdProcedAprovados" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                    ShowHeaderWhenEmpty="true">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum Procedimento associado<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="SA">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <img class="imgsa" style="width: 16px; height: 16px !important" src='<%# Eval("imagem_URL_ACAO") %>'
                                                    alt="Icone" title="Indica se o procedimento foi realizado" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidIdAtendOrcam" Value='<%# Eval("IdAtendOrcam") %>' />
                                                <asp:HiddenField runat="server" ID="hidIdAtendHorar" Value='<%# Eval("IdAtendHorar") %>' />
                                                <asp:HiddenField runat="server" ID="hidProcIdAtendAgend" Value='<%# Eval("IdAtendAgend") %>' />
                                                <asp:HiddenField runat="server" ID="hidIdTrataPlano" Value='<%# Eval("IdTrataPlano") %>' />
                                                <asp:HiddenField runat="server" ID="hidIdProcedimento" Value='<%# Eval("IdProcMedicProce") %>' />
                                                <asp:CheckBox runat="server" ID="chkProcedimento" Enabled='<%# Eval("desabilitar") %>'
                                                    Checked='<%# Eval("isCheck") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtAgenda" HeaderText="DATA" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nomeProfissional" HeaderText="PROFISSIONAL">
                                            <ItemStyle HorizontalAlign="Left" Width="15px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="PROCEDIMENTO">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblProcedimento" Text='<%# Eval("nomeProcedimento") %>'
                                                    ToolTip='<%# Eval("tooltip") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CA">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgCancelarProcedimento" Style="width: 18px !important;
                                                    height: 18px !important; margin: 0 0 0 0 !important" ImageUrl="~/Library/IMG/PGS_IC_Cancelado.png"
                                                    alt="Icone" title="Cancelar procedimento" OnClick="imgCancelarProcedimento" Enabled='<%# Eval("desabilitar") %>' />
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
        <li style="float: right; width: 550px !important;">
            <ul style="margin-left: -18px;">
                <li>
                    <ul>
                        <li>
                            <label>
                                Profissional Responsável</label>
                            <asp:DropDownList ID="drpProfResp" Width="163px" runat="server" />
                        </li>
                        <li style="margin-left: 5px;">
                            <label>
                                Profissional Auxiliar</label>
                            <asp:DropDownList ID="drpProfAuxi" Width="163px" runat="server" />
                        </li>
                        <li style="margin-left: 5px;">
                            <label>
                                Técnico de Atendimento</label>
                            <asp:DropDownList ID="drpTecnAtend" Width="163px" runat="server" />
                        </li>
                        <li class="liTituloGrid" style="width: 508px !important; height: 20px !important;
                            clear: both; margin-right: 0px; background-color: #FFEC8B; text-align: center;
                            font-weight: bold; margin-bottom: 2px; padding-top: 2px;">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: Black;
                                float: left; margin-left: 10px;">
                                INFORMAÇÕES DO ATENDIMENTO</label>
                            <%--<div id="divBtnOdontograma" runat="server" style="margin-right: 5px; float: right;
                                margin-top: 4px;">
                                <img title="Emitir Odontograma do Paciente" style="margin-top: -2px" src="/Library/IMG/PGS_IC_Odontograma.png"
                                    height="16px" width="16px" />
                                <asp:LinkButton ID="lnkbOdontograma" runat="server" OnClick="lnkbOdontograma_OnClick"
                                    ForeColor="#0099ff">ODONTOGRAMA</asp:LinkButton>
                            </div>--%>
                            <div style="margin-right: 3px; float: right; margin-top: 2px;">
                                Dt. Atendimento
                                <asp:TextBox ID="txtDtAtend" runat="server" CssClass="campoData" />
                            </div>
                        </li>
                        <li style="margin: -2px 0 0 5px; border: 1px solid #FFEC8B; border-top: 0;">
                            <ul>
                                <li style="margin-top: 2px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        QUEIXA PRINCIPAL / AÇÃO PLANEJADA</label>
                                    <asp:TextBox runat="server" ID="txtQueixa" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Style="width: 496px; height: 43px; margin-top: 1px; border-top: 1px solid #BDBDBD;
                                        border-left: 0; border-right: 0; border-bottom: 0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -6px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        ANAMNESE / HDA (História da Doença Atual)</label>
                                    <asp:TextBox runat="server" ID="txtHDA" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Style="width: 496px; height: 53px; margin-top: 1px; border-top: 1px solid #BDBDBD;
                                        border-left: 0; border-right: 0; border-bottom: 0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -6px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        HIPÓTESE DIAGNÓSTICA / AÇÃO REALIZADA</label>
                                    <asp:TextBox runat="server" ID="txtHipotese" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Style="width: 496px; height: 40px; border-top: 1px solid #BDBDBD; border-left: 0;
                                        border-right: 0; border-bottom: 0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -6px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        EXAMES</label>
                                    <asp:TextBox runat="server" ID="txtExameFis" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Style="width: 496px; height: 40px; border-top: 1px solid #BDBDBD; border-left: 0;
                                        border-right: 0; border-bottom: 0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -6px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        OBSERVAÇÕES DO ATENDIMENTO</label>
                                    <asp:TextBox runat="server" ID="txtObserAtend" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Style="width: 496px; height: 40px; border-top: 1px solid #BDBDBD; border-left: 0;
                                        border-right: 0; border-bottom: 0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin: 0 0 -2px -1px;">
                                    <asp:CheckBox ID="chkAlergiaMedic" CssClass="chk" runat="server" Style="font-family: Arial Narrow;"
                                        Text="Alergia" ToolTip="O paciente possui alguma alergia a medicamento?" ClientIDMode="Static" />
                                    <asp:TextBox ID="txtAlergiaMedic" Width="446px" Height="14px" runat="server" Font-Size="11px"
                                        ClientIDMode="Static" Placeholder="Qual(is) Medicamento(s)?" ToolTip="Informe qual(is) medicamento(s) que o paciente possui alergia" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li style="margin: 10px 0 0 17px;">
                    <asp:LinkButton ID="lnkbAnexos" Style="background-color: #D8D8D8; margin: 0 3px 0 0;
                        width: 70px; border: 1px solid #BBBBBB;" Height="18px" AccessKey="A" OnClick="lnkbAnexos_OnClick"
                        ToolTip="Anexos associados ao Atendimento/Paciente" runat="server">
                                <img class="imgAnexos" style="width: 16px; height: 16px !important; margin: 2px 0 0 8px; float:left" src="/Library/IMG/PGS_IC_Anexo.png" alt="Icone" />
                                <label for="lnkbAnexos" style="color:#000000; margin:3px 0 0 0">
                                ANEXOS</label>
                    </asp:LinkButton>
                    <asp:Button ID="btnAnamRapida" Style="background-color: #D8D8D8; margin: 0 2px 0 -1px;
                        width: 85px" runat="server" Text="ANAM. RÁPIDA" OnClick="btnAnamRapida_Click"
                        Height="20px" ToolTip="Anamnese Rápida" />
                    <asp:Button ID="lnkMedic" Style="background-color: #D8D8D8; margin-left: 0px; width: 85px"
                        runat="server" Text="MEDICAMENTOS" Height="20px" OnClick="lnkMedic_OnClick" ToolTip="Medicamentos" />
                    <asp:Button ID="lnkExame" Style="background-color: #D8D8D8; margin-left: 2px; width: 70px"
                        runat="server" Text="EXAMES" Height="20px" OnClick="lnkExame_OnClick" />
                    <asp:Button ID="lnkPlanejamento" Style="background-color: #D8D8D8; margin-left: 2px;
                        width: 85px" runat="server" Text="TRATAMENTO" Height="20px" ToolTip="Planejamento"
                        OnClick="lnkPlanejamento_OnClick" />
                    <asp:Button ID="lnkOrcamento" Style="background-color: #D8D8D8; margin-left: 2px;
                        width: 80px" runat="server" Text="ORÇAMENTO" Height="20px" ToolTip="Orçamento"
                        OnClick="lnkOrcamento_OnClick" />
                </li>
                <li style="margin: 15px 0 0 0px;">
                    <ul>
                        <li style="margin: -5px 0 0 0px; padding-left: 10px; clear: both">
                            <asp:Button ID="BtnObserv" Style="background-color: #0099ff; color: #FFFAFA; margin-left: 0px;
                                width: 80px" runat="server" Text="CAMPO LIVRE" Height="20px" OnClick="BtnObserv_OnClick" />
                            <asp:Button ID="BtnFicha" Style="background-color: #0099ff; color: #FFFAFA; margin-left: 2px;
                                width: 80px" runat="server" Text="FICHA ATEND." Height="20px" OnClick="lnkFicha_OnClick" />
                            <asp:Button ID="BtnAtestado" runat="server" Style="background-color: #FF9933; color: #FFFAFA;
                                margin-left: 9px; width: 69px" Text="ATESTADO" Height="20px" OnClick="BtnAtestado_Click" />
                            <asp:Button ID="BtnLaudo" runat="server" Style="background-color: #FF9933; color: #FFFAFA;
                                margin-left: 2px; width: 70px" Text="RELATÓRIO" Height="20px" OnClick="BtnLaudo_Click" />
                            <asp:Button ID="BtnGuia" runat="server" Style="background-color: #FF9933; color: #FFFAFA;
                                margin-left: 2px; width: 35px" Text="GUIA" Height="20px" OnClick="BtnGuia_OnClick" />
                            <asp:Button ID="BtnSalvar" Style="background-color: #006600; color: #FFFAFA; width: 60px;
                                margin-left: 9px;" runat="server" Text="SALVAR" Height="20px" OnClick="BtnSalvar_OnClick" />
                            <asp:Button ID="BtnFinalizar" Style="background-color: #880000; color: #FFFAFA; width: 70px;
                                margin-left: 2px;" runat="server" Text="FINALIZAR" Height="20px" OnClick="BtnFinalizar_OnClick" />
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadShowLogAgenda" style="display: none; height: 600px!important;">
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
            <div id="divAtestado" style="display: none; width: 500px; height: 350px !important;">
                <ul class="ulDados" style="float: left; width: 500px; margin-top: 0px !important;">
                    <li>
                        <label title="Data Comparecimento" class="lblObrigatorio">
                            Data</label>
                        <asp:TextBox ID="txtDtAtestado" runat="server" ValidationGroup="atestado" class="campoData"
                            ToolTip="Informe a data de comparecimento" AutoPostBack="true" OnTextChanged="txtDtAtestado_OnTextChanged" />
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="atestado" ID="rfvDtAtestado"
                            CssClass="validatorField" ErrorMessage="O campo data é requerido" ControlToValidate="txtDtAtestado"></asp:RequiredFieldValidator>
                    </li>
                    <li style="margin-top: 0px !important; margin-left: 70px !important;">
                        <label title="Tipo Documento">
                            Atestado</label>
                        <asp:RadioButton ID="chkAtestado" GroupName="TipoDoc" CssClass="chk" Style="margin-left: -7px !important;"
                            OnCheckedChanged="chkAtestado_OnCheckedChanged" AutoPostBack="true" runat="server"
                            ToolTip="Imprimir um atestado médico" />
                        <%--<!--<asp:CheckBox ID="chkAtestado" OnCheckedChanged="chkAtestado_OnCheckedChanged" AutoPostBack="true" 
                            CssClass="chk" style="margin-left: -7px !important;" Checked="true" runat="server" ToolTip="Imprimir um atestado médico" />-->--%>
                        Dias
                        <asp:TextBox ID="txtQtdDias" runat="server" Width="20px" MaxLength="3" ToolTip="Informe a quantidade de dias de repouso do paciente">
                        </asp:TextBox>
                        <asp:CheckBox ID="chkCid" CssClass="chk" Style="margin-right: -6px !important;" Checked="true"
                            runat="server" ToolTip="Imprimir CID no atestado médico" />
                        CID
                        <asp:TextBox ID="txtCid" runat="server" Width="25px" MaxLength="5" ToolTip="CID">
                        </asp:TextBox>
                    </li>
                    <li style="margin-top: 0px !important; margin-left: 55px !important;">
                        <label title="Tipo Documento">
                            Comparecimento</label>
                        <asp:RadioButton ID="chkComparecimento" GroupName="TipoDoc" CssClass="chk" Style="margin-left: -7px !important;"
                            OnCheckedChanged="chkComparecimento_OnCheckedChanged" AutoPostBack="true" runat="server"
                            ToolTip="Imprimir uma declaração de comparecimento" />
                        Período
                        <asp:DropDownList ID="drpPrdComparecimento" runat="server" Style="vertical-align: top;"
                            ToolTip="Período de comparecimento">
                            <asp:ListItem Value="M" Text="Manhã" />
                            <asp:ListItem Value="T" Text="Tarde" />
                            <asp:ListItem Value="N" Text="Noite" />
                            <asp:ListItem Value="D" Text="Dia" />
                        </asp:DropDownList>
                    </li>
                </ul>
                <ul class="ulDados" style="width: 500px; margin-top: 0px !important; margin-left: 0px !important;">
                    <li>
                        <div style="border: 1px solid #CCCCCC; width: 480px; height: 240px; overflow-y: scroll;
                            margin-top: 10px;">
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
                                            <asp:RadioButton ID="rbtPaciente" GroupName="rbtPac" runat="server" OnCheckedChanged="rbtPaciente_OnCheckedChanged"
                                                AutoPostBack="true" />
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
                    <li id="li10" runat="server" class="liBtnAddA" style="margin-top: 10px !important;
                        margin-left: 230px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbGerarAtestado" OnClick="lnkbGerarAtestado_Click" runat="server"
                            ValidationGroup="atestado" ToolTip="Emitir documento">
                            <asp:Label runat="server" ID="Label12" Text="EMITIR" Style="margin-left: 5px; margin-right: 5px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divGuiaPlano" style="display: none; height: 150px !important;">
                <ul class="ulDados" style="width: 430px; margin-top: 0px !important">
                    <li>
                        <label title="Data Comparecimento" class="lblObrigatorio">
                            Data</label>
                        <asp:TextBox ID="txtDtGuia" runat="server" ValidationGroup="guia" class="campoData"
                            ToolTip="Informe a data de comparecimento" AutoPostBack="true" OnTextChanged="txtDtGuia_OnTextChanged" />
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="guia" ID="rfvDtGuia"
                            CssClass="validatorField" ErrorMessage="O campo data é requerido" ControlToValidate="txtDtGuia"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label title="Paciente">
                            Paciente</label>
                        <asp:DropDownList ID="drpPacienteGuia" runat="server" Width="240px" />
                    </li>
                    <li>
                        <label title="Operadora">
                            Operadora</label>
                        <asp:DropDownList ID="drpOperGuia" runat="server" Width="80px" />
                    </li>
                    <li style="clear: both;">
                        <label title="Observações">
                            Observações / Justificativa</label>
                        <asp:TextBox ID="txtObsGuia" Width="410px" Height="40px" TextMode="MultiLine" MaxLength="180"
                            runat="server" />
                    </li>
                    <li class="liBtnAddA" style="clear: none !important; margin-left: 180px !important;
                        margin-top: 8px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbImprimirGuia" runat="server" ValidationGroup="guia" OnClick="lnkbImprimirGuia_OnClick"
                            ToolTip="Imprimir guia do plano de saúde">
                            <asp:Label runat="server" ID="lblEmitirGuia" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divOdontograma" style="display: none; height: 440px !important;">
                <ul class="ulDados" style="width: 740px; margin-top: 0px !important">
                    <li>
                        <img title="Odontograma do Paciente" style="margin-top: 10px" src="/Library/IMG/imgOdontograma.png" />
                    </li>
                    <li></li>
                    <li class="liBtnAddA" style="clear: none !important; margin-left: 330px !important;
                        margin-top: 8px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbImprimirOdontograma" runat="server" ValidationGroup="guia"
                            OnClick="lnkbImprimirOdontograma_OnClick" ToolTip="Imprimir guia do plano de saúde">
                            <asp:Label runat="server" ID="Label13" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <asp:HiddenField runat="server" ID="hidObserMedicam" ClientIDMode="Static" />
    <div id="divMedicamentos" style="display: none; height: 430px !important; width: 800px">
        <ul class="ulDados" style="margin: 5px 0 0 -10px !important;">
            <li>
                <ul>
                    <li>
                        <label>
                            Grupo</label>
                        <asp:DropDownList ID="drpGrupoMedic" Width="150px" OnSelectedIndexChanged="drpGrupoMedic_SelectedIndexChanged"
                            AutoPostBack="true" runat="server" />
                    </li>
                    <li>
                        <label>
                            SubGrupo</label>
                        <asp:DropDownList ID="drpSubGrupoMedic" Width="150px" runat="server" />
                    </li>
                    <li>
                        <label>
                            Pesquisa por Medicamento</label>
                        <asp:RadioButton ID="rdbMedic" Style="margin: 0 -6px 0 -5px;" GroupName="PesqMedicamento"
                            Checked="true" runat="server" ClientIDMode="Static" />
                        <asp:TextBox ID="txtMedicamento" runat="server" ClientIDMode="Static" />
                    </li>
                    <li>
                        <label>
                            Pesquisa por Princípio Ativo</label>
                        <asp:RadioButton ID="rdbPrinc" Style="margin: 0 -6px 0 -5px;" GroupName="PesqMedicamento"
                            runat="server" ClientIDMode="Static" />
                        <asp:TextBox ID="txtPrincipio" runat="server" ClientIDMode="Static" />
                    </li>
                    <li style="margin-top: 7px;">
                        <asp:ImageButton ID="imgbPesqMedic" OnClick="imgbPesqMedic_OnClick" runat="server"
                            ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" />
                    </li>
                    <li title="Clique para cadastrar um novo medicamento" class="liBtnAddA" style="margin: 5px 0 0 65px !important;
                        width: 40px;">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Medicamento" src="/Library/IMG/Gestor_BtnEdit.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="lnkNovoMedicam" runat="server" OnClick="lnkNovoMedicam_OnClick">Novo</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li>
                <div style="margin-left: 5px; width: 755px; height: 100px; border: 1px solid #CCC;
                    overflow-y: scroll">
                    <asp:GridView ID="grdPesqMedic" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum medicamento encontrado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CK">
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hidIdMedic" Value='<%# Eval("CO_PROD") %>' runat="server" />
                                    <asp:HiddenField ID="hidNomeMedic" Value='<%# Eval("NO_PROD") %>' runat="server" />
                                    <asp:HiddenField ID="hidPrincAtiv" Value='<%# Eval("NO_PRINCIPIO_ATIVO") %>' runat="server" />
                                    <asp:RadioButton ID="rdbMedicamento" GroupName="Medicamentos" runat="server" Width="100%"
                                        Style="margin: 0 -15px 0 -25px !important;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NO_PROD" HeaderText="MEDICAMENTO">
                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_PRINCIPIO_ATIVO" HeaderText="PRINCÍPIO ATIVO">
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_PROD" HeaderText="DESCRIÇÃO">
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FORNEC" HeaderText="FORNECEDOR">
                                <ItemStyle HorizontalAlign="Left" Width="55px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VL_UNIT_PROD" HeaderText="VALOR">
                                <ItemStyle HorizontalAlign="Right" Width="15px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="clear: both;">
                <ul>
                    <li>
                        <label class="lblObrigatorio" title="Informe a prescrição médica">
                            PRESCRIÇÃO</label>
                        <asp:TextBox ID="txtPrescricao" Width="430px" Height="17px" Font-Size="13px" ValidationGroup="AddMedic"
                            ToolTip="Informe a prescrição médica" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvPrescricao" CssClass="validatorField"
                            ValidationGroup="AddMedic" ErrorMessage="O campo uso é obrigatório" ControlToValidate="txtPrescricao"
                            Display="Dynamic" />
                    </li>
                    <li>
                        <label class="lblObrigatorio" title="Informe a quantidade utilizada do medicamento">
                            QTD</label>
                        <asp:TextBox ID="txtQuantidade" CssClass="campoQtd" Width="20px" Height="17px" Font-Size="13px"
                            Style="text-align: right;" ValidationGroup="AddMedic" ToolTip="Informe a quantidade utilizada do medicamento"
                            runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvQuantidade" CssClass="validatorField"
                            ValidationGroup="AddMedic" ErrorMessage="O campo uso é obrigatório" ControlToValidate="txtQuantidade"
                            Display="Dynamic" />
                    </li>
                    <li>
                        <label class="lblObrigatorio" title="Informe a quantidade de dias de uso do medicamento, sendo 0 como Uso Contínuo">
                            USO</label>
                        <asp:TextBox ID="txtUso" CssClass="campoQtd" Width="20px" Height="17px" Font-Size="13px"
                            Style="text-align: right;" ValidationGroup="AddMedic" ToolTip="Informe a quantidade de dias de uso do medicamento, sendo 0 como Uso Contínuo"
                            runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvUso" CssClass="validatorField"
                            ValidationGroup="AddMedic" ErrorMessage="O campo uso é obrigatório" ControlToValidate="txtUso"
                            Display="Dynamic" />
                    </li>
                    <li title="Clique para adicionar o medicamento" class="liBtnAddA" style="clear: both;
                        margin-top: -4px; margin-left: 350px !important; width: 75px;">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Medicamento" src="/Library/IMG/Gestor_SaudeEscolar.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="lnkAddMedicamm" runat="server" OnClick="lnkAddMedicam_OnClick"
                            ValidationGroup="AddMedic">ADICIONAR</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li style="margin-left: 15px !important; clear: both; float: left;">
                <ul>
                    <li>
                        <ul style="width: 745px;">
                            <li class="liTituloGrid" style="height: 20px !important; width: 735px; margin-left: -10px;
                                background-color: #A9D0F5; text-align: center; font-weight: bold; float: left">
                                <ul>
                                    <li style="margin: 0 0 0 10px; float: left">
                                        <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                            MEDICAMENTOS</label>
                                    </li>
                                </ul>
                            </li>
                            <li title="Clique para emitir o Receituario do atendimento" class="liBtnAddA" style="float: right;
                                margin: -25px -2px 3px 0px; width: 12px; height: 15px;">
                                <asp:ImageButton ID="BtnReceituario" runat="server" OnClick="BtnReceituario_Click"
                                    ToolTip="Emitir Receituario do Paciente" Style="margin-top: -2px; margin-left: -3px;"
                                    ImageUrl="/BarrasFerramentas/Icones/Imprimir.png" Height="18px" Width="18px" />
                            </li>
                        </ul>
                    </li>
                    <li style="clear: both; margin: -7px 0 0 -5px;">
                        <div style="width: 755px; height: 80px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdMedicamentos" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum medicamento associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="MEDICAMENTO">
                                        <ItemStyle Width="110px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdMedic" Visible="false" runat="server" />
                                            <asp:Label ID="lblMedic" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Princípio Ativo">
                                        <ItemStyle HorizontalAlign="Left" Width="180px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrincipio" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prescrição">
                                        <ItemStyle HorizontalAlign="Left" Width="400px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrescricao" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTD">
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtd" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="USO">
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUso" Width="100%" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgExcMedic" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Medicamento" OnClick="imgExcMedic_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li style="clear: both; margin: 3px 0 0 -5px;">
                        <asp:TextBox runat="server" ID="txtObserMedicam" TextMode="MultiLine" ClientIDMode="Static"
                            Style="width: 755px; height: 35px;" Font-Size="12px" placeholder=" Digite as observações sobre Medicamentos"></asp:TextBox>
                    </li>
                </ul>
            </li>
        </ul>
    </div>
    <asp:HiddenField runat="server" ID="hidObserExame" ClientIDMode="Static" />
    <div id="divExames" style="display: none; height: 300px !important; width: 420px">
        <ul class="ulDados">
            <li>
                <ul style="width: 506px">
                    <li class="liTituloGrid" style="width: 413px; height: 20px !important; margin-left: -0px;
                        background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                        <ul>
                            <li style="margin: 0 0 0 10px; float: left">
                                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                    EXAMES</label>
                            </li>
                            <li style="float: right; margin-top: 3px; margin-right: 15px;">
                                <ul>
                                    <li>
                                        <asp:Label runat="server" ID="Label5">Contratação</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlOperProcPlan" Width="100px" OnSelectedIndexChanged="ddlOperProcPlan_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: -2px;">
                                        <asp:Label runat="server" ID="Label6">Plano</asp:Label>
                                        <asp:DropDownList runat="server" Width="100px" ID="ddlPlanProcPlan">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li id="li2" runat="server" title="Clique para cadastrar um novo Exame (Procedimento)"
                        class="liBtnAddA" style="float: right; margin: -25px 44px 3px 5px; width: 40px">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_BtnEdit.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="imgNovoExam" runat="server" OnClick="imgNovoExam_OnClick">Novo</asp:LinkButton>
                    </li>
                    <li id="li4" runat="server" title="Clique para adicionar um exame ao atendimento"
                        class="liBtnAddA" style="float: right; margin: -25px 22px 3px 5px; height: 15px;
                        width: 12px;">
                        <asp:ImageButton ID="lnkAddProcPla" Height="15px" Width="15px" Style="margin-top: -1px;
                            margin-left: -2px;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" OnClick="lnkAddProcPla_OnClick"
                            runat="server" />
                    </li>
                    <li title="Clique para emitir os exames do paciente" class="liBtnAddA" style="float: right;
                        margin: -25px -2px 3px 0px; width: 12px; height: 15px;">
                        <asp:ImageButton ID="BtnExames" runat="server" OnClick="BtnExames_OnClick" ToolTip="Emitir Exames do Paciente"
                            Style="margin-top: -2px; margin-left: -3px;" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png"
                            Height="18px" Width="18px" />
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin: -7px 0 0 5px !important;">
                <div style="width: 506px; height: 42px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdExame" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum Procedimento de Plano de Saúde associado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CÓDIGO">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlExame" Width="100%" OnSelectedIndexChanged="ddlExame_OnSelectedIndexChanged"
                                        AutoPostBack="true" Style="margin: 0 0 0 -4px !important;">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIÇÃO">
                                <ItemStyle Width="250px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigProcedPla" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EX">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgExcPla" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                        ToolTip="Excluir Exame" OnClick="imgExcPla_OnClick" Style="margin: 0 0 0 0 !important;"
                                        OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="clear: both; margin: 3px 0 0 5px;">
                <asp:TextBox runat="server" ID="txtObserExame" TextMode="MultiLine" ClientIDMode="Static"
                    Style="width: 506px; height: 15px;" Font-Size="11px" placeholder=" Digite as observações sobre Exames"></asp:TextBox>
            </li>
        </ul>
    </div>
    <asp:HiddenField runat="server" ID="hidObsOrcam" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidDtValidOrcam" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidCkOrcamAprov" ClientIDMode="Static" />
    <div id="divOrcamentos" style="display: none; height: 460px !important; width: 450px">
        <ul class="ulDados">
            <li style="">
                <div>
                    <img style="margin-top: -2px; margin-left: 18px;" src="/Library/IMG/imgOdontograma.png"
                        height="223px" width="550px" />
                </div>
            </li>
            <li style="margin-top: 5px; margin-left: 22px; border-left: 1px solid black; height: 210px;">
            </li>
            <li style="margin-top: 5px; margin-left: 30px;">
                <ul>
                    <li style="clear: both; margin-left: 8px;">
                        <asp:Label ID="Label14" runat="server">Itens Homologados</asp:Label>
                        <asp:TextBox runat="server" ID="txtItensTotalOrcam" Enabled="false" Style="margin-left: 5px;
                            margin-top: 4px; height: 16px !important; width: 15px" CssClass="campoMoeda"></asp:TextBox>
                    </li>
                    <li style="margin-left: 35px;">
                        <asp:Label ID="Label16" runat="server">Itens Cortesia</asp:Label>
                        <asp:TextBox runat="server" ID="txtCortTotalOcam" Enabled="false" Style="margin-left: 5px;
                            margin-top: 4px; height: 16px !important; width: 15px" CssClass="campoMoeda"></asp:TextBox>
                    </li>
                    <li style="clear: both;">
                        <asp:Label ID="Label7" runat="server">Valor Orçamento R$</asp:Label>
                        <asp:TextBox runat="server" ID="txtVlTotalOrcam" Enabled="false" Style="margin-left: 5px;
                            margin-top: 4px; height: 16px !important; width: 47px" CssClass="campoMoeda"></asp:TextBox>
                    </li>
                    <li style="margin-top: 5px; clear: both;">
                        <asp:Label ID="Label3" runat="server">Validade Orçamento</asp:Label>
                        <asp:TextBox runat="server" ID="txtDtValidade" Style="margin-left: 5px;" CssClass="campoData"
                            ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li style="clear: both;">
                        <asp:Label ID="Label8" runat="server">Observação</asp:Label>
                        <asp:TextBox runat="server" ID="txtObsOrcam" Columns="45" Rows="5" TextMode="MultiLine"
                            ClientIDMode="Static" Style="margin-bottom: -62px; clear: both; margin-left: -54px;"
                            MaxLength="500" Font-Size="10px" placeholder=" Digite as observações sobre o Orçamento"></asp:TextBox>
                    </li>
                    <li style="margin-top: 70px; clear: both;">
                        <asp:Label ID="Label15" Style="display: inline-block;" runat="server">Homologar</asp:Label>
                        <asp:CheckBox ID="chkAprovado" Style="margin-left: -7px;" CssClass="chk" runat="server"
                            ToolTip="Marque se o orçamento foi homologado para o faturamento" ClientIDMode="Static" />
                    </li>
                </ul>
            </li>
            <li>
                <ul style="width: 875px; clear: both;">
                    <li class="liTituloGrid" style="width: 734px; height: 20px !important; margin-left: -5px;
                        background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                        <ul>
                            <li style="margin-left: 5px; float: left; width: 60px;">
                                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FF6347">
                                    ORÇAMENTO</label>
                            </li>
                            <li style="float: right; margin-top: 3px; margin-right: -3px;">
                                <ul>
                                    <%--<li>
                                        <asp:Label runat="server" ID="lbloper">Cont</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlOperOrc" OnSelectedIndexChanged="ddlOperOrc_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <asp:Label runat="server" ID="Label3">Plan</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlPlanOrc">
                                        </asp:DropDownList>
                                    </li>--%>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li title="Clique para Adicionar um item de orçamento" class="liBtnAddA" style="float: right;
                        margin: -25px 77px 3px 0px; width: 61px">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="btnAddProcOrc" runat="server" OnClick="btnAddProcOrc_OnClick">Adicionar</asp:LinkButton>
                    </li>
                    <li title="Clique para emitir o orçamento do paciente" class="liBtnAddA" style="float: right;
                        margin: -25px -2px 3px 5px; width: 70px">
                        <img title="Emitir Orçamento do Paciente" style="margin-top: -1px" src="/BarrasFerramentas/Icones/Imprimir.png"
                            height="16px" width="16px" />
                        <asp:LinkButton ID="lnkbOrcamento" runat="server" OnClick="lnkbOrcamento_Click">Orçamento</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin: -7px 0 0 0;">
                <div style="width: 880px; height: 132px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdProcedOrcam" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum Orçamento associado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CONTRAT">
                                <ItemStyle Width="56px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidAtendOrcam" />
                                    <asp:DropDownList runat="server" ID="ddlOperOrc" OnSelectedIndexChanged="ddlOperOrc_OnSelectedIndexChanged"
                                        AutoPostBack="true" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PLANO">
                                <ItemStyle Width="56px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList Width="100%" runat="server" ID="ddlPlanOrc" Style="margin-left: -4px;
                                        margin-bottom: 0px;">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CÓDIGO">
                                <ItemStyle Width="56px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlProcedOrc" OnSelectedIndexChanged="ddlProcedOrc_OnSelectedIndexChanged"
                                        AutoPostBack="true" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hidValUnitProc" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PROCEDIMENTO">
                                <ItemStyle Width="240px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigProcedOrc" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTD">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtQtdProcedOrc" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;" OnTextChanged="txtQtdProcedOrc_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DENTE">
                                <ItemStyle Width="46px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlNumDente" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CORT">
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkCortItem" Width="100%" CssClass="" Style="margin-left: -4px;
                                        margin-bottom: 0px;" AutoPostBack="true" OnCheckedChanged="chkCortItem_OnCheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALOR">
                                <ItemStyle Width="34px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtValorProcedOrc" Width="100%" CssClass="campoDinheiro"
                                        Style="margin-left: -4px; margin-bottom: 0px;" OnTextChanged="txtValorProcedOrc_OnTextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESC">
                                <ItemStyle Width="27px" HorizontalAlign="Center" />
                                <HeaderStyle />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtValorDescOrc" Width="100%" CssClass="campoDinheiro"
                                        Style="margin-left: -4px; margin-bottom: 0px;" OnTextChanged="txtVlDscto_OnTextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TOTAL">
                                <ItemStyle Width="38px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtValorTotalOrc" Width="100%" CssClass="campoDinheiro"
                                        Style="margin-left: -4px; margin-bottom: 0px;"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HOMOL">
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkHomolItem" Width="100%" CssClass="" Style="margin-left: -4px;
                                        margin-bottom: 0px;" AutoPostBack="true" OnCheckedChanged="chkHomolItem_OnCheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EX">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgExcOrc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                        ToolTip="Excluir Orçamento" OnClick="imgExcOrc_OnClick" Style="margin: 0 0 0 0 !important;"
                                        OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID_ATEND_ORCAM">
                                <HeaderStyle CssClass="invisible" />
                                <ItemStyle Width="56px" HorizontalAlign="Center" CssClass="invisible" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtAtendOrcam"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="clear: both; margin: 3px 0 0 0;"></li>
        </ul>
    </div>
    <div id="divLoadShowNovoMedic" style="display: none; height: 370px !important;">
        <ul class="ulDados" style="width: 350px !important;">
            <li>
                <label for="txtNProduto" class="lblObrigatorio" title="Nome do Produto">
                    Nome do Produto</label>
                <asp:TextBox ID="txtNProduto" Width="300px" ToolTip="Informe o Nome do Produto" runat="server"
                    MaxLength="60"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvProduto"
                    CssClass="validatorField" ErrorMessage="O nome do produto é obrigatório" ControlToValidate="txtNProduto"
                    Display="Dynamic" />
            </li>
            <li style="clear: both">
                <label for="txtNReduz" class="lblObrigatorio" title="Nome Reduzido">
                    Nome Reduzido</label>
                <asp:TextBox ID="txtNReduz" Width="180px" ToolTip="Informe o Nome Reduzido" runat="server"
                    MaxLength="33"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvNReduz"
                    CssClass="validatorField" ErrorMessage="O nome reduzido do produto é obrigatório"
                    ControlToValidate="txtNReduz" Display="Dynamic" />
            </li>
            <li>
                <label for="txtCodRef" class="lblObrigatorio" title="Código de Referência">
                    Cód. Referência</label>
                <asp:TextBox ID="txtCodRef" Width="110px" ToolTip="Informe o Código de Referência"
                    runat="server" MaxLength="9"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvCodRef"
                    CssClass="validatorField" ErrorMessage="O código referência do produto é obrigatório"
                    ControlToValidate="txtCodRef" Display="Dynamic" />
            </li>
            <li style="clear: both">
                <label for="txtDescProduto" class="lblObrigatorio" title="Descrição do Produto">
                    Descrição do Produto</label>
                <asp:TextBox ID="txtDescProduto" Width="300px" Height="51px" TextMode="MultiLine"
                    ToolTip="Informe a Descrição do Produto" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvDescProduto"
                    CssClass="validatorField" ErrorMessage="A descrição do produto é obrigatória"
                    ControlToValidate="txtDescProduto" Display="Dynamic" />
            </li>
            <li style="clear: both;">
                <label class="lblObrigatorio">
                    Princípio Ativo</label>
                <asp:TextBox runat="server" ID="txtPrinAtiv" Width="300px" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvPrinAtiv"
                    CssClass="validatorField" ErrorMessage="O princípio ativo do produto é obrigatório"
                    ControlToValidate="txtPrinAtiv" Display="Dynamic" />
            </li>
            <%--<!--<li style="clear: both;">
                <label for="ddlTipoProduto" title="Tipo de Produto" class="lblObrigatorio">
                    Tipo de Produto</label>
                <asp:DropDownList ID="ddlTipoProduto" Width="115px" ToolTip="Selecione o Tipo de Produto"
                    runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvTipoProduto" CssClass="validatorField"
                ErrorMessage="O tipo do produto é obrigatório" ControlToValidate="ddlTipoProduto" Display="Dynamic" />
            </li>-->--%>
            <li style="clear: both; margin-top: 10px;">
                <label for="ddlGrupo" class="lblObrigatorio" title="Grupo de Produtos">
                    Grupo</label>
                <asp:DropDownList ID="ddlGrupo" Width="150px" ToolTip="Selecione o Grupo de Produtos"
                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvGrupo"
                    CssClass="validatorField" ErrorMessage="O grupo do produto é obrigatório" ControlToValidate="ddlGrupo"
                    Display="Dynamic" />
            </li>
            <li style="margin-top: 10px;">
                <label for="ddlSubGrupo" class="lblObrigatorio" title="SubGrupo de Produtos">
                    SubGrupo</label>
                <asp:DropDownList ID="ddlSubGrupo" Width="145px" ToolTip="Selecione o SubGrupo de Produtos"
                    runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvSubGrupo"
                    CssClass="validatorField" ErrorMessage="O SubGrupo do produto é obrigatório"
                    ControlToValidate="ddlSubGrupo" Display="Dynamic" />
            </li>
            <li style="clear: both">
                <label class="lblObrigatorio" title="Unidade do Produtos">
                    Unidade</label>
                <asp:DropDownList ID="ddlUnidade" CssClass="campoDescricao" ToolTip="Unidade do Produtos"
                    runat="server" Width="90px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvUnidade"
                    CssClass="validatorField" ErrorMessage="A unidade do produto é obrigatória" ControlToValidate="ddlUnidade"
                    Display="Dynamic" />
            </li>
            <li id="li6" runat="server" class="liBtnAddA" style="margin-top: 0px !important;
                clear: both !important; height: 15px; margin-left: 140px !important;">
                <asp:LinkButton ID="lnkNovoMedic" runat="server" ValidationGroup="novoMedicamento"
                    OnClick="lnkNovoMedic_OnClick" ToolTip="Armazena as informações na prescrição em questão">
                    <asp:Label runat="server" ID="Label10" Text="SALVAR" Style="margin-left: 4px; margin-right: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divLoadShowNovoExam" style="display: none; height: 355px !important;">
        <ul class="ulDados" style="width: 480px; margin-top: 0px !important">
            <li>
                <label title="Nome do Procedimento de Saúde" class="lblObrigatorio">
                    Nome Exame/Procedimento</label>
                <asp:TextBox runat="server" ID="txtNoProcedimento" Width="290px" MaxLength="100"
                    ToolTip="Nome do Procedimento de Saúde"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rfvNoProcedimento"
                    CssClass="validatorField" ErrorMessage="O nome do procedimento é obrigatório"
                    ControlToValidate="txtNoProcedimento" Display="Dynamic" />
            </li>
            <li>
                <label title="Código do Procedimento de Saúde" class="lblObrigatorio">
                    Código Pro.</label>
                <asp:TextBox runat="server" ID="txtCodProcMedic" ToolTip="Código do Procedimento Médico"
                    Width="50px" CssClass="campoCodigo"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rfvCodProcMedic"
                    CssClass="validatorField" ErrorMessage="O código do procedimento é obrigatório"
                    ControlToValidate="txtCodProcMedic" Display="Dynamic" />
            </li>
            <li style="margin: 12px 0 0 -5px">
                <asp:CheckBox runat="server" ID="chkRequerAuto" Text="Requer Autorização" CssClass="chk"
                    ToolTip="Selecione caso o procedimento em questão precise de aprovação na Central de Regulação" />
            </li>
            <li style="clear: both; margin-top: -3px;">
                <label title="Grupo de Procedimentos de Saúde" class="lblObrigatorio">
                    Grupo</label>
                <asp:DropDownList runat="server" ID="ddlGrupo2" Width="153px" ToolTip="Grupo de Procedimentos de Saúde"
                    OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rvfGrupo2"
                    CssClass="validatorField" ErrorMessage="O grupo do procedimento é obrigatório"
                    ControlToValidate="ddlGrupo2" Display="Dynamic" />
            </li>
            <li style="margin-top: -3px;">
                <label title="SubGrupo de Procedimentos de Saúde" class="lblObrigatorio">
                    SubGrupo</label>
                <asp:DropDownList runat="server" ID="ddlSubGrupo2" Width="153px" ToolTip="SubGrupo de Procedimentos de Saúde">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rfvSubGrupo2"
                    CssClass="validatorField" ErrorMessage="O SubGrupo do procedimento é obrigatório"
                    ControlToValidate="ddlSubGrupo2" Display="Dynamic" />
            </li>
            <li style="margin-top: -3px">
                <label title="Pesquise pelo Operadora de Planos de Saúde">
                    Operadora</label>
                <asp:DropDownList runat="server" ID="ddlOper" Width="140px" ToolTip="Pesquise pelo Operadora de Planos de Saúde">
                </asp:DropDownList>
            </li>
            <li style="margin-top: 10px">
                <label style="color: Blue;">
                    VALORES</label>
                <ul style="margin-left: -5px;">
                    <li>
                        <label title="Valor de Custo do procedimento">
                            R$ Custo</label>
                        <asp:TextBox runat="server" ID="txtVlCusto" CssClass="campoDinheiro" Width="50px"
                            ToolTip="Valor de Custo atual do procedimento" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor base do procedimento de saúde para cálculo">
                            R$ Base</label>
                        <asp:TextBox runat="server" ID="txtVlBase" Enabled="false" Width="60px" CssClass="campoDinheiro"
                            ToolTip="Valor base do procedimento de saúde para cálculo" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor Base da Operadora para o procedimento">
                            R$ Restitu</label>
                        <asp:TextBox runat="server" ID="txtVlRestitu" Enabled="false" Width="60px" CssClass="campoDinheiro"
                            ToolTip="Valor de Restituição atual do procedimento" ClientIDMode="Static"></asp:TextBox>
                    </li>
                </ul>
            </li>
            <li style="margin-left: 130px; margin-top: 10px;">
                <label style="color: Blue;">
                    RESTRIÇÕES</label>
                <ul style="margin-left: -5px;">
                    <li>
                        <label title="Quantidade de Seção Autorizadas Pelo plano de Saúde">
                            QSA</label>
                        <asp:TextBox runat="server" Width="35px" CssClass="campoQtd" ID="txtQtSecaoAutorizada"
                            ToolTip="Quantidade de Seção Autorizadas pelo plano de Saúde"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Quantidade de Auxiliares para o procedimento">
                            QAUX</label>
                        <asp:TextBox runat="server" ID="txtQTAux" Width="30px" ToolTip="Quantidade de Auxiliares para o procedimento"
                            CssClass="campoQtd"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Quantidade de Anestesistas para o procedimento">
                            QANE</label>
                        <asp:TextBox runat="server" ID="txtQTAnest" Width="35px" ToolTip="Quantidade de Anestesistas para o procedimento"
                            CssClass="campoQtd"></asp:TextBox>
                    </li>
                </ul>
            </li>
            <li style="clear: both">
                <label title="Observação do Procedimento de Saúde">
                    Observação</label>
                <asp:TextBox runat="server" ID="txtObsProced" TextMode="MultiLine" Width="463px"
                    Height="60px" ToolTip="Observação do Procedimento de Saúde"></asp:TextBox>
            </li>
            <li id="li7" runat="server" class="liBtnAddA" style="margin-top: 0px !important;
                clear: both !important; height: 15px; margin-left: 230px !important;">
                <asp:LinkButton ID="lnkNovoExam" runat="server" ValidationGroup="novoProcedimento"
                    OnClick="lnkNovoExam_OnClick" ToolTip="Armazena as informações na prescrição em questão">
                    <asp:Label runat="server" ID="Label11" Text="SALVAR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divLaudo" style="display: none; height: 240px !important;">
        <asp:HiddenField ID="hidIdLaudo" runat="server" />
        <ul class="ulDados" style="width: 545px; margin-top: 0px !important">
            <li>
                <label title="Paciente" class="lblObrigatorio">
                    Paciente</label>
                <asp:DropDownList ID="drpPacienteLaudo" OnSelectedIndexChanged="drpPacienteLaudo_SelectedIndexChanged"
                    AutoPostBack="true" runat="server" Width="225px" />
                <asp:RequiredFieldValidator ValidationGroup="laudo" runat="server" ID="rfvPacienteLaudo"
                    CssClass="validatorField" ErrorMessage="O paciente é obrigatório" ControlToValidate="drpPacienteLaudo"
                    Display="Dynamic" />
            </li>
            <li>
                <label title="Título do Laudo">
                    Título do Laudo</label>
                <asp:TextBox ID="txtTituloLaudo" runat="server" Width="200px" Text="LAUDO TÉCNICO" />
            </li>
            <li>
                <label title="Data Laudo">
                    Data Laudo</label>
                <asp:TextBox ID="txtDtLaudo" runat="server" class="campoData" ToolTip="Informe a data do laudo" />
            </li>
            <li style="clear: both;">
                <label title="Laudo Técnico" style="color: Blue;">
                    LAUDO TÉCNICO</label>
                <asp:TextBox ID="txtObsLaudo" Width="520px" Height="200px" TextMode="MultiLine" runat="server" />
            </li>
            <li class="liBtnAddA" style="clear: none !important; margin-left: 250px !important;
                margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirLaudo" ValidationGroup="laudo" runat="server" OnClick="lnkbImprimirLaudo_OnClick"
                    ToolTip="Imprimir laudo técnico">
                    <asp:Label runat="server" ID="Label9" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divEncaminAtend" style="display: none; height: 100px !important; width: 270px">
        <asp:HiddenField ID="hidAgendSelec" runat="server" />
        <ul>
            <li style="margin-bottom: 10px;">
                <asp:Label ID="lblConfEncam" Text=" - " runat="server" />
            </li>
            <li class="liBtnConfirm" style="margin-left: 85px; width: 30px">
                <asp:LinkButton ID="lnkbAtendSim" OnClick="lnkbAtendSim_OnClick" runat="server" ToolTip="Confirma o encaminhamento do paciente para atendimento">
                    <label style="margin-left:5px; color:White;">SIM</label>
                </asp:LinkButton>
            </li>
            <li class="liBtnConfirm" style="margin: -22px 0 0 135px; width: 30px;">
                <asp:LinkButton ID="lnkbAtendNao" runat="server" ToolTip="Seleciona o paciente e mostra a situação atual do atendimento">
                    <label style="margin-left:5px; color:White;">NÃO</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <asp:HiddenField ID="hidTxtObserv" runat="server" />
    <div id="divObservacao" style="display: none; height: 300px !important; width: 580px">
        <ul>
            <li>
                <asp:TextBox runat="server" ID="txtObservacoes" TextMode="MultiLine" Style="width: 530px;
                    height: 240px;" Font-Size="13px" ToolTip="Digite as observações sobre o Atendimento"></asp:TextBox>
            </li>
            <li class="liBtnConfirm" style="margin: 10px 0 0 240px; width: 45px;">
                <asp:LinkButton ID="lnkbSalvarObserv" runat="server" OnClick="lnkbSalvarObserv_OnClick">
                    <label style="margin-left:5px; color:White;">SALVAR</label>
                </asp:LinkButton>
            </li>
            <li class="liBtnAddA" style="margin: -22px 10px 0 0; float: right; width: 45px;">
                <asp:LinkButton ID="lnkbImprimirObserv" runat="server" OnClick="lnkbImprimirObserv_OnClick">
                    <label style="margin-left:5px;">EMITIR</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divFichaAtendimento" style="display: none; height: 470px !important;">
        <ul class="ulDados" style="width: 410px; margin-top: 0px !important">
            <li>
                <label>
                    Queixas</label>
                <asp:TextBox runat="server" ID="txtQxsFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>
                    Anamnese</label>
                <asp:TextBox runat="server" ID="txtAnamneseFicha" TextMode="MultiLine" Width="400px"
                    Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>
                    Diagnostico</label>
                <asp:TextBox runat="server" ID="txtDiagnosticoFicha" TextMode="MultiLine" Width="400px"
                    Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>
                    Exame</label>
                <asp:TextBox runat="server" ID="txtExameFicha" TextMode="MultiLine" Width="400px"
                    Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>
                    Observação</label>
                <asp:TextBox runat="server" ID="txtObsFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
            </li>
            <li id="li5" runat="server" class="liBtnAddA" style="float: right; margin-top: 10px !important;
                clear: none !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirFicha" runat="server" OnClick="lnkbImprimirFicha_Click"
                    ToolTip="Imprimir ficha de atendimento">
                    <asp:Label runat="server" ID="lblEmitirFicha" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divAnexos" style="display: none; height: 360px !important;">
        <asp:HiddenField runat="server" ID="hidTpAnexo" />
        <ul class="ulDados" style="width: 880px; margin-left: -10px !important; margin-top: 0px !important">
            <li style="clear: both; margin: 0 0 0 5px !important;">
                <div style="width: 862px; height: 215px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdAnexos" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum arquivo associado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="DT_CADAS" HeaderText="DATA">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_REGIS" HeaderText="REGISTRO">
                                <ItemStyle Width="30px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NM_PROC_MEDI" HeaderText="PROCEDIMENTO">
                                <ItemStyle Width="180px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="TP">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Image runat="server" ID="imgTpAnexo" ImageUrl='<%# Eval("URL_TP_ANEXO") %>'
                                        Width="16" Height="16" Style="margin: 0 0 0 -3px !important;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NM_TITULO" HeaderText="IDENTIFICAÇÃO DO ARQUIVO">
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="OBSERVAÇÕES">
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidIdAnexo" Value='<%# Eval("ID_ANEXO_ATEND") %>' />
                                    <asp:Label ID="Label2" Text='<%# Eval("DE_OBSER_RES") %>' ToolTip='<%# Eval("DE_OBSER") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SOLICITANTE" HeaderText="SOLICITANTE">
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="BX">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgbBxrAnexo" ImageUrl="/Library/IMG/Gestor_ServicosDownloadArquivos.png"
                                        ToolTip="Baixar Arquivo" OnClick="imgbBxrAnexo_OnClick" Width="16" Height="16"
                                        Style="margin: 0 0 0 -3px !important;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EX">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgbExcAnexo" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                        ToolTip="Excluir Arquivo" OnClick="imgbExcAnexo_OnClick" Width="16" Height="16"
                                        Style="margin: 0 0 0 -3px !important;" OnClientClick="return confirm ('Tem certeza de que deseja excluir o arquivo?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li class="liTituloGrid" style="width: 865px !important; height: 20px !important;
                margin-top: 15px; background-color: #A9E2F3; margin-bottom: 2px; padding-top: 2px;
                clear: both;">
                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; float: left;
                    margin-left: 10px;">
                    INCLUSÃO DE ARQUIVO AO PACIENTE</label>
            </li>
            <li style="clear: both;">
                <label>
                    Tipo</label>
                <asp:DropDownList ID="drpTipoAnexo" Height="19px" runat="server">
                    <asp:ListItem Text="Anexo" Value="A" Selected="True" />
                    <asp:ListItem Text="Audio" Value="U" />
                    <asp:ListItem Text="Foto" Value="F" />
                    <asp:ListItem Text="Video" Value="V" />
                </asp:DropDownList>
            </li>
            <li>
                <label>
                    Arquivo</label>
                <asp:FileUpload ID="flupAnexo" runat="server" ClientIDMode="Static" />
            </li>
            <li>
                <label>
                    Identificação do Arquivo</label>
                <asp:TextBox ID="txtNomeAnexo" runat="server" Width="200px" Height="17px" ClientIDMode="Static" />
            </li>
            <li>
                <label>
                    Observações</label>
                <asp:TextBox ID="txtObservAnexo" TextMode="MultiLine" Width="318px" Height="17px"
                    runat="server" />
            </li>
            <li class="liBtnAddA" style="margin: 5px 10px 0 0; float: right; width: 45px;">
                <asp:LinkButton ID="lnkbAnexar" runat="server" OnClick="lnkbAnexar_OnClick">
                    <label style="margin-left:5px;">ANEXAR</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <asp:HiddenField runat="server" ID="IdTrataPlano" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idProcMedi" ClientIDMode="Static" />
    <div id="divPlanejamento" style="display: none; height: 450px !important; width: 450px">
        <ul>
            <li class="liTituloGrid" style="width: 690px !important; height: 20px !important;
                background-color: #EEEEE0; text-align: center; font-weight: bold; padding-top: 2px;
                margin: 0 0 0 5px;">
                <ul>
                    <li style="margin: 0px 0 0 10px;">
                        <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #000000">
                            GRID DE PROCEDIMENTOS HOMOLOGADOS</label>
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin: -2px 0 0 5px !important;">
                <div style="width: 688px; height: 130px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdPlanejamento" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum Procedimento associado no orçamento<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CK">
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidIdTrataPlano" Value='<%# Eval("ID_TRATA_PLANO") %>' />
                                    <asp:HiddenField runat="server" ID="hidIdAtendOrcam" Value='<%# Eval("ID_ATEND_ORCAM") %>' />
                                    <asp:HiddenField runat="server" ID="hidIdProcMediProce" Value='<%# Eval("ID_PROC_MEDI_PROCE") %>' />
                                    <asp:HiddenField runat="server" ID="hidIdAtendAgend" Value='<%# Eval("ID_ATEND_AGEND") %>' />
                                    <asp:CheckBox runat="server" ID="chkPlanejamento" ToolTip="Indica se o procedimento foi realizado"
                                        OnCheckedChanged="chkPlanejamento_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CÓDIGO">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCodigoProcPlanejamento" Text='<%# Eval("CO_PROCE") %>'
                                        ToolTip="Código do procedimento" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                        Enabled="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PROCEDIMENTO">
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblProcedimentoPlanejamento" Text='<%# Eval("NO_PROCE") %>'
                                        ToolTip="Descrição do procedimento" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                        Enabled="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nº DENTE">
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNumDentePlanejamento" Text='<%# Eval("NU_DENTE") %>'
                                        ToolTip="Número do dente" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                        Enabled="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AGENDA">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" Enabled="false" ID="chkAgendaDefinidaPlanejamnto" Checked='<%# Eval("IS_CHECK_PLAN") %>'
                                        ToolTip="Indica se o procedimento foi agendado" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OBSERVAÇÃO">
                                <ItemStyle Width="250px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox Style="width: 98%; margin: 0" runat="server" ID="txtObervacaoPlanejamento"
                                        Text='<%# Eval("DE_OBSER") %>' ToolTip="Observação referente a este planejamento"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li class="liTituloGrid" style="width: 502px !important; height: 20px !important;
                background-color: #EEEEE0; text-align: center; font-weight: bold; padding-top: 2px;
                margin: 10px 0 0 104px;">
                <ul>
                    <li style="margin: 0px 0 0 10px;">
                        <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #000000">
                            AGENDA PROFISSIONAL</label>
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin: 0 0 0 104px !important;">
                <div style="width: 500px; height: 180px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdAgendaPlanejamento" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum registro encontrado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CK">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidIdTrataPlano" />
                                    <asp:HiddenField runat="server" ID="hidIdProcMediProceAgenda" />
                                    <asp:HiddenField runat="server" ID="hidIdAtendHorar" Value='<%# Eval("ID_ATEND_HORAR") %>' />
                                    <asp:CheckBox runat="server" ID="chkAgendaPlanejamento" Checked='<%# Eval("CK_ATEND_HORAR") %>'
                                        AutoPostBack="true" ToolTip="Indica se o horário foi agendado" OnCheckedChanged="chkAgendaPlanejamento_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DATA">
                                <ItemStyle Width="30px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDataAgendaPlanejamento" Text='<%# Eval("DATA_AGENDA") %>'
                                        ToolTip="Data do agendamento" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                        Enabled="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HORA">
                                <ItemStyle Width="30px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHoraAgendaPlanejamento" Text='<%# Eval("HR_ATEND_HORAR") %>'
                                        ToolTip="Hora do agendamento" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                        Enabled="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LOCAL">
                                <ItemStyle Width="80px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocalAgendaPlanejamento" Text='<%# Eval("LOCAL") %>'
                                        ToolTip="Local onde ocorrerá a realização do procedimento" Width="100%" Style="margin-left: -4px;
                                        margin-bottom: 0px;" Enabled="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CONSULTA">
                                <ItemStyle Width="80px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblConsultaAgendaPlanejamento" Text='<%# Eval("tpAgenda") %>'
                                        ToolTip="Tipo de consulta agendada" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                        Enabled="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="display: flex;">
                <div style="background-color: #ADD8E6; width: 20px; height: 10px; border: solid 1px;
                    margin: 1px 2px 0 104px">
                </div>
                Agendado para o procedimento selecionado
                <div style="background-color: #ffe4c4; width: 20px; height: 10px; border: solid 1px;
                    margin: 1px 2px 0 10px">
                </div>
                Agendado para outro tratamanto ou consulta </li>
            <li style="clear: both; width: 695px;">
                <ul>
                    <li style="float: right; margin-top: 18px;">
                        <asp:Button runat="server" ID="btnSalvarAgendaPlanejamento" Text="SALVAR" Style="height: 20px;
                            background-color: #0b3e6f; color: #fff; cursor: pointer; width: 56px" Font-Bold="true"
                            OnClick="btnSalvarAgendaPlanejamento_Click" />
                    </li>
                </ul>
            </li>
        </ul>
    </div>
    <div id="divProntuarioOdonto" style="display: none; height: 427px !important; width: 450px">
        <ul class="ulDados">
            <li style="margin: -24px 0 9px 198px;">
                <asp:Label ID="lblNomeModalProntOdonto" runat="server"></asp:Label>
            </li>
            <li style="margin: -12px 0 9px 150px;">
                <div>
                    <img style="margin-top: -2px; margin-left: 18px;" src="/Library/IMG/imgOdontograma.png"
                        height="223px" width="550px" />
                </div>
            </li>
            <li class="liTituloGrid" style="width: 820px; height: 20px !important; margin-left: -0px;
                background-color: #EEEEE0; text-align: center; font-weight: bold; float: left;
                clear: both">
                <ul>
                    <li style="display: flex; margin: 3px;">
                        <label title="Período em que os procedimentos foram planejados/realizados" style="margin: 0 4px 0 0">
                            PERÍODO:</label>
                        <asp:TextBox ID="txtDtInicialProntuarioOdonto" runat="server" ValidationGroup="guia"
                            class="campoData" ToolTip="Informe a data de inicial" />
                        <label title="" class="" style="margin: 0 6px 0 6px;">
                            a</label>
                        <asp:TextBox ID="txtDtFinalProntuarioOdonto" runat="server" ValidationGroup="guia"
                            class="campoData" ToolTip="Informe a data de final" Style="" />
                        <asp:ImageButton ID="imgPesqProcAcoesPO" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            Width="13px" Height="13px" Style="margin: 0 0px 0 6px" OnClick="imgPesqProcAcoesPO_OnClick" />
                    </li>
                </ul>
            </li>
            <li title="Clique para emitir o Prontuário" class="" style="width: 12px; height: 15px;
                margin: 2px 0 0px 0px;">
                <asp:ImageButton ID="imgbProntuario" runat="server" ToolTip="Emitir Printuário do Paciente"
                    OnClick="lnkbProntuario_OnClick" Style="margin-top: -2px; margin-left: -3px;"
                    ImageUrl="/BarrasFerramentas/Icones/Imprimir.png" Height="18px" Width="18px" />
            </li>
            <li style="clear: both; margin: -7px 0 0 0;">
                <div style="width: 839px; height: 150px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdProntuarioOdonto" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhuma ação planejada para este paciente<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="lblND" Font-Bold="true" Text="ND" ToolTip="Número do dente"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidAtendOrcam" />
                                    <asp:Label runat="server" ID="lblPONumDente" ToolTip="Número do dente" Text='<%# Eval("nuDente") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="lblSitu" Text="SIT" Font-Bold="true" ToolTip="Situação do procedimento (realizado ou não)"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <img class="imgsa" style="width: 16px; height: 16px !important" src='<%# Eval("imagem_URL_ACAO") %>'
                                        alt="Icone" title="Indica se o procedimento foi realizado" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="lblRealiz" Text="REALIZ" Font-Bold="true" ToolTip="Data no qual o procedimento foi realizado"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPODtRealizado" ToolTip="Data no qual o procedimento foi realizado"
                                        Text='<%# Eval("DT_REALIZADO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="lblProcedimento" Text="PROCEDIMENTO/AÇÃO" Font-Bold="true"
                                        ToolTip="Nome do procedimento/ação"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPOProcAcao" ToolTip="Nome do procedimento/ação"
                                        Text='<%# Eval("nomeProcedimento") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="lblProfissional" Text="PROFISSIONAL" Font-Bold="true"
                                        ToolTip="Nome do profissional que realizou/solicitou o/a procedimento/ação"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPOProfissional" ToolTip="Nome do profissional que realizou/solicitou o/a procedimento/ação"
                                        Text='<%# Eval("nomeProfissional") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="lblLocal" Text="LOCAL" Font-Bold="true" ToolTip="Local onde o procedimento será/foi realizado"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Width="70px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPOLocal" ToolTip="Local onde o procedimento será/foi realizado"
                                        Text='<%# Eval("LOCAL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="lblExame" Text="EX" ToolTip="Exames associados ao paciente"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbProntuario" runat="server" ToolTip="Visualizar exames do paciente"
                                        Style="margin-top: -2px; margin-left: -3px;"
                                        ImageUrl="/Library/IMG/Gestor_IcoPesquisa.png" Height="16px" Width="16px" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="lblOrcamento" Font-Bold="true" Text="ORÇAMENTO" ToolTip="Data do orçamento ao qual o procedimento/ação se refere"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPOOrcamento" ToolTip="Data do orçamento ao qual o procedimento/ação se refere"
                                        Text='<%# Eval("DT_ORCAMENTO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="clear: both; margin: 3px 0 0 0;"></li>
        </ul>
    </div>
    <asp:HiddenField runat="server" ID="hidIdAnamneseRapida" />
    <div id="divAnamRapida" style="display: none; height: 485px !important; width: 900px">
        <ul class="ulDados">
            <li style="width: 430px; border-right: solid 1px #BDBDBD">
                <ul>
                    <li style="clear: both; color: Blue;">
                        <asp:Label runat="server">QUAL O MOTIVO DA CONSULTA?</asp:Label>
                    </li>
                    <li style="clear: both;">
                        <textarea runat="server" id="txtAnamRapMotivoCosulta" rows="2" cols="50" onkeydown="checkTextAreaMaxLength(this,event,'100');"
                            style="background-color: #F1F1F1; font-size: 12px; width: 422px; margin-top: 1px;
                            border-top: 1px solid #BDBDBD; border-left: 0; border-right: 0; border-bottom: 0;"
                            title="Motivo pelo qual a consulta é realizada (máximo de 100 caracteres)" />
                    </li>
                    <li style="clear: both; margin: 0;">
                        <ul>
                            <li style="clear: both; color: Blue;">
                                <label>
                                    SAÚDE BUCAL</label>
                            </li>
                            <li style="clear: both; color: Black;">
                                <label style="width: 422px; border-bottom: 1px solid #BDBDBD;">
                                    APRESENTA ALGUNS DOS PROBLEMAS ABAIXO LISTADOS?</label>
                            </li>
                            <li style="clear: both;">
                                <asp:CheckBox runat="server" ID="chkAnamRapRoerUnha" Style="clear: both;" /><span>Roer
                                    unhas</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapRangerDente" Style="clear: both;" /><span>Ranger
                                    dentes</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapMauHalito" Style="clear: both;" /><span>Mau
                                    hálito</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapAcumAlimenDente" Style="clear: both;" /><span>Acúmulo
                                    de alimento entre os dentes</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapATM" Style="clear: both;" /><span>Estalos
                                    ou dor nas articulações da mandibula (ATM)</span> </li>
                            <li>
                                <asp:CheckBox runat="server" ID="chkAnamRapApertaDente" Style="clear: both;" /><span>Apertamento
                                    dentário</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapSangraDente" Style="clear: both;" /><span>Sangramento
                                    gengival</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapTartaro" Style="clear: both;" /><span>Tártaro</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapZumbidoOuvido" Style="clear: both;" /><span>Zumbido
                                    no ouvido</span> </li>
                            <li style="clear: both; color: Black;">
                                <label style="width: 422px; border-bottom: 1px solid #BDBDBD;">
                                    ÚLTIMO TRATAMENTO ODONTOLÓGICO:</label>
                            </li>
                            <li style="clear: both;">
                                <asp:DropDownList runat="server" ID="ddlTrataOdonto" ClientIDMode="Static" Width="100px">
                                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Menos de 01 ano" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="De 01 a 03 anos" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Mais de 03 anos" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:CheckBox runat="server" ID="chkAnamRapTrata01ano" ClientIDMode="Static" /><span>Menos
                                    de 01 ano</span>
                                <asp:CheckBox runat="server" ID="chkAnamRapTrata01a03anos" ClientIDMode="Static" /><span>De
                                    01 a 03 anos</span>
                                <asp:CheckBox runat="server" ID="chkAnamRapTrata03anos" ClientIDMode="Static" /><span>Mais
                                    de 03 anos</span>--%>
                            </li>
                            <li style="clear: both; color: Black;">
                                <label style="width: 422px; border-bottom: 1px solid #BDBDBD;">
                                    O QUE GOSTARIA DE MELHORAR NA SUA BOCA?</label>
                            </li>
                            <li style="clear: both">
                                <asp:CheckBox runat="server" ID="chkAnamRapGengiva" /><span>Gengiva</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapRestauraMetalica" /><span>Restaurações metálicas</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapFormaDente" /><span>Forma dos dentes</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapRestauraAntiga" /><span>Restaurações antigas</span><br />
                            </li>
                            <li>
                                <asp:CheckBox runat="server" ID="chkAnamRapCorDente" /><span>Cor dos dentes</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapImplanteDente" /><span>Implante dentário</span><br />
                                <asp:CheckBox runat="server" ID="chkAnamRapProtese" /><span>Prótese</span> </li>
                            <li style="clear: both; color: Black;">
                                <label>
                                    GOSTARIA DE FAZER ALGUMA OBSERVAÇÃO PARA MELHOR SER ATENDIDO?</label>
                            </li>
                            <li style="clear: both">
                                <textarea runat="server" id="txtAnamRapObsMelhorAtendido" rows="2" cols="50" onkeydown="checkTextAreaMaxLength(this,event,'100');"
                                    style="background-color: #F1F1F1; font-size: 12px; width: 422px; margin-top: 1px;
                                    border-top: 1px solid #BDBDBD; border-left: 0; border-right: 0; border-bottom: 0;"
                                    title="Motivo pelo qual a consulta é realizada (máximo de 100 caracteres)" />
                            </li>
                        </ul>
                    </li>
                    <li style="clear: both; margin: 0;">
                        <ul>
                            <li style="clear: both; color: Blue;">
                                <label>
                                    SAÚDE GERAL</label>
                            </li>
                            <li style="margin: 0; clear: both">
                                <ul>
                                    <li style="clear: both; color: Black;">
                                        <label style="width: 189px; border-bottom: 1px solid #BDBDBD;">
                                            QUAL O ESTADO DE SAÚDE ATUALMENTE?</label>
                                    </li>
                                    <li style="clear: both;">
                                        <asp:DropDownList runat="server" ID="ddlEstadoSaude" ClientIDMode="Static">
                                            <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Péssimo" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Ruim" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Regular" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Bom" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Ótimo" Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:CheckBox runat="server" ID="chkAnamRapPessimo" Style="clear: both;" /><span>Péssimo</span>
                                        <asp:CheckBox runat="server" ID="chkAnamRapRuin" Style="clear: both;" /><span>Ruim</span>
                                        <asp:CheckBox runat="server" ID="chkAnamRapRegular" Style="clear: both;" /><span>Regular</span>
                                        <asp:CheckBox runat="server" ID="chkAnamRapBom" Style="clear: both;" /><span>Bom</span>
                                        <asp:CheckBox runat="server" ID="chkAnamRapOtimo" Style="clear: both;" /><span>Ótimo</span>--%>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: 0;">
                                <ul>
                                    <li style="clear: both; color: Black; margin-top: 12px;">
                                        <label style="width: 106px; border-bottom: 1px solid #BDBDBD;">
                                            FUMA?</label>
                                    </li>
                                    <li style="clear: both;">
                                        <asp:DropDownList runat="server" ID="ddlFuma" ClientIDMode="Static">
                                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:CheckBox runat="server" ID="chkAnamRapFumaSim" Style="clear: both;" /><span>Sim</span>
                                        <asp:CheckBox runat="server" ID="chkAnamRapFumaNao" Style="clear: both;" /><span>Não</span>--%>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: 0;">
                                <ul>
                                    <li style="clear: both; color: Black; margin-top: 12px;">
                                        <label style="width: 105px; border-bottom: 1px solid #BDBDBD;">
                                            BEBE?</label>
                                    </li>
                                    <li style="clear: both;">
                                        <asp:DropDownList runat="server" ID="ddlBebe" ClientIDMode="Static">
                                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:CheckBox runat="server" ID="chkAnamRapBebeSim" Style="clear: both;" /><span>Sim</span>
                                        <asp:CheckBox runat="server" ID="chkAnamRapBebeNao" Style="clear: both;" /><span>Não</span>--%>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li style="width: 430px;">
                <ul>
                    <li style="margin: 0; clear: both">
                        <ul>
                            <li style="clear: both; color: Black;">
                                <label style="width: 234px; border-bottom: 1px solid #BDBDBD;">
                                    ESTÁ SOB CUIDADOS MÉDICOS ATUALMENTE?</label>
                            </li>
                            <li style="clear: both;">
                                <asp:DropDownList runat="server" ID="AnamRapCuidadoMedico" ClientIDMode="Static">
                                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:CheckBox runat="server" ID="chkAnamRapCuidadoMedicoSim" Style="clear: both;" /><span>Sim</span>
                                        <asp:CheckBox runat="server" ID="chkAnamRapCuidadoMEdicoNao" Style="clear: both;" /><span>Não</span>--%>
                            </li>
                        </ul>
                    </li>
                    <li style="margin: 0;">
                        <ul>
                            <li style="color: Black;">
                                <label style="width: 176px; border-bottom: 1px solid #BDBDBD;">
                                    ÚLTIMO TRATAMENTO MÉDICO:</label>
                            </li>
                            <li style="clear: both;">
                                <asp:DropDownList runat="server" ID="AnamRapTrataMedic" ClientIDMode="Static" Width="100px">
                                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Menos de 01 ano" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="De 01 a 03 anos" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Mais de 03 anos" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:CheckBox runat="server" ID="chkAnamRapTrataMedic01ano" /><span>Menos de 01 ano</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapTrataMedic01a03anos" /><span>De 01 a 03 anos</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapTrataMedic03anos" /><span>Mais de 03 anos</span>--%>
                            </li>
                        </ul>
                    </li>
                    <li style="clear: both; color: Black;">
                        <label style="width: 420px; border-bottom: 1px solid #BDBDBD;">
                            ESTÁ SOB TRATAMENTO CONTROLADO?</label>
                    </li>
                    <li style="clear: both;">
                        <asp:DropDownList runat="server" ID="AnamRapTrata" ClientIDMode="Static" AutoPostBack="true"
                            OnSelectedIndexChanged="AnamRapTrata_SelectedIndexChanged">
                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                            <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:CheckBox runat="server" ID="chkAnamRapTrataSim" Style="clear: both;" /><span>Sim</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapTrataNao" Style="clear: both;" /><span>Não</span>--%>
                        <span style="margin-left: 10px;">Se sim, qual o motivo?</span>
                        <asp:TextBox runat="server" ID="txtAnamRapTrataSim" Enabled="false" onkeydown="checkTextAreaMaxLength(this,event,'50');"
                            Style="background-color: #F1F1F1; font-size: 10px; width: 227px; height: 13px;
                            margin-top: 1px; border-left: 0; border-right: 0; border-bottom: 0; margin-bottom: 0;"
                            title="Motivo pelo qual o paciente está em tratamento controlado (máximo de 50 caracteres)"></asp:TextBox>
                    </li>
                    <li style="clear: both; color: Black;">
                        <label style="width: 420px; border-bottom: 1px solid #BDBDBD;">
                            DOENÇAS QUE JÁ TEVE:</label>
                    </li>
                    <li style="clear: both;">
                        <asp:CheckBox runat="server" ID="chkAnamRapArticulares" /><span style="margin-right: 9px;">Articulares</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapCardioVasculares" /><span>Cárdio Vasculares</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapEndocrinas" /><span>Endócrinas</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapGastricas" /><span>Gástricas</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapHepaticas" Style="margin-left: 11px;" /><span>Hepáticas</span>
                    </li>
                    <li style="">
                        <asp:CheckBox runat="server" ID="chkAnamRapNeurologicas" /><span>Neurológicas</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapPulmonares" /><span>Pulmonares</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapRenais" Style="margin-left: 28px;" /><span>Renais</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapSanguineas" Style="margin-left: 19px;" /><span>Sanguíneas</span>
                        <asp:CheckBox runat="server" ID="chkAnamRapNenhumaDoenca" /><span>Nenhuma</span>
                    </li>
                    <li>
                        <asp:CheckBox runat="server" ID="chkAnamRapOutrasDoenca" OnCheckedChanged="chkAnamRapOutrasDoenca_CheckedChanged"
                            AutoPostBack="true" /><span>Outras:</span>
                        <asp:TextBox runat="server" ID="txtAnamRapDeOutrasDoenca" Enabled="false" onkeydown="checkTextAreaMaxLength(this,event,'50');"
                            Style="background-color: #F1F1F1; font-size: 10px; width: 358px; height: 13px;
                            margin-top: 1px; border-left: 0; border-right: 0; border-bottom: 0; margin-bottom: 0;"
                            title="Doenças que o paciente já teve (máximo de 50 caracteres)"></asp:TextBox>
                    </li>
                    <li style="margin: 0;">
                        <ul>
                            <li style="clear: both; color: Black;">
                                <label style="width: 418px; border-bottom: 1px solid #BDBDBD;">
                                    FAZ USO DE MEDICAMENTO NO MOMENTO?</label>
                            </li>
                            <li style="clear: both;">
                                <asp:DropDownList runat="server" ID="AnamRapMedicAtual" ClientIDMode="Static" OnSelectedIndexChanged="AnamRapMedicAtual_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:CheckBox runat="server" ID="chkAnamRapMedicAtualSim" Style="clear: both;" /><span>Sim</span>
                                <asp:CheckBox runat="server" ID="chkAnamRapMedicAtualNao" Style="clear: both;" /><span>Não</span>--%>
                                <span style="margin-left: 10px;">Se sim, qual?</span>
                                <asp:TextBox runat="server" ID="txtAnamRapMedicAtualQual" Enabled="false" onkeydown="checkTextAreaMaxLength(this,event,'30');"
                                    Style="background-color: #F1F1F1; font-size: 10px; width: 264px; height: 13px;
                                    margin-top: 1px; border-left: 0; border-right: 0; border-bottom: 0; margin-bottom: 0;"
                                    title="Nome do medicamento que o paciente está fazendo uso (máximo de 30 caracteres)"></asp:TextBox>
                            </li>
                        </ul>
                    </li>
                    <li style="margin: 0;">
                        <ul>
                            <li style="clear: both; color: Black;">
                                <label style="width: 418px; border-bottom: 1px solid #BDBDBD;">
                                    TEM ALERGIAS A QUAIS MEDICAMENTOS?</label>
                            </li>
                            <li style="clear: both;">
                                <asp:CheckBox runat="server" ID="chkAnamRapAlergAnestesico" Style="clear: both;" /><span>Anestésicos</span>
                                <asp:CheckBox runat="server" ID="chkAnamRapAlergAntibiotico" Style="clear: both;" /><span>Antibióticos</span>
                                <asp:CheckBox runat="server" ID="chkAnamRapAlergIodo" Style="clear: both;" /><span>Iodo</span>
                                <asp:CheckBox runat="server" ID="chkAnamRapAlergNenhum" Style="clear: both;" /><span>Nenhum</span>
                            </li>
                            <li style="clear: both;">
                                <asp:CheckBox runat="server" ID="chkAnamRapAlergOutros" Style="clear: both;" OnCheckedChanged="chkAnamRapAlergOutros_CheckedChanged"
                                    AutoPostBack="true" /><span>Outros:</span>
                                <asp:TextBox runat="server" ID="txtAnamRapAlerg" Enabled="false" onkeydown="checkTextAreaMaxLength(this,event,'30');"
                                    Style="background-color: #F1F1F1; font-size: 10px; width: 357px; height: 13px;
                                    margin-top: 1px; border-left: 0; border-right: 0; border-bottom: 0; margin-bottom: 0;"
                                    title="Nome do medicamento que o paciente tem alergia (máximo de 30 caracteres)"></asp:TextBox>
                            </li>
                        </ul>
                    </li>
                    <li style="clear: both; margin: 11px 0 0 0;">
                        <ul>
                            <li style="clear: both; color: #e91e63;">
                                <label>
                                    PARA MULHERES</label>
                            </li>
                            <li style="clear: both; margin: 0;">
                                <ul>
                                    <li style="clear: both; color: Black;">
                                        <label style="width: 120px; border-bottom: 1px solid #BDBDBD;">
                                            MENSTRUAÇÃO REGULAR?</label>
                                    </li>
                                    <li style="clear: both;">
                                        <asp:DropDownList runat="server" ID="AnamRapMenstrua" ClientIDMode="Static">
                                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Não" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="inda não menstrua" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Não menstrua mais" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:CheckBox runat="server" ID="chkAnamRapMenstruaSim" /><span style="margin-right: 9px;">Sim</span>
                                <asp:CheckBox runat="server" ID="chkAnamRapMenstruaNao" /><span>Não</span>
                                <asp:CheckBox runat="server" ID="chkAnamRapNaoMenstrua" /><span>Ainda não menstrua</span>
                                <asp:CheckBox runat="server" ID="chkAnamRapNaoMenstruaMais" /><span>Não menstrua mais</span>--%>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: 0;">
                                <ul>
                                    <li style="clear: both; color: Black;">
                                        <label style="width: 197px; border-bottom: 1px solid #BDBDBD;">
                                            ALTERAÇÃO GENGIVAL NA MENSTRUAÇÃO?</label>
                                    </li>
                                    <li style="clear: both;">
                                        <asp:DropDownList runat="server" ID="AnamRapMenstruaAlterGengiva" ClientIDMode="Static">
                                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:CheckBox runat="server" ID="chkAnamRapMenstruaAlterGengivaSim" /><span style="margin-right: 9px;">Sim</span>
                                                <asp:CheckBox runat="server" ID="chkAnamRapMenstruaAlterGengivaNao" /><span>Não</span>--%>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: 0;">
                                <ul>
                                    <li style="color: Black; margin-top: 12px;">
                                        <label style="width: 80px; border-bottom: 1px solid #BDBDBD;">
                                            GRÁVIDA?</label>
                                    </li>
                                    <li style="clear: both;">
                                        <asp:DropDownList runat="server" ID="AnamRapGravida" ClientIDMode="Static">
                                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:CheckBox runat="server" ID="ChchkAnamRapGravidaSim" /><span style="margin-right: 9px;">Sim</span>
                                                <asp:CheckBox runat="server" ID="chkAnamRapGravidaNao" /><span>Não</span> --%></li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li style="margin: 0;">
                        <ul>
                            <li style="clear: both; color: Black;">
                                <label style="width: 418px; border-bottom: 1px solid #BDBDBD;">
                                    UTILIZA ANTICONCEPCIONAL?</label>
                            </li>
                            <li style="clear: both;">
                                <asp:DropDownList runat="server" ID="AnamRapAntiConcepcional" ClientIDMode="Static"
                                    OnSelectedIndexChanged="AnamRapAntiConcepcional_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:CheckBox runat="server" ID="chkAnamRapAntiConcepcionalSim" /><span style="margin-right: 9px;">Sim</span>
                                        <asp:CheckBox runat="server" ID="chkAnamRapAntiConcepcionalNao" /><span>Não</span>--%>
                                <span style="margin-left: 10px;">Se sim, qual?</span>
                                <asp:TextBox runat="server" ID="txAnamRapAntiConcepcionalNome" Enabled="false" onkeydown="checkTextAreaMaxLength(this,event,'30');"
                                    Style="background-color: #F1F1F1; font-size: 10px; width: 264px; height: 13px;
                                    margin-top: 1px; border-left: 0; border-right: 0; border-bottom: 0;" title="Anticoncepcional utilizado (máximo de 30 caracteres)"></asp:TextBox>
                                <span style="margin-left: 10px;">Se sim, para?</span>
                                <asp:DropDownList runat="server" ID="RapAntiConcepcionalMotivo" Enabled="false" ClientIDMode="Static"
                                    Width="106px">
                                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Evitar a gestação" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Controle hormonal" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:CheckBox runat="server" ID="chkAnamRapAntiConcepcionalEvitGest" /><span style="margin-right: 9px;">Evitar
                                    a gestação</span>
                                <asp:CheckBox runat="server" ID="chkAnamRapAntiConcepcionalContHormonal" /><span>Controle
                                    hormonal</span> --%>
                            </li>
                        </ul>
                    </li>
                </ul>
            </li>
        </ul>
        <ul>
            <li style="margin: 0 0 0 814px; clear: both">
                <asp:Button runat="server" ID="btnSalvarAnamRap" Text="SALVAR" Style="height: 20px;
                    background-color: #0b3e6f; color: #fff; cursor: pointer; width: 56px" Font-Bold="true"
                    OnClick="btnSalvarAnamRap_Click" />
            </li>
        </ul>
    </div>
    <script src="/Library/JS/Cronometro.js" type="text/javascript"></script>
    <script type="text/javascript">

        window.onload = function () {
            MaintainScroll1();
            $("#divOcorrencia").show();
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
            $("#flupAnexo").change(function () {
                if ($("#txtNomeAnexo").val() == "") {
                    var fileName = $("#flupAnexo").val();
                    while (fileName.indexOf("\\") != -1)
                        fileName = fileName.slice(fileName.indexOf("\\") + 1);
                    $("#txtNomeAnexo").val(fileName);
                }
            });
        });

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

        function AbreModalAnamRApida() {
            $('#divAnamRapida').dialog({ autoopen: false, modal: true, width: 913, height: 490, resizable: false, title: "ANAMNESE RÁPIDA",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalLog() {
            $('#divLoadShowLogAgenda').dialog({ autoopen: false, modal: true, width: 902, height: 340, resizable: false, title: "HISTÓRICO DO AGENDAMENTO DE ATENDIMENTO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalNovoMedic() {
            $('#divLoadShowNovoMedic').dialog({ autoopen: false, modal: true, width: 360, height: 390, resizable: false, title: "NOVO MEDICAMENTO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalNovoExam() {
            $('#divLoadShowNovoExam').dialog({ autoopen: false, modal: true, width: 560, height: 300, resizable: false, title: "NOVO EXAME",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalAtestado() {
            $('#divAtestado').dialog({ autoopen: false, modal: true, width: 530, height: 370, resizable: false, title: "EMISSÃO DE DOCUMENTO",
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

        function AbreModalOdontograma() {
            $('#divOdontograma').dialog({ autoopen: false, modal: true, width: 750, height: 450, resizable: false, title: "ODONTOGRAMA DO PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalLaudo() {
            $('#divLaudo').dialog({ autoopen: false, modal: true, width: 555, height: 350, resizable: false, title: "LAUDO TÉCNICO",
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

        function AbreModalObservacao() {
            $('#divObservacao').dialog({ autoopen: false, modal: true, width: 560, height: 320, resizable: false, title: "OBSERVAÇÕES DO ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalFichaAtendimento() {
            $('#divFichaAtendimento').dialog({ autoopen: false, modal: true, width: 470, height: 470, resizable: false, title: "IMPRESSÃO DA FICHA DE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalMedicamentos() {
            $('#divMedicamentos').dialog({ autoopen: false, modal: true, width: 800, height: 430, resizable: false, title: "MEDICAMENTOS DO PACIENTE NESTE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalExames() {
            $('#divExames').dialog({ autoopen: false, modal: true, width: 460, height: 200, resizable: false, title: "EXAMES DO PACIENTE NESTE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalPlanejamento() {
            $('#divPlanejamento').dialog({ autoopen: false, modal: true, width: 718, height: 460, left: 38, top: 26, resizable: false, title: "TRATAMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalOrcamentos() {
            $('#divOrcamentos').dialog({ autoopen: false, modal: true, width: 911, height: 460, left: 38, top: 26, resizable: false, title: "ORÇAMENTO DO PACIENTE NESTE ATENDIMENTO",
                open: function (type, data) { $(this).parent().appendTo("form"); }
                //close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalAnexos() {
            $('#divAnexos').dialog({ autoopen: false, modal: true, width: 880, height: 365, resizable: false, title: "ARQUIVOS ANEXADOS AO(S) ATENDIMENTO(S)",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalProntuarioOdonto() {
            $('#divProntuarioOdonto').dialog({ autoopen: false, modal: true, width: 861, height: 450, resizable: false, title: "PRONTUÁRIO DE AÇÕES DO PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        jQuery(function ($) {
            $(".campoCodigo").mask("?99999999");
            $(".campoQtd").mask("?999");
            $(".campoDinheiro").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            //Se ao deixar o campo, o mesmo estiver vazio, insere 0
            $("#txtVlCusto").blur(function () {
                if ($("#txtVlCusto").val() == "")
                    $("#txtVlCusto").val("0,00");
            });
            $("#txtVlBase").blur(function () {
                if ($("#txtVlBase").val() == "")
                    $("#txtVlBase").val("0,00");
            });
            $("#txtVlRestitu").blur(function () {
                if ($("#txtVlRestitu").val() == "")
                    $("#txtVlRestitu").val("0,00");
            });

            $("#chkAprovado").change(function () {
                if ($("#chkAprovado").attr("checked"))
                    $("#hidCkOrcamAprov").val('S');
                else
                    $("#hidCkOrcamAprov").val('N');
            });

            $("#txtDtValidade").change(function () {
                $("#hidDtValidOrcam").val($("#txtDtValidade").val());
            });

            $("#txtObserMedicam").change(function () {
                $("#hidObserMedicam").val($("#txtObserMedicam").val());
            });

            $("#txtObserExame").change(function () {
                $("#hidObserExame").val($("#txtObserExame").val());
            });

            $("#txtObsOrcam").change(function () {
                $("#hidObsOrcam").val($("#txtObsOrcam").val());
            });

            if (!($("#rdbMedic").attr("checked"))) {
                $("#txtMedicamento").enable(false);
            };

            if (!($("#rdbPrinc").attr("checked"))) {
                $("#txtPrincipio").enable(false);
            };

            $("#rdbMedic").change(function () {
                if ($("#rdbMedic").attr("checked")) {
                    $("#txtMedicamento").enable(true);
                    $("#txtPrincipio").enable(false);
                }
                else {
                    $("#txtMedicamento").enable(false);
                    $("#txtPrincipio").enable(true);
                }
            });

            $("#rdbPrinc").change(function () {
                if ($("#rdbPrinc").attr("checked")) {
                    $("#txtPrincipio").enable(true);
                    $("#txtMedicamento").enable(false);
                }
                else {
                    $("#txtPrincipio").enable(false);
                    $("#txtMedicamento").enable(true);
                }
            });
        });

    </script>
</asp:Content>
