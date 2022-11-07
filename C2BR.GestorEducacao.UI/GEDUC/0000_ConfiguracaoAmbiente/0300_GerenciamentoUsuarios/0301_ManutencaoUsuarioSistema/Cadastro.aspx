<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0301_ManutencaoUsuarioSistema.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 720px;
            margin: 30px 0 0 230px !important;
        }
        input
        {
        }
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        .liFotoMUS
        {
            margin-top: -269px;
            margin-left: 427px;
        }
        .fldFotoMUS
        {
            height: 105px;
            width: 86px;
        }
        .liTopMUS
        {
            margin-top: 10px;
        }
        .liG1MUS
        {
            margin-left: 10px;
        }
        .liG3ClearMUS
        {
            margin-top: 10px;
            clear: both;
        }
        .liG3MUS
        {
            margin-top: 10px;
            margin-left: 30px;
        }
        .liAcessosPermitidosMUS
        {
            margin-top: 10px;
            clear: both;
            width: 362px;
        }
        .liAP1MUS
        {
            width: 118px;
            margin-right: -1px !important;
        }
        #ulAPPrincipalMUS li
        {
            margin-bottom: -3px;
        }
        .liServsMUS
        {
            width: 150px;
            float: left;
            margin-top: 16px;
            margin-left: -2px;
        }
        #ulServsMUS li
        {
            margin-bottom: 3px;
            margin-left: -3px;
        }
        #ulAPCabInternMUS li
        {
            width: 17px;
        }
        
        /*--> CSS DADOS */
        .cbDiaAcessoMUS
        {
            border: none;
            margin-top: -2px;
        }
        .cbDiaAcessoMUS tr td label
        {
            margin-left: -7px;
            display: inline-table;
            font-size: 9px;
        }
        .ddlTpUsuMUS
        {
            width: 85px;
        }
        .chkLocaisMUS label
        {
            display: inline !important;
            margin-left: -4px;
        }
        #ulServsMUS
        {
            margin-left: -1px;
            padding-top: 4px;
        }
        .cbDiaAcessoMUS input[type="checkbox"]
        {
            width: 20px !important;
        }
        .ddlUsuaCaixaMUS
        {
            width: 40px;
        }
        .txtQtdSMSMes
        {
            width: 35px;
            text-align: right;
        }
        .txtInstituicao
        {
            width: 265px;
            padding-left: 2px;
        }
        /*--> CSS LIs */
        .liFotoColab
        {
            float: left !important;
            margin-right: 5px !important; /*margin-right: 10px !important;*/
        }
        /*--> CSS DADOS */
        .fldFotoColab
        {
            margin-left: -3px;
            border: none;
            width: 90px;
            height: 122px;
            border: 1px solid #DDDDDD !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtInstituicao" title="Instituição">
                Instituição</label>
            <asp:TextBox ID="txtInstituicao" BackColor="#FFFFE1" runat="server" Enabled="false"
                CssClass="txtInstituicao" ToolTip="Login de Acesso">
            </asp:TextBox>
        </li>
        <li style="margin-left: 10px;">
            <label for="txtUnidade" title="Unidade/Escola">
                Unidade de Origem</label>
            <asp:DropDownList ID="ddlUnidadeMUS" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidadeMUS_SelectedIndexChanged"
                CssClass="campoUnidadeEscolar" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top: 10px;">
            <label class="lblObrigatorio">
                Login de Acesso</label>
            <asp:TextBox ID="txtLoginMUS" runat="server" Width="100px" MaxLength="20" ToolTip="Login de Acesso">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLoginMUS"
                ErrorMessage="Login deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 15px; margin-top: 10px;">
            <label>
                Tipo de Usuário</label>
            <asp:DropDownList ID="ddlTpUsuMUS" runat="server" AutoPostBack="true" ToolTip="Selecione o Tipo de Usuário"
                CssClass="ddlTpUsuMUS" OnSelectedIndexChanged="ddlTpUsuMUS_SelectedIndexChanged" Width="93px">
                <asp:ListItem Text="Funcionário" Value="F" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Professor" Value="P"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-top: 10px; margin-left: 15px;">
            <label>
                Categoria de Usuário</label>
            <asp:DropDownList ID="ddlClaUsuMUS" runat="server" ToolTip="Selecione o Tipo de Usuário">
                <asp:ListItem Text="Comum" Value="C" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Master" Value="M"></asp:ListItem>
                <asp:ListItem Text="Suporte" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left: 15px; margin-top: 10px;">
            <label>
                Usr Caixa</label>
            <asp:DropDownList ID="ddlUsuaCaixaMUS" Width="50px" runat="server" CssClass="ddlUsuaCaixaMUS"
                ToolTip="Selecione se Usuário Caixa">
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left: 10px; margin-top: 8px;">
            <label>
                Usr Cobrança/%</label>
            <asp:DropDownList ID="drpUsrCobranca" Width="50px" runat="server" CssClass="ddlUsuaCaixaMUS"
                ToolTip="Selecione se Usuário Cobrança">
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtPercCobranca" runat="server" CssClass="campoPrcnt" Width="32px" Height="12px" style="margin-top:2px;" />
        </li>
        <li style="margin-left: 15px; margin-top: 10px;">
            <label title="Quantidade Máxima SMS Mês">
                Qtd SMS Mês</label>
            <asp:TextBox ID="txtQtdSMSMes" Enabled="false" CssClass="txtQtdSMSMes" runat="server"
                ToolTip="Informe a quantidade máxima de SMS no mês">
            </asp:TextBox>
        </li>
        <li style="float: left;">
            <ul>
                <li class="liG3ClearMUS">
                    <label class="lblObrigatorio">
                        Nome completo do Usuário</label>
                    <asp:DropDownList ID="ddlColMUS" runat="server" CssClass="campoNomePessoa" AutoPostBack="true" OnSelectedIndexChanged="ddlColMUS_SelectedIndexChanged" ToolTip="Selecione o Colaborador">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlColMUS"
                        ErrorMessage="Usuário deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liG3MUS">
                    <label>
                        Apelido</label>
                    <asp:TextBox ID="txtApelidoMUS" runat="server" Enabled="false" Width="100px" ToolTip="Apelido do Colaborador">
                    </asp:TextBox>
                </li>
                <li class="liClear">
                    <label>
                        Localização</label>
                    <asp:TextBox ID="txtDepartamentoMUS" Width="160px" runat="server" Enabled="false"
                        ToolTip="Departamento do Colaborador">
                    </asp:TextBox>
                </li>
                <li style="margin-left: 68px; width: 264px;">
                    <label>
                        Atividade</label>
                    <asp:TextBox ID="txtFuncaoMUS" runat="server" Enabled="false" Width="258px" ToolTip="Função do Colaborador"></asp:TextBox>
                </li>
                <li class="liClear">
                    <label>
                        Email</label>
                    <asp:TextBox ID="txtEmailMUS" runat="server" CssClass="txtEmail" Enabled="false"
                        ToolTip="E-Mail do Colaborador">
                    </asp:TextBox>
                </li>
                <li class="liG1MUS" style="margin-left: 27px;">
                    <label>
                        Celular</label>
                    <asp:TextBox ID="txtCelularMUS" runat="server" Enabled="false" CssClass="campoTelefone"
                        ToolTip="Celular do Colaborador" Width="80px"></asp:TextBox>
                </li>
                <li class="liG1MUS">
                    <label>
                        Telefone</label>
                    <asp:TextBox ID="txtTelefoneMUS" runat="server" Enabled="false" CssClass="campoTelefone"
                        ToolTip="Telefone do Colaborador" Width="75px"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li class="liFotoColab" style="float: right; margin-left: 20px;">
            <fieldset class="fldFotoColab">
                <uc1:ControleImagem ID="upImagemColab" runat="server" />
            </fieldset>
        </li>
        <li class="liAcessosPermitidosMUS">
            <fieldset>
                <legend>&nbsp;Acessos Permitidos&nbsp;</legend>
                <ul id="ulAPPrincipalMUS">
                    <li style="margin-bottom: 4px; color: black; font-weight: bold; margin-right: 0 !important;">
                        <ul id="ulAPCabInternMUS">
                            <li style="width: 114px; text-align: center">
                                <label>
                                    LOCAIS</label>
                            </li>
                            <li>
                                <label>
                                    SEG</label>
                            </li>
                            <li>
                                <label>
                                    TER</label>
                            </li>
                            <li style="margin-left: -2px;">
                                <label>
                                    QUA</label>
                            </li>
                            <li style="margin-left: 2px;">
                                <label>
                                    QUI</label>
                            </li>
                            <li>
                                <label>
                                    SEX</label>
                            </li>
                            <li>
                                <label>
                                    SAB</label>
                            </li>
                            <li>
                                <label>
                                    DOM</label>
                            </li>
                            <li style="width: 75px; text-align: center; margin-left: 5px;">
                                <label>
                                    HORÁRIO</label>
                            </li>
                        </ul>
                    </li>
                    <li style="margin-right: 0px;">
                        <ul>
                            <li class="liAP1MUS">
                                <asp:CheckBox CssClass="chkLocaisMUS" runat="server" TextAlign="Right" Text="Portal Educação"
                                    ID="chkGestorEducacao" Checked="true" />
                            </li>
                            <li>
                                <asp:CheckBoxList ID="cbDiaAcessoMUS" runat="server" RepeatColumns="7" CssClass="cbDiaAcessoMUS"
                                    ToolTip="Marque a opção desejada" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="" Value="SG"></asp:ListItem>
                                    <asp:ListItem Text="" Value="TR"></asp:ListItem>
                                    <asp:ListItem Text="" Value="QR"></asp:ListItem>
                                    <asp:ListItem Text="" Value="QN"></asp:ListItem>
                                    <asp:ListItem Text="" Value="SX"></asp:ListItem>
                                    <asp:ListItem Text="" Value="SB"></asp:ListItem>
                                    <asp:ListItem Text="" Value="DG"></asp:ListItem>
                                </asp:CheckBoxList>
                            </li>
                            <li>
                                <asp:TextBox ID="txtHoraAcessoIMUS" runat="server" Width="30px" CssClass="campoHora"
                                    ToolTip="Informe a Hora de Acesso Inicio"></asp:TextBox>
                                <span>A</span>
                                <asp:TextBox ID="txtHoraAcessoFMUS" runat="server" Width="30px" CssClass="campoHora"
                                    ToolTip="Informe a Hora de Acesso Final"></asp:TextBox>
                            </li>
                        </ul>
                    </li>
                    <li style="margin-right: 0px;">
                        <ul>
                            <li class="liAP1MUS">
                                <asp:CheckBox CssClass="chkLocaisMUS" runat="server" TextAlign="Right" Enabled="true"
                                    Text="Mobile Phone" ID="chkMobilePhone" />
                            </li>
                            <li>
                                <asp:CheckBoxList Enabled="False" ID="CheckBoxList1" runat="server" RepeatColumns="7"
                                    CssClass="cbDiaAcessoMUS" ToolTip="Marque a opção desejada" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="" Value="SG"></asp:ListItem>
                                    <asp:ListItem Text="" Value="TR"></asp:ListItem>
                                    <asp:ListItem Text="" Value="QR"></asp:ListItem>
                                    <asp:ListItem Text="" Value="QN"></asp:ListItem>
                                    <asp:ListItem Text="" Value="SX"></asp:ListItem>
                                    <asp:ListItem Text="" Value="SB"></asp:ListItem>
                                    <asp:ListItem Text="" Value="DG"></asp:ListItem>
                                </asp:CheckBoxList>
                            </li>
                            <li>
                                <asp:TextBox ID="TextBox1" runat="server" Enabled="false" Width="30px" CssClass="campoHora"
                                    ToolTip="Informe a Hora de Acesso Inicio"></asp:TextBox>
                                <span>A</span>
                                <asp:TextBox ID="TextBox2" runat="server" Enabled="false" AutoPostBack="True" Width="30px"
                                    CssClass="campoHora" ToolTip="Informe a Hora de Acesso Final"></asp:TextBox>
                            </li>
                        </ul>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li class="liServsMUS">
            <fieldset style="height: 170px; width: 273px;">
                <ul id="ulServsMUS">
                    <li>
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkBiometriaMUS" TextAlign="Right" runat="server"
                            Enabled="false" Text="Faz Captura de Digitais" />
                    </li>
                    <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkFreqManuMUS" TextAlign="Right" runat="server"
                            Text="Faz Manutenção de Frequência Funcional" />
                    </li>
                    <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkBibliReservaMUS" TextAlign="Right" runat="server"
                            Text="Faz Reserva de Itens de Biblioteca" />
                    </li>
                    <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkAltLoginSMSMUS" TextAlign="Right" runat="server"
                            Text="Avisar Alteração de Login via SMS" />
                    </li>
                    <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkSerEnvioSMSMUS" TextAlign="Right" runat="server"
                            Text="Utilizar Serviço de Mensagens SMS" />
                    </li>
                     <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkPermAgendaMult" TextAlign="Right" runat="server"
                            Text="Permite agendamento múltiplo" />
                    </li>
                    <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkPermMovLocal" TextAlign="Right" runat="server"
                            Text="Permite movimentação de local/atendimento" />
                    </li>
                       <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkPermFinalizarRecepcao" TextAlign="Right" runat="server"
                            Text="Permite finalização de atendimento (Recepção)" />
                    </li>
                       <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkPermReverter" TextAlign="Right" runat="server"
                            Text="Permite reversao de baixa do paciente para atendimento" />
                    </li>
                    <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkAltPagtoMatric" TextAlign="Right" runat="server"
                            Text="Faz Registro de Pagto Matrícula" />
                    </li>
                    <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkAltParamMatric" TextAlign="Right" runat="server"
                            Text="Faz Alteração de Parâmetros de Matrícula" />
                    </li>
                    <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkAltBolsaAlu" TextAlign="Right" runat="server"
                            Text="Faz Manutenção de Desconto/Bolsa de Matrícula" />
                    </li>
                    <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkAltBolsaEspecAlu" TextAlign="Right"
                            runat="server" Text="Faz Manutenção de Desconto/Especial de Matrícula" />
                    </li>
                      <li class="liClear">
                        <asp:CheckBox CssClass="chkLocaisMUS" ID="chkFazLancMultiNotas" TextAlign="Right"
                            runat="server" Text="Faz lançamento múltiplo de Notas" />
                    </li>
                </ul>
            </fieldset>
        </li>
        <li style="clear: both; margin-top: -28px;">
            <ul>
                <li>
                    <label for="ddlStatus">
                        Status</label>
                    <asp:DropDownList ID="ddlStatusMUS" runat="server" Width="60px" ToolTip="Selecione o Status">
                        <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liG1MUS">
                    <label>
                        Data Status</label>
                    <asp:TextBox ID="txtDtSituacaoMUS" Enabled="False" runat="server" CssClass="campoData"
                        ToolTip="Informe a Data de Situação"></asp:TextBox>
                </li>
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".campoHora").mask("99:99");
            $(".txtQtdSMSMes").mask("?999");
            $(".campoCep").mask("?99999-999");
            $(".campoTelefone").mask("(99) 9999-9999?9");
            $(".campoPrcnt").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>
