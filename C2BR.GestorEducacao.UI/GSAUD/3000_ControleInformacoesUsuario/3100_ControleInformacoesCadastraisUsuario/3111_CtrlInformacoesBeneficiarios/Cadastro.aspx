<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" 
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3111_CtrlInformacoesBeneficiarios.Cadastro" %>

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
        .liFotoAlu
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
        .liSexoAlu
        {
            margin-left: 21px;
        }
        .liDtNascAlu, .liCursoFormacao
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
        .liDadosInfDiv, .liDadosResp, .liBlocoIdent, .liBlocoTitEle, .liBlocoDocDiv, .liBlocoFili, .liBlocoEndRes
        {
            border-left: 1px solid #BEBEBE;
            padding-left: 10px;
        }
        .liDadosInfDiv, .liDadosResp
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
        .fldFotoAlu
        {
            border: none;
            width: 90px;
            height: 110px;
        }
        .txtNomeAlu
        {
            width: 135px;
            text-transform: uppercase;
        }
        .txtDtNascAlu, .txtDtEmissaoAlu, .txtDtAdmissaoAlu, .txtCPFRespAlu
        {
            width: 60px;
        }
        .txtNumeroTituloAlu, .txtNaturalidadeAlu
        {
            width: 80px;
        }
        .ddlSexoAlu
        {
            width: 65px;
        }
        .ddlGrauInstrucaoAlu
        {
            width: 40px;
        }
        .ddlCursoFormacaoAlu, .txtFuncaoAlu
        {
            width: 185px;
        }
        .ddlEstadoCivilAlu
        {
            width: 142px;
        }
        .ddlRendaAlu, .txtCPFAlu
        {
            width: 82px;
        }
        .txtIdentidadeAlu
        {
            width: 104px;
        }
        .txtOrgEmissorAlu, .txtNISAlu, .txtTelEmpresaAlu
        {
            width: 65px;
        }
        .txtComplementoAlu
        {
            width: 83px;
        }
        .txtZonaAlu, .txtSecaoAlu, .txtNumeroAlu
        {
            width: 40px;
        }
        .txtLogradouroAlu
        {
            width: 100px;
        }
        .txtTelResidencialAlu, .txtApelidoAlu
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
        .ddlCidadeAlu, .ddlBairroAlu
        {
            width: 100px;
        }
        .txtCepAlu
        {
            width: 56px;
        }
        .txtEmpresaAlu, .txtEmailAlu
        {
            width: 170px;
        }
        .ddlFuncaoAlu
        {
            width: 130px;
        }
        .txtEmailFuncionalAlu, .txtFuncaoAlu, .txtDepartamentoAlu
        {
            width: 128px; 
        }
        .txtFoPag
        {
            width: 78px;
        }
        .txtQtdMaioresAlu, .txtQtdMenoresAlu
        {
            width: 55px;
        }
        .chkLocais label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .ddlSituacaoAlu, .ddlDeficienciaAlu
        {
            width: 70px;
        }
        .ddlNacionalidadeAlu
        {
            width: 40px;
        }
        .ddlTpSangueAlu
        {
            width: 35px;
            clear: both;
        }
        .ddlStaTpSangueAlu, .txtCargHor
        {
            width: 30px;
        }
        .ddlCorRacaAlu
        {
            width: 68px;
        }
        .lblTitInf
        {
            text-transform: uppercase;
            font-weight: bold;
            font-size: 1.0em;
        }
        .txtPassaporteAlu, .txtCartaoSaudeAlu
        {
            width: 98px;
        }
        .txtObservacoesAlu
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
        <li class="liFotoAlu">
            <fieldset class="fldFotoAlu">
                <uc1:ControleImagem ID="upImagemAlu" runat="server" />
            </fieldset>
        </li>
        <li class="liBloco1">
            <ul>
                <li class="liDados1">
                    <ul>
                        <li>
                            <label for="txtCPFAlu" title="CPF" class="lblObrigatorio">
                                CPF</label>
                            <asp:TextBox ID="txtCPFAlu" ToolTip="Informe o CPF" CssClass="txtCPFAlu" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCPF" runat="server" ControlToValidate="txtCPFAlu"
                                ErrorMessage="CPF deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ControlToValidate="txtCPFAlu" ID="cvCPF" runat="server" ErrorMessage="CPF do Beneficiário inválido"
                                Text="*" Display="None" CssClass="validatorField" EnableClientScript="false"
                                OnServerValidate="cvValidaCPF"></asp:CustomValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="txtNISAlu" title="NIS">
                                Nº CONT. NIS</label>
                            <asp:TextBox ID="txtNISAlu" ToolTip="Informe o NIS" CssClass="txtNISAlu" runat="server"
                                MaxLength="15"></asp:TextBox>
                        </li>
                        <li style="clear: none !important; margin-top: 12px;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkAluFunc" TextAlign="Right" runat="server"
                                ToolTip="Beneficiário é Funcionário" Text="Funcionário" />
                        </li>
                        <li class="liDtNascAlu">
                            <label for="txtDtNascAlu" title="Data de Nascimento" class="lblObrigatorio">
                                Data Nascimento</label>
                            <asp:TextBox ID="txtDtNascAlu" ToolTip="Informe a Data de Nascimento" CssClass="txtDtNasc campoData"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDtNasc" runat="server" ControlToValidate="txtDtNascAlu"
                                ErrorMessage="Data de Nascimento deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDtNasc" ControlToValidate="txtDtNascAlu"
                                runat="server" ErrorMessage="Data de Nascimento inválida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                        </li>
                        <li class="liClear">
                            <label for="txtNomeAlu" class="lblObrigatorio" title="Nome do Beneficiário">
                                Nome</label>
                            <asp:TextBox ID="txtNomeAlu" CssClass="txtNomeAlu" ToolTip="Informe o Nome do Beneficiário"
                                runat="server" MaxLength="60"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNomeAlu"
                                ErrorMessage="Nome deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="txtApelidoAlu" title="Apelido">
                                Apelido</label>
                            <asp:TextBox ID="txtApelidoAlu" ToolTip="Informe o Apelido do Beneficiário" CssClass="txtApelidoAlu"
                                runat="server" MaxLength="15">
                            </asp:TextBox>
                        </li>
                        <li class="liSexoAlu">
                            <label for="ddlSexoAlu" title="Sexo">
                                Sexo</label>
                            <asp:DropDownList ID="ddlSexoAlu" ToolTip="Selecione o Sexo" CssClass="ddlSexoAlu"
                                runat="server">
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <li class="liClear">
                                    <label for="ddlNacionalidadeAlu" title="Nacionalidade do Beneficiário">
                                        Nac.</label>
                                    <asp:DropDownList ID="ddlNacionalidadeAlu" CssClass="ddlNacionalidadeAlu" runat="server"
                                        ToolTip="Informe a Nacionalidade do Beneficiário" AutoPostBack="true" OnSelectedIndexChanged="ddlNacionalidadeAlu_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtNaturalidadeAlu" title="Cidade de Naturalidade do Beneficiário">
                                        Naturalidade</label>
                                    <asp:TextBox ID="txtNaturalidadeAlu" CssClass="txtNaturalidadeAlu" runat="server"
                                        ToolTip="Informe a Cidade de Naturalidade do Beneficiário" MaxLength="40"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="ddlUfNacionalidadeAlu" title="UF de Nacionalidade do Beneficiário">
                                        UF</label>
                                    <asp:DropDownList ID="ddlUfNacionalidadeAlu" CssClass="campoUf" runat="server" ToolTip="Informe a UF de Nacionalidade">
                                    </asp:DropDownList>
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <li class="liOrigem">
                            <label for="ddlOrigemAlu" class="" title="Origem">
                                Origem</label>
                            <asp:DropDownList ID="ddlOrigemAlu" Style="width: 100px;" runat="server" ToolTip="Origem">
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
                                    <label for="ddlGrauInstrucaoAlu" class="lblObrigatorio" title="Grau de Instrução">
                                        Grau Instr.</label>
                                    <asp:DropDownList ID="ddlGrauInstrucaoAlu" ToolTip="Selecione o Grau de Instrução"
                                        CssClass="ddlGrauInstrucaoAlu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrauInstrucao_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGrauInstrucaoAlu"
                                        ErrorMessage="Grau de Instrução deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtTelResidencialAlu" title="Telefone Residencial">
                                        Tel. Residencial</label>
                                    <asp:TextBox ID="txtTelResidencialAlu" ToolTip="Informe o Telefone Residencial"
                                        CssClass="txtTelResidencialAlu" runat="server"></asp:TextBox>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtTelCelularAlu" title="Telefone Celular">
                                        Tel. Celular</label>
                                    <asp:TextBox ID="txtTelCelularAlu" ToolTip="Informe o Telefone Celular" CssClass="txtTelResidencialAlu"
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
                            <asp:DropDownList ID="ddlTpSangueFAlu" ToolTip="Selecione o Tipo de Sangue" CssClass="ddlTpSangueAlu"
                                runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="A">A</asp:ListItem>
                                <asp:ListItem Value="B">B</asp:ListItem>
                                <asp:ListItem Value="AB">AB</asp:ListItem>
                                <asp:ListItem Value="O">O</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlStaSangueFAlu" ToolTip="Selecione o Status do Tipo de Sangue"
                                CssClass="ddlStaTpSangueAlu" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="P">+</asp:ListItem>
                                <asp:ListItem Value="N">-</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlRendaAlu" title="Renda Familiar">
                                Renda Familiar</label>
                            <asp:DropDownList ID="ddlRendaAlu" ToolTip="Selecione a Renda Familiar" CssClass="ddlRendaAlu"
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
                            <label for="ddlCorRacaAlu" title="Etnia do Beneficiário">
                                Etnia</label>
                            <asp:DropDownList ID="ddlCorRacaAlu" ToolTip="Informe a Etnia do Beneficiário" CssClass="ddlCorRacaAlu"
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
                        <%--<li class="liQtdMenores">
                            <label for="txtQtdMenoresAlu" title="Quantidade de Dependentes Menores">
                                Dep Menores</label>
                            <asp:TextBox ID="txtQtdMenoresAlu" ToolTip="Informe a Quantidade de Dependentes Menores"
                                CssClass="txtQtdMenoresAlu" runat="server" MaxLength="2"></asp:TextBox>
                        </li>--%>
                        <li class="liEspaco">
                            <label for="ddlDeficienciaAlu" title="Deficiência">
                                Deficiência</label>
                            <asp:DropDownList ID="ddlDeficienciaAlu" ToolTip="Selecione a Deficiência" CssClass="ddlDeficienciaAlu"
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
                        <%--<li class="liQtdMaiores">
                            <label for="txtQtdMaioresAlu" title="Quantidade de Dependentes Maiores">
                                Dep Maiores</label>
                            <asp:TextBox ID="txtQtdMaioresAlu" ToolTip="Informe a Quantidade de Dependentes Maiores"
                                CssClass="txtQtdMaioresAlu" runat="server" MaxLength="2"></asp:TextBox>
                        </li>--%>
                        <li style="clear:both; margin-top:-5px;">
                            <label for="ddlEstadoCivilAlu" title="Estado Civil">
                                Estado Civil</label>
                            <asp:DropDownList ID="ddlEstadoCivilAlu" ToolTip="Selecione o Estado Civil" CssClass="ddlEstadoCivilAlu"
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
                <li class="liDadosResp" style="max-width: 240px">
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
                                Pendências</label></li>
                        <li class="liEspaco" style="clear: none !important;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkDocPend" TextAlign="Right" runat="server"
                                ToolTip="Beneficiário é Funcionário" Text="Documento" />
                        </li>
                        <li style="clear: none !important;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkFinPend" TextAlign="Right" runat="server"
                                ToolTip="Financeiro Pendente" Text="Financeiro" />
                        </li>
                        <li style="clear: none !important;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkPerPend" TextAlign="Right" runat="server"
                                ToolTip="Pericia Pendente" Text="Pericia" />
                        </li>
                        <li class="liTitInfCont" style="clear: both; margin-top: 3px; width: 251px">
                            <label class="lblTitInf">
                                Dados do Associado</label></li>
                        <li class="liCPFResp">
                            <label for="txtCPFRespAlu" title="CPF">
                                CPF</label>
                            <asp:TextBox ID="txtCPFRespAlu" ToolTip="Informe o CPF do Associado"
                                CssClass="txtCPFRespAlu" runat="server" Enabled="false"></asp:TextBox>
                            <asp:CustomValidator ControlToValidate="txtCPFRespAlu" ID="CustomValidator1" runat="server"
                                ErrorMessage="CPF do associado inválido" Text="*" Display="None" CssClass="validatorField"
                                EnableClientScript="false" OnServerValidate="cvValidaCPF"></asp:CustomValidator>
                        </li>
                        <li style="clear: none !important; margin-top: 12px;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkRespFunc" TextAlign="Right" runat="server"
                                ToolTip="Associado é Funcionário" Text="Funcionário" Enabled="false"/>
                        </li>
                        <li class="liEspaco">
                            <label for="txtDtRespAlu" title="Data de Nascimento">
                                Data Nascimento</label>
                            <asp:TextBox ID="txtDtRespAlu" ToolTip="Informe a Data de Nascimento do Associado"
                                CssClass="txtDtNascAlu campoData" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtNomeRespAlu" title="Nome do Associado">
                                Nome do Associado</label>
                            <asp:TextBox ID="txtNomeRespAlu" ToolTip="Informe o Nome do Associado" CssClass="txtNomeAlu"
                                runat="server" MaxLength="60" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlSexoRespAlu" title="Sexo">
                                Sexo</label>
                            <asp:DropDownList ID="ddlSexoRespAlu" CssClass="ddlSexoAlu" runat="server" Enabled="false" ToolTip="Selecione o Sexo do Associado">
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
                            <label for="txtEmailAlu" title="Email">
                                Email</label>
                            <asp:TextBox ID="txtEmailAlu" ToolTip="Informe o Email" CssClass="txtEmailAlu"
                                runat="server" MaxLength="255"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtFacebookAlu" title="Email">
                                Facebook</label>
                            <asp:TextBox ID="txtFacebookAlu" ToolTip="Informe o Facebook" CssClass="txtFacebookAlu"
                                runat="server" MaxLength="40" Width="170px"></asp:TextBox>
                        </li>
                        <%--<li class="liClear">
                            <label for="txtTwitterAlu" title="Email">
                                Twitter</label>
                            <asp:TextBox ID="txtTwitterAlu" ToolTip="Informe o Twitter" CssClass="txtTwitterAlu"
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
                            <label for="txtIdentidadeAlu" title="Número" class="lblObrigatorio">
                                Número</label>
                            <asp:TextBox ID="txtIdentidadeAlu" ToolTip="Informe o Número da Identidade" CssClass="txtIdentidadeAlu"
                                runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIdentidadeAlu"
                                ErrorMessage="Número do RG deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlIdentidadeUFAlu" title="UF" class="lblObrigatorio">
                                UF</label>
                            <asp:DropDownList ID="ddlIdentidadeUFAlu" ToolTip="Selecione a UF" CssClass="ddlUF"
                                runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="txtOrgEmissorAlu" title="Orgão Emissor" class="lblObrigatorio">
                                Orgão Emissor</label>
                            <asp:TextBox ID="txtOrgEmissorAlu" ToolTip="Informe o Orgão Emissor" CssClass="txtOrgEmissorAlu"
                                runat="server" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtOrgEmissorAlu"
                                ErrorMessage="Órgão Emissor deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="txtDtEmissaoAlu" title="Data de Emissão" class="lblObrigatorio">
                                Data de Emissão</label>
                            <asp:TextBox ID="txtDtEmissaoAlu" ToolTip="Informe a Data de Emissão" CssClass="txtDtEmissao campoData"
                                runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtDtEmissaoAlu"
                                runat="server" ErrorMessage="Data de Emissão inválida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDtEmissaoAlu"
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
                            <label for="txtNumeroTituloAlu" title="Número do Título">
                                Número</label>
                            <asp:TextBox ID="txtNumeroTituloAlu" ToolTip="Informe o Número do Título" CssClass="txtNumeroTituloAlu"
                                runat="server" MaxLength="15"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlUfTituloAlu" title="UF">
                                UF</label>
                            <asp:DropDownList ID="ddlUfTituloAlu" ToolTip="Informe a UF" CssClass="ddlUF" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="txtZonaAlu" title="Zona Eleitoral">
                                Zona</label>
                            <asp:TextBox ID="txtZonaAlu" ToolTip="Informe a Zona Eleitoral" CssClass="txtZonaAlu"
                                runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtSecaoAlu" title="Seção">
                                Seção</label>
                            <asp:TextBox ID="txtSecaoAlu" ToolTip="Informe a Seção" CssClass="txtSecaoAlu"
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
                            <label for="txtPassaporteAlu" title="Passaporte">
                                Passaporte</label>
                            <asp:TextBox ID="txtPassaporteAlu" ToolTip="Informe o Passaporte do Beneficiário"
                                CssClass="txtPassaporteAlu" runat="server" MaxLength="9"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtCarteiraSaudeAlu" class="" title="Número do Cartão Saúde">
                                Cartão de Saúde</label>
                            <asp:TextBox ID="txtCarteiraSaudeAlu" CssClass="txtCartaoSaudeAlu" MaxLength="18"
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
                            <label for="txtNomeMaeAlu" title="Nome da Mãe">
                                Nome da Mãe</label>
                            <asp:TextBox ID="txtNomeMaeAlu" ToolTip="Informe o Nome da Mãe" CssClass="txtNomeAlu"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                        <li style="clear: none !important; margin-top: 12px;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkObtM" TextAlign="Right" runat="server"
                                ToolTip="Mãe está falecida?" Text="Falecida" />
                        </li> 
                        <li class="liClear">
                            <label for="txtNomePaiAlu" title="Nome do Pai">
                                Nome do Pai</label>
                            <asp:TextBox ID="txtNomePaiAlu" ToolTip="Informe o Nome do Pai" CssClass="txtNomeAlu"
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
                            <label for="txtEmpresaAlu" title="Empresa">
                                Entidade de Vinculo (Nome da empresa)</label>
                            <asp:TextBox ID="txtEmpresaAlu" ToolTip="Informe a Empresa" CssClass="txtEmpresaAlu"
                                MaxLength="100" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtCargHor" title="Carga Horária">
                                Car. H.</label>
                            <asp:TextBox ID="txtCargHor" ToolTip="Informe a carga horária" CssClass="txtCargHor"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtDtAdmissaoAlu" title="Data de Admissão">
                                Data de Admissão</label>
                            <asp:TextBox ID="txtDtAdmissaoAlu" ToolTip="Informe a Data de Admissão" CssClass="txtDtAdmissaoAlu campoData"
                                runat="server"></asp:TextBox>
                        </li>
                        <%--<li class="liEspaco">
                            <label for="txtDtSaidaAlu" title="Data de Saída">
                                Data de Saída</label>
                            <asp:TextBox ID="txtDtSaidaAlu" ToolTip="Informe a Data de Desligamento da empresa"
                                CssClass="txtDtSaidaAlu campoData" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtDtSaidaAlu"
                                runat="server" ErrorMessage="Data de Saída inválida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                        </li>--%>
                        <li class="liEspaco">
                            <label for="txtFuncaoAlu" title="Cargo / Função">
                                Cargo / Função</label>
                            <asp:DropDownList ID="ddlFuncaoAlu" ToolTip="Selecione o cargo"
                                CssClass="ddlFuncaoAlu" runat="server">
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txtFuncaoAlu" ToolTip="Informe a Função" CssClass="txtFuncaoAlu"
                                MaxLength="30" runat="server"></asp:TextBox>--%>
                        </li>
                        <li class="liClear">
                            <label for="txtDepartamentoAlu" title="Lotação">
                                Lotação</label>
                            <asp:TextBox ID="txtDepartamentoAlu" ToolTip="Informe a Lotação" CssClass="txtDepartamentoAlu"
                                MaxLength="50" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtFoPag" title="Nº Folha de pagamanto">
                                Nº Folha de Pag.</label>
                            <asp:TextBox ID="txtFoPag" ToolTip="Informe o número da folha de pagamento" CssClass="txtFoPag"
                                MaxLength="50" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtEmailFuncionalAlu" title="Email Funcional">
                                Email Funcional</label>
                            <asp:TextBox ID="txtEmailFuncionalAlu" ToolTip="Informe o Email Funcional" CssClass="txtEmailFuncionalAlu"
                                MaxLength="60" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <div>
                                <label for="txtTelEmpresaAlu" title="Telefone">
                                    Telefone</label>
                                <asp:TextBox ID="txtTelEmpresaAlu" ToolTip="Informe o Telefone" CssClass="txtTelEmpresaAlu"
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
                                    <label for="txtCepAlu" title="Cep" class="lblObrigatorio">
                                        Cep</label>
                                    <asp:TextBox ID="txtCepAlu" ToolTip="Informe o Cep" CssClass="txtCepAlu" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCep" runat="server" ControlToValidate="txtCepAlu"
                                        ErrorMessage="CEP deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li class="liPesqCEPR">
                                    <asp:ImageButton ID="btnPesqCEPR" runat="server" OnClick="btnPesquisarCepAlu_Click"
                                        ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" class="btnPesqMat" CausesValidation="false" />
                                </li>
                                <li>
                                    <label for="txtLogradouroAlu" class="lblObrigatorio" title="Logradouro">
                                        Endereço</label>
                                    <asp:TextBox ID="txtLogradouroAlu" CssClass="txtLogradouroAlu" ToolTip="Informe o Logradouro"
                                        runat="server" MaxLength="80"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLogradouro" runat="server" ControlToValidate="txtLogradouroAlu"
                                        ErrorMessage="Endereço deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtNumeroAlu" title="Número">
                                        Número</label>
                                    <asp:TextBox ID="txtNumeroAlu" ToolTip="Informe o Número" CssClass="txtNumeroAlu"
                                        runat="server" MaxLength="5"></asp:TextBox>
                                </li>
                                <li class="liClear">
                                    <label for="txtComplementoAlu" title="Complemento">
                                        Complemento</label>
                                    <asp:TextBox ID="txtComplementoAlu" ToolTip="Informe o Complemento" CssClass="txtComplementoAlu"
                                        runat="server" MaxLength="30"></asp:TextBox>
                                </li>
                                <li class="liEspaco">
                                    <label for="ddlBairroAlu" title="Bairro" class="lblObrigatorio">
                                        Bairro</label>
                                    <asp:DropDownList ID="ddlBairroAlu" ToolTip="Selecione o Bairro" CssClass="ddlBairroAlu"
                                        runat="server">
                                    </asp:DropDownList>
                                </li>
                                <li class="liClear">
                                    <label for="ddlCidadeAlu" title="Cidade" class="lblObrigatorio">
                                        Cidade</label>
                                    <asp:DropDownList ID="ddlCidadeAlu" ToolTip="Selecione a Cidade" CssClass="ddlCidadeAlu"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-left: 5px;">
                                    <label for="ddlUfAlu" title="UF" class="lblObrigatorio">
                                        UF</label>
                                    <asp:DropDownList ID="ddlUfAlu" ToolTip="Selecione a UF" Width="40" CssClass="ddlUf"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvUf" runat="server" ControlToValidate="ddlUfAlu"
                                        ErrorMessage="UF deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li class="liClear" style="clear: none; margin-left: 0px;">
                                    <asp:CheckBox CssClass="chkLocais" ID="chkResPro" TextAlign="Right" runat="server"
                                        ToolTip="Beneficiário possui Residência Própria" Text="Residência Própria" />
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <li class="liTitInfCont" style="clear: both; margin-top: 5px;">
                            <label class="lblTitInf">
                                Endereço Comercial</label></li>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <li class="liClear">
                                    <label for="txtCepEmpresaAlu" title="Cep">
                                        Cep</label>
                                    <asp:TextBox ID="txtCepEmpresaAlu" ToolTip="Informe o Cep" CssClass="txtCepAlu"
                                        runat="server"></asp:TextBox>
                                </li>
                                <li class="liPesqCEPR">
                                    <asp:ImageButton ID="btnPesquisarCepEndAlu" runat="server" OnClick="btnPesquisarCepEmpAlu_Click"
                                        ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" class="btnPesqMat" CausesValidation="false" />
                                </li>
                                <li>
                                    <label for="txtLogradouroEmpAlu" title="Logradouro">
                                        Endereço</label>
                                    <asp:TextBox ID="txtLogradouroEmpAlu" ToolTip="Informe o Logradouro" CssClass="txtLogradouroAlu"
                                        MaxLength="100" runat="server"></asp:TextBox>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtNumeroEmpAlu" title="Número">
                                        Número</label>
                                    <asp:TextBox ID="txtNumeroEmpAlu" ToolTip="Informe o Número" CssClass="txtNumeroAlu"
                                        MaxLength="5" runat="server"></asp:TextBox>
                                </li>
                                <li class="liClear">
                                    <label for="txtComplementoEmpAlu" title="Complemento">
                                        Complemento</label>
                                    <asp:TextBox ID="txtComplementoEmpAlu" ToolTip="Informe o Complemento" CssClass="txtComplementoAlu"
                                        MaxLength="30" runat="server"></asp:TextBox>
                                </li>
                                <li class="liEspaco">
                                    <label for="ddlBairroEmpAlu" title="Bairro">
                                        Bairro</label>
                                    <asp:DropDownList ID="ddlBairroEmpAlu" ToolTip="Selecione o Bairro" CssClass="ddlBairroAlu"
                                        runat="server">
                                    </asp:DropDownList>
                                </li>
                                <li class="liClear">
                                    <label for="ddlCidadeEmpAlu" title="Cidade">
                                        Cidade</label>
                                    <asp:DropDownList ID="ddlCidadeEmpAlu" ToolTip="Selecione a Cidade" CssClass="ddlCidadeAlu"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidadeEmp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </li>
                                <li class="liUfEmp">
                                    <label for="ddlUfEmpAlu" title="UF">
                                        UF</label>
                                    <asp:DropDownList ID="ddlUfEmpAlu" Width="40" ToolTip="Selecione a UF" CssClass="ddlUf"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUfEmp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ul>
                </li>
                <li class="liBlocoDocDiv" style="margin-right: 5px; padding-left: 5px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Controles</label></li>
                        <li class="liTitInfCont liClear">
                            <label class="lblTitInf">
                                Vínculo</label></li>
                        <li class="liClear">
                            <label for="txtDtSituacaoAlu" title="Data da Situação">
                                Inicial</label>
                            <asp:TextBox ID="TextBox1" Enabled="false" CssClass="txtDtSituacaoAlu campoData"
                                runat="server"></asp:TextBox></li>
                        <li class="liClear">
                            <label for="txtDtSituacaoAlu" title="Data da Situação">
                                Último</label>
                            <asp:TextBox ID="TextBox2" Enabled="false" CssClass="txtDtSituacaoAlu campoData"
                                runat="server"></asp:TextBox></li>
                        <li class="liTitInfCont liClear">
                            <label class="lblTitInf">
                                Carência</label></li>
                        <li class="liClear">
                            <label for="txtDtSituacaoAlu" title="Data da Situação">
                                Inicial</label>
                            <asp:TextBox ID="TextBox3" Enabled="false" CssClass="txtDtSituacaoAlu campoData"
                                runat="server"></asp:TextBox></li>
                        <li class="liClear">
                            <label for="txtDtSituacaoAlu" title="Data da Situação">
                                Último</label>
                            <asp:TextBox ID="TextBox4" Enabled="false" CssClass="txtDtSituacaoAlu campoData"
                                runat="server"></asp:TextBox></li>
                        <li class="liTitInfCont liClear">
                            <label class="lblTitInf">
                                Bloqueio</label></li>
                        <li class="liClear">
                            <label for="txtDtSituacaoAlu" title="Data da Situação">
                                Inicial</label>
                            <asp:TextBox ID="TextBox5" Enabled="false" CssClass="txtDtSituacaoAlu campoData"
                                runat="server"></asp:TextBox></li>
                        <li class="liClear">
                            <label for="txtDtSituacaoAlu" title="Data da Situação">
                                Último</label>
                            <asp:TextBox ID="TextBox6" Enabled="false" CssClass="txtDtSituacaoAlu campoData"
                                runat="server"></asp:TextBox></li>
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
                        <li id="liObsBeneficiario" class="liClear" style="margin-top: 5px;">
                            <label for="txtObservacoesAlu" style="font-weight: bold; text-transform: uppercase;"
                                title="Observações">
                                Observações</label>
                            <asp:TextBox ID="txtObservacoesAlu" CssClass="txtObservacoesAlu" ToolTip="Informe as Observações sobre o Beneficiário"
                                runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 250);"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <div>
                                <label for="ddlSituacaoAlu" title="Situação Atual">
                                    Situação Atual</label>
                                <asp:DropDownList ID="ddlSituacaoAlu" ToolTip="Selecione a Situação Atual do Beneficiário"
                                    CssClass="ddlSituacaoAlu" runat="server">
                                    <asp:ListItem Value="A">Ativo</asp:ListItem>
                                    <asp:ListItem Value="I">Inativo</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div>
                                <label for="txtDtSituacaoAlu" title="Data da Situação">
                                    Data Situação</label>
                                <asp:TextBox ID="txtDtSituacaoAlu" Enabled="false" CssClass="txtDtSituacaoAlu campoData"
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
            $(".txtNumeroAlu").mask("?999999");
            $(".txtCepAlu").mask("99999-999");
            $(".txtCepEmpresa").mask("99999-999");
        });
        $(document).ready(function () {
            $(".txtCepAlu").mask("99999-999");
            $(".txtCepEmpresa").mask("99999-999");
            $(".txtNISAlu").mask("?999999999999999");
            $(".txtNumeroAlu").mask("?999999");
            if ($('.txtTelResidencialAlu').val().length <= 10) {
                $('.txtTelResidencialAlu').mask("(99)9?999-99999");
            } else {
                $('.txtTelResidencialAlu').mask("(99)9?9999-9999");
            }
            $(".txtTelEmpresaAlu").mask("(99) 9999-9999");
            $(".txtCPFAlu").mask("999.999.999-99");
            $(".txtPassaporteAlu").mask("?999999999");
            $(".txtQtdMenoresAlu").mask("?99");
            $(".txtQtdMaioresAlu").mask("?99");
            $(".txtSalarioBruto").mask("?999.999,00");
            $(".txtDescSalario").mask("?999.999,00");
            $(".txtSalarioLiqui").mask("?999.999,00");
            $(".txtDevedor").mask("?999.999,99");
            $(".txtCredito").mask("?999.999,99");
            $(".txtResult").mask("?999.999,99");
        });
    </script>
</asp:Content>
