<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8266_AtendimentoClinico.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--CSS-->
    <style type="text/css">
        .lblSubInfos {
            color: Orange;
            font-size: 10px;
            border-width: 1px;
            border-color: Orange;
        }
        .divAvisoPermissao {
            top: 516px !important;
            left: 390px !important;
        }

        .ulDados {
            width: 1050px;
        }

            .ulDados li {
                margin-left: 5px;
                margin-bottom: 5px;
            }

        .ulDadosLog li {
            float: left;
            margin-left: 10px;
        }

        .ulPer label {
            text-align: left;
        }

        label {
            margin-bottom: 1px;
        }

        input {
        }

        .ulDadosGerais li {
            margin-left: 5px;
        }

        .liBtnAddA {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }

        .grdBusca th {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }

        .liBtnAddA {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            padding: 2px 3px 1px 3px;
        }

        .chk label {
            display: inline;
            margin-left: -4px;
        }

        .chk label {
            display: inline;
        }

        .liBtnConfirmarCiencia {
            width: 47px;
            background-color: #d09ad1;
            margin-left: 115px;
            margin-top: 10px;
            cursor: pointer;
            border: 1px solid #8B8989;
            padding: 4px 3px 3px;
        }

        .liBtnConfirm {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }

        #divCronometro {
            text-align: center;
            background-color: #FFE1E1;
            float: left;
            margin-left: 13px;
            margin-top: -48px;
            width: 115px;
            margin-right: -130px;
            display: none;
        }

        .LabelHora {
            margin-top: 4px;
            font-size: 10px;
        }

        .Hora {
            font-family: Trebuchet MS;
            font-size: 23px;
            color: #9C3535;
            margin-top: -3px;
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
        <label class="LabelHora">
            Tempo de Atendimento</label>
        <label id="lblHora" class="Hora">
            00:00:00</label>
    </div>
    <div style="float: right; margin-right: 10px; margin-top: -18px; color: Green;">
        <asp:CheckBox ID="chkSalvarAutomat" Text="Salvar Atendimento Automaticamente" CssClass="chk"
            runat="server" />
    </div>
    <ul class="ulDados">
        <li style="float: left; margin-left: 3px; width: 461px !important; border-right: 2px solid #EE9A00; padding-right: 10px;">
            <ul>
                <li>
                    <ul>
                        <li class="liTituloGrid" style="width: 450px !important; height: 20px !important; margin-right: 0px; background-color: #ADD8E6; text-align: center; font-weight: bold; margin-bottom: 2px; padding-top: 2px;">
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
                                <asp:GridView ID="grdPacientes" CssClass="grdBusca" runat="server" Style="width: 100%; cursor: default;"
                                    AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
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
                                                <asp:Image ID="imgContratacao" ImageUrl='<%# Eval("tpContr_URL") %>' ToolTip='<%# Eval("tpContr_TIP") %>'
                                                    Width="17px" Height="17px" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ST">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgSituacao" ImageUrl='<%# Eval("imagem_URL") %>'
                                                    ToolTip='<%# Eval("imagem_TIP") %>' Style="width: 18px !important; height: 18px !important; margin: 0 0 0 0 !important"
                                                    OnClick="imgSituacao_OnClick" />
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
                        <li class="liTituloGrid" style="width: 450px; height: 20px !important; margin-right: 0px; background-color: #c1ffc1; text-align: center; font-weight: bold; margin-bottom: 2px; padding-top: 2px; margin-left: 5px">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        DEMONSTRATIVO - AGENDA DO PACIENTE</label>
                                </li>
                                <li style="margin-left: 18px; float: right; margin-top: 2px;">
                                    <ul class="ulPer">
                                        <li>
                                            <asp:TextBox runat="server" class="campoData" ID="txtIniAgenda" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                            <asp:Label runat="server" ID="Label111"> &nbsp à &nbsp </asp:Label>
                                            <asp:TextBox runat="server" class="campoData" ID="txtFimAgenda" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                        </li>
                                        <li style="margin: 0px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqHistAgend" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqHistAgend_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: both; margin: -14px 0 0 0px;">
                                    <div id="divDemonAge" style="width: 448px; height: 105px; border: 1px solid #CCC; overflow-y: scroll">
                                        <input type="hidden" id="divDemonAge_posicao" name="divDemonAge_posicao" />
                                        <asp:GridView ID="grdHistoricoAgenda" CssClass="grdBusca" runat="server" Style="width: 100%; cursor: default;"
                                            AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
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
                                                        <asp:ImageButton runat="server" ID="imgSituacaoHistorico" Style="width: 18px !important; height: 18px !important; margin: 0 0 0 0 !important"
                                                            ImageUrl='<%# Eval("imagem_URL") %>'
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
                                                            ToolTip="Ficha de Atendimento" Style="width: 18px !important; height: 18px !important; margin: 0 0 0 0 !important" />
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
                <li style="clear: both; width: 450px; margin: 106px 0 0 6px;">
                    <ul>
                        <li style="width: 100%; color: Blue; border-bottom: 2px solid #58ACFA; margin-bottom: 2px;">
                            <label style="font-size: 12px;">
                                Registro de Informações</label>
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
                        <li style="margin-top: -4px; color: Red;">
                            <label>
                                CLASSIFICAÇÃO DE RISCO</label>
                            <asp:DropDownList runat="server" ID="ddlClassRisco" Width="90px" ClientIDMode="Static">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:HiddenField runat="server" ID="hidDivAberta" ClientIDMode="Static" />
                            <div id="divClassRisc" title="Selecione a Classificação de risco pela cor" style="cursor: pointer; margin: -20px 0 0 94px; width: 35px; height: 9px; border: 1px solid #CCCCCC; position: absolute; background-color: White; padding: 2px">
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
                    </ul>
                </li>
            </ul>
        </li>
        <li style="float: right; width: 550px !important;">
            <ul style="margin-left: -18px;">
                <li>
                    <ul>
                        <li>
                            <label style="color: Blue;">
                                SEU LOCAL:
                            </label>
                            <asp:DropDownList AutoPostBack="true" ID="ddlLocal" runat="server" OnSelectedIndexChanged="ddlLocal_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: 40px; margin-right: -123px;">
                            <label style="margin-left: -38px;">
                                Profissional Responsável</label>
                            <asp:DropDownList ID="drpProfResp" Width="185px" runat="server" Style="margin-left: -39px;" />
                        </li>
                        <li style="margin-left: 146px;">
                            <label>
                                Dt. Atendimento</label>
                            <asp:TextBox ID="txtDtAtend" runat="server" CssClass="campoData" />
                        </li>
                        <li style="margin-left: -1px;">
                            <label>
                                Hora</label>
                            <asp:TextBox ID="txtHrAtend" CssClass="campoHora" Width="28px" runat="server" />
                        </li>
                        <li style="margin-left: -1px;">
                            <label>
                                SENHA</label>
                            <asp:TextBox ID="txtSenha" Width="35px" BackColor="Yellow" runat="server" />
                        </li>
                        <li style="width: 508px; color: Blue; border-bottom: 2px solid #58ACFA; margin-bottom: 0px; margin-top: -11px;">
                            <label style="font-size: 12px; float: left;">
                                Informações Prévias</label>
                        </li>
                        <li style="clear: both; margin-left: 7px; margin-bottom: -8px; width: 25px;">
                            <label>
                                Altura</label>
                            <asp:TextBox ID="txtAltura" CssClass="campoAltura" Width="30" runat="server" />
                        </li>
                        <li style="margin: 3px -2px 0 6px; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;"></li>
                        <li style="margin-bottom: -8px; width: 25px;">
                            <label>
                                Peso</label>
                            <asp:TextBox ID="txtPeso" CssClass="campoPeso" Width="32" runat="server" />
                        </li>
                        <li style="margin: 3px -2px 0 6px; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;"></li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 70px;">
                            <label>
                                Pressão Val/HR</label>
                            <asp:TextBox ID="txtPressao" Width="30" CssClass="campoPressArteri" runat="server" />
                            <asp:TextBox ID="txtHrPressao" Width="30" CssClass="campoHora" runat="server" />
                        </li>
                        <li style="margin: 3px -2px 0 1px; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;"></li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 70px;">
                            <label>
                                Temp Val/HR</label>
                            <asp:TextBox ID="txtTemp" Width="30" CssClass="campoTemp" runat="server" />
                            <asp:TextBox ID="txtHrTemp" Width="30" CssClass="campoHora" runat="server" />
                        </li>
                        <li style="margin: 3px -2px 0 1px; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;"></li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 70px;">
                            <label>
                                Glicem Val/HR</label>
                            <asp:TextBox ID="txtGlic" Width="30" CssClass="campoGlicem" runat="server" />
                            <asp:TextBox ID="txtHrGlic" Width="30" CssClass="campoHora" runat="server" />
                        </li>
                        <li style="margin: 3px -2px 0 1px; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;"></li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 45px;">
                            <label>
                                Dores?</label>
                            <asp:DropDownList ID="drpDores" Height="13px" Width="40px" runat="server">
                                <asp:ListItem Value="S" Text="Sim" />
                                <asp:ListItem Value="N" Text="Não" Selected="True" />
                            </asp:DropDownList>
                        </li>
                        <li style="margin: 3px -1px 0 0; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;"></li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 45px;">
                            <label>
                                Enjôos?</label>
                            <asp:DropDownList ID="drpEnjoos" Height="13px" Width="40px" runat="server">
                                <asp:ListItem Value="S" Text="Sim" />
                                <asp:ListItem Value="N" Text="Não" Selected="True" />
                            </asp:DropDownList>
                        </li>
                        <li style="margin: 3px -1px 0 0; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;"></li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 45px;">
                            <label>
                                Vômitos?</label>
                            <asp:DropDownList ID="drpVomitos" Height="13px" Width="40px" runat="server">
                                <asp:ListItem Value="S" Text="Sim" />
                                <asp:ListItem Value="N" Text="Não" Selected="True" />
                            </asp:DropDownList>
                        </li>
                        <li style="margin: 3px -1px 0 0; height: 23px; width: 1px; border-left: 1px solid #BDBDBD;"></li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 45px;">
                            <label>
                                Febre?</label>
                            <asp:DropDownList ID="drpFebre" Height="13px" Width="40px" runat="server">
                                <asp:ListItem Value="S" Text="Sim" />
                                <asp:ListItem Value="N" Text="Não" Selected="True" />
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both"></li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 70px; margin-left: -2px;">
                            <label>Pressão Arterial</label>
                            <asp:TextBox runat="server" ID="tbpressaoarterial" Style="width: 67px; margin-left: 0px" CssClass="campoPressArteri"></asp:TextBox>
                        </li>
                        <li style="margin-bottom: -8px; margin-right: 0px; width: 70px; margin-left: 5px;">
                            <label>Saturação</label>
                            <asp:TextBox runat="server" ID="tbsaturacao" Style="width: 40px; margin-left: 0px" CssClass="campoGlicem"></asp:TextBox>
                        </li>

                        <li class="liTituloGrid" style="width: 508px !important; height: 20px !important; clear: both; margin-right: 0px; background-color: #FFEC8B; text-align: center; font-weight: bold; margin-bottom: 2px; padding-top: 2px;">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: Black; float: left; margin-left: 10px;">
                                REGISTRO DO ATENDIMENTO</label>
                            <asp:Button runat="server" ID="btn_SIGTAP" Text="PROCEDIMENTOS" Style="background-color: #ffa07a; margin-top: 0px; font-size: 13px; font-weight: normal; margin-left: -10PX; border-width: 0px; height: 17px !important;" OnClick="btn_SIGTAP_Click" ToolTip="Pesquisar códigos procedimento" />
                            <asp:Button runat="server" ID="btn_GESTANTE" Text="GESTANTE" Style="background-color: yellow; margin-top: 0px; font-size: 13px; font-weight: normal; margin-left: 10PX; border-width: 0px; height: 17px !important;" OnClick="btn_GESTANTE_Click" ToolTip="Inserir dados da Gestante" />

                            &nbsp;<div style="margin-right: 3px; float: right; margin-top: 4px;">
                                <img title="Emitir Prontuário do Paciente" style="margin-top: -2px" src="/BarrasFerramentas/Icones/Imprimir.png"
                                    height="16px" width="16px" />
                                <asp:LinkButton ID="lnkbProntuario" runat="server" OnClick="lnkbProntuario_OnClick"
                                    ForeColor="#0099ff">PRONTUÁRIO</asp:LinkButton>
                            </div>
                            <div id="divBtnOdontograma" runat="server" style="margin-right: 5px; float: right; margin-top: 4px;">
                                <img title="Emitir Odontograma do Paciente" style="margin-top: -2px" src="/Library/IMG/PGS_IC_Anexo.png"
                                    height="16px" width="16px" />
                                <asp:LinkButton ID="lnkbOdontograma" runat="server" ForeColor="#0099ff">HISTÓRICO</asp:LinkButton>
                            </div>
                        </li>
                        <li style="margin: -2px 0 0 5px; border: 1px solid #FFEC8B; border-top: 0;">
                            <ul>
                                <li style="margin-top: 2px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        QUEIXA PRINCIPAL</label>
                                    <asp:TextBox runat="server" ID="txtQueixa" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Rows="2" Style="width: 496px; margin-top: 1px; border-top: 1px solid #BDBDBD; border-left: 0; border-right: 0; border-bottom: 0;"
                                        Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -6px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        ANAMNESE / HDA (História da Doença Atual)</label>
                                    <asp:TextBox runat="server" ID="txtHDA" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Rows="4" Style="width: 496px; margin-top: 1px; border-top: 1px solid #BDBDBD; border-left: 0; border-right: 0; border-bottom: 0; height: 50px;"
                                        Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -6px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        AÇÃO PLANEJADA</label>
                                    <asp:TextBox runat="server" ID="txtAcaoPlanejada" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Rows="2" Style="width: 496px; margin-top: 1px; border-top: 1px solid #BDBDBD; border-left: 0; border-right: 0; border-bottom: 0;"
                                        Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -6px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        HIPÓTESE DIAGNÓSTICA / AÇÃO REALIZADA</label>
                                    <asp:TextBox runat="server" ID="txtHipotese" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Rows="3" Style="width: 400px; border-top: 1px solid #BDBDBD; border-left: 0; border-right: 0; border-bottom: 0;"
                                        Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="margin-top: 6px; margin-left: 0px; width: 90px;">
                                    <asp:Button ID="btnCid" Style="background-color: #8DE4E4; font-size: 9px; width: 90px"
                                        runat="server" Text="DEFINIÇÃO DE CID" Height="14px" />
                                    <asp:TextBox runat="server" ID="txtCids" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Rows="2" Style="width: 90px; border-top: 1px solid #BDBDBD; border-left: 0; border-right: 0; border-bottom: 0;"
                                        Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -6px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        RESULTADO DE EXAMES</label>
                                    <asp:TextBox runat="server" ID="txtExameFis" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Rows="3" Style="width: 245px; border-top: 1px solid #BDBDBD; border-left: 0; border-right: 0; border-bottom: 0; height: 26px;"
                                        Font-Size="12px"></asp:TextBox>
                                </li>
                                <li style="margin-top: -6px; margin-left: 0px;">
                                    <label style="color: Orange; margin-left: 3px; font-size: 9px;">
                                        CONDUTA / OBSERVAÇÕES</label>
                                    <asp:TextBox runat="server" ID="txtObserAtend" BackColor="#FAFAFA" TextMode="MultiLine"
                                        Rows="3" Style="width: 245px; border-top: 1px solid #BDBDBD; border-left: 0; border-right: 0; border-bottom: 0; height: 26px;"
                                        Font-Size="12px"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li style="margin-left: 5px; margin-top: 8px; height: 15px; cursor: pointer"></li>
                <li style="float: left; margin: 8px 0 0 0;">
                    <asp:LinkButton ID="lnkbAnexos" AccessKey="A" OnClick="lnkbAnexos_OnClick" ToolTip="Anexos associados ao Atendimento/Paciente"
                        runat="server">
                                <img class="imgAnexos" style="width: 16px; height: 16px !important;" src="/Library/IMG/PGS_IC_Anexo.png" alt="Icone" />
                                <span style="margin: 3px 22px 0 5px;">ANEXOS</span>
                    </asp:LinkButton>
                    <asp:Button ID="BtnProntuCon" runat="server" Style="background-color: #D8D8D8; margin-left: 0px; width: 85px"
                        ToolTip="Prontuário Convencional" Text="PRONTU.CONVE." Height="20px"
                        OnClick="BtnProntuCon_Click" />
                    <asp:Button ID="lnkAmbul" Style="background-color: #D8D8D8; margin-left: 0px; width: 85px"
                        runat="server" Text="AMBULATÓRIO" Height="20px" OnClick="lnkAmbul_OnClick" />
                    <asp:Button ID="lnkMedic" Style="background-color: #D8D8D8; margin-left: 0px; width: 85px"
                        runat="server" Text="MEDICAMENTOS" Height="20px" OnClick="lnkMedic_OnClick" />
                    <asp:Button ID="lnkExame" Style="background-color: #D8D8D8; margin-left: 2px; width: 70px"
                        runat="server" Text="EXAMES" Height="20px" OnClick="lnkExame_OnClick" />
                    <asp:Button ID="lnkOrcamento" Style="background-color: #D8D8D8; margin-left: 2px; width: 80px"
                        runat="server" Text="ORÇAMENTO" Height="20px" OnClick="lnkOrcamento_OnClick" />
                </li>
                <li style="margin: 15px 0 0 0px;">
                    <ul>
                        <li style="margin: -5px 0 0 0px; padding-left: 10px; clear: both">
                            <asp:Button ID="BtnObserv" Style="background-color: #0099ff; color: #FFFAFA; margin-left: 0px; width: 80px"
                                runat="server" Text="CAMPO LIVRE" Height="20px" OnClick="BtnObserv_OnClick" />
                            <asp:Button ID="BtnFicha" Style="background-color: #0099ff; color: #FFFAFA; margin-left: 2px; width: 80px"
                                runat="server" Text="FICHA ATEND." Height="20px" OnClick="lnkFicha_OnClick" />
                            <asp:Button ID="BtnAtestado" runat="server" Style="background-color: #FF9933; color: #FFFAFA; margin-left: 9px; width: 69px"
                                Text="ATESTADO" Height="20px" OnClick="BtnAtestado_Click" />
                            <asp:Button ID="BtnLaudo" runat="server" Style="background-color: #FF9933; color: #FFFAFA; margin-left: 2px; width: 70px"
                                Text="RELATÓRIO" Height="20px" OnClick="BtnLaudo_Click" />
                            <asp:Button ID="BtnGuia" runat="server" Style="background-color: #FF9933; color: #FFFAFA; margin-left: 2px; width: 35px"
                                Text="GUIA" Height="20px" OnClick="BtnGuia_OnClick" />
                            <asp:Button ID="BtnSalvar" Style="background-color: #006600; color: #FFFAFA; width: 60px; margin-left: 9px;"
                                runat="server" Text="SALVAR" Height="20px" OnClick="BtnSalvar_OnClick" />
                            <asp:Button ID="BtnFinalizar" Style="background-color: #880000; color: #FFFAFA; width: 70px; margin-left: 2px;"
                                runat="server" Text="FINALIZAR" Height="20px" OnClick="BtnFinalizar_OnClick" />
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
                            <asp:GridView ID="grdLogAgendamento" CssClass="grdBusca" runat="server" Style="width: 100%; cursor: default;"
                                AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
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
                                            <img src='<%# Eval("CAMINHO_IMAGEM") %>' alt="" style="width: 16px !important; height: 16px !important; margin: 0 0 0 0 !important"
                                                title="Representação gráfica da Ação" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DE_TIPO" HeaderText="AÇÃO">
                                        <ItemStyle HorizontalAlign="Left" Width="240px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Style="margin: 0 0 0 0 !important; height: 23px !important; width: 180px"
                                                ReadOnly="true" Text='<%# Eval("OBS") %>'></asp:TextBox>
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
                        <div style="border: 1px solid #CCCCCC; width: 480px; height: 240px; overflow-y: scroll; margin-top: 10px;">
                            <asp:GridView ID="grdPacAtestado" CssClass="grdBusca" runat="server" Style="width: 100% !important; cursor: default"
                                AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
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
                    <li id="li10" runat="server" class="liBtnAddA" style="margin-top: 10px !important; margin-left: 230px !important; height: 15px;">
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
                    <li class="liBtnAddA" style="clear: none !important; margin-left: 180px !important; margin-top: 8px !important; height: 15px;">
                        <asp:LinkButton ID="lnkbImprimirGuia" runat="server" ValidationGroup="guia" OnClick="lnkbImprimirGuia_OnClick"
                            ToolTip="Imprimir guia do plano de saúde">
                            <asp:Label runat="server" ID="lblEmitirGuia" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
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
                    <li title="Clique para cadastrar um novo medicamento" class="liBtnAddA" style="margin: 5px 0 0 65px !important; width: 40px;">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Medicamento" src="/Library/IMG/Gestor_BtnEdit.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="lnkNovoMedicam" runat="server" OnClick="lnkNovoMedicam_OnClick">Novo</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li>
                <div style="margin-left: 5px; width: 755px; height: 100px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdPesqMedic" CssClass="grdBusca" runat="server" Style="width: 100%; cursor: default;"
                        AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
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
                    <li title="Clique para adicionar o medicamento" class="liBtnAddA" style="clear: both; margin-top: -4px; margin-left: 350px !important; width: 75px;">
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
                            <li class="liTituloGrid" style="height: 20px !important; width: 735px; margin-left: -10px; background-color: #A9D0F5; text-align: center; font-weight: bold; float: left">
                                <ul>
                                    <li style="margin: 0 0 0 10px; float: left">
                                        <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                            MEDICAMENTOS</label>
                                    </li>
                                </ul>
                            </li>
                            <li title="Clique para emitir o Receituario do atendimento" class="liBtnAddA" style="float: right; margin: -25px -2px 3px 0px; width: 12px; height: 15px;">
                                <asp:ImageButton ID="BtnReceituario" runat="server" OnClick="BtnReceituario_Click"
                                    ToolTip="Emitir Receituario do Paciente" Style="margin-top: -2px; margin-left: -3px;"
                                    ImageUrl="/BarrasFerramentas/Icones/Imprimir.png" Height="18px" Width="18px" />
                            </li>
                        </ul>
                    </li>
                    <li style="clear: both; margin: -7px 0 0 -5px;">
                        <div style="width: 755px; height: 80px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdMedicamentos" CssClass="grdBusca" runat="server" Style="width: 100%; cursor: default;"
                                AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
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
    <div id="divExames" style="display: none; height: 400px !important; width: 800px">
        <ul class="ulDados">
            <li>
                <ul style="width: 766px">
                    <li class="liTituloGrid" style="width: 673px; height: 20px !important; margin-left: -0px; background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
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
                        class="liBtnAddA" style="float: right; margin: -25px 22px 3px 5px; height: 15px; width: 12px;">
                        <asp:ImageButton ID="lnkAddProcPla" Height="15px" Width="15px" Style="margin-top: -1px; margin-left: -2px;"
                            ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" OnClick="lnkAddProcPla_OnClick"
                            runat="server" />
                    </li>
                    <li title="Clique para emitir os exames do paciente" class="liBtnAddA" style="float: right; margin: -25px -2px 3px 0px; width: 12px; height: 15px;">
                        <asp:ImageButton ID="BtnExames" runat="server" OnClick="BtnExames_OnClick" ToolTip="Emitir Exames do Paciente"
                            Style="margin-top: -2px; margin-left: -3px;" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png"
                            Height="18px" Width="18px" />
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin: -7px 0 0 5px !important;">
                <div style="width: 766px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdExame" CssClass="grdBusca" runat="server" Style="width: 100%; cursor: default;"
                        AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
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
                                    <asp:TextBox runat="server" ID="txtCodigProcedPla" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                        Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALOR">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtValorProced" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                        Enabled="false"></asp:TextBox>
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
            <li class="liBtnAddA" style="clear: none !important; margin-left: 727px !important; margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="btnGuiaExames" runat="server" OnClick="BtnGuiaExames_OnClick"
                    ToolTip="Imprimir laudo técnico">
                    <asp:Label runat="server" ID="Label7" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <asp:HiddenField runat="server" ID="hidObsSerAmbulatoriais" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="didIdServAmbulatorial" ClientIDMode="Static" />
    <div id="divAmbulatorio" style="display: none; height: 400px !important; width: 900px">
        <ul class="ulDados">
            <li>
                <ul style="width: 869px">
                    <li class="liTituloGrid" style="width: 769px; height: 20px !important; margin-left: -0px; background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                        <ul>
                            <li style="margin: 0 0 0 10px; float: left">
                                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                    SERVIÇOS AMBULATORIAIS</label>
                            </li>
                            <li style="float: right; margin-top: 3px; margin-right: 15px;">
                                <ul>
                                    <li>
                                        <asp:Label runat="server" ID="Label8">Contratação</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlOperPlanoServAmbu" Width="150px" OnSelectedIndexChanged="ddlOperPlanoServAmbu_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: -2px;">
                                        <asp:Label runat="server" ID="Label13">Plano</asp:Label>
                                        <asp:DropDownList runat="server" Width="150px" ID="ddlPlanoServAmbu" OnSelectedIndexChanged="ddlPlanoServAmbu_OnSelectedIndexChanged"
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
                        class="liBtnAddA" style="float: right; margin: -25px 22px 3px 5px; height: 15px; width: 12px;">
                        <asp:ImageButton ID="ImageButton1" Height="15px" Width="15px" Style="margin-top: -1px; margin-left: -2px;"
                            ImageUrl="/Library/IMG/Gestor_SaudeEscolar.png" runat="server"
                            OnClick="lnkAddProcPlaAmbulatorial_OnClick" />
                    </li>
                    <li title="Clique para emitir os serviços ambulatoriais" class="liBtnAddA" style="float: right; margin: -25px -2px 3px 0px; width: 12px; height: 15px;">
                        <asp:ImageButton ID="ImageButton" runat="server" OnClick="btnGuiaServAmbulatoriais_OnClick"
                            Style="margin-top: -2px; margin-left: -3px;" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png"
                            Height="18px" Width="18px" />
                    </li>
                </ul>
            </li>
            <li style="height: 176px;">
                <ul>
                    <li style="clear: both; margin: -7px 0 0 0 !important;">
                        <div style="width: 869px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdServAmbulatoriais" CssClass="grdBusca" runat="server" Style="width: 100%; cursor: default;"
                                AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Procedimento de Plano de Saúde associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CONTRATAÇÃO *">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlGridContratacaoAmbul" Width="100%" AutoPostBack="true"
                                                Style="margin: 0 0 0 -4px !important;" OnSelectedIndexChanged="ddlGridContratacaoAmbul_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PLANO *">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlGridPlanoAmbul" Width="100%" Style="margin: 0 0 0 -4px !important;">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TIPO *">
                                        <ItemStyle Width="23px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlGridTipoAmbul" Width="100%" Style="margin: 0 0 0 -4px !important;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="M">MED</asp:ListItem>
                                                <asp:ListItem Value="P">PRO</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" DESCRIÇÃO *">
                                        <ItemStyle Width="97px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtDefServAmbulatorial" Style="margin: 0; width: 88%"></asp:TextBox>
                                            <asp:ImageButton ID="imgbPesqServAmbul" CssClass="btnProcurar" ValidationGroup="pesqPac"
                                                runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="ddlGridTipoAmbul_SelectedIndexChanged"
                                                Width="10px" />
                                            <asp:DropDownList runat="server" ID="ddlServAmbulatorial" OnSelectedIndexChanged="ddlServAmbu_OnSelectedIndexChanged"
                                                AutoPostBack="true" Style="margin: 0 0 0 -4px !important; width: 88%" Visible="false">
                                            </asp:DropDownList>
                                            <asp:ImageButton ID="imgbVoltarPesqServAmbul" ValidationGroup="pesqPac" CssClass="btnProcurar"
                                                Width="10px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgbVoltarPesqServAmbul_OnClick"
                                                Visible="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CÓDIGO">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtCodigoServAmbulatorial" Width="100%" Style="margin: 0 0 0 -4px !important;"
                                                Enabled="false">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VALOR">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtValorServAmbulatorial" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                                Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COMPLEMENTO">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtComplementoServAmbulatorial" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"></asp:TextBox>
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
                    <li style="clear: both; margin: 3px 0 0 0;">
                        <asp:TextBox runat="server" ID="txtObsServAmbulatoriais" TextMode="MultiLine" ClientIDMode="Static"
                            Style="width: 869px; height: 15px;" Font-Size="11px" MaxLength="200" placeholder=" Digite as observações sobre o Serviço Ambulatorial"></asp:TextBox>
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin-left: 816px;">
                <asp:Button runat="server" ID="Button3" Text="SALVAR" Style="height: 20px; background-color: #0b3e6f; color: #fff; cursor: pointer; width: 56px"
                    Font-Bold="true" OnClick="btnSalvarServAmbulatorial_OnClick" />
            </li>
        </ul>
    </div>
    <asp:HiddenField runat="server" ID="hidObsOrcam" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidDtValidOrcam" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hidCkOrcamAprov" ClientIDMode="Static" />
    <div id="divOrcamentos" style="display: none; height: 340px !important; width: 450px">
        <ul class="ulDados">
            <li>
                <ul style="width: 443px">
                    <li class="liTituloGrid" style="width: 300px; height: 20px !important; margin-left: -5px; background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                        <ul>
                            <li style="margin-left: 5px; float: left; width: 60px;">
                                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FF6347">
                                    ORÇAMENTO</label>
                            </li>
                            <li style="float: right; margin-top: 3px; margin-right: -3px;">
                                <ul>
                                    <li>
                                        <asp:Label runat="server" ID="lbloper">Cont</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlOperOrc" OnSelectedIndexChanged="ddlOperOrc_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <asp:Label runat="server" ID="Label3">Plan</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlPlanOrc">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li title="Clique para Adicionar um item de orçamento" class="liBtnAddA" style="float: right; margin: -25px 77px 3px 0px; width: 61px">
                        <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="btnAddProcOrc" runat="server" OnClick="btnAddProcOrc_OnClick">Adicionar</asp:LinkButton>
                    </li>
                    <li title="Clique para emitir o orçamento do paciente" class="liBtnAddA" style="float: right; margin: -25px -2px 3px 5px; width: 70px">
                        <img title="Emitir Orçamento do Paciente" style="margin-top: -1px" src="/BarrasFerramentas/Icones/Imprimir.png"
                            height="16px" width="16px" />
                        <asp:LinkButton ID="lnkbOrcamento" runat="server" OnClick="lnkbOrcamento_Click">Orçamento</asp:LinkButton>
                    </li>
                </ul>
            </li>
            <li style="clear: both; margin: -7px 0 0 0;">
                <div style="width: 448px; height: 85px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdProcedOrcam" CssClass="grdBusca" runat="server" Style="width: 100%; cursor: default;"
                        AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
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
                                        AutoPostBack="true" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hidValUnitProc" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PROCEDIMENTO">
                                <ItemStyle Width="180px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtCodigProcedOrc" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                        Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTD">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtQtdProcedOrc" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                        OnTextChanged="txtQtdProcedOrc_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALOR">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtValorProcedOrc" Width="100%" CssClass="campoDecimal"
                                        Style="margin-left: -4px; margin-bottom: 0px;" OnTextChanged="txtValorProcedOrc_OnTextChanged"
                                        AutoPostBack="true"></asp:TextBox>
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
            <li style="clear: both; margin: 3px 0 0 0;">
                <asp:TextBox runat="server" ID="txtObsOrcam" TextMode="MultiLine" ClientIDMode="Static"
                    Style="width: 448px; height: 15px;" Font-Size="13px" placeholder=" Digite as observações sobre o Orçamento"></asp:TextBox>
            </li>
            <li style="clear: both; margin-top: 5px; margin-left: -2px;">
                <ul>
                    <li style="margin-top: 5px;">
                        <asp:Label ID="Label1" runat="server">VALIDADE</asp:Label>
                        <asp:TextBox runat="server" ID="txtDtValidade" CssClass="campoData" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="Label2" runat="server">DESCONTO - R$</asp:Label>
                        <asp:TextBox runat="server" ID="txtVlDscto" Style="margin-top: 4px; height: 16px !important; width: 45px"
                            CssClass="campoDecimal campoMoeda" OnTextChanged="txtVlDscto_OnTextChanged"
                            AutoPostBack="true"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="Label33" runat="server">TOTAL - R$</asp:Label>
                        <asp:TextBox runat="server" ID="txtVlTotalOrcam" Enabled="false" Style="margin-top: 4px; height: 16px !important; width: 47px"
                            CssClass="campoMoeda"></asp:TextBox>
                    </li>
                    <li style="margin-top: 5px; margin-left: 2px;">
                        <asp:CheckBox ID="chkAprovado" CssClass="chk" runat="server" Text="Aprovado" ToolTip="Marque se o orçamento foi aprovado para o faturamento"
                            ClientIDMode="Static" />
                    </li>
                </ul>
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
            <li id="li6" runat="server" class="liBtnAddA" style="margin-top: 0px !important; clear: both !important; height: 15px; margin-left: 140px !important;">
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
            <li id="li7" runat="server" class="liBtnAddA" style="margin-top: 0px !important; clear: both !important; height: 15px; margin-left: 230px !important;">
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
            <li class="liBtnAddA" style="clear: none !important; margin-left: 250px !important; margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirLaudo" ValidationGroup="laudo" runat="server" OnClick="lnkbImprimirLaudo_OnClick"
                    ToolTip="Imprimir laudo técnico">
                    <asp:Label runat="server" ID="Label14" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divProntuCon" style="display: none; height: 440px !important;">
        <asp:HiddenField ID="hidIdProntuCon" runat="server" />
        <ul class="ulDados" style="width: 730px; margin-top: 0px !important">
            <li>
                <label>
                    Nº PRONTUÁRIO</label>
                <%--<asp:CheckBox runat="server" Checked="true" ID="chkNumPront" OnCheckedChanged="chkNumPront_CheckedChanged" AutoPostBack="true" style="margin: 0 -7px 0 -6px;" />--%>
                <asp:TextBox runat="server" ID="txtNumPront" MaxLength="20" Style="width: 60px;"></asp:TextBox>
            </li>
            <li>
                <label>
                    Nº PASTA</label>
                <%-- <asp:CheckBox runat="server" ID="chkNumPasta" Enabled="false" OnCheckedChanged="chkNumPasta_CheckedChanged" AutoPostBack="true" style="margin: 0 -7px 0 -6px;"/>--%>
                <asp:TextBox runat="server" ID="txtNumPasta" MaxLength="20" Style="width: 60px;"></asp:TextBox>
            </li>
            <li style="">
                <label for="drpPacienteProntuCon" title="Paciente" class="lblObrigatorio">
                    Paciente</label>
                <asp:DropDownList ID="drpPacienteProntuCon" OnSelectedIndexChanged="drpPacienteProntuCon_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="160px" Visible="false">
                </asp:DropDownList>
                <asp:TextBox ID="txtPacienteProntuCon" Width="160px" ToolTip="Digite o nome ou parte do nome do paciente, no mínimo 3 letras."
                    runat="server" />
            </li>
            <li style="margin-top: 11px; margin-left: -4px;">
                <asp:ImageButton ID="imgbPesqPacienteProntuCon" Style="width: 16px;" runat="server" ImageUrl="~/Library/IMG/IC_PGS_Recepcao_CadPacien.png"
                    OnClick="imgbPesqPacNome_OnClick" />
                <asp:ImageButton ID="imgbVoltPacienteProntuCon" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                    OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
            </li>
            <li>
                <label>Qualificação Prontuário</label>
                <asp:DropDownList runat="server" ID="ddlQualifPront" Width="150px"></asp:DropDownList>
            </li>
            <li>
                <label>
                    Início</label>
                <asp:TextBox runat="server" ID="txtIniPront" CssClass="campoData"></asp:TextBox>
            </li>
            <li style="margin: 16px 0 0 0;">até</li>
            <li>
                <label>
                    Fim</label>
                <asp:TextBox runat="server" ID="txtFimPront" CssClass="campoData"></asp:TextBox>
            </li>
            <li style="margin: 11px 0 0 0;">
                <asp:ImageButton ID="imgBtnPesqPront" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                    OnClick="imgBtnPesqPront_OnClick" />
            </li>
            <li style="clear: both;">
                <label title="DESCRIÇÃO" style="color: Blue;">
                    DESCRIÇÃO</label>
                <asp:ImageButton ID="imgBRel" runat="server" ToolTip="Emitir relatório dos prontuários selecionados"
                    ImageUrl="~/Library/IMG/Gestor_IcoImpres.ico" Width="15px" Height="15px" Style="margin: -26px 14px 7px 688px;" OnClick="imgBRel_OnClick" />
                <%--<asp:TextBox ReadOnly="true" Font-Size="12px" ID="txtObsProntuCon" Width="600px"
                    Height="200px" TextMode="MultiLine" runat="server" />--%>
                <div runat="server" id="divObsProntuCon" style="font-size: 12px; width: 707px; height: 182px; overflow: auto; border: 1px solid #BBBBBB;">
                </div>
            </li>
            <li style="clear: both;">
                <label title="Inserir descrição" style="color: Blue;">
                    INSIRA A DESCRIÇÃO LOGO ABAIXO</label>
                <asp:TextBox ID="txtCadObsProntuCon" Font-Size="12px" Width="707px" Height="100px"
                    TextMode="MultiLine" runat="server" />
            </li>
            <li class="liBtnAddA" style="clear: none !important; margin-left: 316px !important; margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirProntuCon" ValidationGroup="prontu" runat="server"
                    OnClick="lnkbImprimirProntuCon_OnClick  " ToolTip="Confirmar prontuário convencional">
                    <asp:Label runat="server" ID="Label9" Text="CONFIRMAR" StyltxtCadObsProntuCone="margin-left: 2px;"></asp:Label>
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
                <asp:TextBox runat="server" ID="txtObservacoes" TextMode="MultiLine" Style="width: 530px; height: 240px;"
                    Font-Size="13px" ToolTip="Digite as observações sobre o Atendimento"></asp:TextBox>
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
            <li id="li5" runat="server" class="liBtnAddA" style="float: right; margin-top: 10px !important; clear: none !important; height: 15px;">
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
                    <asp:GridView ID="grdAnexos" CssClass="grdBusca" runat="server" Style="width: 100%; cursor: default;"
                        AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
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
            <li class="liTituloGrid" style="width: 865px !important; height: 20px !important; margin-top: 15px; background-color: #A9E2F3; margin-bottom: 2px; padding-top: 2px; clear: both;">
                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; float: left; margin-left: 10px;">
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
        <!-- INICIO ------------------------------------------------------------------------------------------------------------------------------------- INICIO MODAL PROCEDIMENTOS -->
        <div id="divLoadInfosSigtap" style="display: none; left: 1px !important; width: 995px !important; overflow-x: hidden !important;">
            <div style="width: 100%;">
                <asp:Label runat="server" ID="Label15" Text="Pesquisa" CssClass="lblSubInfos"></asp:Label>
            </div>
            <br />
            <div style="float: left;">
                <asp:Label runat="server" ID="lbl12456" Text="Grupo do Procedimento"></asp:Label><br />
                <asp:DropDownList runat="server" ID="ddlgrupoprocedimento" Style="width: 200px;" ToolTip="Escolha o grupo do procedimento." AutoPostBack="True" OnSelectedIndexChanged="ddlgrupoprocedimento_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div style="float: left;">
                <asp:Label runat="server" ID="Label16" Text="Sub-Grupo do Procedimento" Style="margin-left: 13px;"></asp:Label><br />
                <asp:DropDownList runat="server" ID="ddlsubgrupoprocedimento" Style="width: 200px; margin-left: 13px;" ToolTip="Escolha o grupo do procedimento."></asp:DropDownList>
            </div>
            <div style="float: left;">
                <asp:Label runat="server" ID="Label17" Text="Texto Livre" Style="margin-left: 13px;"></asp:Label><br />
                <asp:TextBox runat="server" ID="tbtextolivreprocedimento" Style="margin-left: 13px; width: 340px;"></asp:TextBox>
            </div>
            <div style="margin-top: 12px; margin-left: 0px; float: right;">
                <asp:ImageButton ID="imgPesqProcedimentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgPesqProcedimentos_Click" />
            </div>
            <div style="clear: both"></div>

            <asp:GridView runat="server" ID="grdListarSIGTAP" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grdListarSIGTAP_PageIndexChanging1" PageSize="14" Width="770">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Paciente Encontrado<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" CssClass="headerStyleLA" />
                <AlternatingRowStyle CssClass="alternateRowStyleLA" Height="15" />
                <RowStyle CssClass="rowStyleLA" Height="15" />
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
                        <ItemStyle Width="500px" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <br />
            <div>
                <center>
                    <asp:Button runat="server" CssClass="btn" ID="btnclose" Text="  Fechar  " Style="height: 30px !important;" />
                    <asp:Button runat="server" CssClass="btn" ID="btnincluir" Text=" Inserir Procedimento no atendimento " Style="height: 30px !important; width: 180px !important" OnClick="btnincluir_Click1" />
                </center>
            </div>
            <br />
            <div id="divHelpTxtLA">
                <p id="pAcesso" class="pAcesso">
                    Verifique os SIDTAP existentes no quadro acima para incluir no atendimento.
                </p>
            </div>
        </div>
        <!-- FIM ------------------------------------------------------------------------------------------------------------------------------------- DIV MODAL PROCEDIMENTOS -->
        <!-- INICIO ------------------------------------------------------------------------------------------------------------------------------------- INICIO MODAL GESTANTE -->
        <div id="divLoadInfosGestante" style="display: none; height: 400px !important; left: 15px !important;">
        <ul class="ulDados" style="width: 400px !important; margin-top: 7px !important;">
            <div class="DivResp" runat="server" id="divResp">
                <ul class="ulDadosResp" style="margin-left: -177px !important; width: 746px !important;">

                    <li>
                        <asp:Label runat="server" ID="titulo" Text="DADOS GESTACIONAIS DO(A) PACIENTE" Font-Bold="false" CssClass="lblSubInfos"></asp:Label>
                    </li>
                    <br />
                    <br />

                    <li>DUM<br />
                        <asp:TextBox runat="server" ID="tbdum" ToolTip="Data da última mestruação" class="campoData"></asp:TextBox>
                    </li>

                    <li>Observações DUM<br />
                        <asp:TextBox runat="server" ID="tbobsdum" Width="545px" ToolTip="Observação sobre a última mestruação"></asp:TextBox>
                    </li>

                    <li>DPP<br />
                        <asp:TextBox runat="server" ID="tbdpp" ToolTip="Data Provável do Parto" class="campoData"></asp:TextBox>
                    </li>
                    <li style="clear: both"></li>

                    <li style="margin-left:-5px;">
                        <asp:Label runat="server" ID="Label18" Text="ESCUTA TRIAGEM - SINAIS VITAIS DA PACIENTE" Font-Bold="false" CssClass="lblSubInfos" style="width:-5px !important;"></asp:Label><br />
                    </li>
                    <li style="clear: both"></li>


                    <li style="margin-left: -7px;">Altura<br />
                        <asp:TextBox runat="server" ID="tbaltura" CssClass="campoGlicem" ToolTip="Altura da Gestante" style="width:-5px;width: 33px;"></asp:TextBox>
                    </li>

                    <li>Peso (Kg)<br />
                        <asp:TextBox runat="server" ID="tbpeso" Style="width: 45px" CssClass="campoGlicem" ToolTip="Peso da Gestante"></asp:TextBox>
                    </li>

                    <li>IMC<br />
                        <asp:TextBox runat="server" ID="tbimc" Style="width: 40px" ToolTip="Índice de Massa Corporal" CssClass="campoGlicem" MaxLength="3"></asp:TextBox>
                    </li>

                    <li>Pressão Arterial<br />
                        <asp:TextBox runat="server" ID="tbpa" Style="width: 40px" ToolTip="Pressão Arterial" CssClass="campoPressArteri"></asp:TextBox>
                    </li>

                    <li>Bat. Card.(bpm)<br />
                        <asp:TextBox runat="server" ID="tbbcbpm" Style="width: 50px" CssClass="campoGlicem" ToolTip="Batimento cardíaco"></asp:TextBox>
                    </li>

                    <li>Saturação<br />
                        <asp:TextBox runat="server" ID="tbsaturacao2" CssClass="campoGlicem" ToolTip="Saturação" Style="width: 50px !important;"></asp:TextBox>
                    </li>

                    <li>Glicemia<br />
                        <asp:TextBox runat="server" ID="tbglicemia" CssClass="campoGlicem" Style="width: 50px !important;"></asp:TextBox>
                    </li>

                    <li>Leitura Glicemia<br />
                        <asp:DropDownList runat="server" ID="ddlleitura" Width="281px" Height="16px" ToolTip="Informe a Glicemia">
                            <asp:ListItem Text="(NE) Não especificado" Value="N"></asp:ListItem>
                            <asp:ListItem Text="(EJ) Em Jejum" Value="E"></asp:ListItem>
                            <asp:ListItem Text="(PR) Pré-Prandia" Value="P"></asp:ListItem>
                            <asp:ListItem Text="(PO) Pós-Prandial" Value="R"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both"></li>

                    <li style="width: 100%;">
                        <asp:Label runat="server" ID="Label19" Text="REGISTRO PRÉ-NATAL" Font-Bold="false" CssClass="lblSubInfos"></asp:Label><br />
                    </li>

                    <li>EDMA<br />
                        <asp:DropDownList runat="server" ID="ddledma" Style="width: 68px;" ToolTip="EDMA">
                            <asp:ListItem Text="Tipo 001" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Tipo 002" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Tipo 003" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Tipo 004" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Tipo 005" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </li>

                    <li>AU (cm)<br />
                        <asp:TextBox runat="server" ID="tbau" CssClass="campoGlicem" ToolTip="Medição do Feto" Style="width: 50px !important;"></asp:TextBox>
                    </li>

                    <li>BCF (bpm)<br />
                        <asp:TextBox runat="server" ID="tbbcf" CssClass="campoGlicem" ToolTip="Presença dos batimentos cardíacos fetais" Style="width: 50px !important;"></asp:TextBox>
                    </li>

                    <li>Movimentos Fetais<br />
                        <asp:TextBox runat="server" ID="tbmf" CssClass="largurali" ToolTip="Movimentos Fetais"></asp:TextBox>
                    </li>

                    <li>Observação MF<br />
                        <asp:TextBox runat="server" ID="tbobsmf" Width="372px" ToolTip="Observações sobre os Movimentos Fetais"></asp:TextBox>
                    </li>

                    <li style="clear: both"></li>

                    <li style="width: 100%;">

                        <asp:Label runat="server" ID="Label20" Text="REGISTRO ANTROPOMETRIA" Font-Bold="false" CssClass="lblSubInfos"></asp:Label><br />

                    </li>
                    <br />
                    <br />

                    <li>PC (cm)<br />
                        <asp:TextBox runat="server" ID="tbpc" CssClass="largurali campoAltu" ToolTip="Perímetro Cefálico"></asp:TextBox>
                    </li>

                    <li>Peso (Kg)<br />
                        <asp:TextBox runat="server" ID="tbpesoantropometria" CssClass="largurali campoAltu" ToolTip="Peso atual" style="width:50px;"></asp:TextBox>
                    </li>

                    <li>Altura (cm)<br />
                        <asp:TextBox runat="server" ID="tbautura" CssClass="largurali campoAltu" ToolTip="Altura atual" style="width:50px;"></asp:TextBox>
                    </li>

                    <li>PP (cm)<br />
                        <asp:TextBox runat="server" ID="tbpp" CssClass="campoAltu largurali" ToolTip="Placenta Prévia" style="width:50px;"></asp:TextBox>
                    </li>

                    <li>IMC<br />
                        <asp:TextBox runat="server" ID="tbimcF" CssClass="campoAltu largurali" ToolTip="Índice de massa corporal" style="width:50px;"></asp:TextBox>
                    </li>

                    <li>Observação Antropometria<br />
                        <asp:TextBox runat="server" ID="tbobsantropometria" Width="325px" ToolTip="Observação Antropometria"></asp:TextBox>
                    </li>

                    <li style="clear: both"></li>
                    <li style="width: 100%;">                        
                        <asp:Label runat="server" ID="Label21" Text="REGISTRO DE PROBLEMAS E CONDIÇÕES ATIVAS" Font-Bold="false" CssClass="lblSubInfos"></asp:Label><br />

                    </li>
                    <br />
                    <br />
                    <li>Tipo Registro<br />
                        <asp:DropDownList ID="ddltiporegistro" runat="server" Width="114px">
                            <asp:ListItem Text="Tipo 001" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Tipo 002" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Tipo 003" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Tipo 004" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Tipo 005" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </li>

                    <li>Dados do Registro<br />
                        <asp:TextBox runat="server" ID="tbdataregistro" CssClass="largurali" Style="width: 90px; !important;" ToolTip="Dados do Registro"></asp:TextBox>
                    </li>

                    <li>Idade da Gestante<br />
                        <asp:TextBox runat="server" ID="tbidadegestante" CssClass="largurali" Style="width: 83px; !important;" ToolTip="Idade da Gestante"></asp:TextBox>
                    </li>

                    <li>Código<br />
                        <asp:DropDownList runat="server" ID="ddlcodigo" Style="width: 52px; !important;" ToolTip="Código">
                            <asp:ListItem Text="Código 1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Código 2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Código 3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Código 4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Código 5" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </li>

                    <li class="divtexto">Descrição Complemento<br />
                        <asp:TextBox runat="server" ID="tbobservacaocomplemento" Width="335px" ToolTip="Descrição Complementar"></asp:TextBox>
                    </li>
                    <li style="clear: both"></li>
                </ul>


                <br />
                <br />
                <br />
                <br />
                <div id="botoes" style="height: 0px; margin-left: 20px; margin-top: 170px;">
 
                    <div>
                        <asp:Button runat="server" ID="btnhistorico" Text="HISTÓRICO DE MEDIÇÕES" Style="background-color: #a7c9d5; border-style: none; float: left; font-family: Trebuchet MS; background-color: #a7c9d5; border-style: none; float: left; font-family: Trebuchet MS; margin-left: -166px; height: 31px !important; width: 142px;" />
                    </div>
                    <div style="width: 160px; float: left;">
                        <asp:Button runat="server" ID="btnproblemas" Text="PROBLEMAS E CONDIÇÕES" Style="background-color: #a7c9d5; border-style: none; float: left; font-family: Trebuchet MS; height: 31px !important; width: 130px !important;" />
                    </div>
                    <div style="width: 200px; float: left;">
                        <asp:Button runat="server" ID="btnresultados" Text="RESULTADO DE EXAMES" Style="background-color: #a7c9d5; border-style: none; float: left; font-family: Trebuchet MS; margin-left: 23px; width: 130px !important; height: 31px !important;" />
                    </div>

                    <div style="width: 20px; float: left;">
                        <asp:Button runat="server" ID="Button1" Text="SALVAR" Style="background-color: #ffd700; border-style: none; float: left; font-family: Trebuchet MS; font-weight: bold; height: 30px !important; width: 165px !important;" OnClick="Button1_Click" />
                    </div>

                </div>
            </div>
            </ul>
    </div>
        <!-- FIM ------------------------------------------------------------------------------------------------------------------------------------- INICIO MODAL GESTANTE -->
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
            $('#divLoadShowLogAgenda').dialog({
                autoopen: false, modal: true, width: 902, height: 340, resizable: false, title: "HISTÓRICO DO AGENDAMENTO DE ATENDIMENTO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
        function AbreModalInfosGestante() {
            $('#divLoadInfosGestante').dialog({
                autoopen: false, modal: true, width: 810, height: 405, resizable: false, title: "GESTANTE - CADASTRO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalInfosSigtap() {
            $('#divLoadInfosSigtap').dialog({
                autoopen: false, modal: true, width: 810, height: 420, resizable: false, title: "CÓDIGO PROCEDIMENTO - PESQUISA",
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
                autoopen: false, modal: true, width: 530, height: 370, resizable: false, title: "EMISSÃO DE DOCUMENTO",
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

        function AbreModalProntuCon() {
            $('#divProntuCon').dialog({
                autoopen: false, modal: true, width: 750, height: 455, top: 98, left: 175, resizable: false, title: "PRONTUÁRIO CONVENCIONAL",
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

        function AbreModalAmbulatorio() {
            $('#divAmbulatorio').dialog({
                autoopen: false, modal: true, width: 900, height: 300, resizable: false, title: "PROCEDIMENTOS AMBULATORIAIS DO PACIENTE NESTE ATENDIMENTO",
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
                    width: "35px",
                    margin: "-20px 0 0 94px",
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


    </script>
</asp:Content>
