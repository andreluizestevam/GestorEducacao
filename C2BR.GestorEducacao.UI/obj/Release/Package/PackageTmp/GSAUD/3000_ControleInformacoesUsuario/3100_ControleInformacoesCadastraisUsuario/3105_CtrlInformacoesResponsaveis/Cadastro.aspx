<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Cadastro.aspx.cs"
    Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3105_CtrlInformacoesResponsaveis.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="artem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 915px;
            margin: -5px auto auto !important;
        }
        input[type='text']
        {
            margin-bottom: 4px;
        }
        select
        {
            margin-bottom: 4px;
        }
        label
        {
            margin-bottom: 1px;
        }
        
        /*--> CSS LIs */
        .liFotoResp
        {
            float: left !important;
        }
        .liBloco1
        {
            max-width: 812px;
            margin-left: 5px;
            margin-right: 0px !important;
        }
        .liDados1
        {
            padding: 0px 0px;
            margin: 0px 0px !important;
        }
        .liOrigem
        {
            margin-left: 5px;
        }
        .liSexoResp
        {
            margin-left: 21px;
        }
        .liDtNascResp, .liCursoFormacao
        {
            margin-left: 6px;
        }
        .liUfEmp
        {
            margin-left: 8px;
        }
        .liEspaco
        {
            margin-left: 7px;
        }
        .liClear
        {
            clear: both !important;
        }
        .liQtdMenores
        {
            padding-left: 30px;
        }
        .liQtdMaiores
        {
            padding-left: 28px;
        }
        .liDadosInfDiv, .liDadosConjug, .liBlocoIdent, .liBlocoTitEle, .liBlocoDocDiv, .liBlocoFili, .liBlocoEndRes
        {
            border-left: 1px solid #BEBEBE;
            padding-left: 10px;
        }
        .liDadosInfDiv, .liDadosConjug
        {
            padding-right: 0px;
            margin-right: 0px;
        }
        .liBloco2, .liBloco3
        {
            margin-top: 7px;
            margin-right: 0px !important;
        }
        .liPesqCEPR
        {
            margin-top: 14px;
            margin-left: -3px;
        }
        
        /*--> CSS DADOS */
        .mapa
        {
            width: 300px !important;
            height: 140px !important;
        }
        .fldFotoResp
        {
            border: none;
            width: 90px;
            height: 110px;
        }
        .txtNomeResp
        {
            width: 165px;
            text-transform: uppercase;
        }
        .txtDtNascResp, .txtDtEmissaoResp, .txtDtAdmissaoResp
        {
            width: 60px;
        }
        .txtNumeroTituloResp, .txtNaturalidadeResp
        {
            width: 80px;
        }
        .ddlSexoResp
        {
            width: 65px;
        }
        .ddlGrauInstrucaoResp
        {
            width: 106px;
        }
        .ddlCursoFormacaoResp, .txtFuncaoResp
        {
            width: 185px;
        }
        .ddlEstadoCivilResp
        {
            width: 142px;
        }
        .ddlRendaResp, .txtCPFResp
        {
            width: 82px;
        }
        .txtIdentidadeResp
        {
            width: 104px;
        }
        .txtOrgEmissorResp
        {
            width: 65px;
        }
        .txtComplementoResp
        {
            width: 165px;
        }
        .txtZonaResp, .txtSecaoResp, .txtNumeroResp
        {
            width: 40px;
        }
        .txtLogradouroResp
        {
            width: 210px;
        }
        .txtTelResidencialResp, .txtTelEmpresaResp, .txtNISResp, .txtApelidoResp
        {
            width: 78px;
        }
        .txtSalarioBruto, .txtDescSalario, .txtSalarioLiqui, .txtDevedor, .txtCredito, .txtResult
        {
            width: 52px;
        }
        .txtPercent, .txtPercentMes
        {
            width: 20px;
        }
        .ddlCidadeResp, .ddlBairroResp
        {
            width: 165px;
        }
        .txtCepResp
        {
            width: 56px;
        }
        .txtEmpresaResp, .txtEmailResp
        {
            width: 170px;
        }
        .ddlFuncaoResp
        {
            width: 130px;
        }
        .txtEmailFuncionalResp, .txtFuncaoResp, .txtDepartamentoResp
        {
            width: 128px; 
        }
        .txtFoPag
        {
            width: 78px;
        }
        .txtQtdMaioresResp, .txtQtdMenoresResp
        {
            width: 55px;
        }
        .chkLocais label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .ddlSituacaoResp, .ddlDeficienciaResp
        {
            width: 70px;
        }
        .ddlNacionalidadeResp
        {
            width: 105px;
        }
        .ddlTpSangueResp
        {
            width: 35px;
            clear: both;
        }
        .ddlStaTpSangueResp, .txtCargHor
        {
            width: 30px;
        }
        .ddlCorRacaResp
        {
            width: 68px;
        }
        .lblTitInf
        {
            text-transform: uppercase;
            font-weight: bold;
            font-size: 1.0em;
        }
        .txtPassaporteResp, .txtCartaoSaudeResp
        {
            width: 98px;
        }
        .txtObservacoesResp
        {
            width: 200px;
            height: 40px;
        }
        .btnPesqMat
        {
            width: 13px;
        }
        #helpMessages
        {
            margin-top: -5px !important;
        }
        .chkRest label
        {
            display:inline;
            color:Red;
            margin-left:-3px;
        }
        .lbltxtDtVencimento
        {
            color:Red;
        }
        /*.file { left: -120px !important; position: absolute !important; }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liFotoResp">
            <fieldset class="fldFotoResp">
                <uc1:ControleImagem ID="upImagemResp" runat="server" />
            </fieldset>
        </li>
        <li class="liBloco1">
            <ul>
                <li class="liDados1">
                    <ul>
                        <li>
                            <label for="txtCPFResp" title="CPF" class="lblObrigatorio">
                                CPF</label>
                            <asp:TextBox ID="txtCPFResp" ToolTip="Informe o CPF" CssClass="txtCPFResp" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCPF" runat="server" ControlToValidate="txtCPFResp"
                                ErrorMessage="CPF deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ControlToValidate="txtCPFResp" ID="cvCPF" runat="server" ErrorMessage="CPF do Responsável inválido"
                                Text="*" Display="None" CssClass="validatorField" EnableClientScript="false"
                                OnServerValidate="cvValidaCPF"></asp:CustomValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="txtNISResp" title="NIS">
                                Nº CONTROLE NIS</label>
                            <asp:TextBox ID="txtNISResp" ToolTip="Informe o NIS" CssClass="txtNISResp" runat="server"
                                MaxLength="15"></asp:TextBox>
                        </li>
                        <li style="clear: none !important; margin-top: 12px;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkRespFunc" TextAlign="Right" runat="server"
                                ToolTip="Responsável é Funcionário" Text="Funcionário" />
                        </li>
                        <li class="liDtNascResp">
                            <label for="txtDtNascResp" title="Data de Nascimento" class="lblObrigatorio">
                                Data de Nascimento</label>
                            <asp:TextBox ID="txtDtNascResp" ToolTip="Informe a Data de Nascimento" CssClass="txtDtNasc campoData"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDtNasc" runat="server" ControlToValidate="txtDtNascResp"
                                ErrorMessage="Data de Nascimento deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDtNasc" ControlToValidate="txtDtNascResp"
                                runat="server" ErrorMessage="Data de Nascimento inválida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                        </li>
                        <li class="liClear">
                            <label for="txtNomeResp" class="lblObrigatorio" title="Nome do Responsável">
                                Nome</label>
                            <asp:TextBox ID="txtNomeResp" CssClass="txtNomeResp" ToolTip="Informe o Nome do Responsável"
                                runat="server" MaxLength="60"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNomeResp"
                                ErrorMessage="Nome deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="txtApelidoResp" title="Apelido">
                                Apelido</label>
                            <asp:TextBox ID="txtApelidoResp" ToolTip="Informe o Apelido do Responsável" CssClass="txtApelidoResp"
                                runat="server" MaxLength="15">
                            </asp:TextBox>
                        </li>
                        <li class="liSexoResp">
                            <label for="ddlSexoResp" title="Sexo">
                                Sexo</label>
                            <asp:DropDownList ID="ddlSexoResp" ToolTip="Selecione o Sexo" CssClass="ddlSexoResp"
                                runat="server">
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <li class="liClear">
                                    <label for="ddlNacionalidadeResp" title="Nacionalidade do Responsável">
                                        Nacionalidade</label>
                                    <asp:DropDownList ID="ddlNacionalidadeResp" CssClass="ddlNacionalidadeResp" runat="server"
                                        ToolTip="Informe a Nacionalidade do Responsável" AutoPostBack="true" OnSelectedIndexChanged="ddlNacionalidadeResp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtNaturalidadeResp" title="Cidade de Naturalidade do Responsável">
                                        Naturalidade</label>
                                    <asp:TextBox ID="txtNaturalidadeResp" CssClass="txtNaturalidadeResp" runat="server"
                                        ToolTip="Informe a Cidade de Naturalidade do Responsável" MaxLength="40"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="ddlUfNacionalidadeResp" title="UF de Nacionalidade do Responsável">
                                        UF</label>
                                    <asp:DropDownList ID="ddlUfNacionalidadeResp" CssClass="campoUf" runat="server" ToolTip="Informe a UF de Nacionalidade">
                                    </asp:DropDownList>
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <li class="liOrigem">
                            <label for="ddlOrigemResp" class="" title="Origem">
                                Origem</label>
                            <asp:DropDownList ID="ddlOrigemResp" Style="width: 100px;" runat="server" ToolTip="Origem">
                                <asp:ListItem Value="SR" Text="Sem Registro"></asp:ListItem>
                                <asp:ListItem Value="AI" Text="Área Indígena"></asp:ListItem>
                                <asp:ListItem Value="AQ" Text="Área Quilombo"></asp:ListItem>
                                <asp:ListItem Value="AR" Text="Área Rural"></asp:ListItem>
                                <asp:ListItem Value="IE" Text="Interior do Estado"></asp:ListItem>
                                <asp:ListItem Value="MU" Text="Município"></asp:ListItem>
                                <asp:ListItem Value="OE" Text="Outro Estado"></asp:ListItem>
                                <asp:ListItem Value="OO" Text="Outra Origem"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <li class="liClear">
                                    <label for="ddlGrauInstrucaoResp" class="lblObrigatorio" title="Grau de Instrução">
                                        Grau de Instrução</label>
                                    <asp:DropDownList ID="ddlGrauInstrucaoResp" ToolTip="Selecione o Grau de Instrução"
                                        CssClass="ddlGrauInstrucaoResp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrauInstrucao_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGrauInstrucaoResp"
                                        ErrorMessage="Grau de Instrução deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtTelResidencialResp" title="Telefone Residencial">
                                        Tel. Residencial</label>
                                    <asp:TextBox ID="txtTelResidencialResp" ToolTip="Informe o Telefone Residencial"
                                        CssClass="txtTelResidencialResp" runat="server"></asp:TextBox>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtTelCelularResp" title="Telefone Celular">
                                        Tel. Celular</label>
                                    <asp:TextBox ID="txtTelCelularResp" ToolTip="Informe o Telefone Celular" CssClass="txtTelResidencialResp"
                                        runat="server"></asp:TextBox>
                                </li>
                                <li style="clear: none !important; margin-top: 12px;">
                                    <asp:CheckBox CssClass="chkLocais" ID="chkWA" TextAlign="Right" runat="server"
                                        ToolTip="Celular é WhatsApp?" Text="WhatsApp?" />
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ul>
                </li>
                <li class="liDadosInfDiv" style="height: 124px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Informações Diversas</label></li>
                        <li class="liClear">
                            <label title="Tipo de Sangue">
                                Tp.Sanguíneo</label>
                            <asp:DropDownList ID="ddlTpSangueFResp" ToolTip="Selecione o Tipo de Sangue" CssClass="ddlTpSangueResp"
                                runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="A">A</asp:ListItem>
                                <asp:ListItem Value="B">B</asp:ListItem>
                                <asp:ListItem Value="AB">AB</asp:ListItem>
                                <asp:ListItem Value="O">O</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlStaSangueFResp" ToolTip="Selecione o Status do Tipo de Sangue"
                                CssClass="ddlStaTpSangueResp" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="P">+</asp:ListItem>
                                <asp:ListItem Value="N">-</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlRendaResp" title="Renda Familiar">
                                Renda Familiar</label>
                            <asp:DropDownList ID="ddlRendaResp" ToolTip="Selecione a Renda Familiar" CssClass="ddlRendaResp"
                                runat="server">
                                <asp:ListItem Value="1">1 a 3 SM</asp:ListItem>
                                <asp:ListItem Value="2">3 a 5 SM</asp:ListItem>
                                <asp:ListItem Value="3">5 a 10 SM</asp:ListItem>
                                <asp:ListItem Value="4">+10 SM</asp:ListItem>
                                <asp:ListItem Value="5">Sem Renda</asp:ListItem>
                                <asp:ListItem Value="6">Não informada</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="ddlCorRacaResp" title="Etnia do Responsável">
                                Etnia</label>
                            <asp:DropDownList ID="ddlCorRacaResp" ToolTip="Informe a Etnia do Responsável" CssClass="ddlCorRacaResp"
                                runat="server">
                                <asp:ListItem Value="">Não Informada</asp:ListItem>
                                <asp:ListItem Value="B">Branca</asp:ListItem>
                                <asp:ListItem Value="A">Amarela</asp:ListItem>
                                <asp:ListItem Value="N">Negra</asp:ListItem>
                                <asp:ListItem Value="P">Parda</asp:ListItem>
                                <asp:ListItem Value="I">Indígena</asp:ListItem>
                                <asp:ListItem Value="O">Outra</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liQtdMenores">
                            <label for="txtQtdMenoresResp" title="Quantidade de Dependentes Menores">
                                Dep Menores</label>
                            <asp:TextBox ID="txtQtdMenoresResp" ToolTip="Informe a Quantidade de Dependentes Menores"
                                CssClass="txtQtdMenoresResp" runat="server" MaxLength="2"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="ddlDeficienciaResp" title="Deficiência">
                                Deficiência</label>
                            <asp:DropDownList ID="ddlDeficienciaResp" ToolTip="Selecione a Deficiência" CssClass="ddlDeficienciaResp"
                                runat="server">
                                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                                <asp:ListItem Value="A">Auditivo</asp:ListItem>
                                <asp:ListItem Value="D">Dificuldade Locomoção</asp:ListItem>
                                <asp:ListItem Value="V">Visual</asp:ListItem>
                                <asp:ListItem Value="F">Físico</asp:ListItem>
                                <asp:ListItem Value="M">Mental</asp:ListItem>
                                <asp:ListItem Value="I">Múltiplas</asp:ListItem>
                                <asp:ListItem Value="O">Outros</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liQtdMaiores">
                            <label for="txtQtdMaioresResp" title="Quantidade de Dependentes Maiores">
                                Dep Maiores</label>
                            <asp:TextBox ID="txtQtdMaioresResp" ToolTip="Informe a Quantidade de Dependentes Maiores"
                                CssClass="txtQtdMaioresResp" runat="server" MaxLength="2"></asp:TextBox>
                        </li>
                        <li style="clear:both; margin-top:-5px;">
                            <label for="ddlEstadoCivilResp" title="Estado Civil">
                                Estado Civil</label>
                            <asp:DropDownList ID="ddlEstadoCivilResp" ToolTip="Selecione o Estado Civil" CssClass="ddlEstadoCivilResp"
                                runat="server">
                                <asp:ListItem Value="">Não Informado</asp:ListItem>
                                <asp:ListItem Value="S">Solteiro(a)</asp:ListItem>
                                <asp:ListItem Value="C">Casado(a)</asp:ListItem>
                                <asp:ListItem Value="S">Separado Judicialmente</asp:ListItem>
                                <asp:ListItem Value="D">Divorciado(a)</asp:ListItem>
                                <asp:ListItem Value="V">Viúvo(a)</asp:ListItem>
                                <asp:ListItem Value="P">Companheiro(a)</asp:ListItem>
                                <asp:ListItem Value="U">União Estável</asp:ListItem>
                                <asp:ListItem Value="O">Outro</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>
                <li class="liDadosConjug" style="max-width: 240px">
                    <ul>
                        <li style="clear:none !important; margin-left:-6px; padding-top:12px;">
                            <asp:CheckBox runat="server" ID="chkRestrPlano" Text="Restrição de Atendimento" CssClass="chkRest" />
                        </li>
                        <li class="liEspaco">
                            <label for="txtDtVencimento" title="Data de Vencimento" class="lbltxtDtVencimento">
                                Data de Vencimento</label>
                            <asp:TextBox ID="txtDtVencimento" ToolTip="Informe a Data de Vencimento da Restrição"
                                CssClass="txtDtVencimento campoData" runat="server"></asp:TextBox>
                        </li>
                        <li class="liTitInfCont" style="clear: both; margin-top: 3px; width: 251px">
                            <label class="lblTitInf">
                                Dados do Conjuge</label></li>
                        <li class="liCPFConjug">
                            <label for="txtCPFConjugResp" title="CPF">
                                CPF</label>
                            <asp:TextBox ID="txtCPFConjugResp" ToolTip="Informe o CPF do Conjuge do Funcionário"
                                CssClass="txtCPFResp" runat="server"></asp:TextBox>
                            <asp:CustomValidator ControlToValidate="txtCPFConjugResp" ID="CustomValidator1" runat="server"
                                ErrorMessage="CPF do Conjuge inválido" Text="*" Display="None" CssClass="validatorField"
                                EnableClientScript="false" OnServerValidate="cvValidaCPF"></asp:CustomValidator>
                        </li>
                        <li style="clear: none !important; margin-top: 12px;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkConjFunc" TextAlign="Right" runat="server"
                                ToolTip="Conjuge é Funcionário" Text="Funcionário" />
                        </li>
                        <li class="liClear">
                            <label for="txtNomeConjugeResp" title="Nome do Conjuge">
                                Nome do Conjuge</label>
                            <asp:TextBox ID="txtNomeConjugeResp" ToolTip="Informe o Nome do Conjuge" CssClass="txtNomeResp"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtDtConjuResp" title="Data de Nascimento">
                                Data de Nascimento</label>
                            <asp:TextBox ID="txtDtConjuResp" ToolTip="Informe a Data de Nascimento do Conjuge"
                                CssClass="txtDtNascResp campoData" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlSexoConjugResp" title="Sexo">
                                Sexo</label>
                            <asp:DropDownList ID="ddlSexoConjugResp" CssClass="ddlSexoResp" runat="server" ToolTip="Selecione o Sexo do Conjuge do Funcionário">
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li class="liBloco2">
            <ul>
                <li style="margin-right: 5px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Informação de Contato</label></li>
                        <li class="liClear">
                            <label for="txtEmailResp" title="Email">
                                Email</label>
                            <asp:TextBox ID="txtEmailResp" ToolTip="Informe o Email" CssClass="txtEmailResp"
                                runat="server" MaxLength="255"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtFacebookResp" title="Email">
                                Facebook</label>
                            <asp:TextBox ID="txtFacebookResp" ToolTip="Informe o Facebook" CssClass="txtFacebookResp"
                                runat="server" MaxLength="40" Width="170px"></asp:TextBox>
                        </li>
                        <%--<li class="liClear">
                            <label for="txtTwitterResp" title="Email">
                                Twitter</label>
                            <asp:TextBox ID="txtTwitterResp" ToolTip="Informe o Twitter" CssClass="txtTwitterResp"
                                runat="server" MaxLength="40" Width="170px"></asp:TextBox>
                        </li>--%>
                    </ul>
                </li>
                <li class="liBlocoIdent" style="margin-right: 5px; padding-left: 5px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Identidade</label></li>
                        <li class="liClear">
                            <label for="txtIdentidadeResp" title="Número" class="lblObrigatorio">
                                Número</label>
                            <asp:TextBox ID="txtIdentidadeResp" ToolTip="Informe o Número da Identidade" CssClass="txtIdentidadeResp"
                                runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIdentidadeResp"
                                ErrorMessage="Número do RG deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlIdentidadeUFResp" title="UF" class="lblObrigatorio">
                                UF</label>
                            <asp:DropDownList ID="ddlIdentidadeUFResp" ToolTip="Selecione a UF" CssClass="ddlUF"
                                runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="txtOrgEmissorResp" title="Orgão Emissor" class="lblObrigatorio">
                                Orgão Emissor</label>
                            <asp:TextBox ID="txtOrgEmissorResp" ToolTip="Informe o Orgão Emissor" CssClass="txtOrgEmissorResp"
                                runat="server" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtOrgEmissorResp"
                                ErrorMessage="Órgão Emissor deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="txtDtEmissaoResp" title="Data de Emissão" class="lblObrigatorio">
                                Data de Emissão</label>
                            <asp:TextBox ID="txtDtEmissaoResp" ToolTip="Informe a Data de Emissão" CssClass="txtDtEmissao campoData"
                                runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtDtEmissaoResp"
                                runat="server" ErrorMessage="Data de Emissão inválida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDtEmissaoResp"
                                ErrorMessage="Data de Emissão do RG deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                    </ul>
                </li>
                <li class="liBlocoTitEle" style="margin-right: 5px; padding-left: 5px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Título de Eleitor</label></li>
                        <li class="liClear">
                            <label for="txtNumeroTituloResp" title="Número do Título">
                                Número</label>
                            <asp:TextBox ID="txtNumeroTituloResp" ToolTip="Informe o Número do Título" CssClass="txtNumeroTituloResp"
                                runat="server" MaxLength="15"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlUfTituloResp" title="UF">
                                UF</label>
                            <asp:DropDownList ID="ddlUfTituloResp" ToolTip="Informe a UF" CssClass="ddlUF" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="txtZonaResp" title="Zona Eleitoral">
                                Zona</label>
                            <asp:TextBox ID="txtZonaResp" ToolTip="Informe a Zona Eleitoral" CssClass="txtZonaResp"
                                runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtSecaoResp" title="Seção">
                                Seção</label>
                            <asp:TextBox ID="txtSecaoResp" ToolTip="Informe a Seção" CssClass="txtSecaoResp"
                                runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li class="liBlocoDocDiv" style="margin-right: 5px; padding-left: 5px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Doc Diversos</label></li>
                        <li class="liClear">
                            <label for="txtPassaporteResp" title="Passaporte">
                                Passaporte</label>
                            <asp:TextBox ID="txtPassaporteResp" ToolTip="Informe o Passaporte do Responsável"
                                CssClass="txtPassaporteResp" runat="server" MaxLength="9"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtCarteiraSaudeResp" class="" title="Número do Cartão Saúde">
                                Cartão de Saúde</label>
                            <asp:TextBox ID="txtCarteiraSaudeResp" CssClass="txtCartaoSaudeResp" MaxLength="18"
                                runat="server" ToolTip="Número do Cartão Saúde"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li class="liBlocoFili" style="padding-left: 5px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Filiação</label></li>
                        <li class="liClear">
                            <label for="txtNomeMaeResp" title="Nome da Mãe">
                                Nome da Mãe</label>
                            <asp:TextBox ID="txtNomeMaeResp" ToolTip="Informe o Nome da Mãe" CssClass="txtNomeResp"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                        <li style="clear: none !important; margin-top: 12px;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkObtM" TextAlign="Right" runat="server"
                                ToolTip="Mãe está falecida?" Text="Falecida" />
                        </li> 
                        <li class="liClear">
                            <label for="txtNomePaiResp" title="Nome do Pai">
                                Nome do Pai</label>
                            <asp:TextBox ID="txtNomePaiResp" ToolTip="Informe o Nome do Pai" CssClass="txtNomeResp"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                        <li style="clear: none !important; margin-top: 12px;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkObtP" TextAlign="Right" runat="server"
                                ToolTip="Pai está falecido?" Text="Falecido" />
                        </li> 
                    </ul>
                </li>
            </ul>
        </li>
        <li class="liBloco3">
            <ul>
                <li>
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Informação Funcional</label></li>
                        <li class="liClear">
                            <label for="txtEmpresaResp" title="Empresa">
                                Entidade de Vinculo (Nome da empresa)</label>
                            <asp:TextBox ID="txtEmpresaResp" ToolTip="Informe a Empresa" CssClass="txtEmpresaResp"
                                MaxLength="100" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtCargHor" title="Carga Horária">
                                Car. H.</label>
                            <asp:TextBox ID="txtCargHor" ToolTip="Informe a carga horária" CssClass="txtCargHor"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtDtAdmissaoResp" title="Data de Admissão">
                                Data de Admissão</label>
                            <asp:TextBox ID="txtDtAdmissaoResp" ToolTip="Informe a Data de Admissão" CssClass="txtDtAdmissaoResp campoData"
                                runat="server"></asp:TextBox>
                        </li>
                        <%--<li class="liEspaco">
                            <label for="txtDtSaidaResp" title="Data de Saída">
                                Data de Saída</label>
                            <asp:TextBox ID="txtDtSaidaResp" ToolTip="Informe a Data de Desligamento da empresa"
                                CssClass="txtDtSaidaResp campoData" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtDtSaidaResp"
                                runat="server" ErrorMessage="Data de Saída inválida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                        </li>--%>
                        <li class="liEspaco">
                            <label for="txtFuncaoResp" title="Cargo / Função">
                                Cargo / Função</label>
                            <asp:DropDownList ID="ddlFuncaoResp" ToolTip="Selecione o cargo"
                                CssClass="ddlFuncaoResp" runat="server">
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txtFuncaoResp" ToolTip="Informe a Função" CssClass="txtFuncaoResp"
                                MaxLength="30" runat="server"></asp:TextBox>--%>
                        </li>
                        <li class="liClear">
                            <label for="txtDepartamentoResp" title="Lotação">
                                Lotação</label>
                            <asp:TextBox ID="txtDepartamentoResp" ToolTip="Informe a Lotação" CssClass="txtDepartamentoResp"
                                MaxLength="50" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtFoPag" title="Nº Folha de pagamanto">
                                Nº Folha de Pag.</label>
                            <asp:TextBox ID="txtFoPag" ToolTip="Informe o número da folha de pagamento" CssClass="txtFoPag"
                                MaxLength="50" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtEmailFuncionalResp" title="Email Funcional">
                                Email Funcional</label>
                            <asp:TextBox ID="txtEmailFuncionalResp" ToolTip="Informe o Email Funcional" CssClass="txtEmailFuncionalResp"
                                MaxLength="60" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <div>
                                <label for="txtTelEmpresaResp" title="Telefone">
                                    Telefone</label>
                                <asp:TextBox ID="txtTelEmpresaResp" ToolTip="Informe o Telefone" CssClass="txtTelEmpresaResp"
                                    runat="server"></asp:TextBox>
                            </div>
                            <div style="clear: both; margin-top: 0px;">
                                <asp:CheckBox CssClass="chkLocais" ID="chkWAFunc" TextAlign="Right" runat="server"
                                    ToolTip="Telefone funcional é WhatsApp?" Text="WhatsApp?" />
                            </div>
                        </li>
                        <li class="liClear">
                            <label for="txtSalarioBruto" title="Salário Bruto">
                                Salário Bruto</label>
                            <asp:TextBox ID="txtSalarioBruto" ToolTip="Informe o salário bruto" CssClass="txtSalarioBruto"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtDescSalario" title="Descontos Salariais">
                                Desc. Sal.</label>
                            <asp:TextBox ID="txtDescSalario" ToolTip="Informe o desconto salarial" CssClass="txtDescSalario"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtSalarioLiqui" title="Salário Líquido">
                                Sal. Líquido</label>
                            <asp:TextBox ID="txtSalarioLiqui" ToolTip="Informe o salário liquido" CssClass="txtSalarioLiqui"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtPercent" title="Percentual">
                                %</label>
                            <asp:TextBox ID="txtPercent" ToolTip="Informe o percentual" CssClass="txtPercent"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liTitInfCont" style="clear: both; margin-top: 5px;">
                            <label class="lblTitInf">
                                COPARTICIPAÇÃO</label>
                        </li>
                        <li class="liClear">
                            <label for="txtPercentMes" title="Percentual ao Mês">
                                %M.</label>
                            <asp:TextBox ID="txtPercentMes" ToolTip="Informe o percentual ao mês" CssClass="txtPercentMes"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtDevedor" title="R$ Devedor">
                                R$ Devedor</label>
                            <asp:TextBox ID="txtDevedor" ToolTip="Informe o valor devido" CssClass="txtDevedor"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtCredito" title="R$ Credito">
                                R$ Credito</label>
                            <asp:TextBox ID="txtCredito" ToolTip="Informe o crédito" CssClass="txtCredito"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtResult" title="Resultado">
                                R$ Result.</label>
                            <asp:TextBox ID="txtResult" ToolTip="Informe o resultado" CssClass="txtResult"
                                runat="server"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li class="liBlocoEndRes">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Endereço Residencial</label></li>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <li class="liClear">
                                    <label for="txtCepResp" title="Cep" class="lblObrigatorio">
                                        Cep</label>
                                    <asp:TextBox ID="txtCepResp" ToolTip="Informe o Cep" CssClass="txtCepResp" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCep" runat="server" ControlToValidate="txtCepResp"
                                        ErrorMessage="CEP deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li class="liPesqCEPR">
                                    <asp:ImageButton ID="btnPesqCEPR" runat="server" OnClick="btnPesquisarCepResp_Click"
                                        ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" class="btnPesqMat" CausesValidation="false" />
                                </li>
                                <li>
                                    <label for="txtLogradouroResp" class="lblObrigatorio" title="Logradouro">
                                        Endereço</label>
                                    <asp:TextBox ID="txtLogradouroResp" CssClass="txtLogradouroResp" ToolTip="Informe o Logradouro"
                                        runat="server" MaxLength="80"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLogradouro" runat="server" ControlToValidate="txtLogradouroResp"
                                        ErrorMessage="Endereço deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtNumeroResp" title="Número">
                                        Número</label>
                                    <asp:TextBox ID="txtNumeroResp" ToolTip="Informe o Número" CssClass="txtNumeroResp"
                                        runat="server" MaxLength="5"></asp:TextBox>
                                </li>
                                <li class="liClear">
                                    <label for="txtComplementoResp" title="Complemento">
                                        Complemento</label>
                                    <asp:TextBox ID="txtComplementoResp" ToolTip="Informe o Complemento" CssClass="txtComplementoResp"
                                        runat="server" MaxLength="30"></asp:TextBox>
                                </li>
                                <li class="liEspaco">
                                    <label for="ddlBairroResp" title="Bairro" class="lblObrigatorio">
                                        Bairro</label>
                                    <asp:DropDownList ID="ddlBairroResp" ToolTip="Selecione o Bairro" CssClass="ddlBairroResp"
                                        runat="server">
                                    </asp:DropDownList>
                                </li>
                                <li class="liClear">
                                    <label for="ddlCidadeResp" title="Cidade" class="lblObrigatorio">
                                        Cidade</label>
                                    <asp:DropDownList ID="ddlCidadeResp" ToolTip="Selecione a Cidade" CssClass="ddlCidadeResp"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-left: 5px;">
                                    <label for="ddlUfResp" title="UF" class="lblObrigatorio">
                                        UF</label>
                                    <asp:DropDownList ID="ddlUfResp" ToolTip="Selecione a UF" Width="40" CssClass="ddlUf"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvUf" runat="server" ControlToValidate="ddlUfResp"
                                        ErrorMessage="UF deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li style="clear: none; margin-left: 0px; margin-top: 14px;">
                                    <asp:CheckBox CssClass="chkLocais" ID="chkResPro" TextAlign="Right" runat="server"
                                        ToolTip="Responsável possui Residência Própria" Text="Residência Própria" />
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <li class="liTitInfCont" style="clear: both; margin-top: 5px;">
                            <label class="lblTitInf">
                                Endereço Comercial</label></li>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <li class="liClear">
                                    <label for="txtCepEmpresaResp" title="Cep">
                                        Cep</label>
                                    <asp:TextBox ID="txtCepEmpresaResp" ToolTip="Informe o Cep" CssClass="txtCepResp"
                                        runat="server"></asp:TextBox>
                                </li>
                                <li class="liPesqCEPR">
                                    <asp:ImageButton ID="btnPesquisarCepEndResp" runat="server" OnClick="btnPesquisarCepEmpResp_Click"
                                        ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" class="btnPesqMat" CausesValidation="false" />
                                </li>
                                <li>
                                    <label for="txtLogradouroEmpResp" title="Logradouro">
                                        Endereço</label>
                                    <asp:TextBox ID="txtLogradouroEmpResp" ToolTip="Informe o Logradouro" CssClass="txtLogradouroResp"
                                        MaxLength="100" runat="server"></asp:TextBox>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtNumeroEmpResp" title="Número">
                                        Número</label>
                                    <asp:TextBox ID="txtNumeroEmpResp" ToolTip="Informe o Número" CssClass="txtNumeroResp"
                                        MaxLength="5" runat="server"></asp:TextBox>
                                </li>
                                <li class="liClear">
                                    <label for="txtComplementoEmpResp" title="Complemento">
                                        Complemento</label>
                                    <asp:TextBox ID="txtComplementoEmpResp" ToolTip="Informe o Complemento" CssClass="txtComplementoResp"
                                        MaxLength="30" runat="server"></asp:TextBox>
                                </li>
                                <li class="liEspaco">
                                    <label for="ddlBairroEmpResp" title="Bairro">
                                        Bairro</label>
                                    <asp:DropDownList ID="ddlBairroEmpResp" ToolTip="Selecione o Bairro" CssClass="ddlBairroResp"
                                        runat="server">
                                    </asp:DropDownList>
                                </li>
                                <li class="liClear">
                                    <label for="ddlCidadeEmpResp" title="Cidade">
                                        Cidade</label>
                                    <asp:DropDownList ID="ddlCidadeEmpResp" ToolTip="Selecione a Cidade" CssClass="ddlCidadeResp"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeEmp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </li>
                                <li class="liUfEmp">
                                    <label for="ddlUfEmpResp" title="UF">
                                        UF</label>
                                    <asp:DropDownList ID="ddlUfEmpResp" Width="40" ToolTip="Selecione a UF" CssClass="ddlUf"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUfEmp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ul>
                </li>
                <li style="margin-right: 0px; margin-left: 10px;">
                    <ul>
                        <li class="liTitInfCont" style="background-color: #DDDDDD; width: 302px; text-align: center;
                            padding: 2px 0px;">
                            <label class="lblTitInf">
                                Posição Georeferencial Endereço Residencial</label></li>
                        <li class="liClear" style="margin-top: 5px;">
                            <div style="border: 1px solid #BEBEBE; width: 300px; height: 140px;">
                                <table id="tbMap" runat="server" visible="false" cellpadding="0" cellspacing="0"
                                    style="width: 98%; border: 0px;">
                                    <tr>
                                        <td class="mapa">
                                            <artem:GoogleMap ID="GMapa" CssClass="mapa" runat="server">
                                            </artem:GoogleMap>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </li>
                        <li id="liObsResponsavel" class="liClear" style="margin-top: 5px;">
                            <label for="txtObservacoesResp" style="font-weight: bold; text-transform: uppercase;"
                                title="Observações">
                                Observações</label>
                            <asp:TextBox ID="txtObservacoesResp" CssClass="txtObservacoesResp" ToolTip="Informe as Observações sobre o Responsável"
                                runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 250);"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <div>
                                <label for="ddlSituacaoResp" title="Situação Atual">
                                    Situação Atual</label>
                                <asp:DropDownList ID="ddlSituacaoResp" ToolTip="Selecione a Situação Atual do Responsável"
                                    CssClass="ddlSituacaoResp" runat="server">
                                    <asp:ListItem Value="A">Ativo</asp:ListItem>
                                    <asp:ListItem Value="I">Inativo</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div>
                                <label for="txtDtSituacaoResp" title="Data da Situação">
                                    Data Situação</label>
                                <asp:TextBox ID="txtDtSituacaoResp" Enabled="false" CssClass="txtDtSituacaoResp campoData"
                                    runat="server"></asp:TextBox>
                            </div>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtNumeroResp").mask("?999999");
            $(".txtCepResp").mask("99999-999");
            $(".txtCepEmpresa").mask("99999-999");
        });
        $(document).ready(function () {
            $(".txtCepResp").mask("99999-999");
            $(".txtCepEmpresa").mask("99999-999");
            $(".txtNISResp").mask("?999999999999999");
            $(".txtNumeroResp").mask("?999999");
            if ($('.txtTelResidencialResp').val().length <= 10) {
                $('.txtTelResidencialResp').mask("(99)9?999-99999");
            } else {
                $('.txtTelResidencialResp').mask("(99)9?9999-9999");
            }
            $(".txtTelEmpresaResp").mask("(99) 9999-9999");
            $(".txtCPFResp").mask("999.999.999-99");
            $(".txtPassaporteResp").mask("?999999999");
            $(".txtQtdMenoresResp").mask("?99");
            $(".txtQtdMaioresResp").mask("?99");
            $(".txtSalarioBruto").mask("?999.999,00");
            $(".txtDescSalario").mask("?999.999,00");
            $(".txtSalarioLiqui").mask("?999.999,00");
            $(".txtDevedor").mask("?999.999,99");
            $(".txtCredito").mask("?999.999,99");
            $(".txtResult").mask("?999.999,99");
        });
    </script>
</asp:Content>
