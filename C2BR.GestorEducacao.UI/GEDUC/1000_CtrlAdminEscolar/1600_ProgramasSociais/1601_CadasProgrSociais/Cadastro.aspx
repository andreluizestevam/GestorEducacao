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
            <legend>Gestor do Programa/Conv�nio S�cio-Educacional</legend>
            <ul>
                <li>
                    <label for="txtInstituicao" title="Institui��o">Institui��o</label>
                    <asp:TextBox ID="txtInstituicao" CssClass="campoUnidadeEscolar" runat="server" Enabled="false"
                        ToolTip="Institui��o" />
                </li>
                <li style="clear:both;">
                    <label for="ddlUnidadeResponsavel" class="lblObrigatorio" title="Unidade do Respons�vel pelo Programa/Conv�nio S�cio-Educacional">Unidade</label>
                    <asp:DropDownList ID="ddlUnidadeResponsavel" CssClass="ddlUnidadeResponsavel" runat="server" Enabled="false"
                        ToolTip="Selecione a Unidade do Respons�vel pelo Programa/Conv�nio S�cio-Educacional" 
                        onselectedindexchanged="ddlUnidadeResponsavel_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUnidadeResponsavel" CssClass="validatorField"
                        ErrorMessage="Unidade do Respons�vel pelo Programa/Conv�nio deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlColaboradorResponsavel" class="lblObrigatorio" title="Colaborador Respons�vel pelo Programa/Conv�nio S�cio-Educacional">Colaborador</label>
                    <asp:DropDownList ID="ddlColaboradorResponsavel" CssClass="ddlColaboradorResponsavel" runat="server"
                        ToolTip="Selecione Colaborador Respons�vel pelo Programa/Conv�nio S�cio-Educacional">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlColaboradorResponsavel" CssClass="validatorField"
                        ErrorMessage="Colaborador Respons�vel pelo Programa/Conv�nio deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
            </ul>
        </fieldset>
        
        <fieldset style="margin-top:10px;">
            <legend>Dados do Programa/Conv�nio S�cio-Educacional</legend>
            <ul>
                <li>
                    <label for="ddlTipoBeneficio" class="lblObrigatorio" title="Tipo do Benef�cio S�cio-Educacional">Tipo Benef�cio</label>
                    <asp:DropDownList ID="ddlTipoBeneficio" CssClass="ddlTipoBeneficio" runat="server"
                        ToolTip="Selecione o Tipo do Benef�cio S�cio-Educacional">
                        <asp:ListItem Value="C" Selected="True">Conv�nio</asp:ListItem>
                        <asp:ListItem Value="P">Programa</asp:ListItem>
                        <asp:ListItem Value="O">Outros</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTipoBeneficio" CssClass="validatorField"
                        ErrorMessage="Tipo do Benef�cio deve ser informado.">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlTipoProgramaConvenio" class="lblObrigatorio" title="Tipo do Programa/Conv�nio S�cio-Educacional">Tipo Programa/Conv�nio</label>
                    <asp:DropDownList ID="ddlTipoProgramaConvenio" CssClass="ddlTipoProgramaConvenio" runat="server"
                        ToolTip="Selecione o Tipo do Programa/Conv�nio S�cio-Educacional">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipoProgramaConvenio" CssClass="validatorField"
                        ErrorMessage="Tipo do Programa/Conv�nio deve ser informado.">
                    </asp:RequiredFieldValidator>
                </li>
                <li style="clear:both;">
                    <label for="txtNomeProgramaSocial" class="lblObrigatorio" 
                        title="Nome do Programa/Conv�nio S�cio-Educacional">Nome do Programa/Conv�nio</label>
                    <asp:TextBox ID="txtNomeProgramaSocial" CssClass="txtNomeProgramaSocial" runat="server" MaxLength="70"
                        ToolTip="Informe o Nome do Programa/Conv�nio S�cio-Educacional" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNomeProgramaSocial" CssClass="validatorField"
                        ErrorMessage="Nome deve ser informado.">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtNomeReduzidoProgramaSocial" class="lblObrigatorio" title="Nome Reduzido do Programa/Conv�nio S�cio-Educacional">Nome Reduzido</label>
                    <asp:TextBox ID="txtNomeReduzidoProgramaSocial" CssClass="txtNomeReduzidoProgramaSocial" runat="server" MaxLength="40"
                        ToolTip="Informe o Nome Reduzido do Programa/Conv�nio S�cio-Educacional" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNomeReduzidoProgramaSocial" CssClass="validatorField"
                        ErrorMessage="Nome Reduzido deve ser informado.">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtSigla" class="lblObrigatorio" title="Sigla do Programa/Conv�nio S�cio-Educacional">Sigla</label>
                    <asp:TextBox ID="txtSigla" CssClass="txtSigla" runat="server" MaxLength="15"
                        ToolTip="Informe a Sigla do Programa/Conv�nio S�cio-Educacional" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSigla" CssClass="validatorField"
                        ErrorMessage="Sigla deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
                
                <li style="clear:both;">
                    <label for="txtObjetivo" title="Objetivo do Programa/Conv�nio">Objetivo</label>
                    <asp:TextBox ID="txtObjetivo" CssClass="txtObjetivo" runat="server" TextMode="MultiLine" Rows="1"
                        onkeyup="javascript:MaxLength(this, 200);"
                        ToolTip="Informe o Objetivo do Programa/Conv�nio" />
                </li>
                
                <li style="clear:both;height:6px;"></li>
                
                <li style="clear:both;">
                    <label for="txtNumeroConvenio" title="N�mero do Conv�nio">N� Conv�nio</label>
                    <asp:TextBox ID="txtNumeroConvenio" CssClass="txtNumeroContratoConvenio" runat="server"
                        ToolTip="Informe o N�mero do Conv�nio" />
                </li>
                <li>
                    <label for="txtNumeroContrato" title="N�mero do Contrato">N� Contrato</label>
                    <asp:TextBox ID="txtNumeroContrato" CssClass="txtNumeroContratoConvenio" runat="server"
                        ToolTip="Informe o N�mero do Contrato" />
                </li>
                <li>
                    <label for="txtDataContrato" class="lblObrigatorio" title="Data do Contrato">Data Contrato</label>
                    <asp:TextBox ID="txtDataContrato" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data do Contrato do Programa/Conv�nio S�cio-Educacional" />             
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDataContrato" CssClass="validatorField"
                        ErrorMessage="Data do Contrato deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
                <li style="clear:both;">
                    <label for="txtDataValidadePrograma" title="Data de Validade do Programa/Conv�nio S�cio-Educacional">Data Validade</label>
                    <asp:TextBox ID="txtDataValidadePrograma" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data de Validade do Programa/Conv�nio S�cio-Educacional" />
                </li>
                <li style="margin-left:10px;">
                    <label for="txtDataPrevisao" class="lblObrigatorio" title="Data de Previs�o do Contrato">Data Previs�o</label>
                    <asp:TextBox ID="txtDataPrevisao" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data de Previs�o do Contrato do Programa/Conv�nio S�cio-Educacional" />             
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDataPrevisao" CssClass="validatorField"
                        ErrorMessage="Data de Previs�o do Contrato deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
                <li style="margin-left:10px;">
                    <label for="txtDataTermino" title="Data de T�rmino do Contrato">Data T�rmino</label>
                    <asp:TextBox ID="txtDataTermino" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data de T�rmino do Contrato do Programa/Conv�nio S�cio-Educacional" />
                </li>
            </ul>
        </fieldset>
        </li>
        
        <li>
        <ul>
        
        <%--DADOS DO RESPONS�VEL NO �RG�O GESTOR--%>
        <li class="liDadosRespOrgaoGestor">
        <fieldset>
            <legend>Par�metros do Programa/Conv�nio S�cio-Educacional</legend>
            <ul>
                <li style="clear:both;">
                    <label for="txtPercentFrequencia" title="Percentual Minimo de Frequ�ncia">% MF</label>
                    <asp:TextBox ID="txtPercentFrequencia" CssClass="txtPercentFrequencia" runat="server"
                        ToolTip="Informe o Percentual M�nimo de Frequ�ncia" />
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
            <legend>Dados do Respons�vel pelo Programa/Conv�nio no �rg�o Regulador</legend>
            <ul>
                <li>
                    <label for="txtNomeOrgaoGestor" title="Nome do �rg�o Gestor do Programa/Conv�nio S�cio-Educacional">Nome do �rg�o Gestor</label>
                    <asp:TextBox ID="txtNomeOrgaoGestor" CssClass="txtNomeOrgaoGestor" runat="server" MaxLength="60"
                        ToolTip="Informe o Nome do �rg�o Gestor do Programa/Conv�nio S�cio-Educacional" />
                </li>
                <li>
                    <label for="txtSiglaOrgaoGestor" title="Sigla do �rg�o Gestor do Programa/Conv�nio S�cio-Educacional">Sigla</label>
                    <asp:TextBox ID="txtSiglaOrgaoGestor" CssClass="campoSigla" runat="server" MaxLength="12"
                        ToolTip="Informe a Sigla do �rg�o Gestor do Programa/Conv�nio S�cio-Educacional" />
                </li>
                <li>
                    <label for="txtResponsavelOrgaoGestor" title="Nome do Respons�vel pelo Programa/Conv�nio S�cio-Educacional no �rg�o Gestor">Nome do Respons�vel</label>
                    <asp:TextBox ID="txtResponsavelOrgaoGestor" CssClass="campoNomePessoa" runat="server" MaxLength="60"
                        ToolTip="Informe o Nome do Respons�vel Programa/Conv�nio S�cio-Educacional no �rg�o Gestor" />
                </li>
                <li>
                    <label for="txtDeptoRespOrgaoGestor" title="Departamento do Respons�vel pelo Programa/Conv�nio S�cio-Educacional no �rg�o Gestor">Departamento</label>
                    <asp:TextBox ID="txtDeptoRespOrgaoGestor" CssClass="campoDptoCurso" runat="server" MaxLength="60"
                        ToolTip="Informe o Departamento do Respons�vel Programa/Conv�nio S�cio-Educacional no �rg�o Gestor" />
                </li>
                <li>
                    <label for="txtTelRespOrgaoGestor" title="Telefone do Respons�vel pelo Programa/Conv�nio S�cio-Educacional no �rg�o Gestor">Telefone</label>
                    <asp:TextBox ID="txtTelRespOrgaoGestor" CssClass="campoTelefone" runat="server"
                        ToolTip="Informe o Telefone do Respons�vel Programa/Conv�nio S�cio-Educacional no �rg�o Gestor" />
                </li>
                <li>
                    <label for="txtEmailRespOrgaoGestor" title="E-mail do Respons�vel pelo Programa/Conv�nio S�cio-Educacional no �rg�o Gestor">E-mail</label>
                    <asp:TextBox ID="txtEmailRespOrgaoGestor" CssClass="campoEmail" runat="server" MaxLength="255"
                        ToolTip="Informe o E-mail do Respons�vel Programa/Conv�nio S�cio-Educacional no �rg�o Gestor" />
                </li>
            </ul>
        </fieldset>
        </li>
        
        <li style="clear:both;float:right;">
        <fieldset>
            <legend>Situa��o</legend>
            <ul>
                <li>
                    <label for="txtDataCadastro" title="Data de Cadastro">Data Cadastro</label>
                    <asp:TextBox ID="txtDataCadastro" CssClass="campoData" Enabled ="false" runat="server" 
                        ToolTip="Data de Cadastro do Programa/Conv�nio S�cio-Educacional" />
                </li>
                <li style="margin-left:10px;">
                    <label for="ddlSituacao" class="lblObrigatorio" title="Situa��o do Programa/Conv�nio S�cio-Educacional">Situa��o</label>
                    <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" 
                        ToolTip="Informe a Situa��o do Programa/Conv�nio S�cio-Educacional" >
                        <asp:ListItem Selected="True" Value="A">Ativo</asp:ListItem>
                        <asp:ListItem Value="I">Inativo</asp:ListItem>
                        <asp:ListItem Value="S">Suspenso</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSituacao" CssClass="validatorField"
                        ErrorMessage="Situa��o deve ser informada.">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtDataSituacao" class="lblObrigatorio" title="Data da Situa��o">Data Situa��o</label>
                    <asp:TextBox ID="txtDataSituacao" Enabled="False" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data da Situa��o do Programa/Conv�nio S�cio-Educacional" />             
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDataSituacao" CssClass="validatorField"
                        ErrorMessage="Data de Situa��o deve ser informada.">
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