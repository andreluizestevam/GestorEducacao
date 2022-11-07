<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3611_CadastramentoAlunos.Cadastro"
    EnableEventValidation="false" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="artem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1035px;
            margin: 15px auto auto 0px !important;
        }
        .ulDados li input
        {
            margin-bottom: 0;
        }
        .ulDados li fieldset ul li
        {
            margin-top: 0;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-top: 0px;
        }
        .lisepPeriodo
        {
            padding-top: 14px;
        }
        .liInstApoioAlu
        {
            margin-top: 10px !important;
        }
        .liDataNascimentoAlu, .liSexoAlu
        {
            margin-left: 10px;
        }
        .liClear
        {
            clear: both !important;
        }
        .liNisAlu
        {
            margin-left: 2px;
        }
        .liPeriodoAte
        {
            margin-top: 12px !important;
        }
        .liPeriodoDe
        {
            margin-right: 0px;
        }
        .liFoto
        {
            float: left !important;
            margin-right: 0 !important;
        }
        .liProgSociaisAlu
        {
            margin-top: 5px !important;
        }
        .liBloco2, .liBloco3, .liBloco5, .liBloco6
        {
            border-left: 1px solid #BEBEBE;
            padding-left: 5px;
        }
        .liBloco4
        {
            margin-left: 5px;
            clear: both !important;
        }
        .liPesqCEPR
        {
            margin-top: 14px !important;
            margin-left: -3px;
        }
        .liListaEndereco
        {
            margin-top: 13px !important;
            margin-left: -3px;
        }
        
        /*--> CSS DADOS */
        fieldset
        {
            padding: 0px 0px 5px 5px;
            margin: 0;
        }
        .fldDadosMatricula
        {
            margin-top: 3px;
        }
        .fldFotoAluno
        {
            border: none;
            width: 80px;
            height: 100px;
        }
        #ControleImagem .liControleImagemComp .fakefile
        {
            width: 75px !important;
        }
        .divInstApoioAlu
        {
            height: 50px;
            width: 148px;
            overflow-y: scroll;
            border: solid 1px #CCCCCC;
            background-color: #FFFFF0;
        }
        .divProgSociaisAlu
        {
            height: 65px;
            width: 170px;
            overflow-y: scroll;
            border: solid 1px #CCCCCC;
            margin-bottom: 5px;
            background-color: #FFFFF0;
        }
        .divInstApoioAlu table tr td label, .divProgSociaisAlu table tr td label
        {
            display: inline;
            margin-left: 0px;
        }
        .divInstApoioAlu table, .divProgSociaisAlu table
        {
            border: none;
        }
        .txtCartorioAlu
        {
            width: 216px;
        }
        .txtLogradouroAlu
        {
            width: 195px;
        }
        .txtComplementoAlu
        {
            width: 180px;
        }
        .ddlCidadeAlu
        {
            width: 177px;
        }
        .ddlBairroAlu
        {
            width: 150px;
        }
        .txtTurmaAlu
        {
            width: 31px;
        }
        .txtSerieAlu, .txtFolhaAlu
        {
            width: 24px;
        }
        .txtModalidadeAlu
        {
            width: 71px;
        }
        .txtUnidadeAlu
        {
            width: 39px;
        }
        .txtMatriculaAlu
        {
            width: 69px;
        }
        .txtResponsavelAlu
        {
            width: 250px;
        }
        .txtGrauParentescoAlu, .ddlEstadoCivilAlu
        {
            width: 77px;
        }
        .txtEmailAlu
        {
            width: 190px;
        }
        .txtCartaoSaudeAlu, .txtApelidoAlu
        {
            width: 78px;
        }
        .txtNireAlu, .txtNisAlu
        {
            width: 52px;
        }
        .ddlDeficienciaAlu, .txtNumeroTituloAlu
        {
            width: 70px;
        }
        .ddlNacionalidadeAlu
        {
            width: 75px;
        }
        .ddlEtniaAlu
        {
            width: 90px;
        }
        .txtNaturalidadeAlu
        {
            width: 100px;
        }
        .ddlRendaFamiliarAlu
        {
            width: 80px;
        }
        .ddlPasseEscolarAlu, .ddlTransporteEscolarAlu, .txtCredLanchoAlu, .ddlSexoAlu
        {
            width: 45px;
        }
        .txtObservacoesAlu
        {
            width: 398px;
            height: 28px;
            margin-top: 2px;
        }
        .ddlTipoCertidaoAlu
        {
            width: 79px;
        }
        .txtNumeroCertidaoAlu
        {
            width: 143px;
        }
        .txtLivroAlu
        {
            width: 25px;
        }
        .txtRgAlu
        {
            width: 45px;
        }
        .txtOrgaoEmissorAlu
        {
            width: 40px;
        }
        .txtSecaoAlu, .txtZonaAlu, .txtNumeroAlu, .txtDescontoAlu
        {
            width: 40px;
        }
        .ddlBolsaAlu
        {
            width: 167px;
        }
        .campoNomePessoa
        {
            text-transform: uppercase;
        }
        .txtQtdPasseAlu, .ddlStaTpSangueAlu
        {
            width: 30px;
        }
        .lblTitInf
        {
            text-transform: uppercase;
            font-weight: bold;
            font-size: 1.0em;
        }
        .chkLocais label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .btnPesqMat
        {
            width: 13px;
        }
        .txtCidadeCertidaoAlu
        {
            width: 88px;
        }
        .mapa
        {
            width: 335px !important;
            height: 168px !important;
        }
        .txtNomeMaeAlu
        {
            width: 192px;
        }
        .ddlTpSangueAlu
        {
            width: 35px;
            clear: both;
        }
        .imgPesRes
        {
            width: 13px;
            height: 15px;
        }
        #divAddTipo
        {
            display: none;
        }
        ul li .divGrid
        {
            width: 400px;
            height: 60px;
            display:block;
            overflow-y: auto;
            margin-top:2px;
        }
        /*.file { left: -120px !important; }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liFoto">
            <ul>
                <li class="liClear" style="margin-top: 5px; margin-bottom: 5px;">
                    <fieldset class="fldFotoAluno">
                        <uc1:ControleImagem ID="upImagemAluno" runat="server" />
                    </fieldset>
                </li>
            </ul>
        </li>
        <li style="width: 425px;">
            <ul>
                <li>
                    <label for="txtNireAlu" class="lblObrigatorio" title="N�mero do NIRE">
                        N� NIRE</label>
                    <asp:TextBox ID="txtNireAlu" CssClass="txtNireAlu" runat="server" ToolTip="Informe o N�mero do NIRE"></asp:TextBox>
                    <asp:RangeValidator ID="rvNire" runat="server" ControlToValidate="txtNireAlu" CssClass="validatorField"
                        ErrorMessage="NIRE deve estar entre 0 e 999999999" Text="*" Type="Integer" MaximumValue="999999999"
                        MinimumValue="0"></asp:RangeValidator>
                    <asp:CustomValidator ID="CustomValidator4" ControlToValidate="txtNireAlu" runat="server"
                        ErrorMessage="Nire deve ser informado" Display="None" CssClass="validatorField"
                        EnableClientScript="false" OnServerValidate="cvVerifObrigNIRE_ServerValidate">
                    </asp:CustomValidator>
                </li>
                <li style="margin-left: 2px;">
                    <label for="txtAnoOri" title="Ano de origem do aluno na escola">
                        Ano</label>
                    <asp:TextBox ID="txtAnoOri" ToolTip="Informe o ano de origem do aluno na escola"
                        runat="server" MaxLength="4" Width="30px">
                    </asp:TextBox>
                </li>
                <li class="liNisAlu" style="margin-left: 2px;">
                    <label for="txtNisAlu" title="N�mero do NIS">
                        N� NIS</label>
                    <asp:TextBox ID="txtNisAlu" CssClass="txtNisAlu" runat="server" ToolTip="Informe o N�mero do NIS"
	                    MaxLength="11"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtNisAlu"
	                    ErrorMessage="NIS deve ser num�rico de 11 d�gitos" Text="*" Type="Double" MaximumValue="99999999999"
	                    MinimumValue="0"></asp:RangeValidator>
                </li>
                <li style="margin-left: 2px;">
                    <label for="txtNomeAlu" class="lblObrigatorio" title="Nome do Aluno">
                        Nome</label>
                    <asp:TextBox ID="txtNomeAlu" runat="server" Width="245px" ToolTip="Informe o Nome do Aluno"
                        MaxLength="80"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNomeAlu"
                        ErrorMessage="Nome deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtApelidoAlu" title="Apelido">
                        Apelido</label>
                    <asp:TextBox ID="txtApelidoAlu" ToolTip="Informe o Apelido do Aluno" CssClass="txtApelidoAlu"
                        runat="server" MaxLength="11">
                    </asp:TextBox>
                </li>
                <li class="liSexoAlu">
                    <label for="ddlSexoAlu" class="lblObrigatorio" title="Sexo do Aluno">
                        Sexo</label>
                    <asp:DropDownList ID="ddlSexoAlu" CssClass="ddlSexoAlu" runat="server" ToolTip="Informe o Sexo do Aluno">
                        <asp:ListItem Value="" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="M">Mas</asp:ListItem>
                        <asp:ListItem Value="F">Fem</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSexoAlu"
                        ErrorMessage="Sexo deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li class="liDataNascimentoAlu">
                    <label for="txtDataNascimentoAlu" class="lblObrigatorio" title="Data de Nascimento">
                        Nascimento</label>
                    <asp:TextBox ID="txtDataNascimentoAlu" CssClass="campoData" runat="server" ToolTip="Informe a Data de Nascimento"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataNascimentoAlu"
                        ErrorMessage="Data de Nascimento deve ser informada" Text="*" Display="None"
                        CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li class="liDataNascimentoAlu">
                    <label for="ddlEtniaAlu" title="Etnia do Aluno">
                        Etnia</label>
                    <asp:DropDownList ID="ddlEtniaAlu" CssClass="ddlEtniaAlu" runat="server" ToolTip="Informe a Etnia do Aluno">
                        <asp:ListItem Value="B">Branca</asp:ListItem>
                        <asp:ListItem Value="N">Negra</asp:ListItem>
                        <asp:ListItem Value="A">Amarela</asp:ListItem>
                        <asp:ListItem Value="P">Parda</asp:ListItem>
                        <asp:ListItem Value="I">Ind�gena</asp:ListItem>
                        <asp:ListItem Value="X" Selected="true">N�o Informada</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liDataNascimentoAlu">
                    <label for="ddlDeficienciaAlu" title="Defici�ncia">
                        Defici�ncia</label>
                    <asp:DropDownList ID="ddlDeficienciaAlu" CssClass="ddlDeficienciaAlu" runat="server"
                        ToolTip="Selecione a Defici�ncia do Aluno">
                    </asp:DropDownList>
                </li>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <li class="liClear">
                            <label for="ddlNacionalidadeAlu" class="lblObrigatorio" title="Nacionalidade do Aluno">
                                Nacionalidade</label>
                            <asp:DropDownList ID="ddlNacionalidadeAlu" CssClass="ddlNacionalidadeAlu" runat="server"
                                ToolTip="Informe a Nacionalidade do Aluno" AutoPostBack="true" OnSelectedIndexChanged="ddlNacionalidade_SelectedIndexChanged">
                                <asp:ListItem Value="B">Brasileiro</asp:ListItem>
                                <asp:ListItem Value="E">Estrangeiro</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlNacionalidadeAlu"
                                ErrorMessage="Nacionalidade deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                        </li>
                        <li style="margin-left: 5px;">
                            <label for="txtNaturalidadeAlu" title="Cidade de Naturalidade do Aluno">
                                Naturalidade</label>
                            <asp:TextBox ID="txtNaturalidadeAlu" CssClass="txtNaturalidadeAlu" runat="server"
                                ToolTip="Informe a Cidade de Naturalidade do Aluno" MaxLength="40"></asp:TextBox>
                        </li>
                        <li>
                            <label for="ddlUfNacionalidadeAlu" title="UF de Nacionalidade do Aluno">
                                UF</label>
                            <asp:DropDownList ID="ddlUfNacionalidadeAlu" CssClass="campoUf" runat="server" ToolTip="Informe a UF de Nacionalidade">
                            </asp:DropDownList>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <li style="margin-left: 5px;">
                    <label for="ddlOrigem" class="" title="Origem">
                        Origem</label>
                    <asp:DropDownList ID="ddlOrigem" Style="width: 100px;" runat="server" ToolTip="Origem">
                        <asp:ListItem Value="SR" Text="Sem Registro"></asp:ListItem>
                        <asp:ListItem Value="MU" Text="Local - Escola P�blica"></asp:ListItem>
                        <asp:ListItem Value="EP" Text="Local - Escola Particular"></asp:ListItem>
                        <asp:ListItem Value="IE" Text="Interior do Estado"></asp:ListItem>
                        <asp:ListItem Value="OE" Text="Outro Estado"></asp:ListItem>
                        <asp:ListItem Value="AR" Text="�rea Rural"></asp:ListItem>
                        <asp:ListItem Value="AI" Text="�rea Ind�gena"></asp:ListItem>
                        <asp:ListItem Value="AQ" Text="�rea Quilombo"></asp:ListItem>
                        <asp:ListItem Value="OO" Text="Outra Origem"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li style="margin-left: 5px; clear: none; margin-right: 0px;">
                    <label title="Tipo de Sangue">
                        Tp.Sangu�neo</label>
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
                <li class="liClear">
                    <label for="txtEmailAlu" title="E-mail">
                        Email</label>
                    <asp:TextBox ID="txtEmailAlu" CssClass="txtEmailAlu" ToolTip="Informe o E-mail do Aluno"
                        runat="server" MaxLength="50"></asp:TextBox>
                </li>
                <li style="margin-left: 5px;">
                    <label for="txtTelResidencialAlu" title="Telefone Residencial">
                        Tel. Residencial</label>
                    <asp:TextBox ID="txtTelResidencialAlu" CssClass="campoTelefone" ToolTip="Informe o Telefone Residencial do Aluno"
                        runat="server"></asp:TextBox>
                </li>
                <li style="margin-left: 5px;">
                    <label for="txtTelCelularAlu" title="Telefone Celular">
                        Tel. Celular</label>
                    <asp:TextBox ID="txtTelCelularAlu" CssClass="campoTelefone" ToolTip="Informe o Telefone Celular do Aluno"
                        runat="server"></asp:TextBox>
                </li>
                <li style="margin-left: 8px; margin-right: 0px;">
                    <label for="ddlAutorSaida" style="color: Red;" title="Autoriza a sa�da?">
                        Aut. Sa�da</label>
                    <asp:DropDownList ID="ddlAutorSaida" Style="width: 42px; margin-left: 4px;" runat="server"
                        ToolTip="Autoriza a sa�da?">
                        <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                        <asp:ListItem Value="N" Text="N�o"></asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ul>
        </li>
        <li class="liBloco2" style="height: 115px;">
            <ul>
                <li class="liTitInfCont" style="margin-top: 0px;">
                    <label class="lblTitInf">
                        Dados Respons�vel</label></li>
                <li class="liClear" style="margin: 3px 0 3px -7px;">
                    <asp:CheckBox CssClass="chkLocais" ID="chkRespFunc" TextAlign="Right" runat="server"
                        ToolTip="Respons�vel � Funcion�rio" Enabled="false" Text="Funcion�rio" /></li>
                <li class="liClear" style="margin-right: 0px;">
                    <label for="txtResponsavelAlu" title="Nome do Respons�vel pelo Aluno">
                        Nome do Respons�vel</label>
                    <asp:TextBox ID="txtResponsavelAlu" CssClass="txtResponsavelAlu" ToolTip="Informe o Nome do Respons�vel pelo Aluno"
                        Enabled="false" runat="server"></asp:TextBox>
                </li>
                <li class="liClear">
                    <label for="txtDtNascRespAlu" title="Data de Nascimento do Respons�vel do Aluno">
                        Nascimento</label>
                    <asp:TextBox ID="txtDtNascRespAlu" CssClass="campoData" runat="server" Enabled="false"
                        ToolTip="Data de Nascimento do Respons�vel do Aluno"></asp:TextBox>
                </li>
                <li style="margin-left: 21px;">
                    <label for="ddlSexoRespAlu" title="Sexo do Respons�vel do Aluno">
                        Sexo</label>
                    <asp:DropDownList ID="ddlSexoRespAlu" CssClass="ddlSexoAlu" Enabled="false" runat="server"
                        ToolTip="Sexo do Respons�vel do Aluno">
                        <asp:ListItem Value="M">Mas</asp:ListItem>
                        <asp:ListItem Value="F">Fem</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li style="margin-left: 21px; margin-right: 0px;">
                    <label for="txtCPFRespAlu" title="CPF do Respons�vel do Aluno">
                        CPF</label>
                    <asp:TextBox ID="txtCPFRespAlu" ToolTip="CPF do Respons�vel do Aluno" Enabled="false"
                        runat="server" CssClass="campoCpf"></asp:TextBox>
                </li>
                <li class="liClear">
                    <label for="txtGrauParentescoAlu" title="Grau de Parentesco do Respons�vel">
                        Grau Parentesco</label>
                    <asp:TextBox ID="txtGrauParentescoAlu" CssClass="txtGrauParentescoAlu" ToolTip="Informe o Grau de Parentesco do Respons�vel"
                        Enabled="false" runat="server"></asp:TextBox>
                </li>
                <li style="margin-left: 7px;">
                    <label for="txtTelefResiResp" title="Telefone Residencial">
                        Tel. Residencial</label>
                    <asp:TextBox ID="txtTelefResiResp" Enabled="false" CssClass="campoTelefone" ToolTip="Telefone Residencial do Respons�vel"
                        runat="server"></asp:TextBox>
                </li>
                <li style="margin-left: 7px; margin-right: 0px;">
                    <label for="txtCelularResp" title="Telefone Celular do Respons�vel">
                        Tel. Celular</label>
                    <asp:TextBox ID="txtCelularResp" Enabled="false" CssClass="campoTelefone" ToolTip="Telefone Celular do Respons�vel"
                        runat="server"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li class="liBloco3" style="padding-left: 6px;">
            <ul>
                <li class="liTitInfCont">
                    <label class="lblTitInf">
                        Informa��es Diversas</label></li>
                <li style="clear: both; margin-top: 2px; margin-left: -7px;">
                    <asp:CheckBox CssClass="chkLocais" ID="chkRestrAlime" TextAlign="Right" runat="server"
                        ToolTip="Apresenta Restri��o Alimentar?" Text="Restri��o Alimentar" Enabled="false" />
                </li>
                <li style="clear: none; margin-top: 2px;">
                    <asp:CheckBox CssClass="chkLocais" ID="chkCuidaSaude" TextAlign="Right" runat="server"
                        ToolTip="Apresenta Cuidados Sa�de?" Text="Cuidados Sa�de" Enabled="false" />
                </li>
                <li style="clear: both; margin-top: 4px; margin-left: -7px;">
                    <asp:CheckBox CssClass="chkLocais" ID="chkTranspEscolar" TextAlign="Right" runat="server"
                        ToolTip="Apresenta Transporte Escolar?" Text="Transporte Escolar" />
                </li>
                <li style="clear: none; margin-top: 4px; margin-left: 3px;">
                    <asp:CheckBox CssClass="chkLocais" ID="chkMerenEscolar" TextAlign="Right" runat="server"
                        ToolTip="Apresenta Merenda Escolar?" Text="Merenda Escolar" />
                </li>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <li class="liClear" style="margin-top: 5px; margin-left: -7px;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkLanchoneteAlu" TextAlign="Right" runat="server"
                                ToolTip="Apresenta Conta na Lanchonete?" AutoPostBack="true" Text="Lanchonete"
                                OnCheckedChanged="chkLanchoneteAlu_CheckedChanged" />
                        </li>
                        <li style="margin-top: 5px; margin-left: 0px;"><span title="Cr�dito na Lanchonete">-
                            Limite Cr�dito R$</span>
                            <asp:TextBox ID="txtCredLanchoAlu" CssClass="txtCredLanchoAlu" Enabled="false" runat="server"
                                Style="clear: none;" ToolTip="Digite o Cr�dito da Lanchonete"></asp:TextBox>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <li class="liClear" style="margin-top: 5px; margin-left: -7px;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkPasseEscolar" TextAlign="Right" runat="server"
                                ToolTip="Apresenta Passe Escolar?" AutoPostBack="true" Text="Passe Escolar" OnCheckedChanged="chkPasseEscolar_CheckedChanged" />
                        </li>
                        <li style="margin-top: 5px; margin-left: 0px;"><span title="Quantidade de Passe Escolar">
                            - Quantidade</span>
                            <asp:TextBox ID="txtQtdPasseAlu" CssClass="txtQtdPasseAlu" Enabled="false" Style="clear: none;"
                                runat="server" ToolTip="Digite a Quantidade de Passe Escolar"></asp:TextBox>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <li class="liClear" style="margin-top: 5px;"><span class="lblObrigatorio" title="Renda Familiar do Aluno">
                    Renda Familiar</span>
                    <asp:DropDownList ID="ddlRendaFamiliarAlu" CssClass="ddlRendaFamiliarAlu" runat="server"
                        Style="clear: none;" ToolTip="Informe a Renda Familiar do Aluno">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">1 a 3 SM</asp:ListItem>
                        <asp:ListItem Value="2">3 a 5 SM</asp:ListItem>
                        <asp:ListItem Value="3">5 a 10 SM</asp:ListItem>
                        <asp:ListItem Value="4">+10 SM</asp:ListItem>
                        <asp:ListItem Value="5">Sem Renda</asp:ListItem>
                        <asp:ListItem Value="6">N�o informada</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlRendaFamiliarAlu"
                        ErrorMessage="Renda familiar deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li class="liClear" style="margin-left: -3px; margin-top: 6px;">
                    <fieldset style="background-color: #FFE4C4; border: 0px;">
                        <ul>
                            <li class="liClear liTitInfCont" style="margin-top: 2px;">
                                <label class="lblTitInf" style="">
                                    Pend�ncias</label></li>
                            <li style="clear: both; margin-top: 2px; margin-left: -4px;">
                                <asp:CheckBox CssClass="chkLocais" ID="chkPendeSecre" TextAlign="Right" runat="server"
                                    ToolTip="Existe pend�ncia na Secretaria Escolar?" Text="Secretaria" Enabled="false" />
                            </li>
                            <li style="clear: none; margin-top: 2px; margin-left: 0px;">
                                <asp:CheckBox CssClass="chkLocais" ID="chkPendeBibli" TextAlign="Right" runat="server"
                                    ToolTip="Existe pend�ncia na Biblioteca?" Text="Biblioteca" Enabled="false" />
                            </li>
                            <li style="clear: none; margin-top: 2px; margin-left: 0px; margin-right: 5px;">
                                <asp:CheckBox CssClass="chkLocais" ID="chkPendeFinan" TextAlign="Right" runat="server"
                                    ToolTip="Existe pend�ncia Financeira?" Text="Financeira" Enabled="false" />
                            </li>
                        </ul>
                    </fieldset>
                </li>
            </ul>
        </li>
        <li class="liBloco4">
            <ul>
                <li class="liTitInfCont" style="clear: both;">
                    <label class="lblTitInf">
                        Informa��es de Documentos</label></li>
                <li class="liClear" style="margin-top: 2px; color: #FF7F24;">
                    <label title="Certid�o">
                        Certid�o</label>
                </li>
                <li class="liClear">
                    <label for="ddlTipoCertidaoAlu" class="lblObrigatorio" title="Tipo de Certid�o">
                        Tipo</label>
                    <asp:DropDownList ID="ddlTipoCertidaoAlu" CssClass="ddlTipoCertidaoAlu" ToolTip="Selecione o Tipo de Certid�o"
                        runat="server">
                        <asp:ListItem Value="N">Nascimento</asp:ListItem>
                        <asp:ListItem Value="C">Casamento</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlTipoCertidaoAlu"
                        ErrorMessage="Tipo de certid�o deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlUFCertidaoAlu" title="UF da Certid�o">
                        UF</label>
                    <asp:DropDownList ID="ddlUFCertidaoAlu" CssClass="campoUf" ToolTip="Informe a UF da Certid�o"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="txtCidadeCertidaoAlu" class="lblObrigatorio" title="Cidade">
                        Cidade</label>
                    <asp:TextBox ID="txtCidadeCertidaoAlu" CssClass="txtCidadeCertidaoAlu" ToolTip="Informe a Cidade da Certid�o"
                        runat="server" MaxLength="40"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtCidadeCertidaoAlu"
                        ErrorMessage="Cidade da Certid�o deve ser informada" Text="*" Display="None"
                        CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtCartorioAlu" class="lblObrigatorio" title="Cart�rio da Certid�o">
                        Cart�rio</label>
                    <asp:TextBox ID="txtCartorioAlu" CssClass="txtCartorioAlu" ToolTip="Informe o Cart�rio da Certid�o"
                        runat="server" MaxLength="80"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtCartorioAlu"
                        ErrorMessage="Cart�rio deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtLivroAlu" title="Livro da Certid�o">
                        Livro</label>
                    <asp:TextBox ID="txtLivroAlu" CssClass="txtLivroAlu" ToolTip="Informe o Livro da Certid�o"
                        runat="server" MaxLength="10"></asp:TextBox>
                </li>
                <li style="margin-left: 5px;">
                    <label for="txtFolhaAlu" title="Folha da Certid�o">
                        Folha</label>
                    <asp:TextBox ID="txtFolhaAlu" CssClass="txtFolhaAlu" ToolTip="Informe a Folha da Certid�o"
                        runat="server" MaxLength="8"></asp:TextBox>
                </li>
                <li style="margin-left: 5px;">
                    <label for="txtNumeroCertidaoAlu" class="lblObrigatorio" title="N�mero de Controle da Certid�o">
                        N� Controle</label>
                    <asp:TextBox ID="txtNumeroCertidaoAlu" CssClass="txtNumeroCertidaoAlu" ToolTip="Informe o N�mero de Controle da Certid�o"
                        runat="server" MaxLength="32"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtNumeroCertidaoAlu"
                        ErrorMessage="N�mero de Controle da Certid�o deve ser informado" Text="*" Display="None"
                        CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li class="liClear" style="margin-top: 15px; color: #FF7F24;">
                    <label title="Carteira de Identidade">
                        Carteira de Identidade</label>
                </li>
                <li class="liClear">
                    <label for="txtRgAlu" title="N�mero da Identidade">
                        N�mero</label>
                    <asp:TextBox ID="txtRgAlu" CssClass="txtRgAlu" ToolTip="Informe o N�mero da Identidade"
                        runat="server" MaxLength="20"></asp:TextBox>
                </li>
                <li>
                    <label for="txtDataEmissaoRgAlu" title="Data de Emiss�o da Identidade">
                        Data</label>
                    <asp:TextBox ID="txtDataEmissaoRgAlu" CssClass="campoData" ToolTip="Informe a Data de Emiss�o da Identidade"
                        runat="server"></asp:TextBox>
                </li>
                <li>
                    <label for="txtOrgaoEmissorAlu" title="�rg�o Emissor da Identidade">
                        Emissor</label>
                    <asp:TextBox ID="txtOrgaoEmissorAlu" CssClass="txtOrgaoEmissorAlu" ToolTip="Informe o �rg�o Emissor da Identidade"
                        runat="server" MaxLength="12"></asp:TextBox>
                </li>
                <li>
                    <label for="ddlUfRgAlu" title="UF do �rg�o Emissor">
                        UF</label>
                    <asp:DropDownList ID="ddlUfRgAlu" CssClass="campoUf" ToolTip="Informe a UF do �rg�o Emissor"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liClear" style="margin-top: 10px; color: #FF7F24;">
                    <label title="T�tulo de Eleitor">
                        T�tulo de Eleitor</label>
                </li>
                <li class="liClear">
                    <label for="txtNumeroTituloAlu" title="N�mero do T�tulo de Eleitor">
                        N�mero</label>
                    <asp:TextBox ID="txtNumeroTituloAlu" CssClass="txtNumeroTituloAlu" ToolTip="Informe o N�mero do T�tulo de Eleitor"
                        runat="server" MaxLength="15"></asp:TextBox>
                </li>
                <li>
                    <label for="txtZonaAlu" title="Zona do T�tulo de Eleitor">
                        Zona</label>
                    <asp:TextBox ID="txtZonaAlu" CssClass="txtZonaAlu" ToolTip="Informe a Zona do T�tulo de Eleitor"
                        runat="server" MaxLength="10"></asp:TextBox>
                </li>
                <li>
                    <label for="txtSecaoAlu" title="Se��o do T�tulo de Eleitor">
                        Se��o</label>
                    <asp:TextBox ID="txtSecaoAlu" CssClass="txtSecaoAlu" ToolTip="Informe a Se��o do T�tulo de Eleitor"
                        runat="server" MaxLength="10"></asp:TextBox>
                </li>
                <li>
                    <label for="ddlUfTituloAlu" title="UF do T�tulo de Eleitor">
                        UF</label>
                    <asp:DropDownList ID="ddlUfTituloAlu" CssClass="campoUf" ToolTip="Informe a UF do T�tulo de Eleitor"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liClear" style="margin-top: 10px; color: #FF7F24;">
                    <label title="Outros Documentos">
                        Outros Documentos</label>
                </li>
                <li class="liClear">
                    <label for="txtCpfAlu" title="CPF do Aluno">
                        CPF ou NIS</label>
                    <asp:TextBox ID="txtCpfAlu" ToolTip="Informe o CPF do Aluno" runat="server" CssClass="campoCpf" style="width: 100px"></asp:TextBox>
                    <asp:CustomValidator ControlToValidate="txtCpfAlu" ID="cvCPF" runat="server" ErrorMessage="CPF inv�lido"
                        Text="*" Display="None" CssClass="validatorField" EnableClientScript="false"
                        OnServerValidate="cvCPF_ServerValidate"></asp:CustomValidator>
                </li>
                <li style="margin-left: 7px;">
                    <label for="txtPassaporteAlu" title="N�mero do Passaporte">
                        N� Passaporte</label>
                    <asp:TextBox ID="txtPassaporteAlu" ToolTip="Informe o N�mero do Passaporte" runat="server"
                        CssClass="txtCartaoSaudeAlu"></asp:TextBox>
                </li>
                <li class="liClear">
                    <label for="txtCartaoSaudeAlu" title="N�mero do Cart�o Sa�de">
                        N� Cart�o Sa�de</label>
                    <asp:TextBox ID="txtCartaoSaudeAlu" ToolTip="Informe o N�mero do Cart�o Sa�de" runat="server"
                        CssClass="txtCartaoSaudeAlu" style="width: 100px"></asp:TextBox>
                </li>
                <li style="margin-left: 7px;">
                    <label for="txtCartaoVacinaAlu" title="N�mero do Cart�o de Vacina">
                        N� Cart Vacina</label>
                    <asp:TextBox ID="txtCartaoVacinaAlu" ToolTip="Informe o N�mero do Cart�o de Vacina"
                        runat="server" CssClass="txtCartaoSaudeAlu"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li class="liBloco5" style="margin-top: 0px; padding-left: 8px; padding-right: 0;">
            <ul>
                <li class="liTitInfCont">
                    <label class="lblTitInf">
                        Endere�o Residencial</label></li>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <li class="liClear">
                            <label for="txtCepAlu" class="lblObrigatorio" title="CEP">
                                CEP</label>
                            <asp:TextBox ID="txtCepAlu" CssClass="campoCep" ToolTip="Informe o CEP" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtCepAlu"
                                ErrorMessage="CEP deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liPesqCEPR">
                            <asp:ImageButton ID="btnPesqCEPA" runat="server" OnClick="btnPesquisarCepAluno_Click"
                                ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" class="btnPesqMat" CausesValidation="false" />
                        </li>
                        <li>
                            <label for="txtLogradouroAlu" class="lblObrigatorio" title="Logradouro da Resid�ncia do Aluno">
                                Logradouro</label>
                            <asp:TextBox ID="txtLogradouroAlu" ClientIDMode="Static" CssClass="txtLogradouroAlu"
                                ToolTip="Informe o Logradouro da Resid�ncia do Aluno" runat="server" MaxLength="40"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtLogradouroAlu"
                                ErrorMessage="Logradouro deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liListaEndereco" title="Clique para Listar os Endere�os">
                            <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Listar Endere�o" />
                        </li>
                        <div id="divAddTipo">
                        </div>
                        <li>
                            <label for="txtNumeroAlu" title="N�mero da Resid�ncia do Aluno">
                                N�mero</label>
                            <asp:TextBox ID="txtNumeroAlu" CssClass="txtNumeroAlu" ToolTip="Informe o N�mero da Resid�ncia do Aluno"
                                runat="server" MaxLength="5"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtComplementoAlu" title="Complemento">
                                Complemento</label>
                            <asp:TextBox ID="txtComplementoAlu" CssClass="txtComplementoAlu" ToolTip="Informe o Complemento"
                                runat="server" MaxLength="100"></asp:TextBox>
                        </li>
                        <li>
                            <label for="ddlBairroAlu" class="lblObrigatorio" title="Bairro">
                                Bairro</label>
                            <asp:DropDownList ID="ddlBairroAlu" CssClass="ddlBairroAlu" ToolTip="Informe o Bairro"
                                runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlBairroAlu"
                                ErrorMessage="Bairro deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liClear">
                            <label for="ddlCidadeAlu" class="lblObrigatorio" title="Cidade">
                                Cidade</label>
                            <asp:DropDownList ID="ddlCidadeAlu" ToolTip="Informe a Cidade" runat="server" CssClass="ddlCidadeAlu"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlCidadeAlu"
                                ErrorMessage="Cidade deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="ddlUfAlu" class="lblObrigatorio" title="UF">
                                UF</label>
                            <asp:DropDownList ID="ddlUfAlu" runat="server" CssClass="campoUf" ToolTip="Informe a UF"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlUfAlu"
                                ErrorMessage="UF deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <li style="clear: none; margin-left: 10px; margin-top: 13px; margin-right: 0px;">
                    <asp:CheckBox CssClass="chkLocais" ID="chkMoraPais" TextAlign="Right" runat="server"
                        ToolTip="Aluno mora com os pais?" Text="Mora com os Pais" />
                </li>
                <li class="liTitInfCont" style="background-color: #DDDDDD; width: 337px; text-align: center;
                    padding: 2px 0 2px 0; clear: both; margin-top: 10px;">
                    <label class="lblTitInf">
                        Posi��o Georeferencial Endere�o Residencial</label></li>
                <li class="liClear" style="margin-top: 5px;">
                    <div style="border: 1px solid #BEBEBE; width: 335px; height: 168px;">
                        <table id="tbMap" runat="server" cellpadding="0" cellspacing="0" style="width: 98%;
                            border: 0px;">
                            <tr>
                                <td class="mapa">
                                    <!--API GOOGLE, ATIVA O servi�o de map na tela-->
                                    <artem:GoogleMap ID="GMapa" CssClass="mapa" runat="server">
                                    </artem:GoogleMap>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                </li>
            </ul>
        </li>
        <li class="liBloco6" style="margin-top: 0px; padding-left: 0px; margin-left: -2px;">
            <ul>
                <li>
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Filia��o</label></li>
                        <li style="clear: none; margin-top: -2px; margin-left: -3px; margin-bottom: 6px;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkPaisMorJunt" TextAlign="Right" runat="server"
                                ToolTip="Os pais moram juntos?" Text="Moram juntos" />
                        </li>
                        <li class="liClear">
                            <label for="txtNomeMaeAlu" class="lblObrigatorio" title="Nome da M�e">
                                Nome da M�e</label>
                            <asp:TextBox ID="txtNomeMaeAlu" CssClass="txtNomeMaeAlu" ToolTip="Informe o Nome da M�e"
                                runat="server" MaxLength="60"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNomeMaeAlu"
                                ErrorMessage="Nome da m�e deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="txtDataNascMae" title="Data de Nascimento">
                                Nascimento</label>
                            <asp:TextBox ID="txtDataNascMae" CssClass="campoData" runat="server" ToolTip="Informe a Data de Nascimento"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtTelefMae" title="Telefone">
                                Tel.</label>
                            <asp:TextBox ID="txtTelefMae" CssClass="campoTelefone" ToolTip="Informe o Telefone da M�e"
                                runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label for="ddlObitoMae" title="�bito">
                                �bito</label>
                            <asp:DropDownList ID="ddlObitoMae" Width="45px" runat="server" ToolTip="Informe se a m�e est� em �bito">
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N">N�o</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="txtEmailMae" title="Email da M�e">
                                Email</label>
                            <asp:TextBox ID="txtEmailMae" CssClass="txtNomeMaeAlu" ToolTip="Informe o Email da M�e"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtFacebookMae" title="Facebook da M�e">
                                Facebook</label>
                            <asp:TextBox ID="txtFacebookMae" Width="105px" ToolTip="Informe o Facebook da M�e"
                                runat="server" MaxLength="30"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtTwitterMae" title="Twitter da M�e">
                                Twitter</label>
                            <asp:TextBox ID="txtTwitterMae" Width="88px" ToolTip="Informe o Twitter da M�e" runat="server"
                                MaxLength="30"></asp:TextBox>
                        </li>
                        <li class="liClear" style="margin-top: 4px;">
                            <label for="txtNomePaiAlu" title="Nome do Pai">
                                Nome do Pai</label>
                            <asp:TextBox ID="txtNomePaiAlu" CssClass="txtNomeMaeAlu" ToolTip="Informe o Nome do Pai"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                        <li style="margin-top: 4px;">
                            <label for="txtDataNascMae" title="Data de Nascimento">
                                Nascimento</label>
                            <asp:TextBox ID="txtDataNascPai" CssClass="campoData" runat="server" ToolTip="Informe a Data de Nascimento"></asp:TextBox>
                        </li>
                        <li style="margin-top: 4px;">
                            <label for="txtTelefPai" title="Telefone">
                                Tel.</label>
                            <asp:TextBox ID="txtTelefPai" CssClass="campoTelefone" ToolTip="Informe o Telefone do Pai"
                                runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-top: 4px;">
                            <label for="ddlObitoMae" title="�bito">
                                �bito</label>
                            <asp:DropDownList ID="ddlObitoPai" Width="45px" runat="server" ToolTip="Informe se o pai est� em �bito">
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N">N�o</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="txtEmailPai" title="Email do Pai">
                                Email</label>
                            <asp:TextBox ID="txtEmailPai" CssClass="txtNomeMaeAlu" ToolTip="Informe o Email do Pai"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtFacebookPai" title="Facebook do Pai">
                                Facebook</label>
                            <asp:TextBox ID="txtFacebookPai" Width="105px" ToolTip="Informe o Facebook do Pai"
                                runat="server" MaxLength="30"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtTwitterPai" title="Twitter do Pai">
                                Twitter</label>
                            <asp:TextBox ID="txtTwitterPai" Width="88px" ToolTip="Informe o Twitter do Pai" runat="server"
                                MaxLength="30"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="clear: both; margin-top: 5px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Bolsa / Conv�nio</label></li>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <li class="liClear">
                                    <label for="ddlTipoBolsa" title="Tipo">
                                        Tipo</label>
                                    <asp:DropDownList ID="ddlTipoBolsa" Width="60px" ToolTip="Selecione o Nome da Bolsa"
                                        AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlTipoBolsa_SelectedIndexChanged">
                                        <asp:ListItem Value="N" Selected="True">Nenhuma</asp:ListItem>
                                        <asp:ListItem Value="B">Bolsa</asp:ListItem>
                                        <asp:ListItem Value="C">Conv�nio</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label for="ddlBolsaAlu" title="Nome da Bolsa">
                                        Nome da Bolsa</label>
                                    <asp:DropDownList ID="ddlBolsaAlu" CssClass="ddlBolsaAlu" ToolTip="Selecione o Nome da Bolsa"
                                        AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlBolsa_SelectedIndexChanged"
                                        Width="115">
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label for="txtValorDescto" title="Valor Desconto">
                                        Valor</label>
                                    <asp:TextBox ID="txtValorDescto" CssClass="txtDescontoAlu campoMoeda" ToolTip="Informe a Porcentagem de Desconto"
                                        runat="server" Enabled="False"></asp:TextBox>
                                </li>
                                <li style="margin-top: 13px; margin-left: -9px;">
                                    <asp:CheckBox CssClass="chkLocais" ID="chkDesctoPercBolsa" TextAlign="Right" Enabled="false"
                                        runat="server" ToolTip="% de Desconto da Bolsa?" Text="%" />
                                </li>
                                <li class="liPeriodoDe" style="margin-left: -4px;">
                                    <label for="txtPeriodoDeAlu" title="Per�odo de Dura��o da Bolsa">
                                        Per�odo</label>
                                    <asp:TextBox ID="txtPeriodoDeAlu" ToolTip="Informe a Data de In�cio da Bolsa" runat="server"
                                        CssClass="campoData" Enabled="False" Width="55px"></asp:TextBox>
                                </li>
                                <li class="lisepPeriodo" style="margin-left: -3px;"><span>�</span> </li>
                                <li class="liPeriodoAte" style="margin-left: -2px;">
                                    <asp:TextBox ID="txtPeriodoAteAlu" ToolTip="Informe a Data de T�rmino da Bolsa" runat="server"
                                        CssClass="campoData" Enabled="False" Width="55px"></asp:TextBox>
                                </li>
                                <li class="liClear">
                                    <label class="lblTitInf">
                                        Observa��es</label>
                                    <asp:TextBox ID="txtObservacoesAlu" CssClass="txtObservacoesAlu" ToolTip="Informe as Observa��es sobre o Aluno"
                                        runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);"
                                        Width="260px"></asp:TextBox>
                                </li>
                                <li style="margin-top: 16px">
                                    <label for="ddlSituacaoAlu" class="lblObrigatorio" title="Situa��o do Aluno">
                                        Situa��o</label>
                                    <asp:DropDownList ID="ddlSituacaoAlu" CssClass="ddlSituacaoAlu" ToolTip="Informe a Situa��o do Aluno"
                                        runat="server" Width="55px">
                                        <asp:ListItem Value="A" Selected="True">Ativo</asp:ListItem>
                                        <asp:ListItem Value="I">Inativo</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacaoAlu"
                                        ErrorMessage="Situa��o deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li style="margin-top: 16px">
                                    <label for="txtDataSituacaoAlu" class="lblObrigatorio" title="Data Situa��o do Aluno">
                                        Data Situa��o</label>
                                    <asp:TextBox ID="txtDataSituacaoAlu" CssClass="campoData" Enabled="False" ToolTip="Informe a Data de Situa��o do Aluno"
                                        runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtDataSituacaoAlu"
                                        ErrorMessage="Data de Situa��o deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                </li>
                                <li class="liClear">
                                    <label class="lblTitInf">
                                        DADOS DE MATR�CULAS</label>
                                    <div id="divGrid" class="divGrid" runat="server">
                                        <asp:GridView ID="grdMatriculas" runat="server" AutoGenerateColumns="False" CssClass="grdBusca"
                                            EmptyDataText="NENHUMA MATR�CULA ENCONTRADA." Width="390px" >
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <Columns>
                                                <asp:BoundField HeaderText="ANO" DataField="CO_ANO_MES_MAT" />
                                                <asp:BoundField HeaderText="MODULO" DataField="DE_MODU_CUR" />
                                                <asp:BoundField HeaderText="SER/CUR" DataField="CO_SIGL_CUR" />
                                                <asp:BoundField HeaderText="TURMA" DataField="CO_SIGLA_TURMA" />
                                                <asp:BoundField HeaderText="SITUA��O" DataField="nomeAprovacao" />
                                                <asp:BoundField HeaderText="STATUS" DataField="nomeMatricula" />
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                                            <RowStyle CssClass="rowStyle" />
                                        </asp:GridView>
                                    </div>
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ul>
                </li>
            </ul>
            <div id="divLoadShowListarCEPsEndereco" style="display: none; height: 205px !important;">
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtNumeroAlu").mask("?99999");
            $(".campoCep").mask("99999-999");
            $(".txtQtdPasseAlu").mask("?999");
            $(".txtCredLanchoAlu").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            $("input.campoData").datepicker();
            $(".campoData").mask("99/99/9999");
        });

        $(document).ready(function () {
            $('.liListaEndereco').click(function () {
                var strEndereco = $('.txtLogradouroAlu').val() + "";
                strEndereco = strEndereco.replace(/ /g, "*");
                $('#divAddTipo').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false,
                    open: function () { $('#divAddTipo').load("/Componentes/ListarCEPsEndereco.aspx?strEndereco=" + strEndereco); }
                });
            });
        });

        jQuery(function ($) {
//            $(".txtNisAlu").mask("?99999999999");
            $(".txtCartaoSaudeAlu").mask("?9999999999999999");
            $(".txtNireAlu").mask("?999999999");
            $(".txtNumeroAlu").mask("?99999");
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtCredLanchoAlu").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoCep").mask("99999-999");
            $(".campoTelefone").mask("(99)9999-9999?9");
            $(".campoCpf").mask("999.999.999-99");

        });
    </script>
</asp:Content>
