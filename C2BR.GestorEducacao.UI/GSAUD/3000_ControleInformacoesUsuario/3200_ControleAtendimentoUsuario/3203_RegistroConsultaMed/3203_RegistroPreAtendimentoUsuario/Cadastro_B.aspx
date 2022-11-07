<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro_B.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3203_RegistroPreAtendimentoUsuario.Cadastro_B" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 905px;
            margin-top: 45px !important;
        }
        .ulLeitura li
        {
            margin-top:-5px !important;
        }
        .ulRisco li
        {
            margin-top:1px !important;
        }
         input
        {
            height: 13px !important;
        }
        .ulDados li
        {
            margin-left: 10px;
            margin-top: -3px;
        }
        label
        {
            margin-bottom:1px;
        }
        .ulQuest li
        {
            margin-top: 7px;
        }
        .Aumen, .campoCpf, .campoTel, .campoData, .campoNis
        {
            
        }
        .lblsub
        {
            clear:both;
            color: #436EEE;
            margin-bottom:3px;
        }
        .lblTituGr
        {
            font-size: 12px;
        }
        .divResp
        {
            <%--border: 1px solid #CCCCCC;--%>;
            width: 430px;
            height: 110px;
            float: left;
            margin-left:-9px;
        }
        .divPaci
        {
            <%--border: 1px solid #CCCCCC;--%>;
            border-left:1px solid #CCCCCC;
            width: 475px;
            padding-left:4px;
            height: 110px;
            float: right;
            margin-bottom:-8px;
        }
        .divLeitura
        {
            border-right:1px solid #CCCCCC;
            width: 99px;
            height: 139px;
            float: left;
            margin-left:-9px
        }
         .divRegRisco
        {
            padding-left:2px;
            width: 810px;
            height: 130px;
            float: right;
        }
        #divRisco1
        {
            width:130px;
            height:128px;
            border-right:1px solid #CCCCCC;
        }
        #divRisco2
        {
            width:219px;
            height:128px;
            border-right:1px solid #CCCCCC;
        }
        #divMedicacao
        {
            width:397px;
            height:87px;
            border-right:1px solid #CCCCCC;     
            margin:30px 0 0 -100px;   
        }
        #divInfosPrev
        {
            margin:12px 0 0 2px;   
        }
        .divQuestion
        {
            <%--border: 1px solid #CCCCCC;--%>;
            margin-top:0px;
            width: 580px;
            height: 90px;
            float: right;
        }
        .divClassRisco
        {
            border-left: 1px solid #CCCCCC;
            width:185px;
            float:right;
            height:237px;
            margin-top:-237px;
            padding-left:13px;
        }
        .chkItens
        {
            margin-left:-5px;
        }
        .chkAreasChk
        {
            margin-left:-6px;
        }
        .liFotoColab { float: left !important; margin-right: 10px !important; }  
        .ddlSexo { width:45px; }
        .campoHora { width:30px; }
        .ddlDor { width:230px; }
        .liDores { margin-top:7px !important; }
        .liDores label
        {
            margin-bottom:2px;
        }
                .divPaciPreAtend
        {
            border: 1px solid #CCCCCC;
            width: 100%;
            height: 135px;
            overflow-y: scroll;
            margin-left: 0px;
            margin-bottom: 5px;
            margin-top: -10px;
        }
        .Cor1{background-color: Red;}
        .Cor2{background-color: Orange;}
        .Cor3{background-color: Yellow;}
        .Cor4{background-color: Green;}
        .Cor5{background-color: Blue;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
        </asp:Timer>
        <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px;
            margin-top: -24px; background-color: #ffff99; text-align: center; font-weight: bold;
            margin-bottom: 5px; vertical-align: middle; margin-left: 0px;">
            <ul>
                <li style="margin-left: 276px;">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                        GRADE DE PACIENTES COM PRÉ-ATENDIMENTO - DIRECIONAMENTO</label>
                </li>
                <li style="float: right; margin-top: 6px;">
                    <ul>
                        <li>
                            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
                            <asp:Label runat="server" ID="Label2"> &nbsp à &nbsp </asp:Label>
                            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
                        </li>
                        <li style="margin-top: -3px; margin-left: 5px;">
                            <asp:ImageButton ID="imgPesqGrid" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                OnClick="imgPesqGrid_OnClick" />
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger EventName="Tick" ControlID="Timer1" />
            </Triggers>
            <ContentTemplate>
                <div class="divPaciPreAtend">
                    <asp:GridView ID="grdEncamMedic" CssClass="grdBusca" runat="server" Style="width: 100%;
                        height: 15px;" AutoGenerateColumns="false" ToolTip="Grade de pacientes direcionados ainda sem Acolhimento"
                        OnRowDataBound="grdEncamMedic_OnRowDataBound" AutoGenerateSelectButton="false">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum Direcionamento Médico em aberto<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField Visible="false" DataField="ID_ENCAM_MEDIC" HeaderText="Cod." SortExpression="CO_EMP"
                                HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                <HeaderStyle CssClass="noprint"></HeaderStyle>
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Width="15px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' runat="server" />
                                    <asp:HiddenField ID="hidCoCol" Value='<%# Eval("CO_COL") %>' runat="server" />
                                    <asp:HiddenField ID="hidAntigos" Value='<%# Eval("ANTIGO") %>' runat="server" />
                                    <asp:HiddenField ID="hidIdEncam" Value='<%# Eval("ID_ENCAM_MEDIC") %>' runat="server" />
                                    <asp:HiddenField ID="hidCoEspec" Value='<%# Eval("CO_ESPEC") %>' runat="server" />
                                    <asp:HiddenField ID="hidCoTpRisco" Value='<%# Eval("CO_TIPO_RISCO") %>' runat="server" />
                                    <asp:HiddenField ID="hidCoResp" Value='<%# Eval("CO_RESP") %>' runat="server" />
                                    <asp:HiddenField ID="hidIdOper" Value='<%# Eval("ID_OPER") %>' runat="server" />
                                    <asp:HiddenField ID="hidIdPlan" Value='<%# Eval("ID_PLAN") %>' runat="server" />
                                    <asp:CheckBox ID="chkselectEn" runat="server" OnCheckedChanged="chkselectEn_OnCheckedChanged"
                                        AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NO_PAC" HeaderText="NOME DO PACIENTE">
                                <ItemStyle HorizontalAlign="Left" Width="210px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CO_SEXO" HeaderText="SX">
                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_IDADE" HeaderText="ID">
                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NM_OPER" HeaderText="OPERADORA">
                                <ItemStyle HorizontalAlign="Left" Width="60px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DTHREncamMed" HeaderText="DT/HR ENCAM.">
                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPECIALIDADE">
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_COL" HeaderText="PROF. SAÚDE">
                                <ItemStyle HorizontalAlign="Left" Width="210px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="CLAS. DE RISCO">
                                <ItemStyle Width="130px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <ul>
                                        <li style="margin-top: 0.5px;">
                                            <div id="Div1" runat="server" class='<%# Eval("classCor") %>' style="width: 10px;
                                                height: 10px; margin: 0px;" title="Representação gráfica da classificação de risco">
                                            </div>
                                        </li>
                                        <li style="margin: -1px 0 0 -2px; clear: none;">
                                            <asp:Label runat="server" ID="lblg1" Text='<%# Eval("CLASS_RISCO") %>'></asp:Label>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li style="margin-top: 5px !important; height: 17px; width: 100%; text-align: center;
            text-transform: uppercase; margin-top: 0px; margin-left: auto; background-color: #B4EEB4;
            margin-bottom: 6px;">
            <label style="font-size: 1.1em; font-family: Tahoma; margin-top: 2px;">
                Avaliação do Paciente</label>
        </li>
        <div class="divLeitura">
            <ul class="ulLeitura">
                <li class="lblsub" style="margin: -3px 0 5px 10px !important;">
                    <asp:Label runat="server" ID="Label4" class="lblTituGr">Leitura</asp:Label>
                </li>
                <li style="clear: both;">
                    <label>
                        Altura</label>
                    <asp:TextBox runat="server" ID="txtAltura" Width="30px" CssClass="campoAltu"></asp:TextBox>
                </li>
                <li style="margin-left: 4px">
                    <label>
                        Peso</label>
                    <asp:TextBox runat="server" ID="txtPeso" Width="30px" CssClass="campoPeso"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Pressão (Val/HR)</label>
                    <asp:TextBox runat="server" ID="txtPressArt" Width="30px" CssClass="campoPressArteri"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtHoraPressArt" Width="30" CssClass="campoHora"
                        Style="margin-left: 6px"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Temper (Val/HR)</label>
                    <asp:TextBox runat="server" ID="txtTemper" Width="30px" CssClass="campoGrau"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtHoraTemper" Width="30" CssClass="campoHora" Style="margin-left: 6px"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Glicem (Val/HR)</label>
                    <asp:TextBox runat="server" ID="txtGlicem" Width="30px" CssClass="campoGlicem"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtHoraGlicem" Width="30" CssClass="campoHora" Style="margin-left: 6px"></asp:TextBox>
                </li>
            </ul>
        </div>
        <div class="divRegRisco">
            <ul>
                <li class="lblsub">
                    <asp:Label runat="server" ID="Label5" class="lblTituGr">Registro de Risco</asp:Label>
                </li>
                <li id="divRisco1" style="margin-left: -0px !important; clear: both">
                    <ul class="ulRisco">
                        <li style="margin-top: -1px !important;">
                            <label>
                                Diabetes</label>
                            <asp:CheckBox runat="server" ID="chkDiabetes" class="chkItens" />
                            <asp:DropDownList runat="server" ID="ddlDiabetes" Width="60px" class="chkAreasChk">
                                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                <asp:ListItem Text="Tipo 1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Tipo 2" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both; margin-top: 6px !important;">
                            <label>
                                Hipertensão</label>
                            <asp:CheckBox runat="server" ID="chkHibert" class="chkItens" />
                            <asp:TextBox runat="server" ID="txtHiper" Width="86px" class="chkAreasChk" MaxLength="20">
                            </asp:TextBox>
                        </li>
                        <li style="clear: both; margin-top: -5px !important;">
                            <label>
                                Fumante (St/Anos)</label>
                            <asp:DropDownList runat="server" ID="ddlFumante" Width="80px">
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Ex-Fumante" Value="E"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="txtTempoFumante" Width="20px"></asp:TextBox>
                        </li>
                        <li style="clear: both; margin-top: -5px !important;">
                            <label>
                                Alcool (St/Anos)</label>
                            <asp:DropDownList runat="server" ID="ddlAlcool" Width="80px">
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="As Vezes" Value="V"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="txtTempoBebidas" Width="20px"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="margin: -1px 0 0 -1px;" id="divRisco2">
                    <ul>
                        <li>
                            <label>
                                Cirurgia</label>
                            <asp:CheckBox runat="server" ID="chkCiru" class="chkItens" />
                            <asp:TextBox runat="server" ID="txtCiru" Width="175px" class="chkAreasChk" MaxLength="50">
                            </asp:TextBox>
                        </li>
                        <li style="clear: both; margin-top: -4px; margin-bottom: -2px;">
                            <label>
                                Alergia</label>
                            <asp:CheckBox runat="server" ID="chkAlergia" class="chkItens" />
                            <asp:TextBox runat="server" ID="txtAlergia" Width="175px" class="chkAreasChk" MaxLength="40">
                            </asp:TextBox>
                        </li>
                        <li style="clear: both;">
                            <label>
                                Marcapasso</label>
                            <asp:CheckBox runat="server" ID="chkMarcPass" class="chkItens" />
                            <asp:TextBox runat="server" ID="txtMarcPass" Width="69px" class="chkAreasChk" MaxLength="20">
                            </asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Válvula</label>
                            <asp:CheckBox runat="server" ID="chkValvulas" class="chkItens" />
                            <asp:TextBox runat="server" ID="txtValvula" Width="69px" class="chkAreasChk" MaxLength="30">
                            </asp:TextBox>
                        </li>
                        <li style="clear: both">
                            <label>
                                Teve Enjôos?</label>
                            <asp:DropDownList runat="server" ID="ddlEnjoos" Width="50px">
                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: 4px">
                            <label>
                                Teve Vômitos?</label>
                            <asp:DropDownList runat="server" ID="ddlVomitos" Width="50px">
                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: 2px">
                            <label>
                                Teve Febre?</label>
                            <asp:DropDownList runat="server" ID="ddlFebre" Width="50px">
                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>
                <li style="margin: -1px 0 0 2px;">
                    <ul>
                        <li>
                            <label>
                                Dores?</label>
                            <asp:DropDownList runat="server" ID="ddlDores" Width="63px">
                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                <asp:ListItem Text="As Vezes" Value="A"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: 7px;">
                            <label>
                                Data</label>
                            <asp:TextBox runat="server" ID="txtDtDor" CssClass="campoData"></asp:TextBox>
                        </li>
                        <li style="margin-left: 2px;">
                            <label>
                                Hora</label>
                            <asp:TextBox runat="server" ID="txtHrDor" CssClass="campoHora"></asp:TextBox>
                        </li>
                        <li style="clear: both; margin-top: -4px !important;" class="liDores">
                            <label>
                                Classificação da Dor</label>
                            <asp:DropDownList runat="server" ID="ddlDor1" CssClass="ddlDor">
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both" class="liDores">
                            <asp:DropDownList runat="server" ID="ddlDor2" CssClass="ddlDor">
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both" class="liDores">
                            <asp:DropDownList runat="server" ID="ddlDor3" CssClass="ddlDor">
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both" class="liDores">
                            <asp:DropDownList runat="server" ID="ddlDor4" CssClass="ddlDor">
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
        <li id="divMedicacao">
            <ul>
                <li class="lblsub">
                    <asp:Label runat="server" ID="Label9" class="lblTituGr">Medicação</asp:Label>
                </li>
                <li style="clear: both">
                    <label title="Medicação de uso contínuo do(a) paciente">
                        Medicação de Uso Contínuo</label>
                    <asp:TextBox runat="server" ID="txtMedicContinuo" TextMode="MultiLine" Style="width: 177px;
                        height: 60px;" MaxLength="200" ToolTip="Medicação de uso contínuo do(a) paciente"></asp:TextBox>
                </li>
                <li>
                    <label title="Medicação administrada no acolhimento no(a) paciente">
                        Medicação
                    </label>
                    <asp:TextBox runat="server" ID="txtMedicacaoAdmin" TextMode="MultiLine" Style="width: 177px;
                        height: 60px;" MaxLength="200" ToolTip="Medicação administrada no acolhimento no(a) paciente"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li id="divInfosPrev">
            <ul>
                <li class="lblsub" style="margin-top: 15px;">
                    <asp:Label runat="server" ID="Label8" class="lblTituGr">Informações Prévias</asp:Label>
                </li>
                <li style="clear: both;">
                    <label>
                        Sintomas</label>
                    <asp:TextBox runat="server" ID="txtSintomas" TextMode="MultiLine" Style="width: 289px;
                        height: 60px;" MaxLength="200"></asp:TextBox><br />
                </li>
            </ul>
        </li>
        <div class="divQuestion">
            <div class="divClassRisco">
                <ul class="ulQuest">
                    <li style="width: 100%; height: 17px; text-align: center; text-transform: uppercase;
                        margin-top: 62px; margin-left: auto; background-color: #FFA07A; margin-bottom: 10px;">
                        <label style="font-size: 1.1em; font-family: Tahoma; margin-top: 1px; color: White;
                            font-weight: bold;">
                            resultado</label>
                    </li>
                    <li style="margin: 0 0 0 0;">
                        <label style="color: Red; font-size: 12px; font-weight: bold;" class="lblObrigatorio">
                            Classf. Risco</label>
                        <asp:DropDownList runat="server" ID="ddlClassRisco" Width="92px" ClientIDMode="Static">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:HiddenField runat="server" ID="hidDivAberta" ClientIDMode="Static" />
                        <div id="divClassRisc" title="Selecione a Classificação de risco pela cor" style="cursor: pointer;
                            margin: 8px 0 0 -6px; width: 83px; height: 9px; border: 1px solid #CCCCCC; position: absolute;
                            background-color: White; padding: 2px">
                            <div id="divClassRiscCorSelec" style="height: 9px; width: 100%">
                            </div>
                            <div id="divClass1" title="Emergência" style="display: none; height: 100%; width: 40px;
                                background-color: Red; cursor: pointer; float: left;">
                            </div>
                            <div id="divClass2" title="Muito Urgente" style="display: none; height: 100%; width: 40px;
                                background-color: Orange; cursor: pointer; float: left; margin-left: 5px;">
                            </div>
                            <div id="divClass3" title="Urgente" style="display: none; height: 100%; width: 40px;
                                background-color: Yellow; cursor: pointer; float: left; margin-left: 5px;">
                            </div>
                            <div id="divClass4" title="Pouco Urgente" style="display: none; height: 100%; width: 40px;
                                background-color: Green; cursor: pointer; float: left; margin-left: 5px;">
                            </div>
                            <div id="divClass5" title="Não Urgente" style="display: none; height: 100%; width: 40px;
                                background-color: Blue; cursor: pointer; float: left; margin-left: 5px;">
                            </div>
                            <div id="divFecha" title="Fechar paleta" style="display: none; float: right; margin-left: 5px;">
                                <a id="lnkClose" class="lnkClose" title="Fechar paleta" href="#">[x]</a>
                            </div>
                        </div>
                    </li>
                    <li style="margin-left: -0px;">
                        <label>
                            Especialidade</label>
                        <asp:DropDownList runat="server" ID="ddlEspec" Width="185px">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both; margin-left: 0px">
                        <label title="Observação do Acolhimento">
                            Observação Acolhimento</label>
                        <asp:TextBox runat="server" ID="txtObserPreAtend" TextMode="MultiLine" Style="width: 183px;
                            height: 60px;" MaxLength="200" ToolTip="Observação do Acolhimento"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </div>
        <div id="divLoadShowResponsaveis" style="display: none; height: 315px !important;" />
        <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".campoCpf").mask("999.999.999-99");
            $(".campoTel").mask("(99)9999-9999");
            $(".campoHora").mask("99:99");
            $(".campoAnos").mask("99");
            $(".campoPressArteri").mask("?99/99");
            //            $(".campoGrau").mask("99,9");
            $(".campoGlicem").mask("?9999");
            $(".campoNis").mask("?9999999");
            $(".campoAltu").mask("9,99");
            $(".campoPeso").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoGrau").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            $(".lnkPesResp").click(function () {
                $('#divLoadShowResponsaveis').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE RESPONSÁVEIS",
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });

            $(".lnkPesNIRE").click(function () {
                $('#divLoadShowAlunos').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });

            //Executado ao trocar a seleção no DropDownList para mudar a cor para a correspondente
            $("#ddlClassRisco").change(function (evento) {
                var e = document.getElementById("ddlClassRisco");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "") {
                    $("#divClassRiscCorSelec").hide()
                    $("#divClassRiscCorSelec").css("background-color", "White");
                    $("#divClassRiscCorSelec").show(100);
                }
                else if (itSelec == 1) {
                    $("#divClassRiscCorSelec").hide()
                    $("#divClassRiscCorSelec").css("background-color", "Red");
                    $("#divClassRiscCorSelec").show(100);
                }
                else if (itSelec == 2) {
                    $("#divClassRiscCorSelec").hide();
                    $("#divClassRiscCorSelec").css("background-color", "Orange");
                    $("#divClassRiscCorSelec").show(100);
                }
                else if (itSelec == 3) {
                    $("#divClassRiscCorSelec").hide();
                    $("#divClassRiscCorSelec").css("background-color", "Yellow");
                    $("#divClassRiscCorSelec").show(100);
                }
                else if (itSelec == 4) {
                    $("#divClassRiscCorSelec").hide();
                    $("#divClassRiscCorSelec").css("background-color", "Green");
                    $("#divClassRiscCorSelec").show(100);
                }
                else if (itSelec == 5) {
                    $("#divClassRiscCorSelec").hide();
                    $("#divClassRiscCorSelec").css("background-color", "Blue");
                    $("#divClassRiscCorSelec").show(100);
                }
            });

            //Executado quando se clica no box de cor de class risco, e abre o leque de opções disponíveis com animação
            $("#divClassRisc").click(function () {
                var sele = $("#hidDivAberta").val();
                $("#divClassRiscCorSelec").hide();
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

            //Executado quando é selecionada a classificação de risco de EMERGENCIA (VERMELHA)
            $("#divClass1").click(function () {
                $("#hidDivAberta").val("1");
                $("#divClass2").css("display", "none");
                $("#divClass1").css("display", "none");
                $("#divClass3").css("display", "none");
                $("#divClass4").css("display", "none");
                $("#divClass5").css("display", "none");

                //Esconde a imagem para fechar a janela
                $("#divFecha").css("display", "none");

                $("#divClassRisc").animate({
                    width: "83px",
                    margin: "8px 0 0 -6px",
                    height: "9px",
                    padding: "2px"
                }, 500, function () {
                    //seleciona a classificação de risco correspondente
                    $("#ddlClassRisco").val("1");

                    //Altera a div pai das classificações conforme necessário
                    $("#divClassRisc").css("cursor", "pointer");
                    $("#divClassRiscCorSelec").css("background-color", "Red");
                    $("#divClassRiscCorSelec").show(100);

                    //Garante que o método pai não vai ser chamado evitando um loop
                    $("#hidDivAberta").val("");
                });
            });

            //Executado quando é selecionada a classificação de risco de MUITO URGENTE (LARANJA)
            $("#divClass2").click(function () {
                $("#hidDivAberta").val("1");
                $("#divClass1").css("display", "none");
                $("#divClass2").css("display", "none");
                $("#divClass3").css("display", "none");
                $("#divClass4").css("display", "none");
                $("#divClass5").css("display", "none");
                //Esconde a imagem para fechar a janela
                $("#divFecha").css("display", "none");
                $("#divClassRisc").animate({
                    width: "83px",
                    margin: "8px 0 0 -6px",
                    height: "9px",
                    padding: "2px"
                }, 500, function () {
                    //seleciona a classificação de risco correspondente
                    $("#ddlClassRisco").val("2");

                    //Altera a div pai das classificações conforme necessário
                    $("#divClassRisc").css("cursor", "pointer");
                    $("#divClassRiscCorSelec").css("background-color", "Orange");
                    $("#divClassRiscCorSelec").show(100);

                    //Garante que o método pai não vai ser chamado evitando um loop
                    $("#hidDivAberta").val("");
                });
            });

            //Executado quando é selecionada a classificação de risco de URGENTE (AMARELA)
            $("#divClass3").click(function () {
                $("#hidDivAberta").val("1");
                $("#divClass1").css("display", "none");
                $("#divClass2").css("display", "none");
                $("#divClass3").css("display", "none");
                $("#divClass4").css("display", "none");
                $("#divClass5").css("display", "none");
                //Esconde a imagem para fechar a janela
                $("#divFecha").css("display", "none");
                $("#divClassRisc").animate({
                    width: "83px",
                    margin: "8px 0 0 -6px",
                    height: "9px",
                    padding: "2px"
                }, 500, function () {
                    //seleciona a classificação de risco correspondente
                    $("#ddlClassRisco").val("3");

                    //Altera a div pai das classificações conforme necessário
                    $("#divClassRisc").css("cursor", "pointer");
                    $("#divClassRiscCorSelec").css("background-color", "Yellow");
                    $("#divClassRiscCorSelec").show(100);

                    //Garante que o método pai não vai ser chamado evitando um loop
                    $("#hidDivAberta").val("");
                });
            });

            //Executado quando é selecionada a classificação de risco de POUCO URGENTE (VERDE)
            $("#divClass4").click(function () {
                $("#hidDivAberta").val("1");
                $("#divClass1").css("display", "none");
                $("#divClass2").css("display", "none");
                $("#divClass3").css("display", "none");
                $("#divClass4").css("display", "none");
                $("#divClass5").css("display", "none");
                //Esconde a imagem para fechar a janela
                $("#divFecha").css("display", "none");
                $("#divClassRisc").animate({
                    width: "83px",
                    margin: "8px 0 0 -6px",
                    height: "9px",
                    padding: "2px"
                }, 500, function () {
                    //seleciona a classificação de risco correspondente
                    $("#ddlClassRisco").val("4");

                    //Altera a div pai das classificações conforme necessário
                    $("#divClassRisc").css("cursor", "pointer");
                    $("#divClassRiscCorSelec").css("background-color", "Green");
                    $("#divClassRiscCorSelec").show(100);

                    //Garante que o método pai não vai ser chamado evitando um loop
                    $("#hidDivAberta").val("");
                });
            });

            //Executado quando é selecionada a classificação de risco de NÃO URGENTE (AZUL)
            $("#divClass5").click(function () {
                $("#hidDivAberta").val("1");
                $("#divClass1").css("display", "none");
                $("#divClass2").css("display", "none");
                $("#divClass3").css("display", "none");
                $("#divClass4").css("display", "none");
                $("#divClass5").css("display", "none");
                //Esconde a imagem para fechar a janela
                $("#divFecha").css("display", "none");

                $("#divClassRisc").animate({
                    width: "83px",
                    margin: "8px 0 0 -6px",
                    height: "9px",
                    padding: "2px"
                }, 500, function () {
                    //seleciona a classificação de risco correspondente
                    $("#ddlClassRisco").val("5");

                    //Altera a div pai das classificações conforme necessário
                    $("#divClassRisc").css("cursor", "pointer");
                    $("#divClassRiscCorSelec").css("background-color", "Blue");
                    $("#divClassRiscCorSelec").show(100);

                    //Garante que o método pai não vai ser chamado evitando um loop
                    $("#hidDivAberta").val("");
                });
            });

            //Executado quando se clica no botão para fechar a paleta de opções
            $("#lnkClose").click(function (event) {
                $("#hidDivAberta").val("1");
                $("#divClass1").css("display", "none");
                $("#divClass2").css("display", "none");
                $("#divClass3").css("display", "none");
                $("#divClass4").css("display", "none");
                $("#divClass5").css("display", "none");
                //Esconde a imagem para fechar a janela
                $("#divFecha").css("display", "none");
                $("#divClassRisc").animate({
                    width: "83px",
                    margin: "8px 0 0 -6px",
                    height: "9px",
                    padding: "2px"
                }, 500, function () {
                    //Altera a div pai das classificações conforme necessário
                    $("#divClassRisc").css("cursor", "pointer");
                    $("#divClassRiscCorSelec").show(100);

                    //Garante que o método pai não vai ser chamado evitando um loop
                    $("#hidDivAberta").val("");
                });
            });

        });
    </script>
</asp:Content>
