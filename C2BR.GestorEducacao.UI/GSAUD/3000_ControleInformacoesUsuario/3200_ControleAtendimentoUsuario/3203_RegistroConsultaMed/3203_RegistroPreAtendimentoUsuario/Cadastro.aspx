<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3203_RegistroPreAtendimentoUsuario.Cadastro" %>

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
        .rblEncaminhar
        {
             list-style:none; margin-left: -7px; margin-top:-12px; padding: 0;  
        }
        .rblEncaminhar.horizontal li { display: inline;}
        .rblEncaminhar label{ display:inline;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li style="width: 100%; height: 17px; text-align: center; text-transform: uppercase;
            margin-top: -20px; margin-left: auto; background-color: #40E0D0; margin-bottom: 10px;">
            <label style="font-size: 1.1em; font-family: Tahoma; margin-top: 1px;">
                DADOS DO RESPONSÁVEL E DO USUÁRIO DE SAÚDE</label>
        </li>
        <div class="divResp">
            <ul>
                <li class="lblsub">
                    <asp:Label runat="server" ID="lblResp" class="lblTituGr">Responsável</asp:Label>
                    <asp:HiddenField runat="server" ID="hidCoResp" />
                </li>
                <li style="margin: 8px -5px 0 8px; clear: both"><a class="lnkPesResp" href="#">
                    <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                        style="width: 17px; height: 17px;" /></a> </li>
                <li style="width: 75px;">
                    <label class="lblObrigatorio">
                        CPF</label>
                    <asp:TextBox runat="server" ID="txtCPFResp" Width="75px" CssClass="campoCpf" class="Aumen"
                        OnTextChanged="txtCPFResp_OnTextChanged"></asp:TextBox>
                </li>
                <li style="margin-top: 10px; margin-left: 0px;">
                    <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                        OnClick="imgCpfResp_OnClick" />
                </li>
                <li style="width: 280px;">
                    <label class="lblObrigatorio">
                        Nome</label>
                    <asp:TextBox runat="server" ID="txtNomeResp" Width="280px" class="Aumen"></asp:TextBox>
                </li>
                <li>
                    <label class="lblObrigatorio">
                        Sexo</label>
                    <asp:DropDownList runat="server" ID="ddlSexoResp" CssClass="ddlSexo">
                        <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                        <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                        <asp:ListItem Text="Outros" Value="O"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <label class="lblObrigatorio">
                        Nascimento</label>
                    <asp:TextBox runat="server" ID="txtDtNascResp" CssClass="campoData" class="Aumen"></asp:TextBox>
                </li>
                <li style="width: 70px;">
                    <label class="lblObrigatorio">
                        Tel Celular</label>
                    <asp:TextBox runat="server" ID="txtTelCelResp" CssClass="campoTel" Width="70px" class="Aumen"></asp:TextBox>
                </li>
                <li style="width: 70px;">
                    <label>
                        Tel Fixo</label>
                    <asp:TextBox runat="server" ID="txtTelResResp" CssClass="campoTel" Width="70px" class="Aumen"></asp:TextBox>
                </li>
                <li>
                    <label class="lblObrigatorio">
                        Grau Parentesco</label>
                    <asp:DropDownList runat="server" ID="ddlGrauParen" Width="91px">
                        <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Pai/Mãe" Value="PM"></asp:ListItem>
                        <asp:ListItem Text="Tio(a)" Value="TI"></asp:ListItem>
                        <asp:ListItem Text="Avô/Avó" Value="AV"></asp:ListItem>
                        <asp:ListItem Text="Primo(a)" Value="PR"></asp:ListItem>
                        <asp:ListItem Text="Cunhado(a)" Value="CN"></asp:ListItem>
                        <asp:ListItem Text="Tutor(a)" Value="TU"></asp:ListItem>
                        <asp:ListItem Text="Irmão(ã)" Value="IR"></asp:ListItem>
                        <asp:ListItem Text="Outros" Value="OU"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li style="clear: both; margin: 4px 0 0 5px;">
                    <asp:CheckBox runat="server" ID="chkRespPasci" OnCheckedChanged="chkRespPasci_OnCheckedChanged"
                        AutoPostBack="true" />
                    <asp:Label runat="server" ID="lblchkRespPasci" Style="font-size: 11px; margin-left: -3px;">Responsável é o Paciente</asp:Label>
                </li>
            </ul>
        </div>
        <div class="divPaci">
            <ul>
                <li class="lblsub">
                    <asp:Label runat="server" ID="Label2" class="lblTituGr">Paciente</asp:Label>
                    <asp:HiddenField runat="server" ID="hidCoPaci" />
                </li>
                <li style="margin: 8px -5px 0 8px; clear: both"><a class="lnkPesNIRE" href="#">
                    <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                        style="width: 17px; height: 17px;" /></a> </li>
                <li>
                    <label>
                        NIS</label>
                    <asp:CheckBox runat="server" ID="chkPesqNisPaci" Style="margin-left: -5px" OnCheckedChanged="chkPesqNisPaci_OnCheckedChanged"
                        AutoPostBack="true" Checked="true" CssClass="campoNis" />
                    <asp:TextBox runat="server" ID="txtNisPaci" Width="75px" Style="margin-left: -6px"
                        CssClass="campoNis"></asp:TextBox>
                </li>
                <li>
                    <label>
                        CPF</label>
                    <asp:CheckBox runat="server" ID="chkPesqCPFPaci" Style="margin-left: -6px" OnCheckedChanged="chkPesqCPFPaci_OnCheckedChanged"
                        AutoPostBack="true" />
                    <asp:TextBox runat="server" ID="txtCPFPaci" Width="75px" CssClass="campoCpf" Style="margin-left: -6px"
                        Enabled="false" OnTextChanged="txtCPFPaci_OnTextChanged"></asp:TextBox>
                </li>
                <li style="margin-top: 10px; margin-left: -4px;">
                    <asp:ImageButton ID="imgCPFPaci" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                        OnClick="imgCPFPaci_OnClick" />
                </li>
                <li style="margin-left: 102px">
                    <label title="Operadora do plano de saúde do Paciente">
                        Operadora</label>
                    <asp:DropDownList runat="server" ID="ddlOperadora" Width="110px" ToolTip="Operadora do plano de saúde do Paciente">
                    </asp:DropDownList>
                </li>
                <li style="clear: both;">
                    <label class="lblObrigatorio" title="Nome do Paciente">
                        Nome</label>
                    <asp:TextBox runat="server" ID="txtNomePaciente" Width="275px" class="Aumen" ToolTip="Nome do Paciente"></asp:TextBox>
                </li>
                <li>
                    <label class="lblObrigatorio" title="Sexo do Paciente">
                        Sexo</label>
                    <asp:DropDownList runat="server" ID="ddlSxPac" CssClass="ddlSexo" ToolTip="Sexo do Paciente">
                        <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                        <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                        <asp:ListItem Text="Outros" Value="F"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <label class="lblObrigatorio">
                        Nascimento</label>
                    <asp:TextBox runat="server" ID="txtDtNasc" CssClass="campoData" OnTextChanged="txtDtNasc_OnTextChanged"
                        AutoPostBack="true"></asp:TextBox>
                </li>
                <li style="margin-left: -3px;">
                    <label>
                        Idade</label>
                    <asp:TextBox runat="server" ID="txtIdadePaci" Width="30px" Enabled="false" class="Aumen"></asp:TextBox>
                </li>
                <li style="width: 70px; clear: both;">
                    <label>
                        Tel Celular</label>
                    <asp:TextBox runat="server" ID="txtTelCelUsu" CssClass="campoTel" Width="70px" ToolTip="Telefone Celular do Paciente"
                        class="Aumen"></asp:TextBox>
                </li>
                <li style="width: 70px;">
                    <label>
                        Tel Fixo</label>
                    <asp:TextBox runat="server" ID="txtTelResUsu" CssClass="campoTel" Width="70px" ToolTip="Telefone Fixo do Paciente"
                        class="Aumen"></asp:TextBox>
                </li>
                <li style="margin-left: 139px;">
                    <label>
                        Data</label>
                    <asp:TextBox runat="server" ID="txtDataSenha" CssClass="campoData" ToolTip="Data da Senha de Atendimento"></asp:TextBox>
                </li>
                <li style="margin-left: 4px;">
                    <label>
                        Hora</label>
                    <asp:TextBox runat="server" CssClass="campoHora" Width="30px" ID="txtHoraSenha" ToolTip="Hora da Senha de Atendimento"></asp:TextBox>
                </li>
                <li style="margin-left: 4px;">
                    <label style="text-align: center;">
                        SENHA</label>
                    <asp:TextBox runat="server" ID="txtSenha" Width="35px" class="Aumen" Style="background-color: #FFFF99;"
                        ToolTip="Senha de Atendimento"></asp:TextBox>
                </li>
            </ul>
        </div>
        <li style="margin-top: 15px !important; height: 17px; width: 100%; text-align: center;
            text-transform: uppercase; margin-top: 0px; margin-left: auto; background-color: #B4EEB4;
            margin-bottom: 10px;">
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
                    <%--<li class="lblsub" style="margin-left:-2px; margin-top: -5px;">
                        <asp:Label runat="server" ID="Label1" class="lblTituGr">Encaminhar</asp:Label>
                    </li>--%>
                    <%-- "P" encaminha para presente
                         "A" encaminha para atendimento --%>
                    <%--<li style="border: none; margin-left:-2px;">
                        <asp:RadioButtonList runat="server" ID="rblEncaminhar" BorderStyle="None" CssClass="rblEncaminhar"
                            RepeatDirection="Vertical">
                            <asp:ListItem Text="Encaminha para recepção" Value="P" />
                            <asp:ListItem Text="Encaminha para atendimento" Value="A" />
                        </asp:RadioButtonList>
                    </li>--%>
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
            if ($('.campoTel').val().length <= 10) {
                $('.campoTel').mask("(99)9?999-99999");
            } else {
                $('.campoTel').mask("(99)9?9999-9999");
            }
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
