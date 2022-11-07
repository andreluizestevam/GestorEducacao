<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5105_FechamentoCaixa.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">  
    .ulDados { width: 700px; margin: 7px auto auto !important;}
    .ulDados input{ margin-bottom: 0;}
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both;}       
    .liBtnFecCaixa
    {
        background-color: #FFFFE0;
        border: 1px solid #D2DFD1;         
        margin-bottom: 8px;
        padding: 2px 3px 1px 3px;   
        margin-top: 0px;  
        margin-left: 10px;
        clear: none !important;             
    }
    .liBtnImpFecCaixa    
    {
        background-color: #F8F8FF;
        border: 1px solid #D2DFD1;       
        margin-bottom: 8px;
        padding: 2px 3px 1px 3px;   
        margin-top: 0px;     
        margin-left: 215px;          
    }
    .liBtnAnaRecPag
    {
    	background-color: #FFFFE0;
        border: 1px solid #D2DFD1;         
        margin-bottom: 8px;
        padding: 2px 3px 1px 3px;   
        margin-top: -4px;  
        margin-left: 11px;
        clear: none !important; 
        margin-right: 0px !important;
    }
    .liNomeCaixa { margin-top: 10px; margin-left: 50px; }
    .liSiglaCaixa { clear:none !important; margin-top: 10px; }
    
    /*--> CSS DADOS */
    .txtNomeCaixa { width: 130px;}
    .txtMoney { width: 55px; text-align:right; }
    #divBarraPadraoContent{display:none;}
    #helpMessages {display:none;}    
    .txtSiglaCaixa { width: 70px; }
    .txtQtdeFP { width: 20px; text-align: right; }
    .txtQtdeRA { width: 20px; text-align: right; }
    .txtValorFP { width: 55px; text-align: right; }
    .imgValida { width: 13px; height: 13px; }
    .liGridForPag { margin-top: -5px; }
    .txtDataMovto { width: 60px; }
    .divgrdNegociacao { height: 159px; border: 1px solid #CCC; }
    .liGridApoSan { width: 358px; margin-left: 170px; margin-top: 3px; }
    .liRecebimento { width: 335px; margin-top: 2px; margin-right: 0px !important; margin-left: -10px; }    
    .liPagamento { width: 335px; margin-top: 2px; margin-right: 0px !important; clear:none !important; margin-left: 15px; margin-left: 40px;}
    .imgliLnk { width: 15px; height: 13px; }
    #divBarraComoChegar { position:absolute; margin-left: 770px; margin-top:-35px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
    #divBarraComoChegar ul { display: inline; float: left; margin-left: 10px; }
    #divBarraComoChegar ul li { display: inline; margin-left: -2px; }
    #divBarraComoChegar ul li img { width: 19px; height: 19px; }   
    .helpMessages
    {
        margin-top: 5px;
        font-size: 11px !important;
    }
    
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraComoChegar { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<div id="helpMessagesFC" class="helpMessages">
    <div id="divMensagGenerica" class="divMensagGenerica">
        <span id="lblMsgGenric" style="margin-top: -3px;">Informe os dados de Recebimentos e Pagamentos nos campos abaixo,</span>
    </div>
    <div id="divMensagCamposObrig" class="divMensagGenerica" style="margin-top: 2px;">
        <span>faça a análise de Recebimentos e Pagamentos, e efetive o Fechamento de Caixa </span>
    </div>
</div>
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
                <img title="Grava o registro atual na base de dados."
                        alt="Icone de Gravar o Registro." 
                        src="/BarrasFerramentas/Icones/Gravar_Desabilitado.png" />
            </li>
        </ul>
        <ul id="ulExcluirCancelar" style="width: 39px;">
            <li id="btnExcluir">
                <img title="Exclui o Registro atual selecionado."
                        alt="Icone de Excluir o Registro." 
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
            </li>
            <li id="btnCancelar">
                <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                        alt="Icone de Cancelar Operacao Atual." 
                        src="/BarrasFerramentas/Icones/Cancelar_Desabilitado.png" />
            </li>
        </ul>
        <ul id="ulAcoes" style="width: 39px;">
            <li  id="btnPesquisar">
                <asp:LinkButton ID="btnNewSearch" runat="server" CausesValidation="false" OnClick="btnNewSearch_Click">
                    <img title="Volta ao formulário de pesquisa."
                            alt="Icone de Pesquisa." 
                            src="/BarrasFerramentas/Icones/Pesquisar.png" />
                </asp:LinkButton>
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
        <li class="liNomeCaixa">
            <label title="Nome do Caixa">
                Caixa</label>
            <asp:TextBox ID="txtNomeCaixa" ToolTip="Nome do Caixa" style="background-color:#FFFFE0;" Enabled="false" CssClass="txtNomeCaixa" runat="server">
            </asp:TextBox>     
        </li>      
        <li class="liSiglaCaixa">
            <label title="Código do Caixa">
                Código Cx</label>
            <asp:TextBox ID="txtSiglaCaixa" Enabled="false" ToolTip="Código do Caixa" CssClass="txtSiglaCaixa" runat="server"></asp:TextBox>
        </li>        
        <li class="liSiglaCaixa" style="margin-left: 10px;">
            <label title="Data Movimento">
                Data Movto</label>
            <asp:TextBox ID="txtDataMovto" Enabled="false" style="background-color:#FFFFE0;" ToolTip="Data Movimento" CssClass="txtDataMovto" runat="server"></asp:TextBox>
        </li>  
        <li class="liSiglaCaixa" style="margin-left: 10px;">
            <label title="Operador de Caixa">
                Operador de Caixa</label>
            <asp:TextBox ID="txtNoOperCaixa" Enabled="false" ToolTip="Operador de Caixa" CssClass="campoNomePessoa" runat="server"></asp:TextBox>
        </li>        
        <li class="liSiglaCaixa" style="margin-left: 10px;">
            <label id="lblValor" runat="server" title="Valor de Abertura">Abertura R$</label>
            <asp:TextBox ID="txtValor" style="background-color:#FFFFE0;"
                ToolTip="Valor de Abertura do Caixa" Enabled="false"
                CssClass="txtMoney" runat="server"></asp:TextBox>
        </li>     
        <li class="liGridApoSan">
            <ul style="width: 100%;">
            <li style="width: 100%;text-align:center;text-transform:uppercase;background-color:#F8F8FF;padding-bottom: 2px; padding-top: 2px;">Aporte/Sangria</li>
            <li style="margin-top: -5px;">
                <div id="div2" runat="server" clientidmode="Static" class="divgrdNegociacao" style="height:88px;overflow-y: scroll;">
                <asp:GridView runat="server" ID="grdAporteSangria" CssClass="grdBusca" AutoGenerateColumns="False"
                    DataKeyNames="CO_OPER_CAIXA" Width="340px">
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum Aporte/Sangria registrado.<br />
                    </EmptyDataTemplate>
                    <RowStyle CssClass="rowStyle" />
                    <HeaderStyle CssClass="th" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <Columns>
                       <asp:TemplateField HeaderText="Nr">
                            <ItemTemplate>
                                <asp:Label ID="Labels" runat="server" > 
                                <%# Container.DataItemIndex + 1 %>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DT_CADASTRO" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy}">
                        </asp:BoundField>
                        <asp:BoundField DataField="HR_CADASTRO" HeaderText="HR">
                        </asp:BoundField>
                        <asp:BoundField DataField="TIPO" HeaderText="Tipo">
                        </asp:BoundField>
                        <asp:BoundField DataField="VALOR" DataFormatString="{0:N}" HeaderText="Valor">
                            <ItemStyle HorizontalAlign="Right" Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DE_RECEBIMENTO" HeaderText="Forma">
                        </asp:BoundField>
                        <asp:BoundField DataField="CO_MAT_COL" HeaderText="Matric">
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                </div>
            </li>
            <li style="width: 100%;text-align:center;text-transform:uppercase;background-color:#F5F5F5;margin-top:-5px;">
                <ul style="width: 100%;padding-top: 3px;">
                    <li style="width: 170px;text-align: left;padding-left: 5px;">
                        <asp:Label ID="lblTotAporte" runat="server"></asp:Label>
                        <asp:Label ID="lblValTotAporte" runat="server" style="color:Blue;clear:none;margin-left: 2px;"></asp:Label>
                    </li>
                    <li style="clear:none;width: 170px;text-align:right;">
                        <asp:Label ID="lblTotSangria" runat="server"></asp:Label>
                        <asp:Label ID="lblValTotSangria" runat="server" style="color:Red;clear:none;margin-left: 2px;"></asp:Label>
                    </li>
                </ul>
            </li>
            </ul>
        </li>
        <li class="liRecebimento">
            <ul style="width:100%;">
                <li style="width: 100%;text-align:center;text-transform:uppercase;background-color:#D1EEEE;margin-bottom:2px;padding-bottom: 2px;padding-top: 2px;">Recebimentos</li>
                <li style="width: 215px;text-align:center;text-transform:uppercase;background-color:#F8F8FF;margin-right:0px;">Informado</li>
                <li style="width: 119px;text-align:center;text-transform:uppercase;background-color:#EEE9BF; clear:none;margin-right:0px;">Análise</li>
                <li class="liGridForPag">
                    <div id="divGrdFormPagRec" runat="server" clientidmode="Static" class="divgrdNegociacao">
                    <asp:GridView runat="server" ID="grdFormPagRec" CssClass="grdBusca" AutoGenerateColumns="False"
                        DataKeyNames="CO_TIPO_REC" Width="215px">
                        <RowStyle CssClass="rowStyle" />
                        <HeaderStyle CssClass="th" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="DE_RECEBIMENTO" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="Left" Width="140px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="QTD">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQtdeFP" ToolTip="Informe a quantidade total de Recebimento do tipo informado" OnTextChanged="txtQtdeFPR_TextChanged" CssClass="txtQtdeFP" runat="server" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total R$">
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValorFP" ToolTip="Informe o valor total de Recebimento do tipo informado" OnTextChanged="txtValorFPR_TextChanged" CssClass="txtValorFP" runat="server" AutoPostBack="true" />
                                    <asp:HiddenField ID="hdCO_TIPO_REC" runat="server" Value='<%# bind("CO_TIPO_REC") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </li>
                <li class="liGridForPag" style="clear:none; margin-left: -7px;margin-right:0px;">
                    <div id="divgrdResulAnaRec" runat="server" clientidmode="Static" class="divgrdNegociacao">
                    <asp:GridView runat="server" ID="grdResulAnaRec" CssClass="grdBusca" AutoGenerateColumns="False"
                        DataKeyNames="CO_TIPO_REC" Width="100px">
                        <RowStyle CssClass="rowStyle" />
                        <HeaderStyle CssClass="th" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="QTD">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQtdeRA" Enabled="false" CssClass="txtQtdeRA" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor R$">
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValorRA" Enabled="false" CssClass="txtValorFP" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CK">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Image ID="imgValorFP" CssClass="imgValida" runat="server" />
                                    <asp:HiddenField ID="hdCO_TIPO_REC" runat="server" Value='<%# bind("CO_TIPO_REC") %>' />
                                    <asp:HiddenField ID="hdOcorValor" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </li>
                <li style="width: 212px;background-color:#F5F5F5;margin-top:-5px;padding-left: 5px;">   
                    <ul style="width: 100%;padding-top: 3px;">
                        <li style="width: 125px;">
                            <span>Total</span>
                        </li>
                        <li style="width: 10px;text-align: left; clear: none; margin-right: 0px;">
                            <asp:Label ID="lblTotQtdRec" runat="server"></asp:Label>
                        </li>
                        <li style="clear:none;width:65px;text-align:right; margin-right: 0px;">
                            <asp:Label ID="lblTotValRec" runat="server"></asp:Label>
                        </li>
                    </ul>                                     
                </li>
                <li id="li3" clientidmode="Static" runat="server" title="Clique para Fazer Análise de Recebimento do Caixa" class="liBtnAnaRecPag">            
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnAnaRecCaixa_Click">
                        <img class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Fazer Análise" />
                        <asp:Label runat="server" ID="Label3" Text="Fazer Análise"></asp:Label>            
                    </asp:LinkButton>
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                </li>
            </ul>
        </li>     
        <li class="liPagamento">
            <ul style="width:100%;">
                <li style="width: 100%;text-align:center;text-transform:uppercase;background-color:#FFF0F5;margin-bottom:2px;padding-bottom: 2px;padding-top: 2px;">Pagamentos</li>
                <li style="width: 215px;text-align:center;text-transform:uppercase;background-color:#F8F8FF;margin-right:0px;">Informado</li>
                <li style="width: 119px;text-align:center;text-transform:uppercase;background-color:#EEE9BF; clear:none;margin-right:0px;">Análise</li>
                <li class="liGridForPag">
                    <div id="divGrdFormPagPag" runat="server" clientidmode="Static" class="divgrdNegociacao">
                    <asp:GridView runat="server" ID="grdFormPagPag" CssClass="grdBusca" AutoGenerateColumns="False"
                        DataKeyNames="CO_TIPO_REC" Width="215px">
                        <RowStyle CssClass="rowStyle" />
                        <HeaderStyle CssClass="th" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="DE_RECEBIMENTO" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="Left" Width="140px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="QTD">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQtdeFP" ToolTip="Informe a quantidade total de Pagamento do tipo informado" OnTextChanged="txtQtdeFPP_TextChanged" CssClass="txtQtdeFP" runat="server" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total R$">
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValorFP" ToolTip="Informe o valor total de Pagamento do tipo informado" OnTextChanged="txtValorFPP_TextChanged" CssClass="txtValorFP" runat="server" AutoPostBack="true" />
                                    <asp:HiddenField ID="hdCO_TIPO_REC" runat="server" Value='<%# bind("CO_TIPO_REC") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </li>
                <li class="liGridForPag" style="clear:none; margin-left: -7px;margin-right:0px;">
                    <div id="divgrdResulAnaPag" runat="server" clientidmode="Static" class="divgrdNegociacao">
                    <asp:GridView runat="server" ID="grdResulAnaPag" CssClass="grdBusca" AutoGenerateColumns="False"
                        DataKeyNames="CO_TIPO_REC" Width="100px">
                        <RowStyle CssClass="rowStyle" />
                        <HeaderStyle CssClass="th" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="QTD">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQtdeRA" Enabled="false" CssClass="txtQtdeRA" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor R$">
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValorRA" Enabled="false" CssClass="txtValorFP" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CK">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Image ID="imgValorFP" CssClass="imgValida" runat="server" />
                                    <asp:HiddenField ID="hdCO_TIPO_REC" runat="server" Value='<%# bind("CO_TIPO_REC") %>' />
                                    <asp:HiddenField ID="hdOcorValor" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </li>
                <li style="width: 212px;background-color:#F5F5F5;margin-top:-5px;padding-left: 5px;">   
                    <ul style="width: 100%;padding-top: 3px;">
                        <li style="width: 125px;">
                            <span>Total</span>
                        </li>
                        <li style="width: 10px;text-align: left; clear: none; margin-right: 0px;">
                            <asp:Label ID="lblTotQtdPag" runat="server"></asp:Label>
                        </li>
                        <li style="clear:none;width:65px;text-align:right; margin-right: 0px;">
                            <asp:Label ID="lblTotValPag" runat="server"></asp:Label>
                        </li>
                    </ul>                                     
                </li>
                <li id="li2" clientidmode="Static" runat="server" title="Clique para Fazer Análise de Pagamento do Caixa" class="liBtnAnaRecPag">            
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="btnAnaPagCaixa_Click">
                        <img class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Fazer Análise" />
                        <asp:Label runat="server" ID="Label2" Text="Fazer Análise"></asp:Label>            
                    </asp:LinkButton>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </li>
            </ul>
        </li>     
        <li id="li1" clientidmode="Static" runat="server" title="Clique para Negociar" class="liBtnImpFecCaixa">
            <asp:LinkButton ID="LinkButton1" runat="server" Enabled="false" OnClick="btnRptInconsistencias_Click">
                <img id="imgRpt" class="imgliLnk" runat="server" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="INCONSISTÊNCIAS" />
                <asp:Label runat="server" ID="lblRpt" Text="INCONSISTÊNCIAS"></asp:Label> 
            </asp:LinkButton>
        </li>      
        <li id="libtnNegociacao" clientidmode="Static" runat="server" title="Clique para Fechar o Caixa" class="liBtnFecCaixa">            
            <asp:LinkButton ID="btnAnaCaixa" runat="server" OnClick="btnConfirmaCaixa_Click">
                <img class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="FECHAR CAIXA" />
                <asp:Label runat="server" ID="Label6" Text="FECHAR CAIXA"></asp:Label>            
            </asp:LinkButton>
            <asp:HiddenField ID="hdVerifPagCaixa" runat="server" />
            <asp:HiddenField ID="hdVerifRecCaixa" runat="server" />
            <asp:HiddenField ID="hdVerifRelac" runat="server" />
        </li>
</ul>
<script type="text/javascript">
    $(document).ready(function () {
        $(".txtQtdeFP").mask("?999");  
        $(".txtValorFP").maskMoney({ symbol: "", decimal: ",", thousands: "." });
    });
</script>
</asp:Content>