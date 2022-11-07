<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8272_AtendimentoRegistroInternar.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--CSS-->
    <style type="text/css">
        .divAvisoPermissao
        {
            top: 516px !important;
            left: 390px !important;
        }
        
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
        
        .liBtnConfirmarCiencia
        {
            width: 47px;
            background-color: #d09ad1;
            margin-left: 115px;
            margin-top: 10px;
            cursor: pointer;
            border: 1px solid #8B8989;
            padding: 4px 3px 3px;
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
            margin-left: 201px;
            margin-top: -26px;
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
        .spInf
        {
            cursor: pointer;
        }
        #cklExameFis tr td
        {
            display: flex;
        }
        .lilnkExaFis
        {
            float: right !important;
            width: 48px;
            height: 15px;
            text-align: -webkit-center;
            border: solid 1px #CCC;
            margin: 0;
            background: #f0f8ff;
        }
        .lnkExcluir
        {
            border: none;
            background-color: #fff;
            color: red;
            font-size: 12px;
            cursor: pointer;
        }
        .modalInf
        {
            z-index: 2000;
            position: absolute;
            background-color: #faebd7;
            top: 34%;
            left: 26%;
            width: 474px;
        }
        .invible
        {
            display: none;
        }
        .indice
        {
            z-index: 2000;
            position: absolute;
            background-color: #fff;
            top: 34%;
            left: 26%;
            padding: 0 20px 20px 20px;
            border-radius: 5px;
        }
        #divBack
        {
            z-index: 1;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            position: absolute;
            opacity: 0.3;
            display: none;
            background: url(images/ui-bg_diagonals-small_100_000000_40x40.png) 50% 50% repeat rgb(0, 0, 0);
        }
        .parecerTec
        {
            z-index: 1000;
            position: absolute;
            background-color: #fff;
            top: 10%;
            left: 19%;
            padding: 0 20px 20px 20px;
            border-radius: 5px;
        }
        .centerCHC
        {
            text-align: center !important;
        }
        .rowColor
        {
            background-color: red;
        }
        .btnProcurar
        {
            margin-top: -3px;
        }
        .mgLeft
        {
            margin-left: 169px;
        }
        .txtNomePacienteInternar
        {
            height: 23px;
            border-radius: 2px;
            background-color: #f6f6f6;
        }
        .colorTextBlue
        {
            color: #0000ff;
        }
        .colorTextRed
        {
            color: #cd0a0a;
        }
        .magin0
        {
            margin: 0 !important;
        }
        .border0
        {
            border: none;
        }
    </style>
    <script src="../../../../../Library/JS/TreeView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="sm" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <ul class="ulDados">
        <li style="margin-left: 3px;">
            <ul>
                <li>
                    <ul>
                        <li class="liTituloGrid" style="width: 479px !important; height: 20px !important;
                            margin-right: 0px; background-color: #DFF1FF; text-align: center; font-weight: bold;
                            margin-bottom: 2px; padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #Black">
                                        PACIENTES</label>
                                </li>
                                <li style="margin-left: 10px; float: right; margin-top: 2px;">
                                    <ul class="ulPer">
                                        <li>
                                            <asp:DropDownList runat="server" ID="ddlOrdenarGrdPaciente" ToolTip="Ordenar por nome ou criticidade">
                                                <asp:ListItem Value="">Ordenar por?</asp:ListItem>
                                                <asp:ListItem Value="1">Nome</asp:ListItem>
                                                <asp:ListItem Value="2">Criticidade</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtNomePacPesqAgendAtend" Width="190px" placeholder="Pesquise pelo Nome"></asp:TextBox>
                                        </li>
                                        <li style="margin: 0px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqAgendamentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqAgendamentos_Click" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both">
                            <div style="width: 479px; height: 120px; border: 1px solid #CCC; overflow-y: scroll"
                                id="divAgendasRecp">
                                <asp:GridView ID="grdPacientes" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                    ShowHeaderWhenEmpty="true">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma registro de ATENDIMENTO<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidIdRegisAtendInter" runat="server" Value='<%# Eval("ID_REGIS_ATEND_INTER") %>' />
                                                <asp:HiddenField ID="hidCoAgendColPlan" runat="server" Value='<%# Eval("CO_AGEND_COL_PLAN") %>' />
                                                <asp:HiddenField ID="hidCoAgendPlant" runat="server" Value='<%# Eval("CO_AGEND_PLANT") %>' />
                                                <asp:HiddenField ID="hidCoAlu" runat="server" Value='<%# Eval("CO_ALU") %>' />
                                                <asp:CheckBox ID="chkSelectPaciente" runat="server" Width="100%" Style="margin: 0 0 0 -15px !important;"
                                                    AutoPostBack="true" OnCheckedChanged="chkSelectPaciente_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="69px" HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Font-Bold="true" ID="lblProntuarioGrid" Text="DATA/HORA"
                                                    ToolTip="Data e hora previstas para o atendimento do paciente"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDataHora" Text='<%# Eval("DTHR") %>' Style="padding: 0px !important;"
                                                    ToolTip="Data e hora previstas para o atendimento do paciente"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LOCAL" HeaderText="LOCAL">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_ALU" HeaderText="PACIENTE">
                                            <ItemStyle HorizontalAlign="Left" Width="127px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="8px" HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Font-Bold="true" ID="lblPrioInter" Text="CR" ToolTip="Criticidade do atendimento"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ForeColor='<%# Eval("COR_PRIORIDADE") %>' BackColor='<%# Eval("COR_PRIORIDADE") %>'
                                                    ToolTip='<%# Eval("TOOLTIP_PRIORIDADE") %>' runat="server" ID="lblPrioridadeInter"
                                                    Style="border: 1px solid BDBDBD;">AA</asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SEXO" HeaderText="SX">
                                            <ItemStyle HorizontalAlign="Center" Width="8px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IDADE" HeaderText="IDADE">
                                            <ItemStyle HorizontalAlign="Left" Width="55px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Font-Bold="true" ID="lblProntuarioGrid" Text="PT" ToolTip="Emitir prontuário do paciente"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgBtnProntuario" OnClick="imgBtnFichaAtend_Click"
                                                    ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_EmissaoGuia.png" Width="15px"
                                                    Height="15px" ToolTip="Emitir prontuário eletrônico do paciente" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Font-Bold="true" ID="lblPacAtendido" Text="AT" ToolTip="Paciente atendido"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <img alt="" src="/Library/IMG/PGS_IC_Positivo.png" runat="server" id="imgPositivo"
                                                    visible='<%# Eval("isAtendido") %>' style="width: 15px; height: 15px" title="Paciente atendido" />
                                                <img alt="" src="/Library/IMG/PGS_IC_Negativo.png" runat="server" id="imgNegativo"
                                                    visible='<%# Eval("naoAtendido") %>' style="width: 15px; height: 15px" title="Paciente não atendido" />
                                                <img alt="" src="/Library/IMG/PGS_IC_Cancelado.png" runat="server" id="imgCancelado"
                                                    visible='<%# Eval("Cancelado") %>' style="width: 15px; height: 15px" title="Atendimento cancelado" />
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
                        <li class="liTituloGrid" style="width: 479px !important; height: 20px !important;
                            margin-right: 0px; background-color: #DFF1FF; text-align: center; font-weight: bold;
                            margin-bottom: 2px; padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: Black">
                                        HISTÓRICO DE ATENDIMENTO</label>
                                </li>
                                <li style="margin-left: 10px; float: right; margin-top: 2px;">
                                    <ul class="ulPer">
                                        <li>
                                            <asp:TextBox runat="server" class="campoData" ID="txtDataIni" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
                                            <asp:TextBox runat="server" class="campoData" ID="txtDataFim" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                        </li>
                                        <li style="margin: 0px 2px 0 -2px;">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqAgendamentos_Click" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="width: 479px; height: 120px; border: 1px solid #CCC; overflow-y: scroll"
                                        id="div1">
                                        <asp:GridView ID="grdHitoricoAtendimento" CssClass="grdBusca" runat="server" Style="width: 100%;
                                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                            ShowHeaderWhenEmpty="true">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhuma AGENDA definida para o profissional<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="DataHora" HeaderText="DATA/HORA" />
                                                <asp:BoundField DataField="Funcao" HeaderText="FUNÇÃO" />
                                                <asp:BoundField DataField="Profissional" HeaderText="PROFISSIONAL" />
                                                <asp:BoundField DataField="Unidade" HeaderText="UNID" />
                                                <asp:BoundField DataField="Local" HeaderText="LOCAL" />
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label runat="server" ID="lblFichaAtendGrid" ToolTip="Ficha de Atendimento" Text="FA"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hidIdAtendimento" Value='<%# Eval("IdAtendimento") %>' />
                                                        <asp:ImageButton runat="server" ID="imgBtnFichaAtend" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_Obs_15x15.png"
                                                            Width="15px" Height="15px" ToolTip="Ficha de Atendimento" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li style="border-right: 1px solid #BDBDBD;">
            <ul>
                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                    <ContentTemplate>
                        <li style="margin-top: 2px;">
                            <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                QUEIXA PRINCIPAL</label>
                            <asp:TextBox runat="server" ID="txtQueixa" BackColor="#f1f1f1" TextMode="MultiLine"
                                Rows="2" Style="width: 225px; height: 85px; margin-top: 1px; border-top: 1px solid #BDBDBD;
                                border-left: 0; border-right: 0; border-bottom: 0;" Font-Size="12px" onkeydown="checkTextAreaMaxLength(this,event,'200');"></asp:TextBox>
                        </li>
                        <li style="margin-top: 2px;">
                            <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                OBSERVAÇÕES DO ATENDIMENTO</label>
                            <asp:TextBox runat="server" ID="txtObservacaoAtendimento" BackColor="#f1f1f1" TextMode="MultiLine"
                                Rows="2" Style="width: 225px; height: 85px; margin-top: 1px; border-top: 1px solid #BDBDBD;
                                border-left: 0; border-right: 0; border-bottom: 0;" Font-Size="12px" onkeydown="checkTextAreaMaxLength(this,event,'200');"></asp:TextBox>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ul>
            <ul>
                <li class="liTituloGrid" style="width: 457px !important; height: 20px !important;
                    margin: 0px; background-color: #DFF1FF; text-align: center; font-weight: bold;
                    margin-bottom: 2px; margin-left: 5px; padding-top: 2px;">
                    <ul>
                        <li style="margin: 0px 0 0 10px; float: left">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: Black">
                                PROCEDIMENTOS DO ATENDIMENTO</label>
                        </li>
                        <li style="margin-left: 10px; float: right; margin-top: 2px;">
                            <ul class="ulPer">
                                <li>
                                    <asp:TextBox runat="server" class="campoData" ID="txtDtIniProcedimentos" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                    <asp:Label runat="server" ID="Label3"> &nbsp à &nbsp </asp:Label>
                                    <asp:TextBox runat="server" class="campoData" ID="txtDtFimProcedimentos" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                </li>
                                <li style="margin: 0px 2px 0 -2px;">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        Width="13px" Height="13px" OnClick="imgPesqAgendamentos_Click" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="width: 457px; height: 148px; border: 1px solid #CCC; overflow-y: scroll;
                            margin-left: 5px;" id="div2">
                            <asp:GridView ID="grdProcedimentos" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum PROCEDIMENTO foi definido para este atendimento<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="DataHora" HeaderText="DATA/HORA" />
                                    <asp:BoundField DataField="nomeProcedimento" HeaderText="PROCEDIMENTO" />
                                    <asp:BoundField DataField="Tipo" HeaderText="TP" />
                                    <asp:BoundField DataField="Descricao" HeaderText="DESCRIÇÃO" />
                                    <asp:TemplateField>
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            <asp:Label runat="server" Font-Bold="true" ID="lblPacAtendido" Text="AT" ToolTip="Procedimento realizado"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <img alt="" src="/Library/IMG/PGS_IC_Positivo.png" runat="server" id="imgPositivo"
                                                visible='<%# Eval("isAplic") %>' style="width: 15px; height: 15px" title="Procedimento realizado" />
                                            <img alt="" src="/Library/IMG/PGS_IC_Negativo.png" runat="server" id="imgNegativo"
                                                visible='<%# Eval("isNaoReali") %>' style="width: 15px; height: 15px" title="Procedimento não realizado" />
                                            <img alt="" src="/Library/IMG/PGS_IC_Cancelado.png" runat="server" id="imgCancelado"
                                                visible='<%# Eval("isCancelado") %>' style="width: 15px; height: 15px" title="Procedimento cancelado" />
                                            <img alt="" src="/Library/IMG/Gestor_ComoChegar.png" runat="server" id="img1" visible='<%# Eval("isNaoInfo") %>'
                                                style="width: 15px; height: 15px" title="Não informado" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label runat="server" ID="lblFichaAtendGrid" ToolTip="Resultado do procedimento"
                                                Text="RE"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hidIdTipoProcedimento" Value='<%# Eval("idProcedimento") %>' />
                                            <asp:HiddenField runat="server" ID="hidOrigemProc" Value='<%# Eval("idOrigemProc") %>' />
                                            <asp:ImageButton runat="server" ToolTip="Resultado do procedimento" ID="imgBtnFichaAtend"
                                                ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_Obs_15x15.png" Width="15px"
                                                Height="15px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ul>
        </li>
        <li style="border-right: solid 1px #BDBDBD; width: 238px;">
            <ul style="margin-left: -1px;">
                <li>
                    <ul>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger EventName="Click" ControlID="imgPesProfSolicitado" />
                            </Triggers>
                            <ContentTemplate>
                                <li style="margin-left: -6px; margin-bottom: -5px; clear: both; display: flex;">
                                    <label style="color: Orange; font-size: 9px;">
                                        SOLICITAR PARECER</label>
                                    <label style="border-top: solid 1px #BDBDBD; width: 146px; margin-top: 6px;">
                                    </label>
                                </li>
                                <li style="clear: both; margin: 0 0 0 -6px;">
                                    <asp:TextBox runat="server" ID="txtProSolicitado" Style="margin: 0; width: 213px;"
                                        Height="12px"></asp:TextBox>
                                    <asp:ImageButton ID="imgPesProfSolicitado" ValidationGroup="pesqPac" Style="margin-top: 5px;"
                                        runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" Width="14px" OnClick="imgPesProfSolicitado_OnClick" />
                                    <asp:DropDownList ID="drpProSolicitado" Width="213px" runat="server" Visible="false"
                                        AutoPostBack="true" Height="15px" OnSelectedIndexChanged="drpProSolicitado_OnSelectedIndexChanged" />
                                    <asp:ImageButton ID="imgVoltarPesProfSOlicitado" ValidationGroup="pesqPac" Width="12px"
                                        Style="margin-top: 5px;" Height="12px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                                        Visible="false" runat="server" OnClick="imgVoltarPesProfSOlicitado_OnClick" />
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <li style="clear: both; margin-left: -6px; margin-bottom: 6px; margin-top: 4px">
                            <div id="div4" style="width: 230px; height: 107px; border: 1px solid #CCC; overflow-y: scroll">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="drpProSolicitado" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:GridView ID="grdProfSolicitado" CssClass="grdBusca grdExamFis" runat="server"
                                            Style="width: 100%; cursor: default;" AutoGenerateColumns="false" AllowPaging="false"
                                            GridLines="Vertical" ShowHeaderWhenEmpty="false">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhum profissional foi adicionado<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="PROFISSIONAL">
                                                    <ItemStyle HorizontalAlign="Left" Width="115px" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hidIdItemCID" Value='<%# Eval("idItem") %>' />
                                                        <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("coCol") %>' />
                                                        <asp:Label runat="server" ID="lblNomeProf" Text='<%# Eval("NomeCol") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PT">
                                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <input type="button" runat="server" id="btnObsProfSol" style="background: url('/Library/IMG/PGS_CentralRegulacao_Icone_Obs_15x15.png');
                                                            width: 15px; height: 15px;" title="Inserir observação sobre o protocolo da CID utilizado neste atendimento"
                                                            onclick="hideObsProfSol(this)" />
                                                        <asp:HiddenField runat="server" ID="hidIdProfSol" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EX">
                                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" Width="15px" ID="btnDelProfSol" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                            ToolTip="Excluir profissional solicitado deste atendimento" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');"
                                                            OnClick="btnDelProfSol_OnClick" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </li>
                    </ul>
                </li>
                <li style="height: 95px">
                    <ul>
                        <li style="margin-left: -6px;">
                            <ul>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger EventName="Click" ControlID="imgbPesqPacNome" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <li style="display: flex; margin-left: 0">
                                            <label style="color: Orange; font-size: 9px; width: 62px;">
                                                DEFINIR A CID</label>
                                            <label style="border-top: solid 1px #BDBDBD; width: 47px; margin-top: 6px;">
                                            </label>
                                        </li>
                                        <li style="cursor: pointer">
                                            <asp:TextBox runat="server" ID="txtDefCid" Width="103px" Height="12px" Style="margin: 0 0 0 -9px;"></asp:TextBox>
                                            <asp:ImageButton ID="imgbPesqPacNome" CssClass="btnProcurar" ValidationGroup="pesqPac"
                                                runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" Width="14px" OnClick="imgbPesqCID_OnClick" />
                                            <asp:DropDownList runat="server" ID="drpDefCid" OnSelectedIndexChanged="drpDefCid_OnSelectedIndexChanged"
                                                Visible="false" AutoPostBack="true" Width="107px" Style="margin: 0 0 0 -9px;">
                                            </asp:DropDownList>
                                            <asp:ImageButton ID="imgbVoltarPesq" ValidationGroup="pesqPac" CssClass="btnProcurar"
                                                Width="12px" Height="12px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgbVoltarPesq_OnClick"
                                                Visible="false" runat="server" />
                                        </li>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <li>
                                    <div id="div3" style="width: 230px; margin-left: -5px; height: 107px; border: 1px solid #CCC;
                                        overflow-y: scroll">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="drpDefCid" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:GridView ID="grdItensCID" CssClass="grdBusca grdExamFis" runat="server" Style="width: 100%;
                                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                                    ShowHeaderWhenEmpty="false" AllowUserToAddRows="false">
                                                    <RowStyle CssClass="rowStyle" />
                                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                    <EmptyDataTemplate>
                                                        Nenhuma CID foi adicionada<br />
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="CID">
                                                            <ItemStyle HorizontalAlign="Left" Width="25px" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hidIdItemCID" Value='<%# Eval("idItem") %>' />
                                                                <asp:HiddenField runat="server" ID="idListaCID" Value='<%# Eval("idCID") %>' />
                                                                <asp:Label runat="server" ID="lblProtCID" Text='<%# Eval("coCID")%>' ToolTip='<%# Eval("descCID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JE">
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="idImgCID" Value='<%# Eval("existeProtocolo") %>' />
                                                                <input type="button" runat="server" id="btnObsProtCID" style="background: url('/Library/IMG/PGS_CentralRegulacao_Icone_Obs_15x15.png');
                                                                    width: 15px; height: 15px;" title="Definir o(s) protocolo(s) da CID utilizado neste atendimento"
                                                                    visible='<%# Eval("existeProtocolo")%>' onclick="addProtocoloCID_Click(this)" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EX">
                                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="idDelCID" Value='<%# Eval("idCID") %>' />
                                                                <asp:HiddenField runat="server" ID="hidExisteProtocolo" Value='<%# Eval("existeProtocolo") %>' />
                                                                <asp:ImageButton runat="server" Width="15px" ID="btnDelCID" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                                    ToolTip="Excluir a CID e seu(s) protocolo(s) deste atendimento" OnClick="btnDelCID_OnClick"
                                                                    OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <%--<li>
                    <ul>
                       
                    </ul>
                </li>--%>
            </ul>
        </li>
        <li style="width: 262px;">
            <ul>
                <li style="margin: 0 0 0 -5px;">
                    <label style="color: Orange; font-size: 9px; width: 62px;">
                        LEITURA</label>
                    <label style="border-top: solid 1px #BDBDBD; width: 262px;">
                    </label>
                </li>
                <li style="margin-left: 17px; margin-bottom: -8px; margin-right: 0px; width: 70px;">
                    <label>
                        Pressão Val/HR</label>
                    <asp:TextBox ID="txtPressao" Width="30" CssClass="campoPressArteri" runat="server" />
                    <asp:TextBox ID="txtHrPressao" Width="30" CssClass="campoHora" runat="server" />
                </li>
                <li style="margin-bottom: -8px; margin-right: 0px; width: 70px;">
                    <label>
                        Temp Val/HR</label>
                    <asp:TextBox ID="txtTemp" Width="30" CssClass="campoTemp" runat="server" />
                    <asp:TextBox ID="txtHrTemp" Width="30" CssClass="campoHora" runat="server" />
                </li>
                <li style="margin-bottom: -8px; margin-right: 0px; width: 70px;">
                    <label>
                        Glicem Val/HR</label>
                    <asp:TextBox ID="txtGlic" Width="30" CssClass="campoGlicem campoPeso" runat="server" />
                    <asp:TextBox ID="txtHrGlic" Width="30" CssClass="campoHora" runat="server" />
                </li>
                <li style="margin: 0 19px -8px 12px; width: 25px; clear: both">
                    <label>
                        Peso</label>
                    <asp:TextBox ID="txtPeso" ClientIDMode="Static" CssClass="campoPeso" Width="26" runat="server" />
                </li>
                <li style="margin-left: -6px; margin-bottom: -8px; margin-right: 0px; width: 45px;">
                    <label>
                        Dores?</label>
                    <asp:DropDownList ID="drpDores" Height="13px" Width="40px" runat="server">
                        <asp:ListItem Value="S" Text="Sim" />
                        <asp:ListItem Value="N" Text="Não" Selected="True" />
                    </asp:DropDownList>
                </li>
                <li style="margin-bottom: -8px; margin-right: 0px; width: 45px;">
                    <label>
                        Enjôos?</label>
                    <asp:DropDownList ID="drpEnjoos" Height="13px" Width="40px" runat="server">
                        <asp:ListItem Value="S" Text="Sim" />
                        <asp:ListItem Value="N" Text="Não" Selected="True" />
                    </asp:DropDownList>
                </li>
                <li style="margin-bottom: -8px; margin-right: 0px; width: 45px;">
                    <label>
                        Vômitos?</label>
                    <asp:DropDownList ID="drpVomitos" Height="13px" Width="40px" runat="server">
                        <asp:ListItem Value="S" Text="Sim" />
                        <asp:ListItem Value="N" Text="Não" Selected="True" />
                    </asp:DropDownList>
                </li>
                <li style="margin-right: 0px; width: 45px;">
                    <label>
                        Febre?</label>
                    <asp:DropDownList ID="drpFebre" Height="13px" Width="40px" runat="server">
                        <asp:ListItem Value="S" Text="Sim" />
                        <asp:ListItem Value="N" Text="Não" Selected="True" />
                    </asp:DropDownList>
                </li>
                <li style="margin: 0 0 0 -5px;">
                    <label style="color: Orange; font-size: 9px; width: 134px; margin-top: 15px">
                        REGISTRO ATENDIMENTO</label>
                    <label style="border-top: solid 1px #BDBDBD; width: 262px;">
                    </label>
                </li>
                <li style="margin-left: -6px;">
                    <label>
                        Dt. Atendimento</label>
                    <asp:TextBox ID="txtDtAtend" runat="server" CssClass="campoData" />
                </li>
                <li style="margin-left: -1px;">
                    <label title="Hora inicial do atendimento">
                        Início</label>
                    <asp:TextBox ID="txtHrAtendIni" Enabled="false" ToolTip="Hora inicial do atendimento" CssClass="campoHora"
                        Width="28px" runat="server" />
                </li>
                <li style="margin-left: -1px;">
                    <label title="Hora final do atendimento">
                        Térm.</label>
                    <asp:TextBox ID="txtHrAtenFim" ToolTip="Hora final do atendimento" CssClass="campoHora"
                        Width="28px" runat="server" />
                </li>
                <li style="margin-top: 1px; color: Red;">
                    <label style="font-size: 0.85em;" title="Classificação de risco">
                        CLASS. DE RISCO</label>
                    <asp:DropDownList runat="server" ID="ddlClassRisco" title="Classificação de risco"
                        Width="90px" ClientIDMode="Static" OnSelectedIndexChanged="ddlClassRisco_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:HiddenField runat="server" ID="hidDivAberta" ClientIDMode="Static" />
                    <asp:TextBox runat="server" ID="txtClassRiso" Enabled="false" Width="16px" Height="12px"></asp:TextBox>
                </li>
                <li style="display: flex">
                    <ul>
                        <li style="margin-left: -12px; float: inherit;">
                            <label style="float: inherit; width: 112px;">
                                Repasse do Atendimento</label>
                            <asp:DropDownList ID="ddlProfResp" Width="149px" runat="server" />
                        </li>
                        <li style="margin-left: 141px; margin-top: -20px;">
                            <asp:TextBox ID="txtDataRepasse" runat="server" CssClass="campoData" />
                        </li>
                        <li style="margin-left: 219px; margin-top: -39px;">
                            <label title="Hora do repasse do atendimento">
                                Hora</label>
                            <asp:TextBox ID="txtHoraRepasse" ToolTip="Hora do repasse do atendimento" CssClass="campoHora"
                                Width="28px" runat="server" />
                        </li>
                    </ul>
                </li>
                <li style="margin-left: -6px; clear: both; margin-top: -5px;">
                    <label style="color: Orange; font-size: 9px; width: 262px; border-bottom: 1px solid #BDBDBD;">
                        PRESCRIÇÃO</label>
                </li>
                <li style="margin-left: -6px; clear: both;">
                    <asp:Button ID="lnkMedic" Style="background-color: #c1ffc1; margin-left: 0px; width: 85px;
                        cursor: pointer;" runat="server" Height="15px" Text="RECEITA" Font-Bold="true"
                        Font-Size="8px" OnClick="lnkMedic_OnClick" ToolTip="Solicitar medicamento para o paciente." />
                    <asp:Button ID="lnkExame" Style="background-color: #c1ffc1; margin-left: 2px; width: 85px;
                        cursor: pointer;" runat="server" Text="EXAME" Height="15px" Font-Bold="true"
                        Font-Size="8px" OnClick="lnkExame_OnClick" ToolTip="Emitir guia ou solicitar exame externo." />
                    <asp:Button ID="lnkAmbul" Style="background-color: #c1ffc1; margin-left: 0px; width: 85px;
                        cursor: pointer;" runat="server" Text="AMBULATÓRIO" Height="15px" Font-Bold="true"
                        Font-Size="8px" OnClick="lnkAmbul_OnClick" ToolTip="Solicitar serviço ambulatorial." />
                </li>
                <li style="margin: 0 -11px 0; clear: both; width: 268px">
                    <ul>
                        <li style="width: 267px;">
                            <asp:Button ID="lnkbProntuario" CssClass="colorTextBlack" Style="background-color: #ccc;
                                cursor: pointer;" Width="64px" Height="13px" Font-Bold="true" Font-Size="8px"
                                runat="server" Text="PRONTUÁRIO" OnClick="lnkbProntuario_OnClick" />
                            <asp:Button ID="BtnLaudo" Style="background-color: #ccc; cursor: pointer; margin-left: 0"
                                Width="63px" Height="13px" runat="server" Font-Bold="true" Font-Size="8px" Text="LAUDO"
                                OnClick="BtnLaudo_Click" />
                            <asp:Button ID="BtnAtestado" Style="background-color: #ccc; cursor: pointer; margin: 0 0 0 0"
                                Width="63px" Height="13px" runat="server" Text="ATESTADO" Font-Bold="true" Font-Size="8px"
                                OnClick="BtnAtestado_Click" />
                            <asp:Button ID="btnAnexos" OnClick="btnAnexos_OnClick" ToolTip="Anexos associados ao Atendimento/Paciente"
                                Style="background-color: #ccc; cursor: pointer; margin: 0" runat="server" Text="ANEXO"
                                Width="63px" Height="13px" Font-Bold="true" Font-Size="8px" AccessKey="A" CssClass="colorTextBlack" />
                        </li>
                    </ul>
                </li>
                <li style="margin-left: -6px; clear: both; margin-top: 13px">
                    <ul>
                        <li style="border-top: 1px solid #BDBDBD; width: 85px; margin: 0"></li>
                        <li style="border-top: 1px solid #BDBDBD; width: 60px;"></li>
                        <li style="margin: -5px 0;"><span style="font-size: 8px; color: #666">FINALIZAR</span></li>
                        <li style="border-top: 1px solid #BDBDBD; width: 60px;"></li>
                        <li style="margin: 0; clear: both">
                            <asp:Button ID="BtnSalvar" runat="server" Text="FINALIZAR" Height="20px" BackColor="#87CEEB"
                                Font-Bold="true" Style="color: #fff; cursor: pointer;" Width="86px" OnClick="BtnSalvar_OnClick" />
                        </li>
                        <li style="margin: 0 0 0 3px">
                            <asp:Button ID="BtnFinalizar" runat="server" Text="ALTA" Height="20px" Width="85px"
                                Font-Bold="true" Style="color: #fff; cursor: pointer;" BackColor="#008000" OnClick="BtnFinalizar_OnClick" />
                            <asp:Button ID="BtnObito" runat="server" Text="ÓBITO" BackColor="#000000" Font-Bold="true"
                                Style="color: #fff; cursor: pointer;" Height="20px" Width="85px" OnClick="BtnObito_OnClick" />
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
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
                                        <asp:HiddenField runat="server" ID="hidCoALuAtes" Value='<%# Eval("CO_ALU") %>' />
                                        <asp:HiddenField runat="server" ID="hidNmPac" Value='<%# Eval("NO_PAC_") %>' />
                                        <asp:HiddenField runat="server" ID="hidNmResp" Value='<%# Eval("NO_RESP_IMP") %>' />
                                        <asp:HiddenField runat="server" ID="hidRgPac" Value='<%# Eval("RG_PAC") %>' />
                                        <asp:HiddenField runat="server" ID="hidHora" Value='<%# Eval("hr_Consul") %>' />
                                        <asp:HiddenField runat="server" ID="hidCoRespAtest" Value='<%# Eval("CO_RESP") %>' />
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
                        <asp:Label runat="server" ID="Label4" Text="EMITIR" Style="margin-left: 5px; margin-right: 5px;"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
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
        <div id="divAnexos" style="display: none; height: 360px !important;">
            <asp:HiddenField runat="server" ID="hidTpAnexo" />
            <ul class="ulDados" style="width: 880px; margin-left: -10px !important; margin-top: 0px !important">
                <li style="clear: both; margin: 0
    0 0 5px !important;">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="width: 862px; height: 215px; border: 1px solid
    #CCC; overflow-y: scroll">
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
                                        <asp:BoundField DataField="NM_TITULO" HeaderText="IDENTIFICAÇÃO
    DO ARQUIVO">
                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OBSERVAÇÕES">
                                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidIdAnexo" Value='<%# Eval("ID_ANEXO_ATEND")
    %>' />
                                                <asp:Label ID="Label2" Text='<%# Eval("DE_OBSER_RES") %>' ToolTip='<%# Eval("DE_OBSER")
    %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SOLICITANTE" HeaderText="SOLICITANTE">
                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="VZ">
                                            <ItemStyle Width="10px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgVisualizarAnexo" ImageUrl="/Library/IMG/PG_view-icone-16px-16px.png"
                                                    ToolTip="Visualizar anexo" OnClick="imgVisualizarAnexo_OnClick" Width="16" Height="16"
                                                    Style="margin: 0 0 0 -3px !important;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                    Style="margin: 0 0 0 -3px !important;" OnClientClick="return confirm ('Tem certeza
    de que deseja excluir o arquivo?');" />
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
                            <li class="liBtnAddA" style="margin: 5px 10px
    0 0; float: right; width: 45px;">
                                <asp:LinkButton ID="lnkbAnexar" runat="server" OnClick="lnkbAnexar_OnClick"> <label style="margin-left:5px;">ANEXAR</label> </asp:LinkButton>
                            </li>
                        </ContentTemplate>
                    </asp:UpdatePanel>
            </ul>
        </div>
        <asp:HiddenField runat="server" ID="hidObserMedicam" ClientIDMode="Static" />
        <div id="divMedicamentos" style="display: none; height: 430px !important; width: 800px">
            <ul class="ulDados" style="margin: 5px 0 0 -10px !important;">
                <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Conditional">
                    <ContentTemplate>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ul>
        </div>
        <div id="divLoadShowNovoMedic" style="display: none; height: 370px !important;">
            <ul class="ulDados" style="width: 350px !important;">
                <asp:UpdatePanel runat="server" ID="UpdatePanel12" UpdateMode="Conditional">
                    <ContentTemplate>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ul>
        </div>
        <asp:HiddenField runat="server" ID="hidCheckEmitirGuiaExame" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hidCheckSolicitarExame" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hidObserExame" ClientIDMode="Static" />
        <div id="divExames" style="display: none; height: 400px !important; width: 800px">
            <ul class="ulDados">
                <li>
                    <ul style="width: 766px">
                        <li class="liTituloGrid" style="width: 673px; height: 20px !important; margin-left: -0px;
                            background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                            <ul>
                                <li style="margin: 0 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        EXAMES</label>
                                </li>
                                <li style="display: flex; margin-top: 3px;">
                                    <asp:CheckBox runat="server" ID="chkIsGuiaExame" ClientIDMode="Static" ToolTip="Emitir apenas a guia dos exames selecionados (se nenhum for selecionado, a funcionalidade apenas emitirá a guia)." />Emitir
                                    guia
                                    <asp:CheckBox runat="server" ID="chkIsExameExterno" ClientIDMode="Static" ToolTip="Solicitar exame para o paciente em atendimento (se nenhum for selecionado, a funcionalidade apenas emitirá a guia)." />Solicitar
                                    Exame </li>
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
                                            <asp:DropDownList runat="server" Width="100px" ID="ddlPlanProcPlan" OnSelectedIndexChanged="ddlPlanProcPlan_OnSelectedIndexChanged"
                                                AutoPostBack="true">
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
                    <div style="width: 766px; border: 1px solid #CCC; overflow-y: scroll">
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
                                <asp:TemplateField HeaderText="VALOR">
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtValorProced" Width="100%" Style="margin-left: -4px;
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
                        Style="width: 766px; height: 15px;" Font-Size="11px" placeholder=" Digite as observações sobre Exames"></asp:TextBox>
                </li>
                <li class="liBtnAddA" style="clear: none !important; margin-left: 727px !important;
                    margin-top: 8px !important; height: 15px;">
                    <asp:LinkButton ID="btnGuiaExames" runat="server" OnClick="BtnGuiaExames_OnClick"
                        ToolTip="Imprimir laudo técnico">
                        <asp:Label runat="server" ID="Label7" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <asp:HiddenField runat="server" ID="hidObsSerAmbulatoriais" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="didIdServAmbulatorial" ClientIDMode="Static" />
        <div id="divAmbulatorio" style="display: none; height: 400px !important; width: 800px">
            <ul class="ulDados">
                <li>
                    <ul style="width: 766px">
                        <li class="liTituloGrid" style="width: 673px; height: 20px !important; margin-left: -0px;
                            background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                            <ul>
                                <li style="margin: 0 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        SERVIÇOS AMBULATORIAIS</label>
                                </li>
                                <li style="float: right; margin-top: 3px; margin-right: 15px;">
                                    <ul>
                                        <li>
                                            <asp:Label runat="server" ID="Label8">Contratação</asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlOperPlanoServAmbu" Width="100px" OnSelectedIndexChanged="ddlOperPlanoServAmbu_OnSelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin-left: -2px;">
                                            <asp:Label runat="server" ID="Label13">Plano</asp:Label>
                                            <asp:DropDownList runat="server" Width="100px" ID="ddlPlanoServAmbu" OnSelectedIndexChanged="ddlPlanoServAmbu_OnSelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li id="li1" runat="server" title="Clique para cadastrar um novo Serviço Ambulatorial (Procedimento)"
                            class="liBtnAddA" style="float: right; margin: -25px 44px 3px 5px; width: 40px">
                            <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_BtnEdit.png"
                                height="15px" width="15px" />
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="imgNovoExam_OnClick" Enabled="false">Novo</asp:LinkButton>
                        </li>
                        <li id="li3" runat="server" title="Clique para adicionar um serviço ambulatorial ao atendimento"
                            class="liBtnAddA" style="float: right; margin: -25px 22px 3px 5px; height: 15px;
                            width: 12px;">
                            <asp:ImageButton ID="ImageButton3" Height="15px" Width="15px" Style="margin-top: -1px;
                                margin-left: -2px;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" runat="server"
                                OnClick="lnkAddProcPlaAmbulatorial_OnClick" />
                        </li>
                        <li title="Clique para emitir os serviços ambulatoriais" class="liBtnAddA" style="float: right;
                            margin: -25px -2px 3px 0px; width: 12px; height: 15px;">
                            <asp:ImageButton ID="ImageButton" runat="server" OnClick="btnGuiaServAmbulatoriais_OnClick"
                                Style="margin-top: -2px; margin-left: -3px;" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png"
                                Height="18px" Width="18px" />
                        </li>
                    </ul>
                </li>
                <asp:UpdatePanel runat="server" ID="UpdatePanel13" UpdateMode="Conditional">
                    <ContentTemplate>
                        <li style="clear: both; margin: -7px 0 0 5px !important;">
                            <div style="width: 766px; border: 1px solid #CCC; overflow-y: scroll">
                                <asp:GridView ID="grdServAmbulatoriais" CssClass="grdBusca" runat="server" Style="width: 100%;
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
                                                <asp:DropDownList runat="server" ID="ddlServAmbulatorial" Width="100%" OnSelectedIndexChanged="ddlServAmbu_OnSelectedIndexChanged"
                                                    AutoPostBack="true" Style="margin: 0 0 0 -4px !important;">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIÇÃO">
                                            <ItemStyle Width="125px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtDeServAmbulatorial" Width="100%" Style="margin-left: -4px;
                                                    margin-bottom: 0px;" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VALOR">
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtValorServAmbulatorial" Width="100%" Style="margin-left: -4px;
                                                    margin-bottom: 0px;" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EX">
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgServAmbulatoriaisPla" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                    ToolTip="Excluir Serviço Ambulatorial" OnClick="imgServAmbulatoriaisPla_OnClick"
                                                    Style="margin: 0 0 0 0 !important;" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <li style="clear: both; margin: 3px 0 0 5px;">
                    <asp:TextBox runat="server" ID="txtObsServAmbulatoriais" TextMode="MultiLine" ClientIDMode="Static"
                        Style="width: 766px; height: 15px;" Font-Size="11px" MaxLength="200" placeholder=" Digite as observações sobre o Serviço Ambulatorial"></asp:TextBox>
                </li>
            </ul>
        </div>
        <div id="divAvisoPermissao" style="display: none;">
            <ul>
                <li>
                    <label>
                        Este usuário não tem permissão para realizar atendimento, por não ser um profissional
                        de saúde.
                    </label>
                </li>
                <li class="liBtnConfirmarCiencia" onclick="javascript:BackToHome()">
                    <asp:LinkButton ID="lnkEncAtendimento" runat="server" ToolTip="Realiza o encaminhamento para o atendimento">
                    <label style="margin-left:5px; color:White; cursor:pointer;">VOLTAR</label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <div id="div5" style="display: none; height: 370px !important;">
            <ul class="ulDados" style="width: 350px !important;">
                <li>
                    <label for="txtNProduto" class="lblObrigatorio" title="Nome do Produto">
                        Nome do Produto</label>
                    <asp:TextBox ID="TextBox1" Width="300px" ToolTip="Informe o Nome do Produto" runat="server"
                        MaxLength="60"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="RequiredFieldValidator1"
                        CssClass="validatorField" ErrorMessage="O nome do produto é obrigatório" ControlToValidate="txtNProduto"
                        Display="Dynamic" />
                </li>
                <li style="clear: both">
                    <label for="txtNReduz" class="lblObrigatorio" title="Nome Reduzido">
                        Nome Reduzido</label>
                    <asp:TextBox ID="TextBox4" Width="180px" ToolTip="Informe o Nome Reduzido" runat="server"
                        MaxLength="33"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="RequiredFieldValidator2"
                        CssClass="validatorField" ErrorMessage="O nome reduzido do produto é obrigatório"
                        ControlToValidate="txtNReduz" Display="Dynamic" />
                </li>
                <li>
                    <label for="txtCodRef" class="lblObrigatorio" title="Código de Referência">
                        Cód. Referência</label>
                    <asp:TextBox ID="TextBox5" Width="110px" ToolTip="Informe o Código de Referência"
                        runat="server" MaxLength="9"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="RequiredFieldValidator3"
                        CssClass="validatorField" ErrorMessage="O código referência do produto é obrigatório"
                        ControlToValidate="txtCodRef" Display="Dynamic" />
                </li>
                <li style="clear: both">
                    <label for="txtDescProduto" class="lblObrigatorio" title="Descrição do Produto">
                        Descrição do Produto</label>
                    <asp:TextBox ID="TextBox6" Width="300px" Height="51px" TextMode="MultiLine" ToolTip="Informe a Descrição do Produto"
                        runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="RequiredFieldValidator4"
                        CssClass="validatorField" ErrorMessage="A descrição do produto é obrigatória"
                        ControlToValidate="txtDescProduto" Display="Dynamic" />
                </li>
                <li style="clear: both;">
                    <label class="lblObrigatorio">
                        Princípio Ativo</label>
                    <asp:TextBox runat="server" ID="TextBox7" Width="300px" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="RequiredFieldValidator5"
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
                    <asp:DropDownList ID="DropDownList1" Width="150px" ToolTip="Selecione o Grupo de Produtos"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="RequiredFieldValidator6"
                        CssClass="validatorField" ErrorMessage="O grupo do produto é obrigatório" ControlToValidate="ddlGrupo"
                        Display="Dynamic" />
                </li>
                <li style="margin-top: 10px;">
                    <label for="ddlSubGrupo" class="lblObrigatorio" title="SubGrupo de Produtos">
                        SubGrupo</label>
                    <asp:DropDownList ID="DropDownList2" Width="145px" ToolTip="Selecione o SubGrupo de Produtos"
                        runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="RequiredFieldValidator7"
                        CssClass="validatorField" ErrorMessage="O SubGrupo do produto é obrigatório"
                        ControlToValidate="ddlSubGrupo" Display="Dynamic" />
                </li>
                <li style="clear: both">
                    <label class="lblObrigatorio" title="Unidade do Produtos">
                        Unidade</label>
                    <asp:DropDownList ID="DropDownList3" CssClass="campoDescricao" ToolTip="Unidade do Produtos"
                        runat="server" Width="90px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="RequiredFieldValidator8"
                        CssClass="validatorField" ErrorMessage="A unidade do produto é obrigatória" ControlToValidate="ddlUnidade"
                        Display="Dynamic" />
                </li>
                <li id="li5" runat="server" class="liBtnAddA" style="margin-top: 0px !important;
                    clear: both !important; height: 15px; margin-left: 140px !important;">
                    <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="novoMedicamento"
                        OnClick="lnkNovoMedic_OnClick" ToolTip="Armazena as informações na prescrição em questão">
                        <asp:Label runat="server" ID="Label11" Text="SALVAR" Style="margin-left: 4px; margin-right: 4px;"></asp:Label>
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
                        Width="65px" CssClass="campoCodigo"></asp:TextBox>
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
                            <asp:TextBox runat="server" ID="txtVlCusto" CssClass="campoDecimal" Width="50px"
                                ToolTip="Valor de Custo atual do procedimento" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li>
                            <label title="Valor base do procedimento de saúde para cálculo">
                                R$ Base</label>
                            <asp:TextBox runat="server" ID="txtVlBase" Enabled="false" Width="60px" CssClass="campoDecimal"
                                ToolTip="Valor base do procedimento de saúde para cálculo" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li>
                            <label title="Valor Base da Operadora para o procedimento">
                                R$ Restitu</label>
                            <asp:TextBox runat="server" ID="txtVlRestitu" Enabled="false" Width="60px" CssClass="campoDecimal"
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
                        <asp:Label runat="server" ID="Label12" Text="SALVAR" Style="margin-left: 4px;"></asp:Label>
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
                        <asp:Label runat="server" ID="Label14" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <div id="divMaisInfo" style="display: none; height: 166px !important;">
            <ul class="ulDados" style="width: 610px; margin-top: 0px !important">
                <li style="clear: both; width: 120px;">
                    <ul>
                        <li style="margin-bottom: 4px;">
                            <label>
                                Diabetes</label>
                            <asp:CheckBox ID="chkDiabetes" Style="margin: 0 -6px 0 -6px;" runat="server" />
                            <asp:DropDownList ID="drpTipoDiabete" Width="90px" runat="server">
                                <asp:ListItem Value="" Text="Selecione" />
                                <asp:ListItem Value="1" Text="Tipo 1" />
                                <asp:ListItem Value="2" Text="Tipo 2" />
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both;">
                            <label>
                                Hipertensão</label>
                            <asp:CheckBox ID="chkHipertensao" Style="margin: 0 -6px 0 -6px;" runat="server" />
                            <asp:TextBox ID="txtHipertensao" Width="90px" runat="server" MaxLength="20" />
                        </li>
                        <li style="clear: both; margin-top: -8px;">
                            <label>
                                Fumante (St/Anos)</label>
                            <asp:DropDownList ID="drpFumante" Width="85px" runat="server">
                                <asp:ListItem Value="S" Text="Sim" />
                                <asp:ListItem Value="N" Text="Não" />
                                <asp:ListItem Value="E" Text="Ex-Fumante" />
                            </asp:DropDownList>
                            <asp:TextBox ID="txtFumanteAnos" CssClass="campoAnos" Height="13px" Width="20px"
                                runat="server" />
                        </li>
                        <li style="clear: both; margin-top: -8px;">
                            <label>
                                Alcool (St/Anos)</label>
                            <asp:DropDownList ID="drpAlcool" Width="85px" runat="server">
                                <asp:ListItem Value="S" Text="Sim" />
                                <asp:ListItem Value="N" Text="Não" />
                                <asp:ListItem Value="A" Text="As Vezes" />
                            </asp:DropDownList>
                            <asp:TextBox ID="txtAlcoolAnos" CssClass="campoAnos" Height="13px" Width="20px" runat="server" />
                        </li>
                    </ul>
                </li>
                <li style="margin-left: -5px; border-left: 1px solid #BDBDBD; width: 185px;">
                    <ul>
                        <li>
                            <asp:CheckBox ID="chkCirurgia" Text="Cirurgia" CssClass="chk" Style="margin-left: -5px;"
                                runat="server" />
                            <asp:TextBox ID="txtCirurgia" TextMode="MultiLine" Rows="3" Width="170px" runat="server"
                                MaxLength="40" />
                        </li>
                        <li style="margin-top: -3px;">
                            <asp:CheckBox ID="chkAlergiaMedic" Text="Alergia" CssClass="chk" Style="margin-left: -5px;"
                                ToolTip="O paciente possui alguma alergia a medicamento?" runat="server" />
                            <asp:TextBox ID="txtAlergiaMedic" TextMode="MultiLine" Rows="6" Width="170px" MaxLength="40"
                                ToolTip="Informe qual(is) medicamento(s) que o paciente possui alergia" runat="server" />
                        </li>
                    </ul>
                </li>
                <li style="margin-right: -5px; color: Blue;">
                    <label>
                        Medicação de uso contínuo</label>
                    <asp:TextBox ID="txtMedicacaoCont" TextMode="MultiLine" Rows="11" Width="132px" runat="server"
                        MaxLength="200" />
                </li>
                <li style="margin-left: 18px; color: Blue;">
                    <label>
                        Sintomas</label>
                    <asp:TextBox ID="txtSintomasModal" TextMode="MultiLine" Rows="11" Width="132px" runat="server"
                        MaxLength="200" />
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
        <%--<div id="divObservacao" style="display: none; height: 300px !important; width: 580px">
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
        </div>--%>
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
                <li id="li70" runat="server" class="liBtnAddA" style="float: right; margin-top: 10px !important;
                    clear: none !important; height: 15px;">
                    <asp:LinkButton ID="lnkbImprimirFicha" runat="server" OnClick="lnkbImprimirFicha_Click"
                        ToolTip="Imprimir ficha de atendimento">
                        <asp:Label runat="server" ID="lblEmitirFicha" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <div id="div6" style="display: none; height: 360px !important;">
            <asp:HiddenField runat="server" ID="HiddenField2" />
            <ul class="ulDados" style="width: 880px; margin-left: -10px !important; margin-top: 0px !important">
                <li style="clear: both; margin: 0 0 0 5px !important;">
                    <div style="width: 862px; height: 215px; border: 1px solid #CCC; overflow-y: scroll">
                        <asp:GridView ID="GridView2" CssClass="grdBusca" runat="server" Style="width: 100%;
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
                    <asp:DropDownList ID="DropDownList4" Height="19px" runat="server">
                        <asp:ListItem Text="Anexo" Value="A" Selected="True" />
                        <asp:ListItem Text="Audio" Value="U" />
                        <asp:ListItem Text="Foto" Value="F" />
                        <asp:ListItem Text="Video" Value="V" />
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Arquivo</label>
                    <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" />
                </li>
                <li>
                    <label>
                        Identificação do Arquivo</label>
                    <asp:TextBox ID="TextBox8" runat="server" Width="200px" Height="17px" ClientIDMode="Static" />
                </li>
                <li>
                    <label>
                        Observações</label>
                    <asp:TextBox ID="TextBox9" TextMode="MultiLine" Width="318px" Height="17px" runat="server" />
                </li>
                <li class="liBtnAddA" style="margin: 5px 10px 0 0; float: right; width: 45px;">
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lnkbAnexar_OnClick">
                    <label style="margin-left:5px;">ANEXAR</label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <%--<div id="modalInf" class="modalInf" style="display: none; height: 360px !important;">
        <ul class="ulDados">
            <li style="border: 1px solid #00000">
                <ul>
                    <li style="margin-left: 2px; width: 99%; color: Blue; border-bottom: 2px solid #58ACFA;
                        margin-bottom: 2px; display: inline-flex;">
                        <label style="font-size: 12px; margin-right: 304px">
                            Registro de Informações</label>
                        <input id="btnModalInf" type="button" style="background: url('/Library/IMG/Gestor_BtnDel.png') no-repeat;
                            width: 20px; border: none; cursor: pointer;" onclick="hideModal(this)" />
                    </li>
                    <li style="clear: both; width: 120px;">
                        <ul>
                            <li style="margin-bottom: 4px;">
                                <label>
                                    Diabetes</label>
                                <asp:CheckBox ID="chkDiabetes" Style="margin: 0 -6px 0 -6px;" runat="server" />
                                <asp:DropDownList ID="drpTipoDiabete" Width="90px" runat="server">
                                    <asp:ListItem Value="" Text="Selecione" />
                                    <asp:ListItem Value="1" Text="Tipo 1" />
                                    <asp:ListItem Value="2" Text="Tipo 2" />
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both;">
                                <label>
                                    Hipertensão</label>
                                <asp:CheckBox ID="chkHipertensao" Style="margin: 0 -6px 0 -6px;" runat="server" />
                                <asp:TextBox ID="txtHipertensao" Width="90px" runat="server" MaxLength="20" />
                            </li>
                            <li style="clear: both; margin-top: -8px;">
                                <label>
                                    Fumante (St/Anos)</label>
                                <asp:DropDownList ID="drpFumante" Width="85px" runat="server">
                                    <asp:ListItem Value="S" Text="Sim" />
                                    <asp:ListItem Value="N" Text="Não" />
                                    <asp:ListItem Value="E" Text="Ex-Fumante" />
                                </asp:DropDownList>
                                <asp:TextBox ID="txtFumanteAnos" CssClass="campoAnos" Height="13px" Width="20px"
                                    runat="server" />
                            </li>
                            <li style="clear: both; margin-top: -8px;">
                                <label>
                                    Alcool (St/Anos)</label>
                                <asp:DropDownList ID="drpAlcool" Width="85px" runat="server">
                                    <asp:ListItem Value="S" Text="Sim" />
                                    <asp:ListItem Value="N" Text="Não" />
                                    <asp:ListItem Value="A" Text="As Vezes" />
                                </asp:DropDownList>
                                <asp:TextBox ID="txtAlcoolAnos" CssClass="campoAnos" Height="13px" Width="20px" runat="server" />
                            </li>
                        </ul>
                    </li>
                    <li style="margin-left: -5px; border-left: 1px solid #BDBDBD; width: 185px;">
                        <ul>
                            <li>
                                <asp:CheckBox ID="chkCirurgia" Text="Cirurgia" CssClass="chk" Style="margin-left: -5px;"
                                    runat="server" />
                                <asp:TextBox ID="txtCirurgia" TextMode="MultiLine" Rows="3" Width="170px" runat="server"
                                    MaxLength="40" />
                            </li>
                            <li style="margin-top: -3px;">
                                <asp:CheckBox ID="chkAlergiaMedic" Text="Alergia" CssClass="chk" Style="margin-left: -5px;"
                                    ToolTip="O paciente possui alguma alergia a medicamento?" runat="server" />
                                <asp:TextBox ID="txtAlergiaMedic" TextMode="MultiLine" Rows="6" Width="170px" MaxLength="40"
                                    ToolTip="Informe qual(is) medicamento(s) que o paciente possui alergia" runat="server" />
                            </li>
                        </ul>
                    </li>
                    <li style="margin-right: -5px; color: Blue;">
                        <label>
                            Medicação de uso contínuo</label>
                        <asp:TextBox ID="txtMedicacaoCont" TextMode="MultiLine" Rows="8" Width="132px" runat="server"
                            MaxLength="200" />
                    </li>
                </ul>
            </li>
        </ul>
    </div>--%>
        <div id="obsTr" class="indice" style="display: none; width: 572px;">
            <ul class="ulDados">
                <li style="margin-left: 2px; width: 54%; color: Blue; border-bottom: 2px solid #58ACFA;
                    margin-bottom: 2px; display: inline-flex;">
                    <label style="font-size: 12px; margin-right: 469px">
                        Oberservação:</label>
                    <input id="Button1" type="button" style="background: url('/Library/IMG/Gestor_BtnDel.png') no-repeat;
                        width: 20px; border: none; cursor: pointer;" onclick="hideObs(this)" />
                </li>
                <li>
                    <textarea id="txtObsTr" cols="100" title="Observação referente ao uso deste protocolo para este atendimento. (Máximo de 200 carateres)"
                        rows="4" onkeydown="checkTextAreaMaxLength(this,event,'200');"></textarea>
                    <input id="txtId" type="text" style="display: none" />
                </li>
            </ul>
        </div>
        <div id="divObsProSol" style="display: none; height: 443px; width: 590px">
            <ul class="ulDados">
                <li style="clear: both;">
                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                        ANAMNESE / HDA (História da Doença Atual)</label>
                    <textarea id="txtAnamRespModal" rows="6" cols="100" style="width: 557px; border-top: 1px solid #BDBDBD;
                        border-left: 0; border-right: 0; border-bottom: 0; font-size: 12px; background-color: #FAFAFA"
                        onkeydown="checkTextAreaMaxLength(this,event,'400');">
                </textarea>
                </li>
                <li style="margin-bottom: 0; clear: both;">
                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                        HIPÓTESE DIAGNÓSTICA / AÇÃO REALIZADA</label>
                    <textarea id="txtAcaoRepasModal" rows="6" cols="100" style="width: 557px; border-top: 1px solid #BDBDBD;
                        border-left: 0; border-right: 0; border-bottom: 0; font-size: 12px; background-color: #FAFAFA"
                        onkeydown="checkTextAreaMaxLength(this,event,'400');">
                </textarea>
                </li>
                <li style="margin-bottom: 0; clear: both;">
                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                        OBSERVAÇÃO</label>
                    <textarea id="txtObsPro" rows="3" cols="100" style="width: 557px; border-top: 1px solid #BDBDBD;
                        border-left: 0; border-right: 0; border-bottom: 0; font-size: 12px; background-color: #FAFAFA"
                        onkeydown="checkTextAreaMaxLength(this,event,'200');">
                </textarea>
                    <input id="inputObsPro" type="text" style="display: none" />
                </li>
                <li style="margin-bottom: 0; clear: both;">
                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                        EEXAMES FÍSICOS</label>
                    <asp:DropDownList runat="server" ID="drpExamFisParecer" ClientIDMode="Static" Width="269px"
                        onclientselectedindexchanged="addExameFísico(this)">
                    </asp:DropDownList>
                </li>
                <li style="margin-bottom: 0;">
                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                        CID - PROTOCOLO</label>
                    <asp:DropDownList runat="server" ID="drpCIDProtocolo" ClientIDMode="Static" Width="269px"
                        onclientselectedindexchanged="drpCIDProtocolo_OnSelectedIndexChanged(this)">
                    </asp:DropDownList>
                </li>
                <li style="clear: both; margin-top: 6px; width: 269px; height: 77px; overflow-y: scroll;">
                    <div id="tbExamesFisRepasse">
                        <table id="myTable" style="border: none">
                            <tbody id="myTBody">
                            </tbody>
                        </table>
                    </div>
                </li>
                <li style="margin-top: 6px; width: 269px; height: 77px; overflow-y: scroll;">
                    <div id="dvCIDRepasse">
                        <table id="tbCIDRepasse" style="border: none">
                            <tbody id="bodyCIDRepasse">
                            </tbody>
                        </table>
                    </div>
                </li>
                <li style="clear: both; margin-top: 22px; border: solid 1px #75abff; background: #75abff;
                    margin-left: 506px;">
                    <input type="button" id="btnInserirProfSol" value="Salvar" style="height: 20px; background-color: #0b3e6f;
                        color: #fff; cursor: pointer; width: 56px" />
                </li>
                <asp:HiddenField ID="coColaboradorParecerMedico" ClientIDMode="Static" runat="server" />
            </ul>
        </div>
        <div id="dvObito" class="" style="display: none; width: 450px; height: 200px;">
            <ul class="ulDados">
                <li style="width: 273px; margin-bottom: 13px;">
                    <asp:Label ID="Label17" runat="server" CssClass="colorTextBlue">NOME DO PACIENTE</asp:Label><br />
                    <asp:TextBox runat="server" ID="txtPacienteObito" Width="232px" CssClass="txtNomePacienteInternar"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label18" runat="server" CssClass="colorTextBlue">Nº REG. DE ATENDIMENTO:</asp:Label><br />
                    <asp:TextBox runat="server" Style="color: Red" ID="txtRegAtendimentoObito" CssClass="txtNomePacienteInternar"></asp:TextBox>
                </li>
                <li style="clear: both">
                    <asp:Label ID="lblDataObito" runat="server" CssClass="txtNomePacienteInternar">DATA DO ÓBITO:</asp:Label><br />
                    <asp:TextBox CssClass="campoData" runat="server" ID="txtDataObito"></asp:TextBox>
                </li>
                <li style="">
                    <asp:Label runat="server" ID="lblHoraObito" CssClass="txtNomePacienteInternar">HORA DO ÓBITO:</asp:Label><br />
                    <asp:TextBox runat="server" CssClass="campoHora" Width="55px" ID="txtHoraObito"></asp:TextBox>
                </li>
                <li style="clear: both">
                    <asp:Label runat="server" ID="Label177" CssClass="txtNomePacienteInternar">OBSERVAÇÃO:</asp:Label><br />
                    <asp:TextBox TextMode="MultiLine" Width="413px" Height="40px" Rows="4" MaxLength="200"
                        runat="server" ID="txtObsObito" onkeydown="checkTextAreaMaxLength(this,event,'200');"
                        CssClass="txtNomePacienteInternar"></asp:TextBox>
                </li>
                <li style="clear: both; margin-left: 364px;">
                    <asp:Button runat="server" ID="Button2" Text="SALVAR" Style="height: 20px; background-color: #0b3e6f;
                        color: #fff; cursor: pointer; width: 56px" Font-Bold="true" OnClick="btnSalvarObito_OnClick" />
                </li>
            </ul>
        </div>
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
                $("#flupAnexo").change(function () {
                    if ($("#txtNomeAnexo").val() == "") {
                        var fileName = $("#flupAnexo").val();
                        while (fileName.indexOf("\\") != -1)
                            fileName = fileName.slice(fileName.indexOf("\\") + 1);
                        $("#txtNomeAnexo").val(fileName);
                    }
                });
                $('.campoHora').mask('99:99');

            });

            //Determina a quantidade de caracteres do textbox multiline
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


            $(document).ready(function () {
                $("#flupAnexo").change(function () {
                    if ($("#txtNomeAnexo").val() == "") {
                        var fileName = $("#flupAnexo").val();
                        while (fileName.indexOf("\\") != -1) fileName = fileName.slice(fileName.indexOf("\\") + 1); $("#txtNomeAnexo").val(fileName);
                    }
                });

                $('.campoHora').mask('99:99');
                $(".campoCodigo").mask("?9999999999");
                $(".campoQtd").mask("?999");
                $(".campoHora").mask("99:99");
                $(".campoAnos").mask("?99");
                $(".campoPeso").mask("?99,99");
                $(".campoTemp").mask("99.9");
                $(".campoPressArteri").mask("?99/99");
                $(".campoGlicem").mask("?9999");
                $(".campoAltura").mask("9,99");
                $(".campoDecimal").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            });
            function AbreModalLog() {
                $('#divLoadShowLogAgenda').dialog({
                    autoopen: false, modal: true, width: 902, height: 340, resizable: false, title: "HISTÓRICO DO AGENDAMENTO DE ATENDIMENTO",
                    //                open: function () { $('#divLoadInfosCadas').show(); }
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalNovoMedic() {
                $('#divLoadShowNovoMedic').dialog({
                    autoopen: false, modal: true, width: 360, height: 390, resizable: false, title: "NOVO MEDICAMENTO",
                    //                open: function () { $('#divLoadInfosCadas').show(); }
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalNovoExam() {
                $('#divLoadShowNovoExam').dialog({
                    autoopen: false, modal: true, width: 560, height: 300, resizable: false, title: "NOVO EXAME",
                    //                open: function () { $('#divLoadInfosCadas').show(); }
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalAtestado() {
                $('#divAtestado').dialog({
                    autoopen: false, modal: true, width: 530, height: 370, resizable: false, title: "EMISSÃO DE ATESTADO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalGuiaPlano() {
                $('#divGuiaPlano').dialog({
                    autoopen: false, modal: true, width: 450, height: 180, resizable: false, title: "IMPRESSÃO DA GUIA DE ATENDIMENTO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalLaudo() {
                $('#divLaudo').dialog({
                    autoopen: false, modal: true, width: 555, height: 350, resizable: false, title: "LAUDO TÉCNICO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalMaisInfo() {
                $('#divMaisInfo').dialog({
                    autoopen: false, modal: true, width: 635, height: 185, left: 365, resizable: false, title: "REGISTRO DE MAIS INFORMAÇÕES",
                    open: function (type, data) { $(this).parent().appendTo("form"); }
                });
            }

            function AbreModalEncamAtend() {
                $('#divEncaminAtend').dialog({
                    autoopen: false, modal: true, width: 280, height: 80, resizable: false, title: "ENCAMINHAMENTO PARA ATENDIMENTO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalObservacao() {
                $('#divObservacao').dialog({
                    autoopen: false, modal: true, width: 560, height: 320, resizable: false, title: "OBSERVAÇÕES DO ATENDIMENTO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalFichaAtendimento() {
                $('#divFichaAtendimento').dialog({
                    autoopen: false, modal: true, width: 470, height: 470, resizable: false, title: "IMPRESSÃO DA FICHA DE ATENDIMENTO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalMedicamentos() {
                $('#divMedicamentos').dialog({
                    autoopen: false, modal: true, width: 800, height: 430, resizable: false, title: "MEDICAMENTOS DO PACIENTE NESTE ATENDIMENTO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalExames() {
                $('#divExames').dialog({
                    autoopen: false, modal: true, width: 800, height: 300, resizable: false, title: "EXAMES DO PACIENTE NESTE ATENDIMENTO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalExameFisico() {
                $('#divExameFisico').dialog({
                    autoopen: false, modal: true, width: 450, height: 300, resizable: false, title: "EXAMES FÍSICOS",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalProtocoloCID() {
                $('#divProtocoloCID').dialog({
                    autoopen: false, modal: true, width: 560, height: 300, resizable: false, title: "CID - PROTOCOLOS",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalProfissionalSolicitado() {
                $('#divObsProSol').dialog({
                    autoopen: false, modal: true, width: 590, height: 480, resizable: false, title: "PARECER PROFISSIONAL MÉDICO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalAmbulatorio() {
                $('#divAmbulatorio').dialog({
                    autoopen: false, modal: true, width: 800, height: 300, resizable: false, title: "PROCEDIMENTOS AMBULATORIAIS DO PACIENTE NESTE ATENDIMENTO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalOrcamentos() {
                $('#divOrcamentos').dialog({
                    autoopen: false, modal: true, width: 580, height: 300, resizable: false, title: "ORÇAMENTO DO PACIENTE NESTE ATENDIMENTO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalAnexos() {
                $('#divAnexos').dialog({
                    autoopen: false, modal: true, width: 880, height: 365, resizable: false, title: "ARQUIVOS ANEXADOS AO PACIENTE",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalAvisoPermissao() {
                $('#divAvisoPermissao').dialog({
                    dialogClass: 'divAvisoPermissao', height: 100, autoopen: false, modal: true, resizable: false, title: "AVISO DE PERMISSÃO",
                    //                open: function () { $('#divLoadInfosCadas').show(); }
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { BackToHome(); }
                });
            }

            function AbreModalInternar() {
                $('#dvIntarnar').dialog({
                    autoopen: false, modal: true, width: 707, height: 500, resizable: false, title: "GUIA DE INTERNAÇÃO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            function AbreModalObito() {
                $('#dvObito').dialog({
                    autoopen: false, modal: true, width: 447, height: 248, resizable: false, title: "ÓBITO",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

            jQuery(function ($) {
                $(".campoCodigo").mask("?9999999999");
                $(".campoQtd").mask("?999");
                $(".campoHora").mask("99:99");
                $(".campoAnos").mask("?99");
                $(".campoPeso").mask("?99,99");
                $(".campoTemp").mask("99.9");
                $(".campoPressArteri").mask("?99/99");
                $(".campoGlicem").mask("?9999");
                $(".campoAltura").mask("9,99");
                $(".campoDecimal").maskMoney({ symbol: "", decimal: ",", thousands: "." });

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

                $("#txtObsServAmbulatoriais").change(function () {
                    $("#hidObsSerAmbulatoriais").val($("#txtObsServAmbulatoriais").val());
                });

                $("#txtObserExame").change(function () {
                    $("#hidObserExame").val($("#txtObserExame").val());
                });

                $("#txtObsOrcam").change(function () {
                    $("#hidObsOrcam").val($("#txtObsOrcam").val());
                });

                if (!($("#rdbMedic").attr("checked"))) {
                    $("#txtMedicamento").enable(false);
                    $("#txtPrincipio").val("");
                };

                if (!($("#rdbPrinc").attr("checked"))) {
                    $("#txtPrincipio").enable(false);
                    $("#txtMedicamento").val("");
                };

                $("#rdbMedic").change(function () {
                    if ($("#rdbMedic").attr("checked")) {
                        $("#txtMedicamento").enable(true);
                        $("#txtPrincipio").enable(false);
                        $("#txtPrincipio").val("");
                    }
                    else {
                        $("#txtMedicamento").enable(false);
                        $("#txtPrincipio").enable(true);
                        $("#txtMedicamento").val("");
                    }
                });

                $("#rdbPrinc").change(function () {
                    if ($("#rdbPrinc").attr("checked")) {
                        $("#txtPrincipio").enable(true);
                        $("#txtMedicamento").enable(false);
                        $("#txtMedicamento").val("");
                    }
                    else {
                        $("#txtPrincipio").enable(false);
                        $("#txtMedicamento").enable(true);
                        $("#txtPrincipio").val("");
                    }
                });

                //Executado ao trocar a seleção no DropDownList para mudar a cor para a correspondente
                $("#ddlClassRisco").change(function (evento) {
                    var e = document.getElementById("ddlClassRisco");
                    var itSelec = e.options[e.selectedIndex].value;
                    if (itSelec == "" || itSelec == 0) {
                        $("#divClassInternar").hide()
                        $("#divClassInternar").css("background-color", "White");
                        $("#divClassInternar").show(100);
                    }
                    else if (itSelec == 1) {
                        $("#divClassInternar").hide()
                        $("#divClassInternar").css("background-color", "Red");
                        $("#divClassInternar").show(100);
                    }
                    else if (itSelec == 2) {
                        $("#divClassInternar").hide();
                        $("#divClassInternar").css("background-color", "Orange");
                        $("#divClassInternar").show(100);
                    }
                    else if (itSelec == 3) {
                        $("#divClassInternar").hide();
                        $("#divClassInternar").css("background-color", "Yellow");
                        $("#divClassInternar").show(100);
                    }
                    else if (itSelec == 4) {
                        $("#divClassInternar").hide();
                        $("#divClassInternar").css("background-color", "Green");
                        $("#divClassInternar").show(100);
                    }
                    else if (itSelec == 5) {
                        $("#divClassInternar").hide();
                        $("#divClassInternar").css("background-color", "Blue");
                        $("#divClassInternar").show(100);
                    }
                });

                //Executado quando se clica no box de cor de class risco, e abre o leque de opções disponíveis com animação
                $("#divClassRisc").click(function () {
                    var sele = $("#hidDivAberta").val();
                    $("#divClassInternar").hide();
                    if (sele != 1) {
                        $("#divClassRisc").animate({
                            height: "30px",
                            width: "240px",
                            margin: "-50px 0 0 -150px",
                            padding: "5px"
                        }, 500, function () {
                            //Ao final da animação, são mostradas as opções de cor
                            $("#divClass1").css("display", "block");
                            $("#divClass2").css("display", "block");
                            $("#divClass3").css("display", "block");
                            $("#divClass4").css("display", "block");
                            $("#divClass5").css("display", "block");

                            //Mostra a imagem para fechar a janela
                            $("#divFecha").css("display", "block");

                            //Remove o ícone de "mãozinha" quando termina a animação
                            $("#divClassRisc").css("cursor", "auto");
                        });
                    }
                });

                function PrepararAnimacao(numClass, cor) {
                    $("#hidDivAberta").val("1");
                    $("#divClass1").css("display", "none");
                    $("#divClass2").css("display", "none");
                    $("#divClass3").css("display", "none");
                    $("#divClass4").css("display", "none");
                    $("#divClass5").css("display", "none");
                    //Esconde a imagem para fechar a janela
                    $("#divFecha").css("display", "none");

                    $("#divClassRisc").animate({
                        width: "11px",
                        margin: "13px 0 0 -17px",
                        height: "9px",
                        padding: "2px"
                    }, 500, function () {
                        //seleciona a classificação de risco correspondente
                        if (numClass != null)
                            $("#ddlClassRisco").val(numClass);

                        //Altera a div pai das classificações conforme necessário
                        $("#divClassRisc").css("cursor", "pointer");

                        if (cor != null)
                            $("#divClassRiscCorSelec").css("background-color", cor);

                        $("#divClassRiscCorSelec").show(100);

                        //Garante que o método pai não vai ser chamado evitando um loop
                        $("#hidDivAberta").val("");
                    });
                };

                //Executado quando é selecionada a classificação de risco de EMERGENCIA (VERMELHA)
                $("#divClass1").click(function () {
                    PrepararAnimacao("1", "Red");
                });

                //Executado quando é selecionada a classificação de risco de MUITO URGENTE (LARANJA)
                $("#divClass2").click(function () {
                    PrepararAnimacao("2", "Orange");
                });

                //Executado quando é selecionada a classificação de risco de URGENTE (AMARELA)
                $("#divClass3").click(function () {
                    PrepararAnimacao("3", "Yellow");
                });

                //Executado quando é selecionada a classificação de risco de POUCO URGENTE (VERDE)
                $("#divClass4").click(function () {
                    PrepararAnimacao("4", "Green");
                });

                //Executado quando é selecionada a classificação de risco de NÃO URGENTE (AZUL)
                $("#divClass5").click(function () {
                    PrepararAnimacao("5", "Blue");
                });

                //Executado quando se clica no botão para fechar a paleta de opções
                $("#lnkClose").click(function () {
                    PrepararAnimacao();
                });
            });

            function hideObsProfSol(btn) {
                AbreModalProfissionalSolicitado();
                $("#coColaboradorParecerMedico").val(btn.id);
            }

        </script>
</asp:Content>
