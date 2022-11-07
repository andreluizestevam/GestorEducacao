<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8263_AtendimentoMedico.Cadastro" %>

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
        .ulMedicExam
        {
            width: 496px;
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
                            <div style="width: 448px; height: 143px; border: 1px solid #CCC; overflow-y: scroll"
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
                                                <asp:HiddenField ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' runat="server" />
                                                <asp:HiddenField ID="hidSituacao" Value='<%# Eval("Situacao") %>' runat="server" />
                                                <asp:CheckBox ID="chkSelectPaciente" runat="server" Width="100%" style="margin: 0 -5px 0 -20px !important;" Enabled='<%# Eval("podeClicar") %>'
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
                <li style="clear: both; margin: 2px 0 0 10px;">
                    <ul>
                        <li>
                            <ul style="width: 443px">
                                <li class="liTituloGrid" style="width: 405px; height: 20px !important; margin-left: -5px;
                                    background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                                    <ul>
                                        <li style="margin-left: 5px; float: left; width:60px;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FF6347">
                                                ORÇAMENTO</label>
                                        </li>
                                        <li style="float: right; margin-top: 3px; margin-right: 10px;">
                                            <ul>
                                                <li>
                                                    <asp:Label runat="server" ID="lbloper">Contratação</asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddlOperOrc" Width="100" OnSelectedIndexChanged="ddlOperOrc_OnSelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </li>
                                                <li style="margin-left: -2px;">
                                                    <asp:Label runat="server" ID="Label3">Plano</asp:Label>
                                                    <asp:DropDownList runat="server" Width="100" ID="ddlPlanOrc">
                                                    </asp:DropDownList>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li id="li3" runat="server" title="Clique para adicionar um item ao orçamento do atendimento"
                                    class="liBtnAddA" style="float: right; margin: -25px 22px 3px 0px; height:15px; width: 12px;">
                                    <asp:ImageButton ID="btnAddProcOrc" height="15px" width="15px" style="margin-top: -1px; margin-left:-2px;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" OnClick="btnAddProcOrc_OnClick" runat="server" />
                                </li>
                                <li id="li11" runat="server" title="Clique para emitir o orçamento do paciente"
                                    class="liBtnAddA" style="float: right; margin: -25px -2px 3px 5px; height:15px; width: 12px;">
                                    <asp:ImageButton ID="lnkbOrcamento" runat="server" OnClick="lnkbOrcamento_Click" ToolTip="Emitir Orçamento do Paciente" style="margin-top: -2px; margin-left:-3px;" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png" Height="18px" Width="18px" />
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both; margin: -7px 0 0 0;">
                            <div style="width: 448px; height: 110px; border: 1px solid #CCC; overflow-y: scroll">
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
                                        <asp:TemplateField HeaderText="CÓDIGO">
                                            <ItemStyle Width="56px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlProcedOrc" OnSelectedIndexChanged="ddlProcedOrc_OnSelectedIndexChanged"
                                                    AutoPostBack="true" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                </asp:DropDownList>
                                                <asp:HiddenField runat="server" ID="hidValUnitProc" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PROCEDIMENTO">
                                            <ItemStyle Width="180px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtCodigProcedOrc" Width="100%" Style="margin-left: -4px; margin-bottom:0px;"
                                                    Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTD">
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtQtdProcedOrc" Width="100%" Style="margin-left: -4px; margin-bottom:0px;"
                                                    OnTextChanged="txtQtdProcedOrc_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VALOR">
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtValorProcedOrc" Width="100%" CssClass="campoDinheiro" Style="margin-left: -4px; margin-bottom:0px;"
                                                    OnTextChanged="txtValorProcedOrc_OnTextChanged" AutoPostBack="true"></asp:TextBox>
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
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                        <li style="margin-top: 1px; margin-left:-2px;">
                            <ul>
                                <li style="margin-top: 5px;">
                                    <asp:Label runat="server" ID="Label8">VALIDADE </asp:Label>
                                    <asp:TextBox runat="server" ID="txtDtValidade" Style="" CssClass="campoData"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="Label7">DESCONTO - R$</asp:Label>
                                    <asp:TextBox runat="server" ID="txtVlDscto" Style="margin-top:4px; height: 16px !important; width: 45px" CssClass="campoDinheiro campoMoeda" 
                                        OnTextChanged="txtVlDscto_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lbl">TOTAL - R$</asp:Label>
                                    <asp:TextBox runat="server" ID="txtVlTotalOrcam" Enabled="false" Style="margin-top:4px; height: 16px !important; width: 47px" CssClass="campoMoeda" ></asp:TextBox>
                                </li>
                                <li style="margin-top: 5px; margin-left:2px;">
                                    <asp:CheckBox ID="chkAprovado" CssClass="chk" runat="server" Text="Aprovado" ToolTip="Marque se o orçamento foi aprovado para o faturamento"/>
                                </li>
                            </ul>
                        </li>
                        <li style="margin: -13px 0 0 0; width:375px;">
                            <label Style="color:Orange; margin-left:0px;">OBSERVAÇÕES DO ORÇAMENTO</label>
                            <asp:TextBox runat="server" ID="txtObsOrcam" TextMode="MultiLine" Style="width: 365px;
                                height: 80px;" Font-Size="12px" ToolTip="Digite as observações sobre o Orçamento"></asp:TextBox>
                        </li>
                        <li style="margin: -13px 0 0 5px; width:60px;">
                            <label Style="color:Orange; margin-left:10px;">ANEXOS</label><hr />
                        </li>
                        <li style="margin-left: 5px; margin-top:2px; height:15px;">
                            <asp:LinkButton ID="lnkbFotos" AccessKey="F" OnClick="lnkbAnexos_OnClick" ToolTip="Fotos associados ao Atendimento/Paciente" runat="server">
                                <img class="imgFotos" style="width: 18px; height: 18px !important" src="/Library/IMG/PGS_IC_Imagens.jpg" alt="Icone" />
                                <label style="margin: 3px 0 0 5px; float: right">FOTOS</label>
                            </asp:LinkButton>
                        </li>
                        <li class="lnkImagens" style="margin-top: -2px; height:15px; cursor: pointer">
                            <asp:LinkButton ID="lnkbVideos" AccessKey="V" OnClick="lnkbAnexos_OnClick" ToolTip="Videos associados ao Atendimento/Paciente" runat="server">
                                <img class="imgVideos" style="width: 18px; height: 18px !important" src="/Library/IMG/PGS_IC_Imagens2.png" alt="Icone" />
                                <label style="margin: 3px 0 0 5px; float: right">VIDEO</label>
                            </asp:LinkButton>
                        </li>
                        <li class="lnkAudios" style="margin-top: 2px; height:15px; cursor: pointer">
                            <asp:LinkButton ID="lnkbAudios" AccessKey="U" OnClick="lnkbAnexos_OnClick" ToolTip="Áudios associados ao Atendimento/Paciente" runat="server">
                                <img class="imgAudios" style="width: 16px; height: 16px !important" src="/Library/IMG/PGS_IC_ArquivoAudio.png" alt="Icone" />
                                <label style="margin: 2px 0 0 5px; float: right">ÁUDIO</label>
                            </asp:LinkButton>
                        </li>
                        <li style="margin-top:2px;">
                            <asp:LinkButton ID="lnkbAnexos" AccessKey="A" OnClick="lnkbAnexos_OnClick" ToolTip="Anexos associados ao Atendimento/Paciente" runat="server">
                                <img class="imgAnexos" style="width: 16px; height: 16px !important" src="/Library/IMG/PGS_IC_Anexo.png" alt="Icone" />
                                <label style="margin: 1px 0 0 5px; float: right">ANEXOS</label>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li style="float: right; width: 550px !important;">
            <ul style="margin-left: -18px;">
                <li>
                    <ul>
                        <li class="liTituloGrid" style="width: 508px !important; height: 20px !important;
                            margin-right: 0px; background-color: #FFEC8B; text-align: center; font-weight: bold;
                            margin-bottom: 2px; padding-top: 2px;">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: Black;
                                float: left; margin-left: 10px;">
                                REGISTRO DE INFORMAÇÕES DO PACIENTE</label>
                            <div style="margin-right: 3px; float: right; margin-top: 4px;">
                                <img title="Emitir Prontuário do Paciente" style="margin-top: -2px" src="/BarrasFerramentas/Icones/Imprimir.png" height="16px" width="16px" />
                                <asp:LinkButton ID="lnkbProntuario" runat="server" OnClick="lnkbProntuario_OnClick" ForeColor="#0099ff">PRONTUÁRIO</asp:LinkButton>
                            </div>
                            <div id="divBtnOdontograma" runat="server" style="margin-right: 5px; float: right; margin-top: 4px;">
                                <img title="Emitir Odontograma do Paciente" style="margin-top: -2px" src="/Library/IMG/PGS_IC_Odontograma.png" height="16px" width="16px" />
                                <asp:LinkButton ID="lnkbOdontograma" runat="server" OnClick="lnkbOdontograma_OnClick" ForeColor="#0099ff">ODONTOGRAMA</asp:LinkButton>
                            </div>
                        </li>
                        <li style="margin: -2px 0 0 5px; border:1px solid #FFEC8B; border-top:0;">
                            <ul>
                                <li style="margin-top:2px;">
                                    <label Style="color:Orange; margin-left:3px; font-size:9px;">QUEIXA PRINCIPAL</label>
                                    <asp:TextBox runat="server" ID="txtQueixa" BackColor="#FAFAFA" TextMode="MultiLine" Style="width: 496px; height: 15px; margin-top:1px;
                                        border-top:1px solid #BDBDBD; border-left:0; border-right:0; border-bottom:0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top:-6px;">
                                    <label Style="color:Orange; margin-left:3px; font-size:9px;">HDA (História da Doença Atual)</label>
                                    <asp:TextBox runat="server" ID="txtHDA" BackColor="#FAFAFA" TextMode="MultiLine" Style="width: 496px; height: 53px; margin-top:1px;
                                        border-top:1px solid #BDBDBD; border-left:0; border-right:0; border-bottom:0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top:-6px;">
                                    <label Style="color:Orange; margin-left:3px; font-size:9px;">EXAME FÍSICO</label>
                                    <asp:TextBox runat="server" ID="txtExameFis" BackColor="#FAFAFA" TextMode="MultiLine" Style="width: 496px; height: 40px;
                                        border-top:1px solid #BDBDBD; border-left:0; border-right:0; border-bottom:0;" Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top:-6px;">
                                    <label Style="color:Orange; margin-left:3px; font-size:9px;">HIPÓTESE DIAGNÓSTICA</label>
                                    <asp:TextBox runat="server" ID="txtHipotese" BackColor="#FAFAFA" TextMode="MultiLine" Style="width: 496px; height: 28px;
                                        border-top:1px solid #BDBDBD; border-left:0; border-right:0; border-bottom:0;" Font-Size="12px"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both; margin-top:7px;">
                            <ul>
                                <li style="float: left">
                                    <ul>
                                        <li>
                                            <ul class="ulMedicExam">
                                                <li class="liTituloGrid" style="height: 20px !important; width: 413px; margin-left: -10px;
                                                    background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                                                    <ul>
                                                        <li style="margin: 0 0 0 10px; float: left">
                                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                                MEDICAMENTOS</label>
                                                        </li>
                                                        <li style="margin-top:2px; margin-right:-3px;">
                                                            <asp:CheckBox ID="chkAlergiaMedic" CssClass="chk" runat="server" style="font-family: Arial Narrow;" Text="Alergia" ToolTip="O paciente possui alguma alergia a medicamento?" ClientIDMode="Static" />
                                                            <asp:TextBox ID="txtAlergiaMedic" Width="250px" Height="14px" runat="server" Font-Size="11px" ClientIDMode="Static"
                                                                Placeholder="Qual(is) Medicamento(s)?" ToolTip="Informe qual(is) medicamento(s) que o paciente possui alergia" />
                                                        </li>
                                                    </ul>
                                                </li>
                                                <li id="li8" runat="server" title="Clique para cadastrar um novo medicamento"
                                                    class="liBtnAddA" style="float: right; margin: -25px 44px 3px 5px; width: 40px">
                                                    <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Medicamento" src="../../../../../Library/IMG/Gestor_BtnEdit.png"
                                                        height="15px" width="15px" />
                                                    <asp:LinkButton ID="lnkNovoMedicam" runat="server" OnClick="lnkNovoMedicam_OnClick">Novo</asp:LinkButton>
                                                </li>
                                                <li id="li1" runat="server" title="Clique para adicionar um medicamento ao atendimento"
                                                    class="liBtnAddA" style="float: right; margin: -25px 22px 3px 5px; height:15px; width: 12px">
                                                    <asp:ImageButton ID="lnkAddMedicam" height="15px" width="15px" style="margin-top: -1px; margin-left:-2px;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" OnClick="lnkAddMedicam_OnClick" runat="server" />
                                                </li>
                                                <li title="Clique para emitir o Receituario do atendimento" class="liBtnAddA" style="float: right; margin: -25px -2px 3px 0px; width: 12px; height:15px;">
                                                    <asp:ImageButton ID="BtnReceituario" runat="server" OnClick="BtnReceituario_Click" ToolTip="Emitir Receituario do Paciente" style="margin-top: -2px; margin-left:-3px;" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png" Height="18px" Width="18px" />
                                                </li>
                                            </ul>
                                        </li>
                                        <li style="clear: both; margin: -7px 0 0 -5px;">
                                            <div style="width: 506px; height: 42px; border: 1px solid #CCC; overflow-y: scroll">
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
                                                            <ItemStyle Width="130px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:DropDownList runat="server" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" ID="ddlMedic">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prescrição">
                                                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPrescricao" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" ToolTip="Informe a prescrição médica" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="QTD">
                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQtd" Width="100%" CssClass="campoQtd" Style="margin-left: -4px; margin-bottom:0px;" runat="server" ToolTip="Informe a quantidade utilizada do medicamento" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="USO">
                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtUso" Width="100%" CssClass="campoQtd" Style="margin-left: -4px; margin-bottom:0px;" runat="server" ToolTip="Informe a quantidade de dias de uso do medicamento, sendo 0 como Uso Contínuo" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EX">
                                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
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
                                            <asp:TextBox runat="server" ID="txtObserMedicam" TextMode="MultiLine" Style="width: 506px;
                                                height: 15px;" Font-Size="11px" placeholder=" Digite as observações sobre Medicamentos"></asp:TextBox>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li style="clear: both; margin-top: -10px;">
                    <ul>
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
                                    class="liBtnAddA" style="float: right; margin: -25px 22px 3px 5px; height:15px; width: 12px;">
                                    <asp:ImageButton ID="lnkAddProcPla" height="15px" width="15px" style="margin-top: -1px; margin-left:-2px;" ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" OnClick="lnkAddProcPla_OnClick" runat="server" />
                                </li>
                                <li title="Clique para emitir os exames do paciente" class="liBtnAddA" style="float: right; margin: -25px -2px 3px 0px; width: 12px; height:15px;">
                                    <asp:ImageButton ID="BtnExames" runat="server" OnClick="BtnExames_OnClick" ToolTip="Emitir Exames do Paciente" style="margin-top: -2px; margin-left:-3px;" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png" Height="18px" Width="18px" />
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
                                                <asp:TextBox runat="server" ID="txtCodigProcedPla" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" Enabled="false"></asp:TextBox>
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
                            <asp:TextBox runat="server" ID="txtObserExame" TextMode="MultiLine" Style="width: 506px;
                                height: 15px;" Font-Size="11px" placeholder=" Digite as observações sobre Exames"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="margin: 5px 0 0 0px;">
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
                                    Nenhum log associado<br />
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
            <div class="liTituloGrid" style="display: none; width: 450px; height: 20px !important; margin-right: 0px;
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
                        <div id="divDemonAge" style="width: 448px; height: 90px; border: 1px solid #CCC;
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
                                    <asp:TemplateField HeaderText="ST">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgSituacaoHistorico" Style="width: 18px !important;
                                                height: 18px !important; margin: 0 0 0 0 !important" ImageUrl='<%# Eval("imagem_URL") %>'
                                                ToolTip='<%# Eval("imagem_TIP") %>' />
                                            <asp:HiddenField runat="server" ID="hidIdAgenda" Value='<%# Eval("ID_AGENDA") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="dtAgenda_V" HeaderText="AGENDA">
                                        <ItemStyle HorizontalAlign="Left" Width="115px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="SA">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <img class="imgsa" style="width: 16px; height: 16px !important" src='<%# Eval("imagem_URL_ACAO") %>'
                                                alt="Icone" title="Ação já realizada ou não" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="dtAgenda_Atend_V" HeaderText="REALIZADO">
                                        <ItemStyle HorizontalAlign="Center" Width="75px" />
                                        <HeaderStyle HorizontalAlign="Center" />
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
        <li>
            <div id="divOdontograma" style="display: none; height: 440px !important;">
                <ul class="ulDados" style="width: 740px; margin-top: 0px !important">
                    <li>
                        <img title="Odontograma do Paciente" style="margin-top: 10px" src="/Library/IMG/imgOdontograma.png" />
                    </li>
                    <li>
                        
                    </li>
                    <li class="liBtnAddA" style="clear: none !important; margin-left: 330px !important; margin-top: 8px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbImprimirOdontograma" runat="server" ValidationGroup="guia" OnClick="lnkbImprimirOdontograma_OnClick" ToolTip="Imprimir guia do plano de saúde">
                            <asp:Label runat="server" ID="Label13" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <div id="divLoadShowNovoMedic" style="display: none; height: 370px !important;">
        <ul class="ulDados" style="width: 350px !important;">
            <li>
                <label for="txtNProduto" class="lblObrigatorio" title="Nome do Produto">
                    Nome do Produto</label>
                <asp:TextBox ID="txtNProduto" Width="300px" ToolTip="Informe o Nome do Produto" runat="server"
                    MaxLength="60"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvProduto" CssClass="validatorField"
                ErrorMessage="O nome do produto é obrigatório" ControlToValidate="txtNProduto" Display="Dynamic" />
            </li>
            <li style="clear: both">
                <label for="txtNReduz" class="lblObrigatorio" title="Nome Reduzido">
                    Nome Reduzido</label>
                <asp:TextBox ID="txtNReduz" Width="180px" ToolTip="Informe o Nome Reduzido" runat="server"
                    MaxLength="33"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvNReduz" CssClass="validatorField"
                ErrorMessage="O nome reduzido do produto é obrigatório" ControlToValidate="txtNReduz" Display="Dynamic" />
            </li>
            <li>
                <label for="txtCodRef" class="lblObrigatorio" title="Código de Referência">
                    Cód. Referência</label>
                <asp:TextBox ID="txtCodRef" Width="110px" ToolTip="Informe o Código de Referência"
                    runat="server" MaxLength="9"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvCodRef" CssClass="validatorField"
                ErrorMessage="O código referência do produto é obrigatório" ControlToValidate="txtCodRef" Display="Dynamic" />
            </li>
            <li style="clear: both">
                <label for="txtDescProduto" class="lblObrigatorio" title="Descrição do Produto">
                    Descrição do Produto</label>
                <asp:TextBox ID="txtDescProduto" Width="300px" Height="51px" TextMode="MultiLine"
                    ToolTip="Informe a Descrição do Produto" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvDescProduto" CssClass="validatorField"
                ErrorMessage="A descrição do produto é obrigatória" ControlToValidate="txtDescProduto" Display="Dynamic" />
            </li>
            <li style="clear: both;">
                <label class="lblObrigatorio">
                    Princípio Ativo</label>
                <asp:TextBox runat="server" ID="txtPrinAtiv" Width="300px" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvPrinAtiv" CssClass="validatorField"
                ErrorMessage="O princípio ativo do produto é obrigatório" ControlToValidate="txtPrinAtiv" Display="Dynamic" />
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
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvGrupo" CssClass="validatorField"
                ErrorMessage="O grupo do produto é obrigatório" ControlToValidate="ddlGrupo" Display="Dynamic" />
            </li>
            <li style="margin-top: 10px;">
                <label for="ddlSubGrupo" class="lblObrigatorio" title="SubGrupo de Produtos">
                    SubGrupo</label>
                <asp:DropDownList ID="ddlSubGrupo" Width="145px" ToolTip="Selecione o SubGrupo de Produtos"
                    runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvSubGrupo" CssClass="validatorField"
                ErrorMessage="O SubGrupo do produto é obrigatório" ControlToValidate="ddlSubGrupo" Display="Dynamic" />
            </li>
            <li style="clear: both">
                <label class="lblObrigatorio" title="Unidade do Produtos">
                    Unidade</label>
                <asp:DropDownList ID="ddlUnidade" CssClass="campoDescricao" ToolTip="Unidade do Produtos"
                    runat="server" Width="90px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoMedicamento" runat="server" ID="rfvUnidade" CssClass="validatorField"
                ErrorMessage="A unidade do produto é obrigatória" ControlToValidate="ddlUnidade" Display="Dynamic" />
            </li>
            <li id="li6" runat="server" class="liBtnAddA" style="margin-top: 0px !important;
                clear: both !important; height: 15px; margin-left: 140px !important;">
                <asp:LinkButton ID="lnkNovoMedic" runat="server" ValidationGroup="novoMedicamento" OnClick="lnkNovoMedic_OnClick"
                    ToolTip="Armazena as informações na prescrição em questão">
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
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rfvNoProcedimento" CssClass="validatorField"
                ErrorMessage="O nome do procedimento é obrigatório" ControlToValidate="txtNoProcedimento" Display="Dynamic" />
            </li>
            <li>
                <label title="Código do Procedimento de Saúde" class="lblObrigatorio">
                    Código Pro.</label>
                <asp:TextBox runat="server" ID="txtCodProcMedic" ToolTip="Código do Procedimento Médico"
                    Width="65px" CssClass="campoCodigo"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rfvCodProcMedic" CssClass="validatorField"
                ErrorMessage="O código do procedimento é obrigatório" ControlToValidate="txtCodProcMedic" Display="Dynamic" />
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
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rvfGrupo2" CssClass="validatorField"
                ErrorMessage="O grupo do procedimento é obrigatório" ControlToValidate="ddlGrupo2" Display="Dynamic" />
            </li>
            <li style="margin-top: -3px;">
                <label title="SubGrupo de Procedimentos de Saúde" class="lblObrigatorio">
                    SubGrupo</label>
                <asp:DropDownList runat="server" ID="ddlSubGrupo2" Width="153px" ToolTip="SubGrupo de Procedimentos de Saúde">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ValidationGroup="novoProcedimento" runat="server" ID="rfvSubGrupo2" CssClass="validatorField"
                ErrorMessage="O SubGrupo do procedimento é obrigatório" ControlToValidate="ddlSubGrupo2" Display="Dynamic" />
            </li>
            <li style="margin-top: -3px">
                <label title="Pesquise pelo Operadora de Planos de Saúde">
                    Operadora</label>
                <asp:DropDownList runat="server" ID="ddlOper" Width="140px" ToolTip="Pesquise pelo Operadora de Planos de Saúde">
                </asp:DropDownList>
            </li>
            <li style="margin-top: 10px">
                <label style="color:Blue;">VALORES</label>
                <ul style="margin-left: -5px;">
                    <li>
                        <label title="Valor de Custo do procedimento">
                            R$ Custo</label>
                        <asp:TextBox runat="server" ID="txtVlCusto" CssClass="campoDinheiro" Width="50px" ToolTip="Valor de Custo atual do procedimento"
                            Enabled="false" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor base do procedimento de saúde para cálculo">
                            R$ Base</label>
                        <asp:TextBox runat="server" ID="txtVlBase" Enabled="false"  Width="60px" CssClass="campoDinheiro"
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
                <label style="color:Blue;">RESTRIÇÕES</label>
                <ul style="margin-left: -5px;">
                    <li>
                        <label title="Quantidade de Seção Autorizadas Pelo plano de Saúde">
                            QSA</label>
                        <asp:TextBox runat="server" Width="35px" CssClass="campoQtd" ID="txtQtSecaoAutorizada" ToolTip="Quantidade de Seção Autorizadas pelo plano de Saúde"></asp:TextBox>
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
                <asp:TextBox runat="server" ID="txtObsProced" TextMode="MultiLine" Width="463px" Height="60px"
                    ToolTip="Observação do Procedimento de Saúde"></asp:TextBox>
            </li>
            <li id="li7" runat="server" class="liBtnAddA" style="margin-top: 0px !important;
                clear: both !important; height: 15px; margin-left: 230px !important;">
                <asp:LinkButton ID="lnkNovoExam" runat="server" ValidationGroup="novoProcedimento" OnClick="lnkNovoExam_OnClick"
                    ToolTip="Armazena as informações na prescrição em questão">
                    <asp:Label runat="server" ID="Label11" Text="SALVAR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
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
                <label title="Título do Laudo">Título do Relatório</label>
                <asp:TextBox ID="txtTituloLaudo" runat="server" Width="200px" Text="RELATÓRIO MÉDICO" />
            </li>
            <li>
                <label title="Data Relatório">Data </label>
                <asp:TextBox ID="txtDtLaudo" runat="server" class="campoData" ToolTip="Informe a data do relatório" />
            </li>
            <li style="clear: both;">
                <label title="Relatório Médico" style="color:Blue;">RELATÓRIO MÉDICO</label>
                <asp:TextBox ID="txtObsLaudo" Width="520px" Height="200px" TextMode="MultiLine" runat="server" />
            </li>
            <li class="liBtnAddA" style="clear: none !important; margin-left: 250px !important; margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirLaudo" ValidationGroup="laudo" runat="server" OnClick="lnkbImprimirLaudo_OnClick" ToolTip="Imprimir Relatório Médico">
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
                                    <asp:Label Text='<%# Eval("DE_OBSER_RES") %>' ToolTip='<%# Eval("DE_OBSER") %>' runat="server" />
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
            if (!($("#chkAlergiaMedic").attr("checked"))) {
                $("#txtAlergiaMedic").hide();
            }

            $("#flupAnexo").change(function () {
                if ($("#txtNomeAnexo").val() == "") {
                    var fileName = $("#flupAnexo").val();
                    while (fileName.indexOf("\\") != -1)
                        fileName = fileName.slice(fileName.indexOf("\\") + 1);
                    $("#txtNomeAnexo").val(fileName);
                }
            });

            $("#chkAlergiaMedic").click(function () {
                if ($("#chkAlergiaMedic").attr("checked")) {
                    $("#txtAlergiaMedic").show();
                }
                else {
                    $("#txtAlergiaMedic").hide();
                    $("#txtAlergiaMedic").val("");
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
            $('#divLaudo').dialog({ autoopen: false, modal: true, width: 555, height: 350, resizable: false, title: "RELATÓRIO MÉDICO",
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
        });

    </script>
</asp:Content>
