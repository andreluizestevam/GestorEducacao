<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5301_CadastramentoTituloDespesaPgto.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    /*form { margin: 0 auto  }*/
    ul { display: table; list-style-type: none; }
    li { display: inline-table; }
    fieldset { padding: 3px; border: none; }
    legend { color: Black; }
    .divDados { width: 840px; margin: 0 auto; }
    .divTopoGeral ul { margin: 0 auto }
    .ulTopo3 li { display:block; }
    .fldData { display: inline-block; height: 80px; margin-bottom:5px; }
    .fldData li { display: block; }
    .fldDoc { display: inline-block; height: 80px;}
    .fldBanco { display: inline-block; height: 80px;}
    .fldContrato { display: inline-block; }
    .marginBotton {margin-bottom: 10px;}
    
    .divMeio { display: inline-block; float: left;}
    .divDireita { display: inline-block; float: left; margin-left:3px; }
    .ulContratoBaixo li { display: block; }
    .divBaixo { margin-top: 5px; }
    .ulObs { display:inline-table; }
    .ulObs li { display:block; }
    .ulSituacao { display:inline-table; float:right; margin-top:70px;  }
    
    .liValor { margin-left: 78px; }
    .liMulta { margin-left: 1px; }
    .liJuros { margin-left: 31px; }
    .liDesc { margin-left: 31px; }
    .liDescAnt { margin-left: 31px; }
    
    /*--> CSS DADOS */
    .ddlTpPes{ width: 80px; }
    .ddlTipoCobranca{ width: 102px; }
    .ddlNomeFonte{ width: 210px; }
    .txtApelidoFonte{ width: 210px; }    
    .ddlTpCAQ {width: 143px;}
    .ddlPlanejamento {width: 143px;}
    .ddlDotacao {width: 143px;}
    .check label{ display: inline; margin-left: -5px; }
    .check input{ margin-left: -5px; }    
    .labelTitle { font-weight: bold; margin-bottom: -2px!important;}
    .labelAux { font-size: 1.5em; margin-left: 5px; float: right; margin-top: -2px; }
    .txtNumeroDocumento{ width: 80px; }
    .txtNumeroParcela, .txtQuantidadeParcelas { width: 24px; }
    .txtCodRefDocumento, .ddlContrato, .txtCodigoFonte, .ddlLocalCobranca { width: 100px; }
    .ddlAditivo{ width: 40px; }
    .ddlHistorico { width: 335px; margin-bottom:5px; }
    .ddlCentroCusto { width: 365px; }
    .txtComplementoHistorico{ width: 335px; }    
    .ddlTipoDocumento{ width: 110px; }
    .ddlBanco{ width: 45px; }
    .ddlAgencia{ width: 78px; }
    .txtConta, .txtNossoNumero { width: 75px; }
    .txtCodigoContaContabil{ width: 100px; }
    .ddlContaContabil{ width: 112px; }    
    .txtValorTotal, .txtValorParcela, .txtValorMulta, .txtValorMultaRecebido, .txtValorJuros, .txtValorJurosRecebido { width: 72px; }
    .txtValorDesconto, .txtDescontoBolsa, .txtValorDescontoRecebido, .txtValorDescontoBolsaRecebido, .txtValorRecebido { width: 72px; }      
    .txtCodigoBarras { width: 310px; }
    .txtObservacao { width: 310px; }
    .liCadasForne
    {
        background-color:#F0FFFF;
        border:1px solid #D2DFD1;
        clear:none !important;
        margin-left:14px;
        /*margin-top:21px !important;*/
        padding:2px 3px 1px 7px;
        width: 65px;
        margin-right: 0px;
    }
    
</style>

