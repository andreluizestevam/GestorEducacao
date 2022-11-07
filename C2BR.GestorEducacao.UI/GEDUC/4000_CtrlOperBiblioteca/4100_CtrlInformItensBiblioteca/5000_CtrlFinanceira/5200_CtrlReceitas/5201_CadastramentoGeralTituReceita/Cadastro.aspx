<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5201_CadastramentoGeralTituReceita.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados { width: 1000px; padding-left: 45px; margin: 30px auto auto !important; }
    .ulDados input{ margin-bottom: 0;}

    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 8px;}
    .liTipoFonte, .liHistorico{ margin-right: 15px;}
    .liNomeFonte {margin-left:10px;}
    .liClear { clear: both; }
    .liContrato { margin-left: 90px; }
    .liObservacao{ clear: both; }
    .liCodigoFonte { margin-right: 2px !important; }
    .liCentroCusto{ margin-left: 60px; margin-top: 0px; }
    .liNomeContaContabil {margin-left:0px; margin-right: 5px !important;}
    .liValores { margin-top: 5px; }    
    .liBlocoEsquerdo { clear: both; margin-top: 5px; }
    .liBlocoDireito { width: 755px; margin-top: 10px; }
    .liValorTotal { margin-right: 10px; clear: both; }
    .liEmitirBoleto { margin-top: 13px; }
    .liNomeResponsavel { margin-left: 78px; clear: both; }        
    .liAux { margin-right: 2px !important; margin-left: -5px;}
    .lilnkBolCarne 
    {
        background-color:#F0FFFF;
        border:1px solid #D2DFD1;
        padding:2px 3px 1px;
        margin-top:10px;
    }
    .liBlocoBoleto { width: 370px; margin-right: 0px; margin-left: 20px; padding-top: 10px; }    
    .liObservacaoMatricula { margin-bottom: 5px !important; }
    
    /*--> CSS DADOS */    
    .fldDatas { padding-left: 10px; width: 170px;}
    .fldCobranca, .fldValores { padding-left: 10px; }    
    .check label{ display: inline; margin-left: -5px; }
    .check input{ margin-left: -5px; }    
    .labelTitle { font-weight: bold; margin-top: 7px; margin-bottom: 2px!important;}
    .labelAux { font-weight: bold; font-size: 1.5em; margin-top: 10px;}
    .liEmitirBoleto label { display: inline; }
    .txtNumeroDocumento{ width: 120px; }
    .txtNumeroParcela, .txtQuantidadeParcelas{ width: 24px; }
    .ddlContrato, .txtCodigoFonte { width: 100px; }
    .txtCodRefDocumento { width: 95px; }
    .ddlTipoLocalCobranca { width: 70px; }
    .ddlLocalCobranca { width: 132px; }
    .ddlAditivo{ width: 40px; }
    .ddlTipoFonte{ width: 70px; }
    .ddlNomeFonte{ width: 260px; }
    .ddlHistorico { width: 328px; }
    .ddlCentroCusto { width: 313px; }
    .txtComplementoHistorico{ width: 325px; }    
    .ddlTipoDocumento{ width: 110px; }    
    .txtCodigoContaContabil{ width: 78px; }
    .ddlContaContabil { width: 70px; }    
    .txtValorTotal, .txtValorParcela, .txtValorMulta, .txtValorJuros, .txtValorDesconto, .txtDescontoBolsa  { width: 70px; }
    .txtValorRecebido, .txtValorDescontoRecebido, .txtValorDescontoBolsaRecebido, .txtValorMultaRecebido, .txtValorJurosRecebido { width: 70px; margin-left: 25px; }  
    .txtObservacao, .txtObservacaoMatricula{ width: 410px; }    
    .txtCodigoBarras { width: 277px; }
    .ddlBanco { width:38px; }
    .ddlAgencia { width:45px; }
    .ddlConta { width:65px; }
    .ddlTipoTaxaBoleto { width:110px; }
    .ddlBoleto { width:183px; }
    .txtNomeResponsavel { width: 283px; }
    .ddlFlagBoleto { width: 45px; }
    .imgliLnk { width: 15px; height: 13px; }
    .ddlAgrupador { width:200px; }
    .txtLocalPagto { width:80px; }
    .liCobranca { margin-top: 45px; clear: both; }
    .liBlocoObser { margin-top: -20px; }
    .ddlTpPrevReceb { width:110px; }
    
