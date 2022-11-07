<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5202_BaixaTituloReceita.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados { width: 522px;}
    .ulDados input{ margin-bottom: 0;}   
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 8px;}
    .liValores { margin-top: 10px; }    
    .liValorTotal { margin-right: 10px; }
    .liAux { margin-right: 2px; margin-left: -2px;}
    .liValorExcedente { margin-top: 10px; margin-left: 120px; }
    .liClear { clear: both; margin-top: -5px; }
    
    /*--> CSS DADOS */
    .labelInLine { clear: both; padding-top: 2px; width: 68px; }
    .fldValores { padding-left: 10px; margin-top: -10px; }    
    .labelTitle { font-weight: bold; margin-top: 7px; margin-bottom: -2px !important;}
    .labelAux { font-weight: bold; font-size: 1.5em; margin-top: 10px;}
    .txtValor { width: 70px; }
    .btnAtualizarValores { background-color: #F1FFEF; border: 1px solid #D2DFD1; padding: 0 4px 1px 5px; }
    .txtDiasDocto { width: 30px; text-align: right; }
    
</style>
<script type="text/javascript">
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
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
        <li style="margin-left: 5px;">
            <label for="txtNumeroParcela" class="lblObrigatorio" title="Número da Parcela">NP</label>
            <asp:TextBox ID="txtNumeroParcela" style="width:24px;" CssClass="txtNumeroParcela campoNumerico" Enabled="false" runat="server" ToolTip="Informe o Número da Parcela"></asp:TextBox>
        </li>
        <li class="liAux">
            <label class="labelAux">/</label>
        </li>
        <li>
            <label for="txtQuantidadeParcelas" class="lblObrigatorio" title="Quantidade de Parcelas">QP</label>
            <asp:TextBox ID="txtQuantidadeParcelas" style="width:24px;" CssClass="txtQuantidadeParcelas campoNumerico" runat="server" ToolTip="Informe a Quantidade de Parcelas" Enabled="false"></asp:TextBox>
        </li>
        
        <li style="clear:both;">
            <label for="txtNumeroDocumento" class="lblObrigatorio" title="Número do Documento">N° Documento</label>
            <asp:TextBox ID="txtNumeroDocumento" CssClass="txtNumeroDocumento" Enabled="false" MaxLength="20" runat="server" ToolTip="Informe o Número do Documento"></asp:TextBox>
        </li>
        
        <li>
            <label for="txtTipoDocumento" title="Tipo de Documento">Tipo de Documento</label>
            <asp:TextBox ID="txtTipoDocumento" runat="server" Enabled="false" ToolTip="Tipo de Documento"></asp:TextBox>
        </li>
    
        <li>
            <label for="txtDataCadastro" title="Data de Cadastro">Cadastro</label>
            <asp:TextBox ID="txtDataCadastro" CssClass="campoData" runat="server" Enabled="false" ToolTip="Informe a Data de Cadastro"></asp:TextBox>
        </li>
        
        <li>
            <label for="txtDataVencimento" class="lblObrigatorio" title="Data de Vencimento">Vencimento</label>
            <asp:TextBox ID="txtDataVencimento" CssClass="campoData" runat="server" ToolTip="Informe a Data de Vencimento" Enabled="false"></asp:TextBox>
        </li>

        <li style="margin-left: -3px;">
            <label for="txtDiasDocto" title="Dias">Dias</label>
            <asp:TextBox ID="txtDiasDocto" CssClass="txtDiasDocto" runat="server" ToolTip="Dias do documento" Enabled="false"></asp:TextBox>
        </li>

        <li class="liClear">
            <label runat="server" id="lblDescNome" for="txtNome">Nome</label>
            <asp:TextBox ID="txtNome" CssClass="campoNomePessoa" Enabled="false" runat="server"></asp:TextBox>
        </li>
        <li>
            <label for="txtCodigo">CPF/CNPJ</label>
            <asp:TextBox ID="txtCodigo" CssClass="txtCodigo" Enabled="false" runat="server"></asp:TextBox>
        </li>
        
        <li class="liValores" style="clear:both; width: 330px;">
            <fieldset class="fldValores">
                <legend>Valores</legend>
                <ul>
                    <li style="border-right: 1px solid #F0F0F0;">
                        <ul style="padding-left: 40px;">
                        <li>
                            <label class="labelTitle">Informações do Título</label>
                        </li>
                        
                        <li class="labelInLine"><span title="Valor da Parcela">Parcela</span></li>
                        <li id="liValorParcela" class="liClear">
                            <asp:TextBox ID="txtValorParcela" CssClass="txtValor campoMoeda" runat="server" ToolTip="Informe o Valor Total" Enabled="false"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine"><span title="Valor Total Pago">Total Já Pago</span></li>
                        <li class="liClear">
                            <asp:TextBox ID="txtValorTotalPago" CssClass="txtValor campoMoeda" runat="server" ToolTip="Valor Total Recebido" Enabled="false"></asp:TextBox>
                        </li>

                        <li class="labelInLine"><span title="Valor da Multa">Multa</span></li>
                        <li id="liValorMulta" class="liClear">
                            <asp:TextBox ID="txtValorMulta" CssClass="txtValor campoMoeda" runat="server" ToolTip="Informe o Valor da Multa" Enabled="false"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine"><span title="Valor do Juros">Juros</span></li>
                        <li id="liValorJuros" class="liClear">
                            <asp:TextBox ID="txtValorJuros" CssClass="txtValor campoMoeda" runat="server" ToolTip="Informe o Valor do Juros" Enabled="false"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine"><span title="Valor do Desconto">Desconto</span></li>
                        <li id="liValorDesconto" class="liClear">
                            <asp:TextBox ID="txtValorDesconto" CssClass="txtValor campoMoeda" runat="server" ToolTip="Informe o Valor do Desconto" Enabled="false"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine"><span title="Desconto de Bolsa">Desconto Bolsa</span></li>
                        <li id="liDescontoBolsa" class="liClear">
                            <asp:TextBox ID="txtDescontoBolsa" CssClass="txtValor campoMoeda" runat="server" ToolTip="Informe o Desconto de Bolsa" Enabled="false"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine"><span title="Outros">Outros (+)</span></li>
                        <li class="liClear">
                            <asp:TextBox ID="txtOutrosValores" CssClass="txtValor campoMoeda" runat="server" ToolTip="Outros Valores" Enabled="false"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine"><span title="Valor Total">Total</span></li>
                        <li class="liClear liValorTotal">
                            <asp:TextBox ID="txtValorTotal" CssClass="txtValor campoMoeda" runat="server" ToolTip="Informe o Valor Total" Enabled="false"></asp:TextBox>
                        </li>
                        
                        
                        </ul>
                    </li>
                    <li style="margin-right: 0px;">
                        <ul>
                        <li>
                            <label class="labelTitle">Informações de Pagamento</label>
                        </li>
                        
                        <li class="labelInLine"><span title="Valor Excedente">Residual</span></li>
                        <li class="liClear">
                            <asp:TextBox ID="txtValorResidual" CssClass="txtValor campoMoeda" runat="server" Enabled="false" ToolTip="Valor Residual"></asp:TextBox>
                        </li>                        
                        
                        <li class="labelInLine"><span title="Valor da Multa Recebido">Multa</span></li>
                        <li class="liClear">
                            <asp:TextBox ID="txtValorMultaRecebido" CssClass="txtValor campoMoeda" 
                                runat="server" ToolTip="Valor da Multa Recebido" 
                                ontextchanged="txtValorMultaRecebido_TextChanged" AutoPostBack="True"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine"><span title="Valor de Juros Recebido">Juros</span></li>
                        <li class="liClear">
                            <asp:TextBox ID="txtValorJurosRecebido" CssClass="txtValor campoMoeda" 
                                runat="server" ToolTip="Valor do Juros Recebido" 
                                ontextchanged="txtValorJurosRecebido_TextChanged" AutoPostBack="True"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine"><span title="Valor de Desconto Recebido">Desconto</span></li>
                        <li class="liClear">
                            <asp:TextBox ID="txtValorDescontoRecebido" CssClass="txtValor campoMoeda" 
                                runat="server" ToolTip="Valor de Desconto Recebido" 
                                ontextchanged="txtValorDescontoRecebido_TextChanged" AutoPostBack="True"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine"><span title="Valor de Desconto de Bolsa Recebido">Desconto Bolsa</span></li>
                        <li class="liClear">
                            <asp:TextBox ID="txtValorDescontoBolsaRecebido" CssClass="txtValor campoMoeda" 
                                runat="server" ToolTip="Valor de Desconto de Bolsa Recebido" 
                                ontextchanged="txtValorDescontoBolsaRecebido_TextChanged" 
                                AutoPostBack="True"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine"><span title="Outros">Outros (+)</span></li>
                        <li class="liClear">
                            <asp:TextBox ID="txtOutrosValoresRecebidos" CssClass="txtValor campoMoeda" 
                                runat="server" ToolTip="Outros Valores Recebidos" 
                                ontextchanged="txtOutrosValoresRecebidos_TextChanged" AutoPostBack="True"></asp:TextBox>
                        </li>
                        
                        <li class="labelInLine" style="width: 75px;"><span title="Valor Recebido" class="lblObrigatorio">Valor Recebido</span></li>
                        <li class="liClear">
                            <asp:TextBox ID="txtValorRecebido" CssClass="txtValor campoMoeda" 
                                runat="server" ToolTip="Valor Recebido" AutoPostBack="True" 
                                ontextchanged="txtValorRecebido_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtValorRecebido" 
                                ErrorMessage="Informe o Valor Recebido" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li class="labelInLine" style="width: 75px;"><span title="Valor Recebido" class="lblObrigatorio">VL Excedente</span></li>
                        <li class="liClear">
            <asp:TextBox ID="txtValorExcedente" CssClass="txtValor campoMoeda" runat="server" Enabled="false" ToolTip="Valor Excedente"></asp:TextBox>
        </li>
                        </ul>
                    </li>
                </ul>
            </fieldset>
        </li>
        
        <li class="liClear"></li>
        <li style="margin-top: 18px; float:left;">
        <asp:Button ID="btnCalcular" runat="server" Text="Calcular" 
                onclick="btnCalcular_Click" />
        </li>
        <li style="margin-top: 10px; float:left;">
            <label for="txtDataRecebimento" title="Data de Recebimento">Data de Recebimento</label>
            <asp:TextBox ID="txtDataRecebimento" CssClass="campoData" runat="server" ToolTip="Data de Recebimento"></asp:TextBox>
        </li>
        <li style="margin-top: 10px; margin-left: 5px; float:left;">
            <label for="ddlTpPrevReceb" title="Tipo de Recebimento">Tipo Recbto.</label>
            <asp:DropDownList ID="ddlTipoReceb" runat="server" Width="110px" ToolTip="Selecione o Tipo de Recebimento">
                
            </asp:DropDownList>
        </li>
        <li style="margin-top: 10px; margin-left: 5px; float:left;">
            <label for="chkQuitado" title="Título Quitado?">Quitado</label>
            <asp:CheckBox ID="chkQuitado" runat="server" ToolTip="Título Quitado"></asp:CheckBox>
        </li>
        
    </ul>
<script type="text/javascript">
    $(document).ready(function() {
        $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        $(".txtNumeroParcela").mask('?999');
        $(".txtQuantidadeParcelas").mask('?999');
    });
</script>
</asp:Content>