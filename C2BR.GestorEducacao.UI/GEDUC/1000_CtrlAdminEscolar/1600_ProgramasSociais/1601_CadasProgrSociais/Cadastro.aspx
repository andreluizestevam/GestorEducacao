<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1600_ProgramasSociais.F1601_CadasProgrSociais.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados{ width: 855px; }
    fieldset { padding: 10px 0 0 10px !important; border-width: 0px; }
    .ulDados input, .ulDados select{ margin-bottom: 0;}
    
    /*--> CSS LIs */
    fieldset li { margin: -5px 5px 0 0; }    
    fieldset legend { font-size: 12px; }
    .ulDados li{ margin-bottom: 10px; margin-right: 10px;}        
    .liDadosRespOrgaoGestor {width:400px;clear:both;}
        
    /*--> CSS DADOS */
    .ddlTipoBeneficio {width:68px;}
    .ddlTipoProgramaConvenio {width:120px;}
    .campoUnidadeEscolar {width:265px !important;}
    .txtNomeProgramaSocial {width:200px;}
    .ddlUnidadeResponsavel {width:200px;}
    .txtNomeOrgaoGestor {width:200px;}
    .campoNomePessoa {width:200px !important;}
    .ddlColaboradorResponsavel {width:185px;}
    .campoTelefone {width:76px !important;}
    .campoEmail {width:135px !important;}
    .campoDptoCurso {width:135px !important;}
    .ddlTipo {width:180px;}
    .txtTipo {width:210px;}
    .txtSigla {width:60px;}
    .ddlSituacao {width:80px;}
    .txtObjetivo {width:393px;}
    .txtNumeroContratoConvenio {width:80px;text-align:right;}        
    .txtPercentFrequencia {width:45px;text-align:right;}
    .txtRendaFamiliar {width:60px;text-align:right;}
    .txtQtDependentes {width:18px;text-align:right;}
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">
    <li>
        <fieldset>
            <legend>Gestor do Programa/Convênio Sócio-Educacional</legend>
            <ul>
                <li>
                    <label for="txtInstituicao" title="Instituição">Instituição</label>
                    <asp:TextBox ID="txtInstituicao" CssClass="campoUnidadeEscolar" runat="server" Enabled="false"
                        ToolTip="Instituição" />
                </li>
                <li style="clear:both;">
                    <label for="ddlUnidadeResponsavel" class="lblObrigatorio" title="Unidade do Responsável pelo Programa/Convênio Sócio-Educacional">Unidade</label>
                    <asp:DropDownList ID="ddlUnidadeResponsavel" CssClass="ddlUnidadeResponsavel" runat="server" Enabled="false"
                        ToolTip="Selecione a Unidade do Responsável pelo Programa/Convênio Sócio-Educacional" 
                        onselectedindexchanged="ddlUnidadeResponsavel_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUnidadeResponsavel" CssClass="validatorField"
                        ErrorMessage="Unidade do Responsável pelo Programa/Convênio deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlColaboradorResponsavel" class="lblObrigatorio" title="Colaborador Responsável pelo Programa/Convênio Sócio-Educacional">Colaborador</label>
                    <asp:DropDownList ID="ddlColaboradorResponsavel" CssClass="ddlColaboradorResponsavel" runat="server"
                        ToolTip="Selecione Colaborador Responsável pelo Programa/Convênio Sócio-Educacional">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlColaboradorResponsavel" CssClass="validatorField"
                        ErrorMessage="Colaborador Responsável pelo Programa/Convênio deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
            </ul>
        </fieldset>
        
        <fieldset style="margin-top:10px;">
            <legend>Dados do Programa/Convênio Sócio-Educacional</legend>
            <ul>
                <li>
                    <label for="ddlTipoBeneficio" class="lblObrigatorio" title="Tipo do Benefício Sócio-Educacional">Tipo Benefício</label>
                    <asp:DropDownList ID="ddlTipoBeneficio" CssClass="ddlTipoBeneficio" runat="server"
                        ToolTip="Selecione o Tipo do Benefício Sócio-Educacional">
                        <asp:ListItem Value="C" Selected="True">Convênio</asp:ListItem>
                        <asp:ListItem Value="P">Programa</asp:ListItem>
                        <asp:ListItem Value="O">Outros</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTipoBeneficio" CssClass="validatorField"
                        ErrorMessage="Tipo do Benefício deve ser informado.">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlTipoProgramaConvenio" class="lblObrigatorio" title="Tipo do Programa/Convênio Sócio-Educacional">Tipo Programa/Convênio</label>
                    <asp:DropDownList ID="ddlTipoProgramaConvenio" CssClass="ddlTipoProgramaConvenio" runat="server"
                        ToolTip="Selecione o Tipo do Programa/Convênio Sócio-Educacional">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipoProgramaConvenio" CssClass="validatorField"
                        ErrorMessage="Tipo do Programa/Convênio deve ser informado.">
                    </asp:RequiredFieldValidator>
                </li>
                <li style="clear:both;">
                    <label for="txtNomeProgramaSocial" class="lblObrigatorio" 
                        title="Nome do Programa/Convênio Sócio-Educacional">Nome do Programa/Convênio</label>
                    <asp:TextBox ID="txtNomeProgramaSocial" CssClass="txtNomeProgramaSocial" runat="server" MaxLength="70"
                        ToolTip="Informe o Nome do Programa/Convênio Sócio-Educacional" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNomeProgramaSocial" CssClass="validatorField"
                        ErrorMessage="Nome deve ser informado.">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtNomeReduzidoProgramaSocial" class="lblObrigatorio" title="Nome Reduzido do Programa/Convênio Sócio-Educacional">Nome Reduzido</label>
                    <asp:TextBox ID="txtNomeReduzidoProgramaSocial" CssClass="txtNomeReduzidoProgramaSocial" runat="server" MaxLength="40"
                        ToolTip="Informe o Nome Reduzido do Programa/Convênio Sócio-Educacional" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNomeReduzidoProgramaSocial" CssClass="validatorField"
                        ErrorMessage="Nome Reduzido deve ser informado.">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtSigla" class="lblObrigatorio" title="Sigla do Programa/Convênio Sócio-Educacional">Sigla</label>
                    <asp:TextBox ID="txtSigla" CssClass="txtSigla" runat="server" MaxLength="15"
                        ToolTip="Informe a Sigla do Programa/Convênio Sócio-Educacional" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSigla" CssClass="validatorField"
                        ErrorMessage="Sigla deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
                
                <li style="clear:both;">
                    <label for="txtObjetivo" title="Objetivo do Programa/Convênio">Objetivo</label>
                    <asp:TextBox ID="txtObjetivo" CssClass="txtObjetivo" runat="server" TextMode="MultiLine" Rows="1"
                        onkeyup="javascript:MaxLength(this, 200);"
                        ToolTip="Informe o Objetivo do Programa/Convênio" />
                </li>
                
                <li style="clear:both;height:6px;"></li>
                
                <li style="clear:both;">
                    <label for="txtNumeroConvenio" title="Número do Convênio">N° Convênio</label>
                    <asp:TextBox ID="txtNumeroConvenio" CssClass="txtNumeroContratoConvenio" runat="server"
                        ToolTip="Informe o Número do Convênio" />
                </li>
                <li>
                    <label for="txtNumeroContrato" title="Número do Contrato">N° Contrato</label>
                    <asp:TextBox ID="txtNumeroContrato" CssClass="txtNumeroContratoConvenio" runat="server"
                        ToolTip="Informe o Número do Contrato" />
                </li>
                <li>
                    <label for="txtDataContrato" class="lblObrigatorio" title="Data do Contrato">Data Contrato</label>
                    <asp:TextBox ID="txtDataContrato" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data do Contrato do Programa/Convênio Sócio-Educacional" />             
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDataContrato" CssClass="validatorField"
                        ErrorMessage="Data do Contrato deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
                <li style="clear:both;">
                    <label for="txtDataValidadePrograma" title="Data de Validade do Programa/Convênio Sócio-Educacional">Data Validade</label>
                    <asp:TextBox ID="txtDataValidadePrograma" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data de Validade do Programa/Convênio Sócio-Educacional" />
                </li>
                <li style="margin-left:10px;">
                    <label for="txtDataPrevisao" class="lblObrigatorio" title="Data de Previsão do Contrato">Data Previsão</label>
                    <asp:TextBox ID="txtDataPrevisao" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data de Previsão do Contrato do Programa/Convênio Sócio-Educacional" />             
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDataPrevisao" CssClass="validatorField"
                        ErrorMessage="Data de Previsão do Contrato deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
                <li style="margin-left:10px;">
                    <label for="txtDataTermino" title="Data de Término do Contrato">Data Término</label>
                    <asp:TextBox ID="txtDataTermino" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data de Término do Contrato do Programa/Convênio Sócio-Educacional" />
                </li>
            </ul>
        </fieldset>
        </li>
        
        <li>
        <ul>
        
        <%--DADOS DO RESPONSÁVEL NO ÓRGÃO GESTOR--%>
        <li class="liDadosRespOrgaoGestor">
        <fieldset>
            <legend>Parâmetros do Programa/Convênio Sócio-Educacional</legend>
            <ul>
                <li style="clear:both;">
                    <label for="txtPercentFrequencia" title="Percentual Minimo de Frequência">% MF</label>
                    <asp:TextBox ID="txtPercentFrequencia" CssClass="txtPercentFrequencia" runat="server"
                        ToolTip="Informe o Percentual Mínimo de Frequência" />
                </li>
                <li>
                    <label for="txtRendaFamiliar" title="Renda Familiar">Renda Familiar</label>
                    <asp:TextBox ID="txtRendaFamiliar" CssClass="txtRendaFamiliar money" runat="server"
                        ToolTip="Informe a Renda Familiar" />
                </li>
                <li>
                    <label for="txtQtDependentes" title="Quantidade de Dependentes">QD</label>
                    <asp:TextBox ID="txtQtDependentes" CssClass="txtQtDependentes" runat="server"
                        ToolTip="Informe a Quantidade de Dependentes" />
                </li>
            </ul>
        </fieldset>
        
        <fieldset style="margin-top:10px;">
            <legend>Dados do Responsável pelo Programa/Convênio no Órgão Regulador</legend>
            <ul>
                <li>
                    <label for="txtNomeOrgaoGestor" title="Nome do Órgão Gestor do Programa/Convênio Sócio-Educacional">Nome do Órgão Gestor</label>
                    <asp:TextBox ID="txtNomeOrgaoGestor" CssClass="txtNomeOrgaoGestor" runat="server" MaxLength="60"
                        ToolTip="Informe o Nome do Órgão Gestor do Programa/Convênio Sócio-Educacional" />
                </li>
                <li>
                    <label for="txtSiglaOrgaoGestor" title="Sigla do Órgão Gestor do Programa/Convênio Sócio-Educacional">Sigla</label>
                    <asp:TextBox ID="txtSiglaOrgaoGestor" CssClass="campoSigla" runat="server" MaxLength="12"
                        ToolTip="Informe a Sigla do Órgão Gestor do Programa/Convênio Sócio-Educacional" />
                </li>
                <li>
                    <label for="txtResponsavelOrgaoGestor" title="Nome do Responsável pelo Programa/Convênio Sócio-Educacional no Órgão Gestor">Nome do Responsável</label>
                    <asp:TextBox ID="txtResponsavelOrgaoGestor" CssClass="campoNomePessoa" runat="server" MaxLength="60"
                        ToolTip="Informe o Nome do Responsável Programa/Convênio Sócio-Educacional no Órgão Gestor" />
                </li>
                <li>
                    <label for="txtDeptoRespOrgaoGestor" title="Departamento do Responsável pelo Programa/Convênio Sócio-Educacional no Órgão Gestor">Departamento</label>
                    <asp:TextBox ID="txtDeptoRespOrgaoGestor" CssClass="campoDptoCurso" runat="server" MaxLength="60"
                        ToolTip="Informe o Departamento do Responsável Programa/Convênio Sócio-Educacional no Órgão Gestor" />
                </li>
                <li>
                    <label for="txtTelRespOrgaoGestor" title="Telefone do Responsável pelo Programa/Convênio Sócio-Educacional no Órgão Gestor">Telefone</label>
                    <asp:TextBox ID="txtTelRespOrgaoGestor" CssClass="campoTelefone" runat="server"
                        ToolTip="Informe o Telefone do Responsável Programa/Convênio Sócio-Educacional no Órgão Gestor" />
                </li>
                <li>
                    <label for="txtEmailRespOrgaoGestor" title="E-mail do Responsável pelo Programa/Convênio Sócio-Educacional no Órgão Gestor">E-mail</label>
                    <asp:TextBox ID="txtEmailRespOrgaoGestor" CssClass="campoEmail" runat="server" MaxLength="255"
                        ToolTip="Informe o E-mail do Responsável Programa/Convênio Sócio-Educacional no Órgão Gestor" />
                </li>
            </ul>
        </fieldset>
        </li>
        
        <li style="clear:both;float:right;">
        <fieldset>
            <legend>Situação</legend>
            <ul>
                <li>
                    <label for="txtDataCadastro" title="Data de Cadastro">Data Cadastro</label>
                    <asp:TextBox ID="txtDataCadastro" CssClass="campoData" Enabled ="false" runat="server" 
                        ToolTip="Data de Cadastro do Programa/Convênio Sócio-Educacional" />
                </li>
                <li style="margin-left:10px;">
                    <label for="ddlSituacao" class="lblObrigatorio" title="Situação do Programa/Convênio Sócio-Educacional">Situação</label>
                    <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" 
                        ToolTip="Informe a Situação do Programa/Convênio Sócio-Educacional" >
                        <asp:ListItem Selected="True" Value="A">Ativo</asp:ListItem>
                        <asp:ListItem Value="I">Inativo</asp:ListItem>
                        <asp:ListItem Value="S">Suspenso</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSituacao" CssClass="validatorField"
                        ErrorMessage="Situação deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtDataSituacao" class="lblObrigatorio" title="Data da Situação">Data Situação</label>
                    <asp:TextBox ID="txtDataSituacao" Enabled="False" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data da Situação do Programa/Convênio Sócio-Educacional" />             
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDataSituacao" CssClass="validatorField"
                        ErrorMessage="Data de Situação deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
            </ul>
        </fieldset>
        </li>
        </ul>
        </li>
</ul>
<script type="text/javascript">
    jQuery(function ($) {
        $(".campoTelefone").mask("(99) 9999-9999");
        $(".txtNumeroContratoConvenio").mask("?99999999");
        $(".txtPercentFrequencia").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        $(".money").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        $(".txtQtDependentes").mask("?99");
    });
    </script>
</asp:Content>