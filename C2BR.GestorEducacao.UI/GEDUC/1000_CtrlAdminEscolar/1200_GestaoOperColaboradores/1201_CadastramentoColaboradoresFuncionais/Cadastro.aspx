<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Cadastro.aspx.cs"
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1201_CadastramentoColaboradoresFuncionais.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 985px;
        }
        .desLiTipoAtiv
        {
            margin-top: 17px;
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
        .liFotoColab
        {
            float: left !important;
            margin-right: 10px !important;
        }
        .liSituacao
        {
            margin-top: 0px;
            margin-left: 0px;
        }
        .liBloco1
        {
            padding: 0 10px 0 2px;
            width: 500px;
        }
        .liBloco2
        {
            width: 655px;
            clear: both !important;
            margin-top: 30px;
        }
        .liClear
        {
            clear: both !important;
        }
        .liEspaco
        {
            margin-left: 7px;
        }
        .liNIS
        {
            clear: none !important;
            margin-left: 0px;
        }
        .liOrigem
        {
            margin-left: 30px;
        }
        .liBloco3
        {
            width: 315px;
            border-left: 1px solid #BEBEBE;
            padding-left: 10px;
            margin-top: -130px;
            margin-right: 0px !important;
            margin-left: -10px;
        }
        .liNumeroColab
        {
            margin-left: 2px;
        }
        .liTipoVinculoColab, .liDtSituacaoColab
        {
            margin-top: 0px;
        }
        .liValidadeCnhColab
        {
            margin-left: 6px;
        }
        .liddlTpSangueF
        {
            margin-left: 23px;
        }
        .liUfNacionalidadeColab
        {
            margin-left: 3px;
        }
        .liObsResponsavel
        {
            clear: both;
            margin-top: 10px;
        }
        .liPesqCEPR
        {
            margin-top: 14px;
            margin-left: -3px;
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
        .lblTitInf
        {
            text-transform: uppercase;
            font-weight: bold;
            font-size: 1.0em;
        }
        .txtMatriculaColab
        {
            width: 55px;
        }
        .txtEmailFuncColab
        {
            width: 215px;
        }
        .txtNISColab
        {
            width: 80px;
        }
        .txtConvSaudeColab
        {
            width: 70px;
        }
        .txtNomeColab
        {
            width: 221px;
        }
        .txtApelidoColab
        {
            width: 78px;
        }
        .txtDtNascColab
        {
            width: 60px;
        }
        .ddlSexoColab
        {
            width: 45px;
        }
        .ddlDeficienciaColab
        {
            width: 70px;
        }
        .ddlGrauInstrucaoColab
        {
            width: 150px;
        }
        .ddlCategFuncColab
        {
            width: 76px;
        }
        .ddlEstadoCivilColab
        {
            width: 142px;
        }
        .ddlTpSangueColab
        {
            width: 35px;
            clear: both;
        }
        .ddlNacionalidadeColab
        {
            width: 70px;
        }
        .chkLocais label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .txtCPFColab
        {
            width: 82px;
        }
        .txtRegCnhColab, .txtDocCnhColab
        {
            width: 85px;
        }
        .txtPassaporteColab
        {
            width: 60px;
        }
        .txtPisPasepColab
        {
            width: 68px;
        }
        .txtIdentidadeColab
        {
            width: 100px;
        }
        .txtOrgEmissorColab
        {
            width: 65px;
        }
        .txtDtEmissaoColab
        {
            width: 60px;
        }
        .txtNumeroTituloColab
        {
            width: 95px;
        }
        .txtZonaColab
        {
            width: 40px;
        }
        .txtSecaoColab
        {
            width: 40px;
        }
        .txtNumeroCtpsColab
        {
            width: 58px;
        }
        .txtSerieCtpsColab
        {
            width: 40px;
        }
        .txtViaColab
        {
            width: 30px;
        }
        .txtValidadeCnhColab
        {
            width: 60px;
        }
        .txtObservacoesFuncColab
        {
            height: 37px;
            width: 352px;
        }
        .ddlStaTpSangueColab
        {
            width: 30px;
        }
        .txtLogradouroColab
        {
            width: 225px;
        }
        .btnPesqMat
        {
            width: 13px;
        }
        .txtNumeroColab
        {
            width: 40px;
        }
        .txtComplementoColab
        {
            width: 177px;
        }
        .txtTelResidencialColab
        {
            width: 78px;
        }
        .ddlCidadeColab
        {
            width: 195px;
        }
        .ddlBairroColab
        {
            width: 170px;
        }
        .txtCepColab
        {
            width: 56px;
        }
        .txtTelCelularColab
        {
            width: 78px;
        }
        .ddlTipoContratoColab, .ddlDepartamentoColab
        {
            width: 100px;
        }
        .ddlFuncaoColab
        {
            width: 250px;
        }
        .ddlEspecialidadeColab
        {
            width: 150px;
        }
        .ddlTipoPontoColab
        {
            width: 150px;
        }
        .txtCargaHorariaColab
        {
            width: 35px;
        }
        .txtSalarioBaseColab
        {
            width: 54px;
        }
        .ddlSalarioTipoColab
        {
            width: 70px;
        }
        .txtDtAdmissaoColab, .txtDtSaidaColab
        {
            width: 60px;
        }
        .ddlUnidadeFuncionalColab
        {
            width: 230px;
        }
        .txtEmailColab
        {
            width: 218px;
        }
        .txtCatCnhColab
        {
            width: 25px;
        }
        .txtNomePaiMaeColab
        {
            width: 180px;
        }
        .ddlCorRacaColab
        {
            width: 68px;
        }
        .txtDtCadastro
        {
            width: 60px;
        }
        .ddlSituacaoColab
        {
            width: 125px;
        }
        .ddlTipoVinculoColab
        {
            width: 110px;
        }
        .txtDtSituacaoColab
        {
            width: 60px;
        }
        .ddlPermiMultiFrequ
        {
            width: 40px;
        }
        .ddlGrupoCBO
        {
            width: 40px;
        }
        .ddlFuncaoColab
        {
            width: 63px;
        }
        .txtFuncaoCol
        {
            width: 185px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li>
            <ul>
                <li class="liFotoColab">
                    <fieldset class="fldFotoColab">
                        <uc1:ControleImagem ID="upImagemColab" runat="server" />
                    </fieldset>
                </li>
                <li class="liBloco1">
                    <ul>
                        <li>
                            <label for="txtMatriculaColab" class="lblObrigatorio" title="Matr�cula">
                                Matr�cula</label>
                            <asp:TextBox ID="txtMatriculaColab" ToolTip="Informe a Matr�cula do Funcion�rio"
                                CssClass="txtMatriculaColab" runat="server"></asp:TextBox>
                            <asp:CustomValidator ControlToValidate="txtMatriculaColab" ID="cvMatricula" runat="server"
                                ErrorMessage="Matr�cula j� existente" Text="*" Display="None" CssClass="validatorField"
                                EnableClientScript="false" OnServerValidate="cvValidaMatricula">
                            </asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="rfvMatricula" runat="server" ControlToValidate="txtMatriculaColab"
                                ErrorMessage="Matr�cula deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlCategFuncColab" class="lblObrigatorio" title="Categoria Funcional">
                                Categ. Funcional</label>
                            <asp:DropDownList ID="ddlCategFuncColab" ToolTip="Selecione a Categoria Funcional do Funcion�rio"
                                CssClass="ddlCategFuncColab" runat="server">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="N">Funcion�rio</asp:ListItem>
                                <asp:ListItem Value="S">Professor</asp:ListItem>
                                <asp:ListItem Value="O">Outro</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCategoriaFuncional" runat="server" ControlToValidate="ddlCategFuncColab"
                                ErrorMessage="Categoria Funcional deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liNIS">
                            <label for="txtNISColab" title="N� NIS">
                                N� NIS</label>
                            <asp:TextBox ID="txtNISColab" ToolTip="Informe o N� NIS do Funcion�rio" CssClass="txtNISColab"
                                runat="server" MaxLength="12"></asp:TextBox>
                        </li>
                        <%--                    <li>
                        <label title="N� CNES">N� CNES</label>
                        <asp:TextBox ID="txtCNESColab" runat="server" Width="90px" ToolTip="Informe o N� CNES do funcion�rio" MaxLength="15">
                        </asp:TextBox>
                    </li>--%>
                        <li>
                            <label for="ddlUnidadeFuncionalColab" title="Unidade Funcional">
                                Unidade Funcional</label>
                            <asp:DropDownList ID="ddlUnidadeFuncionalColab" Enabled="false" ToolTip="Selecione a Unidade Funcional"
                                CssClass="ddlUnidadeFuncionalColab" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="txtNomeColab" class="lblObrigatorio" title="Nome">
                                Nome</label>
                            <asp:TextBox ID="txtNomeColab" ToolTip="Informe o Nome do Funcion�rio" CssClass="txtNomeColab"
                                runat="server" MaxLength="60"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNomeColab"
                                ErrorMessage="Nome deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liEspaco">
                            <label for="txtApelidoColab" title="Apelido">
                                Apelido</label>
                            <asp:TextBox ID="txtApelidoColab" ToolTip="Informe o Apelido do Funcion�rio" CssClass="txtApelidoColab"
                                runat="server" MaxLength="15">
                            </asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlSexoColab" title="Sexo">
                                Sexo</label>
                            <asp:DropDownList ID="ddlSexoColab" CssClass="ddlSexoColab" runat="server" ToolTip="Selecione o Sexo do Funcion�rio">
                                <asp:ListItem Value="M">Mas</asp:ListItem>
                                <asp:ListItem Value="F">Fem</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liddlTpSangueF">
                            <label title="Tipo de Sangue">
                                Tp.Sangu�neo</label>
                            <asp:DropDownList ID="ddlTpSangueFColab" ToolTip="Selecione o Tipo de Sangue" CssClass="ddlTpSangueColab"
                                runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="A">A</asp:ListItem>
                                <asp:ListItem Value="B">B</asp:ListItem>
                                <asp:ListItem Value="AB">AB</asp:ListItem>
                                <asp:ListItem Value="O">O</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlStaSangueFColab" ToolTip="Selecione o Status do Tipo de Sangue"
                                CssClass="ddlStaTpSangueColab" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="P">+</asp:ListItem>
                                <asp:ListItem Value="N">-</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="txtDtNascColab" class="lblObrigatorio" title="Data de Nascimento">
                                Data Nascto</label>
                            <asp:TextBox ID="txtDtNascColab" ToolTip="Informe a Data de Nascimento do Funcion�rio"
                                CssClass="txtDtNascColab campoData" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revDtNasc" ControlToValidate="txtDtNascColab"
                                runat="server" ErrorMessage="Data de Nascimento inv�lida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvDtNasc" runat="server" ControlToValidate="txtDtNascColab"
                                ErrorMessage="Data de Nascimento deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <li class="liEspaco">
                                    <label for="ddlNacionalidadeColab" title="Nacionalidade do Funcion�rio">
                                        Nacionalidade</label>
                                    <asp:DropDownList ID="ddlNacionalidadeColab" AutoPostBack="true" OnSelectedIndexChanged="ddlNacionalidadeColab_SelectedIndexChanged"
                                        CssClass="ddlNacionalidadeColab" runat="server" ToolTip="Informe a Nacionalidade do Funcion�rio">
                                        <asp:ListItem Value="B">Brasileiro</asp:ListItem>
                                        <asp:ListItem Value="E">Estrangeiro</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li class="liEspaco">
                                    <label for="txtNaturalidadeColab" title="Cidade de Naturalidade do Funcion�rio">
                                        Naturalidade</label>
                                    <asp:TextBox ID="txtNaturalidadeColab" CssClass="txtNaturalidadeColab" runat="server"
                                        ToolTip="Informe a Cidade de Naturalidade do Funcion�rio" MaxLength="40"></asp:TextBox>
                                </li>
                                <li class="liUfNacionalidadeColab">
                                    <label for="ddlUfNacionalidadeColab" title="UF de Nacionalidade do Funcion�rio">
                                        UF</label>
                                    <asp:DropDownList ID="ddlUfNacionalidadeColab" CssClass="campoUf" runat="server"
                                        ToolTip="Informe a UF de Nacionalidade">
                                    </asp:DropDownList>
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <li class="liOrigem">
                            <label for="ddlOrigem" class="" title="Origem">
                                Origem</label>
                            <asp:DropDownList ID="ddlOrigem" Style="width: 112px;" runat="server" ToolTip="Origem">
                                <asp:ListItem Value="SR" Text="Sem Registro"></asp:ListItem>
                                <asp:ListItem Value="AI" Text="�rea Ind�gena"></asp:ListItem>
                                <asp:ListItem Value="AQ" Text="�rea Quilombo"></asp:ListItem>
                                <asp:ListItem Value="AR" Text="�rea Rural"></asp:ListItem>
                                <asp:ListItem Value="IE" Text="Interior do Estado"></asp:ListItem>
                                <asp:ListItem Value="MU" Text="Munic�pio"></asp:ListItem>
                                <asp:ListItem Value="OE" Text="Outro Estado"></asp:ListItem>
                                <asp:ListItem Value="OO" Text="Outra Origem"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="ddlCorRacaColab" title="Cor/Ra�a do Funcion�rio">
                                Cor/Ra�a</label>
                            <asp:DropDownList ID="ddlCorRacaColab" ToolTip="Informe a Cor/Ra�a do Funcion�rio"
                                CssClass="ddlCorRacaColab" runat="server">
                                <asp:ListItem Value="B">Branca</asp:ListItem>
                                <asp:ListItem Value="A">Amarela</asp:ListItem>
                                <asp:ListItem Value="N">Negra</asp:ListItem>
                                <asp:ListItem Value="P">Parda</asp:ListItem>
                                <asp:ListItem Value="I">Ind�gena</asp:ListItem>
                                <asp:ListItem Value="O">Outra</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlDeficienciaColab" title="Defici�ncia">
                                Defici�ncia</label>
                            <asp:DropDownList ID="ddlDeficienciaColab" ToolTip="Informe se o Funcion�rio possui Defici�ncias"
                                CssClass="ddlDeficienciaColab" runat="server">
                                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                                <asp:ListItem Value="A">Auditivo</asp:ListItem>
                                <asp:ListItem Value="V">Visual</asp:ListItem>
                                <asp:ListItem Value="F">F�sico</asp:ListItem>
                                <asp:ListItem Value="M">Mental</asp:ListItem>
                                <asp:ListItem Value="I">M�ltiplas</asp:ListItem>
                                <asp:ListItem Value="O">Outros</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlEstadoCivilColab" title="Estado Civil">
                                Estado Civil</label>
                            <asp:DropDownList ID="ddlEstadoCivilColab" ToolTip="Selecione o Estado Civil do Funcion�rio"
                                CssClass="ddlEstadoCivilColab" runat="server">
                                <asp:ListItem Value="">N�o Informado</asp:ListItem>
                                <asp:ListItem Value="O">Solteiro(a)</asp:ListItem>
                                <asp:ListItem Value="C">Casado(a)</asp:ListItem>
                                <asp:ListItem Value="S">Separado Judicialmente</asp:ListItem>
                                <asp:ListItem Value="D">Divorciado(a)</asp:ListItem>
                                <asp:ListItem Value="V">Vi�vo(a)</asp:ListItem>
                                <asp:ListItem Value="P">Companheiro(a)</asp:ListItem>
                                <asp:ListItem Value="U">Uni�o Est�vel</asp:ListItem>
                                <asp:ListItem Value="X">Outro</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liEspaco">
                            <label for="ddlGrauInstrucaoColab" title="Grau de Instru��o">
                                Grau de Instru��o</label>
                            <asp:DropDownList ID="ddlGrauInstrucaoColab" ToolTip="Selecione o Grau de Instru��o do Funcion�rio"
                                CssClass="ddlGrauInstrucaoColab" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvGrauInstrucao" runat="server" ControlToValidate="ddlGrauInstrucaoColab"
                                ErrorMessage="Grau de Instru��o deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li class="liBloco2">
            <ul>
                <li style="margin-right: 0px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Informa��o de Contato</label></li>
                        <li class="liClear">
                            <label for="txtEmailColab" title="E-mail">
                                E-mail</label>
                            <asp:TextBox ID="txtEmailColab" ToolTip="Informe o E-mail do Funcion�rio" CssClass="txtEmailColab"
                                runat="server" MaxLength="255"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtTelResidencialColab" title="Telefone Residencial">
                                Tel. Residencial</label>
                            <asp:TextBox ID="txtTelResidencialColab" ToolTip="Informe o Telefone Residencial do Funcion�rio"
                                CssClass="txtTelResidencialColab" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEspaco">
                            <label for="txtTelCelularColab" title="Telefone Celular">
                                Tel. Celular</label>
                            <asp:TextBox ID="txtTelCelularColab" ToolTip="Informe o Telefone Celular do Funcion�rio"
                                CssClass="txtTelResidencialColabCel" runat="server" ClientIDMode="Static" type="tel"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="border-left: 1px solid #BEBEBE; margin-right: 0px; padding-left: 5px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Filia��o</label></li>
                        <li class="liClear">
                            <label for="txtNomeMaeColab" class="lblObrigatorio" title="Nome da M�e">
                                Nome da M�e</label>
                            <asp:TextBox ID="txtNomeMaeColab" ToolTip="Informe o Nome da M�e" CssClass="txtNomePaiMaeColab"
                                runat="server" MaxLength="60"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNomeMae" runat="server" ControlToValidate="txtNomeMaeColab"
                                ErrorMessage="Nome da M�e deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liClear">
                            <label for="txtNomePaiColab" title="Nome do Pai">
                                Nome do Pai</label>
                            <asp:TextBox ID="txtNomePaiColab" ToolTip="Informe o Nome do Pai" CssClass="txtNomePaiMaeColab"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="border-left: 1px solid #BEBEBE; padding-left: 5px;">
                    <ul>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Dados Conjugue</label></li>
                        <li style="margin-left: 10px; clear: none !important;">
                            <asp:CheckBox CssClass="chkLocais" ID="chkConjFunc" TextAlign="Right" runat="server"
                                ToolTip="Conjugue � Funcion�rio" Text="Funcion�rio" /></li>
                        <li id="liNomeConjugue" class="liClear">
                            <label for="txtNomeConjugue" title="Nome do Conjugue">
                                Nome do Conjugue</label>
                            <asp:TextBox ID="txtNomeConjugue" ToolTip="Informe o Nome do Conjugue" CssClass="txtNomePaiMaeColab"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtDtConju" title="Data de Nascimento">
                                Data Nascto</label>
                            <asp:TextBox ID="txtDtConju" ToolTip="Informe a Data de Nascimento do Conjugue" CssClass="txtDtNascColab campoData"
                                runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label for="ddlSexoConjug" title="Sexo">
                                Sexo</label>
                            <asp:DropDownList ID="ddlSexoConjug" CssClass="ddlSexoColab" runat="server" ToolTip="Selecione o Sexo do Conjugue do Funcion�rio">
                                <asp:ListItem Value="M">Mas</asp:ListItem>
                                <asp:ListItem Value="F">Fem</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label for="txtCPFConjug" title="CPF">
                                CPF</label>
                            <asp:TextBox ID="txtCPFConjug" ToolTip="Informe o CPF do Conjugue do Funcion�rio"
                                CssClass="txtCPFColab" runat="server"></asp:TextBox>
                            <asp:CustomValidator ControlToValidate="txtCPFConjug" ID="CustomValidator1" runat="server"
                                ErrorMessage="CPF inv�lido" Text="*" Display="None" CssClass="validatorField"
                                EnableClientScript="false" OnServerValidate="cvValidaCPF"></asp:CustomValidator>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li class="liBloco3">
            <ul>
                <li class="liTitInfCont">
                    <label class="lblTitInf">
                        Informa��es de Documentos</label></li>
                <li style="clear: both; margin-top: 3px;">
                    <label class="lblObrigatorio">
                        Carteira de Identidade</label></li>
                <li class="liClear">
                    <ul>
                        <li>
                            <label for="txtIdentidadeColab" title="N�mero de Identidade">
                                N�mero</label>
                            <asp:TextBox ID="txtIdentidadeColab" ToolTip="Informe o N�mero da Carteira de Identidade"
                                CssClass="txtIdentidadeColab" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtIdentidadeColab"
                                ErrorMessage="N�mero do RG deve ser informado" CssClass="validatorField" Text="*"
                                Display="Static"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="txtDtEmissaoColab" title="Data de Emiss�o da Carteira de Identidade">
                                Data Emiss�o</label>
                            <asp:TextBox ID="txtDtEmissaoColab" ToolTip="Informe a Data de Emiss�o da Carteira de Identidade"
                                CssClass="txtDtEmissaoColab campoData" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtDtEmissaoColab"
                                runat="server" ErrorMessage="Data de Emiss�o inv�lida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDtEmissaoColab"
                                ErrorMessage="Data de Emiss�o do RG deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="txtOrgEmissorColab" title="�rg�o Emissor da Carteira de Identidade">
                                Org�o Emissor</label>
                            <asp:TextBox ID="txtOrgEmissorColab" ToolTip="Informe o �rg�o Emissor da Carteira de Identidade"
                                CssClass="txtOrgEmissorColab" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtOrgEmissorColab"
                                ErrorMessage="�rg�o Emissor deve ser informado" CssClass="validatorField" Text="*"
                                Display="Static"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="ddlIdentidadeUFColab" title="UF do �rg�o Emissor">
                                UF</label>
                            <asp:DropDownList ID="ddlIdentidadeUFColab" ToolTip="Informe a UF do �rg�o Emissor"
                                CssClass="ddlUF" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlIdentidadeUFColab"
                                ErrorMessage="UF da identidade deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                    </ul>
                </li>
                <li style="clear: both; margin-top: 3px;">
                    <label>
                        T�tulo de Eleitor</label></li>
                <li id="liTituloEleitor" class="liClear">
                    <ul>
                        <li class="liClear">
                            <label for="txtNumeroTituloColab" title="N�mero do T�tulo de Eleitor">
                                N�mero</label>
                            <asp:TextBox ID="txtNumeroTituloColab" ToolTip="Informe o N�mero do T�tulo de Eleitor"
                                CssClass="txtNumeroTituloColab" runat="server" MaxLength="15"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtZonaColab" title="Zona Eleitoral">
                                Zona</label>
                            <asp:TextBox ID="txtZonaColab" ToolTip="Informe a Zona Eleitoral" CssClass="txtZonaColab"
                                runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtSecaoColab" title="Se��o Eleitoral">
                                Se��o</label>
                            <asp:TextBox ID="txtSecaoColab" ToolTip="Informe a Se��o Eleitoral" CssClass="txtSecaoColab"
                                runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                        <li>
                            <label for="ddlUfTituloColab" title="UF">
                                UF</label>
                            <asp:DropDownList ID="ddlUfTituloColab" ToolTip="Informe a UF do T�tulo de Eleitor"
                                CssClass="ddlUF" runat="server">
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>
                <li style="clear: both; margin-top: 3px;">
                    <label>
                        CTPS</label></li>
                <li id="liCTPS" class="liClear">
                    <ul>
                        <li class="liClear">
                            <label for="txtNumeroCtpsColab" title="N�mero da Carteira de Trabalho">
                                N�mero</label>
                            <asp:TextBox ID="txtNumeroCtpsColab" ToolTip="Informe o N�mero da Carteira de Trabalho"
                                CssClass="txtNumeroCtpsColab" runat="server" MaxLength="9"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtSerieCtpsColab" title="N�mero de S�rie da Carteira de Trabalho">
                                S�rie</label>
                            <asp:TextBox ID="txtSerieCtpsColab" ToolTip="Informe o N�mero de S�rie da Carteira de Trabalho"
                                CssClass="txtSerieCtpsColab" runat="server" MaxLength="6"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtViaColab" title="Via">
                                Via</label>
                            <asp:TextBox ID="txtViaColab" ToolTip="Informe a Via da Carteira de Trabalho" CssClass="txtViaColab"
                                runat="server" MaxLength="2"></asp:TextBox>
                        </li>
                        <li>
                            <label for="ddlCtpsUFColab" title="UF">
                                UF</label>
                            <asp:DropDownList ID="ddlCtpsUFColab" ToolTip="Informe a UF de origem da Carteira de Trabalho"
                                CssClass="ddlUF" runat="server">
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>
                <li style="clear: both; margin-top: 3px;">
                    <label>
                        CNH</label></li>
                <li class="liClear">
                    <ul>
                        <li>
                            <label for="txtRegCnhColab" title="N� de Regitro da CNH">
                                N� Registro</label>
                            <asp:TextBox ID="txtRegCnhColab" ToolTip="Informe o N� de Regitro da CNH" CssClass="txtRegCnhColab"
                                runat="server" MaxLength="12"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtDocCnhColab" title="N� do Documento da CNH">
                                N� Documento</label>
                            <asp:TextBox ID="txtDocCnhColab" ToolTip="Informe o N� do Documento da CNH" CssClass="txtDocCnhColab"
                                runat="server" MaxLength="10"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtCatCnhColab" title="Categoria da CNH">
                                Categ</label>
                            <asp:TextBox ID="txtCatCnhColab" ToolTip="Informe a Categoria da CNH" CssClass="txtCatCnhColab"
                                runat="server" MaxLength="2"></asp:TextBox>
                        </li>
                        <li class="liValidadeCnhColab">
                            <label for="txtValidadeCnhColab" title="Data de Validade da CNH">
                                Validade</label>
                            <asp:TextBox ID="txtValidadeCnhColab" ToolTip="Informe a Data de Validade da CNH"
                                CssClass="txtValidadeCnhColab campoData" runat="server" MaxLength="30"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li class="liClear" style="margin-top: 3px;">
                    <label for="txtCPFColab" class="lblObrigatorio" title="CPF">
                        N� CPF</label>
                    <asp:TextBox ID="txtCPFColab" ToolTip="Informe o CPF do Funcion�rio" CssClass="txtCPFColab"
                        runat="server"></asp:TextBox>
                    <asp:CustomValidator ControlToValidate="txtCPFColab" ID="cvCPF" runat="server" ErrorMessage="CPF inv�lido"
                        Text="*" Display="None" CssClass="validatorField" EnableClientScript="false"
                        OnServerValidate="cvValidaCPF"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="rfvCPF" runat="server" ControlToValidate="txtCPFColab"
                        ErrorMessage="CPF deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-top: 3px;">
                    <label for="txtPisPasepColab" title="PIS/PASEP">
                        PIS/PASEP</label>
                    <asp:TextBox ID="txtPisPasepColab" ToolTip="Informe o PIS/PASEP do Funcion�rio" CssClass="txtPisPasepColab"
                        runat="server" MaxLength="20"></asp:TextBox>
                </li>
                <li style="margin-top: 3px;">
                    <label for="txtPassaporteColab" title="Passaporte">
                        Passaporte</label>
                    <asp:TextBox ID="txtPassaporteColab" ToolTip="Informe o Passaporte do Funcion�rio"
                        CssClass="txtPassaporteColab" runat="server" MaxLength="9"></asp:TextBox>
                </li>
                <li style="margin-top: 3px;">
                    <label for="txtConvSaudeColab" title="N�mero do Cart�o Sa�de">
                        Cart�o Sa�de</label>
                    <asp:TextBox ID="txtConvSaudeColab" ToolTip="Informe o N�mero do Cart�o Sa�de do Funcion�rio"
                        CssClass="txtConvSaudeColab" runat="server" MaxLength="16"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li style="margin-top: 10px;">
            <ul>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <li class="liTitInfCont">
                            <label class="lblTitInf">
                                Endere�o Residencial</label></li>
                        <li class="liClear">
                            <label for="txtCepColab" class="lblObrigatorio" title="CEP">
                                CEP</label>
                            <asp:TextBox ID="txtCepColab" ToolTip="Informe o CEP" CssClass="txtCepColab" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCep" runat="server" ControlToValidate="txtCepColab"
                                ErrorMessage="CEP deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liPesqCEPR">
                            <asp:ImageButton ID="btnPesqCEPColab" runat="server" OnClick="btnPesquisarCepColab_Click"
                                ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" class="btnPesqMat" CausesValidation="false" />
                        </li>
                        <li>
                            <label for="txtLogradouroColab" class="lblObrigatorio" title="Logradouro">
                                Logradouro</label>
                            <asp:TextBox ID="txtLogradouroColab" ToolTip="Informe o Logradouro" CssClass="txtLogradouroColab"
                                runat="server" MaxLength="40"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLogradouro" runat="server" ControlToValidate="txtLogradouroColab"
                                ErrorMessage="Endere�o deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liNumeroColab">
                            <label for="txtNumeroColab" title="N�mero">
                                N�mero</label>
                            <asp:TextBox ID="txtNumeroColab" ToolTip="Informe o N�mero da resid�ncia" CssClass="txtNumeroColab"
                                runat="server" MaxLength="5"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtComplementoColab" title="Complemento">
                                Complemento</label>
                            <asp:TextBox ID="txtComplementoColab" ToolTip="Informe o Complemento" CssClass="txtComplementoColab"
                                runat="server" MaxLength="40"></asp:TextBox>
                        </li>
                        <li>
                            <label for="ddlBairroColab" class="lblObrigatorio" title="Bairro">
                                Bairro</label>
                            <asp:DropDownList ID="ddlBairroColab" ToolTip="Selecione o Bairro" CssClass="ddlBairroColab"
                                runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBairroColab"
                                ErrorMessage="Bairro deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liClear">
                            <label for="ddlCidadeColab" class="lblObrigatorio" title="Cidade">
                                Cidade</label>
                            <asp:DropDownList ID="ddlCidadeColab" ToolTip="Selecione a Cidade" CssClass="ddlCidadeColab"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCidadeColab"
                                ErrorMessage="Cidade deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="ddlUfColab" class="lblObrigatorio" title="UF">
                                UF</label>
                            <asp:DropDownList ID="ddlUfColab" ToolTip="Selecione o Estado" Width="40px" CssClass="ddlUf"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvUf" runat="server" ControlToValidate="ddlUfColab"
                                ErrorMessage="UF deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <li style="clear: none; margin-left: 5px; margin-top: 13px; margin-right: 0px;">
                    <asp:CheckBox CssClass="chkLocais" ID="chkResPro" TextAlign="Right" runat="server"
                        ToolTip="Funcion�rio possui Resid�ncia Pr�pria" Text="Resid�ncia Pr�pria" />
                </li>
                <li class="liObsResponsavel liClear" style="margin-top: 13px !important;">
                    <label for="txtObservacoesFuncColab" style="font-weight: bold; text-transform: uppercase;"
                        title="Observa��es">
                        Observa��es</label>
                    <asp:TextBox ID="txtObservacoesFuncColab" CssClass="txtObservacoesFuncColab" ToolTip="Informe as Observa��es sobre o Funcion�rio"
                        runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 250);"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li style="border-left: 1px solid #BEBEBE; padding-left: 16px; margin-left: 6px;
            margin-top: 10px;">
            <ul>
                <li class="liTitInfCont" style="margin-left: -10px; width: 590px">
                    <label class="lblTitInf" style="float: left">
                        Informa��o Funcional</label>
                    <ul>
                        <li style="margin-left: 150px !important;">
                            <label for="ddlTipoContratoColab" style="font-weight: bold; margin-bottom: 5px;"
                                title="Tipo de Contrato">
                                Tipo de Atividades</label>
                            <asp:CheckBox ID="chkAtiviInter" runat="server" ToolTip="Selecione se o colaborador estiver em atividade interna" />
                            <asp:Label ID="lblAtiviInter" runat="server" ToolTip="Selecione se o colaborador estiver em atividade interna"
                                Style="margin-left: -5px">Ativ. Interna</asp:Label>
                        </li>
                        <li class="desLiTipoAtiv" style="margin-left: -20px !important;">
                            <asp:CheckBox ID="chkAtiviExter" Style="float: left;" runat="server" ToolTip="Selecione se o colaborador estiver em atividade externa" />
                            <asp:Label ID="lblAtiviExter" Style="float: right; margin-left: -5px" runat="server"
                                ToolTip="Selecione se o colaborador estiver em atividade externa">Ativ. Externa</asp:Label>
                        </li>
                        <li class="desLiTipoAtiv" style="margin-left: -4px;">
                            <asp:CheckBox ID="chkAtiviDomic" Style="float: left;" runat="server" ToolTip="Selecione se o colaborador estiver em atividade domiciliar" />
                            <asp:Label ID="lblAtiviDomic" Style="float: right; margin-left: -5px" runat="server"
                                ToolTip="Selecione se o colaborador estiver em atividade domiciliar">Domiciliar</asp:Label>
                        </li>
                        <li class="desLiTipoAtiv" style="margin-left: -4px;">
                            <asp:CheckBox ID="chkFlMonitor" Style="float: left;" runat="server" ToolTip="Selecione se o colaborador for um plantonista" />
                            <asp:Label ID="lblFlMonitoria" Style="float: right; margin-left: -5px" runat="server"
                                ToolTip="Selecione se o colaborador for um plantonista">Monitoria</asp:Label>
                        </li>
                    </ul>
                </li>
                <li class="liClear">
                    <ul>
                        <li class="liClear">
                            <label for="ddlTipoContratoColab" title="Tipo de Contrato">
                                Tipo de Contrato</label>
                            <asp:DropDownList ID="ddlTipoContratoColab" ToolTip="Selecione o Tipo de Contrato do Funcion�rio"
                                CssClass="ddlTipoContratoColab" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label for="txtDtAdmissaoColab" class="lblObrigatorio" title="Data de Admiss�o">
                                Data Admiss�o</label>
                            <asp:TextBox ID="txtDtAdmissaoColab" ToolTip="Informe a Data de Admiss�o" CssClass="txtDtAdmissaoColab campoData"
                                runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtDtAdmissaoColab"
                                runat="server" ErrorMessage="Data de Admiss�o inv�lida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvDtAdmissao" runat="server" ControlToValidate="txtDtAdmissaoColab"
                                ErrorMessage="Data de Admiss�o deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="txtDtSaidaColab" title="Data de Sa�da">
                                Data Sa�da</label>
                            <asp:TextBox ID="txtDtSaidaColab" ToolTip="Informe a Data de Desligamento da empresa"
                                CssClass="txtDtSaidaColab campoData" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtDtSaidaColab"
                                runat="server" ErrorMessage="Data de Sa�da inv�lida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                                Display="None"></asp:RegularExpressionValidator>
                        </li>
                        <li class="liClear">
                            <label for="ddlUnidadeFuncionalColab" title="Unidade Funcional">
                                Unidade de Contrato</label>
                            <asp:DropDownList ID="ddlUnidadeContrato" ToolTip="Selecione a Unidade Contrato"
                                CssClass="ddlUnidadeFuncionalColab" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="ddlDepartamentoColab" title="Departamento">
                                Departamento</label>
                            <asp:DropDownList ID="ddlDepartamentoColab" ToolTip="Selecione o Departamento do Funcion�rio"
                                CssClass="ddlDepartamentoColab" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label for="ddlTipoPontoColab" title="Tipo de Ponto">
                                Tipo de Ponto</label>
                            <asp:DropDownList ID="ddlTipoPontoColab" ToolTip="Selecione o Tipo de Ponto do Funcion�rio"
                                CssClass="ddlTipoPontoColab" runat="server">
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>
                <li style="margin-top: 12px; margin-left: 6px;">
                    <ul>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <li>
                                    <label for="ddlGrupoCBO" class="lblObrigatorio" title="Grupo CBO">
                                        Grupo</label>
                                    <asp:DropDownList ID="ddlGrupoCBO" AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoCBO_SelectedIndexChanged"
                                        ToolTip="Selecione o Grupo CBO" CssClass="ddlGrupoCBO" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlGrupoCBO"
                                        ErrorMessage="Grupo CBO deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label for="ddlFuncaoColab" class="lblObrigatorio" title="Selecione o CBO">
                                        CBO</label>
                                    <asp:DropDownList ID="ddlFuncaoColab" ToolTip="Selecione a Fun��o do Funcion�rio"
                                        Width="257px" CssClass="ddlFuncaoColab" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvFuncao" runat="server" ControlToValidate="ddlFuncaoColab"
                                        ErrorMessage="CBO deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <%--            <li>
                        <label for="txtFuncaoCol" class="lblObrigatorio" title="Informe a fun�ao do colaborador">Fun&ccedil;&atilde;o</label>
                        <asp:TextBox ID="txtFuncaoCol" CssClass="txtFuncaoCol" runat="server" MaxLength="200"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFuncaoCol" ErrorMessage="Fun��o deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                    </li>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <li class="liClear">
                            <label for="ddlSalarioTipoColab" title="Tipo de Pagamento">
                                Tipo Pagto</label>
                            <asp:DropDownList ID="ddlSalarioTipoColab" ToolTip="Selecione o Tipo de Pagamento do Funcion�rio"
                                CssClass="ddlSalarioTipoColab" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label for="txtCargaHorariaColab" class="lblObrigatorio" title="Carga Hor�ria">
                                C. Hr.</label>
                            <asp:TextBox ID="txtCargaHorariaColab" ToolTip="Informe a Carga Hor�ria do Funcion�rio"
                                CssClass="txtCargaHorariaColab" runat="server" MaxLength="7"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCargaHorariaColab"
                                ErrorMessage="Carga Hor�ria deve ser informada" CssClass="validatorField" Text="*"
                                Display="Static"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="txtSalarioBaseColab" title="Sal�rio Base">
                                R$ Base</label>
                            <asp:TextBox ID="txtSalarioBaseColab" ToolTip="Informe o Sal�rio Base" CssClass="txtSalarioBaseColab"
                                runat="server" MaxLength="9"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revSalarioBase" ControlToValidate="txtSalarioBaseColab"
                                runat="server" ErrorMessage="R$ Base deve ter formato decimal separado por v�rgula"
                                ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None"></asp:RegularExpressionValidator>
                        </li>
                        <li>
                            <label for="txtSalarioColab" title="Sal�rio">
                                R$ Sal�rio</label>
                            <asp:TextBox ID="txtSalarioColab" ToolTip="Sal�rio" CssClass="txtSalarioBaseColab"
                                runat="server" MaxLength="9" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtSalarioColab"
                                runat="server" ErrorMessage="R$ Sal�rio deve ter formato decimal separado por v�rgula"
                                ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None"></asp:RegularExpressionValidator>
                        </li>
                        <li style="float: right;">
                            <label for="ddlPermiMultiFrequ" title="Permite Multifrequ�ncia?">
                                Multifreq?</label>
                            <asp:DropDownList ID="ddlPermiMultiFrequ" ToolTip="Permite Multifrequ�ncia?" CssClass="ddlPermiMultiFrequ"
                                runat="server">
                                <asp:ListItem Value="S" Selected="true">Sim</asp:ListItem>
                                <asp:ListItem Value="N">N�o</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <label for="txtEmailFuncColab" title="E-mail">
                                E-mail</label>
                            <asp:TextBox ID="txtEmailFuncColab" ToolTip="Informe o E-mail do Funcion�rio" CssClass="txtEmailFuncColab"
                                runat="server" MaxLength="255"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtTelFuncColab" title="Telefone Comercial">
                                Tel. Comercial</label>
                            <asp:TextBox ID="txtTelFuncColab" ToolTip="Informe o Telefone Comercial do Funcion�rio"
                                CssClass="txtTelResidencialColab" runat="server"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li class="liClear">
                    <label title="Selecione uma categoria funcional para o colaborador" class="lblObrigatorio">
                        Categ. Funcional</label>
                    <asp:DropDownList ID="ddlCategFuncional" runat="server" Width="150px" ToolTip="Selecione uma catgoria funcional para o colaborador">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Selecione uma fun��o para o colaborador" class="lblObrigatorio">
                        Fun��o</label>
                    <asp:DropDownList ID="ddlFuncFuncional" runat="server" Width="140px" ToolTip="Seleceione uma fun��o para o colaborador">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Selecione o grupo de especialidade">
                        Grupo de Especialidade</label>
                    <asp:DropDownList ID="ddlGrupoEspeci" OnSelectedIndexChanged="ddlGrupoEspeci_SelectedIndexChanged"
                        AutoPostBack="true" runat="server" Width="120px" ToolTip="Selecione o grupo de especialidade">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="ddlEspecialidadeColab" title="Especialidade">
                        Especialidade</label>
                    <asp:DropDownList ID="ddlEspecialidadeColab" ToolTip="Selecione a Especialidade do Funcion�rio"
                        CssClass="ddlEspecialidadeColab" runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liSituacao liClear">
                    <label for="ddlSituacaoColab" class="lblObrigatorio" title="Situa��o Atual">
                        Situa��o Atual</label>
                    <asp:DropDownList ID="ddlSituacaoColab" ToolTip="Selecione a Situa��o Atual do Funcion�rio"
                        CssClass="ddlSituacaoColab" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacaoColab"
                        ErrorMessage="Situa��o deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liTipoVinculoColab">
                    <label for="ddlTipoVinculoColab" title="Tipo de V�nculo">
                        Tipo</label>
                    <asp:DropDownList ID="ddlTipoVinculoColab" ToolTip="Selecione o Tipo de V�nculo"
                        CssClass="ddlTipoVinculoColab" runat="server">
                        <asp:ListItem Value="R">Remunerado</asp:ListItem>
                        <asp:ListItem Value="N">N�o Remunerado</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liDtSituacaoColab">
                    <label for="txtDtSituacaoColab" class="lblObrigatorio" title="Data da Situa��o">
                        Data Situa��o</label>
                    <asp:TextBox ID="txtDtSituacaoColab" Enabled="false" CssClass="txtDtSituacaoColab campoData"
                        runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtDtSituacaoColab"
                        runat="server" ErrorMessage="Data da Situa��o inv�lida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$"
                        Display="None"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvDtSituacao" runat="server" ControlToValidate="txtDtSituacaoColab"
                        ErrorMessage="Data da Situa��o deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtCepColab").mask("99999-999");
            $(".txtNumeroColab").mask("?999999");
        });

        // Somente letras mai�sculas e min�sculas
        $(".txtCatCnhColab").keyup(function () {
            var valor = $(".txtCatCnhColab").val().replace(/[^a-zA-Z]+/g, '');
            $(".txtCatCnhColab").val(valor);
        });

        $(document).ready(function () {
            var datePickerOptions = { showOn: 'button',
                buttonImage: '../../../../Library/IMG/Gestor_IcoCalendario.gif',
                buttonImageOnly: true,
                inline: true,
                duration: 'fast',
                yearRange: '-110:+110'
            };

            $(".txtCepColab").mask("99999-999");
            $(".txtNumeroColab").mask("?999999");
            $(".txtCargaHorariaColab").mask("?9999");
            $(".txtNIS").mask("999999999999");
            $(".txtTelResidencialColab").mask("(99) 9999-9999");
            $(".txtTelResidencialColabCel").mask("(99) 9999-9999?9");
            $(".txtTelCelularColab").mask("(99) 9999-9999");
            $(".txtCPFColab").mask("999.999.999-99");
            $(".txtPassaporteColab").mask("?999999999");
            $(".txtMatriculaColab").mask("99.999-9");
            $(".txtPisPasepColab").mask("999.99999.99-9");

            $(".txtNumeroCtpsColab").mask("?999999999");
            $(".txtSerieCtpsColab").mask("?999999");
            $(".txtViaColab").mask("?99");

            $(".txtRegCnhColab").mask("?999999999999");
            $(".txtDocCnhColab").mask("?9999999999");
            $(".txtConvSaudeColab").mask("?9999999999999999");

            $(".txtSalarioBaseColab").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtSalario").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>