<script type="text/javascript">

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="divDados" class="divDados">
        <div class="divTopoGeral">
            <ul class="ulTopo1">
                <li>
                    <label for="ddlTpPes" title="Natureza da Fonte" class="lblObrigatorio">Tipo</label>
                    <asp:DropDownList ID="ddlTpPes" CssClass="ddlTpPes" runat="server" ToolTip="Selecione a Natureza da Fonte"
                        OnSelectedIndexChanged="ddlTpPes_SelectedIndexChanged" AutoPostBack="true">                        
                        <asp:ListItem Value="F">Física</asp:ListItem>
                        <asp:ListItem Value="J">Jurídica</asp:ListItem>
                    </asp:DropDownList>          
                </li>
                <li style="margin-left: 5px;">
                    <label for="txtCodigoFonte" title="Código da Fonte">Nº CNPJ/CPF/Outros</label>
                    <asp:TextBox ID="txtCodigoFonte" CssClass="txtCodigoFonte campoNumerico" runat="server" Enabled="false"></asp:TextBox>
                </li>
                <li style="margin-left: 5px;">
                    <label for="ddlNomeFonte" class="lblObrigatorio" title="Nome da Fonte">Razão Social/Nome</label>
                    <asp:DropDownList ID="ddlNomeFonte" CssClass="ddlNomeFonte" runat="server" ToolTip="Selecione o Nome do Credor" 
                        OnSelectedIndexChanged="ddlNomeFonte_SelectedIndexChanged" AutoPostBack="true" />
                    <asp:RequiredFieldValidator ControlToValidate="ddlNomeFonte" ID="RequiredFieldValidator6" runat="server" 
                        ErrorMessage="Nome do Credor deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 5px;">
                    <label for="txtApelidoFonte" title="Apelido da Fonte">Nome Fantasia/Apelido</label>
                    <asp:TextBox ID="txtApelidoFonte" CssClass="txtApelidoFonte" runat="server" Enabled="false"></asp:TextBox>
                </li>
                <li>
                    <div style="margin-top: 0px; position: absolute;">
                        <ul>
                            <li title="Clique para Redirecionar para o Cadastro de Fornecedor" class="liCadasForne">                                    
                                <a id="lnkCadasForne" runat="server" href="" style="cursor: pointer;">CAD FORNEC</a>
                            </li>
                        </ul>
                    </div>
                </li>
                                                          
            </ul>
            <ul class="ulTopo2">
                <li>
                    <label for="ddlTipoDocumento" class="lblObrigatorio" title="Tipo de Documento">Tipo de Documento</label>
                    <asp:DropDownList ID="ddlTipoDocumento" CssClass="ddlTipoDocumento" runat="server" ToolTip="Selecione o Tipo de Documento"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlTipoDocumento" ID="RequiredFieldValidator11" runat="server" 
                        ErrorMessage="Tipo de Documento deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtNumeroDocumento" class="lblObrigatorio" title="Número do Documento">N° Documento</label>
                    <asp:TextBox ID="txtNumeroDocumento" CssClass="txtNumeroDocumento" Enabled="false" MaxLength="15" runat="server" ToolTip="Informe o Número do Documento"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtNumeroDocumento" ID="RequiredFieldValidator9" runat="server" 
                        ErrorMessage="N° Documento deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtNumeroParcela" class="lblObrigatorio" title="Número da Parcela">NP</label>
                    <asp:TextBox ID="txtNumeroParcela" CssClass="txtNumeroParcela campoNumerico" Enabled="false" runat="server" ToolTip="Informe o Número da Parcela"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtNumeroParcela" ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="N° Parcela deve ser informado" Display="None"></asp:RequiredFieldValidator>
                    <label class="labelAux">/</label>
                </li>
                <li>
                    <label for="txtQuantidadeParcelas" class="lblObrigatorio" title="Quantidade de Parcelas">QP</label>
                    <asp:TextBox ID="txtQuantidadeParcelas" CssClass="txtQuantidadeParcelas campoNumerico" runat="server" ToolTip="Informe a Quantidade de Parcelas"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtQuantidadeParcelas" ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Qtde Parcelas deve ser informada" Display="None"></asp:RequiredFieldValidator>                    
                </li>
                <li>
                    <label for="txtCodRefDocumento" title="Código de Referência do Documento">Cód. Ref. Documento</label>
                    <asp:TextBox ID="txtCodRefDocumento" CssClass="txtCodRefDocumento" MaxLength="4" runat="server" ToolTip="Informe o Código de Referência do Documento"></asp:TextBox>
                </li>
            </ul>
            <ul class="ulTopo3">
                <li>
                    <label for="ddlHistorico" class="lblObrigatorio" title="Histórico">Histórico</label>
                    <asp:DropDownList ID="ddlHistorico" CssClass="ddlHistorico" runat="server" ToolTip="Selecione o Histórico"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlHistorico" ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="Histórico deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>

                <li>
                    <label for="txtComplementoHistorico" title="Complemento do Histórico">Complemento Histórico</label>
                    <asp:TextBox ID="txtComplementoHistorico" CssClass="txtComplementoHistorico" MaxLength="200" runat="server" 
                        ToolTip="Informe o Complemento do Histórico"></asp:TextBox>
                </li>
            </ul>
        </div>

        <div class="divMeio">
            <fieldset class="fldData">
                <legend>DATAS</legend>
                <ul>
                    <li>
                        <label for="txtDataCadastro" title="Data de Cadastro">Cadastro</label>
                        <asp:TextBox ID="txtDataCadastro" CssClass="campoData" runat="server" Enabled="false" ToolTip="Informe a Data de Cadastro"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtDataVencimento" class="lblObrigatorio" title="Data de Vencimento">Vencimento</label>
                        <asp:TextBox ID="txtDataVencimento" CssClass="campoData" runat="server" ToolTip="Informe a Data de Vencimento"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtDataVencimento" ID="RequiredFieldValidator7" runat="server" 
                            ErrorMessage="Data de Vencimento deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    </li>
                </ul>
            </fieldset>
        
            <fieldset class="fldDoc">
                <legend>CLASSIFICAÇÃO DO DOCUMENTO</legend>
                <ul>
                    <li>
                        <label for="txtCodigoContaContabil" title="Código da Conta Contábil">Código</label>
                        <asp:TextBox ID="txtCodigoContaContabil" Width="90px" runat="server" Enabled="false"></asp:TextBox>
                    </li>
                    <li>
                        <label for="ddlTipoConta" class="lblObrigatorio" title="Tipo de Conta Contábil">Tp</label>
                        <asp:DropDownList ID="ddlTipoConta" CssClass="ddlTipoConta" Width="30px" runat="server" ToolTip="Selecione o Tipo de Conta Contábil"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlTipoConta_SelectedIndexChanged">
                            <asp:ListItem Value="P">2 - Passivo</asp:ListItem>
                            <asp:ListItem Value="D">4 - Custo e Despesa</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for="ddlTipoConta" class="lblObrigatorio" title="Grupo de Conta Contábil">Grp</label>
                        <asp:DropDownList ID="ddlGrupoConta" CssClass="ddlGrupoConta" Width="35px" runat="server" ToolTip="Selecione o Grupo de Conta Contábil"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoConta_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlGrupoConta" ID="RequiredFieldValidator17" runat="server" 
                            ErrorMessage="Grupo de Conta Contábil deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="ddlSubGrupoContaA" class="lblObrigatorio" title="SubGrupo de Conta Contábil">SGrp</label>
                        <asp:DropDownList ID="ddlSubGrupoConta" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupoConta_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupoConta" ID="RequiredFieldValidator12" runat="server" 
                            ErrorMessage="SubGrupo de Conta Contábil deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="ddlSubGrupo2ContaA" class="lblObrigatorio" title="SubGrupo 2 de Conta Contábil">SGrp 2</label>
                        <asp:DropDownList ID="ddlSubGrupo2Conta" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo 2 de Conta Contábil"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupo2Conta_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupo2Conta" ID="RequiredFieldValidator24" runat="server" 
                            ErrorMessage="SubGrupo 2 de Conta Contábil deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="ddlContaContabil" class="lblObrigatorio" title="Conta Contábil">Conta Contábil</label>
                        <asp:DropDownList ID="ddlContaContabil" CssClass="ddlContaContabil" runat="server" ToolTip="Selecione a Conta Contábil"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabil_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlContaContabil" ID="RequiredFieldValidator14" runat="server" 
                            ErrorMessage="Conta Contábil deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    </li>
                </ul>
                <ul>
                    <li>
                        <label for="ddlCentroCusto" class="lblObrigatorio" title="Centro de Custo">Centro de Custo</label>
                        <asp:DropDownList ID="ddlCentroCusto" CssClass="ddlCentroCusto" runat="server" ToolTip="Selecione o Centro de Custo"></asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlCentroCusto" ID="RequiredFieldValidator4" runat="server" 
                            ErrorMessage="Centro de Custo deve ser informado" Display="None"></asp:RequiredFieldValidator>
                    </li>
                <//ul>
            </fieldset>

            <fieldset class="fldBanco">
                <legend>INFORMAÇÕES DE COBRANÇA</legend>
                <ul class="marginBotton">
                    <li>
                        <label for="ddlTpCobranca" title="Tipo" class="lblObrigatorio">Tipo</label>
                        <asp:DropDownList ID="ddlTipoCobranca" CssClass="ddlTipoCobranca" runat="server" ToolTip="Selecione o tipo da cobrança"
                            OnSelectedIndexChanged="ddlTipoCobranca_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="" >Selecione</asp:ListItem>
                            <asp:ListItem Value="B">Bancária</asp:ListItem>
                            <asp:ListItem Value="E">Externa</asp:ListItem>
                            <asp:ListItem Value="I">Instituição</asp:ListItem>
                            <asp:ListItem Value="O">Outros</asp:ListItem>
                        </asp:DropDownList>          
                    </li>
                    <li>
                        <label for="ddlLocalCobranca" class="lblObrigatorio" title="Local de Cobraça">Local</label>
                        <asp:DropDownList ID="ddlLocalCobranca" CssClass="ddlLocalCobranca" runat="server" ToolTip="Selecione o Local de Cobraça"></asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlLocalCobranca" ID="RequiredFieldValidator15" runat="server" 
                            ErrorMessage="Local de Cobraça deve ser informado" Display="None"></asp:RequiredFieldValidator>
                    </li>
                </ul>
                <ul>
                    <li>
                        <label for="ddlBanco" title="Banco">Banco</label>
                        <asp:DropDownList ID="ddlBanco" CssClass="ddlBanco" Enabled="false" runat="server" ToolTip="Selecione o Banco" 
                            OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </li>
                    <li>
                        <label for="ddlAgencia" title="Agência">Agência</label>
                        <asp:DropDownList ID="ddlAgencia" CssClass="ddlAgencia" Enabled="false" runat="server" ToolTip="Selecione a Agência"></asp:DropDownList>
                    </li>
                    <li>
                        <label for="txtConta" title="Conta">Conta</label>
                        <asp:TextBox ID="txtConta" CssClass="txtConta" MaxLength="15" Enabled="false" runat="server" ToolTip="Informe a Conta"></asp:TextBox>
                    </li>
                </ul>
            </fieldset>

            <fieldset class="fldValores">
                <legend>INFORMAÇÕES FINANCEIRAS DO DOCUMENTO</legend>
                <ul>
                    <li>
                        <label class="labelTitle">Dados Financeiros do Documento</label>
                    </li>
                </ul>
                <ul>
                    <li>
                        <label for="txtDataDocumento" class="lblObrigatorio" title="Data de Emissão do Documento">Documento</label>
                        <asp:TextBox ID="txtDataDocumento" CssClass="campoData" runat="server" ToolTip="Informe a Data de Emissão do Documento"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtDataDocumento" ID="RequiredFieldValidator5" runat="server" 
                            ErrorMessage="Data de Emissão do Documento deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtValorTotal" class="lblObrigatorio" title="Valor Total">Total</label>
                        <asp:TextBox ID="txtValorTotal" CssClass="txtValorTotal campoMoeda" runat="server" ToolTip="Informe o Valor Total"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtValorTotal" ID="RequiredFieldValidator8" runat="server" 
                            ErrorMessage="Valor Total deve ser informado" Display="None"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtValorParcela" class="lblObrigatorio" title="Valor da Parcela">Parcela</label>
                        <asp:TextBox ID="txtValorParcela" CssClass="txtValorParcela campoMoeda" runat="server" ToolTip="Informe o Valor Total"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtValorParcela" ID="RequiredFieldValidator10" runat="server" 
                            ErrorMessage="Valor da Parcela deve ser informado" Display="None"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtValorMulta" title="Valor da Multa">Multa</label>
                        <asp:TextBox ID="txtValorMulta" CssClass="txtValorMulta campoMoeda" runat="server" ToolTip="Informe o Valor da Multa"></asp:TextBox>
                        <asp:CheckBox ID="chkValorMultaPercentual" CssClass="check chkValorMultaPercentual" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
                    </li>
                    <li>
                        <label for="txtValorJuros" title="Valor do Juros">Juros</label>
                        <asp:TextBox ID="txtValorJuros" CssClass="txtValorJuros campoMoeda" runat="server" ToolTip="Informe o Valor do Juros"></asp:TextBox>
                        <asp:CheckBox ID="chkValorJurosPercentual" CssClass="check chkValorJurosPercentual" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
                    </li>
                    <li>
                        <label for="txtValorDesconto" title="Valor Desconto">Desconto</label>
                        <asp:TextBox ID="txtValorDesconto" CssClass="txtValorDesconto campoMoeda" runat="server" ToolTip="Informe o Valor do Desconto"></asp:TextBox>
                        <asp:CheckBox ID="chkValorDescontoPercentual" CssClass="check chkValorDescontoPercentual" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
                    </li>
                    <li>
                        <label for="txtDescontoBolsa" title="Desconto por Antecipação">Desc Antecip</label>
                        <asp:TextBox ID="txtDescontoBolsa" CssClass="txtDescontoBolsa campoMoeda" runat="server" ToolTip="Informe o Desconto de Bolsa"></asp:TextBox>
                        <asp:CheckBox ID="chkValorDescontoBolsaPercentual" CssClass="check chkValorDescontoBolsaPercentual" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
                    </li>
                
                </ul>
                <ul>
                    <li>
                        <label class="labelTitle">Dados Financeiro do Pagamento</label>
                    </li>
                </ul>
                <ul>
                    <li class="liData">
                        <asp:TextBox ID="txtDataRecebimento" CssClass="campoData" runat="server" Enabled="false"></asp:TextBox>
                    </li>
                    <li class="liValor">
                        <asp:TextBox ID="txtValorRecebido" CssClass="txtValorRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                    </li>
                    <li class="liMulta">
                        <asp:TextBox ID="txtValorMultaRecebido" CssClass="txtValorMultaRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                    </li>
                    <li class="liJuros">
                        <asp:TextBox ID="txtValorJurosRecebido" CssClass="txtValorJurosRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                    </li>
                    <li class="liDesc">
                        <asp:TextBox ID="txtValorDescontoRecebido" CssClass="txtValorDescontoRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                    </li>
                    <li class="liDescAnt">
                        <asp:TextBox ID="txtValorDescontoBolsaRecebido" CssClass="txtValorDescontoBolsaRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                    </li>
                </ul>
            </fieldset>
        </div>

        <div class="divDireita">
            <fieldset class="fldContrato">
                <legend>INFORMAÇÕES DE CONTROLE</legend>
                <ul class="marginBotton">
                    <li>
                        <label for="ddlContrato" title="Contrato de Receita Fixa">N° Contrato</label>
                        <asp:DropDownList ID="ddlContrato" CssClass="ddlContrato" runat="server" ToolTip="Selecione o Contrato" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlContrato_SelectedIndexChanged"></asp:DropDownList>
                    </li>
                    <li>
                        <label for="ddlAditivo" title="Aditivo do Contrato">Aditivo</label>
                        <asp:DropDownList ID="ddlAditivo" CssClass="ddlAditivo" runat="server" ToolTip="Selecione o Aditivo"></asp:DropDownList>
                    </li>
                </ul>
                <ul class="ulContratoBaixo">
                    <li class="marginBotton">
                        <label for="ddlPlanejamento" title="Item Planejamento Executivo">Item Planejamento Executivo</label>
                        <asp:DropDownList ID="ddlPlanejamento" CssClass="ddlPlanejamento" runat="server" ToolTip="Selecione o Item Planejamento Executivo"/>
                    </li>
                    <li class="marginBotton">
                        <label for="ddlDotacao" title="Dotação Orçamentária">Dotação Orçamentária</label>
                        <asp:DropDownList ID="ddlDotacao" CssClass="ddlDotacao" runat="server" ToolTip="Selecione a Dotação Orçamentária"/>
                    </li>
                    <li class="marginBotton">
                        <label for="ddlAgrupador" title="Agrupador de Despesa">Agrupador de Despesa</label>
                        <asp:DropDownList ID="ddlAgrupador" CssClass="ddlDotacao" runat="server" ToolTip="Selecione o Agrupador de Despesa"/>
                    </li>
                </ul>
            </fieldset>
        </div>

        <div class="divBaixo">
            <ul class="ulObs">
                 <li class="liCodigoBarras">
                    <label for="txtCodigoBarras" title="Código de Barras">Código de Barras</label>
                    <asp:TextBox ID="txtCodigoBarras" CssClass="txtCodigoBarras" Enabled="false" runat="server"></asp:TextBox>
                </li>
                <li class="liObservacao">
                    <label for="txtObservacao" title="Observação">Observação</label>
                    <asp:TextBox ID="txtObservacao" CssClass="txtObservacao" MaxLength="200" 
                        runat="server" ToolTip="Informe a Observação" Rows="3" TextMode="MultiLine"></asp:TextBox>
                </li>
            </ul>
            <ul class="ulSituacao">
                <li>
                    <label for="txtDataSituacao" class="lblObrigatorio" title="Data da Situação">Data Situação</label>
                    <asp:TextBox ID="txtDataSituacao" Enabled="false" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtDataSituacao" ID="RequiredFieldValidator13" runat="server" 
                        ErrorMessage="Data da Situação deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlSituacao" class="lblObrigatorio" title="Situação">Situação</label>
                    <asp:DropDownList ID="ddlSituacao" Enabled="false" CssClass="ddlSituacao" runat="server" ToolTip="Selecione a Situação">
                        <asp:ListItem Value="A">Em Aberto</asp:ListItem>
                        <asp:ListItem Value="Q">Quitado</asp:ListItem>
                        <asp:ListItem Value="C">Cancelado</asp:ListItem>
                        <asp:ListItem Value="P">Parcialmente Quitado</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlSituacao" ID="RequiredFieldValidator16" runat="server" 
                        ErrorMessage="Situação deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
            </ul>
        </div>
    </div>

<script type="text/javascript">
    $(document).ready(function () {
        $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        $(".txtNumeroParcela").mask('?999');
        $(".txtQuantidadeParcelas").mask('?999');
    });
</script>
</asp:Content>