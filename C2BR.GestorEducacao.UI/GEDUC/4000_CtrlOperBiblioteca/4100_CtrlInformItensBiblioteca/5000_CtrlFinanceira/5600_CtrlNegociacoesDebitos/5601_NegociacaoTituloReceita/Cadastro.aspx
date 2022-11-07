<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5600_CtrlNegociacoesDebitos.F5601_NegociacaoTituloReceita.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1100px;
            margin-top:15px !important;
        }
        .ulDados input { margin-bottom: 0; }
                
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 5px;
            margin-right: 8px;
        }
        .liValores { margin-top: 10px; }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            float: right !important;           
            margin-bottom: 8px;
            margin-right: 13px !important;
            padding: 2px 3px 1px 3px;                  
        }
        .libtnJurNegoc
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;       
            margin-bottom: 8px;
            margin-right: 13px !important;
            padding: 2px 3px 1px 3px;                  
        }
        .liCliente { width: 530px; }
        .liValorSubTotal, .liValorBase
        {
            border-top: 1px solid #F0F0F0;
            clear: both;
            width: 150px;
        }
        .liValorLiquido
        {
        	border-top: 1px solid #F0F0F0;
            clear: none;
            width: 150px;
            margin-top: -20px;
        }
        .liGrdContratos
        {
        	width: 500px;
        	clear:both;
        }
        
        /*--> CSS DADOS */
        .labelInLine
        {
            clear: both;
            padding-top: 2px;
            width: 68px;            
        }
        .fldValores
        {
            padding-left: 25px;
            display: inline;
            border-width: 0px;
        }
        .fldNegociacao
        {
            padding-left: 10px;
            border-width: 0px;
            clear: both;
            padding-left: 25px;
        }
        .labelTitle
        {
            font-weight: bold;
            margin-top: 7px;
            margin-bottom: -2px !important;
        }
        .txtValor { width: 70px; }
        .txtNumeroParcela, .txtQuantidadeParcelas { width: 25px; }        
        .txtObservacoes
        {
            width: 230px;
            height: 55px;
            margin-top: 2px;
            border-width: 0px !important;
        }
        .th { position: relative !important; }                
        .txtCedente
        {
            width: 80px;
            margin-left: -5px;
        }
        .ddlMelhorDia {width:35px;}
        .txtTotSelecionado,.txtTotDebitos { width:60px;text-align:right; }
        .txtNomeResp { width:180px; }
        .divgrdNegociacao 
        {
        	height: 77px; 
        	overflow-y: scroll; 
        	overflow-x: hidden;
        	border: 1px solid #CCCCCC;
        }
        .ddlUnidContr { width:70px; }
        
    </style>
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liCliente" style="padding-left: 25px !important;">
            <ul>                
                <li>
                    <span title="Nome do Cedente">Nome Cedente</span>
                    <asp:TextBox ID="txtNomeCedente" CssClass="txtNomePessoa" style="display:block;" runat="server" Enabled="false"></asp:TextBox>
                </li>               
                <li style="display:inline;margin-left:10px !important;">
                    <span id="spaCedente" runat="server" title="Cedente">Cedente</span>
                    <asp:TextBox ID="txtCedente" CssClass="txtCedente" style="display:block;margin-left:0px !important;" runat="server" Enabled="false"></asp:TextBox>
                </li>      
                <li style="display:inline;margin-left:10px !important;">
                    <span id="spaNomeResp" visible="false" runat="server" title="Responsável">Responsável</span>
                    <asp:TextBox ID="txtNomeResp" Visible="false" CssClass="txtNomeResp" style="display:block;margin-left:0px !important;" runat="server" Enabled="false"></asp:TextBox>
                </li>                                               
             </ul>
        </li>
        <li class="liGrdContratos" style="margin-bottom:2px !important; margin-left: -10px;">
            <div id="divGrdContratos" runat="server"> 
                <asp:GridView runat="server" ID="grdContratos" CssClass="grdBusca"
                    AutoGenerateColumns="False" onrowdatabound="grdContratos_RowDataBound" >
                    <RowStyle CssClass="rowStyle" />
                    <HeaderStyle CssClass="th" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelect" runat="server" AutoPostBack="true" 
                                    oncheckedchanged="ckSelect_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="grdFooter" />
                </asp:GridView>
            </div>
        </li>       
        <li style="clear:both;">
            <ul id="ulFooterGrdContratos" runat="server">
                <li style="background-color:#F8F8FF;width:86%;height:18px;padding-top:2px;">
                    <span title="Total Selecionado">Total Selecionado:</span>
                    <asp:TextBox ID="txtTotSelecionado" CssClass="txtTotSelecionado" runat="server" Enabled="false"></asp:TextBox>
                
                    <span style="margin-left:170px;" title="Total Débito">Total Débito:</span>
                    <asp:TextBox ID="txtTotDebitos" CssClass="txtTotDebitos" runat="server" Enabled="false"></asp:TextBox>            
                </li>
                <li id="libtnNegociacao" runat="server" title="Clique para Negociar" class="liBtnAdd" style="margin-top:1px;">
                    <asp:LinkButton ID="btnGeraNegociacao" runat="server" OnClick="btnGeraNegociacao_Click">Negociar</asp:LinkButton>
                </li> 
            </ul>   
        </li>   
        <li style="width:470px;text-align:center;clear:none;margin-top: -320px !important;"><h1 style="font-size:14px;">DADOS DA NEGOCIAÇÃO</h1></li>
        <li class="liValores" style="width: 470px;margin-top: -290px;">
            <fieldset class="fldValores">
                <ul>
                    <li>
                        <ul>
                            <li style="margin-top: -5px !important;">
                                <label class="labelTitle">
                                    Resumo</label>
                            </li>
                            <li class="labelInLine"><span title="Valor do Débito">R$ Débito</span></li>
                            <li id="liValorDebito">
                                <asp:TextBox ID="txtValorDebito" ClientIDMode="Static" CssClass="txtValor campoMoeda" runat="server" ToolTip="Informe o Valor Débito"
                                    Enabled="false"></asp:TextBox>
                            </li>
                            <li class="labelInLine"><span title="Valor do Desconto">R$ Desconto</span></li>
                            <li id="li1">
                                <asp:TextBox ID="txtValorDesconto" ClientIDMode="Static" CssClass="txtValor campoMoeda" runat="server"
                                    ToolTip="Informe o Valor do Desconto" AutoPostBack="True" ontextchanged="txtValorDesconto_TextChanged"></asp:TextBox>
                            </li>
                            <li class="liValorSubTotal"><span style="margin-right: 13px; margin-top: 5px;" title="Valor do SubTotal">
                                SubTotal - R$</span>
                                <asp:TextBox ID="txtValorSubTotal" ClientIDMode="Static" Style="margin-top: 5px;" CssClass="txtValor campoMoeda"
                                    runat="server" ToolTip="Informe o Valor da Entrada" Enabled="false"></asp:TextBox>
                            </li>
                            <li class="labelInLine"><span title="Valor da Entrada">R$ Entrada</span></li>
                            <li style="border-bottom: 1px solid #F0F0F0;">
                                <asp:TextBox ID="txtValorEntrada" ClientIDMode="Static" CssClass="txtValor campoMoeda" runat="server" ToolTip="Informe o Valor da Entrada"
                                    AutoPostBack="True" ontextchanged="txtValorEntrada_TextChanged"></asp:TextBox>
                            </li>
                            <li class="liValorBase"><span style="margin-right: 5px; margin-top: 5px;" title="Valor Base">
                                Valor Base - R$</span>
                                <asp:TextBox ID="txtValorBase" ClientIDMode="Static" Style="margin-top: 5px;" CssClass="txtValor campoMoeda"
                                    runat="server" ToolTip="Informe o Valor da Entrada" Enabled="false"></asp:TextBox>
                            </li>
                            <li class="labelInLine"><span title="Valor Percentual do Juros">% Juros</span></li>
                            <li style="border-bottom: 1px solid #F0F0F0;">
                                <asp:TextBox ID="txtValorJuros" ClientIDMode="Static" CssClass="txtValor campoMoeda" runat="server" ToolTip="Informe o Valor Percentual do Juros"></asp:TextBox>
                            </li>
                            <li style="margin-top: -126px;">
                                <fieldset>
                                    <legend>Observação</legend>
                                    <asp:TextBox ID="txtObservacoes" CssClass="txtObservacoes" ToolTip="Informe as Observações"
                                        runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 250);"></asp:TextBox>
                                </fieldset>
                            </li>
                            <li runat="server" title="Clique para Calcular juros" class="libtnJurNegoc" style="margin-top:-1px;">
                                <asp:LinkButton ID="btnJurNegoc" Enabled="false" runat="server" OnClick="btnJurNegoc_Click">Gera Parce</asp:LinkButton>
                            </li>
                            <li class="liValorLiquido" style="margin-bottom:15px;clear:none;"><span style="margin-right: 12px; margin-top: 5px;font-weight:bold;" title="Valor Negociação">
                                Valor Negociação - R$</span>
                                <asp:TextBox ID="txtValorLiquido" ClientIDMode="Static" Style="margin-top: 7px;" CssClass="txtValor campoMoeda"
                                    runat="server" ToolTip="Informe o Valor Líquido" Enabled="false"></asp:TextBox>
                            </li>
                        </ul>
                    </li>
                </ul>
            </fieldset>
            <fieldset class="fldNegociacao">
                <ul>
                    <li>
                        <ul style="border-right: 1px solid #F0F0F0;">
                            <li>
                                <label class="labelTitle">
                                    Parcelamento</label>
                            </li>                            
                            <li style="clear: both;">
                                <span style="display:block;" title="Quantidade de Parcelas">QP</span>
                                <asp:TextBox ID="txtQtdeParcelas" Style="width: 25px;" CssClass="txtNumeroParcela"
                                    runat="server" ToolTip="Informe a Quantidade de Parcelas" AutoPostBack="True"
                                    OnTextChanged="txtQtdeParcelas_TextChanged"></asp:TextBox>
                            </li>                            
                            <li>
                                <span style="display:block;" title="Valor da Parcela">R$ Parcela</span>
                                <asp:TextBox ID="txtValorParcela" Style="width: 65px;" CssClass="txtValor campoMoeda"
                                    runat="server" ToolTip="Informe o Valor da Parcela" Enabled="false"></asp:TextBox>
                            </li>                            
                            <li>
                                <span style="display:block" title="Melhor dia">MD</span>
                                <asp:DropDownList ID="ddlMelhorDia" CssClass="ddlMelhorDia" runat="server" 
                                    ToolTip="Informe o Melhor Dia para Pagamento" AutoPostBack="True" 
                                    onselectedindexchanged="ddlMelhorDia_SelectedIndexChanged">
                                </asp:DropDownList>
                            </li>                            
                            <li>
                                <span style="display:block" title="Data de Vencimento da 1ª Parcela">1ª Vencto</span>
                                <asp:TextBox ID="txtVencPrimParcela" CssClass="campoData" runat="server" ToolTip="Informe a Data de Vencimento da 1ª Parcela"
                                    AutoPostBack="True" OnTextChanged="txtVencPrimParcela_TextChanged"></asp:TextBox>
                            </li>
                            <li>
                                <span style="display:block" title="Unidade de Contrato">Sigla</span>
                                <asp:DropDownList ID="ddlUnidadeContrato" CssClass="ddlUnidContr" runat="server" 
                                    ToolTip="Informe a Unidade de Contrato">
                                </asp:DropDownList>
                            </li>                            
                            <li class="labelInLine" style="width: 325px;">
                                <div class="divgrdNegociacao">
                                <asp:GridView runat="server" ID="grdNegociacao" CssClass="grdBusca" ToolTip="Para impressão do(s) boleto(s) selecione o(s) item(s)." AutoGenerateColumns="False"
                                    DataKeyNames="numContrato" OnPageIndexChanging="grdNegociacao_PageIndexChanging"
                                    OnDataBound="grdNegociacao_DataBound" Width="300px">
                                    <RowStyle CssClass="rowStyle" />
                                    <HeaderStyle CssClass="th" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="numContrato" HeaderText="Nº Doc">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numParcela" HeaderText="Nº Par">
                                            <ItemStyle HorizontalAlign="Right" Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dtVencimento" HeaderText="Dt Vencto" DataFormatString="{0:d}">
                                            <ItemStyle Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valorParcela" DataFormatString="{0:N}" HeaderText="R$ Parcela">
                                            <ItemStyle HorizontalAlign="Right" Width="70px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="IB">
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckSelect" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ckSelect_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="grdFooter" />
                                    <PagerTemplate>
                                        <table id="tblGridFooter">
                                            <tr>
                                                <td>
                                                    <span>Página:&nbsp;
                                                        <asp:DropDownList runat="server" ID="ddlGrdPages" OnSelectedIndexChanged="ddlGrdPages_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        &nbsp;de
                                                        <%# grdNegociacao.PageCount%></span>
                                                </td>
                                            </tr>
                                        </table>
                                    </PagerTemplate>
                                </asp:GridView>
                                </div>
                            </li>                                              
                            <li runat="server" title="Clique para Gerar Boleto" class="libtnJurNegoc" style="margin-top:-1px;clear:both;">
                                <asp:LinkButton ID="btnGeraBoleto" runat="server" Enabled="false" OnClick="btnGeraBoleto_Click">Gerar Boleto</asp:LinkButton>
                            </li>       
                            <li id="liTotalgrdNegoc" style="margin-left: 130px;">
                                <span title="Valor Total da Grade de Negociação" style="margin-right:5px !important;">Total:</span>
                                <asp:TextBox ID="txtTotalgrdNegoc" CssClass="txtValor campoMoeda" runat="server" ToolTip="Valor Total da Grid de Negociação" Enabled="false"></asp:TextBox>
                            </li>                              
                        </ul>
                    </li>
                </ul>
            </fieldset>
        </li>
    </ul>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtNumeroParcela").mask('?999');
            $(".txtQuantidadeParcelas").mask('?999');
        });
    </script>

</asp:Content>
