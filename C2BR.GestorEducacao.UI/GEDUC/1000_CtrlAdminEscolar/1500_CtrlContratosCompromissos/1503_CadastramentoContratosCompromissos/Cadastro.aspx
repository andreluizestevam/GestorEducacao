<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1500_CtrlContratosCompromissos.F1503_CadastramentoContratosCompromissos.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">  
    .divFormData { width: 955px; margin: auto auto auto 35px; }
    input[type="text"] 
    {
    	font-size: 10px !important;
    	font-family: Arial !important;
    }
    select 
    {
    	margin-bottom: 0;
	    font-family: Arial !important;
        border: 1px solid #BBBBBB !important;
        font-size:0.9em !important;
        height: 15px !important;
    }

    /*--> CSS LIs */
    .liInforContrato { clear: both; width: 100%; margin: 4px 0 0 380px !important; }
    .liClear { clear: both; }
    .liSituacao { margin-top: 5px; float: right !important; padding-right: 10px; }
    .liDtCadastro, .liDtStatus, .liValorDocumento { margin-left: 5px; }    
    .liConta { clear: both; margin-top: 10px; border-right: 1px solid #DDDDDD; padding-right: 15px; margin-right: 3px !important; }
    .liFinanceiro { margin: 10px 0 0 12px; }    
    .liFinanceiro li { margin-bottom: 5px; }
    .liDtCancelamento { float: right !important; }
    .liStatus { margin-left: 15px; }
    .liFinan { margin-top: 8px; }
    .liObjetContr { margin-bottom: 10px !important; }
    .liNumPubli { margin-left: 20px; }
    .liEspaco { margin-left: 5px; }
    .liTipoCliente { margin-left: 130px; }
    .liLeftFinanceiro { border-right: 1px solid #DDDDDD; padding-right: 10px; }
    
    /*--> CSS DADOS */
    .liFinanceiro input[type="text"] { margin-bottom: 0; }
    .liFinanceiro select { margin-bottom: 0; }
    .fldClassificacao, .fldFinanceiro { padding: 5px 5px 4px 9px; }
    .imgEnd { margin: 28px 0 0 5px; }    
    .ddlTipoCliente { width: 80px; }
    .ddlNomeFantasia { width: 205px; }
    .txtCNPJ, .txtCPF { width: 100px; }
    .txtLogradouro { width: 218px; }
    .txtComplemento { width: 95px; }
    .txtBairro { width: 170px; }
    .txtCidade { width: 195px; }
    .txtUF { width: 20px; }
    .txtCEP { width: 54px; }
    .txtMoney { text-align: right; width: 67px; }
    .txtQtd { width: 16px; }
    .txtValorDocumento { text-align: right; }
    .ddlStatus, .txtNumEmpen { width: 70px; }
    .txtNumDoc { width: 96px; }
    .txtNumAditivo { width: 35px; }
    .lblCNPJ { display:block;}
    .ddlCateg{ width: 210px; }
    .liFinan label { display: inline; }
    .txtTitulContrat { width: 423px; }
    .txtObjetContrat { width: 423px; height: 38px; }
    .txtLocalPubli { width: 230px; }
    .ddlPadraoBoletBanca { width: 195px; }
    .ddlDotacOrcam { width: 110px; }
    .txtElemeDespe { width: 103px; }
    .ddlAnoReferDotac { width: 50px; }
    
</style>
<script type="text/javascript">
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">
    <li class="liUnidade">
        <ul>
            <li class="liTipoCliente">
                <label for="ddlTipoCliente" title="Tipo do Cliente">Tipo do Cliente</label>
                <asp:DropDownList ID="ddlTipoCliente" 
                    ToolTip="Selecione o Tipo do Cliente"
                    CssClass="ddlTipoCliente" runat="server"
                    Enabled="false">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="F">Física</asp:ListItem>
                    <asp:ListItem Value="J">Jurídica</asp:ListItem>
                    <asp:ListItem Value="O">Outros</asp:ListItem>
                </asp:DropDownList>
            </li>               
            <li class="liEspaco">
                <label for="ddlNomeFantasia" class="lblObrigatorio" title="Nome Fantasia">Nome Fantasia</label>
                <asp:DropDownList ID="ddlNomeFantasia" ToolTip="Selecione o Nome Fantasia" runat="server"
                     CssClass="ddlNomeFantasia"
                     AutoPostBack="true"
                     onselectedindexchanged="ddlNomeFantasia_SelectedIndexChanged"
                     Enabled="false">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlNomeFantasia" 
                    ErrorMessage="Selecione o Cliente" Display="None">
                </asp:RequiredFieldValidator>
            </li>        
            <li class="liEspaco">
                <asp:Label id="lblCNPJ" CssClass="lblCNPJ" runat="server" ToolTip="CNPJ" Text="CNPJ"></asp:Label>
                <asp:TextBox ID="txtCNPJ" 
                    ToolTip="Informe o CNPJ"
                    CssClass="txtCNPJ" runat="server"
                    Enabled="false"></asp:TextBox>
            </li>
            <li class="liEspaco">
                <label for="txtCidade" title="Cidade">Cidade</label>
                <asp:TextBox ID="txtCidade" 
                    CssClass="txtCidade" runat="server"
                    Enabled="false">
                </asp:TextBox>
            </li>
            <li class="liEspaco">
                <label for="txtUF" title="UF">UF</label>
                <asp:TextBox ID="txtUF" 
                    CssClass="txtUF" runat="server"
                    Enabled="false">
                </asp:TextBox>
            </li>
        </ul>
    </li>      

    <li class="liInforContrato">
        <ul>
            <li>
                <label for="txtNumContrato" class="lblObrigatorio" title="Número do Contrato">N° Contrato</label>
                <asp:TextBox ID="txtNumContrato" MaxLength="10"
                    ToolTip="Informe o Número do Contrato" style="background-color:#E6E6FA; font-weight:bold;"
                    CssClass="txtNumDoc" runat="server"
                    Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNumContrato" 
                    ErrorMessage="N° Contrato deve ser informado" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="txtNumAditivo" title="Número do Aditivo">AD</label>
                <asp:TextBox ID="txtNumAditivo" style="background-color:#E6E6FA; font-weight:bold;"
                    ToolTip="Informe o Número do Aditivo"
                    CssClass="txtNumAditivo" runat="server"
                    Enabled="false">0</asp:TextBox>
            </li>
        </ul>
    </li>

    <li class="liConta">
        <ul>
            <li>
                <label for="ddlCateg" title="Tipo de Categoria">Categoria</label>
                <asp:DropDownList ID="ddlCateg"  CssClass="ddlCateg" runat="server" ToolTip="Selecione a Categoria"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlCateg_SelectedIndexChanged">
                </asp:DropDownList>
            </li>
            <li style="clear: none;">
                <label for="ddlSubCateg" title="Tipo de SubCategoria">SubCategoria</label>
                <asp:DropDownList ID="ddlSubCateg"  CssClass="ddlCateg" runat="server" ToolTip="Selecione a SubCategoria">
                </asp:DropDownList>
            </li>
            <li class="liClear" style="margin-top: 10px;">
                <label for="txtTitulContrat" class="lblObrigatorio" title="Número do Aditivo">Título do Contrato</label>
                <asp:TextBox ID="txtTitulContrat" MaxLength="60"
                    ToolTip="Informe o Título do Contrato"
                    CssClass="txtTitulContrat" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtTitulContrat" 
                    ErrorMessage="Título do Contrato deve ser informado" Display="None">
                </asp:RequiredFieldValidator>
            </li>            
            <li class="liClear liObjetContr">
                <label for="txtObjetContrat" title="Objeto Contratual">Objeto Contratual</label>
                <asp:TextBox ID="txtObjetContrat" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);"
                    ToolTip="Informe o Objetivo Contratual" style="overflow-y: hidden;"
                    CssClass="txtObjetContrat" runat="server"></asp:TextBox>
            </li>
            <li class="liClear">
                <label for="txtTitulContrat" title="Local da Publicação">Local da Publicação</label>
                <asp:TextBox ID="txtLocalPubli" MaxLength="60"
                    ToolTip="Informe o Local da Publicação"
                    CssClass="txtLocalPubli" runat="server"></asp:TextBox>
            </li>
            <li class="liEspaco">
                <label for="txtNumPublicacao" title="Número da Publicação">N° Publicação</label>
                <asp:TextBox ID="txtNumPublicacao" 
                    ToolTip="Informe o Número da Publicação"
                    CssClass="txtNumDoc" runat="server"
                    Enabled="false"></asp:TextBox>
            </li>            
            <li class="liEspaco">
                <label for="txtDtPublicacao" title="Data de Publicação">Dt Publicação</label>
                <asp:TextBox ID="txtDtPublicacao" 
                    ToolTip="Informe a Data de Publicação"
                    CssClass="campoData" runat="server">
                </asp:TextBox>
            </li>
            <li class="liClear">
                <label for="txtDtContrato" title="Data de Assinatura do Contrato">Assinatura</label>
                <asp:TextBox ID="txtDtContrato" Enabled="false"
                    ToolTip="Informe a Data de Assinatura do Contrato"
                    CssClass="campoData" runat="server">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDtContrato" 
                    ErrorMessage="Data de Assinatura do Contrato deve ser informada" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="txtDtInicioContrato" class="lblObrigatorio" title="Data de Início do Contrato">Início</label>
                <asp:TextBox ID="txtDtInicioContrato" 
                    ToolTip="Informe a Data de Início do Contrato"
                    CssClass="campoData" runat="server">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDtInicioContrato" 
                    ErrorMessage="Data de Início do Contrato deve ser informada" Display="None">
                </asp:RequiredFieldValidator>
            </li>            
            <li>
                <label for="txtDtFimContrato" class="lblObrigatorio" title="Data de Término do Contrato">Término</label>
                <asp:TextBox ID="txtDtFimContrato" 
                    ToolTip="Informe a Data de Término do Contrato"
                    CssClass="campoData" runat="server">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDtFimContrato" 
                    ErrorMessage="Data de Término do Contrato deve ser informada" Display="None">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtDtFimContrato" runat="server"
                    ErrorMessage="Data de Término não pode ser menor que a Data de Início do Contrato" Display="None"
                    CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataTerminoContrato_ServerValidate">
                </asp:CustomValidator>
            </li>
            <li style="margin-left: 19px;">
                <label for="txtValorDocumento" class="lblObrigatorio" title="Valor do Contrato">R$ Contrato</label>
                <asp:TextBox ID="txtValorDocumento" ToolTip="Informe o Valor do Contrato" CssClass="txtMoney" runat="server" MaxLength="9"
                    Enabled="false"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ControlToValidate="txtValorDocumento" runat="server" 
                    ErrorMessage="Valor do Contrato deve ter formato decimal separado por vírgula" 
                    ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtValorDocumento" 
                    ErrorMessage="Valor do Contrato deve ser informado" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            <li style="margin-left: 20px;">
                <label for="txtDtCancelamento" title="Data de Cancelamento do Contrato">Cancelamento</label>
                <asp:TextBox ID="txtDtCancelamento" 
                    ToolTip="Informe a Data de Cancelamento do Contrato"
                    CssClass="campoData" runat="server">
                </asp:TextBox>
                <asp:CustomValidator ID="CustomValidator3" ControlToValidate="txtDtCancelamento" runat="server"
                    ErrorMessage="Data de Cancelamento não pode ser menor que a Data de Cadastro" Display="None"
                    CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataCancelamento_ServerValidate">
                </asp:CustomValidator>
            </li>
            <li class="liClear" style="margin-bottom: 25px;">
                <label for="txtObserContr" title="Observação do Contrato">Observação</label>
                <asp:TextBox ID="txtObserContr" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);"
                    ToolTip="Informe a Observação do Contrato" style="overflow-y: hidden;"
                    CssClass="txtObjetContrat" runat="server"></asp:TextBox>
            </li>
            <li class="liClear">
                <label for="txtDtCadastro" title="Data Cadastro">Data Cadastro</label>
                <asp:TextBox ID="txtDtCadastro" 
                    ToolTip="Informe a Data de Cadastro"
                    CssClass="campoData" Enabled="false" runat="server"></asp:TextBox>
            </li>   
            <li>
                <label for="txtDtStatus" title="Data Status" class="lblObrigatorio">Data Status</label>
                <asp:TextBox ID="txtDtStatus" Enabled="False"
                    ToolTip="Informe a Data Status"
                    CssClass="campoData" runat="server">
                </asp:TextBox>
            </li> 
            <li>
                <label for="ddlStatus" class="lblObrigatorio" title="Status">Status</label>
                <asp:DropDownList ID="ddlStatus" runat="server"
                    ToolTip="Selecione o Status"
                    CssClass="ddlStatus">
                    <asp:ListItem Value="A">Em Aberto</asp:ListItem>
                    <asp:ListItem Value="C">Cancelado</asp:ListItem>
                    <asp:ListItem Value="Q">Quitado</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlStatus" 
                    ErrorMessage="Status deve ser informado" Display="None">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator4" ControlToValidate="ddlStatus" runat="server"
                    ErrorMessage="Data de Status não pode ser menor que a Data de Cadastro" Display="None"
                    CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataStatus_ServerValidate">
                </asp:CustomValidator>
            </li>                
        </ul>
    </li>       
    
    <li style="width: 490px; padding-left: 5px;">
        <ul>
            <li class="liClear liFinanceiro">
                <ul>  
                    <li style="width: 100%; margin-bottom: 0;"><label style="font-weight: bold;" title="Informações Financeiras">Informações Financeiras</label></li>
                    <li class="liLeftFinanceiro">
                        <ul>
                            <li class="liFinan liClear" style="margin-left: -5px;">
                                <asp:CheckBox ID="chkAtualFinan" CssClass="chkAtualFinan" ToolTip="Informe se atualizará Financeiro" runat="server" Text="Atualiza Financeiro"/>
                            </li>  
                            <li class="liFinan">
                                <asp:CheckBox ID="chkGeraBoleto" CssClass="chkAtualFinan" 
                                    ToolTip="Informe se gerará boleto bancário" runat="server" Text="Gera Boleto" AutoPostBack="true"
                                    oncheckedchanged="chkGeraBoleto_CheckedChanged"/>
                            </li>    
                            <li class="liClear">
                                <label for="txtDtPrimeiroVencto" class="lblObrigatorio" title="Data do 1° vencimento">Data 1° Vecto</label>
                                <asp:TextBox ID="txtDtPrimeiroVencto" 
                                    ToolTip="Informe a Data de Vencimento da primeira parcela"
                                    CssClass="campoData" runat="server"
                                    Enabled="false">
                                </asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator2" ControlToValidate="txtDtPrimeiroVencto" runat="server"
                                    ErrorMessage="Data do 1° Vencimento não pode ser menor que a Data de Início do Contrato" Display="None"
                                    CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataPrimeiroVencto_ServerValidate">
                                </asp:CustomValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDtPrimeiroVencto" 
                                    ErrorMessage="Data 1° Vencimento da Parcela deve ser informada" Display="None">
                                </asp:RequiredFieldValidator>
                            </li>
                            <li class="liDtPrimeiroVencto">
                                <label for="txtValorPrimParc" class="lblObrigatorio" title="Valor da Primeira Parcela do Contrato">R$ 1º Vecto</label>
                                <asp:TextBox ID="txtValorPrimParc" ToolTip="Informe o Valor da Primeira Parcela do Contrato" Enabled="false" CssClass="txtMoney" runat="server" MaxLength="9"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator32" ControlToValidate="txtValorPrimParc" runat="server" 
                                    ErrorMessage="Valor da Primeira Parcela do Contrato deve ter formato decimal separado por vírgula" 
                                    ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None">
                                </asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtValorPrimParc" 
                                    ErrorMessage="Valor da Primeira Parcela do Contrato deve ser informado" Display="None">
                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label for="txtQtParcelas" class="lblObrigatorio" title="Quantidade de Parcelas do Contrato">QP</label>
                                <asp:TextBox ID="txtQtParcelas" 
                                    ToolTip="Informe a Quantidade de Parcelas do Contrato"
                                    CssClass="txtQtd" runat="server"
                                    Enabled="false">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtQtParcelas" 
                                    ErrorMessage="Quantidade de Parcelas deve ser informada" Display="None">
                                </asp:RequiredFieldValidator>
                            </li> 
                            <li>
                                <label for="txtDiasIntervalo" class="lblObrigatorio" title="Dias de intervalo entre parcelas">DI</label>
                                <asp:TextBox ID="txtDiasIntervalo" 
                                    ToolTip="Informe a quantidade de Dias de intervalo entre parcelas"
                                    CssClass="txtQtd" runat="server"
                                    Enabled="false">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDiasIntervalo" 
                                    ErrorMessage="Intervalo entre Parcelas deve ser informado" Display="None">
                                </asp:RequiredFieldValidator>
                            </li>  
                            <li class="liClear">
                                <label for="ddlTipoDocumento" class="lblObrigatorio" title="Tipo de Documento">Tipo Documento</label>
                                <asp:DropDownList ID="ddlTipoDocumento" ToolTip="Selecione o Tipo de Documento"
                                    CssClass="ddlTipoDocumento" runat="server" Enabled="false">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTipoDocumento" 
                                    ErrorMessage="Tipo Documento deve ser informado" Display="None">
                                </asp:RequiredFieldValidator>
                            </li>            
                            <li style="margin-left: 5px;">
                                <label for="txtNumDoc" class="lblObrigatorio" title="Número do Documento">N° Documento</label>
                                <asp:TextBox ID="txtNumDoc" 
                                    ToolTip="Informe o Número do Documento"
                                    CssClass="txtNumDoc" runat="server"
                                    Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNumDoc" 
                                    ErrorMessage="N° Documento deve ser informado" Display="None">
                                </asp:RequiredFieldValidator>
                            </li>   
                            <li class="liClear">
                                <label for="ddlPadraoBoletBanca" title="Padrão de Boleto Bancário">Padrão de Boleto Bancário</label>
                                <asp:DropDownList ID="ddlPadraoBoletBanca" ToolTip="Selecione o Padrão de Boleto Bancário" Enabled="false"
                                    CssClass="ddlPadraoBoletBanca" runat="server">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdfOcorrCadas" runat="server" />
                            </li>                                                                
                        </ul>
                    </li>  
                    <li style="padding-left: 10px;">
                        <ul>
                            <li><label style="text-transform: uppercase;" title="Informações de Correção">Informações de Correção</label></li>
                            <li style="clear:both; border-right: 1px solid #F0F0F0;">
                                <label for="txtMultaPercent" style="clear: none; float: left;" title="Valor da Multa sobre a parcela">Multa</label>
                                <asp:TextBox ID="txtMulta" style="margin-left: 21px;" ToolTip="Informe o Valor da Multa sobre a parcela" runat="server"
                                    CssClass="txtMoney" MaxLength="9"></asp:TextBox>
                                <asp:CheckBox ID="cbFlagPercentualMulta" runat="server" /><span style="margin-left:-5px !important;">%</span>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtMulta" runat="server" 
                                    ErrorMessage="Valor da Multa deve ter formato decimal separado por vírgula" 
                                    ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None">
                                </asp:RegularExpressionValidator>
                            </li>    
                            <li style="clear:both;">
                                <label for="txtJurosPercent" style="clear: none; float: left;" title="Valor dos Juros sobre a parcela">Juros</label>
                                <asp:TextBox ID="txtJuros" style="margin-left: 21px;" ToolTip="Informe o Valor dos Juros sobre a parcela" runat="server"
                                    CssClass="txtMoney"></asp:TextBox>
                                <asp:CheckBox ID="cbFlagPercentualJuros" runat="server" /><span style="margin-left:-5px !important;">%</span>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtJuros" runat="server" 
                                    ErrorMessage="Valor dos Juros deve ter formato decimal separado por vírgula" 
                                    ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None">
                                </asp:RegularExpressionValidator>
                            </li>
                            <li style="clear:both;">
                                <label for="txtDescontoPercent" style="clear: none; float: left;" title="Valor do Desconto/Mês sobre a parcela">Desconto</label>
                                <asp:TextBox ID="txtDesconto" style="margin-left: 3px;" ToolTip="Informe o Valor do Desconto/Mês sobre a parcela" runat="server"
                                    CssClass="txtMoney"></asp:TextBox>
                                <asp:CheckBox ID="cbFlagPercentualDesconto" runat="server" /><span style="margin-left:-5px !important;">%</span>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtDesconto" runat="server" 
                                    ErrorMessage="Valor do Desconto/Mês deve ter formato decimal separado por vírgula" 
                                    ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None">
                                </asp:RegularExpressionValidator>
                            </li>
                            <li class="liClear" style="margin: 2px 0 2px 0;"><label style="text-transform: uppercase;" title="Empenho">Empenho</label></li>
                            <li class="liClear">
                                <label for="txtNumEmpen" title="Número do Empenho">N° Empenho</label>
                                <asp:TextBox ID="txtNumEmpen" 
                                    ToolTip="Informe o Número do Empenho"
                                    CssClass="txtNumEmpen" runat="server"></asp:TextBox>
                            </li>
                            <li>
                                <label for="txtDtEmpenho" title="Data Empenho">Dt Empenho</label>
                                <asp:TextBox ID="txtDtEmpenho" 
                                    ToolTip="Informe a Data de Empenho"
                                    CssClass="campoData" runat="server">
                                </asp:TextBox>
                            </li>
                        </ul>
                    </li>         
                </ul>
            </li>

            <li style="padding-left: 10px;">                
                <ul>
                    <li style="width: 100%; margin-bottom: 0;"><label style="font-weight: bold;" title="Informações Classificação">Informações Classificação</label></li>
                    <li style="border-right: 1px solid #DDDDDD; padding-right: 5px; height: 95px;">
                        <ul>
                            <li class="liClear">
                                <label for="ddlDepartamento" title="Departamento">Departamento</label>
                                <asp:DropDownList ID="ddlDepartamento" runat="server" 
                                    ToolTip="Selecione o Departamento" 
                                    onselectedindexchanged="ddlDepartamento_SelectedIndexChanged"
                                    AutoPostBack="true" Width="220px">
                                </asp:DropDownList>
                            </li>
                            <li class="liClear" style="margin-top: 5px;">
                                <label for="ddlCentroCusto" title="Centro de Custo">Centro de Custo</label>
                                <asp:DropDownList ID="ddlCentroCusto" runat="server" 
                                    ToolTip="Selecione o Centro de Custo" Width="220px">
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </li>
                    <li style="padding-left: 5px;">
                        <ul>
                            <li>
                                <label for="ddlAnoReferDotac" title="Ano de Referência da Dotação Orçamentária">Ano Ref</label>
                                <asp:DropDownList ID="ddlAnoReferDotac" ToolTip="Selecione o Ano de Referência da Dotação Orçamentária"
                                    CssClass="ddlAnoReferDotac" runat="server" AutoPostBack="true" onselectedindexchanged="ddlAnoReferDotac_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label for="ddlDotacOrcam" title="Dotação Orçamentária">Dotação Orçamentária</label>
                                <asp:DropDownList ID="ddlDotacOrcam" ToolTip="Selecione a Dotação Orçamentária"
                                    CssClass="ddlDotacOrcam" runat="server">
                                </asp:DropDownList>
                            </li>
                            <li class="liClear" style="margin-top: 5px;">
                                <label for="txtElemeDespe" title="Elemento Despesa">Elemento Despesa</label>
                                <asp:TextBox ID="txtElemeDespe" style="margin-bottom: 0px;"
                                    ToolTip="Informe o Elemento Despesa"
                                    CssClass="txtElemeDespe" runat="server"></asp:TextBox>
                            </li>
                            <li class="liClear" style="margin-top: 5px;">
                                <label for="ddlOrdenRespo" title="Ordenador (Responsável)">Ordenador (Responsável)</label>
                                <asp:DropDownList ID="ddlOrdenRespo" runat="server" 
                                    ToolTip="Selecione o Ordenador (Responsável)" Width="220px">
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </li>                                                                                    
                </ul>
            </li>

            <li style="padding-left: 10px;">                                
                <ul>
                    <li style="width: 100%; margin-bottom: 0;"><label style="font-weight: bold;" title="Informações Contábeis">Informações Contábeis</label></li>
                    <li>
                        <label for="ddlTipoConta" title="Tipo de Conta" class="lblObrigatorio labelPixel">Tipo de Conta</label>
                        <asp:DropDownList ID="ddlTipoConta" ToolTip="Selecione o Tipo de Conta" Width="110px" runat="server"
                        onselectedindexchanged="ddlTipoConta_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                            <asp:ListItem Value="D">4 - Custo e Despesa</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlTipoConta" 
                        ErrorMessage="Tipo de Conta deve ser informado" Display="None">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="ddlGrupo" class="lblObrigatorio" title="Grupo">
                            Grupo</label>
                        <asp:DropDownList ID="ddlGrupo" runat="server" Width="90px"
                            AutoPostBack="True" onselectedindexchanged="ddlGrupo_SelectedIndexChanged"
                            ToolTip="Selecione o Grupo">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlGrupo" 
                            ErrorMessage="Grupo deve ser informado" Display="None">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li style="clear: none; margin-left: 5px;">
                        <label for="ddlSubGrupoReceita" class="lblObrigatorio" title="Subgrupo">SubGrupo</label>
                        <asp:DropDownList ID="ddlSubGrupoReceita" runat="server" Width="120px"
                            ToolTip="Selecione o Subgrupo"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoReceita_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlSubGrupoReceita" 
                            ErrorMessage="Subgrupo deve ser informado" Display="None">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="ddlSubGrupo" class="lblObrigatorio" title="Subgrupo 2">
                            SubGrupo 2</label>
                        <asp:DropDownList ID="ddlSubGrupo2" runat="server" Width="121px" AutoPostBack="True" 
                            onselectedindexchanged="ddlSubGrupo2_SelectedIndexChanged"
                            ToolTip="Selecione o Subgrupo 2">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlSubGrupo2"
                            CssClass="validatorField" ErrorMessage="SubGrupo 2 deve ser informado" Text="*"
                            Display="Static"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liClear" style="margin-top: 5px;">
                        <label for="ddlContaContabil" class="lblObrigatorio" title="Conta Contábil">Conta Cont&aacute;bil</label>
                        <asp:DropDownList ID="ddlContaContabil" ToolTip="Selecione a Conta Contábil" runat="server" Width="220px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlContaContabil" 
                            ErrorMessage="Selecione a Conta Contábil" Display="None">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li style="margin-top: 5px;">
                        <label for="ddlHistLancamento" class="lblObrigatorio" title="Histórico Padrão de Lançamento">Hist&oacute;rico Padr&atilde;o de Lan&ccedil;amento</label>
                        <asp:DropDownList ID="ddlHistLancamento" Width="236px"
                            ToolTip="Selecione o Histórico Padrão de Lançamento"
                            CssClass="ddlHistLancamento" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlHistLancamento" 
                            ErrorMessage="Histórico Padrão deve ser informado" Display="None">
                        </asp:RequiredFieldValidator>
                    </li>
                </ul>
            </li>
        </ul>
    </li>       
</ul>
<script type="text/javascript">
    $(document).ready(function() {
        $(".txtCNPJ").mask("99.999.999/9999-99");
        $(".txtCPF").mask("999.999.999-99");
        $(".txtCEP").mask("99999-999");
        $('.txtQtd').mask("?99");
        $('.txtElemeDespe').mask("?9999999");        
        $(".txtMoney").maskMoney({ symbol: "", decimal: ",", thousands: "." });
    });
</script>
</asp:Content>