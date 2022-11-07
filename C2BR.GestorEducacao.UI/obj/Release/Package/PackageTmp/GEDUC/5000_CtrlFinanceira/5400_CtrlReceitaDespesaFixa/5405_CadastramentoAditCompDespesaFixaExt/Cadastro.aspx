<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5405_CadastramentoAditCompDespesaFixaExt.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">      
    .divFormData { width: 666px; margin: auto; }
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
    .liUnidade { border-right: 1px solid #F0F0F0; height: 85px; margin-top: 25px; padding-right: 6px; width: 323px;}
    .liUnidade2 { margin-top: 25px; }
    .liClear { clear: both; }
    .liSituacao { margin-top: 10px; float: right !important; }
    .liDtCadastro, .liDtStatus, .liDtPrimeiroVencto, .liValorDocumento { margin-left: 5px; }
    .liUnidade li, .liUnidade2 li { margin-top: -7px; }    
    .liConta { clear: both; margin-top: 10px; }
    .liConta li { clear: both; margin-bottom: 5px; }        
    .liContrato { margin: 10px 0 0 12px; }    
    .liContrato li { margin-bottom: 5px; }
    .liDtInicioContrato { margin-left: 10px; }
    .liDtCancelamento { float: right !important; }
    .liStatus { margin-left: 15px; }    
    
    /*--> CSS DADOS */     
    .liContrato input[type="text"] { margin-bottom: 0; }
    .liContrato select { margin-bottom: 0; }
    .fldConta, .fldContrato { padding: 5px 5px 4px 9px; }
    .imgEnd { margin: 28px 0 0 5px; }    
    .ddlNomeFantasia { width: 205px; }
    .txtCNPJ { width: 100px; }
    .txtLogradouro { width: 218px; }
    .txtComplemento { width: 95px; }
    .txtBairro { width: 170px; }
    .txtCidade { width: 195px; }
    .txtUF { width: 20px; }
    .txtCEP { width: 54px; }
    .txtMoney { width: 67px; text-align: right;  }
    .txtQtd { width: 16px; }
    .txtValorDocumento { text-align: right; }
    .txtNumDoc, .ddlStatus { width: 70px; }
    
</style>
<script type="text/javascript">
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">
    <li class="liUnidade">
        <ul>       
            <li style="clear: both; margin-top: 10px !important;"></li>        
            <li class="liClear">
                <label for="ddlNomeFantasia" class="lblObrigatorio" title="Nome">Nome Fantasia</label>
                <asp:DropDownList ID="ddlNomeFantasia" runat="server" CssClass="ddlNomeFantasia" AutoPostBack="true"
                    onselectedindexchanged="ddlNomeFantasia_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlNomeFantasia" ErrorMessage="Selecione o Cliente" 
                    Display="None"></asp:RequiredFieldValidator>
            </li>        
            <li>
                <label for="txtCNPJ" title="CNPJ">CNPJ</label>
                <asp:TextBox ID="txtCNPJ" ToolTip="Informe o CNPJ" CssClass="txtCNPJ" runat="server" Enabled="false"></asp:TextBox>
            </li>
        </ul>
    </li>    
    <li>
        <img id="imgEnd" class="imgEnd" src="../../../../Library/IMG/Gestor_ImgEndereco.png" alt="endereço" />
    </li>    
    <li class="liUnidade2">
        <ul>
            <li>
                <label for="txtCEP" title="CEP">CEP</label>
                <asp:TextBox ID="txtCEP" ToolTip="Informe o CEP" CssClass="txtCEP" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li>
                <label for="txtLogradouro" title="Endereço">Endere&ccedil;o</label>
                <asp:TextBox ID="txtLogradouro" ToolTip="Informe o Endereço" CssClass="txtLogradouro" MaxLength="60" runat="server" 
                    Enabled="false"></asp:TextBox>
            </li>
            <li class="liClear">
                <label for="txtComplemento" title="Complemento">Complemento</label>
                <asp:TextBox ID="txtComplemento" ToolTip="Informe o Complemento" CssClass="txtComplemento" MaxLength="30" runat="server"
                    Enabled="false"></asp:TextBox>
            </li>
            <li>
                <label for="txtBairro" title="Bairro">Bairro</label>
                <asp:TextBox ID="txtBairro" CssClass="txtBairro" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li class="liClear">
                <label for="txtCidade" title="Cidade">Cidade</label>
                <asp:TextBox ID="txtCidade" CssClass="txtCidade" runat="server" Enabled="false"></asp:TextBox>
            </li>
            <li>
                <label for="txtUF" title="UF">UF</label>
                <asp:TextBox ID="txtUF" CssClass="txtUF" runat="server" Enabled="false"></asp:TextBox>
            </li>
        </ul>
    </li>
    <li class="liConta">
        <fieldset class="fldConta">
            <legend>Informações de Controle/Contábil</legend>
            <ul>
                <li class="liTipoReceita">
                    <label for="ddlTipoReceita" title="Grupo">Grupo</label>
                    <asp:DropDownList ID="ddlTipoReceita" runat="server" Enabled="false" Width="90px"></asp:DropDownList>
                </li>
                <li>
                    <label for="ddlSubGrupoReceita" title="Subgrupo" class="lblObrigatorio">SubGrupo</label>
                    <asp:DropDownList ID="ddlSubGrupoReceita" runat="server" Width="220px" ToolTip="Selecione o Subgrupo"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoReceita_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubGrupoReceita" 
                        ErrorMessage="Selecione o SubGrupo" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlContaContabil" class="lblObrigatorio" title="Conta Contábil">Conta Cont&aacute;bil</label>
                    <asp:DropDownList ID="ddlContaContabil" ToolTip="Selecione a Conta Contábil" runat="server" Width="220px"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlContaContabil" 
                        ErrorMessage="Selecione a Conta Contábil" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlHistLancamento" class="lblObrigatorio" title="Histórico Padrão de Lançamento">Hist&oacute;rico Padr&atilde;o de Lan&ccedil;amento</label>
                    <asp:DropDownList ID="ddlHistLancamento" ToolTip="Selecione o Histórico Padrão de Lançamento"
                        CssClass="ddlHistLancamento" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlHistLancamento" 
                        ErrorMessage="Selecione o Histórico Padrão de Lançamento" Display="None"></asp:RequiredFieldValidator>
                <li style="margin-top:22px;">
                    <label for="ddlDepartamento" title="Departamento">Departamento</label>
                    <asp:DropDownList ID="ddlDepartamento" runat="server" ToolTip="Selecione o Departamento" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged"
                        AutoPostBack="true" CssClass="campoDptoCurso"></asp:DropDownList>
                </li>
                <li>
                    <label for="ddlCentroCusto" title="Centro de Custo">Centro de Custo</label>
                    <asp:DropDownList ID="ddlCentroCusto" runat="server" ToolTip="Selecione o Centro de Custo" CssClass="campoDptoCurso"></asp:DropDownList>
                </li>
            </ul>
        </fieldset>
    </li>    
    <li class="liContrato">
        <fieldset class="fldContrato">
            <legend>Informações do Contrato</legend>
            <ul>
                <li>
                    <label for="txtNumContrato" class="lblObrigatorio" title="Número do Contrato">N° Contrato</label>
                    <asp:TextBox ID="txtNumContrato" ToolTip="Informe o Número do Contrato" CssClass="txtNumDoc" runat="server" Enabled="false"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNumContrato" 
                        ErrorMessage="N° Contrato deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>                
                <li>
                    <label for="txtNumAditivo" title="Número do Aditivo">N° Aditivo</label>
                    <asp:TextBox ID="txtNumAditivo" ToolTip="Informe o Número do Aditivo" CssClass="txtNumDoc" runat="server" Enabled="false">0</asp:TextBox>
                </li>                
                <li>
                    <label for="txtNumPublicacao" title="Número da Publicação">N° Publicação</label>
                    <asp:TextBox ID="txtNumPublicacao" ToolTip="Informe o Número da Publicação" MaxLength="20" CssClass="txtNumDoc" runat="server"></asp:TextBox>
                </li>                  
                <li>
                    <label for="txtDataPublicacao" title="Data de Publicação">Data</label>
                    <asp:TextBox ID="txtDataPublicacao" ToolTip="Informe a Data de Publicação" CssClass="campoData" runat="server"></asp:TextBox>
                </li>              
                <li class="liClear">
                    <label for="ddlTipoDocumento" class="lblObrigatorio" title="Tipo de Documento">Tipo Documento</label>
                    <asp:DropDownList ID="ddlTipoDocumento" ToolTip="Selecione o Tipo de Documento" CssClass="ddlTipoDocumento" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTipoDocumento" 
                        ErrorMessage="Tipo de Documento deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>                
                <li>
                    <label for="txtNumDoc" class="lblObrigatorio" title="Número do Documento">N° Documento</label>
                    <asp:TextBox ID="txtNumDoc" ToolTip="Informe o Número do Documento" CssClass="txtNumDoc" MaxLength="20" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNumDoc" 
                        ErrorMessage="N° Documento deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>                
                <li class="liDtInicioContrato">
                    <label for="txtDtInicioContrato" class="lblObrigatorio" title="Data de Início do Contrato">Início</label>
                    <asp:TextBox ID="txtDtInicioContrato" ToolTip="Informe a Data de Início do Contrato"
                        CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtInicioContrato" 
                        ErrorMessage="Data de Início do Contrato deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>                
                <li>
                    <label for="txtDtFimContrato" class="lblObrigatorio" title="Data de Término do Contrato">Término</label>
                    <asp:TextBox ID="txtDtFimContrato" ToolTip="Informe a Data de Término do Contrato"
                        CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:CustomValidator ControlToValidate="txtDtFimContrato" runat="server" ErrorMessage="Data de Término não pode ser menor que a Data de Início do Contrato" 
                        Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataTerminoContrato_ServerValidate"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDtFimContrato" 
                        ErrorMessage="Data de Término do Contrato deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>            
                <li class="liClear">
                    <label for="txtQtParcelas" class="lblObrigatorio" title="Quantidade de Parcelas de Recebimento">QP</label>
                    <asp:TextBox ID="txtQtParcelas" ToolTip="Informe a Quantidade de Parcelas de Recebimento" CssClass="txtQtd" runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtQtParcelas" 
                        ErrorMessage="Quantidade de Parcelas deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>                
                <li class="liDtPrimeiroVencto">
                    <label for="txtDtPrimeiroVencto" class="lblObrigatorio" title="Data do 1° vencimento">Data 1° Vecto</label>
                    <asp:TextBox ID="txtDtPrimeiroVencto" ToolTip="Informe a Data de Vencimento da primeira parcela"
                        CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:CustomValidator ControlToValidate="txtDtPrimeiroVencto" runat="server" ErrorMessage="Data do 1° Vencimento não pode ser menor que a Data de Início do Contrato" 
                        Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataPrimeiroVencto_ServerValidate"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDtPrimeiroVencto" 
                        ErrorMessage="Data 1° Vencimento da Parcela deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>                
                <li>
                    <label for="txtDiasIntervalo" class="lblObrigatorio" title="Dias de intervalo entre parcelas">DI</label>
                    <asp:TextBox ID="txtDiasIntervalo" ToolTip="Informe a quantidade de Dias de intervalo entre parcelas" CssClass="txtQtd" runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDiasIntervalo" 
                        ErrorMessage="Dias de Intervalo deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>                
                <li class="liValorDocumento">
                    <label for="txtValorDocumento" class="lblObrigatorio" title="Valor do Contrato">R$ Contrato</label>
                    <asp:TextBox ID="txtValorDocumento" CssClass="txtMoney" runat="server" MaxLength="9"></asp:TextBox>
                    <asp:RegularExpressionValidator ControlToValidate="txtValorDocumento" runat="server" 
                        ErrorMessage="Valor do Contrato deve ter formato decimal separado por vírgula" 
                        ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtValorDocumento" 
                        ErrorMessage="Valor do Contrato deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>                
                <li class="liDtCancelamento">
                    <label for="txtDtCancelamento" title="Data de Cancelamento do Contrato">Cancelamento</label>
                    <asp:TextBox ID="txtDtCancelamento" ToolTip="Informe a Data de Cancelamento do Contrato" CssClass="campoData" runat="server">
                    </asp:TextBox>
                    <asp:CustomValidator ControlToValidate="txtDtCancelamento" runat="server" ErrorMessage="Data de Cancelamento não pode ser menor que a Data de Cadastro" 
                        Display="None" CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataCancelamento_ServerValidate"></asp:CustomValidator>
                </li>                
                <li style="clear:both;"></li>                
                <li style="clear:both; border-right: 1px solid #F0F0F0; padding-right: 5px;">
                    <label for="txtMultaPercent" title="Valor da Multa em % sobre a parcela">Multa</label>
                    <asp:TextBox ID="txtMulta" runat="server" CssClass="txtMoney" MaxLength="9"></asp:TextBox>
                    <asp:CheckBox ID="cbFlagPercentualMulta" runat="server" /><span style="margin-left:-5px !important;">%</span>
                    <asp:RegularExpressionValidator ControlToValidate="txtMulta" runat="server" ErrorMessage="Valor da Multa deve ter formato decimal separado por vírgula" 
                        ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None"></asp:RegularExpressionValidator>
                </li>                            
                <li style="border-right: 1px solid #F0F0F0; padding: 0 5px 0 5px;">
                    <label for="txtJurosPercent" title="Valor dos Juros/Mês sobre a parcela">Juros Mês</label>
                    <asp:TextBox ID="txtJuros" runat="server" CssClass="txtMoney"></asp:TextBox>
                    <asp:CheckBox ID="cbFlagPercentualJuros" runat="server" /><span style="margin-left:-5px !important;">%</span>
                    <asp:RegularExpressionValidator ControlToValidate="txtJuros" runat="server" 
                        ErrorMessage="Valor dos Juros/Mês deve ter formato decimal separado por vírgula" 
                        ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None"></asp:RegularExpressionValidator>
                </li>                
                <li style="padding: 0 0 0 5px;">
                    <label for="txtDescontoPercent" title="Valor do Desconto/Mês sobre a parcela">Desconto Mês</label>
                    <asp:TextBox ID="txtDesconto" runat="server" CssClass="txtMoney"></asp:TextBox>
                    <asp:CheckBox ID="cbFlagPercentualDesconto" runat="server" /><span style="margin-left:-5px !important;">%</span>
                    <asp:RegularExpressionValidator ControlToValidate="txtDesconto" runat="server" 
                        ErrorMessage="Valor do Desconto/Mês deve ter formato decimal separado por vírgula" 
                        ValidationExpression="^((\d+|\d{1,3}(\.\d{3})+)(\,\d*)?|\,\d+)$" Display="None"></asp:RegularExpressionValidator>
                </li>
            </ul>
        </fieldset>
    </li>
    <li class="liSituacao">
    <ul>
        <li class="liDtCadastro">
            <label for="txtDtCadastro" title="Data Cadastro">Data Cadastro</label>
            <asp:TextBox ID="txtDtCadastro" ToolTip="Informe a Data de Cadastro" CssClass="campoData" Enabled="false" runat="server"></asp:TextBox>
        </li>    
        <li class="liStatus">
            <label for="ddlStatus" class="lblObrigatorio" title="Status">Status</label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione o Status" CssClass="ddlStatus">
                <asp:ListItem Value="A">Em Aberto</asp:ListItem>
                <asp:ListItem Value="C">Cancelado</asp:ListItem>
                <asp:ListItem Value="Q">Quitado</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlStatus" ErrorMessage="Status deve ser informado" Display="None"></asp:RequiredFieldValidator>
            <asp:CustomValidator ControlToValidate="ddlStatus" runat="server" ErrorMessage="Data de Status não pode ser menor que a Data de Cadastro" Display="None"
                CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataStatus_ServerValidate"></asp:CustomValidator>
        </li>        
        <li class="liDtStatus">
            <label for="txtDtStatus" title="Data Status" class="lblObrigatorio">Data Status</label>
            <asp:TextBox ID="txtDtStatus" Enabled="False" ToolTip="Informe a Data Status" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtStatus" ErrorMessage="Data de Status deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
    </ul>
    </li>
</ul>
<script type="text/javascript">
    $(document).ready(function() {
        $(".txtCEP").mask("99999-999");
        $('.txtQtd').mask("?99");
        $(".txtMoney").maskMoney({ symbol: "", decimal: ",", thousands: "." });
    });
</script>
</asp:Content>