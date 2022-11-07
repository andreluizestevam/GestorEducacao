<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="MovimentacaoCaixa.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5100_Recebimento.F5103_RecebPagamCompromisso.MovimentacaoCaixa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">      
    .ulDados { width: 890px; margin-top: 25px !important;}
    .ulDados input{ margin-bottom: 0;} 
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both;} 
    .liDataMovimento, .liDocumento, .liDtVencto, .liGridForPag {clear:none !important; display:inline !important;}    
    .liGrdContratos
    {
        width: 800px;
        clear:both;
    }
    .liValSis { width: 100%; margin-top: 4px; }  
    .liInfCad { background-color: #E0FFFF; margin-top: 3px; }
    .liInfTitulo { background-color:#FFEFDB; margin-bottom: 7px !important; }
    .liValores { border-bottom: 1px solid #CCC; height: 165px; }
    .liCadCheque { margin-left: 5px; }    
    .liClear { clear: both; }
    
    /*--> CSS DADOS */
    .chkLocais { margin-left: -5px; }
    .chkLocais label { display: inline !important; margin-left:-4px;}
    .txtMoney { width: 60px; text-align:right; float: right; margin-top: -3px; }
    #divBarraPadraoContent{display:none;}    
    .th { position: relative !important; }
    .divGrdContratos
    {
    	height: 145px;
    	border: 1px solid #CCCCCC;
    	overflow-y: scroll;
    	width: 865px;
    }
    .lblResultados { margin-left: 0px; font-weight:bold; }
    .lblResultadosSis { margin-left: 5px; font-weight:bold; float: right;}      
    .txtValorFP { text-align:right; width:50px; }
    .fldFiliaResp { border: 0px;}    
    .imgCadCheque { height: 13px; width: 13px; }    
    .txtQtdeFP { width: 17px; text-align: right; }
    #divBarraComoChegar { position:absolute; margin-left: 770px; margin-top:-57px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
    #divBarraComoChegar ul { display: inline; float: left; margin-left: 10px; }
    #divBarraComoChegar ul li { display: inline; margin-left: -2px; }
    #divBarraComoChegar ul li img { width: 19px; height: 19px; }     
    .grdBusca .emptyDataRowStyle td { padding: 10px 355px !important; } 
    .lilnkRegPgto 
    {
        background-color:#F0FFFF;
        border:1px solid #D2DFD1;
        clear:none !important;
        margin-left:14px;
        margin-top:11px;
        padding:2px 3px 1px 7px;
        width: 56px;
        margin-right: 0px;
    }
      
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraComoChegar { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar" > 
        <div id="divBarraComoChegar" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
        <ul id="ulNavegacao" style="width: 39px;">
            <li id="btnVoltarPainel">
                <a href="javascript:parent.showHomePage()">
                    <img title="Clique para voltar ao Painel Inicial." 
                            alt="Icone Voltar ao Painel Inicial." 
                            src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                </a>
            </li>
            <li id="btnVoltar">
                <a href="javascript:BackToHome();">
                    <img title="Clique para voltar a Pagina Anterior."
                            alt="Icone Voltar a Pagina Anterior." 
                            src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                </a>
            </li>
        </ul>
        <ul id="ulEditarNovo" style="width: 39px;">
            <li id="btnEditar">
                <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar." src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
            </li>
            <li id="btnNovo">
                <img title="Abre o formulario para Criar um Novo Registro."
                        alt="Icone de Criar Novo Registro." 
                        src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
            </li>
        </ul>
        <ul id="ulGravar">
            <li>
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnQuitarTitulo_Click">
                    <img title="Grava o registro atual na base de dados."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar.png" />
                </asp:LinkButton>
            </li>
        </ul>
        <ul id="ulExcluirCancelar" style="width: 39px;">
            <li id="btnExcluir">
                <img title="Exclui o Registro atual selecionado."
                        alt="Icone de Excluir o Registro." 
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
            </li>
            <li id="btnCancelar">
                <a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                            alt="Icone de Cancelar Operacao Atual." 
                            src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a>
            </li>
        </ul>
        <ul id="ulAcoes" style="width: 39px;">
            <li  id="btnPesquisar">
                <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                        alt="Icone de Pesquisa." 
                        src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
            </li>
            <li id="liImprimir">
                <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão." 
                        alt="Icone de Impressao." 
                        src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />                    
            </li>
        </ul>
    </div>
</div>
<ul id="ulDados" class="ulDados">    
        <li style="margin-left: 0px;">
            <label style="clear:both;" title="Nome do Beneficiário">Tipo</label>
            <asp:CheckBox CssClass="chkLocais" ID="chkCredito" Checked="true" OnCheckedChanged="chkCredito_CheckedChanged" TextAlign="Right" AutoPostBack="true" runat="server" Text="Recebimento"/>                                
            <asp:CheckBox CssClass="chkLocais" ID="chkDebito" OnCheckedChanged="chkDebito_CheckedChanged" TextAlign="Right" AutoPostBack="true" runat="server" Text="Pagamento"/>
        </li>  
        <li style="clear: none;margin-left:10px;">
            <label title="Classificação Título">Classificação Título</label>
            <asp:CheckBox CssClass="chkLocais" ID="chkBenif1" Checked="true" OnCheckedChanged="chkBenif1_CheckedChanged" TextAlign="Right" AutoPostBack="true" runat="server" Text="Paciente"/>                                
            <asp:CheckBox CssClass="chkLocais" ID="chkBenif2" OnCheckedChanged="chkBenif2_CheckedChanged" TextAlign="Right" AutoPostBack="true" runat="server" Text="Cliente"/>
        </li>
        <li id="liNomeBenef" clientidmode="Static" style="clear: none;margin-left:10px;" runat="server">
            <label title="Responsável do Título">Responsável do Título</label>
            <asp:DropDownList ID="ddlBenef" ToolTip="Selecione o Responsável do Título" AutoPostBack="true" OnSelectedIndexChanged="ddlBenef_SelectedIndexChanged"
                CssClass="campoNomePessoa" runat="server">
            </asp:DropDownList>
        </li>
        <li class="liDataMovimento" style="clear: none;margin-left:10px;">
            <label title="Data da Quitação">Data da Quitação</label>
            <asp:TextBox ID="txtDataQuitacao" ToolTip="Data da Quitação" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidatorDataAtual" runat="server" CssClass="validatorField"
                                ForeColor="Red" ControlToValidate="txtDataQuitacao" Type="Date" Operator="LessThanEqual"
                                ErrorMessage="Data Status não pode ser maior que Data Atual.">
                            </asp:CompareValidator>
            <asp:RequiredFieldValidator ControlToValidate="txtDataQuitacao" ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Data da Quitação deve ser informada." Display="None"></asp:RequiredFieldValidator>
        </li>
        <li id="liAgrupador" style="clear: none;margin-left:10px;" runat="server">
            <label title="Agrupador">Agrupador</label>
            <asp:DropDownList ID="ddlAgrupador" ToolTip="Selecione o Agrupador" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlAgrupador_SelectedIndexChanged" Width="145px" 
                runat="server">
            </asp:DropDownList>
        </li>
        <li class="liGrdContratos" style="margin-bottom:7px !important;">
            <div class="divGrdContratos"> 
                <asp:GridView runat="server" ID="grdContratos" CssClass="grdBusca" 
                    OnRowDataBound="grdContratos_RowDataBound"
                    AutoGenerateColumns="False" 
                    OnSelectedIndexChanged="grdContratos_SelectedIndexChanged" >
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle"  />
                    <EmptyDataTemplate>                    
                        Nenhum Título em Aberto foi Encontrado<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelect" runat="server" AutoPostBack="true"
                                    oncheckedchanged="ckSelect_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="rowStyle" />
                    <HeaderStyle CssClass="th" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />                                        
                    <PagerStyle CssClass="grdFooter" />
                </asp:GridView>
            </div>
        </li>          
        <li class="liInfTitulo">
            <fieldset class="fldFiliaResp">
                <ul style="width:862px;padding-left: 5px;">
                    <li style="width: 101%; text-align:center;text-transform:uppercase;margin-left: -5px;"><span title="Informações do Título">Informações do Título</span></li>
                    <li class="liDataMovimento" style="width: 140px;">
                        <span title="Tipo Documento">
                            Tipo:</span>
                        <asp:Label ID="lblTipo" CssClass="lblResultados"
                            ToolTip="Tipo Documento" runat="server"></asp:Label>
                    </li>
                    <li class="liDocumento" style="width: 300px;">
                        <span title="Documento (Número/Parcela)">
                            Documento (Número/Parcela):</span>
                        <asp:Label ID="lblDocNum" CssClass="lblResultados" runat="server" ToolTip="Documento (Número)"></asp:Label> 
                        <span>/</span>
                        <asp:Label ID="lblDocParc" CssClass="lblResultados" runat="server" style="margin-left: 0px;" ToolTip="Documento (Parcela)"></asp:Label> 
                    </li>                                                                                         
                    <li class="liDtVencto" style="width: 95px; margin-left: 25px;">       
                        <span title="Data Emissão">
                            Emissão:</span>                 
                        <asp:Label ID="lblDtEmissao" CssClass="lblResultados" style="font-weight:normal; float: right;"
                            ToolTip="Data Emissão" runat="server"></asp:Label>
                    </li>
                    <li class="liDtVencto" style="width: 130px; margin-left: 25px;">
                        <span title="Data Vencimento">
                            Vencimento:</span>
                        <asp:Label ID="lblDtVencto" CssClass="lblResultados" style="float: right;"
                            ToolTip="Data Vencimento" runat="server"></asp:Label>
                    </li>
                    <li class="liDtVencto" style="width: 80px; margin-left: 25px;">
                        <span title="Quantidade Dias">
                            Qtde Dias:</span>
                        <asp:Label ID="lblQtdDias" CssClass="lblResultados"
                            ToolTip="Quantidade Dias" runat="server"></asp:Label>
                    </li>
                    <li class="liClear" style="width: 420px;">
                        <span title="Histórico">
                            Histórico:</span>
                        <asp:Label ID="lblHistorico" CssClass="lblResultados" style="font-weight:normal;"
                            ToolTip="Histórico" runat="server"></asp:Label>
                    </li>                    
                    <li class="liDtVencto" style="float: right; width: 330px; margin-right: 0px;">
                        <span title="Código de Barras" style="float:left;">
                            Cód. de Barras:</span>
                        <asp:Label ID="lblCodBarras" CssClass="lblResultados" style="font-weight:normal; margin-left: 3px;"
                            ToolTip="Código de Barras" runat="server"></asp:Label>
                    </li>                    
                </ul>
            </fieldset>
         </li>       
         <li class="liValores">
            <fieldset class="fldFiliaResp">                
                <ul style="width:865px;padding-left: 5px;">
                    <li style="background-color: #5F9EA0; width: 288px; text-align: center; padding-top: 3px; padding-bottom: 2px;"> <label style="text-transform: uppercase; color: White;">Resumo Financeiro</label></li>
                    <li style="width: 170px;border-right:1px solid #CCCCCC;padding-right: 5px; margin-top: -7px;">
                        <ul style="width: 100%;padding-right: 5px;">
                            <li class="liValSis">
                                <span title="Valor do Documento" style="float:left;">
                                    Valor do Documento</span>
                                <asp:Label ID="lblValDoctoSis" CssClass="lblResultadosSis"
                                    ToolTip="Valor do Documento"  runat="server">0,00</asp:Label>
                            </li>
                            <li class="liValSis">
                                <span title="Valor de Multa">
                                    Valor de Multa</span>
                                <asp:Label ID="lblValMulSis" CssClass="lblResultadosSis" style="font-weight:normal;"
                                    ToolTip="Valor de Multa" runat="server">0,00</asp:Label>
                            </li>
                            <li class="liValSis">
                                <span title="Valor de Juros">
                                    Valor de Juros</span>
                                <asp:Label ID="lblValCorSis" CssClass="lblResultadosSis" style="font-weight:normal;"
                                    ToolTip="Valor de Correção" runat="server">0,00</asp:Label>
                            </li>
                            <li class="liValSis">
                                <span title="Valor Adicional">
                                    Valor Adicional</span>
                                <asp:Label ID="lblValAdcSis" CssClass="lblResultadosSis" style="font-weight:normal;"
                                    ToolTip="Valor Adicional" runat="server">0,00</asp:Label>
                            </li>
                            <li class="liValSis">
                                <span title="Sub - Total">
                                    Sub - Total</span>
                                <asp:Label ID="lblValSubTotalSis" CssClass="lblResultadosSis"
                                    ToolTip="Sub - Total" runat="server">0,00</asp:Label>
                            </li>
                            <li class="liValSis">
                                <span title="Valor Desconto">
                                    Valor Desconto</span>
                                <asp:Label ID="lblValDesctoSis" CssClass="lblResultadosSis" style="font-weight:normal;"
                                    ToolTip="Valor Desconto" runat="server">0,00</asp:Label>
                            </li>
                            <li class="liValSis">
                                <span title="Total">
                                    Total</span>
                                <asp:Label ID="lblValTotalSis" CssClass="lblResultadosSis"
                                    ToolTip="Total" runat="server">0,00</asp:Label>
                            </li>
                        </ul>
                    </li>
                    <li style="width: 100px;border-right:1px solid #CCCCCC;padding-right: 5px;clear:none; margin-top: -7px;">
                        <ul style="width: 100%;padding-right: 5px;">         
                            <li class="liValSis">
                                <asp:Label ID="lblValDoctoInf" CssClass="lblResultadosSis"
                                    ToolTip="Valor do Documento"  runat="server">0,00</asp:Label>
                            </li>
                            <li class="liValSis" style="margin-top: 7px;">
                                <asp:TextBox ID="txtValMulInf" Enabled="false" CssClass="txtMoney" ontextchanged="txtValMulInf_TextChanged"
                                    ToolTip="Valor de Multa" runat="server" AutoPostBack="true"></asp:TextBox>
                            </li>
                            <li class="liValSis" style="margin-top: 5px;">
                                <asp:TextBox ID="txtValCorInf" Enabled="false" CssClass="txtMoney" ontextchanged="txtValCorInf_TextChanged"
                                    ToolTip="Valor de Correção" runat="server" AutoPostBack="true"></asp:TextBox>
                            </li>
                            <li class="liValSis" style="margin-top: 5px;">
                                <asp:TextBox ID="txtValAdcInf" Enabled="false" CssClass="txtMoney" ontextchanged="txtValAdcInf_TextChanged"
                                    ToolTip="Valor Adicional" runat="server" AutoPostBack="true"></asp:TextBox>
                            </li>
                            <li class="liValSis">
                                <asp:Label ID="lblValSubTotInf" CssClass="lblResultadosSis"
                                    ToolTip="Sub - Total" runat="server">0,00</asp:Label>
                            </li>
                            <li class="liValSis" style="margin-top: 7px;">
                                <asp:TextBox ID="txtValDesInf" Enabled="false" CssClass="txtMoney" ontextchanged="txtValDesInf_TextChanged"
                                    ToolTip="Valor Desconto" runat="server" AutoPostBack="true"></asp:TextBox>
                            </li>
                            <li class="liValSis">
                                <asp:Label ID="lblValTotInf" CssClass="lblResultadosSis"
                                    ToolTip="Total" runat="server">0,00</asp:Label>
                            </li>
                        </ul>
                    </li>
                    <li class="liGridForPag" style="margin-top: -24px; margin-right: 0; margin-left: 5px;">
                        <div class="divgrdNegociacao">
                        <asp:GridView runat="server" ID="grdFormPag" CssClass="grdBusca" AutoGenerateColumns="False"
                            DataKeyNames="CO_TIPO_REC" Width="565px">
                            <RowStyle CssClass="rowStyle" />
                            <HeaderStyle CssClass="th" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="DE_SIG_RECEB" HeaderText="TP">
                                    <ItemStyle Width="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DE_RECEBIMENTO" HeaderText="Descrição">
                                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Qtde">
                                    <ItemStyle Width="17px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQtdeFP" Enabled="false" CssClass="txtQtdeFP" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor R$">
                                    <ItemStyle Width="45px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValorFP" Enabled="false" CssClass="txtValorFP" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observação">
                                    <ItemStyle Width="275px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtObservacao" MaxLength="255" Enabled="false" style="width:295px; margin-right: 0 !important;" runat="server" />
                                        <asp:HiddenField ID="hdCO_TIPO_REC" runat="server" Value='<%# bind("CO_TIPO_REC") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                    </li>
                </ul>
            </fieldset>
          </li>    
          <li id="liCadCheque" class="liCadCheque" title="Clique para cadastrar cheques.">
            <img src="/Library/IMG/Gestor_SetaDireita.png" class="imgCadCheque" alt="" />&nbsp; 
            <a id="lnkCadCheque" runat="server" href="">Ir para Cadastro de Cheques</a> 
          </li>          
</ul>
<script type="text/javascript">
    $(document).ready(function () {
        $(".txtValorFP").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        $(".txtQtdeFP").mask("?99");        
    });
</script>
</asp:Content>