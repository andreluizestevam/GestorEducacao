﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5100_Recebimento._5102_Receber.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .campoHora
        {
            font-weight:bold;
            display:inline;
            width: 30px;
        }
        .ulDados
        {
            width: 990px;
        }
        input
        {
            height: 13px !important;
        }
        .lblTitInf
        {
            text-transform: uppercase;
            font-weight: bold;
            font-size: 1.0em;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
        .lblTop
        {
            font-size: 9px;
            margin-bottom: 6px;
            color: #436EEE;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            padding: 2px 3px 1px 3px;
        }
        .divGridData
        {
            overflow-y: scroll;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
        .liTituloGrid
        {
            height: 15px;
            width: 100px;
            text-align: center;
            padding: 5 0 5 0;
            clear: both;
        }
        .lachk label
        {
            display: inline;
            margin-left: -3px;
        }
        .pAcesso
        {
            font-size: 1.1em;
            color: #4169E1;
        }
        .liBtnGrdFinan
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
            width:10px;
        }
        .pFechar
        {
            font-size: 0.9em;
            color: #FF6347;
            margin-top: 2px;
        }
        #divListarResponsaveisContent
        {
            height: 261px;
            overflow-y: auto;
            border: 1px solid #EBF0FB;
        }
        .liFoto
        {
            float: left !important;
            margin-right: 0 !important;
        }
        .fldFotoAluno
        {
            border: none;
            width: 80px;
            height: 100px;
        }
        .divCarregando
        {
            width: 100%;
            text-align: center;
            position: absolute;
            z-index: 9999;
            left: 50px;
            top: 40%;
        }
        .chkLocais label
        {
            margin-left: -3px;
            display: inline !important;
        }
        .DivResp
        {
            float: left;
            width: 500px;
            height: 207px;
        }
        .chk label
        {
            display: inline;
        }
        .ulDadosResp li
        {
            margin-top: -2px;
            margin-left: 5px;
        }
        .ulDadosPaciente li
        {
            margin-left: 0px;
        }
        .ulDadosResp
        {
            float: left;
        }
        .ulIdentResp li
        {
            margin-left: 0px;
        }
        .ulInfosGerais
        {
            margin-top: -3px;
        }
        .ulInfosGerais li
        {
            margin: 1px 0 3px 0px;
        }
        .lblSubInfos
        {
            color: Orange;
            font-size: 8px;
        }
        .ulEndResiResp
        {
        }
        .ulEndResiResp li
        {
            margin-left: 2px;
        }
        .ulDadosContatosResp li
        {
            margin-left: 0px;
        }
        .liFotoColab
        {
            float: left !important;
            margin-right: 10px !important;
            border: 0 none;
        }
        /*--> CSS DADOS */
        .fldFotoColab
        {
            border: none;
            width: 90px;
            height: 108px;
        }
        .successMessageSMS
        {
            background: url(/Library/IMG/Gestor_ImgGood.png) no-repeat scroll center 10px;
            font-size: 15px;
            font-weight: bold;
            margin: 25% auto 13% auto;
            padding: 35px 10px 10px;
            text-align: center;
            width: 220px;
        }
        
        .divValidationSummary
        {
            cursor: move;
            padding: 10px;
            display: none;
            width: 210px;
            position: absolute;
            top: 35px;
            left: 0;
        }
        .divValidationSummary .divButtons
        {
            text-align: right;
        }
        .divValidationSummary .vsValidation
        {
            margin-left: 10px;
        }
        .divValidationSummary #divValidationSummaryContent
        {
            margin-bottom: 10px;
        }
        .divValidationSummary li
        {
            color: #666666;
            line-height: 11px;
            border-bottom: 1px solid #CFCFCF;
            list-style-type: circle;
            padding-bottom: 2px;
        }
          .divEsquePgto
        {
            border-right: 1px solid #CCCCCC;
            margin-top:15px;
            margin-right:20px !Important;
            width: 165px;
            height: 180px;
            float: left;
        }
        .divDirePgto
        {
            <%--border: 1px solid #CCCCCC;--%>;
            margin-top:15px;
            width: 550px;
            height: 180px;
            float: right;
        }
        .divGrdChequePgto
        {
            border: 1px solid #CCCCCC;
            width: 720px;
            height: 106px;
            overflow-y: scroll;
        }
        .lblchkPgto
        {
            font-weight:bold;
            color: #FFA07A;
            margin-left:-5px;
        }
        .ulReceb li 
        {
            margin-top:-6px;
        }
        .chk label
        {
            display:inline;
            margin-left:-4px;
        }
        .chkDestaque label
        {
            font-weight:bold;
            color: #FFA07A;
            margin-left:-4px;
            display:inline;
        }
        .sitPacPadrao
        {
            color:Blue;
            margin-left:1px;
        }
        .sitPacAnalise
        {
            font-weight:bold;
            color:Black;
            margin-left:1px;
        }
        .sitPacAlta
        {
            font-weight:bold;
            color:Red;
            margin-left:1px;
        }
        .fixedHeader
        {
            position:absolute;
            font-weight:bold;
        }
        
        input[type="text"]
        {
            font-size: 10px !important;
            font-family: Arial !important;
        }
        select
        {
            margin-bottom: 4px;
            font-family: Arial !important;
            border: 1px solid #BBBBBB !important;
            font-size: 0.9em !important;
            height: 15px !important;
        }
        .liBtnEmt
        {
            background-color: #04B486;
            border: 1px solid #D2DFD1;
            padding: 0px 8px 1px 8px;
            margin-top: -20px;
            margin-left: 826px;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }
        input[type='text']
        {
            margin-bottom: 4px;
        }
        label
        {
            margin-bottom: 1px;
        }
        .ulDados
        {
            width: 1003px;
            margin-top: -15px !important;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .chk label
        {
            display:inline;
            margin-left:-4px
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="width: 970px; height: 30px; margin-top: 0px !important;">
        <li style="margin: 10px 13px 4px 6px;"><a class="lnkPesPaci" href="#">
            <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                style="width: 17px; height: 17px;" />
        </a></li>
        <li style="margin: 11px 8px 0px -5px;">
            <asp:ImageButton ID="imgCadPac" runat="server" ImageUrl="~/Library/IMG/PGN_IconeTelaCadastro2.png"
                OnClick="imgCadPac_OnClick" Style="width: 18px !important; height: 17px !important;"
                ToolTip="Cadastro de Pacientes" ValidationGroup="nenhum" />
        </li>
        <li>
            <label title="Paciente" class="lblObrigatorio">
                Paciente</label>
            <asp:DropDownList ID="drpPacienteReceb" Visible="false" OnSelectedIndexChanged="drpPacienteReceb_OnSelectedIndexChanged"
                AutoPostBack="true" runat="server" Width="230px" />
            <asp:TextBox ID="txtNomePacPesq" Width="230px" ValidationGroup="pesqPac" ToolTip="Digite o nome ou parte do nome do paciente"
                runat="server" />
            <asp:RequiredFieldValidator runat="server" ID="rfvPacienteReceb" CssClass="validatorField"
                ErrorMessage="O campo data é requerido" ControlToValidate="drpPacienteReceb"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 11px; margin-left: -4px;">
            <asp:ImageButton ID="imgbPesqPacNome" ValidationGroup="pesqPac" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" />
            <asp:ImageButton ID="imgbVoltarPesq" ValidationGroup="pesqPac" Width="16px" Height="16px"
                ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgbVoltarPesq_OnClick" Visible="false"
                runat="server" />
        </li>
        <li style="margin-right: 20px;">
            <label title="Responsável">
                Responsável</label>
            <asp:TextBox runat="server" ID="txtResponsavelReceb" Width="165px" Enabled="false"></asp:TextBox>
        </li>
        <li>
            <label>
                Tipo</label>
            <asp:DropDownList ID="drpTipoRecebimento" Width="87px" runat="server">
                <asp:ListItem Value="-" Text="" Selected="True" />
                <asp:ListItem Value="C" Text="Consulta" />
                <asp:ListItem Value="P" Text="Procedimentos" />
                <asp:ListItem Value="S" Text="Serviços" />
                <asp:ListItem Value="E" Text="Exames Internos" />
                <asp:ListItem Value="O" Text="Outros" />
                <asp:ListItem Value="EE" Text="Exames Externos" />
            </asp:DropDownList>
        </li>
        <li>
            <label title="Número do RAP">
                Nº RAP</label>
            <asp:DropDownList ID="drpRAP" Width="200px" runat="server" OnSelectedIndexChanged="CarregaValorTotal"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="margin: 12px 5px 0 0;">
            <asp:ImageButton ID="imgbListaRap" runat="server" ImageUrl="~/Library/IMG/PGS_CentralRegulacao_Icone_EmissaoGuia.png"
                Style="width: 18px !important; height: 17px !important;" OnClick="imgbListaRap_OnClick"
                ToolTip="Abre uma janela com as informações das RAPs" ValidationGroup="nenhum" />
        </li>
        <li style="margin-right: 18px;">
            <label title="Valor Total" class="lblObrigatorio">
                Valor Total</label>
            <asp:TextBox runat="server" CssClass="campoDin" ID="txtVlrReceb" Width="50px" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvVlrReceb" CssClass="validatorField"
                ClientIDMode="Static" ErrorMessage="O campo data é requerido" ControlToValidate="txtVlrReceb"></asp:RequiredFieldValidator>
        </li>
        <li>
            <asp:HiddenField ID="hidAgendReceb" runat="server" />
            <asp:HiddenField ID="hidAgendAvalReceb" runat="server" />
            <asp:HiddenField ID="hidIdRecebimento" runat="server" />
            <label title="Data Comparecimento" class="lblObrigatorio">
                Data Receb</label>
            <asp:TextBox ID="txtDtReceb" runat="server" class="campoData" ToolTip="Informe a data de comparecimento"
                AutoPostBack="true" OnTextChanged="txtDtReceb_OnTextChanged" Width="58px" />
            <asp:RequiredFieldValidator runat="server" ID="rfvDtReceb" CssClass="validatorField"
                ErrorMessage="O campo data é requerido" ControlToValidate="txtDtReceb"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <ul class="ulDados" style="width: 955px; height: 100px; margin-top: 5px !important;">
        <li style="width: 100%; height: 19px !important; background-color: #9AFF9A; text-align: center;
            margin-bottom: 5px;">
            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; float: left;
                margin-left: 5px;">
                FORMA DE RECEBIMENTO</label>
            <asp:LinkButton ID="lnkbRecepcao" runat="server" OnClick="lnkbRecepcao_OnClick" ValidationGroup="voltarRecepcao"
                ToolTip="Voltar à Recepção" Style="float: right; margin-top: 3px; margin-right: 6px;
                color: black; font-weight: bold;">
                Voltar à Recepção
            </asp:LinkButton>
        </li>
        <li>
            <label title="Atendimento de Cortesia (Sem Valor)" style="margin-top: 2px; margin-left: 35px;
                float: right; margin-right: 18px;">
                <asp:CheckBox ID="chkCortesia" Style="margin: 0 -5px 0 -5px;" runat="server" ToolTip="Atendimento de Cortesia (Sem Valor)"
                    ClientIDMode="Static" />Cortesia
            </label>
            <label style="float: right; margin-top: 2px; margin-left: 35px; margin-right: -15px;">
                Plano
                <asp:DropDownList ID="drpPlanoReceb" runat="server" Width="100px" />
            </label>
            <label style="float: right; margin-top: 2px; margin-right: -25px;">
                Contratação
                <asp:DropDownList ID="drpContratacao" runat="server" OnSelectedIndexChanged="drpContratacao_OnSelectedIndexChanged"
                    AutoPostBack="true" Width="100px" />
            </label>
        </li>
        <li style="float: right; margin-top: 0px; padding-left: 20px; height: 80px; width: 167px;
            border-left: 1px solid #CCCCCC;">
            <asp:CheckBox ID="chkNotaFiscal" Style="margin: 0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />
            <strong style="color: Blue;">EMITIR NOTA FISCAL</strong>
            <label style="border-bottom: 1px solid #CCCCCC; margin-bottom: -18px; margin-top: 7px;
                height: 25px;">
                Nº
                <asp:TextBox ID="txtNotaFiscal" Width="50" runat="server" ToolTip="Nº da nota fiscal emitida para o Responsável/Paciente"
                    ClientIDMode="Static" />
                Data
                <asp:TextBox ID="txtDtNotaFiscal" runat="server" class="campoData" ToolTip="Informe a data de comparecimento" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="validatorField"
                    ErrorMessage="O campo data é requerido" ControlToValidate="txtDtNotaFiscal"></asp:RequiredFieldValidator>
            </label>
        </li>
        <li style="clear: both; padding-right: 15px; padding-left: 0px; border-right: 1px solid #CCCCCC;
            margin-top: -57px">
            <label title="Dinheiro" style="margin-bottom: 2px;">
                <asp:CheckBox ID="chkDinheiro" Style="margin: 0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Dinheiro
            </label>
            <asp:TextBox runat="server" CssClass="campoDin" ID="txtDinheiro" Width="50px" ToolTip="Valor Total em Dinheiro"
                ClientIDMode="Static" />
        </li>
        <li style="padding-right: 15px; padding-left: 10px; border-right: 1px solid #CCCCCC;
            margin-top: -57px; margin-left: 72px;">
            <label title="Cheque" style="margin-bottom: 2px;">
                <asp:CheckBox ID="chkCheque" Style="margin: 0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Cheque
            </label>
            <asp:TextBox runat="server" ID="txtCheque" CssClass="campoDin" Width="50px" ToolTip="Valor Total em Cheque"
                ClientIDMode="Static" />
            /
            <asp:TextBox runat="server" ID="txtParcelasCheque" MaxLength="3" Width="20px" ToolTip="Quantidade de Cheques"
                ClientIDMode="Static" />
        </li>
        <li style="padding-right: 15px; padding-left: 10px; border-right: 1px solid #CCCCCC;
            margin-top: -57px; margin-left: 186px;">
            <label title="Cartão de Débito" style="margin-bottom: 2px;">
                <asp:CheckBox ID="chkDebito" Style="margin: 0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Débito
            </label>
            <asp:TextBox runat="server" ID="txtDebito" CssClass="campoDin" Width="50px" ToolTip="Valor Total em Cartão de Débito"
                ClientIDMode="Static" />
            /
            <asp:TextBox runat="server" ID="txtParcelasDebito" MaxLength="3" Width="20px" ToolTip="Quantidade de Cartões"
                ClientIDMode="Static" />
        </li>
        <li style="padding-right: 15px; padding-left: 10px; border-right: 1px solid #CCCCCC;
            margin-top: -57px; margin-left: 300px;">
            <label title="Cartão de Crédito" style="margin-bottom: 2px;">
                <asp:CheckBox ID="chkCredito" Style="margin: 0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Crédito
            </label>
            <asp:TextBox runat="server" ID="txtCredito" CssClass="campoDin" Width="50px" ToolTip="Valor Total em Cartão de Crédito"
                ClientIDMode="Static" />
            /
            <asp:TextBox runat="server" ID="txtParcelasCredito" MaxLength="3" Width="20px" ToolTip="Quantidade de Parcelas"
                ClientIDMode="Static" />
        </li>
        <li style="padding-right: 15px; padding-left: 10px; border-right: 1px solid #CCCCCC;
            margin-top: -57px; margin-left: 414px;">
            <label title="Transferência" style="margin-bottom: 2px;">
                <asp:CheckBox ID="chkTransferencia" Style="margin: 0 -5px 0 -5px;" runat="server"
                    ClientIDMode="Static" />Transfer
            </label>
            <asp:TextBox runat="server" ID="txtTransferencia" CssClass="campoDin" Width="50px"
                ToolTip="Valor Total Transferido" ClientIDMode="Static" />
        </li>
        <li style="padding-right: 15px; padding-left: 10px; border-right: 1px solid #CCCCCC;
            margin-top: -57px; margin-left: 496px;">
            <label title="Deposíto" style="margin-bottom: 2px;">
                <asp:CheckBox ID="chkDeposito" Style="margin: 0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Deposíto
            </label>
            <asp:TextBox runat="server" ID="txtDeposito" CssClass="campoDin" Width="50px" ToolTip="Valor Total Depositado"
                ClientIDMode="Static" />
        </li>
        <li style="padding-right: 15px; padding-left: 10px; border-right: 1px solid #CCCCCC;
            margin-top: -57px; margin-left: 578px;">
            <label title="Boleto" style="margin-bottom: 2px;">
                <asp:CheckBox ID="chkBoleto" Style="margin: 0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Boleto
                Bancário
            </label>
            <asp:TextBox runat="server" ID="txtBoleto" CssClass="campoDin" Width="50px" ToolTip="Valor Total em Boleto Bancário"
                ClientIDMode="Static" />
            /
            <asp:TextBox runat="server" ID="txtParcelasBoleto" MaxLength="3" Width="20px" ToolTip="Quantidade de Boletos"
                ClientIDMode="Static" />
        </li>
        <li style="padding-left: 10px; padding-right: 15px; margin-top: -57px; margin-left: 692px;">
            <label title="Outros" style="margin-bottom: 2px;">
                <asp:CheckBox ID="chkOutros" Style="margin: 0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Outros
            </label>
            <asp:TextBox runat="server" ID="txtOutros" CssClass="campoDin" Width="50px" ToolTip="Valor Recebido em Outras Formas"
                ClientIDMode="Static" />
        </li>
        <li style="clear: both; margin-top: -10px; margin-top: -16px;">Observações
            <asp:TextBox ID="txtObsReceb" Width="694" runat="server" />
        </li>
        <li class="liBtnEmt">
            <asp:LinkButton ID="lnkbEmitirRecibo" runat="server" Text="EMITIR RECIBO" ForeColor="White"
                OnClick="lnkbEmitirRecibo_Click" ToolTip="Imprimir o recibo do recebimento">
            </asp:LinkButton>
        </li>
    </ul>
    <!--Pagamento Debito-->
    <ul class="ulDados" style="clear: both; width: 955px; height: 100px; margin-top: 10px !important;">
        <li style="background-color: #ADD8E6; width: 955px; margin-bottom: 3px; text-align: center;">
            <label style="color: Black;">
                DETALHES DO RECEBIMENTO DE CARTÕES E CHEQUES</label>
        </li>
        <li>
            <ul style="width: 955px;">
                <li style="height: 20px !important; width: 885px; background-color: #EEEEE0; text-align: center;
                    float: left;">
                    <label style="font-family: Tahoma; font-weight: bold; float: left; margin: 3px 0 0 1px;">
                        CARTÃO DE DÉBITO</label>
                </li>
                <li id="li12" runat="server" title="Clique para adicionar um cartão de débito" class="liBtnAddA"
                    style="float: right; margin: -20px 0px 3px 0px; width: 61px">
                    <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Cartão de Débito" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                        height="15px" width="15px" />
                    <asp:LinkButton ID="lnkAddCartaoDebito" runat="server" OnClick="lnkAddCartaoDebito_OnClick"
                        OnClientClick="if($('#chkDebito').attr('checked')){return true;}else{alert('Selecione a opção débito e informe o valor'); return false;}">Adicionar</asp:LinkButton>
                </li>
            </ul>
        </li>
        <li>
            <div style="width: 953px; height: 60px; border: 1px solid #CCC; overflow-y: scroll">
                <asp:GridView ID="grdCartaoDebito" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                    ClientIDMode="Static" ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum cartão associado<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Banco">
                            <ItemStyle HorizontalAlign="Center" Width="75px" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlBcoDebito" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                    runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agência">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtAgenDebito" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Conta">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumContaDebito" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Número">
                            <ItemStyle HorizontalAlign="Center" Width="105px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumCartaoDebito" CssClass="campoNumCart" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Titular">
                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtTitulDebito" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="R$ Débito">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtVlrDebito" CssClass="campoDin" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nº Autorização">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumAutoriDebito" MaxLength="50" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EX">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgExcCartaoDebito" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                    ToolTip="Excluir Cartão" OnClick="imgExcCartaoDebito_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <!--Pagamento Credito-->
    <ul class="ulDados" style="width: 955px; height: 85px; margin-top: 10px !important;">
        <li>
            <ul style="width: 955px;">
                <li style="height: 20px !important; width: 885px; background-color: #EEEEE0; text-align: center;
                    float: left;">
                    <label style="font-family: Tahoma; font-weight: bold; float: left; margin: 3px 0 0 1px;">
                        CARTÃO DE CRÉDITO</label>
                </li>
                <li id="li11" runat="server" title="Clique para adicionar um cartão de crédito" class="liBtnAddA"
                    style="float: right; margin: -20px 0px 3px 0px; width: 61px">
                    <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Cartão de Crédito" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                        height="15px" width="15px" />
                    <asp:LinkButton ID="lnkAddCartaoCredito" runat="server" OnClick="lnkAddCartaoCredito_OnClick"
                        OnClientClick="if($('#chkCredito').attr('checked')){return true;}else{alert('Selecione a opção crédito e informe o valor'); return false;}">Adicionar</asp:LinkButton>
                </li>
            </ul>
        </li>
        <li>
            <div style="width: 953px; height: 60px; border: 1px solid #CCC; overflow-y: scroll">
                <asp:GridView ID="grdCartaoCredito" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                    ClientIDMode="Static" ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum cartão associado<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Bandeira">
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlBandCredito" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                    runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Número">
                            <ItemStyle HorizontalAlign="Center" Width="105px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumCartaoCredito" CssClass="campoNumCart" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Titular">
                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtTitulCredito" Width="100%" Style="margin-left: -4px; margin-bottom: 0px;"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vencimento">
                            <ItemStyle HorizontalAlign="Center" Width="65px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtVencimentoCredito" CssClass="campoVenc" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="R$ Crédito">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtVlrCredito" CssClass="campoDin" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label Font-Bold="true" runat="server" Text="QTP" ToolTip="Quantidade de Parcelas"></asp:Label>
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="15px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtQtdParcelas" CssClass="campoAnos" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" MaxLength="3" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label Font-Bold="true" runat="server" Text="QDP" ToolTip="Quantidade de Dias entre Parcelas"></asp:Label>
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="15px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtQtdDias" CssClass="campoAnos" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" MaxLength="3" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nº Autorização">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumAutoriCredito" MaxLength="50" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EX">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgExcCartaoCredito" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                    ToolTip="Excluir Cartão" OnClick="imgExcCartaoCredito_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <!--Pagamento Cheque-->
    <ul class="ulDados" style="width: 955px; height: 113px; margin-top: 10px !important;">
        <li>
            <ul style="width: 955px;">
                <li style="height: 20px !important; width: 885px; background-color: #EEEEE0; text-align: center;
                    float: left;">
                    <label style="font-family: Tahoma; font-weight: bold; float: left; margin: 3px 0 0 1px;">
                        CHEQUE</label>
                </li>
                <li id="li13" runat="server" title="Clique para adicionar um cheque" class="liBtnAddA"
                    style="float: right; margin: -20px 0px 3px 0px; width: 61px">
                    <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Cheque" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                        height="15px" width="15px" />
                    <asp:LinkButton ID="lnkAddCheque" runat="server" OnClick="lnkAddCheque_OnClick" OnClientClick="if($('#chkCheque').attr('checked')){return true;}else{alert('Selecione a opção cheque e informe o valor'); return false;}">Adicionar</asp:LinkButton>
                </li>
            </ul>
        </li>
        <li>
            <div style="width: 953px; height: 81px; border: 1px solid #CCC; overflow-y: scroll">
                <asp:GridView ID="grdCheque" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                    ClientIDMode="Static" ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum cheque associado<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Banco">
                            <ItemStyle HorizontalAlign="Center" Width="75px" />
                            <ItemTemplate>
                                <asp:DropDownList ClientIDMode="AutoID" ID="ddlBcoCheque" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agência">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            <ItemTemplate>
                                <asp:TextBox ClientIDMode="AutoID" ID="txtAgenCheque" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Conta">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:TextBox ClientIDMode="AutoID" ID="txtNumContaCheque" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Número">
                            <ItemStyle HorizontalAlign="Center" Width="95px" />
                            <ItemTemplate>
                                <asp:TextBox ClientIDMode="AutoID" ID="txtNumCheque" MaxLength="30" Width="100%"
                                    Style="margin-left: -4px; margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CPF">
                            <ItemStyle HorizontalAlign="Center" Width="83px" />
                            <ItemTemplate>
                                <asp:TextBox ClientIDMode="AutoID" ID="txtCPFCheque" class="campoCpf" Width="100%"
                                    Style="margin-left: -4px; margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Titular">
                            <ItemStyle HorizontalAlign="Center" Width="165px" />
                            <ItemTemplate>
                                <asp:TextBox ClientIDMode="AutoID" ID="txtTitulCheque" Width="100%" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="R$ Cheque">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:TextBox ClientIDMode="AutoID" ID="txtVlrCheque" CssClass="campoDin" Width="100%"
                                    Style="margin-left: -4px; margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vencimento">
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                            <ItemTemplate>
                                <asp:TextBox ClientIDMode="AutoID" ID="txtVencimentoCheque" class="campoData" Style="margin-left: -4px;
                                    margin-bottom: 0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EX">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgExcCheque" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                    ToolTip="Excluir Cheque" OnClick="imgExcCheque_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li>
            <div id="divLoadInfosCadas" style="display: none; height: 350px !important;">
                <ul class="ulDados" style="width: 400px !important; margin-top: 15px !important;">
                    <div class="DivResp" runat="server" id="divResp">
                        <ul class="ulDadosResp" style="margin-left: -95px !important; width: 600px !important;">
                            <li style="margin: -5px 0 0px 0px">
                                <label class="lblTop">
                                    DADOS DO RESPONSÁVEL PELO PACIENTE
                                </label>
                            </li>
                            <li style="clear: both; margin: 9px -1px 0 0px;"><a class="lnkPesResp" href="#">
                                <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                                    style="width: 17px; height: 17px;" /></a> </li>
                            <li>
                                <label class="lblObrigatorio">
                                    CPF</label>
                                <asp:TextBox runat="server" ID="txtCPFResp" Style="width: 74px;" CssClass="campoCpf"
                                    ToolTip="CPF do Responsável"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidCoResp" />
                            </li>
                            <li style="margin-top: 10px; margin-left: 0px;">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                    Width="13px" Height="13px" OnClick="imgCpfResp_OnClick" ValidationGroup="validaCPF" />
                            </li>
                            <li>
                                <label class="lblObrigatorio">
                                    Nome</label>
                                <asp:TextBox runat="server" ID="txtNomeResp" Width="216px" ToolTip="Nome do Responsável"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Sexo</label>
                                <asp:DropDownList runat="server" ID="ddlSexResp" Width="44px" ToolTip="Selecione o Sexo do Responsável">
                                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label class="lblObrigatorio">
                                    Nascimento</label>
                                <asp:TextBox runat="server" ID="txtDtNascResp" CssClass="campoData"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Grau Parentesco</label>
                                <asp:DropDownList runat="server" ID="ddlGrParen" Width="100px">
                                    <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Pai/Mãe" Value="PM"></asp:ListItem>
                                    <asp:ListItem Text="Tio(a)" Value="TI"></asp:ListItem>
                                    <asp:ListItem Text="Avô/Avó" Value="AV"></asp:ListItem>
                                    <asp:ListItem Text="Primo(a)" Value="PR"></asp:ListItem>
                                    <asp:ListItem Text="Cunhado(a)" Value="CN"></asp:ListItem>
                                    <asp:ListItem Text="Tutor(a)" Value="TU"></asp:ListItem>
                                    <asp:ListItem Text="Irmão(ã)" Value="IR"></asp:ListItem>
                                    <asp:ListItem Text="Outros" Value="OU" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both; margin: 5px 0 0 0px;">
                                <ul class="ulIdentResp">
                                    <li>
                                        <asp:Label runat="server" ID="lblcarteIden" Style="font-size: 9px;" CssClass="lblObrigatorio">Carteira de Identidade</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Número</label>
                                        <asp:TextBox runat="server" ID="txtNuIDResp" Width="70px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Org Emiss</label>
                                        <asp:TextBox runat="server" ID="txtOrgEmiss" Width="50px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            UF</label>
                                        <asp:DropDownList runat="server" ID="ddlUFOrgEmis" Width="40px">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: 5px 0 0 10px;">
                                <ul class="ulDadosContatosResp">
                                    <li>
                                        <asp:Label runat="server" ID="Label1" Style="font-size: 9px;" CssClass="lblObrigatorio">Dados de Contato</asp:Label>
                                    </li>
                                    <li style="clear: both;">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtTelFixResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtTelCelResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Tel. Comercial</label>
                                        <asp:TextBox runat="server" ID="txtTelComResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Nº WhatsApp</label>
                                        <asp:TextBox runat="server" ID="txtNuWhatsResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            Facebook</label>
                                        <asp:TextBox runat="server" ID="txtDeFaceResp" Width="91px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li style="clear: both; width: 206px; border-right: 1px solid #CCCCCC; margin-left: -5px;
                                height: 65px; margin-top: 7px;">
                                <ul style="margin-left: 0px" class="ulInfosGerais">
                                    <li style="margin-left: 5px; margin-bottom: 1px;">
                                        <label class="lblSubInfos">
                                            INFORMAÇÕES GERAIS</label>
                                    </li>
                                    <li style="clear: both">
                                        <asp:CheckBox runat="server" ID="chkPaciEhResp" OnCheckedChanged="chkPaciEhResp_OnCheckedChanged"
                                            AutoPostBack="true" Text="Responsável é o próprio paciente" CssClass="chk" />
                                    </li>
                                    <li style="clear: both">
                                        <asp:CheckBox runat="server" ID="chkPaciMoraCoResp" Text="Paciente mora com o(a) Responsável"
                                            CssClass="chk" />
                                    </li>
                                    <li style="clear: both">
                                        <asp:CheckBox runat="server" ID="chkRespFinanc" Text="É o responsável financeiro"
                                            CssClass="chk" />
                                    </li>
                                </ul>
                            </li>
                            <li>
                                <ul style="margin-left: 0px" class="ulEndResiResp">
                                    <li style="margin-left: 1px; margin-bottom: 1px; margin-top: 7px;">
                                        <label class="lblSubInfos">
                                            ENDEREÇO RESIDENCIAL / CORRESPONDÊNCIA</label>
                                    </li>
                                    <li style="clear: both;">
                                        <label class="lblObrigatorio">
                                            CEP</label>
                                        <asp:TextBox runat="server" ID="txtCEP" Width="55px" CssClass="campoCEP"></asp:TextBox>
                                    </li>
                                    <li style="margin: 11px 2px 0 -2px;">
                                        <asp:ImageButton ID="imgPesqCEP" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                            ValidationGroup="validaCEP" OnClick="imgPesqCEP_OnClick" Width="13px" Height="13px" />
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            UF</label>
                                        <asp:DropDownList runat="server" ID="ddlUF" Width="40px" OnSelectedIndexChanged="ddlUF_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Cidade</label>
                                        <asp:DropDownList runat="server" ID="ddlCidade" Width="130px" OnSelectedIndexChanged="ddlCidade_OnSelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Bairro</label>
                                        <asp:DropDownList runat="server" ID="ddlBairro" Width="115px">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: both;">
                                        <label class="lblObrigatorio">
                                            Logradouro</label>
                                        <asp:TextBox runat="server" ID="txtLograEndResp" Width="160px"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 10px;">
                                        <label>
                                            Email</label>
                                        <asp:TextBox runat="server" ID="txtEmailResp" Width="197px"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li style="clear: both; margin-left: -5px;">
                                <ul>
                                    <li class="liFotoColab">
                                        <fieldset class="fldFotoColab">
                                            <uc1:ControleImagem ID="upImagemAluno" runat="server" />
                                        </fieldset>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin: 5px 0 0 -23px;">
                                <ul class="ulDadosPaciente">
                                    <li style="margin-bottom: -6px;">
                                        <label class="lblTop">
                                            DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                                    </li>
                                    <li style="clear: both">
                                        <label>
                                            Nº NIRE</label>
                                        <asp:TextBox runat="server" ID="txtNuNis" Enabled="false" Width="60"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label>
                                            CPF</label>
                                        <asp:TextBox runat="server" ID="txtCPFMOD" CssClass="campoCpf" Width="75px"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hidCoPac" />
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Nome</label>
                                        <asp:TextBox runat="server" ID="txtnompac" ToolTip="Nome do Paciente" Width="308px"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label class="lblObrigatorio">
                                            Sexo</label>
                                        <asp:DropDownList runat="server" ID="ddlSexoPaci" Width="44px">
                                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="clear: both" class="lisobe">
                                        <label class="lblObrigatorio">
                                            Nascimento</label>
                                        <asp:TextBox runat="server" ID="txtDtNascPaci" CssClass="campoData"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Origem</label>
                                        <asp:DropDownList runat="server" ID="ddlOrigemPaci" Width="90px">
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-left: 10px;" class="lisobe">
                                        <label>
                                            Nº Cartão Saúde</label>
                                        <asp:TextBox runat="server" ID="txtNuCarSaude" Width="87px"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 10px;" class="lisobe">
                                        <label>
                                            Tel. Celular</label>
                                        <asp:TextBox runat="server" ID="txtTelResPaci" Width="68px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Tel. Fixo</label>
                                        <asp:TextBox runat="server" ID="txtTelCelPaci" Width="68px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li class="lisobe">
                                        <label>
                                            Nº WhatsApp</label>
                                        <asp:TextBox runat="server" ID="txtWhatsPaci" Width="68px" CssClass="campoTel"></asp:TextBox>
                                    </li>
                                    <li style="clear: both; margin-top: -2px; float: right">
                                        <asp:Label runat="server" ID="lblEmailPaci">Email</asp:Label>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtEmailPaci" Width="220px"></asp:TextBox>
                                    </li>
                                    <li style="clear: both; margin-top: -32px;">
                                        <asp:Label runat="server" ID="Label12" class="lblObrigatorio">Apelido</asp:Label>
                                        <br />
                                        <asp:TextBox runat="server" ID="txtApelido" MaxLength="25" Width="80px"></asp:TextBox>
                                    </li>
                                    <li style="margin-left: 88px; margin-top: -32px;">
                                        <label for="txtIndicacao" title="Indicação">
                                            Indicação
                                        </label>
                                        <asp:DropDownList ID="ddlIndicacao" Style="width: 194px;" runat="server" ToolTip="Indicação">
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                            <li class="liBtnAddA" style="margin-left: 10px !important; margin-top: 0px !important;
                                clear: both !important; height: 15px;">
                                <asp:LinkButton ID="lnkCadastroCompleto" runat="server" OnClick="lnkCadastroCompleto_OnClick"
                                    ValidationGroup="validacaoCadastro">
                                    <asp:Label runat="server" ID="Label23" Text="CADASTRO COMPLETO" Style="margin-left: 4px;
                                        margin-right: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li class="liBtnAddA" style="margin-left: 543px !important; margin-top: -17px !important;">
                                <asp:LinkButton ID="lnkSalvar" Enabled="true" runat="server" OnClick="lnkSalvar_OnClick"
                                    ValidationGroup="validacaoSalvar">
                                    <asp:Label runat="server" ID="Label2" Text="SALVAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                    <div id="divSuccessoMessage" runat="server" class="successMessageSMS" visible="false">
                        <asp:Label ID="lblMsg" runat="server" Visible="false" />
                        <asp:Label Style="color: #B22222 !important; display: block;" Visible="false" ID="lblMsgAviso"
                            runat="server" />
                    </div>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadRaps" style="display: none; height: 350px !important;">
                <asp:GridView ID="grdRap" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                    ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Não existe nenhum procedimento associado à este atendimento (RAP)<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="TP_PROCE" HeaderText="TP">
                            <ItemStyle HorizontalAlign="Left" Width="15px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NU_PROCE" HeaderText="PROCED">
                            <ItemStyle HorizontalAlign="Left" Width="45px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GRP_PROCE" HeaderText="GRUPO">
                            <ItemStyle HorizontalAlign="Left" Width="85px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SGRP_PROCE" HeaderText="SUBGRUPO">
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NM_PROCE" HeaderText="PROCEDIMENTO">
                            <ItemStyle HorizontalAlign="Left" Width="185px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VLR_PROCE" HeaderText="R$ VLR">
                            <ItemStyle HorizontalAlign="Left" Width="35px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SOLICI_PROCE" HeaderText="SOLICITANTE">
                            <ItemStyle HorizontalAlign="Left" Width="185px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CRM_SOLICI_PROCE" HeaderText="CRM">
                            <ItemStyle HorizontalAlign="Left" Width="25px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li>
            <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#divOcorrencia").show();
            $(".nuQtde").mask("9?99");
            $(".campoVenc").mask("99/99");
            $(".campoDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoNumCart").mask("9999.9999.9999.9999");

            if (!($("#chkDinheiro").attr("checked"))) {
                $("#txtDinheiro").enable(false);
            }
            if (!($("#chkCheque").attr("checked"))) {
                $("#txtCheque").enable(false);
                $("#txtParcelasCheque").enable(false);
            }
            if (!($("#chkDebito").attr("checked"))) {
                $("#txtDebito").enable(false);
                $("#txtParcelasDebito").enable(false);
            }
            if (!($("#chkCredito").attr("checked"))) {
                $("#txtCredito").enable(false);
                $("#txtParcelasCredito").enable(false);
            }
            if (!($("#chkTransferencia").attr("checked"))) {
                $("#txtTransferencia").enable(false);
            }
            if (!($("#chkDeposito").attr("checked"))) {
                $("#txtDeposito").enable(false);
            }
            if (!($("#chkBoleto").attr("checked"))) {
                $("#txtBoleto").enable(false);
                $("#txtParcelasBoleto").enable(false);
            }
            if (!($("#chkOutros").attr("checked"))) {
                $("#txtOutros").enable(false);
            }
            if (!($("#chkNotaFiscal").attr("checked"))) {
                $("#txtNotaFiscal").enable(false);
                $("#txtDtNotaFiscal").enable(false);
            }
            if (($("#chkCortesia").attr("checked"))) {
                Cortesia(false);
            }

            $("#chkDinheiro").click(function () {
                if ($("#chkDinheiro").attr("checked")) {
                    $("#txtDinheiro").enable(true);
                }
                else {
                    $("#txtDinheiro").enable(false);
                    $("#txtDinheiro").val("");
                }
            });

            $("#chkCheque").click(function () {
                if ($("#chkCheque").attr("checked")) {
                    $("#txtCheque").enable(true);
                    $("#txtParcelasCheque").enable(true);
                    $("#txtParcelasCheque").val("1");
                }
                else {
                    $("#txtCheque").enable(false);
                    $("#txtParcelasCheque").enable(false);
                    $("#txtCheque").val("");
                    $("#txtParcelasCheque").val("");
                }
            });

            $("#chkDebito").click(function () {
                if ($("#chkDebito").attr("checked")) {
                    $("#txtDebito").enable(true);
                    $("#txtParcelasDebito").enable(true);
                    $("#txtParcelasDebito").val("1");
                }
                else {
                    $("#txtDebito").enable(false);
                    $("#txtParcelasDebito").enable(false);
                    $("#txtDebito").val("");
                    $("#txtParcelasDebito").val("");
                }
            });

            $("#chkCredito").click(function () {
                if ($("#chkCredito").attr("checked")) {
                    $("#txtCredito").enable(true);
                    $("#txtParcelasCredito").enable(true);
                    $("#txtParcelasCredito").val("1");
                }
                else {
                    $("#txtCredito").enable(false);
                    $("#txtParcelasCredito").enable(false);
                    $("#txtCredito").val("");
                    $("#txtParcelasCredito").val("");
                }
            });

            $("#chkTransferencia").click(function () {
                if ($("#chkTransferencia").attr("checked")) {
                    $("#txtTransferencia").enable(true);
                }
                else {
                    $("#txtTransferencia").enable(false);
                    $("#txtTransferencia").val("");
                }
            });

            $("#chkDeposito").click(function () {
                if ($("#chkDeposito").attr("checked")) {
                    $("#txtDeposito").enable(true);
                }
                else {
                    $("#txtDeposito").enable(false);
                    $("#txtDeposito").val("");
                }
            });

            $("#chkBoleto").click(function () {
                if ($("#chkBoleto").attr("checked")) {
                    $("#txtBoleto").enable(true);
                    $("#txtParcelasBoleto").enable(true);
                    $("#txtParcelasBoleto").val("1");
                }
                else {
                    $("#txtBoleto").enable(false);
                    $("#txtParcelasBoleto").enable(false);
                    $("#txtBoleto").val("");
                    $("#txtParcelasBoleto").val("");
                }
            });

            $("#chkOutros").click(function () {
                if ($("#chkOutros").attr("checked")) {
                    $("#txtOutros").enable(true);
                }
                else {
                    $("#txtOutros").enable(false);
                    $("#txtOutros").val("");
                }
            });

            $("#chkNotaFiscal").click(function () {
                if ($("#chkNotaFiscal").attr("checked")) {
                    $("#txtNotaFiscal").enable(true);
                    $("#txtDtNotaFiscal").enable(true);
                }
                else {
                    $("#txtNotaFiscal").enable(false);
                    $("#txtDtNotaFiscal").enable(false);
                    $("#txtNotaFiscal").val("");
                    $("#txtDtNotaFiscal").val("");
                }
            });

            $("#chkCortesia").click(function () {
                if ($("#chkCortesia").attr("checked")) {
                    Cortesia(false);
                }
                else {
                    Cortesia(true);
                }
            });

            $(".lnkPesPaci").click(function () {
                $('#divLoadShowAlunos').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });
        });

        function carregaCss() {
            $(".campoCpf").unmask();
            $(".campoCpf").mask("999.999.999-99");
            $(".campoTel").unmask();
            $(".campoTel").mask("(99)9999-9999");
            $(".campoCEP").unmask();
            $(".campoCEP").mask("99999-999");
            $(".campoHora").unmask();
            $(".campoHora").mask("99:99");
            $(".campoAnos").unmask();
            $(".campoAnos").mask("99");
            $(".txtNireAluno").unmask();
            $(".txtNireAluno").mask("?999999999");
            $(".txtNIS").mask("?999999999999999?9");
            //            $(".campoData").datepicker();
            //            $(".campoData").mask("99/99/9999");
            $(".campoVenc").unmask();
            $(".campoVenc").mask("99/99");
            $(".numeroCartao").unmask();
            $(".numeroCartao").mask("9999.9999.9999.9999");
            $(".campoMoeda").unmask();
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            $(".lnkPesPaci").click(function () {
                $('#divLoadShowAlunos').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });

            $(".lnkPesResp").click(function () {
                $('#divLoadShowResponsaveis').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE RESPONSÁVEIS",
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });
        }

        function AbreModalInfosCadas() {
            $('#divLoadInfosCadas').dialog({ autoopen: false, modal: true, width: 652, height: 350, resizable: false, title: "USUÁRIO DE SAÚDE - CADASTRO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
        function AbreModalListaRaps() {
            $('#divLoadRaps').dialog({ autoopen: false, modal: true, width: 900, height: 350, resizable: false, title: "RAP - LISTAR",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function Cortesia(enable) {
            $("#txtVlrReceb").val("");
            $("#txtParcelas").val("1");
            $("#txtVlrReceb").enable(enable);

            //$("#rfvVlrReceb").enabled = false;
            //$("#txtVlrReceb").enable(enable);
            //$("#rfvVlrReceb").IsValid = false;
            //$("#rfvVlrReceb").disabled = true;
            //$("#rfvVlrReceb").attr("enabled", false);
            //$("#rfvVlrReceb").attr("disabled", true);
            //$("#rfvVlrReceb").attr("display", "none");
            //$("#rfvVlrReceb").val("");
            //$("#rfvVlrReceb").html("");
            //$("#rfvVlrReceb").innerHTML("");
            //$("#rfvVlrReceb").style.visibility = "hidden";
            //$("#rfvVlrReceb").removeAttr("data-val-required");

            $("#txtParcelas").enable(enable);
            //$("#rfvParcelas").enabled = enable;

            $("#chkDinheiro").enable(enable);
            $("#chkCheque").enable(enable);
            $("#chkDebito").enable(enable);
            $("#chkCredito").enable(enable);
            $("#chkTransferencia").enable(enable);
            $("#chkDeposito").enable(enable);
            $("#chkBoleto").enable(enable);
            $("#chkOutros").enable(enable);
            $("#chkNotaFiscal").enable(enable);

            if (!enable) {
                $("#chkDinheiro").attr("checked", false);
                $("#chkCheque").attr("checked", false);
                $("#chkDebito").attr("checked", false);
                $("#chkCredito").attr("checked", false);
                $("#chkTransferencia").attr("checked", false);
                $("#chkDeposito").attr("checked", false);
                $("#chkBoleto").attr("checked", false);
                $("#chkOutros").attr("checked", false);
                $("#chkNotaFiscal").attr("checked", false);

                $("#txtDinheiro").enable(enable);
                $("#txtCheque").enable(enable);
                $("#txtParcelasCheque").enable(enable);
                $("#txtDebito").enable(enable);
                $("#txtParcelasDebito").enable(enable);
                $("#txtCredito").enable(enable);
                $("#txtParcelasCredito").enable(enable);
                $("#txtTransferencia").enable(enable);
                $("#txtDeposito").enable(enable);
                $("#txtBoleto").enable(enable);
                $("#txtParcelasBoleto").enable(enable);
                $("#txtOutros").enable(enable);
                $("#txtNotaFiscal").enable(enable);

                $("#txtVlrReceb").val("0");
                $("#txtParcelas").val("0");
                $("#txtDinheiro").val("");
                $("#txtCheque").val("");
                $("#txtParcelasCheque").val("");
                $("#txtDebito").val("");
                $("#txtParcelasDebito").val("");
                $("#txtCredito").val("");
                $("#txtParcelasCredito").val("");
                $("#txtTransferencia").val("");
                $("#txtDeposito").val("");
                $("#txtBoleto").val("");
                $("#txtParcelasBoleto").val("");
                $("#txtOutros").val("");
                $("#txtNotaFiscal").val("");

                $("#grdCartaoDebito").html("");
                $("#grdCartaoCredito").html("");
                $("#grdCheque").html("");
            }
        }
    </script>
</asp:Content>
