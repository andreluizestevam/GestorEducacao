<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5908_ContaCorrente.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 256px; }
        
        /*--> CSS LIs */
        .liFlagEmiteBoleto {margin-left:8px;}
        .liClear { clear: both; }
        .liClear2 { clear: both; height:10px; }
        
        /*--> CSS DADOS */
        .ddlBanco, .txtNomeGerenteConta {width:160px;}
        .txtNumeroConta {text-align: right; width: 60px;}
        .txtDigitoConta {text-align: right; width: 20px;text-transform:uppercase;}
        .money  {text-align: right; width: 70px;}
        .txtEmailGerenteConta {width: 140px;}
        .telefone {width: 76px;}
        .ddlFlagEmiteBoleto {width: 44px;}        
        .ddlAgencia {width:155px;}
        .ddlTipoConta {width:62px;}
        .ddlSituacao {width:70px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlBanco" class="lblObrigatorio" title="Banco">Banco</label>
            <asp:DropDownList ID="ddlBanco" runat="server" CssClass="ddlBanco"
                ToolTip="Selecione o Banco" 
                onselectedindexchanged="ddlBanco_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBanco" CssClass="validatorField"
                ErrorMessage="Banco é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear2"></li>
        <li class="liClear">
            <label for="ddlAgencia" class="lblObrigatorio" title="Agência">Agência</label>
            <asp:DropDownList ID="ddlAgencia" runat="server" CssClass="ddlAgencia"
                ToolTip="Selecione a Agência">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlAgencia" CssClass="validatorField"
                ErrorMessage="Agência é requerida">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtNumeroConta" class="lblObrigatorio" title="Número da Conta Corrente">Número CC/Dígito</label>
            <asp:TextBox ID="txtNumeroConta" runat="server" CssClass="txtNumeroConta"
                ToolTip="Informe o Número da Conta Corrente"
                MaxLength="10">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNumeroConta" CssClass="validatorField"
                ErrorMessage="Campo Número CC é requerido">
            </asp:RequiredFieldValidator>
            <asp:TextBox ID="txtDigitoConta" runat="server" CssClass="txtDigitoConta"
                ToolTip="Informe o Dígito da Conta Corrente"
                MaxLength="1">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDigitoConta" CssClass="validatorField"
                ErrorMessage="Campo Dígito da Conta Corrente é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlTipoConta" class="lblObrigatorio" title="Tipo de Conta">Tipo Conta</label>
            <asp:DropDownList ID="ddlTipoConta" runat="server" CssClass="ddlTipoConta"
                ToolTip="Selecione o Tipo de Conta">
                <asp:ListItem Value="F" Selected="True">Física</asp:ListItem>
                <asp:ListItem Value="J">Jurídica</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoConta" CssClass="validatorField"
                ErrorMessage="Tipo de Conta é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear2"></li>
        <li class="liClear">
            <label for="txtLimiteCredito" title="Valor Limite de Crédito">Lim. Crédito</label>
            <asp:TextBox ID="txtLimiteCredito" runat="server" CssClass="money"
                ToolTip="Informe o Valor Limite de Crédito">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtLimiteChequeEspecial" title="Valor Limite do Cheque Especial">Lim. Cheque Esp.</label>
            <asp:TextBox ID="txtLimiteChequeEspecial" runat="server" CssClass="money"
                ToolTip="Informe o Valor Limite do Cheque Especial">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtDataVencimentoChequeEspecial" title="Data de Vencimento do Cheque Especial">Vencto Cheque Esp.</label>
            <asp:TextBox ID="txtDataVencimentoChequeEspecial" runat="server" CssClass="campoData"
                ToolTip="Informe a Data de Vencimento do Cheque Especial">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtNomeGerenteConta" title="Nome do Gerente da Conta">Nome Gerente</label>
            <asp:TextBox ID="txtNomeGerenteConta" runat="server" CssClass="txtNomeGerenteConta"
                ToolTip="Informe o Nome do Gerente da Conta"
                MaxLength="60">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtEmailGerenteConta" title="E-mail do Gerente da Conta">E-mail Gerente</label>
            <asp:TextBox ID="txtEmailGerenteConta" runat="server" CssClass="txtEmailGerenteConta"
                ToolTip="Informe o E-mail do Gerente da Conta"
                MaxLength="60">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtTelefoneGerenteConta" title="Telefone do Gerente da Conta">Telefone Gerente</label>
            <asp:TextBox ID="txtTelefoneGerenteConta" runat="server" CssClass="telefone"
                ToolTip="Informe o Telefone do Gerente da Conta">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtDataAberturaConta" class="lblObrigatorio" title="Data de Abertura da Conta">Data Abertura</label>
            <asp:TextBox ID="txtDataAberturaConta" runat="server" CssClass="campoData"
                ToolTip="Informe a Data de Abertura da Conta">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataAberturaConta" CssClass="validatorField"
                ErrorMessage="Data Abertura é requerida">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liFlagEmiteBoleto">
            <label for="ddlFlagEmiteBoleto" class="lblObrigatorio" title="Conta permite emissão de boleto bancário?">Emite Boleto?</label>
            <asp:DropDownList ID="ddlFlagEmiteBoleto" runat="server" CssClass="ddlFlagEmiteBoleto"
                ToolTip="Informe Conta permite emissão de boleto bancário">
                <asp:ListItem Value="S" Selected="True">Sim</asp:ListItem>
                <asp:ListItem Value="N">Não</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação da Conta">Situação</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="ddlSituacao"
                ToolTip="Selecione a Situação da Conta">
                <asp:ListItem Value="A" Selected="True">Ativa</asp:ListItem>
                <asp:ListItem Value="I">Inativa</asp:ListItem>
                <asp:ListItem Value="B">Bloqueada</asp:ListItem>
                <asp:ListItem Value="E">Encerrada</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSituacao" CssClass="validatorField"
                ErrorMessage="Situação é requerida">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDataSituacao" class="lblObrigatorio" title="Data da Situação da Conta">Data Situação</label>
            <asp:TextBox ID="txtDataSituacao" runat="server" CssClass="campoData"
                ToolTip="Informe a Data da Situação da Conta">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataSituacao" CssClass="validatorField"
                ErrorMessage="Data da Situação é requerida">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtNumeroConta").mask("?999999999");
            $(".telefone").mask("(99) 9999-9999");
            $(".money").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>
