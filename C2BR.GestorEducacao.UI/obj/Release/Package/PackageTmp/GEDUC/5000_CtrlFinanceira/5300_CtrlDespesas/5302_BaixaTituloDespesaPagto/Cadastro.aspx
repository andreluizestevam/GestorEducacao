<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5302_BaixaTituloDespesaPagto.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados { width: 463px;}
    .ulDados input{ margin-bottom: 0;}   

    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 8px;}
    .liClear { clear: both; }
    .liNumeroDocumento{ clear: both; margin-right: 15px; }
    .liNumeroParcela{ margin-right: 5px; }
    .liQuantidadeParcelas{ margin-right: 20px;}
    .liContrato{ margin-right: 5px; }
    .liValores { margin-top: 10px; }    
    .liValorTotal { margin-right: 10px; clear: both; }
    .liValorMultaRecebido, .liValorJurosRecebido, .liValorDescontoRecebido { margin-right: 36px; }
    .liAux { margin-right: 2px; margin-left: -2px;}
     
    /*--> CSS DADOS */   
    .fldValores { padding-left: 10px; }    
    .labelTitle { font-weight: bold; margin-top: 7px; margin-bottom: -2px!important;}
    .labelAux { font-weight: bold; font-size: 1.5em; margin-top: 10px;}
    .txtNumeroDocumento{ width: 80px; }
    .txtNumeroParcela, .txtQuantidadeParcelas { width: 24px; }  
    .txtValorTotal, .txtValorParcela, .txtValorMulta, .txtValorMultaRecebido, .txtValorJuros, .txtValorJurosRecebido { width: 70px; }
    .txtValorDesconto, .txtValorDescontoRecebido, .txtValorDescontoBolsaRecebido, .txtDescontoBolsa, .txtValorRecebido { width: 70px; }       
    .btnAtualizarValores { background-color: #F1FFEF; border: 1px solid #D2DFD1; padding: 0 4px 1px 5px; }
    .txtRazaoSocial { width: 200px; }
    .txtCodigoFornecedor { width: 100px; }
    
</style>

<script type="text/javascript">

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">        
        
        <li class="liNumeroDocumento">
            <label for="txtNumeroDocumento" class="lblObrigatorio" title="Número do Documento">N° Documento</label>
            <asp:TextBox ID="txtNumeroDocumento" CssClass="txtNumeroDocumento" Enabled="false" MaxLength="20" runat="server" ToolTip="Informe o Número do Documento"></asp:TextBox>
        </li>
        
        <li>
            <label for="txtTipoDocumento" title="Tipo de Documento">Tipo de Documento</label>
            <asp:TextBox ID="txtTipoDocumento" runat="server" Enabled="false"></asp:TextBox>
        </li>
    
        <li>
            <label for="txtDataCadastro" title="Data de Cadastro">Cadastro</label>
            <asp:TextBox ID="txtDataCadastro" CssClass="campoData" runat="server" Enabled="false" ToolTip="Informe a Data de Cadastro"></asp:TextBox>
        </li>
        
        <li>
            <label for="txtDataVencimento" class="lblObrigatorio" title="Data de Vencimento">Vencimento</label>
            <asp:TextBox ID="txtDataVencimento" CssClass="campoData" runat="server" ToolTip="Informe a Data de Vencimento" Enabled="false"></asp:TextBox>
        </li>

        <li class="liNumeroParcela">
            <label for="txtNumeroParcela" class="lblObrigatorio" title="Número da Parcela">NP</label>
            <asp:TextBox ID="txtNumeroParcela" CssClass="txtNumeroParcela campoNumerico" Enabled="false" runat="server" ToolTip="Informe o Número da Parcela"></asp:TextBox>
        </li>
        <li class="liAux">
            <label class="labelAux">/</label>
        </li>
        <li class="liQuantidadeParcelas">
            <label for="txtQuantidadeParcelas" class="lblObrigatorio" title="Quantidade de Parcelas">QP</label>
            <asp:TextBox ID="txtQuantidadeParcelas" CssClass="txtQuantidadeParcelas campoNumerico" runat="server" ToolTip="Informe a Quantidade de Parcelas" Enabled="false"></asp:TextBox>
        </li>       
        
        <li class="liClear">
            <label for="txtCodigoFornecedor" title="Código do Fornecedor">CNPJ/CPF</label>
            <asp:TextBox ID="txtCodigoFornecedor" CssClass="txtCodigoFornecedor" runat="server" Enabled="false"></asp:TextBox>
        </li>
        <li>
            <label for="txtRazaoSocial" title="Nome da Razão Social do Fornecedor">Razão Social</label>
            <asp:TextBox ID="txtRazaoSocial" CssClass="txtRazaoSocial" runat="server" Enabled="false"></asp:TextBox>
        </li>        

        <li class="liClear liContrato">
            <label for="txtContrato" title="Número do Contrato">N° Contrato</label>
            <asp:TextBox ID="txtContrato" runat="server" CssClass="txtContrato" ToolTip="Número do Contrato" Enabled="false"></asp:TextBox>
        </li>
        
        <li>
            <label for="txtNumeroAditivo" title="Número do Aditivo">N° Aditivo</label>
            <asp:TextBox ID="txtNumeroAditivo" CssClass="txtNumeroAditivo" Enabled="false" MaxLength="20" runat="server" ToolTip="Número do Aditivo"></asp:TextBox>
        </li>
        
        <li>
            <label for="txtNumeroPublicacao" title="Número da Publicação">N° Publicação</label>
            <asp:TextBox ID="txtNumeroPublicacao" CssClass="txtNumeroPublicacao" Enabled="false" MaxLength="20" runat="server" ToolTip="Número da Publicação"></asp:TextBox>
        </li>
        
        <li class="liValores" style="clear: both;">
            <fieldset class="fldValores">
                <legend>Valores</legend>
                <ul>
                    <li style="border-right: 1px solid #F0F0F0;">
                        <ul>
                            <li>
                                <label class="labelTitle">
                                    Informações do Título</label>
                            </li>
                            <li class="liClear">
                                <label for="txtValorParcela" class="lblObrigatorio" title="Valor da Parcela">
                                    Parcela</label>
                                <asp:TextBox ID="txtValorParcela" CssClass="txtValorParcela campoMoeda" runat="server"
                                    ToolTip="Informe o Valor Total" Enabled="false"></asp:TextBox>
                            </li>
                            <li class="liClear">
                                <label for="txtValorMulta" title="Valor da Multa">
                                    Multa</label>
                                <asp:TextBox ID="txtValorMulta" CssClass="txtValorMulta campoMoeda" runat="server"
                                    ToolTip="Informe o Valor da Multa" Enabled="false"></asp:TextBox>
                            </li>
                            <li class="liClear">
                                <label for="txtValorJuros" title="Valor do Juros">
                                    Juros</label>
                                <asp:TextBox ID="txtValorJuros" CssClass="txtValorJuros campoMoeda" runat="server"
                                    ToolTip="Informe o Valor do Juros" Enabled="false"></asp:TextBox>
                            </li>
                            <li class="liClear">
                                <label for="txtValorDesconto" title="Valor Desconto">
                                    Desconto</label>
                                <asp:TextBox ID="txtValorDesconto" CssClass="txtValorDesconto campoMoeda" runat="server"
                                    ToolTip="Informe o Valor do Desconto" Enabled="false"></asp:TextBox>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <ul>
                            <li>
                                <label class="labelTitle">
                                    Informações de Pagamento</label>
                            </li>
                            <li class="liClear">
                                <label for="txtValor" class="lblObrigatorio" title="Valor">
                                    Valor</label>
                                <asp:TextBox ID="txtValor" CssClass="txtValorTotal campoMoeda" runat="server"
                                    ToolTip="Informe o Valor"></asp:TextBox>
                            </li>
                            <li class="liValorMultaRecebido" style="clear: both; margin-top: 5px;">
                                <label for="txtValorMultaRecebido" title="Valor da Multa Recebido">
                                    Multa</label>
                                <asp:TextBox ID="txtValorMultaRecebido" CssClass="txtValorMultaRecebido campoMoeda"
                                    runat="server"></asp:TextBox>
                            </li>
                            <li class="liValorJurosRecebido" style="clear: both;">
                                <label for="txtValorJurosRecebido" title="Valor de Juros Recebido">
                                    Juros</label>
                                <asp:TextBox ID="txtValorJurosRecebido" CssClass="txtValorJurosRecebido campoMoeda"
                                    runat="server"></asp:TextBox>
                            </li>
                            <li class="liValorDescontoRecebido" style="clear: both;">
                                <label for="txtValorDescontoRecebido" title="Valor de Desconto Recebido">
                                    Desconto</label>
                                <asp:TextBox ID="txtValorDescontoRecebido" CssClass="txtValorDescontoRecebido campoMoeda"
                                    runat="server"></asp:TextBox>
                            </li>
                            <li style="clear: both; margin-top: 20px;">
                                <label for="txtValorRecebido" class="lblObrigatorio" title="Valor Recebido">
                                    Total Recebido</label>
                                <asp:TextBox ID="txtValorRecebido" CssClass="txtValorRecebido campoMoeda" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtValorRecebido" ID="RequiredFieldValidator8"
                                    runat="server" ErrorMessage="Valor Recebido deve ser informado" Display="None"></asp:RequiredFieldValidator>
                            </li>
                        </ul>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li style="margin-top: 10px;">
            <label for="txtDataRecebimento" title="Data de Recebimento">
                Data</label>
            <asp:TextBox ID="txtDataRecebimento" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:LinkButton ID="btnAtualizarValores" CssClass="btnAtualizarValores" Style="margin-left: 10px;"
                runat="server" OnClick="btnAtualizarValores_Click">Recalcular</asp:LinkButton>
            <label for="chkQuitado" title="Data de Recebimento" style="clear: both; position: relative;
                top: 13px; left: 22px;">
                Quitado</label>
            <asp:CheckBox ID="chkQuitado" runat="server"></asp:CheckBox>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtNumeroParcela").mask('?999');
            $(".txtQuantidadeParcelas").mask('?999');
        });
    </script>
</asp:Content>