</style>
<!--[if IE]>
<style type="text/css">
       .liCobranca { margin-top: 35px; clear: both; }
       .lilnkBolCarne 
        {
            background-color:#F0FFFF;
            border:1px solid #D2DFD1;
            padding:2px 3px 1px;
            margin-top:8px;
        }
        .liBlocoObser { margin-top: -30px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li>
            <ul>
                <li>
                    <ul>
                        <li>
                            <label for="txtNomeResponsavel" title="Responsável do Aluno">Responsável</label>
                            <asp:TextBox ID="txtNomeResponsavel" CssClass="txtNomeResponsavel" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="liCpfResponsavel">
                            <label for="txtCpfResponsavel" title="Responsável do Aluno">CPF</label>
                            <asp:TextBox ID="txtCpfResponsavel" CssClass="txtCpfResponsavel campoCpf" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="ddlTipoFonte" class="lblObrigatorio" title="Tipo da Fonte de Receita">TFR</label>
                            <asp:DropDownList ID="ddlTipoFonte" CssClass="ddlTipoFonte" runat="server" ToolTip="Selecione o Tipo da Fonte" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlTipoFonte_SelectedIndexChanged">
                                <asp:ListItem Value="A">Aluno</asp:ListItem>
                                <asp:ListItem Value="O">Não Aluno</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlTipoFonte" 
                                ID="RequiredFieldValidator4" runat="server" 
                                ErrorMessage="Tipo da Fonte de Receita deve ser informado" Text="*"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liCodigoFonte">
                            <label for="txtCodigoFonte" title="Código da Fonte">Código</label>
                            <asp:TextBox ID="txtCodigoFonte" CssClass="txtCodigoFonte campoNumerico" runat="server" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="liNomeFonte">
                            <label for="ddlNomeFonte" class="lblObrigatorio" title="Nome da Fonte">Nome</label>
                            <asp:DropDownList ID="ddlNomeFonte" CssClass="ddlNomeFonte" runat="server" ToolTip="Selecione o Nome da Fonte" 
                                OnSelectedIndexChanged="ddlNomeFonte_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlNomeFonte" ID="RequiredFieldValidator6" runat="server" 
                                ErrorMessage="Nome da Fonte deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        </li>                                                
                    </ul>
                </li>
                <li style="padding-left: 20px;">
                    <ul>
                        <li>
                            <label for="ddlTipoDocumento" class="lblObrigatorio" title="Tipo de Documento">Tipo de Documento</label>
                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="ddlTipoDocumento"  
                                ToolTip="Selecione o Tipo de Documento">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoDocumento"
                                ErrorMessage="Tipo de Documento deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="txtNumeroDocumento" class="lblObrigatorio" title="Número do Documento">N° Documento</label>
                            <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="txtNumeroDocumento" 
                                Enabled="false" MaxLength="20"
                                ToolTip="Informe o Número do Documento">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="txtNumeroDocumento"
                                ErrorMessage="N° Documento deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li class="liNumeroParcela">
                            <label for="txtNumeroParcela" class="lblObrigatorio" title="Número da Parcela">NP</label>
                            <asp:TextBox ID="txtNumeroParcela" CssClass="txtNumeroParcela campoNumerico" Enabled="false" runat="server" ToolTip="Informe o Número da Parcela"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtNumeroParcela" ID="RequiredFieldValidator50" runat="server" 
                                ErrorMessage="N° Parcela deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liAux">
                            <label class="labelAux">/</label>
                        </li>
                        <li class="liQuantidadeParcelas">
                            <label for="txtQuantidadeParcelas" class="lblObrigatorio" title="Quantidade de Parcelas">QP</label>
                            <asp:TextBox ID="txtQuantidadeParcelas" CssClass="txtQuantidadeParcelas campoNumerico" runat="server" ToolTip="Informe a Quantidade de Parcelas"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtQuantidadeParcelas" ID="RequiredFieldValidator2" runat="server" 
                                ErrorMessage="Qtde Parcelas deve ser informada" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liCodRefDocumento">
                            <label for="txtCodRefDocumento" title="Código de Referência do Documento">Cód. Ref. Documento</label>
                            <asp:TextBox ID="txtCodRefDocumento" CssClass="txtCodRefDocumento" MaxLength="20" runat="server" ToolTip="Informe o Código de Referência do Documento"></asp:TextBox>
                        </li>                                                                        
                        <%-- 
                        <li class="liContrato">
                            <label for="ddlContrato" title="Contrato de Receita Fixa">N° Contrato</label>
                            <asp:DropDownList ID="ddlContrato" CssClass="ddlContrato" runat="server" ToolTip="Selecione o Contrato" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlContrato_SelectedIndexChanged"></asp:DropDownList>
                        </li>
                        <li>
                            <label for="ddlAditivo" title="Aditivo do Contrato">Aditivo</label>
                            <asp:DropDownList ID="ddlAditivo" CssClass="ddlAditivo" runat="server" ToolTip="Selecione o Aditivo"></asp:DropDownList>
                        </li>--%>    
                    </ul>
                </li>
            </ul>
        </li>                
        <li class="liBlocoEsquerdo">
            <ul>
                <li>
                    <fieldset class="fldDatas">
                        <legend>Datas</legend>
                        <ul id="ulDatas">
                            <li>
                                <label for="txtDataCadastro" title="Data de Cadastro">Cadastro</label>
                                <asp:TextBox ID="txtDataCadastro" CssClass="campoData" runat="server" Enabled="false" ToolTip="Informe a Data de Cadastro"></asp:TextBox>
                            </li>
                            <li style="margin-left: 5px;">
                                <label for="txtDataDocumento" class="lblObrigatorio" title="Data de Emissão do Documento">Documento</label>
                                <asp:TextBox ID="txtDataDocumento" CssClass="campoData" runat="server" ToolTip="Informe a Data de Emissão do Documento"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtDataDocumento" ID="RequiredFieldValidator5" runat="server" 
                                    ErrorMessage="Data de Emissão do Documento deve ser informada" Display="None"></asp:RequiredFieldValidator>
                            </li>
                            <li class="liDataVencimento" style="margin-top: 5px;">
                                <label for="txtDataVencimento" class="lblObrigatorio" title="Data de Vencimento">Vencimento</label>
                                <asp:TextBox ID="txtDataVencimento" CssClass="campoData" runat="server" ToolTip="Informe a Data de Vencimento"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtDataVencimento" ID="RequiredFieldValidator7" runat="server" 
                                    ErrorMessage="Data de Vencimento deve ser informada" Display="None"></asp:RequiredFieldValidator>
                            </li>
                        </ul>
                    </fieldset>
                </li>
                <li class="liCobranca">
                    <fieldset class="fldCobranca">
                        <legend>Cobrança</legend>
                        <ul>
                            <li class="liClear" style="margin-top: 5px;">
                                <label for="ddlTipoLocalCobranca" class="lblObrigatorio" title="Tipo de Local de Cobrança">Tipo</label>
                                <asp:DropDownList ID="ddlTipoLocalCobranca" runat="server" CssClass="ddlTipoLocalCobranca" 
                                    ToolTip="Selecione o Tipo de Local de Cobrança" AutoPostBack="true" onselectedindexchanged="ddlTipoLocalCobranca_SelectedIndexChanged">
                                    <asp:ListItem Selected="true" Value="I" Text="Instituição"></asp:ListItem>
                                    <asp:ListItem Value="E" Text="Externa"></asp:ListItem>
                                    <asp:ListItem Value="B" Text="Bancária"></asp:ListItem>
                                    <asp:ListItem Value="O" Text="Outros"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li class="liClear">
                                <label for="ddlLocalCobranca" title="Local de Cobrança">Local</label>
                                <asp:DropDownList ID="ddlLocalCobranca" runat="server" CssClass="ddlLocalCobranca" 
                                    ToolTip="Selecione o Local de Cobrança" Enabled="false">
                                </asp:DropDownList>
                            </li>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                            <li style="clear:both; margin-top: 5px;">
                                <label for="ddlBanco" title="Banco">Banco</label>
                                <asp:DropDownList ID="ddlBanco" runat="server" CssClass="ddlBanco"
                                    ToolTip="Selecione o Banco" Enabled="false"
                                    onselectedindexchanged="ddlBanco_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </li>
                            <li style="margin-top: 5px;">
                                <label for="ddlAgencia" title="Agência">Agência</label>
                                <asp:DropDownList ID="ddlAgencia" runat="server" CssClass="ddlAgencia"
                                    ToolTip="Selecione a Agência" Enabled="false"
                                    onselectedindexchanged="ddlAgencia_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </li>
                            <li style="margin-top: 5px;">
                                <label for="ddlConta" title="Conta">Conta</label>
                                <asp:DropDownList ID="ddlConta" runat="server" CssClass="ddlConta" Enabled="false"
                                    ToolTip="Selecione a Conta">
                                </asp:DropDownList>
                            </li>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </ul>
                    </fieldset>
                </li>
            </ul>        
        </li>
        <li class="liBlocoDireito">
            <ul>
                <li class="liHistorico">
                    <label for="ddlHistorico" class="lblObrigatorio" title="Histórico">Histórico</label>
                    <asp:DropDownList ID="ddlHistorico" CssClass="ddlHistorico" runat="server" ToolTip="Selecione o Histórico"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlHistorico" ID="RequiredFieldValidator9" runat="server" 
                        ErrorMessage="Histórico deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>        
                <li style="margin-left: 173px;">
                            <label for="ddlAgrupador" title="Agrupador de Receita">Agrupador de Receita</label>
                            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Receita"/>
                        </li>                                                           
                <li class="liClear">
                    <label for="txtComplementoHistorico" title="Complemento do Histórico">Complemento Histórico</label>
                    <asp:TextBox ID="txtComplementoHistorico" CssClass="txtComplementoHistorico" MaxLength="200" runat="server" ToolTip="Informe o Complemento do Histórico"></asp:TextBox>
                </li>  
                <li class="liCentroCusto">
                    <label for="ddlCentroCusto" class="lblObrigatorio" title="Centro de Custo">Centro de Custo</label>
                    <asp:DropDownList ID="ddlCentroCusto" CssClass="ddlCentroCusto" runat="server" ToolTip="Selecione o Centro de Custo"></asp:DropDownList>
                    <asp:RequiredFieldValidator ControlToValidate="ddlCentroCusto" 
                        ID="RequiredFieldValidator12" runat="server" 
                        ErrorMessage="Centro de Custo deve ser informado" Text="*" 
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </li>              
                <li class="liClear" style="margin-right: 0px;">
                    <ul>
                        <li class="liValores">
                            <fieldset class="fldValores">
                                <legend>Valores</legend>
                                <ul>
                                    <li>
                                        <ul>
                                            <li>
                                                <label class="labelTitle">Documento</label>
                                            </li>
                                            <li class="liValorTotal">
                                                <label for="txtValorTotal" style="float:left;" class="lblObrigatorio" title="Valor Total">Total</label>
                                                <asp:TextBox ID="txtValorTotal" style="margin-left: 47px;" CssClass="txtValorTotal campoMoeda" runat="server" ToolTip="Informe o Valor Total"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtValorTotal" ID="RequiredFieldValidator8" runat="server" 
                                                    ErrorMessage="Valor Total deve ser informado" Display="None"></asp:RequiredFieldValidator>
                                            </li>
                                            <li class="liClear">
                                                <label for="txtValorParcela" style="float:left;" class="lblObrigatorio" title="Valor da Parcela">Parcela</label>
                                                <asp:TextBox ID="txtValorParcela" style="margin-left: 35px;" CssClass="txtValorParcela campoMoeda" runat="server" ToolTip="Informe o Valor da Parcela"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtValorParcela" ID="RequiredFieldValidator10" runat="server" 
                                                    ErrorMessage="Valor da Parcela deve ser informado" Display="None"></asp:RequiredFieldValidator>
                                            </li>
                                            <li class="liClear">
                                                <label for="txtValorMulta" style="float:left;" title="Valor da Multa">Multa</label>
                                                <asp:TextBox ID="txtValorMulta" style="margin-left: 49px;" CssClass="txtValorMulta campoMoeda" runat="server" ToolTip="Informe o valor ou percentual (mensal)"></asp:TextBox>
                                                <asp:CheckBox ID="chkValorMultaPercentual" CssClass="check chkValorMultaPercentual" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
                                            </li>
                                            <li class="liClear">
                                                <label for="txtValorJuros" style="float:left;" title="Valor do Juros">Juros</label>
                                                <asp:TextBox ID="txtValorJuros" style="margin-left: 49px; text-align: right;" CssClass="txtValorJuros" runat="server" ToolTip="Informe o valor ou percentual (diário) do juros (correção)"></asp:TextBox>
                                                <asp:CheckBox ID="chkValorJurosPercentual" CssClass="check chkValorJurosPercentual" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
                                            </li>
                                            <li class="liClear">
                                                <label for="txtOutroValor" style="float:left;" title="Outros (+)">Outros (+)</label>
                                                <asp:TextBox ID="txtOutroValor" style="margin-left: 24px;" CssClass="txtDescontoBolsa campoMoeda" runat="server" ToolTip="Informe o valor ou percentual de outros a ser cobrado"></asp:TextBox>                                
                                                <asp:CheckBox ID="chkOutroValorPercentual" CssClass="check chkValorDescontoBolsaPercentual" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
                                            </li>
                                            <li class="liClear">
                                                <label for="txtValorDesconto" style="float:left;" title="Valor Desconto">Desconto</label>
                                                <asp:TextBox ID="txtValorDesconto" style="margin-left: 31px;" CssClass="txtValorDesconto campoMoeda" runat="server" ToolTip="Informe o valor ou percentual de desconto extra"></asp:TextBox>
                                                <asp:CheckBox ID="chkValorDescontoPercentual" CssClass="check chkValorDescontoPercentual" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
                                            </li>
                                            <li class="liClear">
                                                <label for="txtDescontoBolsa" style="float:left;" title="Desconto de Bolsa">Desconto Bolsa</label>
                                                <asp:TextBox ID="txtDescontoBolsa" style="margin-left: 5px;" CssClass="txtDescontoBolsa campoMoeda" runat="server" ToolTip="Informe o valor ou percentual de deconto de bolsa"></asp:TextBox>
                                                <asp:CheckBox ID="chkValorDescontoBolsaPercentual" CssClass="check chkValorDescontoBolsaPercentual" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
                                            </li>
                                            <li class="liClear">
                                                <label for="ddlTpPrevReceb" title="Tipo de Previsão de Recebimento">Tipo Pre. Recbto.</label>
                                                <asp:DropDownList ID="ddlTpPrevReceb" runat="server" CssClass="ddlTpPrevReceb" ToolTip="Selecione o Tipo de Previsão de Recebimento">
                                                    <asp:ListItem Selected="true" Value="" Text="Nenhum"></asp:ListItem>
                                                    <asp:ListItem Value="B" Text="Banco"></asp:ListItem>
                                                    <asp:ListItem Value="D" Text="Dinheiro"></asp:ListItem>
                                                    <asp:ListItem Value="C" Text="Cartão"></asp:ListItem>
                                                    <asp:ListItem Value="H" Text="Cheque"></asp:ListItem>
                                                    <asp:ListItem Value="M" Text="Misto"></asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                        </ul>
                                    </li>
                                    <li>
                                        <ul>
                                            <li>
                                                <label class="labelTitle">Inform de Pagamento</label>
                                            </li>
                                            <li class="liClear">
                                                <label for="txtDataRecebimento" style="float:left;" title="Data de Recebimento">Data</label>
                                                <asp:TextBox ID="txtDataRecebimento" style="margin-left: 5px;" CssClass="campoData" runat="server" Enabled="false"></asp:TextBox>
                                            </li>
                                            <li class="liClear" style="margin-top: -2px;">
                                                <asp:TextBox ID="txtValorRecebido" CssClass="txtValorRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                                            </li>
                                            <li class="liClear">
                                                <asp:TextBox ID="txtValorMultaRecebido" CssClass="txtValorMultaRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                                            </li>
                                            <li class="liClear">
                                                <asp:TextBox ID="txtValorJurosRecebido" CssClass="txtValorJurosRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                                            </li>
                                            <li class="liClear">
                                                <asp:TextBox ID="txtValorOutroRecebido" CssClass="txtValorDescontoBolsaRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                                            </li>
                                            <li class="liClear">
                                                <asp:TextBox ID="txtValorDescontoRecebido" CssClass="txtValorDescontoRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                                            </li>
                                            <li class="liClear">
                                                <asp:TextBox ID="txtValorDescontoBolsaRecebido" CssClass="txtValorDescontoBolsaRecebido campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                                            </li>     
                                            <li class="liClear">
                                                <label for="ddlTipoReceb" title="Tipo de Previsão de Recebimento">Tipo Recbto.</label>
                                                <asp:DropDownList ID="ddlTipoReceb" runat="server" CssClass="ddlTpPrevReceb" Enabled="false" ToolTip="Selecione o Tipo de Recebimento">
                                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                                    <asp:ListItem Value="B" Text="Banco"></asp:ListItem>
                                                    <asp:ListItem Value="D" Text="Dinheiro"></asp:ListItem>
                                                    <asp:ListItem Value="C" Text="Cartão"></asp:ListItem>
                                                    <asp:ListItem Value="H" Text="Cheque"></asp:ListItem>
                                                    <asp:ListItem Value="M" Text="Misto"></asp:ListItem>
                                                </asp:DropDownList>
                                            </li>                                           
                                        </ul>
                                    </li>                                                                                                                      
                                </ul>
                            </fieldset>
                        </li>
                    </ul>
                </li>                            
                <li style="">
                    <ul>
                        <li>
                            <ul>                                
                                <li class="liBlocoBoleto">
                                    <ul>      
                                        <li style="clear: both; width: 100%; margin-bottom: 1px;">
                                            <label style="font-size: 1.1em; font-weight: bold;" title="Classificação Contábil">Classificação Contábil</label>
                                        </li>                                                                             
                                        <li style=" margin-right: 0px; width: 55px; padding-top: 15px;">
                                            <label style="font-size:1.2em;" title="Conta Contábil Ativo">Cta Ativo</label>
                                        </li>             
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                        <li style="margin-left: 10px;">
                                            <label for="ddlTipoContaA" class="lblObrigatorio" title="Tipo de Conta Contábil">Tp</label>
                                            <asp:DropDownList ID="ddlTipoContaA" CssClass="ddlTipoConta" Width="30px" runat="server" ToolTip="Selecione o Tipo de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlTipoConta_SelectedIndexChanged">
                                                <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                                                <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <label for="ddlGrupoContaA" class="lblObrigatorio" title="Grupo de Conta Contábil">Grp</label>
                                            <asp:DropDownList ID="ddlGrupoContaA" CssClass="ddlGrupoConta" Width="35px" runat="server" ToolTip="Selecione o Grupo de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoConta_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlGrupoContaA" ID="RequiredFieldValidator17" runat="server" 
                                                ErrorMessage="Grupo de Conta Contábil Ativa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <label for="ddlSubGrupoContaA" class="lblObrigatorio" title="SubGrupo de Conta Contábil">SGrp</label>
                                            <asp:DropDownList ID="ddlSubGrupoContaA" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupoConta_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupoContaA" ID="RequiredFieldValidator11" runat="server" 
                                                ErrorMessage="SubGrupo de Conta Contábil Ativa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <label for="ddlSubGrupo2ContaA" class="lblObrigatorio" title="SubGrupo 2 de Conta Contábil">SGrp 2</label>
                                            <asp:DropDownList ID="ddlSubGrupo2ContaA" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo 2 de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupo2Conta_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupo2ContaA" ID="RequiredFieldValidator23" runat="server" 
                                                ErrorMessage="SubGrupo 2 de Conta Contábil Ativa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <label for="ddlContaContabilA" class="lblObrigatorio" title="Conta Contábil">Conta Contábil</label>
                                            <asp:DropDownList ID="ddlContaContabilA" CssClass="ddlContaContabil" runat="server" ToolTip="Selecione a Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabil_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlContaContabilA" ID="RequiredFieldValidator3" runat="server" 
                                                ErrorMessage="Conta Contábil Ativa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li>
                                            <label for="txtCodigoContaContabilA" title="Código da Conta Contábil">Código</label>
                                            <asp:TextBox ID="txtCodigoContaContabilA" Width="50px" runat="server" Enabled="false"></asp:TextBox>
                                        </li>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <li style="clear: both; margin-right: 0px; width: 55px; padding-top: 2px;">
                                            <label style="font-size:1.2em;" title="Conta Contábil Caixa">Cta Caixa</label>
                                        </li>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>                
                                        <li style="margin-left: 10px;">
                                            <asp:DropDownList ID="ddlTipoContaC" CssClass="ddlTipoConta" Width="30px" runat="server" ToolTip="Selecione o Tipo de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlTipoContaC_SelectedIndexChanged">
                                                <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                                                <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <asp:DropDownList ID="ddlGrupoContaC" CssClass="ddlGrupoConta" Width="35px" runat="server" ToolTip="Selecione o Grupo de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaC_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlGrupoContaC" ID="RequiredFieldValidator14" runat="server" 
                                                ErrorMessage="Grupo de Conta Contábil de Caixa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <asp:DropDownList ID="ddlSubGrupoContaC" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupoContaC_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupoContaC" ID="RequiredFieldValidator15" runat="server" 
                                                ErrorMessage="SubGrupo de Conta Contábil de Caixa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <asp:DropDownList ID="ddlSubGrupo2ContaC" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo 2 de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupo2ContaC_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupo2ContaC" ID="RequiredFieldValidator24" runat="server" 
                                                ErrorMessage="SubGrupo 2 de Conta Contábil de Caixa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <asp:DropDownList ID="ddlContaContabilC" CssClass="ddlContaContabil" runat="server" ToolTip="Selecione a Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilC_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlContaContabilC" ID="RequiredFieldValidator18" runat="server" 
                                                ErrorMessage="Conta Contábil de Caixa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li>
                                            <asp:TextBox ID="txtCodigoContaContabilC" Width="50px" runat="server" Enabled="false"></asp:TextBox>
                                        </li>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <li style="clear: both; margin-right: 0px; width: 55px; padding-top: 2px;">
                                            <label style="font-size:1.2em;" title="Conta Contábil Banco">Cta Banco</label>
                                        </li>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>                
                                        <li style="margin-left: 10px;">
                                            <asp:DropDownList ID="ddlTipoContaB" CssClass="ddlTipoConta" Width="30px" runat="server" ToolTip="Selecione o Tipo de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlTipoContaB_SelectedIndexChanged">
                                                <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                                                <asp:ListItem Value="C">3 - Receita</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <asp:DropDownList ID="ddlGrupoContaB" CssClass="ddlGrupoConta" Width="35px" runat="server" ToolTip="Selecione o Grupo de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoContaB_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlGrupoContaB" ID="RequiredFieldValidator20" runat="server" 
                                                ErrorMessage="Grupo de Conta Contábil de Banco deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <asp:DropDownList ID="ddlSubGrupoContaB" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupoContaB_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupoContaB" ID="RequiredFieldValidator21" runat="server" 
                                                ErrorMessage="SubGrupo de Conta Contábil de Banco deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <asp:DropDownList ID="ddlSubGrupo2ContaB" CssClass="ddlSubGrupoConta" Width="40px" runat="server" ToolTip="Selecione o SubGrupo 2 de Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSubGrupo2ContaB_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlSubGrupo2ContaB" ID="RequiredFieldValidator25" runat="server" 
                                                ErrorMessage="SubGrupo 2 de Conta Contábil de Banco deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li class="liNomeContaContabil">
                                            <asp:DropDownList ID="ddlContaContabilB" CssClass="ddlContaContabil" runat="server" ToolTip="Selecione a Conta Contábil"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilB_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlContaContabilB" ID="RequiredFieldValidator22" runat="server" 
                                                ErrorMessage="Conta Contábil de Banco deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li>
                                            <asp:TextBox ID="txtCodigoContaContabilB" Width="50px" runat="server" Enabled="false"></asp:TextBox>
                                        </li>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <li style="clear: both; width: 120px; margin-bottom: 0px; margin-left:0px !important; margin-top: 13px;text-align:left;">
                                            <label style="font-size: 1.1em; font-weight: bold;float:left" title="Boleto Bancário">Boleto Bancário</label>
                                        </li>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                        <li style="margin-top: 0px;clear:left">
                                            <label for="ddlUnidadeContrato" class="lblObrigatorio" title="Unidade de Contrato">Unidade de Contrato</label>
                                            <asp:DropDownList ID="ddlUnidadeContrato" runat="server" CssClass="campoUnidadeEscolar"
                                                ToolTip="Selecione a Unidade de Contrato" AutoPostBack="true" onselectedindexchanged="ddlUnidadeContrato_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlUnidadeContrato" 
                                                ErrorMessage="Unidade de Contrato deve ser informada" Display="None" CssClass="validatorField">
                                            </asp:RequiredFieldValidator>
                                        </li>
                                        <li>
                                            <label for="ddlUnidadeContrato" class="lblObrigatorio" title="Nosso Número">Nosso Número</label>
                                            <asp:TextBox ID="txtNossoNum" Width="116" Enabled="false" runat="server"></asp:TextBox>
                                        </li>
                                        <li class="liClear">
                                            <label for="ddlFlagBoleto" title="Emite Boleto Bancário?">Boleto</label>
                                            <asp:DropDownList ID="ddlFlagBoleto" runat="server" CssClass="ddlFlagBoleto"
                                                ToolTip="Selecione se irá emitir o Boleto Bancário" AutoPostBack="true" onselectedindexchanged="ddlFlagBoleto_SelectedIndexChanged">
                                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                                <asp:ListItem Value="N" Selected="true">Não</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>                                                  
                                        <li>
                                            <label for="ddlTipoTaxaBoleto" title="Tipo de Taxa do Boleto">Tipo</label>
                                            <asp:DropDownList ID="ddlTipoTaxaBoleto" runat="server" CssClass="ddlTipoTaxaBoleto"
                                                ToolTip="Selecione o Tipo de Taxa do Boleto" AutoPostBack="true" onselectedindexchanged="ddlTipoTaxaBoleto_SelectedIndexChanged">
                                                <asp:ListItem Value="" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="M">Matricula</asp:ListItem>
                                                <asp:ListItem Value="R">Renovação</asp:ListItem>
                                                <asp:ListItem Value="E">Mensalidade</asp:ListItem>
                                                <asp:ListItem Value="A">Atividades Extras</asp:ListItem>
                                                <asp:ListItem Value="B">Biblioteca</asp:ListItem>
                                                <asp:ListItem Value="S">Serv. de Secretaria</asp:ListItem>
                                                <asp:ListItem Value="D">Serv. Diversos</asp:ListItem>
                                                <asp:ListItem Value="N">Negociação</asp:ListItem>
                                                <asp:ListItem Value="O">Outros</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin-left: -2px;">
                                            <label for="ddlBoleto" title="Boleto Bancário">Boleto</label>
                                            <asp:DropDownList ID="ddlBoleto" runat="server" CssClass="ddlBoleto"
                                                ToolTip="Selecione o Boleto Bancário">
                                            </asp:DropDownList>
                                        </li>                                               
                                        </ContentTemplate>
                                        </asp:UpdatePanel>                
                                        <li class="liClear">
                                            <label for="txtCodigoBarras" title="Boleto Bancário - Linha Digitável">Boleto Bancário - Linha Digitável</label>
                                            <asp:TextBox ID="txtCodigoBarras" CssClass="txtCodigoBarras" Enabled="false" runat="server"></asp:TextBox>
                                        </li>
                                        <li id="lilnkBolCarne" runat="server" title="Clique para Emprimir a Segunda Via do Boleto de Mensalidades" class="lilnkBolCarne" style="clear: both;">
                                            <asp:LinkButton ID="lnkBoleto" OnClick="lnkBoleto_Click" ValidationGroup="boleto" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir segunda via do Boleto">
                                                <img class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Boleto" title="BOLETO" />
                                                <asp:Label runat="server" ID="lblBoleto" Text="2° VIA"></asp:Label>
                                            </asp:LinkButton>
                                        </li>                                        
                                        <li id="lilnkNovBolCarne" runat="server" title="Clique para Emprimir um Novo Boleto de Mensalidades" class="lilnkBolCarne">                                    
                                            <asp:LinkButton ID="lnkNovoBoleto" OnClick="lnkNovoBoleto_Click" ValidationGroup="boleto" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir novo Boleto">
                                                <img class="imgliLnk" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Boleto" title="NOVO BOLETO" />
                                                <asp:Label runat="server" ID="lblNovoBoleto" Text="NOVO BOLETO"></asp:Label>
                                            </asp:LinkButton>
                                        </li>                                        
                                        <li style="margin-bottom: 1px !important;">
                                            <label for="txtDataSituacao" class="lblObrigatorio" title="Data da Situação">Data Situação</label>
                                            <asp:TextBox ID="txtDataSituacao" Enabled="False" ToolTip="Informe a Data da Situação" CssClass="campoData" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="txtDataSituacao" ID="RequiredFieldValidator13" runat="server" 
                                                ErrorMessage="Data da Situação deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                        <li style="margin-bottom: 1px !important;">
                                            <label for="ddlSituacao" class="lblObrigatorio" title="Situação">Situação</label>
                                            <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" ToolTip="Selecione a Situação">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlSituacao" ID="RequiredFieldValidator16" runat="server" 
                                                ErrorMessage="Situação deve ser informada" Display="None"></asp:RequiredFieldValidator>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>                        
                    </ul>
                </li>                                
            </ul>
        </li>
        <li class="liBlocoObser">
            <ul>
                <li class="liObservacao" style="margin-top: -30px !important">
                    <label for="txtObservacao" title="Observação">Observação</label>
                    <asp:TextBox ID="txtObservacao" CssClass="txtObservacao" MaxLength="200" runat="server" ToolTip="Informe a Observação"></asp:TextBox>
                </li>
                <li style="margin-left: 20px; margin-top: -30px;">
                    <label for="txtObservacao" title="Local de Pagamento">Local de Pagto</label>
                    <asp:TextBox ID="txtLocalPagto" CssClass="txtLocalPagto" Enabled="false" runat="server" ToolTip="Local de Pagamento"></asp:TextBox>
                </li>
                <li class="liClear">
                    <label for="txtObservacaoMatricula" title="Observação da Matrícula">Observação da Matrícula</label>
                    <asp:TextBox ID="txtObservacaoMatricula" CssClass="txtObservacaoMatricula" runat="server" Enabled="false" ToolTip="Informe a Observação da Matrícula"></asp:TextBox>
                </li>
            </ul>
        </li>                               
    </ul>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        //$(".campoMoeda").mask("?99999");
        //$(".txtNumeroParcela").mask("99999-999");
        //$(".txtQuantidadeParcelas").mask("99999-999");
    });

    $(document).ready(function() {
        $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        $(".txtNumeroParcela").mask('?999');
        $(".txtQuantidadeParcelas").mask('?999');
    });
</script>
</asp:Content>