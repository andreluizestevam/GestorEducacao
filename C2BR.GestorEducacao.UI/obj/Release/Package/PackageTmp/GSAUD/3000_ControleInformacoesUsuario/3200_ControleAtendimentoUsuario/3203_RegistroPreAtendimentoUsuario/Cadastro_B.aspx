<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro_B.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3203_RegistroPreAtendimentoUsuario.Cadastro_B" %>

<%@ Register TagPrefix="myprocedimento" TagName="procedimento" Src="~/Componentes/CadastroProcedimento.ascx" %>
      
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">

    .total {
        width: 100%;        
    }
    
    .titulo {
        font-family: Calibri;
        color: #1A73E8;
        font-size: 20px;
        font-weight: bold;
    }

    .dvtitulo {
        width: 100%;
    }

    .dados {
        font-size: 15px;
    }
    .largurali {
        width:70px;
    }
    .divtexto {
        float:left;
        margin-left:5px;
    }
        #divEnvioSMSContent {
            margin: auto;
            width: 340px;
        }

        .ulDados {
            width: 905px;
            margin-top: 45px !important;
        }

        .ulLeitura li {
            margin-top: -5px !important;
        }

        .ulRisco li {
            margin-top: 1px !important;
        }

        input {
            height: 13px !important;
        }

        .ulDados li {
            margin-left: 10px;
            margin-top: -3px;
        }

        label {
            margin-bottom: 1px;
        }

        .ulQuest li {
            margin-top: 7px;
        }

        .Aumen, .campoCpf, .campoTel, .campoData, .campoNis {
        }

        .lblsub {
            clear: both;
            color: #436EEE;
            margin-bottom: 3px;
        }

        .lblTituGr {
            font-size: 12px;
        }

        .divResp {
            width: 430px;
            height: 110px;
            float: left;
            margin-left: -9px;
        }

        .divPaci {
            border-left: 1px solid #CCCCCC;
            width: 475px;
            padding-left: 4px;
            height: 110px;
            float: right;
            margin-bottom: -8px;
        }

        .divLeitura {
            border-right: 1px solid #CCCCCC;
            width: 99px;
            height: 139px;
            float: left;
            margin-left: -9px;
        }

        .divRegRisco {
            padding-left: 2px;
            width: 810px;
            height: 130px;
            float: right;
        }

        #divRisco1 {
            width: 130px;
            height: 128px;
            border-right: 1px solid #CCCCCC;
        }

        #divRisco2 {
            width: 219px;
            height: 128px;
            border-right: 1px solid #CCCCCC;
        }

        #divMedicacao {
            width: 397px;
            height: 87px;
            border-right: 1px solid #CCCCCC;
            margin: 47px 0 0 -100px;
        }

        #divInfosPrev {
            margin: 12px 0 0 2px;
        }

        .divQuestion {
            margin-top: 0px;
            width: 580px;
            height: 90px;
            float: right;
        }

        .divClassRisco {
            border-left: 1px solid #CCCCCC;
            width: 185px;
            float: right;
            height: 237px;
            margin-top: -263px;
            padding-left: 13px;
        }

        .chkItens {
            margin-left: -5px;
        }

        .chkAreasChk {
            margin-left: -6px;
        }

        .liFotoColab {
            float: left !important;
            margin-right: 10px !important;
        }

        .ddlSexo {
            width: 45px;
        }

        .campoHora {
            width: 30px;
        }

        .ddlDor {
            width: 230px;
        }

        .liDores {
            margin-top: 7px !important;
        }

            .liDores label {
                margin-bottom: 2px;
            }

        .divPaciPreAtend {
            border: 1px solid #CCCCCC;
            width: 100%;
            height: 135px;
            overflow-y: scroll;
            margin-left: 0px;
            margin-bottom: 5px;
            margin-top: -10px;
        }

        .Cor1 {
            background-color: Red;
        }

        .Cor2 {
            background-color: Orange;
        }

        .Cor3 {
            background-color: Yellow;
        }

        .Cor4 {
            background-color: Green;
        }

        .Cor5 {
            background-color: Blue;
        }

        .window {
            display: none;
            width: 470px;
            height: 550px;
            position: absolute;
            left: 0;
            top: 0;
            background: #fffff0;
            z-index: 9900;
            padding: 10px;
            border-radius: 10px;
            overflow: scroll;
        }
        .windowGestante {
            display: none;
            width: 800px;
            height: 550px;
            position: absolute;
            left: 85px;
            top: 65px;
            background: white;
            z-index: 9900;
            padding: 10px;
            border-radius: 10px;
            overflow: scroll;
        }

        #mascara {
            display: none;
            position: absolute;
            left: 0;
            top: 0;
            z-index: 9000;
            background-color: #000;
        }

        .fechar {
            display: block;
            text-align: right;
        }

        .btn {
            height: 15px;
            margin-top: 5px;
            background-color: #5858FA;
            margin-right: 5px;
            float: right;
            font: bold;
            color: white;
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <%--<asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
        </asp:Timer>--%>
        <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px; margin-top: -24px; background-color: #ffff99; text-align: center; font-weight: bold; margin-bottom: 5px; vertical-align: middle; margin-left: 0px;">
            <ul>
                <li style="margin-left: 0px; width: 70%;">
                    <asp:DropDownList runat="server" ID="ddlescolheunidade" Style="float: left; width: 30%; margin-top: 7px; margin-left: 10px;" AutoPostBack="True" OnSelectedIndexChanged="ddlescolheunidade_SelectedIndexChanged"></asp:DropDownList>
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 7px; float: left; margin-left: 70px;">
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
            <%--<Triggers>
                <asp:AsyncPostBackTrigger EventName="Tick" ControlID="Timer1" />
            </Triggers>--%>
            <ContentTemplate>
                <div class="divPaciPreAtend">
                    <asp:GridView ID="grdEncamMedic" CssClass="grdBusca" runat="server" Style="width: 100%; height: 15px;"
                        AutoGenerateColumns="false" ToolTip="Grade de pacientes direcionados ainda sem Acolhimento"
                        OnRowDataBound="grdEncamMedic_OnRowDataBound" AutoGenerateSelectButton="false" OnRowCommand="grdEncamMedic_RowCommand">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum Direcionamento Médico em aberto<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField Visible="false" DataField="ID_AGEND_HORA" HeaderText="Cod." SortExpression="CO_EMP"
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
                                    <asp:HiddenField ID="hidIdEncam" Value='<%# Eval("ID_AGEND_HORA") %>' runat="server" />
                                    <asp:HiddenField ID="hidCoEspec" Value='<%# Eval("CO_ESPEC") %>' runat="server" />
                                    <asp:HiddenField ID="hidCoTpRisco" Value='<%# Eval("CO_TIPO_RISCO") %>' runat="server" />
                                    <asp:HiddenField ID="hidCoResp" Value='<%# Eval("CO_RESP") %>' runat="server" />
                                    <asp:HiddenField ID="hidIdOper" Value='<%# Eval("ID_OPER") %>' runat="server" />
                                    <asp:HiddenField ID="hidIdPlan" Value='<%# Eval("ID_PLAN") %>' runat="server" />
                                    <asp:CheckBox ID="chkselectEn" runat="server" OnCheckedChanged="chkselectEn_OnCheckedChanged"
                                        AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DTHREncamMed" HeaderText="DT/HR ENCAM.">
                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TP_AGEND_HORA" HeaderText="TIPO">
                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="NO_PAC" HeaderText="NOME DO PACIENTE">
                                <ItemStyle HorizontalAlign="Left" Width="210px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CO_SEXO" HeaderText="SX">

                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_IDADE" HeaderText="IDADE">
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NM_OPER_FORMATADO" HeaderText="CARTÃO SUS">
                                <ItemStyle HorizontalAlign="Left" Width="115px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPECIALIDADE">
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="NO_COL" HeaderText="PROF. SAÚDE">
                                <ItemStyle HorizontalAlign="Left" Width="210px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="CLAS. DE RISCO">
                                <ItemStyle Width="115px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <ul>
                                        <li style="margin-top: 0.5px;">
                                            <div id="Div1" runat="server" class='<%# Eval("classCor") %>' style="width: 10px; height: 10px; margin: 0px;"
                                                title="Representação gráfica da classificação de risco">
                                            </div>
                                        </li>
                                        <li style="margin: -1px 0 0 -2px; clear: none;">
                                            <asp:Label runat="server" ID="lblg1" ToolTip='<%# Eval("__FICHA") %>' Text='<%# Eval("CLASS_RISCO") %>'></asp:Label>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CO_ALU" HeaderText="código" Visible="true"></asp:BoundField>

                            <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li style="margin-top: 5px !important; height: 17px; width: 100%; text-align: center; text-transform: uppercase; margin-top: 0px; margin-left: auto; background-color: #B4EEB4; margin-bottom: 6px;">

            <asp:Button runat="server" ID="btn_SIGTAP" Text="PROCEDIMENTOS" Style="background-color: #ffff99; margin-top: 0px; font-size: 13px; font-weight: normal; margin-left: 15PX; border-width: 0px; height: 17px !important;" OnClick="btn_SIGTAP_Click" ToolTip="Pesquisar códigos procedimento" />
            <%--<a href="#janela1" rel="modal" style="background-color: #ffff99; margin-top: 4px; font-size: 13px; font-weight: bold; margin-left: 15PX;">SIGTAP  </a>--%>

            <asp:Button runat="server" ID="btn_GESTANTE" Text="GESTANTE" Style="background-color: yellow; margin-top: 0px; font-size: 13px; font-weight: normal; margin-left: 16PX; border-width: 0px; height: 17px !important;" OnClick="btn_GESTANTE_Click" ToolTip="Inserir dados da Gestante" />
            <%--<a href="#janela2" rel="modal" style="background-color: yellow; margin-top: 4px; font-size: 13px; font-weight: bold; margin-left: 15PX;">GESTANTE  </a>--%>


            <label style="font-size: 1.1em; font-family: Tahoma; margin-top: 2px; float: left; margin-left: 40%;">Avaliação do Paciente</label>
        </li>
        <div class="divLeitura">
            <ul class="ulLeitura">
                <li class="lblsub" style="margin: -3px 0 5px 10px !important;">
                    <asp:Label runat="server" ID="Label4" class="lblTituGr">Leitura</asp:Label>
                </li>
                <li style="clear: both;">
                    <label>
                        Altura</label>
                    <asp:TextBox runat="server" ID="txtAltura" Width="30px" CssClass="campoAltu" Value='<%# Eval("45")%>' OnTextChanged="txtAltura_TextChanged"> </asp:TextBox>
                </li>
                <li style="margin-left: 4px">
                    <label>
                        Peso</label>
                    <asp:TextBox runat="server" ID="txtPeso" Width="30px" CssClass="campoPeso" OnTextChanged="txtPeso_TextChanged"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Pressão (Val/HR)</label>
                    <asp:TextBox runat="server" ID="txtPressArt" Width="30px" CssClass="campoPressArteri" OnTextChanged="txtPressArt_TextChanged"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtGlicem" Width="30px" CssClass="campoGlicem" OnTextChanged="txtGlicem_TextChanged"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtHoraGlicem" Width="30" CssClass="campoHora" Style="margin-left: 6px"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Leitura da Glicemia</label>
                    <asp:DropDownList runat="server" ID="ddlglicemia" Style="width: 88px;" OnSelectedIndexChanged="ddlglicemia_SelectedIndexChanged">
                        <asp:ListItem Text="(NE) Não especificado" Value="N" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="(EJ) Em Jejum" Value="E"></asp:ListItem>
                        <asp:ListItem Text="(PR) Pré-Prandia" Value="P"></asp:ListItem>
                        <asp:ListItem Text="(PO) Pós-Prandial" Value="R"></asp:ListItem>
                    </asp:DropDownList>

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
                                Batimentos (Val/HR)</label>
                            <asp:TextBox runat="server" ID="tbbatimento" Width="30px" CssClass="campoGlicem" OnTextChanged="tbbatimento_TextChanged"></asp:TextBox>
                            <asp:TextBox runat="server" ID="tbvalorbatimento" Width="30" CssClass="campoHora" Style="margin-left: 6px"></asp:TextBox>
                        </li>

                        <li>
                            <label>
                                Saturação (Val/HR)</label>
                            <asp:TextBox runat="server" ID="tbsaturacao" Width="30px" CssClass="campoGlicem" OnTextChanged="tbsaturacao_TextChanged"></asp:TextBox>
                            <asp:TextBox runat="server" ID="tbvalorsaturacao" Width="30" CssClass="campoHora" Style="margin-left: 6px"></asp:TextBox>
                        </li>

                        <li>
                            <label>
                                Diabetes</label>
                            <asp:CheckBox runat="server" ID="chkDiabetes" class="chkItens" />
                            <asp:DropDownList runat="server" ID="ddlDiabetes" Width="60px" class="chkAreasChk">
                                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                <asp:ListItem Text="Tipo 1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Tipo 2" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <<li style="clear: both; margin-top: 3px !important;">
                            <label>
                                Hipertensão</label>
                            <asp:CheckBox runat="server" ID="chkHibert" class="chkItens" />
                            <asp:TextBox runat="server" ID="txtHiper" Width="86px" class="chkAreasChk" MaxLength="20"> </asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="margin: -1px 0 0 -1px;" id="divRisco2">
                    <ul>
                        <li style="clear: both; margin-top: -13px; float: left; !important;">
                            <label>
                                Fumante (St/Anos)</label>
                            <asp:DropDownList runat="server" ID="ddlFumante" Width="57px">
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Ex-Fumante" Value="E"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="txtTempoFumante" Width="20px"></asp:TextBox>
                        </li>
                        <li style="margin-top: -13px !important;">
                            <label>
                                Alcool (St/Anos)</label>
                            <asp:DropDownList runat="server" ID="ddlAlcool" Width="57px">
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="As Vezes" Value="V"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox runat="server" ID="txtTempoBebidas" Width="20px"></asp:TextBox>
                        </li>

                        <li style="margin-top: -7px;">
                            <label>
                                Cirurgia</label>
                            <asp:CheckBox runat="server" ID="chkCiru" class="chkItens" />
                            <asp:TextBox runat="server" ID="txtCiru" Width="175px" class="chkAreasChk" MaxLength="50" Text='<%# Eval("CLASS_RISCO") %>'> </asp:TextBox>
                        </li>
                        <li style="margin-top: -7px; margin-bottom: -2px;">
                            <label>
                                Alergia</label>
                            <asp:CheckBox runat="server" ID="chkAlergia" class="chkItens" />
                            <asp:TextBox runat="server" ID="txtAlergia" Width="175px" class="chkAreasChk" MaxLength="40"> </asp:TextBox>
                        </li>
                        <li style="margin-top: -7px;">
                            <label>
                                Marcapasso</label>
                            <asp:CheckBox runat="server" ID="chkMarcPass" class="chkItens" />
                            <asp:TextBox runat="server" ID="txtMarcPass" Width="69px" class="chkAreasChk" MaxLength="20"> </asp:TextBox>
                        </li>
                        <li style="margin-top: -7px;">
                            <label>
                                Válvula</label>
                            <asp:CheckBox runat="server" ID="chkValvulas" class="chkItens" />
                            <asp:TextBox runat="server" ID="txtValvula" Width="69px" class="chkAreasChk" MaxLength="30"> </asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="margin: -1px 0 0 2px;">
                    <ul>
                        <li>
                            <label>
                                Teve Febre?</label>
                            <asp:DropDownList runat="server" ID="ddlFebre" Width="50px">
                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                            </asp:DropDownList>
                        </li>

                        <li>
                            <label>
                                Teve Enjôos?</label>
                            <asp:DropDownList runat="server" ID="ddlEnjoos" Width="50px">
                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="float: left">
                            <label>
                                Teve Vômitos?</label>
                            <asp:DropDownList runat="server" ID="ddlVomitos" Width="50px">
                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both; margin-top: 2px;">
                            <label>
                                Dores?</label>
                            <asp:DropDownList runat="server" ID="ddlDores" Width="63px">
                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                <asp:ListItem Text="As Vezes" Value="A"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: 7px; margin-top: 2px;">
                            <label>
                                Data</label>
                            <asp:TextBox runat="server" ID="txtDtDor" CssClass="campoData"></asp:TextBox>
                        </li>
                        <li style="margin-left: 2px; margin-top: 2px;">
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
                    <asp:TextBox runat="server" ID="txtMedicContinuo" TextMode="MultiLine" Style="width: 177px; height: 47px;"
                        MaxLength="200" ToolTip="Medicação de uso contínuo do(a) paciente"></asp:TextBox>
                </li>
                <li>
                    <label title="Medicação administrada no acolhimento no(a) paciente">
                        Medicação
                    </label>
                    <asp:TextBox runat="server" ID="txtMedicacaoAdmin" TextMode="MultiLine" Style="width: 177px; height: 47px;"
                        MaxLength="200" ToolTip="Medicação administrada no acolhimento no(a) paciente"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li id="divInfosPrev">
            <ul>
                <li class="lblsub" style="margin-top: 34px;">
                    <asp:Label runat="server" ID="Label8" class="lblTituGr">Informações Prévias</asp:Label>
                </li>
                <li style="clear: both;">
                    <label>
                        Sintomas</label>
                    <asp:TextBox runat="server" ID="txtSintomas" TextMode="MultiLine" Style="width: 289px; height: 45px;"
                        MaxLength="200"></asp:TextBox><br />
                </li>
            </ul>
        </li>
        <div class="divQuestion">
            <div class="divClassRisco">
                <ul class="ulQuest">
                    <li style="margin-left: -0px;">
                        <label>
                            Profissional Responsável Triagem</label>
                        <asp:DropDownList runat="server" ID="ddlProfResp" Width="185px">
                        </asp:DropDownList>
                    </li>
                    <li style="margin-left: -0px;">
                        <label>
                            Encaminhamento de paciente</label>
                        <asp:DropDownList runat="server" ID="ddlEncamP" Width="185px">
                        </asp:DropDownList>
                    </li>

                    <li style="margin-left: -0px;">
                        <label>
                            Profissional Atendimento</label>
                        <asp:DropDownList runat="server" ID="ddlprofatendimento" Width="185px">
                        </asp:DropDownList>
                    </li>

                    <li style="width: 100%; height: 17px; text-align: center; text-transform: uppercase; margin-left: auto; background-color: #FFA07A; margin-bottom: 10px;">
                        <label style="font-size: 1.1em; font-family: Tahoma; margin-top: 1px; color: White; font-weight: bold;">
                            resultado</label>
                    </li>
                    <li style="margin: -4px 0 0 0;">
                        <label style="color: Red; font-size: 12px; font-weight: bold;" class="lblObrigatorio">
                            Classf. Risco</label>
                        <asp:DropDownList runat="server" ID="ddlClassRisco" Width="92px" ClientIDMode="Static">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:HiddenField runat="server" ID="hidDivAberta" ClientIDMode="Static" />
                        <div id="divClassRisc" title="Selecione a Classificação de risco pela cor" style="cursor: pointer; margin: 3px 0 0 -6px; width: 83px; height: 9px; border: 1px solid #CCCCCC; position: absolute; background-color: White; padding: 2px">
                            <div id="divClassRiscCorSelec" style="height: 9px; width: 100%">
                            </div>
                            <div id="divClass1" title="Emergência" style="display: none; height: 100%; width: 40px; background-color: Red; cursor: pointer; float: left;">
                            </div>
                            <div id="divClass2" title="Muito Urgente" style="display: none; height: 100%; width: 40px; background-color: Orange; cursor: pointer; float: left; margin-left: 5px;">
                            </div>
                            <div id="divClass3" title="Urgente" style="display: none; height: 100%; width: 40px; background-color: Yellow; cursor: pointer; float: left; margin-left: 5px;">
                            </div>
                            <div id="divClass4" title="Pouco Urgente" style="display: none; height: 100%; width: 40px; background-color: Green; cursor: pointer; float: left; margin-left: 5px;">
                            </div>
                            <div id="divClass5" title="Não Urgente" style="display: none; height: 100%; width: 40px; background-color: Blue; cursor: pointer; float: left; margin-left: 5px;">
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
                        <asp:TextBox runat="server" ID="txtObserPreAtend" TextMode="MultiLine" Style="width: 183px; height: 26px;"
                            MaxLength="200" ToolTip="Observação do Acolhimento"></asp:TextBox>
                        <br>
                        <br>
                    </li>
                </ul>
            </div>
        </div>
        <div id="divLoadShowResponsaveis" style="display: none; height: 315px !important;" />
        <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />

    </ul>
    <!-- INICIO ------------------------------------------------------------------------------------------------------------------------------------- DIV MODAL PARA ESCOLHER OS SIGTAP -->
    <%--<div class="window" id="janela1">--%>
    <div id="divLoadInfosSigtap" style="display: none; height: 435px !important;">
        <myprocedimento:procedimento ID="procedimento" runat="server" />

       <%-- <asp:Label runat="server" ID="lblpesquisasigtab" Text="  Pesquisa  "></asp:Label>
        <asp:TextBox runat="server" ID="tbpesquisasigtab" Style="width: 530px;"></asp:TextBox>
        <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgCpfResp_Click" />
        <br />
        <asp:GridView runat="server" ID="grdListarSIGTAP" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdListarSIGTAP_PageIndexChanging" CssClass="grdBusca">
            <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
            <EmptyDataTemplate>
                Nenhum Paciente Encontrado<br />
            </EmptyDataTemplate>
            <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" CssClass="headerStyleLA" />
            <AlternatingRowStyle CssClass="alternateRowStyleLA" Height="15" />
            <RowStyle CssClass="rowStyle" Height="15" />
            <AlternatingRowStyle CssClass="alternatingRowStyle" />
            <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
            <PagerStyle CssClass="grdFooter" />
            <Columns>

                <asp:TemplateField>
                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkselectEn" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Cód. SIGTAP" DataField="CO_PROC_MEDI">
                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Procedimento" DataField="NM_PROC_MEDI">
                    <ItemStyle Width="500px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <br />
        <div>
            <center>
                <asp:Button runat="server" CssClass="btn" ID="btnclose" Text="  Fechar  " Style="height: 30px  !important; width: 80px !important;" />
                <asp:Button runat="server" CssClass="btn" ID="btnincluir" Text=" Inserir CÓDIGO no atendimento " OnClick="btnincluir_Click" Style="height: 31px !important; width: 160px !important;" />
            </center>
        </div>
        <br />
        <div id="divHelpTxtLA">
            <p id="pAcesso" class="pAcesso">
                Verificar acima os códigos que deseja associar ao atendimento deste usuário.
            </p>
        </div>--%>
    </div>
    <%--<div class="windowGestante" id="janela2" style="top: 65px; left: 85px; !important;">--%>
    <div id="divLoadInfosGestante" style="display: none; height: 400px !important; left: 15px !important;">
        <ul class="ulDados" style="width: 400px !important; margin-top: 7px !important;">
            <div class="DivResp" runat="server" id="divResp">
                <ul class="ulDadosResp" style="margin-left: -177px !important; width: 746px !important;">

                    <li>
                        <asp:Label runat="server" ID="titulo" Text="DADOS GESTACIONAIS DO(A) PACIENTE" Font-Bold="true"></asp:Label>
                    </li>
                    <br /><br />

                    <li>DUM<br />
                        <asp:TextBox runat="server" ID="tbdum"></asp:TextBox>
                    </li>

                    <li>Observações DUM<br />
                        <asp:TextBox runat="server" ID="tbobsdum" Width="430px"></asp:TextBox>
                    </li>

                    <li>DPP<br />
                        <asp:TextBox runat="server" ID="tbdpp"></asp:TextBox>
                    </li>
                    <li style="clear: both"></li>

                    <li>
                        <br /><asp:Label runat="server" ID="Label1" Text="ESCUTA TRIAGEM - SINAIS VITAIS DA PACIENTE" Font-Bold="true"></asp:Label><br /><br />
                    </li>
                    <li style="clear: both"></li>
                    

                    <li>Altura<br />
                        <asp:TextBox runat="server" ID="tbaltura" style="width:40px"></asp:TextBox>
                    </li>

                    <li>Peso (Kg)<br />
                        <asp:TextBox runat="server" ID="tbpeso" style="width:45px"></asp:TextBox>
                    </li>

                    <li>IMC<br />
                        <asp:TextBox runat="server" ID="tbimc" style="width:40px"></asp:TextBox>
                    </li>

                    <li>PA<br />
                        <asp:TextBox runat="server" ID="tbpa" style="width:40px"></asp:TextBox>
                    </li>

                    <li>BC(bpm)<br />
                        <asp:TextBox runat="server" ID="tbbcbpm" style="width:50px"></asp:TextBox>
                    </li>

                    <li>Saturação<br />
                        <asp:TextBox runat="server" ID="TextBox1" CssClass="largurali"></asp:TextBox>
                    </li>

                    <li>Glicemia<br />
                        <asp:TextBox runat="server" ID="tbglicemia" CssClass="largurali"></asp:TextBox>
                    </li>

                    <li>Leitura Glicemia<br />
                        <asp:DropDownList runat="server" ID="ddlleitura" Width="255px" Height="16px"></asp:DropDownList>
                    </li>
                    <li style="clear: both"></li>

                    <li style="width:100%;">
                        <br /><asp:Label runat="server" ID="Label3" Text="REGISTRO PRÉ-NATAL" Font-Bold="true" ></asp:Label><br /><br />
                    </li>

                    <li>edma<br />
                        <asp:DropDownList runat="server" ID="ddledma" Style="width: 68px;"></asp:DropDownList>
                    </li>

                    <li>AU (cm)<br />
                        <asp:TextBox runat="server" ID="tbau" CssClass="largurali"></asp:TextBox>
                    </li>

                    <li>BCF (bpm)<br />
                        <asp:TextBox runat="server" ID="tbbcf" CssClass="largurali"></asp:TextBox>
                    </li>

                    <li>MF<br />
                        <asp:TextBox runat="server" ID="tbmf" CssClass="largurali"></asp:TextBox>
                    </li>

                    <li>Observação MF<br />
                        <asp:TextBox runat="server" ID="tbobsmf" Width="385px"></asp:TextBox>
                    </li>

                    <li style="clear: both"></li>

                     <li style="width:100%;">
                        <br /><asp:Label runat="server" ID="Label6" Text="REGISTRO ANTROPOMETRIA" Font-Bold="true" ></asp:Label><br /><br />
                    </li>
                    <br /><br />

                    <li>PC (cm)<br />
                        <asp:TextBox runat="server" ID="tbpc" CssClass="largurali"></asp:TextBox>
                    </li>

                    <li>Peso (Kg)<br />
                        <asp:TextBox runat="server" ID="tbpesoantropometria" CssClass="largurali"></asp:TextBox>
                    </li>

                    <li>Altura (cm)<br />
                        <asp:TextBox runat="server" ID="tbautura" CssClass="largurali"></asp:TextBox>
                    </li>

                    <li>PP (cm)<br />
                        <asp:TextBox runat="server" ID="tbpp" CssClass="largurali"></asp:TextBox>
                    </li>

                    <li>IMC<br />
                        <asp:TextBox runat="server" ID="TextBox2" CssClass="largurali"></asp:TextBox>
                    </li>

                    <li>Observação Antropometria<br />
                        <asp:TextBox runat="server" ID="tbobsantropometria" Width="294px"></asp:TextBox>
                    </li>

                    <li style="clear: both"></li>
                    <li style="width:100%;">
                       <br /> <asp:Label runat="server" ID="Label7" Text="REGISTRO DE PROBLEMAS E CONDIÇÕES ATIVAS" Font-Bold="true"></asp:Label><br /><br />
                    </li>
                    <br /><br />
                    <li>Tipo Registro<br />
                        <asp:DropDownList ID="ddltiporegistro" runat="server" Width="114px"></asp:DropDownList>
                    </li>

                    <li>Dados do Registro<br />
                        <asp:TextBox runat="server" ID="tbdataregistro" CssClass="largurali" Style="width: 90px; !important;"></asp:TextBox>
                    </li>

                    <li>Idade da Gestante<br />
                        <asp:TextBox runat="server" ID="tbidadegestante" CssClass="largurali" Style="width: 83px; !important;"></asp:TextBox>
                    </li>

                    <li>Código<br />
                        <asp:DropDownList runat="server" ID="ddlcodigo" Style="width: 52px; !important;"></asp:DropDownList>
                    </li>

                    <li class="divtexto">Descrição Complemento<br />
                        <asp:TextBox runat="server" ID="tbobservacaocomplemento" Width="326px"></asp:TextBox>
                    </li>
                    <li style="clear: both"></li>
                </ul>


                
                <div id="botoes" style="height: 0px;margin-left: 20px;">
                    <br /><br /><br /><br />
                    <div >
                        <asp:Button runat="server" ID="btnhistorico" Text="HISTÓRICO DE MEDIÇÕES" Style="background-color: #a7c9d5; border-style: none; float: left; font-family: Trebuchet MS; background-color: #a7c9d5;border-style: none;float: left;font-family: Trebuchet MS;margin-left: -166px;height: 31px !important;width: 142px;"/>
                    </div>
                    <div style="width: 160px; float: left;">
                        <asp:Button runat="server" ID="btnproblemas" Text="PROBLEMAS E CONDIÇÕES" Style="background-color: #a7c9d5; border-style: none; float: left; font-family: Trebuchet MS; height: 31px !important;width: 130px !important; "/>
                    </div>
                    <div style="width: 200px; float: left;">
                        <asp:Button runat="server" ID="btnresultados" Text="RESULTADO DE EXAMES" Style="background-color: #a7c9d5;border-style: none;float: left;font-family: Trebuchet MS;margin-left: 23px;width: 130px !important;height: 31px !important;" />
                    </div>

                    <div style="width: 20px; float: left;">
                        <asp:Button runat="server" ID="Button1" Text="SALVAR" Style="background-color: #ffd700;border-style: none;float: left;font-family: Trebuchet MS;font-weight: bold;height: 30px !important;width: 165px !important;" OnClick="Button1_Click" />
                    </div>

                </div>
            </div>
    </div>

    <!-- FIM -------------------------------------------------------------------------------------------------------------------------------------DIV MODAL PARA ESCOLHER OS SIGTAP -->
    <div id="DivSIGTAP" style="display: none; height: 305px !important;" />

    <script type="text/javascript">
        function AbreModalInfosGestante() {
            $('#divLoadInfosGestante').dialog({ autoopen: false, modal: true, width: 810, height: 420, resizable: false, title: "GESTANTE - CADASTRO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }




        function AbreModalInfosSigtap() {
            $('#divLoadInfosSigtap').dialog({
                autoopen: false, modal: true, width: 652, height: 350, resizable: false, title: "CÓDIGO FATURAMENTO - PESQUISA",
                open: function () { $('#divLoadInfosCadas').load("/Componentes/CadastroProcedimento.aspx"); }
               // open: function (type, data) { $(this).parent().appendTo("form"); },
                //close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

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
                $('#divLoadShowResponsaveis').dialog({
                    autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE RESPONSÁVEIS",
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });

            $(".lnkPesNIRE").click(function () {
                $('#divLoadShowAlunos').dialog({
                    autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });

            $(".lnkSIGTAP").click(function () {
                $('#DivSIGTAP').dialog({
                    autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA SIGTAP",
                    open: function () { $('#DivSIGTAP').load("/Componentes/ListaSIGTAB.aspx"); }
                });
            });
            $(".lnkGestante").click(function () {
                $('#DivGESTANTE').dialog({
                    autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "INFORMAÇÕES DE GESTANTE",
                    open: function () { $('#DivGESTANTE').load("/Componentes/Gestante.ascx"); }
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
        $(document).ready(function () {
            $("a[rel=modal]").click(function (ev) {
                ev.preventDefault();

                var id = $(this).attr("href");

                var alturaTela = $(document).height();
                var larguraTela = $(window).width();

                //colocando o fundo preto
                $('#mascara').css({ 'width': larguraTela, 'height': alturaTela });
                $('#mascara').fadeIn(1000);
                $('#mascara').fadeTo("slow", 0.8);

                var left = ($(window).width() / 2) - ($(id).width() / 2);
                var top = ($(window).height() / 2) - ($(id).height() / 2);

                $(id).css({ 'top': top, 'left': left });
                $(id).show();
            });

            $("#mascara").click(function () {
                $(this).hide();
                $(".window").hide();
            });

            $('.fechar').click(function (ev) {
                ev.preventDefault();
                $("#mascara").hide();
                $(".window").hide();
            });
        });
    </script>
    </div>
</asp:Content>
