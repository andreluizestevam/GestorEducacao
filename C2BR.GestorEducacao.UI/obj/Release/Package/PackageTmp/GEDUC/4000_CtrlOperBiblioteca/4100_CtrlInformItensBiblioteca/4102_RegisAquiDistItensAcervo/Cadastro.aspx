<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4102_RegisAquiDistItensAcervo.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">    
    .ulDados{ width: 710px; margin-top:10px;}
    .ulDados input{ margin-bottom: 0;}
    input { font-family:Arial !important; }    
    input[type="text"] {font-size: 10px !important;font-family: Arial !important;}
    select {margin-bottom: 0;font-family: Arial !important;border: 1px solid #BBBBBB !important;font-size:0.9em !important;height: 15px !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 10px; margin-right: 10px;}
    .liBarraTitulo { background-color: #EEEEEE;margin-bottom: 2px; padding: 5px; text-align: center; width: 816px; height:10px; clear:both}
    .liClear { clear:both; }
    .liDoctoOrgao { clear:both; margin-left:10px; } 
    .liddlUnidadeBiblioteca{margin-left:5px;}
    .liBtnAdd{clear:both; margin-left:595px; margin-top:-20px; background-color:#EEEEEE; border:1px solid #CCCCCC;padding:2px;}
    
    /*--> CSS DADOS */                      
    .divGridView { position:absolute; margin-top:77px; margin-left:-64px; overflow-y: auto; height: 250px; }
    .grdItensAquisi { width: 824px; }
    .SpanPage { width:819px; text-align:center; margin-top:2px; }
    #tab01{ height: 282px; padding: 10px 0 0 10px; width: 870px; }    
    .ddlFornecedor { width:193px; }
    .ddlUF, .lblNumEmpenho, .lblNumNota { margin-left:10px; } 
    .txtNumNota, .txtNumEmpenho, .txtNumDocto {width:56px; margin-left:10px;}
    .txtSerieNota{width:37px;}
    .txtObsAcervo{width:516px;height:50px;}
    .txtNumAquisicao, .txtValor {width:55px; text-align: right;}
    .txtEmpenhoOrgao{width:150px; margin-left:10px;}
    .txtDoctoOrgao, .ddlAcervo {width:150px;}
    #tab02{ height: 350px; padding: 6px 0 0 10px; width: 870px; }
    #tab02 li input { margin-bottom: 0; }
    #tab02 li { margin-bottom: 5px; }        
    .txtQtdPaginas, .txtQtdItens {width:40px;}
    .btnLabel {width:100px;}
    .lblNumAcervAquisi{color:#666666;font-weight:bold;}
    .tabs .ui-widget-header { border: 0 !important; background: none !important; }
    
</style>
<script type="text/javascript">
    function SetCurrentSelectedTab(s) {
            $('.hiddenSelectedTab').val(s);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
 <div class="tabs">
    <ul>
        <li><a href="#tab01" onclick="SetCurrentSelectedTab(0)"><span>Controle Aquisição</span></a></li>
        <li><a href="#tab02" onclick="SetCurrentSelectedTab(1)"><span>Controle Itens</span></a></li>
        <li><input type="hidden" class="hiddenSelectedTab" runat="server" /></li>
    </ul>
    <div id="tab01">
        <ul id="ulDados" class="ulDados">
             <li id="liFornecedor">
                <label class="lblObrigatorio" for="ddlFornecedor">
                    Fornecedor</label>
                <asp:DropDownList ID="ddlFornecedor" CssClass="ddlFornecedor" runat="server" ToolTip="Selecione um Fornecedor">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="validatorField"
                ControlToValidate="ddlFornecedor" Text="*" 
                ErrorMessage="Campo Fornecedor é requerido"></asp:RequiredFieldValidator>
             </li>
             <li>
                <label class="lblObrigatorio" for="ddlUnidade">
                    Unidade/Escola</label>
                <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlUnidade" Text="*" 
                ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
             </li>
             <li>
                <label for="ddlTipo" class="lblObrigatorio" title="Tipo de Aquisição">Tipo Aquisição</label>
                <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo"
                     runat="server" AutoPostBack="true" onselectedindexchanged="ddlTipo_SelectedIndexChanged">
                    <asp:ListItem Value="">Selecione</asp:ListItem>
                    <asp:ListItem Value="C">Compra</asp:ListItem>
                    <asp:ListItem Value="D">Doação</asp:ListItem>
                    <asp:ListItem Value="T">Transferência</asp:ListItem>
                    <asp:ListItem Value="O">Outros</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
                    ControlToValidate="ddlTipo" Text="*" 
                    ErrorMessage="Campo Tipo de Aquisição é requerido"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="txtNumAquisicao" title="Número de Controle de Aquisição">Nº Ctrl Aquisição</label>
                <asp:TextBox ID="txtNumAquisicao" runat="server" CssClass="txtNumAquisicao"  Enabled="false" ToolTip="Número de Controle de Aquisição"></asp:TextBox>
            </li>
            <li>
                <fieldset id="fldInfoEmpenho">
                    <legend> Dados Empenho</legend>
                    <ul>
                        <li class="liClear">
                            <label for="txtNumEmpenho" class="lblNumEmpenho" title="Número do Empenho">Nº Empenho</label>
                            <asp:TextBox ID="txtNumEmpenho" runat="server" CssClass="txtNumEmpenho campoNumerico" ToolTip="Informe o Número do Empenho" MaxLength="10"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtDataEmpenho" title="Data do Empenho">Data Empenho</label>
                            <asp:TextBox ID="txtDataEmpenho" runat="server" CssClass="campoData" ToolTip="Informe a Data do Empenho"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtEmpenhoOrgao" class="lblNumEmpenho" title="Nome do orgão">Orgão</label>
                            <asp:TextBox ID="txtEmpenhoOrgao" runat="server" CssClass="txtEmpenhoOrgao" ToolTip="Nome do Orgão"></asp:TextBox>
                        </li>
                    </ul> 
                </fieldset>
            </li>
            <li>
                <fieldset id="fldInfoNota">
                <legend> Dados Nota Fiscal</legend>
                    <ul>
                        <li>
                            <label for="txtNumNota"  class="lblNumNota" title="Número da Nota fiscal">Nº Nota</label>
                            <asp:TextBox ID="txtNumNota" runat="server" CssClass="txtNumNota campoNumerico" ToolTip="Informe o Número da Nota Fiscal" MaxLength="10"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtSerieNota" title="Série da Nota Fiscal">Série</label>
                            <asp:TextBox ID="txtSerieNota" runat="server" CssClass="txtSerieNota campoNumerico" MaxLength="5" ToolTip="Informe a Série da Nota Fiscal"></asp:TextBox>
                        </li>
                        <li id="liUF" class="liClear" runat="server">
                            <label for="ddlUF" title="Estado da Nota Fiscal" class="lblNumNota">UF</label>
                            <asp:DropDownList ID="ddlUF" ToolTip="Selecione a UF" CssClass="ddlUF"
                                AutoPostBack="true" runat="server"></asp:DropDownList>
                        </li>
                        <li>
                            <label for="txtDataNota" title="Data da Nota Fiscal">Data da Nota</label>
                            <asp:TextBox ID="txtDataNota" runat="server" CssClass="campoData" ToolTip="Informe a Data da Nota Fiscal"></asp:TextBox>
                        </li>
                    </ul>
                </fieldset>
            </li>
            <li>
                <fieldset id="fldDoctoControle">
                <legend> Docto Controle</legend>
                    <ul>
                        <li>
                            <label for="txtNumDocto"  class="lblNumNota" title="Número do Documento">Nº Docto</label>
                            <asp:TextBox ID="txtNumDocto" runat="server" CssClass="txtNumDocto campoNumerico" ToolTip="Informe o Número do Documento" MaxLength="10"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtDataDocto" title="Data do Documento">Data do Docto</label>
                            <asp:TextBox ID="txtDataDocto" runat="server" CssClass="campoData" ToolTip="Informe a Data do Documento"></asp:TextBox>
                        </li>
                        <li>
                            <label for="ddlTipoDocto" title="Tipo de Documento" class="lblNumNota">Tipo Docto</label>
                            <asp:DropDownList ID="ddlTipoDocto" ToolTip="Selecione o Tipo de Documento"
                                 runat="server">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="R">Recibo</asp:ListItem>
                                <asp:ListItem Value="M">Memorando</asp:ListItem>
                                <asp:ListItem Value="O">Ofício</asp:ListItem>
                                <asp:ListItem Value="D">Declaração</asp:ListItem>
                                <asp:ListItem Value="O">Outros</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li class="liDoctoOrgao">
                            <label for="txtDoctoOrgao" title="Nome do orgão do Documento">Orgão</label>
                            <asp:TextBox ID="txtDoctoOrgao" runat="server" CssClass="txtDoctoOrgao" ToolTip="Nome do orgão do Documento"></asp:TextBox>
                        </li>
                    </ul>
                </fieldset>
            </li>
            <li>
                <label for="txtObsAcervo" title="Observação">Observação</label>
                <asp:TextBox ID="txtObsAcervo" runat="server" CssClass="txtObsAcervo" 
                    ToolTip="Informe a Observação" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 200);"></asp:TextBox>
            </li>
            <li>
                <label for="txtDataCad" title="Data de Cadastro">Data de Cadastro</label>
                <asp:TextBox ID="txtDataCad" runat="server" CssClass="campoData" Enabled="false" ToolTip="Data de Cadastro"></asp:TextBox>
            </li>
        </ul>
    </div>
    <div id="tab02">
        <ul id="ulDados2" class="ulDados">
            <li>
                <label for="txtNumAcervAquisi" title="Número do Acervo Aquisição">Nº Aquisição</label>
                <asp:Label ID="lblNumAcervAquisi" runat="server" CssClass="lblNumAcervAquisi" ToolTip="Número do Acervo Aquisição"></asp:Label>                
            </li>
            <li>
                <label for="ddlAcervo" title="Selecione um Acervo" class="lblObrigatorio">Acervo</label>
                <asp:DropDownList ID="ddlAcervo" CssClass="ddlAcervo" ToolTip="Acervo" runat="server">
                </asp:DropDownList>
            </li>
            <li class="liddlUnidadeBiblioteca">
                <label for="ddlUnidadeBiblioteca" title="Unidade/Biblioteca" class="lblObrigatorio">Unidade/Biblioteca</label>
                <asp:DropDownList ID="ddlUnidadeBiblioteca" CssClass="ddlUnidadeEscolar"
                    ToolTip="Unidade/Biblioteca" runat="server">
                </asp:DropDownList>
            </li>
            <li>
                <label for="txtValor" title="Valor Unitário do Item de Acervo" class="lblObrigatorio">Valor R$</label>
                <asp:TextBox ID="txtValor" runat="server" CssClass="money txtValor"
                    ToolTip="Valor do Ítem"></asp:TextBox>
            </li>            
            <li>
                <label for="ddlEstadoConservacao" class="lblObrigatorio" title="Estado de Conservação do Ítem">Estado</label>
                <asp:DropDownList ID="ddlEstadoConservacao" runat="server" CssClass="ddlEstadoConservacao"
                    ToolTip="Selecione o Estado de Conservação">
                    <asp:ListItem Value="O">Ótimo</asp:ListItem>
                    <asp:ListItem Value="B">Bom</asp:ListItem>
                    <asp:ListItem Value="R">Ruim</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
                    runat="server" ControlToValidate="ddlEstadoConservacao"
                    ErrorMessage="Estado de Conservação deve ser informado">
                </asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="txtQtdPaginas" title="Quantidade de Páginas" class="lblObrigatorio">Qtd Pág.</label>
                <asp:TextBox ID="txtQtdPaginas" runat="server" CssClass="txtQtdPaginas"
                    ToolTip="Informe a Quantidade de Páginas"></asp:TextBox>
            </li>
            <li>
                <label for="txtQtdItens" title="Quantidade de Itens" class="lblObrigatorio">Qtd Ítens</label>
                <asp:TextBox ID="txtQtdItens" runat="server" CssClass="txtQtdPaginas"
                    ToolTip="Informe a Quantidade de Ítens"></asp:TextBox>
            </li>
            <li>
                <label for="txtLocalEnd" title="Local onde se encontra o Ítem">Local Ítem</label>
                <asp:TextBox ID="txtLocalEnd" runat="server" CssClass="txtLocalEnd"
                    ToolTip="Informe o Local onde o item está Localizado"></asp:TextBox>
            </li>
            <li>
                <label for="txtLocalEnd1" title="Endereço onde se encontra o Ítem">End1. Ítem</label>
                <asp:TextBox ID="txtLocalEnd1" runat="server" CssClass="txtLocalEnd1"
                    ToolTip="Informe O Endereço onde se encontra o Ítem"></asp:TextBox>
            </li>
            <li>
                <label for="txtLocalEnd2" title="Endereço onde se encontra o Ítem">End2. Ítem</label>
                <asp:TextBox ID="txtLocalEnd2" runat="server" CssClass="txtLocalEnd2"
                    ToolTip="Informe O Endereço onde se encontra o Ítem"></asp:TextBox>
            </li>
            <li>
                <label for="txtLocalEnd3" title="Endereço onde se encontra o Ítem">End3. Ítem</label>
                <asp:TextBox ID="txtLocalEnd3" runat="server" CssClass="txtLocalEnd3"
                    ToolTip="Informe O Endereço onde se encontra o Ítem"></asp:TextBox>
            </li>
            <li id="liAddItens" runat="server" title="Clique para Adicionar os Itens Aquisição" class="liBtnAdd">
                <img alt="" title="Adicionar Itens Aquisição" src="../../../../Library/IMG/Gestor_SaudeEscolar.png" height="15px" width="15px" />
                <asp:LinkButton ID="btnAddItens" runat="server" class="btnLabel" OnClick="imgAdd_Click">Adicionar Itens</asp:LinkButton>
            </li>
            <div id="divGridView" runat="server" class="divGridView">
            <label class="liBarraTitulo">GRADE DE ITENS DE AQUISIÇÂO ACERVO</label>
            <asp:GridView ID="grdItensAquisi" runat="server" CssClass="grdItensAquisi grdBusca" AutoGenerateColumns="False"
                DataKeyNames="CO_ACERVO_ITENS" AllowPaging="True" GridLines="Vertical" OnPageIndexChanging="grdItensAquisi_PageIndexChanging" OnDataBound="grdItensAquisi_DataBound" >
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhuma registro encontrado.<br />
                </EmptyDataTemplate>
                <PagerStyle CssClass="grdFooter" />
                <Columns>
                    <asp:BoundField DataField="NO_ACERVO" HeaderText="Acervo">
                    <HeaderStyle Width="360px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_ISBN_ACER" HeaderText="ISBN">
                    <HeaderStyle Width="110px" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NO_FANTAS_EMP" HeaderText="Empresa">
                    <HeaderStyle Width="360px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_ACERVO_ITENS" HeaderText="Item" >
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_ESTADO_ACERVO_ITENS" HeaderText="Estado" >                    
                    <HeaderStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NU_PAGINA_ACERVO_ITENS" HeaderText="Páginas" >
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_CTRL_INTERNO" HeaderText="Cod.Barras" >
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VL_ACERVO_ITENS" HeaderText="Valor R$" >
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <PagerTemplate>
                    <table id="tblGridFooter">
                        <tr>
                            <td>
                                <label class="SpanPage">Página:&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlGrdPages" OnSelectedIndexChanged="ddlGrdPages_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    &nbsp;de
                                    <%# grdItensAquisi.PageCount%></label>
                            </td>
                        </tr>
                    </table>
                </PagerTemplate>
            </asp:GridView>
        </div>
        </ul>
    </div>
</div>        
<script type="text/javascript">
    $(document).ready(function() {
        $(".txtNumDocto").mask("?9999999999");
        $(".txtNumNota").mask("?9999999999");
        $(".txtNumEmpenho").mask("?9999999999");
        $(".txtQtdPaginas").mask("?99999");
        $(".money").maskMoney({ symbol: "", decimal: ",", thousands: "." });        
        $('.tabs').tabs({ selected: $('.hiddenSelectedTab').val()});                
    });
</script>
</asp:Content>
