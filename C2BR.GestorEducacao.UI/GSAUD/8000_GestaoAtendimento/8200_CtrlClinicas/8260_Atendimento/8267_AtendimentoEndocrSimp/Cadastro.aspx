<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8267_AtendimentoEndocrSimp.Cadastro" %>

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
    <div style="float:right; margin-right:10px; margin-top:-18px; color:Green;">
        <asp:CheckBox ID="chkSalvarAutomat" Text="Salvar Atendimento Automaticamente" CssClass="chk" runat="server" />
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
                                                <asp:CheckBox ID="chkSelectPaciente" runat="server" Enabled='<%# Eval("podeClicar") %>' Width="100%" style="margin: 0 0 0 -15px !important;"
                                                    OnCheckedChanged="chkSelectPaciente_OnCheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DTHR" HeaderText="HORÁRIO">
                                            <ItemStyle HorizontalAlign="Center" Width="110px" />
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
                                            <ItemStyle HorizontalAlign="Left" Width="85px" />
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
                                                        <asp:CheckBox runat="server" ID="chkSelectHistAge" Enabled='<%# Eval("podeClicar") %>' Width="100%" style="margin: 0 0 0 -15px !important;"
                                                            OnCheckedChanged="chkSelectHistAge_OnCheckedChanged" AutoPostBack="true" />
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
                                                <asp:TemplateField HeaderText="FA">
                                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgbFicha" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png"
                                                            ToolTip="Ficha de Atendimento" Style="width: 18px !important; height: 18px !important;
                                                            margin: 0 0 0 0 !important" />
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
                <li style="clear: both; width:450px; margin: 108px 0 0 6px;">
                    <ul style="width:310px;">
                        <li style="width:100%; color:Blue; border-bottom:2px solid #58ACFA; margin-bottom:2px;">
                            <label style="font-size:12px;">Observações</label>
                        </li>
                        <li style="clear:both; width:99%;">
                            <asp:TextBox ID="txtObserAtend" Rows="9" Width="100%" Font-Size="12px" TextMode="MultiLine" runat="server" />
                        </li>
                    </ul>
                    <ul style="margin-top:-18px; width:130px; float:right;">
                        <li style="width:100%; color:Blue; border-bottom:2px solid #58ACFA;">
                            <asp:Label ID="Label3" Text="Inform." style="font-size:12px;" runat="server" />
                            <strong style="margin-left:15px;">PESO</strong>
                            <strong style="margin-left:8px;">%GOR</strong>
                        </li>
                        <li style="width:50px; color:Green;">
                            <label>ATUAL</label>
                        </li>
                        <li style="width:30px; margin-left:2px;">
                            <asp:TextBox ID="txtPesoAtual" Height="12px" Width="100%" CssClass="campoDecimal" BackColor="#ccffcc" runat="server" />
                        </li>
                        <li style="width:30px; margin-left:5px; margin-right:-5px;">
                            <asp:TextBox ID="txtGordAtual" Height="12px" Width="100%" CssClass="campoDecimal" BackColor="#ccffcc" runat="server" />
                        </li>
                        <li style="margin-top:-10px; width:50px; color:Blue;">
                            <label>ANTERIOR</label>
                        </li>
                        <li style="width:30px; margin:-10px 0 0 2px;">
                            <asp:TextBox ID="txtPesoAnterior" Height="12px" Width="100%" CssClass="campoDecimal" runat="server" />
                        </li>
                        <li style="width:30px; margin:-10px -5px 0 10px;">
                            <asp:TextBox ID="txtGordAnterior" Height="12px" Width="100%" CssClass="campoDecimal" runat="server" />
                        </li>
                        <li style="margin-top:-5px; width:50px;">
                            <label>Desejado</label>
                        </li>
                        <li style="width:30px; margin:-5px 0 0 2px;">
                            <asp:TextBox ID="txtPesoDesejado" Height="12px" Width="100%" CssClass="campoDecimal" runat="server" />
                        </li>
                        <li style="width:30px; margin:-5px -5px 0 10px;">
                            <asp:TextBox ID="txtGordDesejada" Height="12px" Width="100%" CssClass="campoDecimal" runat="server" />
                        </li>
                        <li style="margin-top:-5px; width:50px;">
                            <label>Meta</label>
                        </li>
                        <li style="width:30px; margin:-5px 0 0 2px;">
                            <asp:TextBox ID="txtPesoMeta" Height="12px" Width="100%" CssClass="campoDecimal" runat="server" />
                        </li>
                        <li style="width:30px; margin:-5px -5px 0 10px;">
                            <asp:TextBox ID="txtGordMeta" Height="12px" Width="100%" CssClass="campoDecimal" runat="server" />
                        </li>
                        <li style="margin-top:-5px; width:50px;">
                            <label>Magro</label>
                        </li>
                        <li style="width:30px; margin:-5px 0 0 2px;">
                            <asp:TextBox ID="txtPesoMagro" Height="12px" Width="100%" CssClass="campoDecimal" runat="server" />
                        </li>
                        <li style="width:30px; margin:-5px -5px 0 10px;">
                            <asp:TextBox ID="txtGordMagra" Height="12px" Width="100%" CssClass="campoDecimal" runat="server" />
                        </li>
                        <li style="margin-top:-5px; width:50px;">
                            <label>Gordo</label>
                        </li>
                        <li style="width:30px; margin:-5px 0 0 2px;">
                            <asp:TextBox ID="txtPesoGordo" Height="12px" Width="100%" CssClass="campoDecimal" runat="server" />
                        </li>
                        <li style="width:30px; margin:-5px -5px 0 10px;">
                            <asp:TextBox ID="txtGordGorda" Height="12px" Width="100%" CssClass="campoDecimal" runat="server" />
                        </li>
                        <li style="margin-top:-7px; width:100%; border-top:2px solid #58ACFA;"></li>
                        <li style="margin-top:-2px; width:50px; color:Orange;">
                            <label>ESTATURA</label>
                        </li>
                        <li style="width:30px; margin:-2px 0 0 2px;">
                            <asp:TextBox ID="txtAltura" Height="12px" Width="100%" CssClass="campoAltura" runat="server" />
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
                            <label>Profissional Responsável</label>
                            <asp:DropDownList ID="drpProfResp" Width="185px" runat="server" />
                        </li>
                        <li style="margin-left: 170px;">
                            <label>Dt. Atendimento</label>
                            <asp:TextBox ID="txtDtAtend" runat="server" CssClass="campoData" />
                        </li>
                        <li style="margin-left: -1px;">
                            <label>Hora</label>
                            <asp:TextBox ID="txtHrAtend" CssClass="campoHora" Width="28px" runat="server" />
                        </li>
                        <li style="margin-left: -1px;">
                            <label>SENHA</label>
                            <asp:TextBox ID="txtSenha" Width="35px" BackColor="Yellow" runat="server" />
                        </li>
                        <li class="liTituloGrid" style="width: 508px !important; height: 20px !important; clear:both;
                            margin-right: 0px; background-color: #FFEC8B; text-align: center; font-weight: bold;
                            margin-bottom: 2px; padding-top: 2px;">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: Black;
                                float: left; margin-left: 10px;">
                                REGISTRO DO ATENDIMENTO</label>
                            <div style="margin-right: 3px; float: right; margin-top: 4px;">
                                <img title="Emitir Prontuário do Paciente" style="margin-top: -2px" src="/BarrasFerramentas/Icones/Imprimir.png" height="16px" width="16px" />
                                <asp:LinkButton ID="lnkbProntuario" runat="server" OnClick="lnkbProntuario_OnClick" ForeColor="#0099ff">PRONTUÁRIO</asp:LinkButton>
                            </div>
                            <div id="divBtnOdontograma" runat="server" style="margin-right: 5px; float: right; margin-top: 4px;">
                                <img title="Emitir Odontograma do Paciente" style="margin-top: -2px" src="/Library/IMG/PGS_IC_Anexo.png" height="16px" width="16px" />
                                <asp:LinkButton ID="lnkbOdontograma" runat="server" ForeColor="#0099ff">HISTÓRICO</asp:LinkButton>
                            </div>
                        </li>
                        <li style="margin: -2px 0 0 5px; border:1px solid #FFEC8B; border-top:0;">
                            <ul>
                                <li style="margin-top:2px;">
                                    <label Style="color:Orange; margin-left:3px; font-size:9px;">QUEIXA PRINCIPAL</label>
                                    <asp:TextBox runat="server" ID="txtQueixa" BackColor="#FAFAFA" TextMode="MultiLine" Rows="2" Style="width: 496px; margin-top:1px;
                                        border-top:1px solid #BDBDBD; border-left:0; border-right:0; border-bottom:0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top:-6px;">
                                    <label Style="color:Orange; margin-left:3px; font-size:9px;">ANAMNESE / HDA (História da Doença Atual)</label>
                                    <asp:TextBox runat="server" ID="txtHDA" BackColor="#FAFAFA" TextMode="MultiLine" Rows="7" Style="width: 496px; margin-top:1px;
                                        border-top:1px solid #BDBDBD; border-left:0; border-right:0; border-bottom:0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top:-6px;">
                                    <label Style="color:Orange; margin-left:3px; font-size:9px;">HIPÓTESE DIAGNÓSTICA / AÇÃO REALIZADA</label>
                                    <asp:TextBox runat="server" ID="txtHipotese" BackColor="#FAFAFA" TextMode="MultiLine" Rows="4" Style="width: 496px;
                                        border-top:1px solid #BDBDBD; border-left:0; border-right:0; border-bottom:0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top:-6px;">
                                    <label Style="color:Orange; margin-left:3px; font-size:9px;">RESULTADO DE EXAMES</label>
                                    <asp:TextBox runat="server" ID="txtExameFis" BackColor="#FAFAFA" TextMode="MultiLine" Rows="4" Style="width: 496px;
                                        border-top:1px solid #BDBDBD; border-left:0; border-right:0; border-bottom:0;" Font-Size="12px"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li>
                    <ul>
                        <li style="margin-left: 5px; margin-top:8px; height:15px; cursor: pointer">
                            <asp:LinkButton ID="lnkbFotos" AccessKey="F" OnClick="lnkbAnexos_OnClick" ToolTip="Fotos associados ao Atendimento/Paciente" runat="server">
                                <img class="imgFotos" style="width: 18px; height: 18px !important" src="/Library/IMG/PGS_IC_Imagens.jpg" alt="Icone" />
                                <label style="margin: 3px 0 0 5px; float: right">FOTOS</label>
                            </asp:LinkButton>
                        </li>
                        <li style="margin-top: 8px; height:15px; cursor: pointer">
                            <asp:LinkButton ID="lnkbVideos" AccessKey="V" OnClick="lnkbAnexos_OnClick" ToolTip="Videos associados ao Atendimento/Paciente" runat="server">
                                <img class="imgVideos" style="width: 18px; height: 18px !important" src="/Library/IMG/PGS_IC_Imagens2.png" alt="Icone" />
                                <label style="margin: 3px 0 0 5px; float: right">VIDEO</label>
                            </asp:LinkButton>
                        </li>
                        <li style="margin-top: 8px; height:15px; cursor: pointer">
                            <asp:LinkButton ID="lnkbAudios" AccessKey="U" OnClick="lnkbAnexos_OnClick" ToolTip="Áudios associados ao Atendimento/Paciente" runat="server">
                                <img class="imgAudios" style="width: 16px; height: 16px !important" src="/Library/IMG/PGS_IC_ArquivoAudio.png" alt="Icone" />
                                <label style="margin: 3px 0 0 5px; float: right">IMAGEM</label>
                            </asp:LinkButton>
                        </li>
                        <li style="margin-top:8px; height:15px;">
                            <asp:LinkButton ID="lnkbAnexos" AccessKey="A" OnClick="lnkbAnexos_OnClick" ToolTip="Anexos associados ao Atendimento/Paciente" runat="server">
                                <img class="imgAnexos" style="width: 16px; height: 16px !important" src="/Library/IMG/PGS_IC_Anexo.png" alt="Icone" />
                                <label style="margin: 3px 0 0 5px; float: right">ANEXOS</label>
                            </asp:LinkButton>
                        </li>
                    </ul>
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
            <div id="divAtestado" style="display: none; width:500px; height: 350px !important;">
                <ul class="ulDados" style="float:left; width:500px; margin-top: 0px !important;">
                    <li>
                        <label title="Data Comparecimento" class="lblObrigatorio">Data</label>
                        <asp:TextBox ID="txtDtAtestado" runat="server" ValidationGroup="atestado" class="campoData" ToolTip="Informe a data de comparecimento"
                            AutoPostBack="true" OnTextChanged="txtDtAtestado_OnTextChanged"/>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="atestado" ID="rfvDtAtestado" CssClass="validatorField"
                            ErrorMessage="O campo data é requerido" ControlToValidate="txtDtAtestado"></asp:RequiredFieldValidator>
                    </li>
                    <li style="margin-top: 0px !important; margin-left:70px !important;">
                        <label title="Tipo Documento">Atestado</label>
                        <asp:RadioButton ID="chkAtestado" GroupName="TipoDoc" CssClass="chk" style="margin-left: -7px !important;" OnCheckedChanged="chkAtestado_OnCheckedChanged" AutoPostBack="true" runat="server" ToolTip="Imprimir um atestado médico" />
                        <%--<!--<asp:CheckBox ID="chkAtestado" OnCheckedChanged="chkAtestado_OnCheckedChanged" AutoPostBack="true" 
                            CssClass="chk" style="margin-left: -7px !important;" Checked="true" runat="server" ToolTip="Imprimir um atestado médico" />-->--%>
                        Dias <asp:TextBox ID="txtQtdDias" runat="server" Width="20px" MaxLength="3" ToolTip="Informe a quantidade de dias de repouso do paciente">
                             </asp:TextBox>
                            <asp:CheckBox ID="chkCid" CssClass="chk" style="margin-right: -6px !important;" Checked="true" runat="server" ToolTip="Imprimir CID no atestado médico" />
                        CID <asp:TextBox ID="txtCid" runat="server" Width="25px" MaxLength="5" ToolTip="CID">
                            </asp:TextBox>
                    </li>
                    <li style="margin-top: 0px !important; margin-left:55px !important;">
                        <label title="Tipo Documento">Comparecimento</label>
                        <asp:RadioButton ID="chkComparecimento" GroupName="TipoDoc" CssClass="chk" style="margin-left: -7px !important;" OnCheckedChanged="chkComparecimento_OnCheckedChanged" AutoPostBack="true" runat="server" ToolTip="Imprimir uma declaração de comparecimento" />
                        Período <asp:DropDownList ID="drpPrdComparecimento" runat="server" style="vertical-align:top;" ToolTip="Período de comparecimento">
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
                    <li id="li10" runat="server" class="liBtnAddA" style="margin-top: 10px !important; margin-left:230px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbGerarAtestado" OnClick="lnkbGerarAtestado_Click" runat="server" ValidationGroup="atestado" ToolTip="Emitir documento">
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
    </ul>
    <div id="divLaudo" style="display: none; height: 240px !important;">
        <asp:HiddenField ID="hidIdLaudo" runat="server" />
        <ul class="ulDados" style="width: 545px; margin-top: 0px !important">
            <li>
                <label title="Paciente" class="lblObrigatorio">Paciente</label>
                <asp:DropDownList ID="drpPacienteLaudo" OnSelectedIndexChanged="drpPacienteLaudo_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="225px" />
                <asp:RequiredFieldValidator ValidationGroup="laudo" runat="server" ID="rfvPacienteLaudo" CssClass="validatorField"
                ErrorMessage="O paciente é obrigatório" ControlToValidate="drpPacienteLaudo" Display="Dynamic" />
            </li>
            <li>
                <label title="Título do Laudo">Título do Laudo</label>
                <asp:TextBox ID="txtTituloLaudo" runat="server" Width="200px" Text="LAUDO TÉCNICO" />
            </li>
            <li>
                <label title="Data Laudo">Data Laudo</label>
                <asp:TextBox ID="txtDtLaudo" runat="server" class="campoData" ToolTip="Informe a data do laudo" />
            </li>
            <li style="clear: both;">
                <label title="Laudo Técnico" style="color:Blue;">LAUDO TÉCNICO</label>
                <asp:TextBox ID="txtObsLaudo" Width="520px" Height="200px" TextMode="MultiLine" runat="server" />
            </li>
            <li class="liBtnAddA" style="clear: none !important; margin-left: 250px !important; margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirLaudo" ValidationGroup="laudo" runat="server" OnClick="lnkbImprimirLaudo_OnClick" ToolTip="Imprimir laudo técnico">
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
    <asp:HiddenField ID="hidTxtObserv" runat="server" />
    <div id="divObservacao" style="display: none; height: 300px !important; width: 580px">
        <ul>
            <li>
                <asp:TextBox runat="server" ID="txtObservacoes" TextMode="MultiLine" Style="width: 530px;
                    height: 240px;" Font-Size="13px" ToolTip="Digite as observações sobre o Atendimento"></asp:TextBox>
            </li>
            <li class="liBtnConfirm" style="margin:10px 0 0 240px; width: 45px;">
                <asp:LinkButton ID="lnkbSalvarObserv" runat="server" OnClick="lnkbSalvarObserv_OnClick">
                    <label style="margin-left:5px; color:White;">SALVAR</label>
                </asp:LinkButton>
            </li>
            <li class="liBtnAddA" style="margin:-22px 10px 0 0; float:right; width: 45px;">
                <asp:LinkButton ID="lnkbImprimirObserv" runat="server" OnClick="lnkbImprimirObserv_OnClick">
                    <label style="margin-left:5px;">EMITIR</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divFichaAtendimento" style="display: none; height: 470px !important;">
        <ul class="ulDados" style="width: 410px; margin-top: 0px !important">
            <li>
                <label>Queixas</label>
                <asp:TextBox runat="server" ID="txtQxsFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>Anamnese</label>
                <asp:TextBox runat="server" ID="txtAnamneseFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>Diagnostico</label>
                <asp:TextBox runat="server" ID="txtDiagnosticoFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>Exame</label>
                <asp:TextBox runat="server" ID="txtExameFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
            </li>
            <li>
                <label>Observação</label>
                <asp:TextBox runat="server" ID="txtObsFicha" TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
            </li>
            <li id="li5" runat="server" class="liBtnAddA" style="float: right; margin-top: 10px !important;
                clear: none !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirFicha" runat="server" OnClick="lnkbImprimirFicha_Click" ToolTip="Imprimir ficha de atendimento">
                    <asp:Label runat="server" ID="lblEmitirFicha" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divAnexos" style="display: none; height: 310px !important;">
        <asp:HiddenField runat="server" ID="hidTpAnexo" />
        <ul class="ulDados" style="width: 530px; margin-left:-10px !important; margin-top: 0px !important">
            <li style="width:98%;">
                <label style="color:Blue">ANEXAR <asp:Label ID="lblTpAnexo" Text="ARQUIVO" runat="server" /></label>
                <hr />
            </li>
            <li style="clear:both;">
                <label>Nome</label>
                <asp:TextBox ID="txtNomeAnexo" runat="server" Width="200px" ClientIDMode="Static" />
            </li>
            <li style="float:right; margin:8px 10px 0 0;">
                <asp:FileUpload ID="flupAnexo" runat="server" ClientIDMode="Static" />
            </li>
            <li style="clear:both; margin-top:-10px;">
                <label>Observações</label>
                <asp:TextBox ID="txtObservAnexo" TextMode="MultiLine" Width="320px" Rows="3" runat="server" />
            </li>
            <li class="liBtnAddA" style="margin:-25px 10px 0 0; float:right; width: 45px;">
                <asp:LinkButton ID="lnkbAnexar" runat="server" OnClick="lnkbAnexar_OnClick">
                    <label style="margin-left:5px;">ANEXAR</label>
                </asp:LinkButton>
            </li>
            <li class="liTituloGrid" style="width: 520px !important; height: 20px !important;
                background-color: #A9E2F3; margin-bottom: 2px; padding-top: 2px; clear:both;">
                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;
                    float: left; margin-left: 10px;">
                    ARQUIVOS ANEXADOS</label>
            </li>
            <li style="clear: both; margin: 0 0 0 5px !important;">
                <div style="width: 518px; height: 130px; border: 1px solid #CCC; overflow-y: scroll">
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
                            <asp:BoundField DataField="NU_REGIS" HeaderText="RAP">
                                <ItemStyle Width="30px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NM_TITULO" HeaderText="NOME">
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="OBSERVAÇÕES">
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidIdAnexo" Value='<%# Eval("ID_ANEXO_ATEND") %>' />
                                    <asp:Label ID="Label2" Text='<%# Eval("DE_OBSER_RES") %>' ToolTip='<%# Eval("DE_OBSER") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BX">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgbBxrAnexo" ImageUrl="/Library/IMG/Gestor_ServicosDownloadArquivos.png"
                                        ToolTip="Baixar Arquivo" OnClick="imgbBxrAnexo_OnClick" Width="16" Height="16" Style="margin: 0 0 0 -3px !important;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EX">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgbExcAnexo" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                        ToolTip="Excluir Arquivo" OnClick="imgbExcAnexo_OnClick" Width="16" Height="16" Style="margin: 0 0 0 -3px !important;"
                                        OnClientClick="return confirm ('Tem certeza de que deseja excluir o arquivo?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
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
        });

        function AbreModalLog() {
            $('#divLoadShowLogAgenda').dialog({ autoopen: false, modal: true, width: 902, height: 340, resizable: false, title: "HISTÓRICO DO AGENDAMENTO DE ATENDIMENTO",
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

        function AbreModalAnexos() {
            $('#divAnexos').dialog({ autoopen: false, modal: true, width: 530, height: 315, resizable: false, title: "ARQUIVOS ANEXADOS AO(S) ATENDIMENTO(S)",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        jQuery(function ($) {
            $(".campoCodigo").mask("?9999999999");
            $(".campoQtd").mask("?999");
            $(".campoHora").mask("99:99");
            $(".campoAnos").mask("?99");
            $(".campoAltura").mask("9,99");
            $(".campoDecimal").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });

    </script>
</asp:Content>
