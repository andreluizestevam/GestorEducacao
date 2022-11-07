<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5800_CtrlDiversosFinanceiro.F5810_CtrlCheques.F5811_CadastramentoChequeEmitidoInstit.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">      
    .ulDados { width: 310px;}
    .ulDados input{ margin-bottom: 0;}      
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both;}
    .liAgencia, .liConta, .liNome, .liSituacao { clear:none !important; display:inline !important; }       
    .liNumDocto, .liDtVencimento, .liNomeCheque, .liValor { clear: none !important; margin-left: 5px; }
    
    /*--> CSS DADOS */
    .txtObservacoes
    {
        width: 300px;
        height: 37px;
        margin-top: 2px;
        border-width: 0px !important;
    }    
    .ddlNomeFantasia { width: 220px; }
    .txtCPF { width: 80px; }
    .txtMoney { text-align: right; width: 67px; }
    .ddlBanco { width: 45px; }
    .ddlAgencia { width: 70px; }
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">        
        <li>
            <label id="Label1" title="Banco">
                Banco</label>
            <asp:DropDownList ID="ddlBanco" ToolTip="Selecione um Banco" 
                CssClass="ddlBanco" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlBanco_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlBanco" runat="server" ControlToValidate="ddlBanco"
                CssClass="validatorField" ErrorMessage="Banco deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>        
        <li class="liAgencia">
            <label id="Label2" title="Agência" runat="server">
                Agência</label>
            <asp:DropDownList ID="ddlAgencia" ToolTip="Selecione uma Agência" 
                CssClass="ddlAgencia" runat="server">
            </asp:DropDownList>                
            <asp:RequiredFieldValidator ID="rfvddlAgencia" runat="server" ControlToValidate="ddlAgencia"
                CssClass="validatorField" ErrorMessage="Agência deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>                
        <li class="liConta">
             <label id="lblConta" runat="server" title="Conta">Conta</label>
            <asp:TextBox ID="txtConta" 
                ToolTip="Informe a Conta"
                CssClass="txtConta" MaxLength="50" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvConta" runat="server" 
                 ControlToValidate="txtConta" ErrorMessage="Conta deve ser informada">*</asp:RequiredFieldValidator>
        </li>
        <li>
             <label id="lblNumCheque" runat="server" title="Informe o Nº do Cheque">Nº do Cheque</label>
            <asp:TextBox ID="txtNumCheque" 
                ToolTip="Informe o Número do Cheque"
                CssClass="txtNumCheque" MaxLength="50" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvNCheque" runat="server" 
                 ErrorMessage="Informe o numero do cheque" ControlToValidate="txtNumCheque">*</asp:RequiredFieldValidator>
        </li>           
        <li class="liNumDocto">
            <label id="lblNumDocto" runat="server" title="Nº Documento">Nº Documento</label>
            <asp:TextBox ID="txtNumDocto" 
                ToolTip="Informe o Número do Documento"
                CssClass="txtNumDocto" runat="server"></asp:TextBox>
        </li>                   
        <li>
             <label id="lblValor" runat="server" title="Informe o Valor do Cheque">Valor R$</label>
            <asp:TextBox ID="txtValor" 
                ToolTip="Informe o Valor do Cheque"
                CssClass="txtMoney" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                 ControlToValidate="txtValor" ErrorMessage="Informe o valor do cheque">*</asp:RequiredFieldValidator>
        </li>
        <li class="liNumDocto">
            <label id="lblCPF" runat="server" title="CPF">CPF</label>
            <asp:TextBox ID="txtCPF" 
                ToolTip="Informe o CPF"
                CssClass="txtCPF" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCpf" runat="server" 
                ControlToValidate="txtCPF" ErrorMessage="Informe o CPF">*</asp:RequiredFieldValidator>
        </li>
        <li>
            <Label class="checkboxLabel" for="cbDepositar" runat="server" title="Informar se não deverá ser depositado">Não depositar</Label>
        <asp:CheckBox ID="cbDepositar" runat="server" AutoPostBack="True" />
        </li>        
        
        <li >
             <label id="lblNomeCheque" runat="server" title="Nome do Cheque">Nome</label><asp:TextBox ID="txtNomeCheque" 
                ToolTip="Informe o Nome do Cheque"
                CssClass="txtNomePessoa" MaxLength="50" runat="server"></asp:TextBox>
             
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" 
                 ErrorMessage="Informe o nome" ControlToValidate="txtNomeCheque">*</asp:RequiredFieldValidator>
        </li>               
        <li>
            <label id="lblDtEmissao" runat="server" title="Informe a Data de Emissão">Data Emissão</label>
            <asp:TextBox ID="txtDtEmissao" CssClass="campoData" runat="server" 
                ToolTip="Informe a Data de Emissão"></asp:TextBox>
            <asp:RequiredFieldValidator ID="Emit" runat="server" 
                ControlToValidate="txtDtEmissao" ErrorMessage="Informe a data de emitir">*</asp:RequiredFieldValidator>
        </li>         
        <li class="liDtVencimento">
            <label id="lblDtVencimento" runat="server" title="Informe a Data de Vencimento">Data de Vencimento</label>
            <asp:TextBox ID="txtDtVencimento" CssClass="campoData" runat="server" 
                ToolTip="Informe a Data de Vencimento"></asp:TextBox>
            <asp:CompareValidator ID="cvVenc" runat="server" 
                ControlToCompare="txtDtEmissao" ControlToValidate="txtDtVencimento" 
                ErrorMessage="A data de vencimento deve ser maior que a de emitir" 
                Operator="GreaterThanEqual" Type="Date">*</asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvVenc" runat="server" 
                ControlToValidate="txtDtVencimento" ErrorMessage="Informe a data de vencimento">*</asp:RequiredFieldValidator>
        </li>                 
        <li>
            <label id="Label3" runat="server">Tipo Cliente</label>
            <asp:DropDownList ID="ddlTpCliente" ToolTip="Selecione o Tipo de Cliente" 
                runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlTpCliente_SelectedIndexChanged" >
            <asp:ListItem Value="R" Selected="True">Responsável</asp:ListItem>
            <asp:ListItem Value="C">Cliente</asp:ListItem>
            </asp:DropDownList>                
        </li>               
        <li class="liNome">
            <label for="ddlNomeFantasia" class="lblObrigatorio" title="Nome">Nome Cliente</label>
            <asp:DropDownList ID="ddlNomeFantasia" runat="server"
                 CssClass="ddlNomeFantasia">
            </asp:DropDownList>            
            <asp:RequiredFieldValidator ID="rfvddlNomeFantasia" runat="server" ControlToValidate="ddlNomeFantasia"
                CssClass="validatorField" ErrorMessage="Cliente deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>           
        <li>            
            <fieldset>
                <legend>Observação</legend>
                <asp:TextBox ID="txtObservacoes" CssClass="txtObservacoes" ToolTip="Informe as Observações"
                    runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 100);"></asp:TextBox>
            </fieldset>
        </li>                
        <li>
            <label for="txtDataSituacao" class="lblObrigatorio" title="Data da Situação">Data Situação</label>
            <asp:TextBox ID="txtDataSituacao" ToolTip="Informe a Data da Situação" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtDataSituacao" ID="RFV1" runat="server" 
                ErrorMessage="Data da Situação deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>        
        <li class="liSituacao">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação">Situação</label>
            <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" ToolTip="Selecione a Situação">
                <asp:ListItem Value="A">Em Aberto</asp:ListItem>
                <asp:ListItem Value="Q">Baixado</asp:ListItem>
                <asp:ListItem Value="C">Cancelado</asp:ListItem>                
            </asp:DropDownList>
            <asp:RequiredFieldValidator ControlToValidate="ddlSituacao" ID="RFV2" runat="server" 
                ErrorMessage="Situação deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>            
</ul>
<script type="text/javascript">
    $(document).ready(function() {
        $(".txtCPF").mask("999.999.999-99");        
        $(".txtMoney").maskMoney({ symbol: "", decimal: ",", thousands: "." });       
    });
</script>
</asp:Content>